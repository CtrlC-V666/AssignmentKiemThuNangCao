-- Tạo cơ sở dữ liệu QLNH với collation hỗ trợ tiếng Việt
CREATE DATABASE QLNH COLLATE Vietnamese_CI_AS;
GO
drop database if exists QLBN
-- Sử dụng cơ sở dữ liệu QLNH
USE QLNH;
GO
drop table if exists NhanVien
-- Tạo bảng NhanVien
CREATE TABLE NhanVien (
    MaNhanVien NVARCHAR(50) PRIMARY KEY,
    Ten NVARCHAR(100) COLLATE Vietnamese_CI_AS NOT NULL,
    ChucVu NVARCHAR(50) COLLATE Vietnamese_CI_AS
);
GO
CREATE TABLE PhanQuyen (
    MaQuyen INT PRIMARY KEY IDENTITY(1,1),
    TenQuyen NVARCHAR(50) COLLATE Vietnamese_CI_AS NOT NULL
);
GO

-- Tạo bảng NguoiDung
CREATE TABLE NguoiDung(
   TenDangNhap NVARCHAR(50) COLLATE Vietnamese_CI_AS PRIMARY KEY,
   MatKhau NVARCHAR(50) COLLATE Vietnamese_CI_AS NOT NULL,
   Email NVARCHAR(50) COLLATE Vietnamese_CI_AS NOT NULL,
   MaNhanVien NVARCHAR(50) COLLATE Vietnamese_CI_AS,
   MaQuyen INT,
   FOREIGN KEY (MaQuyen) REFERENCES PhanQuyen(MaQuyen)
);
GO


-- Chèn dữ liệu vào bảng NguoiDung
INSERT INTO NguoiDung (TenDangNhap, MatKhau, Email) 
VALUES 
('1', '123', '1@gmail.com'),
('2', '123', '2@gmail.com'),
('3', '123', '3@gmail.com');
GO

-- Truy vấn tất cả dữ liệu từ bảng NguoiDung
SELECT * FROM NguoiDung;
GO

-- Truy vấn dữ liệu từ bảng NguoiDung với mật khẩu được ẩn
SELECT TenDangNhap, REPLACE(MatKhau, MatKhau, '***') AS MatKhau, Email
FROM NguoiDung;
GO

-- Tạo view NguoiDung_View
CREATE VIEW NguoiDung_View AS
SELECT TenDangNhap, '***' AS MatKhau, Email
FROM NguoiDung;
GO

-- Truy vấn dữ liệu từ view NguoiDung_View
SELECT * FROM NguoiDung_View;
GO

-- Tạo bảng MenuHaiSan
CREATE TABLE MenuHaiSan (
    MaMon INT PRIMARY KEY IDENTITY(1,1),
    TenMon NVARCHAR(100) COLLATE Vietnamese_CI_AS NOT NULL,
    Gia DECIMAL(18, 2) NOT NULL,
    MoTa NVARCHAR(255) COLLATE Vietnamese_CI_AS,
    TinhTrang NVARCHAR(50) COLLATE Vietnamese_CI_AS,
    SoLuong INT NOT NULL DEFAULT 0
);
GO
drop table if exists  HoaDon
-- Tạo bảng HoaDon
CREATE TABLE HoaDon (
    MaHoaDon INT PRIMARY KEY IDENTITY(1,1),
    NgayLap DATE NOT NULL,
    TenKhachHang NVARCHAR(100),
    SDT NVARCHAR(15),
    TrangThai NVARCHAR(20) NOT NULL,
    PhuongThuc NVARCHAR(20) NOT NULL,
    TongTien DECIMAL(18,0) NOT NULL
);
GO
drop table if exists  ChiTietHoaDon
-- Tạo bảng ChiTietHoaDon
CREATE TABLE ChiTietHoaDon (
    MaChiTietHoaDon INT PRIMARY KEY IDENTITY(1,1),
    MaHoaDon INT NOT NULL,
    TenKhachHang NVARCHAR(100) NOT NULL,
    TenSanPham NVARCHAR(100) NOT NULL,
    SoLuong INT NOT NULL,
    DonGia DECIMAL(18, 0) NOT NULL,
    TongTien AS (SoLuong * DonGia) PERSISTED,
    FOREIGN KEY (MaHoaDon) REFERENCES HoaDon(MaHoaDon)
);
GO
-- Tạo Bảng KhoHang
drop table if exists KhoHang
create table KhoHang(
MaHang  INT PRIMARY KEY IDENTITY(1,1),
TenHang nvarchar(100) COLLATE Vietnamese_CI_AS not null,
Soluong int not null,
GiaNhap float not null,
GiaBan float not null,
NgayNhap datetime not null
);
-- Chèn dữ liệu vào bảng HoaDon
INSERT INTO HoaDon (NgayLap, TenKhachHang, SDT, TrangThai, PhuongThuc, TongTien) 
VALUES 
('2024-07-01', N'Nguyen Van A', N'0123456789', N'Đã thanh toán', N'Tiền mặt', 2200000),
('2024-07-02', N'Tran Thi B', N'0987654321', N'Chưa thanh toán', N'Thẻ', 300000),
('2024-07-03', N'Lê Văn C', N'0123987456', N'Đang xử lý', N'Tiền mặt', 1100000),
('2024-07-04', N'Phạm Văn D', N'0987432123', N'Đã thanh toán', N'Thẻ', 2400000);
GO

-- Chèn dữ liệu vào bảng ChiTietHoaDon
INSERT INTO ChiTietHoaDon (MaHoaDon, TenSanPham, SoLuong, DonGia, TenKhachHang) 
VALUES 
(1, N'Tôm Hùm Nướng Phô Mai', 2, 1000000, N'Nguyen Van A'),
(1, N'Cua Hoàng Đế Hấp', 1, 2000000, N'Nguyen Van A'),
(2, N'Mực Nướng Sa Tế', 1, 300000, N'Tran Thi B'),
(3, N'Hàu Nướng Mỡ Hành', 1, 500000, N'Lê Văn C'),
(3, N'Sò Điệp Nướng Bơ Tỏi', 2, 400000, N'Lê Văn C'),
(4, N'Mực nướng bóng đêm', 1, 600000, N'Phạm Văn D'),
(4, N'Tôm Hùm Nướng Phô Mai', 2, 1000000, N'Phạm Văn D'),
(4, N'Cua Hoàng Đế Hấp', 1, 2000000, N'Phạm Văn D');
GO
-- Chèn dữ liệu vào bảng PhanQuyen
INSERT INTO PhanQuyen (TenQuyen)
VALUES 
(N'Quản lý'),
(N'Nhân viên');
GO

-- Chèn tài khoản quản lý vào bảng NguoiDung
INSERT INTO NguoiDung (TenDangNhap, MatKhau, Email, MaNhanVien, MaQuyen)
VALUES ('ruaseafoods@gmail.com', '1', 'ruaseafoods@gmail.com', 'NV0001', 1);
INSERT INTO NguoiDung (TenDangNhap, MatKhau, Email, MaNhanVien, MaQuyen)
VALUES ('du@gmail.com', '1', 'du@gmail.com', 'NV0002', 2);


-- Chèn thông tin nhân viên quản lý vào bảng NhanVien
INSERT INTO NhanVien (MaNhanVien, Ten, ChucVu)
VALUES ('NV0001', N'Quản lý', N'Quản lý');
INSERT INTO NhanVien (MaNhanVien, Ten, ChucVu)
VALUES ('NV0002', N'Du', N'Nhân viên');
GO
-- Chèn dữ liệu vào bảng MenuHaiSan
INSERT INTO MenuHaiSan (TenMon, Gia, MoTa, TinhTrang, SoLuong) 
VALUES 
(N'Tôm Hùm Nướng Phô Mai', 1000000, N'Tôm hùm nướng phô mai hảo hạng', N'Còn hàng', 0),
(N'Cua Hoàng Đế Hấp', 2000000, N'Cua hoàng đế hấp tươi ngon', N'Còn hàng', 0),
(N'Mực Nướng Sa Tế', 300000, N'Mực tươi nướng sa tế cay nồng', N'Còn hàng', 0),
(N'Hàu Nướng Mỡ Hành', 500000, N'Hàu nướng mỡ hành đậm đà', N'Hết hàng', 0),
(N'Sò Điệp Nướng Bơ Tỏi', 400000, N'Sò điệp nướng bơ tỏi thơm ngon', N'Còn hàng', 0),
(N'Mực nướng bóng đêm', 600000, N'Mực nướng ngon', N'Còn hàng', 0);
GO

CREATE PROCEDURE GetChiTietDonDaThanhToan
    @MaHoaDon INT
AS
BEGIN
    SELECT ct.*
    FROM ChiTietHoaDon ct
    WHERE ct.MaHoaDon = @MaHoaDon;
END
GO

ALTER TABLE KhoHang
ADD MoTa NVARCHAR(255) COLLATE Vietnamese_CI_AS;
UPDATE HoaDon
SET TrangThai = N'Đã thanh toán'
WHERE TrangThai = N'Đ? thanh toán';
 CREATE PROCEDURE UpdateDatabaseData
AS
BEGIN
    UPDATE HoaDon
    SET TrangThai = N'Đã thanh toán'
    WHERE TrangThai = N'Đ? thanh toán';

    UPDATE MenuHaiSan
    SET TinhTrang = N'Còn hàng'
    WHERE TinhTrang = N'C?n hàng';
END;

SELECT * FROM NguoiDung