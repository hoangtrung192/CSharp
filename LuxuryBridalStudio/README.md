# Luxury Bridal Studio

Windows desktop application for managing a bridal rental studio, built with WinForms, .NET Framework 4.7.2, and SQL Server. It manages customers, products, suppliers, staff, rentals, returns, stock receipts, and cash-flow reports.

## Requirements

- Windows.
- Git to clone the repository, or download and extract the repository ZIP from GitHub.
- Visual Studio with the **.NET desktop development** workload and the **.NET Framework 4.7.2 targeting pack**. MSBuild with the same targeting pack can also build the project.
- A running SQL Server instance: a local default instance, SQL Server Express, or SQL Server Express LocalDB.
- A Windows account allowed to create a database on that instance during setup, and access the application database afterward.

SQL Server Management Studio is optional. The included PowerShell setup script does not require SSMS or `sqlcmd`.

## 1. Get the project

Run in PowerShell from the folder where you want to keep the code:

```powershell
git clone https://github.com/hoangtrung192/CSharp.git
cd .\CSharp\LuxuryBridalStudio
```

If you already cloned the repository, open its `LuxuryBridalStudio` folder instead. Run the remaining PowerShell commands from that folder, where `LuxuryBridalStudio.csproj` is located. The project does not need to be under `F:\DataReport`.

## 2. Choose your SQL Server instance

Use the same server name in the database setup command and the `Data Source` setting in [App.config](App.config).

| SQL Server installation | Server name |
| --- | --- |
| Local default instance (the project default) | `.` |
| Express named instance, if installed with this name | `.\SQLEXPRESS` |
| LocalDB default instance, if installed | `(localdb)\MSSQLLocalDB` |

The default application connection string is:

```text
Data Source=.;Initial Catalog=LuxuryBridalStudio;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;Connect Timeout=10
```

For example, with SQL Server Express, change only `Data Source=.` to `Data Source=.\SQLEXPRESS`. `Integrated Security=True` uses the Windows account running the app; it does not use your app login. The trusted certificate setting is for local development; see [database notes](Database/README.md) for deployment details.

## 3. Create the database

For the default local SQL Server instance:

```powershell
.\Database\Initialize-Database.ps1 -ServerInstance '.' -SeedDemo
```

For Express or LocalDB, use the matching command instead:

```powershell
# SQL Server Express
.\Database\Initialize-Database.ps1 -ServerInstance '.\SQLEXPRESS' -SeedDemo

# SQL Server Express LocalDB
.\Database\Initialize-Database.ps1 -ServerInstance '(localdb)\MSSQLLocalDB' -SeedDemo
```

Run only the command for your instance. This creates the `LuxuryBridalStudio` database and its tables. `-SeedDemo` adds sample customers, products, and suppliers; omit it to start with empty catalogs. It does not create login accounts, rentals, or receipts.

The script can be rerun: it skips existing tables and catalog codes rather than resetting the database. It is not a migration tool for future schema changes. For the SQL script order and more details, see [Database/README.md](Database/README.md).

## 4. Build and run

### Using Visual Studio

1. Open `LuxuryBridalStudio.csproj` in Visual Studio.
2. Build the project.
3. Press **F5** to run, or **Ctrl+F5** to run without debugging.

### Using the command line

Open **Developer PowerShell for Visual Studio**, change to the project folder, then run:

```powershell
msbuild LuxuryBridalStudio.csproj /t:Build /p:Configuration=Debug
& .\bin\Debug\LuxuryBridalStudio.exe
```

After changing `App.config`, rebuild so the executable receives the updated settings.

## 5. Create your first account

On the first launch with an empty accounts table, the setup dialog asks you to create a manager account. Choose a password containing letters and numbers, between 10 and 128 characters. There is no default username or password.

Sign in with that account. Later registrations create employee accounts; users cannot choose their own manager permissions. Staff profiles are separate from app login permissions.

## Working with teammates

GitHub shares the source code and database setup scripts. It does not host SQL Server or synchronize your current database records.

- **Separate development databases:** each teammate installs SQL Server, runs the setup script, and creates their own manager account. Everyone gets the same structure, with independent data.
- **One shared database:** initialize it once on a SQL Server accessible to the team, point each app at that server, and grant each connecting Windows identity database access. This also requires network access and compatible Windows authentication. Running the local setup steps alone does not configure a shared server.
- **Existing data:** use a separate SQL Server backup/restore process if a teammate needs a copy of your current records. The demo seed does not copy your data.

The app needs a working database connection to start. Saved records remain after closing the app, and are not reset on launch. Product images currently use local file paths; share the image files and use accessible paths when working on multiple computers.

## Troubleshooting

| Problem | What to check |
| --- | --- |
| Cannot connect to SQL Server | Ensure your instance is installed and running. Match `Data Source` in `App.config` to the instance used for setup, then rebuild. |
| Database or table not found | Run `Initialize-Database.ps1` on the same instance as the app. |
| Windows login or permission error | Ask the SQL Server administrator to grant your Windows account database access; setup also requires database creation permission. |
| PowerShell blocks the setup script | Run the one-command example below, using your actual server name. |
| `msbuild` is not recognized | Use Developer PowerShell for Visual Studio, or build inside Visual Studio. |
| .NET Framework reference assemblies are missing | Install the .NET Framework 4.7.2 targeting pack through Visual Studio Installer. |
| Product images do not appear on another computer | Copy the image files and update the saved paths to locations that computer can access. |

If PowerShell blocks the script, this runs it with a policy override for that new PowerShell process:

```powershell
powershell.exe -NoProfile -ExecutionPolicy Bypass -File .\Database\Initialize-Database.ps1 -ServerInstance '.' -SeedDemo
```

## Project layout

```text
LuxuryBridalStudio/
  App.config                  SQL Server connection settings
  Program.cs                  Application startup
  LuxuryBridalStudio.csproj    Visual Studio project
  Database/                   Database setup, schema, seed, and verification
  Features/
    DangKy/                   Registration form and designer
    DangNhap/                 Login form, designer, and resources
    InitialSetup/             First manager account setup form
    ...                       Other feature screens and services
  Models/                     Data models
  Shared/                     Shared UI, validation, authentication, and SQL storage
  Tests/                      Regression and SQL integration tests
```

The feature, business-rule, and test notes below are in Vietnamese.

## Quy trình đã nối

1. Thêm khách hàng, sản phẩm và nhà cung cấp từ danh mục.
2. Lập phiếu nhập, chọn sản phẩm và đơn giá. Cùng sản phẩm khác đơn giá được giữ thành các dòng riêng. Lưu phiếu cập nhật tổng số bộ và lịch sử nhập trong SQL; phiếu mới bắt đầu trống.
3. Lập hợp đồng: chọn khách và nhiều sản phẩm, thêm số lượng vào bảng, chọn ngày thuê/trả và tiền thu trước. Giá lấy từ danh mục; hợp đồng giữ bản chụp tên, loại và đơn giá.
4. Chuyển sang Trả đồ để chọn hợp đồng vừa lập, nhập phụ phí và lý do, xác nhận nhận đủ đồ và thu đủ tiền. Không thể trả hai lần hoặc trả hợp đồng có ngày bắt đầu ở tương lai.
5. Mở Tổng quan/Báo cáo để xem thu, hoàn và thực thu phát sinh. CSV xuất đúng bảng đang lọc/sắp xếp, có BOM UTF-8 và bảo vệ ô bắt đầu bằng ký tự công thức.

Hợp đồng chưa thu tiền được hủy bởi nhân viên hoặc quản lý. Hợp đồng đã thu cần quản lý nhập lý do và xác nhận đã hoàn đủ tiền; giao dịch hoàn được ghi riêng. Hủy hoặc trả sẽ giải phóng số lượng đã đặt.

## Quy tắc nghiệp vụ

- **Tổng số bộ** là tổng số lượng sở hữu, gồm cả các bộ đang thuê. Số sẵn sàng được tính từ tổng số bộ trừ lượng thuê đồng thời cao nhất trong khoảng ngày được chọn. Không cộng dồn các lịch không giao nhau.
- Ngày bắt đầu/kết thúc đều tính vào khoảng thuê. Hợp đồng quá hạn chưa trả tiếp tục chiếm số lượng cho đến khi nhận lại; không tự giả định khách sẽ trả hôm nay.
- Ngày thuê mới phải từ hôm nay. Ngày trả thực tế phải từ ngày thuê đến hôm nay.
- Tiền dùng `decimal`, là số nguyên VNĐ. Bộ nhập tiền chấp nhận `3500000`, `3.500.000` hoặc `3,500,000`, không phụ thuộc thiết lập vùng của Windows.
- Mã danh mục có khóa duy nhất và lấy số lớn nhất đang có cộng một. Chỉ tạo mã thật khi lưu; mã trên form thêm mới là dự kiến.
- Điện thoại được chuẩn hóa và kiểm tra trùng; CCCD có thể trống hoặc gồm 12 chữ số và không trùng.
- Không xóa khách/sản phẩm/nhà cung cấp đã có giao dịch. Không giảm tổng số bộ xuống dưới số đã được đặt trong lịch thuê.
- Thời điểm ghi nhận tiền là ngày thu/hoàn thực tế, kể cả khi nhập ngày trả đồ cũ. Tiền của hợp đồng nhiều loại được phân bổ theo giá trị dòng; sai số làm tròn dồn vào dòng cuối, bảo toàn tổng tiền.
- Báo cáo thể hiện dòng tiền, không phải lợi nhuận. Mỗi dòng phân bổ theo loại là một dòng giao dịch trên báo cáo.

## Phân quyền

| Chức năng | Quản lý | Nhân viên | Thủ kho |
|---|---|---|---|
| Khách hàng, hợp đồng, trả đồ | Có | Có | Không |
| Xem sản phẩm | Có | Có | Có |
| Sửa sản phẩm, nhà cung cấp, nhập kho | Có | Không | Có |
| Nhân sự, báo cáo | Có | Không | Không |
| Hủy và hoàn tiền hợp đồng đã thu | Có | Không | Không |

Quyền được kiểm tra tại service và khi mở chức năng. Tổng quan của Thủ kho ẩn bảng khách hàng và số liệu thu tiền; Nhân viên không xem giá nhập trên màn hình sản phẩm.

## Lưu trữ SQL

Schema, script khởi tạo và hướng dẫn kết nối nằm trong [Database/README.md](Database/README.md).

- `SqlAccountService` thực hiện đăng nhập/đăng ký và tạo quản lý ban đầu trong SQL. Tài khoản bị khóa hoặc thay đổi quyền không được tiếp tục ghi dữ liệu bằng phiên cũ.
- `SqlStudioStore` nạp các bảng vào `StudioWorkspace`, giữ nguyên các collection đang được form bind và chỉ ghi những dòng thay đổi. Các tham số SQL được truyền riêng với câu lệnh.
- Mỗi thao tác ghi dùng một transaction `Serializable` và `sp_getapplock` dùng chung giữa các phiên ứng dụng. Service kiểm tra nghiệp vụ trên dữ liệu mới nhất trước khi lưu; hợp đồng + chi tiết + tiền, trả + thu, hủy + hoàn, nhập kho + số lượng được commit cùng nhau. Lỗi SQL không làm thay đổi workspace đang hiển thị.
- Mã mới được tính khi giữ khóa ghi SQL; hai phiên không cấp cùng mã. `rowversion` từ chối ghi đè bản ghi đã bị phiên khác sửa. Giá sản phẩm thay đổi sau khi mở hợp đồng yêu cầu tải lại trước khi lưu.
- Đăng nhập, chuyển chức năng, mở Tổng quan và làm mới Báo cáo sẽ đọc lại dữ liệu SQL. Đây là cách phù hợp cho studio nhỏ: mỗi lần lưu nạp toàn bộ dữ liệu, các thao tác ghi được tuần tự hóa. Dữ liệu lớn cần repository/query riêng và phân trang.

Hiện chưa hỗ trợ trả một phần hợp đồng, giặt/sửa chữa, giảm giá, thanh toán nhiều đợt ngoài thu trước + quyết toán, in hợp đồng hoặc tự cập nhật giao diện theo thời gian thực giữa các máy. Ảnh sản phẩm hiện là đường dẫn file cục bộ; ảnh được đọc thành bản sao để không khóa file. Khi triển khai nhiều máy cần kho ảnh dùng chung hoặc sao chép ảnh vào nơi quản lý cố định.

## Kiểm tra hồi quy

Không cần cài thêm thư viện kiểm thử:

```powershell
msbuild Tests\RegressionTests.csproj /t:Build /p:Configuration=Debug
& .\bin\Tests\LuxuryBridalStudio.Tests.exe
```

Bộ kiểm tra bao gồm xác thực/phân quyền; chuẩn hóa tiền và tìm ký tự đặc biệt; mã không trùng sau xóa; nhập kho nhiều đơn giá; hợp đồng nhiều sản phẩm; lịch giao nhau; trả, hủy và hoàn tiền; từ chối thao tác không hợp lệ mà không sửa dữ liệu; cùng vòng đời khởi tạo các form. Các form kiểm tra chạy trong cửa sổ trong suốt ngoài màn hình; ảnh xem bố cục nằm tại `bin/Tests/previews/`.

Kiểm tra tích hợp SQL dùng database tạm có tên GUID, tự xóa sau khi hoàn tất và không thay đổi database ứng dụng:

```powershell
msbuild Tests\SqlIntegrationTests.csproj /t:Build /p:Configuration=Debug
& .\bin\SqlTests\LuxuryBridalStudio.SqlTests.exe
```

Chạy từ thư mục project bằng tài khoản Windows có quyền tạo/xóa database. Kiểm tra bao gồm lưu/đọc lại, rollback khi giao dịch tiền thất bại, đăng nhập sau khi nạp lại, quyền, chỉnh sửa cũ và đặt thuê đồng thời.
