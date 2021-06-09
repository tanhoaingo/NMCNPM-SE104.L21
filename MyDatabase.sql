CREATE DATABASE MYDATABASE
USE MYDATABASE

/*=============================================================================*/

CREATE TABLE PHIEUNHAPSACH(
	MaPhieuNhapSach INT IDENTITY(1,1),
	NgayNhap SMALLDATETIME,
)

ALTER TABLE PHIEUNHAPSACH
ADD CONSTRAINT PK_PHIEUNHAPSACH
PRIMARY KEY (MaPhieuNhapSach)

/*=============================================================================*/

CREATE TABLE THELOAI(
	MaTheLoai INT IDENTITY(1,1),
	TenTheLoai NVARCHAR(50),
)

ALTER TABLE THELOAI 
ADD CONSTRAINT PK_THELOAI
PRIMARY KEY (MaTheLoai)

/*=============================================================================*/

CREATE TABLE DAUSACH(
	MaDauSach INT  IDENTITY(1,1),
	MaTheLoai INT NOT NULL,
	TenSach NVARCHAR(50),
	TheLoai NVARCHAR(50),
	LuongTon INT,
)

ALTER TABLE DAUSACH
ADD CONSTRAINT PK_DAUSACH
PRIMARY KEY (MaDauSach)

ALTER TABLE DAUSACH
ADD CONSTRAINT FK_DAUSACH_THELOAI
FOREIGN KEY (MaTheLoai) REFERENCES THELOAI (MaTheLoai)

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

CREATE TABLE HOADON(
	MaHoaDon INT IDENTITY(1,1),
	MaKhachHang INT NOT NULL,
	NgayLapHoaDon DATETIME,
	TongTien MONEY,
	SoTienTra MONEY,
	ConLai MONEY,
)

ALTER TABLE HOADON
ADD CONSTRAINT PK_HOADON
PRIMARY KEY (MaHoaDon)

ALTER TABLE HOADON
ADD CONSTRAINT FK_HOADON_KHACHHANG
FOREIGN KEY (MaKhachHang) REFERENCES KHACHHANG (MaKhachHang)

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