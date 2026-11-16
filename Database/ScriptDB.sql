CREATE DATABASE QuanLyBanXoi;
GO
USE QuanLyBanXoi;
GO

-- Bảng Món Xôi chính
CREATE TABLE MonXoi (
    MaMon INT IDENTITY(1,1) PRIMARY KEY,
    TenMon NVARCHAR(100) NOT NULL,
    LoaiMon NVARCHAR(50) NOT NULL, -- Xôi mặn, Xôi ngọt, Đồ uống
    DonGia DECIMAL(18, 0) NOT NULL,
    TrangThai NVARCHAR(50) DEFAULT N'Còn hàng'
);

-- Bảng Topping ăn kèm
CREATE TABLE Topping (
    MaTopping INT IDENTITY(1,1) PRIMARY KEY,
    TenTopping NVARCHAR(100) NOT NULL,
    DonGia DECIMAL(18, 0) NOT NULL,
    TrangThai NVARCHAR(50) DEFAULT N'Còn hàng'
);

-- Bảng Hóa Đơn
CREATE TABLE HoaDon (
    MaHD INT IDENTITY(1,1) PRIMARY KEY,
    NgayLap DATETIME DEFAULT GETDATE(),
    TenKhach NVARCHAR(100) DEFAULT N'Khách lẻ',
    TongTien DECIMAL(18, 0) NOT NULL,
    GhiChu NVARCHAR(255)
);

-- Bảng Chi Tiết Hóa Đơn (Lưu từng hộp xôi + danh sách topping đi kèm)
CREATE TABLE ChiTietHoaDon (
    MaCTHD INT IDENTITY(1,1) PRIMARY KEY,
    MaHD INT FOREIGN KEY REFERENCES HoaDon(MaHD) ON DELETE CASCADE,
    MaMon INT FOREIGN KEY REFERENCES MonXoi(MaMon),
    SoLuong INT NOT NULL,
    ToppingKem NVARCHAR(255), -- VD: Chả lụa, Pate, Trứng kho
    ThanhTien DECIMAL(18, 0) NOT NULL
);

-- Dữ liệu mẫu
INSERT INTO MonXoi (TenMon, LoaiMon, DonGia) VALUES
(N'Xôi Xéo Hà Nội', N'Xôi mặn', 15000),
(N'Xôi Trắng Thập Cẩm', N'Xôi mặn', 12000),
(N'Xôi Gấc Đậu Xanh', N'Xôi ngọt', 20000),
(N'Xôi Ngô Hành Phi', N'Xôi mặn', 15000),
(N'Sữa Đậu Nành', N'Đồ uống', 10000);

INSERT INTO Topping (TenTopping, DonGia) VALUES
(N'Chả lụa', 8000),
(N'Pate gan', 7000),
(N'Trứng kho', 7000),
(N'Thịt kho tàu', 15000),
(N'Lạp xưởng', 10000),
(N'Ruốc thịt', 5000);
GO