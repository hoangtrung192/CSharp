USE [LuxuryBridalStudio];
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET ANSI_PADDING ON;
SET ANSI_WARNINGS ON;
SET CONCAT_NULL_YIELDS_NULL ON;
SET ARITHABORT ON;
SET NUMERIC_ROUNDABORT OFF;
SET NOCOUNT ON;
SET XACT_ABORT ON;
BEGIN TRY
    BEGIN TRANSACTION;
    DECLARE @lockResult int;
    EXEC @lockResult = sys.sp_getapplock
        @Resource = N'LuxuryBridalStudio.Schema', @LockMode = 'Exclusive', @LockOwner = 'Transaction', @LockTimeout = 15000;
    IF @lockResult < 0 THROW 51000, 'Could not acquire the database migration lock.', 1;

    IF OBJECT_ID(N'dbo.SchemaVersions', N'U') IS NOT NULL
    BEGIN
        IF NOT EXISTS (SELECT 1 FROM dbo.SchemaVersions WHERE Version = 1)
            THROW 51001, 'Unrecognized schema version; review the database before continuing.', 1;
        COMMIT;
        RETURN;
    END;

    CREATE TABLE dbo.SchemaVersions (
        Version int NOT NULL CONSTRAINT PK_SchemaVersions PRIMARY KEY,
        AppliedAtUtc datetime2(0) NOT NULL CONSTRAINT DF_SchemaVersions_Applied DEFAULT SYSUTCDATETIME()
    );
    CREATE TABLE dbo.Roles (
        RoleName nvarchar(30) NOT NULL CONSTRAINT PK_Roles PRIMARY KEY
    );
    INSERT dbo.Roles (RoleName) VALUES (N'Quản lý'), (N'Nhân viên'), (N'Thủ kho');

    -- Staff profiles and login permissions remain separate, as in the current app.
    CREATE TABLE dbo.Employees (
        EmployeeCode nvarchar(20) NOT NULL CONSTRAINT PK_Employees PRIMARY KEY,
        FullName nvarchar(150) NOT NULL,
        Phone varchar(11) NOT NULL CONSTRAINT UQ_Employees_Phone UNIQUE,
        RoleName nvarchar(30) NOT NULL CONSTRAINT FK_Employees_Roles REFERENCES dbo.Roles(RoleName),
        IsActive bit NOT NULL CONSTRAINT DF_Employees_IsActive DEFAULT 1,
        RowVersion rowversion NOT NULL,
        CONSTRAINT CK_Employees_Name CHECK (LEN(TRIM(FullName)) BETWEEN 2 AND 150),
        CONSTRAINT CK_Employees_Phone CHECK (LEN(Phone) BETWEEN 9 AND 11 AND Phone COLLATE Latin1_General_100_BIN2 NOT LIKE '%[^0-9]%')
    );
    CREATE TABLE dbo.Accounts (
        UserName varchar(50) COLLATE DATABASE_DEFAULT NOT NULL CONSTRAINT PK_Accounts PRIMARY KEY,
        DisplayName nvarchar(150) NOT NULL,
        RoleName nvarchar(30) NOT NULL CONSTRAINT DF_Accounts_Role DEFAULT N'Nhân viên'
            CONSTRAINT FK_Accounts_Roles REFERENCES dbo.Roles(RoleName),
        EmployeeCode nvarchar(20) NULL CONSTRAINT FK_Accounts_Employees REFERENCES dbo.Employees(EmployeeCode),
        PasswordHash binary(32) NOT NULL,
        PasswordSalt binary(16) NOT NULL,
        PasswordIterations int NOT NULL CONSTRAINT DF_Accounts_Iterations DEFAULT 100000,
        PasswordAlgorithm varchar(30) NOT NULL CONSTRAINT DF_Accounts_Algorithm DEFAULT 'PBKDF2-SHA256',
        IsActive bit NOT NULL CONSTRAINT DF_Accounts_IsActive DEFAULT 1,
        CreatedAtUtc datetime2(0) NOT NULL CONSTRAINT DF_Accounts_Created DEFAULT SYSUTCDATETIME(),
        RowVersion rowversion NOT NULL,
        CONSTRAINT CK_Accounts_UserName CHECK (LEN(UserName) BETWEEN 3 AND 50 AND UserName COLLATE Latin1_General_100_BIN2 NOT LIKE '%[^A-Za-z0-9_.-]%'),
        CONSTRAINT CK_Accounts_Hashing CHECK (PasswordIterations >= 100000 AND PasswordAlgorithm = 'PBKDF2-SHA256')
    );
    CREATE UNIQUE INDEX UX_Accounts_Employee ON dbo.Accounts(EmployeeCode) WHERE EmployeeCode IS NOT NULL;

    CREATE TABLE dbo.Customers (
        CustomerCode nvarchar(20) NOT NULL CONSTRAINT PK_Customers PRIMARY KEY,
        FullName nvarchar(150) NOT NULL,
        Phone varchar(11) NOT NULL CONSTRAINT UQ_Customers_Phone UNIQUE,
        IdentityNumber varchar(12) NULL,
        Address nvarchar(300) NOT NULL CONSTRAINT DF_Customers_Address DEFAULT N'',
        RowVersion rowversion NOT NULL,
        CONSTRAINT CK_Customers_Name CHECK (LEN(TRIM(FullName)) BETWEEN 2 AND 150),
        CONSTRAINT CK_Customers_Phone CHECK (LEN(Phone) BETWEEN 9 AND 11 AND Phone COLLATE Latin1_General_100_BIN2 NOT LIKE '%[^0-9]%'),
        CONSTRAINT CK_Customers_Identity CHECK (IdentityNumber IS NULL OR (LEN(IdentityNumber) = 12 AND IdentityNumber COLLATE Latin1_General_100_BIN2 NOT LIKE '%[^0-9]%'))
    );
    CREATE UNIQUE INDEX UX_Customers_Identity ON dbo.Customers(IdentityNumber) WHERE IdentityNumber IS NOT NULL;
    CREATE TABLE dbo.Suppliers (
        SupplierCode nvarchar(20) NOT NULL CONSTRAINT PK_Suppliers PRIMARY KEY,
        SupplierName nvarchar(150) NOT NULL,
        Phone varchar(11) NOT NULL CONSTRAINT UQ_Suppliers_Phone UNIQUE,
        Address nvarchar(300) NOT NULL CONSTRAINT DF_Suppliers_Address DEFAULT N'',
        Notes nvarchar(1000) NOT NULL CONSTRAINT DF_Suppliers_Notes DEFAULT N'',
        RowVersion rowversion NOT NULL,
        CONSTRAINT CK_Suppliers_Name CHECK (LEN(TRIM(SupplierName)) BETWEEN 2 AND 150),
        CONSTRAINT CK_Suppliers_Phone CHECK (LEN(Phone) BETWEEN 9 AND 11 AND Phone COLLATE Latin1_General_100_BIN2 NOT LIKE '%[^0-9]%')
    );
    CREATE TABLE dbo.Products (
        ProductCode nvarchar(20) NOT NULL CONSTRAINT PK_Products PRIMARY KEY,
        ProductName nvarchar(150) NOT NULL,
        Category nvarchar(30) NOT NULL,
        Color nvarchar(50) NOT NULL,
        Origin nvarchar(50) NOT NULL,
        TotalQuantity int NOT NULL,
        RentalPrice decimal(18,0) NOT NULL,
        PurchasePrice decimal(18,0) NOT NULL,
        ImagePath nvarchar(2048) NOT NULL CONSTRAINT DF_Products_Image DEFAULT N'',
        RowVersion rowversion NOT NULL,
        CONSTRAINT CK_Products_Name CHECK (LEN(TRIM(ProductName)) BETWEEN 2 AND 150),
        CONSTRAINT CK_Products_Category CHECK (Category IN (N'Váy cưới', N'Vest', N'Áo dài', N'Phụ kiện')),
        CONSTRAINT CK_Products_ColorOrigin CHECK (LEN(TRIM(Color)) >= 2 AND LEN(TRIM(Origin)) >= 2),
        CONSTRAINT CK_Products_Quantity CHECK (TotalQuantity BETWEEN 0 AND 1000000),
        CONSTRAINT CK_Products_Prices CHECK (RentalPrice > 0 AND RentalPrice <= 1000000000 AND PurchasePrice BETWEEN 0 AND 1000000000)
    );
    CREATE TABLE dbo.RentalContracts (
        ContractCode nvarchar(20) NOT NULL CONSTRAINT PK_RentalContracts PRIMARY KEY,
        CustomerCode nvarchar(20) NOT NULL CONSTRAINT FK_RentalContracts_Customers REFERENCES dbo.Customers(CustomerCode),
        CustomerName nvarchar(150) NOT NULL,
        StartDate date NOT NULL,
        DueDate date NOT NULL,
        RentalTotal decimal(18,0) NOT NULL,
        AmountCollected decimal(18,0) NOT NULL CONSTRAINT DF_RentalContracts_Collected DEFAULT 0,
        Status nvarchar(20) NOT NULL CONSTRAINT DF_RentalContracts_Status DEFAULT N'Đang thuê',
        ReturnedDate date NULL,
        ExtraCharge decimal(18,0) NOT NULL CONSTRAINT DF_RentalContracts_Extra DEFAULT 0,
        CollectedOnReturn decimal(18,0) NOT NULL CONSTRAINT DF_RentalContracts_OnReturn DEFAULT 0,
        Notes nvarchar(1000) NOT NULL CONSTRAINT DF_RentalContracts_Notes DEFAULT N'',
        RowVersion rowversion NOT NULL,
        CONSTRAINT CK_RentalContracts_Dates CHECK (DueDate >= StartDate AND (ReturnedDate IS NULL OR ReturnedDate >= StartDate)),
        CONSTRAINT CK_RentalContracts_Amounts CHECK (RentalTotal > 0 AND RentalTotal <= 1000000000 AND ExtraCharge BETWEEN 0 AND 1000000000
            AND AmountCollected BETWEEN 0 AND RentalTotal + ExtraCharge AND CollectedOnReturn BETWEEN 0 AND AmountCollected),
        CONSTRAINT CK_RentalContracts_State CHECK (
            (Status = N'Đang thuê' AND ReturnedDate IS NULL AND ExtraCharge = 0 AND CollectedOnReturn = 0) OR
            (Status = N'Đã trả' AND ReturnedDate IS NOT NULL AND AmountCollected = RentalTotal + ExtraCharge) OR
            (Status = N'Đã hủy' AND ReturnedDate IS NULL AND AmountCollected = 0 AND ExtraCharge = 0 AND CollectedOnReturn = 0))
    );
    CREATE INDEX IX_RentalContracts_Customer ON dbo.RentalContracts(CustomerCode);
    CREATE INDEX IX_RentalContracts_StatusDates ON dbo.RentalContracts(Status, StartDate, DueDate);
    CREATE TABLE dbo.RentalLines (
        ContractCode nvarchar(20) NOT NULL CONSTRAINT FK_RentalLines_Contracts REFERENCES dbo.RentalContracts(ContractCode),
        LineNumber int NOT NULL,
        ProductCode nvarchar(20) NOT NULL CONSTRAINT FK_RentalLines_Products REFERENCES dbo.Products(ProductCode),
        ProductName nvarchar(150) NOT NULL,
        Category nvarchar(30) NOT NULL,
        Quantity int NOT NULL,
        UnitPrice decimal(18,0) NOT NULL,
        LineTotal AS (CONVERT(decimal(18,0), Quantity * UnitPrice)) PERSISTED,
        CONSTRAINT PK_RentalLines PRIMARY KEY (ContractCode, LineNumber),
        CONSTRAINT UQ_RentalLines_Product UNIQUE (ContractCode, ProductCode),
        CONSTRAINT CK_RentalLines_Values CHECK (LineNumber > 0 AND Quantity BETWEEN 1 AND 1000000 AND UnitPrice > 0 AND UnitPrice <= 1000000000),
        CONSTRAINT CK_RentalLines_Category CHECK (Category IN (N'Váy cưới', N'Vest', N'Áo dài', N'Phụ kiện'))
    );
    CREATE INDEX IX_RentalLines_Product ON dbo.RentalLines(ProductCode, ContractCode) INCLUDE (Quantity);
    CREATE TABLE dbo.StockReceipts (
        ReceiptCode nvarchar(20) NOT NULL CONSTRAINT PK_StockReceipts PRIMARY KEY,
        SupplierCode nvarchar(20) NOT NULL CONSTRAINT FK_StockReceipts_Suppliers REFERENCES dbo.Suppliers(SupplierCode),
        RecordedBy varchar(50) COLLATE DATABASE_DEFAULT NOT NULL CONSTRAINT FK_StockReceipts_Accounts REFERENCES dbo.Accounts(UserName),
        ReceiptDate date NOT NULL,
        Notes nvarchar(1000) NOT NULL CONSTRAINT DF_StockReceipts_Notes DEFAULT N'',
        Total decimal(18,0) NOT NULL,
        RowVersion rowversion NOT NULL,
        CONSTRAINT CK_StockReceipts_Total CHECK (Total > 0 AND Total <= 1000000000)
    );
    CREATE INDEX IX_StockReceipts_Supplier ON dbo.StockReceipts(SupplierCode);
    CREATE TABLE dbo.StockReceiptLines (
        ReceiptCode nvarchar(20) NOT NULL CONSTRAINT FK_StockReceiptLines_Receipts REFERENCES dbo.StockReceipts(ReceiptCode),
        LineNumber int NOT NULL,
        ProductCode nvarchar(20) NOT NULL CONSTRAINT FK_StockReceiptLines_Products REFERENCES dbo.Products(ProductCode),
        ProductName nvarchar(150) NOT NULL,
        Category nvarchar(30) NOT NULL,
        Quantity int NOT NULL,
        UnitPrice decimal(18,0) NOT NULL,
        LineTotal AS (CONVERT(decimal(18,0), Quantity * UnitPrice)) PERSISTED,
        CONSTRAINT PK_StockReceiptLines PRIMARY KEY (ReceiptCode, LineNumber),
        CONSTRAINT CK_StockReceiptLines_Values CHECK (LineNumber > 0 AND Quantity BETWEEN 1 AND 1000000 AND UnitPrice > 0 AND UnitPrice <= 1000000000),
        CONSTRAINT CK_StockReceiptLines_Category CHECK (Category IN (N'Váy cưới', N'Vest', N'Áo dài', N'Phụ kiện'))
    );
    CREATE INDEX IX_StockReceiptLines_Product ON dbo.StockReceiptLines(ProductCode);
    -- Each row is a payment/refund allocation to a rental line, matching RecordPayment().
    -- Customer, category and description are historical snapshots used by frmThongKe.
    CREATE TABLE dbo.CashTransactions (
        TransactionCode nvarchar(20) NOT NULL CONSTRAINT PK_CashTransactions PRIMARY KEY,
        ContractCode nvarchar(20) NOT NULL,
        LineNumber int NOT NULL,
        TransactionDate date NOT NULL,
        CustomerName nvarchar(150) NOT NULL,
        Category nvarchar(30) NOT NULL,
        Description nvarchar(1200) NOT NULL,
        Collected decimal(18,0) NOT NULL CONSTRAINT DF_CashTransactions_Collected DEFAULT 0,
        Refunded decimal(18,0) NOT NULL CONSTRAINT DF_CashTransactions_Refunded DEFAULT 0,
        NetCollected AS (Collected - Refunded) PERSISTED,
        CONSTRAINT FK_CashTransactions_RentalLines FOREIGN KEY (ContractCode, LineNumber) REFERENCES dbo.RentalLines(ContractCode, LineNumber),
        CONSTRAINT CK_CashTransactions_Amounts CHECK ((Collected >= 0 AND Refunded = 0) OR (Refunded >= 0 AND Collected = 0)),
        CONSTRAINT CK_CashTransactions_Category CHECK (Category IN (N'Váy cưới', N'Vest', N'Áo dài', N'Phụ kiện'))
    );
    CREATE INDEX IX_CashTransactions_DateCategory ON dbo.CashTransactions(TransactionDate, Category) INCLUDE (Collected, Refunded);
    CREATE INDEX IX_CashTransactions_ContractLine ON dbo.CashTransactions(ContractCode, LineNumber);

    EXEC(N'CREATE VIEW dbo.vw_ThongKe AS
        SELECT TransactionCode AS Ma, TransactionDate AS Ngay, CustomerName AS KhachHang,
            Category AS Nhom, Description AS NoiDung, Collected AS Thu, Refunded AS Hoan, NetCollected AS ThucThu
        FROM dbo.CashTransactions;');
    EXEC(N'CREATE VIEW dbo.vw_DailyCashFlow AS
        SELECT TransactionDate, Category, COUNT_BIG(*) AS TransactionCount,
            SUM(Collected) AS Collected, SUM(Refunded) AS Refunded, SUM(NetCollected) AS NetCollected
        FROM dbo.CashTransactions GROUP BY TransactionDate, Category;');
    INSERT dbo.SchemaVersions (Version) VALUES (1);
    COMMIT;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK;
    THROW;
END CATCH;
GO
