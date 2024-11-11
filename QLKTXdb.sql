CREATE DATABASE QLKTX
GO
USE QLKTX
GO
-- Bảng TOANHA
CREATE TABLE ToaNha (
    ToaNhaID INT IDENTITY(1,1) PRIMARY KEY,
    TenToaNha NVARCHAR(30) NOT NULL,
    MoTa NVARCHAR(255)
);
GO
-- Bảng LOAIPHONG
CREATE TABLE LoaiPhong (
    LoaiPhongID INT IDENTITY(1,1) PRIMARY KEY,
    TenLoaiPhong NVARCHAR(30) NOT NULL,
    DonGia FLOAT,
    MoTa NVARCHAR(255),
    GioiTinh NVARCHAR(10)
);
GO
-- Bảng PHONG
CREATE TABLE Phong (
    PhongID INT IDENTITY(1,1) PRIMARY KEY,
    ToaNhaID INT,
    LoaiPhongID INT,
    TenPhong NVARCHAR(30) NOT NULL,
    TinhTrang NVARCHAR(30),
    SoLuongGiuong INT,
    FOREIGN KEY (ToaNhaID) REFERENCES ToaNha(ToaNhaID) ON DELETE CASCADE,
    FOREIGN KEY (LoaiPhongID) REFERENCES LoaiPhong(LoaiPhongID) ON DELETE CASCADE
);
GO
-- Bảng VAITRO
CREATE TABLE VaiTro (
    VaiTroID INT IDENTITY(1,1) PRIMARY KEY,
    TenVaiTro NVARCHAR(30) NOT NULL,
);
GO
-- Bảng TAIKHOAN
CREATE TABLE TaiKhoan (
    TaiKhoanID INT IDENTITY(1,1) PRIMARY KEY,
    TenDangNhap NVARCHAR(20) NOT NULL UNIQUE,
    MatKhau NVARCHAR(20) NOT NULL,
	VaiTroID INT,
    FOREIGN KEY (VaiTroID) REFERENCES VaiTro(VaiTroID)
);
GO
-- Bảng SINHVIEN
CREATE TABLE SinhVien (
    SinhVienID INT IDENTITY(1,1) PRIMARY KEY,
    HoSV NVARCHAR(50) NOT NULL,
    TenSV NVARCHAR(20) NOT NULL,
    MSSV NVARCHAR(10) NOT NULL,
    Lop NVARCHAR(30),
    GioiTinh NVARCHAR(10),
    NgaySinh DATE,
    NoiSinh NVARCHAR(100),
    DiaChi NVARCHAR(100),
    Email NVARCHAR(100),
    SoDienThoai NVARCHAR(10),
    SoCCCD NVARCHAR(20),
	TaiKhoanID INT,
	FOREIGN KEY (TaiKhoanID) REFERENCES TaiKhoan(TaiKhoanID)
);
GO
-- Bảng DKPHONG
CREATE TABLE DKPhong (
    DKPhongID INT IDENTITY(1,1) PRIMARY KEY,
	SinhVienID INT,
    PhongID INT,
    NgayDK DATE,
    NgayTra DATE,
    FOREIGN KEY (SinhVienID) REFERENCES SinhVien(SinhVienID) ON DELETE CASCADE,
    FOREIGN KEY (PhongID) REFERENCES Phong(PhongID) ON DELETE CASCADE
);
GO
-- Bảng DIENNUOC
CREATE TABLE DienNuoc (
    DienNuocID INT IDENTITY(1,1) PRIMARY KEY,
    PhongID INT,
    TuNgay DATE,
    DenNgay DATE,
    ChiSoDienCu INT,
    ChiSoDienMoi INT,
    ChiSoNuocCu INT,
    ChiSoNuocMoi INT,
    DonGiaDien FLOAT,
    DonGiaNuoc FLOAT,
    FOREIGN KEY (PhongID) REFERENCES Phong(PhongID) ON DELETE CASCADE
);
GO
-- Bảng HOADON
CREATE TABLE HoaDon (
    HoaDonID INT IDENTITY(1,1) PRIMARY KEY,
    PhongID INT,
	DienNuocID INT,
    NgayLapHD DATE NOT NULL,
    TinhTrang NVARCHAR(30) NOT NULL,
    FOREIGN KEY (PhongID) REFERENCES Phong(PhongID),
    FOREIGN KEY (DienNuocID) REFERENCES DienNuoc(DienNuocID)
);

INSERT INTO LoaiPhong VALUES
	(N'Phòng VIP', 1000000, N'Thoáng mát, tiện nghi, thiết bị đầy đủ', 'Nam'),
	(N'Phòng bình dân nam', 300000, N'Thoáng mát', 'Nam'),
	(N'Phòng bình dân nữ', 300000, N'Thoáng mát', 'Nữ')

GO

INSERT INTO ToaNha VALUES
	(N'K1', N'Thoáng mát, gần khu công nghệ thông tin, có căn tin'),
	(N'K2', N'Thoáng mát, gần khu du lịch'),
	(N'K3', N'Thoáng mát, gần khu ngôn ngữ Anh, có căn tin'),
	(N'K4', N'Thoáng mát, gần khu công nghệ thông tin'),
	(N'K5', N'Thoáng mát, gần nhà đa năng'),
	(N'K6', N'Thoáng mát, gần cổng trường')

GO

INSERT INTO Phong (ToaNhaID, LoaiPhongID, TenPhong, TinhTrang, SoLuongGiuong)
VALUES
    (1, 1, N'101', N'Đang sử dụng', 2),
    (1, 1, N'102', N'Đang sử dụng', 2),
    (1, 2, N'103', N'Đang sử dụng', 6),
    (1, 3, N'104', N'Đang sử dụng', 6),
    (2, 1, N'101', N'Đang sử dụng', 2),
    (2, 1, N'102', N'Đang sử dụng', 2),
    (2, 2, N'103', N'Đang sử dụng', 6),
    (2, 2, N'104', N'Đang sử dụng', 6),
    (3, 3, N'101', N'Đang sử dụng', 4),
    (3, 3, N'102', N'Đang sử dụng', 4);

GO

INSERT INTO VaiTro (TenVaiTro)
VALUES
	(N'Admin'),
	(N'Sinh viên');

GO

INSERT INTO TaiKhoan (TenDangNhap, MatKhau, VaiTroID)
VALUES
    (N'63135148', N'1', 2),
    (N'63123456', N'1', 2),
    (N'63123457', N'1', 2),
    (N'63123458', N'1', 2),
    (N'62123459', N'1', 2),
    (N'63132184', N'1', 2),
    (N'63123461', N'1', 2),
    (N'63123462', N'1', 2),
    (N'63123463', N'1', 2),
    (N'63123464', N'1', 2),
	(N'admin', N'1', 1);

GO

INSERT INTO SinhVien (HoSV, TenSV, MSSV, Lop, GioiTinh, NgaySinh, NoiSinh, DiaChi, Email, SoDienThoai, SoCCCD, TaiKhoanID)
VALUES
    (N'Nguyễn Hùng Tuấn', N'Kiệt', N'63132184', N'63.CNTT-3', N'Nam', '2003-06-29', N'Bắc Giang', N'Ninh Hòa, Khánh Hòa', N'tuankiet2962003@gmail.com', N'0523059456', N'012345678912', 1),
    (N'Nguyễn Công', N'Phương', N'63135148', N'63.CNTT-3', N'Nam', '2003-07-01', N'Bà Rịa - Vũng tàu', N'Ninh Hòa, Khánh Hòa', N'ncphuong0107@gmail.com', N'0123456789', N'012345678900', 2),
    (N'Bùi Tiến', N'Dũng', N'63123456', N'63.CNTT-3', N'Nam', '2003-02-02', 'Khánh Hòa', N'Nha Trang, Khánh Hòa', N'btdung0202@gmail.com', '0123456790', N'012345678901', 3),
    (N'Nguyễn Thị', N'Thủy', N'63123458', N'63.CNTT-2', N'Nữ', '2003-04-04', 'Khánh Hòa', N'Nha Trang, Khánh Hòa', N'ntthuy0404@gmail.com', N'0123456792', N'012345678903', 4),
    (N'Nguyễn Kiều Cẩm', N'Thơ', N'62123459', N'62.NNA-1', N'Nữ', '2002-05-05', 'TP.Hồ Chí Minh', N'Nha Trang, Khánh Hòa', N'nkctho0505@gmail.com', N'0123456793', N'012345678904', 5),
    (N'Nguyễn Hùng Tuấn', N'Kiệt', N'63132184', N'63.CNTT-3', N'Nam', '2003-06-29', 'Khánh Hòa', N'Ninh Sim, Khánh Hòa', N'nhtkiet2906@gmail.com', N'0123456794', N'012345678905', 6),
    (N'Vũ Thanh', N'Vân', N'63123461', N'63.NNA-2', N'Nữ', '2003-07-07', 'TP.Hồ Chí Minh', N'Nha Trang, Khánh Hòa', N'vtvan0707@gmail.com', N'0123456795', N'012345678906', 7),
    (N'Phạm Văn', N'Lộc', N'63123462', N'63.NNA-1', N'Nam', '2003-08-08', 'Khánh Hòa', N'Nha Trang, Khánh Hòa', N'pvloc0808@gmail.com', N'0123456796', N'012345678907', 8),
    (N'Vũ Phạm Đình', N'Thái', N'63123463', N'63.CNTT-2', N'Nam', '2003-09-09', N'TP.Hồ Chí Minh', 'TP.Hồ Chí Minh', N'vpdthai0909@gmail.com', N'0123456797', N'012345678908', 9),
    (N'Hoàng Xuân', N'Vinh', N'63123464', N'63.CNTT-2', N'Nam', '2003-10-10', N'TP.Hồ Chí Minh', 'TP.Hồ Chí Minh', N'hxvinh1010@gmail.com', N'0123456798', N'012345678909', 10),
    (N'Trần Văn', N'Cao', N'63123457', N'62.CNTT-1', N'Nam', '2002-03-03', 'Khánh Hòa', N'Ninh Hòa, Khánh Hòa', N'tvcao0303@gmail.com', N'0123456791', N'012345678902', 11)
