create database web_karaoke

use web_karaoke

set dateformat DMY

create table users(
	id char(20) primary key NOT Null,
	hoten nvarchar(50) NOT NULL,
	role int,
	avatar nvarchar(100),
	username char(20),
	password char(10)
	CONSTRAINT FK_user_role FOREIGN KEY (role)REFERENCES role(id)
)

create table customer(
	id int Identity(1,1) primary key,
	hoten nvarchar(50),
	phone char(11),
)

create table room(
	id int Identity(1,1) primary key,
	name nvarchar(40),
	status int default 0,
	price float,
	typeRoom int
)

create table category(
	id int identity(1,1) primary key,
	name nvarchar(50)
)

create table food(
	id int Identity(1,1) primary key,
	name nvarchar(40),
	price float,
	quantity int,
	image nvarchar(100),
	idCategory int,
	CONSTRAINT FK_food_category FOREIGN KEY (idCategory)REFERENCES category(id)
)

create table bill(
	id int Identity(1,1) primary key,
	createAt DateTime,
	dateBook DateTime,
	checkIn time,
	checkOut time,
	total float default null,
	status int default 0,
	idRoom int,
	idCus int,
	CONSTRAINT FK_bill_room FOREIGN KEY (idRoom)REFERENCES room(id),
	CONSTRAINT FK_bill_cus FOREIGN KEY (idCus)REFERENCES customer(id)
)

create table billDetail(
	id int Identity(1,1) primary key,
	idFood int,
	idBill int,
	quantity int,
	CONSTRAINT FK_billdetail_food FOREIGN KEY (idFood)REFERENCES food(id),
	CONSTRAINT FK_billdetail_bill FOREIGN KEY (idBill)REFERENCES bill(id),
)

create table role(
	id int identity(1,1) primary key,
	name nvarchar(50)
)



insert into role values('admin'),(N'Kế toán'),(N'Nhân viên'),(N'Quản lý')

insert into users values('admin','admin','1','~/AdminAssets/images/avatars/thumb-3.jpg','admin','admin'),
('NV1',N'Phạm Duy Thái','3','~/AdminAssets/images/avatars/thumb-3.jpg','paduta','paduta'),
('KT1',N'Trần Phú Quý','2','~/AdminAssets/images/avatars/thumb-3.jpg','quy123','quy123')

insert into room values('Phong 1',0,60000,1),
('Phong 2',0,35000,3),
('Phong 3',0,40000,2),
('Phong 4',0,60000,1),
('Phong 5',0,40000,2),
('Phong 6',1,40000,2)

insert into category values(N'Nước giải khát'),
(N'Món ăn chính'),
(N'Món ăn vặt'),
(N'Các loại rượu bia'),
(N'Trái cây')























































































































































































