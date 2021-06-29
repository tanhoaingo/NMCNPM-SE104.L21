CREATE DATABASE MYDATABASE
USE MYDATABASE
SET DATEFORMAT DMY

/*Chạy 3 dòng này trước rồi chạy toàn bộ database*/

/*=============================================================================*/

CREATE TABLE CHUCNANG(
	MaChucNang INT IDENTITY(1,1),
	TenChucNang NVARCHAR(50),
	TenManHinhDuocLoad NVARCHAR(50),
)

ALTER TABLE CHUCNANG
ADD CONSTRAINT PK_CHUCNANG
PRIMARY KEY (MaChucNang)

/*=============================================================================*/

CREATE TABLE NHOMNGUOIDUNG(
	MaNhom INT IDENTITY(1,1),
	TenNhom NVARCHAR(50),
)

ALTER TABLE NHOMNGUOIDUNG
ADD CONSTRAINT PK_NHOMNGUOIDUNG
PRIMARY KEY (MaNhom)

/*=============================================================================*/

CREATE TABLE PHANQUYEN(
	MaNhom INT IDENTITY(1,1),
	MaChucNang INT NOT NULL,
)

ALTER TABLE PHANQUYEN 
ADD CONSTRAINT PK_PHANQUYEN
PRIMARY KEY (MaNhom, MaChucNang) 

ALTER TABLE PHANQUYEN
ADD CONSTRAINT FK_PHANQUYEN_NHOMNGUOIDUNG
FOREIGN KEY (MaNhom) REFERENCES NHOMNGUOIDUNG (MaNhom)

ALTER TABLE PHANQUYEN 
ADD CONSTRAINT FK_PHANQUYEN_CHUCNANG
FOREIGN KEY (MaChucNang) REFERENCES CHUCNANG (MaChucNang)

/*=============================================================================*/

CREATE TABLE NGUOIDUNG(
	MaNguoiDung INT IDENTITY(1,1),
	TenDangNhap VARCHAR(20),
	MatKhau VARCHAR(20),
	MaNhom INT NOT NULL,
)

ALTER TABLE NGUOIDUNG
ADD CONSTRAINT PK_NGUOIDUNG
PRIMARY KEY (MaNguoiDung)

ALTER TABLE NGUOIDUNG
ADD CONSTRAINT FK_NGUOIDUNG_NHOMNGUOIDUNG
FOREIGN KEY (MaNhom) REFERENCES NHOMNGUOIDUNG (MaNhom)
/*=============================================================================*/

CREATE TABLE KHACHHANG(
	MaKhachHang INT IDENTITY(1,1),
	HoTenKhachHang NVARCHAR(50),
	SoNo MONEY,
	DiaChi NVARCHAR(100),
	DienThoai NVARCHAR(15),
	Email NVARCHAR(50)
)

ALTER TABLE KHACHHANG
ADD CONSTRAINT PK_KHACHHANG
PRIMARY KEY (MaKhachHang)

/*=============================================================================*/

CREATE TABLE PHIEUNHAPSACH(
	MaPhieuNhapSach INT IDENTITY(1,1),
	NgayNhap SMALLDATETIME,
)

ALTER TABLE PHIEUNHAPSACH
ADD CONSTRAINT PK_PHIEUNHAPSACH
PRIMARY KEY (MaPhieuNhapSach)

/*=============================================================================*/

CREATE TABLE DAUSACH(
	MaDauSach INT  IDENTITY(1,1),
	TenSach NVARCHAR(50),
	LuongTon INT,
)

ALTER TABLE DAUSACH
ADD CONSTRAINT PK_DAUSACH
PRIMARY KEY (MaDauSach)

/*=============================================================================*/

CREATE TABLE SACH(
	MaSach INT IDENTITY(1,1),
	MaDauSach INT NOT NULL,
	LuongTon INT,
)

ALTER TABLE SACH
ADD CONSTRAINT PK_SACH
PRIMARY KEY (MaSach)

ALTER TABLE SACH
ADD CONSTRAINT FK_SACH_DAUSACH
FOREIGN KEY (MaDauSach) REFERENCES DAUSACH (MaDauSach)

/*=============================================================================*/

CREATE TABLE CT_PNS(
	MaCT_PNS INT IDENTITY(1,1),
	MaPhieuNhapSach INT NOT NULL,
	MaSach INT NOT NULL,
	SoLuong INT,
	DonGiaNhap MONEY,
)

ALTER TABLE CT_PNS
ADD CONSTRAINT PK_CT_PNS
PRIMARY KEY (MaCT_PNS)

ALTER TABLE CT_PNS
ADD CONSTRAINT FK_CT_PNS_PHIEUNHAPSACH
FOREIGN KEY (MaPhieuNhapSach) REFERENCES PHIEUNHAPSACH (MaPhieuNhapSach)

ALTER TABLE CT_PNS
ADD CONSTRAINT FK_CT_PNS_SACH
FOREIGN KEY (MaSach) REFERENCES SACH (MaSach)

/*=============================================================================*/

CREATE TABLE THELOAI(
	MaTheLoai INT IDENTITY(1,1),
	TenTheLoai NVARCHAR(50),
)

ALTER TABLE THELOAI 
ADD CONSTRAINT PK_THELOAI
PRIMARY KEY (MaTheLoai)

/*=============================================================================*/

CREATE TABLE TT_TL(
	MaTheLoai INT NOT NULL,
	MaDauSach INT NOT NULL,
)

ALTER TABLE TT_TL 
ADD CONSTRAINT PK_TT_TL
PRIMARY KEY (MaTheLoai, MaDauSach)

ALTER TABLE TT_TL
ADD CONSTRAINT FK_TT_TL_THELOAI
FOREIGN KEY (MaTheLoai) REFERENCES THELOAI(MaTheLoai)

ALTER TABLE TT_TL	
ADD CONSTRAINT FK_TT_TL_DAUSACH
FOREIGN KEY (MaDauSach) REFERENCES DAUSACH(MaDauSach)

/*=============================================================================*/

CREATE TABLE TACGIA(
	MaTacGia INT IDENTITY(1,1), 
	TenTacGia NVARCHAR(50),
)

ALTER TABLE TACGIA
ADD CONSTRAINT PK_TACGIA
PRIMARY KEY (MaTacGia)

/*=============================================================================*/

CREATE TABLE TT_TG(
	MaDauSach INT NOT NULL, 
	MaTacGia INT NOT NULL,
)

ALTER TABLE TT_TG
ADD CONSTRAINT PK_TT_TG
PRIMARY KEY (MaDauSach, MaTacGia)

ALTER TABLE TT_TG
ADD CONSTRAINT FK_TT_TG_DAUSACH
FOREIGN KEY (MaDauSach) REFERENCES DAUSACH (MaDauSach)

ALTER TABLE TT_TG
ADD CONSTRAINT FK_TT_TG_TACGIA
FOREIGN KEY (MaTacGia) REFERENCES TACGIA (MaTacGia)

/*=============================================================================*/

CREATE TABLE THAMSO(
	MaThamSo INT IDENTITY(1,1),
	LuongNhapToiThieu INT,
	LuongTonToiThieu INT,
	SoNoToiDa MONEY,
	LuongTonToiThieuSauBan INT,
	ChoPhepThuLonHonNo VARCHAR(10),
)
ALTER TABLE THAMSO 
ADD CONSTRAINT PK_THAMSO
PRIMARY KEY (MaThamSo)

/*=============================================================================*/

CREATE TABLE HOADON(
	MaHoaDon INT IDENTITY(1,1),
	MaKhachHang INT NOT NULL,
	NgayLapHoaDon DATETIME,
	TongTien MONEY,
	SoTienTra MONEY,
	ConLai MONEY,
)

ALTER TABLE HOADON ADD MaNguoiLap INT NOT NULL

ALTER TABLE HOADON
ADD CONSTRAINT PK_HOADON
PRIMARY KEY (MaHoaDon)

ALTER TABLE HOADON
ADD CONSTRAINT FK_HOADON_KHACHHANG
FOREIGN KEY (MaKhachHang) REFERENCES KHACHHANG (MaKhachHang)

ALTER TABLE HOADON
ADD CONSTRAINT FK_HOADON_NGUOIDUNG
FOREIGN KEY (MaNguoiLap) REFERENCES NGUOIDUNG (MaNguoiDung)

/*=============================================================================*/

CREATE TABLE CT_HD(
	MaCT_HD INT IDENTITY(1,1),
	MaHoaDon INT NOT NULL,
	MaSach INT NOT NULL,
	SoLuong INT, 
	DonGiaBan MONEY,
)

ALTER TABLE CT_HD
ADD CONSTRAINT PK_CT_HD
PRIMARY KEY (MaCT_HD)

ALTER TABLE CT_HD
ADD CONSTRAINT FK_CT_HD_HOADON
FOREIGN KEY (MaHoaDon) REFERENCES HOADON (MaHoaDon)

ALTER TABLE CT_HD
ADD CONSTRAINT FK_CT_HD_SACH
FOREIGN KEY (MaSach) REFERENCES SACH (MaSach)

/*=============================================================================*/

CREATE TABLE PHIEUTHUTIEN(
	MaPhieuThuTien INT IDENTITY(1,1),
	MaKhachHang INT NOT NULL,
	NgayThuTien DATETIME,
	SoTienThu MONEY,
)

ALTER TABLE PHIEUTHUTIEN
ADD CONSTRAINT PK_PHIEUTHUTIEN
PRIMARY KEY (MaPhieuThuTien)

ALTER TABLE PHIEUTHUTIEN 
ADD CONSTRAINT FK_PHIEUTHUTIEN_KHACHHANG
FOREIGN KEY (MaKhachHang) REFERENCES KHACHHANG (MaKhachHang)

/*=============================================================================*/

CREATE TABLE BAOCAOTON(
	MaBaoCaoTon INT IDENTITY(1,1),
	Thang DATETIME,
)

ALTER TABLE BAOCAOTON
ADD CONSTRAINT PK_BAOCAOTON
PRIMARY KEY (MaBaoCaoTon)

/*=============================================================================*/

CREATE TABLE CT_BCT(
	MaCT_BCT INT IDENTITY(1,1),
	MaBaoCaoTon INT NOT NULL,
	MaSach INT NOT NULL,
	TonDau INT,
	PhatSinh INT, 
	TonCuoi INT,
)

ALTER TABLE CT_BCT
ADD CONSTRAINT PK_CT_BCT
PRIMARY KEY (MaCT_BCT)

ALTER TABLE CT_BCT
ADD CONSTRAINT FK_CT_BCT_BAOCAOTON
FOREIGN KEY (MaBaoCaoTon) REFERENCES BAOCAOTON (MaBaoCaoTon)

ALTER TABLE CT_BCT
ADD CONSTRAINT FK_CT_BCT_SACH
FOREIGN KEY (MaSach) REFERENCES SACH (MaSach)

/*=============================================================================*/

CREATE TABLE BAOCAOCONGNO(
	MaBaoCaoCongNo INT IDENTITY(1,1),
	Thang DATETIME,
)

ALTER TABLE BAOCAOCONGNO
ADD CONSTRAINT PK_BAOCAOCONGNO
PRIMARY KEY (MaBaoCaoCongNo) 

/*=============================================================================*/

CREATE TABLE CT_BCCN(
	MaCT_BCCN INT IDENTITY(1,1),
	MaBaoCaoCongNo INT NOT NULL,
	MaKhachHang INT NOT NULL,
	NoDau MONEY,
	PhatSinh MONEY, 
	NoCuoi MONEY,
)

ALTER TABLE CT_BCCN
ADD CONSTRAINT PK_CT_BCCN
PRIMARY KEY (MaCT_BCCN)

ALTER TABLE CT_BCCN
ADD CONSTRAINT FK_CT_BCCN_BAOCAOCONGNO
FOREIGN KEY (MaBaoCaoCongNo) REFERENCES BAOCAOCONGNO (MaBaoCaoCongNo)

ALTER TABLE CT_BCCN
ADD CONSTRAINT FK_CT_BCCN_KHACHHANG
FOREIGN KEY (MaKhachHang) REFERENCES KHACHHANG (MaKhachHang)

/*=============================================================================*/

CREATE TABLE HinhAnhSach(
	MaHinhAnh INT IDENTITY(1,1),
	MaDauSach INT NOT NULL,	
	DuongDan varchar(100)
)

/*=============================================================================*/

INSERT INTO NHOMNGUOIDUNG VALUES('admin')
INSERT INTO NGUOIDUNG VALUES ('admin','1',1)
INSERT INTO THELOAI VALUES(N'Khoa học viễn tưởng'),(N'Hài hước'),(N'Kinh dị'),(N'Trinh thám'),(N'Cổ tích'),('Anime')
INSERT INTO DAUSACH(TenSach,LuongTon) VALUES
	('One Piece',50),
	('Naruto',100),
	('Dracula',0)
INSERT INTO PHIEUNHAPSACH VALUES ('11/6/2021')
INSERT INTO SACH(MaDauSach, LuongTon) VALUES
	(2,100),
	(1,50)
INSERT INTO CT_PNS(MaPhieuNhapSach,MaSach,SoLuong,DonGiaNhap) VALUES
	(1,1,100,35000),
	(1,2,50,75000)
INSERT INTO TACGIA(TenTacGia) VALUES ('Oda Eiichiro'), ('Kishimoto Masashi')
INSERT INTO TT_TG(MaDauSach, MaTacGia) VALUES (1,1) , (2,2)
INSERT INTO TT_TL(MaDauSach, MaTheLoai) VALUES(1,6) , (2,6)
INSERT INTO TACGIA(TenTacGia) VALUES (N'Đặng Anh Tú'), (N'Lâm Văn Hồng'),('Arthur Conan Doyle'),('Fujiko Fujio')

/*=============================================================================*/
/*Log 2 database*/
/*=============================================================================*/
	
DROP TABLE HinhAnhSach

ALTER TABLE DAUSACH
ADD HinhAnhSach Image

/*=============================================================================*/
/*Log 3 database*/
/*=============================================================================*/

ALTER TABLE NGUOIDUNG
ADD TenNguoiDung NVARCHAR(50)

/*=============================================================================*/
/*Log 4 database*/
/*=============================================================================*/

ALTER TABLE PHIEUNHAPSACH ADD MaNguoiLap INT NOT NULL DEFAULT 1

ALTER TABLE PHIEUNHAPSACH
ADD CONSTRAINT FK_PHIEUNHAPSACH_NGUOIDUNG
FOREIGN KEY (MaNguoiLap) REFERENCES NGUOIDUNG (MaNguoiDung)

/*=============================================================================*/
/*Log 5 database*/
/*=============================================================================*/

ALTER TABLE THAMSO
DROP COLUMN ChoPhepThuLonHonNo

ALTER TABLE THAMSO
ADD ChoPhepThuLonHonNo BIT

/*=============================================================================*/
/*Log 6 database*/
/*=============================================================================*/

INSERT INTO THAMSO (LuongNhapToiThieu, LuongTonToiThieu, SoNoToiDa, LuongTonToiThieuSauBan, ChoPhepThuLonHonNo)
VALUES (150, 300, 20000, 20, 1)

/*=============================================================================*/ 
/*Log 7 database*/
/*=============================================================================*/ 
ALTER TABLE CT_BCT 
DROP CONSTRAINT FK_CT_BCT_SACH 

ALTER TABLE CT_BCT
DROP COLUMN MaSach

ALTER TABLE CT_BCT
ADD MaDauSach INT NOT NULL

ALTER TABLE CT_BCT 
ADD CONSTRAINT
FK_CT_BCT_DAUSACH 
FOREIGN KEY (MaDauSach) REFERENCES DAUSACH (MaDauSach)

/*=============================================================================*/ 
/*Log 8 database*/
/*=============================================================================*/ 

ALTER TABLE KHACHHANG
ADD TrangThai INT DEFAULT 0

/*=============================================================================*/ 
/*Log 9 database*/
/*=============================================================================*/ 

ALTER TABLE DAUSACH
ADD TrangThai INT DEFAULT 0

/*=============================================================================*/ 
/*Log 10 database*/
/*=============================================================================*/ 

ALTER TABLE CT_BCT
 DROP COLUMN PhatSinh 
 ALTER TABLE CT_BCT
 ADD NhapVao int 
 ALTER TABLE CT_BCT
 ADD BanRa int 

  ALTER TABLE CT_BCCN
 DROP column PhatSinh 
 ALTER TABLE CT_BCCN
 ADD NoMoi money 
  ALTER TABLE CT_BCCN
 ADD DaThu money

 alter table nguoidung add trangthai int default 0
 

