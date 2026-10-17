USE [LuxuryBridalStudio];
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET ANSI_PADDING ON;
SET ANSI_WARNINGS ON;
SET CONCAT_NULL_YIELDS_NULL ON;
SET ARITHABORT ON;
SET NUMERIC_ROUNDABORT OFF;
-- All test records are rolled back, including on failure.
SET NOCOUNT ON;
SET XACT_ABORT OFF;
BEGIN TRY
    BEGIN TRANSACTION;
    INSERT dbo.Customers (CustomerCode, FullName, Phone) VALUES (N'__test_customer', N'Database Test', '99900000001');
    INSERT dbo.Products (ProductCode, ProductName, Category, Color, Origin, TotalQuantity, RentalPrice, PurchasePrice)
        VALUES (N'__test_product', N'Test Dress', N'Váy cưới', N'Trắng', N'Việt Nam', 2, 1000000, 2000000);
    -- Placeholder bytes are test-only; this transaction never creates a persistent login.
    INSERT dbo.Accounts (UserName, DisplayName, RoleName, PasswordHash, PasswordSalt)
        VALUES ('__test_login', N'Database Test', N'Quản lý', CONVERT(binary(32), 0x01), CONVERT(binary(16), 0x02));
    BEGIN TRY
        INSERT dbo.Accounts (UserName, DisplayName, RoleName, PasswordHash, PasswordSalt)
            VALUES ('__TEST_LOGIN', N'Duplicate Login', N'Nhân viên', CONVERT(binary(32), 0x01), CONVERT(binary(16), 0x02));
        THROW 52010, 'Login uniqueness is not case-insensitive.', 1;
    END TRY
    BEGIN CATCH
        IF ERROR_NUMBER() NOT IN (2601, 2627) THROW;
    END CATCH;
    INSERT dbo.Suppliers (SupplierCode, SupplierName, Phone) VALUES (N'__test_supplier', N'Test Supplier', '99900000002');
    INSERT dbo.StockReceipts (ReceiptCode, SupplierCode, RecordedBy, ReceiptDate, Total)
        VALUES (N'__test_receipt', N'__test_supplier', '__test_login', '20261016', 4100000);
    -- Same product at different purchase prices must remain two separate lines.
    INSERT dbo.StockReceiptLines (ReceiptCode, LineNumber, ProductCode, ProductName, Category, Quantity, UnitPrice)
        VALUES (N'__test_receipt', 1, N'__test_product', N'Test Dress', N'Váy cưới', 1, 2000000),
               (N'__test_receipt', 2, N'__test_product', N'Test Dress', N'Váy cưới', 1, 2100000);
    IF (SELECT SUM(LineTotal) FROM dbo.StockReceiptLines WHERE ReceiptCode = N'__test_receipt') <> 4100000
        THROW 52011, 'Receipt lines at different prices were not preserved.', 1;

    BEGIN TRY
        INSERT dbo.Customers (CustomerCode, FullName, Phone) VALUES (N'__test_duplicate', N'Duplicate Phone', '99900000001');
        THROW 52000, 'Duplicate customer phone was accepted.', 1;
    END TRY
    BEGIN CATCH
        IF ERROR_NUMBER() NOT IN (2601, 2627) THROW;
    END CATCH;
    BEGIN TRY
        INSERT dbo.RentalContracts (ContractCode, CustomerCode, CustomerName, StartDate, DueDate, RentalTotal)
            VALUES (N'__test_invalid', N'__missing_customer', N'Missing Customer', '20261016', '20261017', 1000000);
        THROW 52001, 'A rental without an existing customer was accepted.', 1;
    END TRY
    BEGIN CATCH
        IF ERROR_NUMBER() <> 547 THROW;
    END CATCH;

    INSERT dbo.RentalContracts (ContractCode, CustomerCode, CustomerName, StartDate, DueDate, RentalTotal, AmountCollected)
        VALUES (N'__test_contract', N'__test_customer', N'Database Test', '20261016', '20261017', 1000000, 300000);
    BEGIN TRY
        INSERT dbo.RentalLines (ContractCode, LineNumber, ProductCode, ProductName, Category, Quantity, UnitPrice)
            VALUES (N'__test_contract', 1, N'__test_product', N'Test Dress', N'Váy cưới', 0, 1000000);
        THROW 52002, 'A zero rental quantity was accepted.', 1;
    END TRY
    BEGIN CATCH
        IF ERROR_NUMBER() <> 547 THROW;
    END CATCH;
    INSERT dbo.RentalLines (ContractCode, LineNumber, ProductCode, ProductName, Category, Quantity, UnitPrice)
        VALUES (N'__test_contract', 1, N'__test_product', N'Test Dress', N'Váy cưới', 1, 1000000);
    INSERT dbo.CashTransactions (TransactionCode, ContractCode, LineNumber, TransactionDate, CustomerName, Category, Description, Collected)
        VALUES (N'__test_deposit', N'__test_contract', 1, '20261016', N'Database Test', N'Váy cưới', N'Deposit', 300000);
    BEGIN TRY
        INSERT dbo.CashTransactions (TransactionCode, ContractCode, LineNumber, TransactionDate, CustomerName, Category, Description, Collected)
            VALUES (N'__test_negative', N'__test_contract', 1, '20261016', N'Database Test', N'Váy cưới', N'Invalid', -1);
        THROW 52003, 'A negative collection was accepted.', 1;
    END TRY
    BEGIN CATCH
        IF ERROR_NUMBER() <> 547 THROW;
    END CATCH;
    BEGIN TRY
        UPDATE dbo.RentalContracts SET Status = N'Đã trả', ReturnedDate = '20261017' WHERE ContractCode = N'__test_contract';
        THROW 52004, 'An unsettled return was accepted.', 1;
    END TRY
    BEGIN CATCH
        IF ERROR_NUMBER() <> 547 THROW;
    END CATCH;

    -- A cancelled rental refunds the deposit; reports retain both cash movements.
    INSERT dbo.CashTransactions (TransactionCode, ContractCode, LineNumber, TransactionDate, CustomerName, Category, Description, Refunded)
        VALUES (N'__test_refund', N'__test_contract', 1, '20261017', N'Database Test', N'Váy cưới', N'Cancellation refund', 300000);
    UPDATE dbo.RentalContracts SET Status = N'Đã hủy', AmountCollected = 0 WHERE ContractCode = N'__test_contract';
    IF (SELECT SUM(Thu) FROM dbo.vw_ThongKe WHERE Ma LIKE N'__test[_]%') <> 300000
        THROW 52005, 'The report collection total is incorrect.', 1;
    IF (SELECT SUM(Hoan) FROM dbo.vw_ThongKe WHERE Ma LIKE N'__test[_]%') <> 300000
        THROW 52006, 'The report refund total is incorrect.', 1;
    IF (SELECT SUM(ThucThu) FROM dbo.vw_ThongKe WHERE Ma LIKE N'__test[_]%') <> 0
        THROW 52007, 'The report net total is incorrect.', 1;
    IF (SELECT LineTotal FROM dbo.RentalLines WHERE ContractCode = N'__test_contract') <> 1000000
        THROW 52008, 'The computed rental line total is incorrect.', 1;
    BEGIN TRY
        DELETE dbo.Customers WHERE CustomerCode = N'__test_customer';
        THROW 52009, 'A customer with rental history could be deleted.', 1;
    END TRY
    BEGIN CATCH
        IF ERROR_NUMBER() <> 547 THROW;
    END CATCH;

    ROLLBACK;
    PRINT 'PASS: login/customer uniqueness, relationships, receipt lines, quantities, payments, rental state, report totals and history protection. Test data rolled back.';
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK;
    THROW;
END CATCH;
GO
