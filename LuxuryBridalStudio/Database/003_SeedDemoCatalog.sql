USE [LuxuryBridalStudio];
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET ANSI_PADDING ON;
SET ANSI_WARNINGS ON;
SET CONCAT_NULL_YIELDS_NULL ON;
SET ARITHABORT ON;
SET NUMERIC_ROUNDABORT OFF;
SET XACT_ABORT ON;
BEGIN TRY
    BEGIN TRANSACTION;
    INSERT dbo.Customers (CustomerCode, FullName, Phone, IdentityNumber, Address)
    SELECT s.* FROM (VALUES
        (N'KH001', N'Nguyễn Thu Hà', '0901234567', '079186001234', N'123 Lê Lợi, TP. HCM'),
        (N'KH002', N'Trần Thị Mai Anh', '0987654321', '001206012345', N'45 Cầu Giấy, Hà Nội'),
        (N'KH003', N'Hoàng Thùy Linh', '0963111222', '038195007890', N'78 Trần Phú, Hà Nội'),
        (N'KH004', N'Lê Hoàng Yến', '0912345678', '025198009876', N'12 Kim Mã, Hà Nội'),
        (N'KH005', N'Phạm Minh Đức', '0935678901', '001099003456', N'89 Nguyễn Trãi, Hà Nội')
    ) s(Code, FullName, Phone, IdentityNumber, Address)
    WHERE NOT EXISTS (SELECT 1 FROM dbo.Customers c WHERE c.CustomerCode = s.Code);

    INSERT dbo.Products (ProductCode, ProductName, Category, Color, Origin, TotalQuantity, RentalPrice, PurchasePrice)
    SELECT s.* FROM (VALUES
        (N'SP001', N'Váy cưới công chúa đính đá', N'Váy cưới', N'Trắng', N'Việt Nam', 2, 3500000, 10000000),
        (N'SP002', N'Váy cưới cúp ngực Satin', N'Váy cưới', N'Trắng kem', N'Hàn Quốc', 1, 2800000, 8000000),
        (N'SP003', N'Vest chú rể classic đen', N'Vest', N'Đen', N'Việt Nam', 4, 1500000, 3000000),
        (N'SP004', N'Áo dài cưới Long Phụng', N'Áo dài', N'Đỏ thẫm', N'Việt Nam', 0, 2000000, 4000000),
        (N'SP005', N'Váy cưới đuôi cá phối ren', N'Váy cưới', N'Trắng', N'Pháp', 3, 4200000, 12000000)
    ) s(Code, ProductName, Category, Color, Origin, TotalQuantity, RentalPrice, PurchasePrice)
    WHERE NOT EXISTS (SELECT 1 FROM dbo.Products p WHERE p.ProductCode = s.Code);

    INSERT dbo.Suppliers (SupplierCode, SupplierName, Phone, Address, Notes)
    SELECT s.* FROM (VALUES
        (N'NCC001', N'Xưởng May Bella Bridal', '02438225678', N'102 Bà Triệu, Hà Nội', N'Váy cưới ren cao cấp'),
        (N'NCC002', N'Thời Trang HERA', '02839301234', N'45 Nam Kỳ Khởi Nghĩa, TP. HCM', N'Váy cưới satin'),
        (N'NCC003', N'Xưởng Vest Hoàng Gia', '0918222333', N'88 Nguyễn Đình Chiểu, TP. HCM', N'Vest chú rể'),
        (N'NCC004', N'Áo Dài Truyền Thống', '0905456789', N'12 Lê Lợi, Huế', N'Áo dài gấm'),
        (N'NCC005', N'Phụ Kiện Diamond Studio', '0977888999', N'25 Kim Mã, Hà Nội', N'Voan, vương miện')
    ) s(Code, SupplierName, Phone, Address, Notes)
    WHERE NOT EXISTS (SELECT 1 FROM dbo.Suppliers p WHERE p.SupplierCode = s.Code);
    COMMIT;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK;
    THROW;
END CATCH;
GO
