# LuxuryBridalStudio database

SQL Server schema for the existing WinForms models. The default setup targets the installed local SQL Server (`.`), uses Windows authentication and creates a database named `LuxuryBridalStudio`. New databases use a case-insensitive collation, including login names; an existing database should also use a case-insensitive collation.

## Create the database

From the project folder in PowerShell:

```powershell
.\Database\Initialize-Database.ps1 -ServerInstance '.' -SeedDemo
```

For LocalDB, use `-ServerInstance '(localdb)\MSSQLLocalDB'` instead. Omit `-SeedDemo` for empty catalogs. The scripts never drop an existing database, recreate existing tables, or overwrite existing catalog rows. Schema version 1 is recorded in `dbo.SchemaVersions`; future structural changes need new migrations.

You can also run `001_CreateDatabase.sql`, `002_Schema.sql`, and optionally `003_SeedDemoCatalog.sql` in that order in SQL Server Management Studio. A Windows account with permission to create the database is required for setup.

## Tables and relationships

| Tables | Existing app data |
| --- | --- |
| `Roles`, `Accounts` | Login identities, permissions and PBKDF2-SHA256 hashes/salts/iterations |
| `Employees` | `StaffMember`; employment role is separate from login permissions |
| `Customers`, `Suppliers`, `Products` | The three `StudioWorkspace` catalog tables |
| `RentalContracts`, `RentalLines` | `RentalContract` and its product/price snapshots |
| `StockReceipts`, `StockReceiptLines` | `StockReceipt` and its purchase lines |
| `CashTransactions` | `GiaoDichThongKe`; one collection/refund allocation per rental line |

`Customers` → `RentalContracts` → `RentalLines` → `CashTransactions`.
`Suppliers` → `StockReceipts` → `StockReceiptLines`.
Both detail tables reference `Products`; stock receipts also reference the recording login in `Accounts`.

Vietnamese text uses `nvarchar`. Amounts use whole VND (`decimal(18,0)`); callers must validate fractional input before SQL conversion, which rounds to the column scale. Product quantity means total units owned, including units currently rented. Names, categories and unit prices on rental and receipt lines are historical snapshots. Foreign keys prevent deleting referenced history. Customer identity numbers must use SQL `NULL` when absent, rather than an empty string.

`vw_ThongKe` exposes `Ma`, `Ngay`, `KhachHang`, `Nhom`, `NoiDung`, `Thu`, `Hoan`, and `ThucThu` for the statistics screen. `vw_DailyCashFlow` groups receipts and refunds by date and category. These are cash-flow reports, not profit reports. Zero-value allocations are allowed because the current app can produce them when splitting a small payment across multiple lines.

Demo seeding adds the same five customers, products and suppliers as the app. It does not create login accounts, staff, rentals, cash transactions or receipts. Existing codes are skipped; conflicting unique phone/identity values cause the seed transaction to roll back.

## Verify

```powershell
sqlcmd -S . -E -C -b -f 65001 -i .\Database\Verify-Database.sql
```

The verification checks constraints and a collection/refund reporting scenario inside a transaction, then rolls all test rows back. Run this on a development database; it reserves codes starting with `__test_`.

## Running the connected app

`Program.cs` loads `SqlAccountService` and `SqlStudioStore` using the `LuxuryBridalStudio` connection string in `App.config`. Catalogs, staff, rentals, complete rental lines, receipts and report transactions are loaded from SQL. Saves persist immediately, so closing and reopening the app retains data. The app shows a connection error if SQL is unavailable; it does not silently switch to temporary storage.

The default connection string is:

```text
Data Source=.;Initial Catalog=LuxuryBridalStudio;Integrated Security=True;Encrypt=True;TrustServerCertificate=True
```

`TrustServerCertificate=True` is for the local development server's self-signed certificate. Configure certificate validation for a deployed server.

On the first launch, when `Accounts` is empty, create your manager username and password in the setup dialog. No shared default password is installed. Later registration always creates an employee login. Staff profiles remain separate from account permissions.

Each save takes the `LuxuryBridalStudio.Workspace` database application lock, reads current data in a `Serializable` transaction, runs the existing domain service and writes only changed rows with parameterized SQL. Rentals and payment allocations, returns and collections, cancellations and refunds, and receipts and stock changes are committed together. Concurrent app instances cannot allocate the same new code or overbook stock. `rowversion` rejects stale edits. A stale product selection cannot silently use a changed rental price.

The workspace is updated only after a successful commit and retains the tables/lists bound to forms. Data refreshes on login, feature navigation, overview and report refresh. Active forms do not receive automatic updates from other machines.

This implementation loads the full workspace and serializes writes, which is intended for a small studio. Larger deployments need targeted queries and pagination. SQL check constraints do not enforce cross-table totals or rental availability themselves; these rules are enforced by app services within the locked transaction. Direct database edits bypass application rules and locks.

The Windows identity running the app needs access to this database. App roles are enforced by the application and are separate from SQL Server permissions. Account activation/role changes are checked again on every save. There is not yet an account administration screen; changing a staff profile does not change a login's role.

## Integration tests

```powershell
msbuild Tests\SqlIntegrationTests.csproj /t:Build /p:Configuration=Debug
& .\bin\SqlTests\LuxuryBridalStudio.SqlTests.exe
```

Run from the project folder with Windows permission to create and remove a database on `.`. Tests use a newly created GUID-named database, validate persistence, authorization, rollback and concurrent sessions, then remove only that test database. They do not write test users or transactions into `LuxuryBridalStudio`.
