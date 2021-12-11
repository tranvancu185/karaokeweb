create database web_karaoke

use web_karaoke

set dateformat DMY

create table users(
	id char(6) primary key NOT Null,
	hoten nvarchar(50) NOT NULL,
	role int default 0,
	username char(20),
	password char(10)
)

create table customer(
	id int Identity(1,1) primary key,
	hoten nvarchar(50),
	phone char(11),
	username char(20) unique,
	password char(20)
)

create table room(
	id int Identity(1,1) primary key,
	name nvarchar(40),
	status int default 0,
	price float,
	typeRoom int
)

create table food(
	id int Identity(1,1) primary key,
	name nvarchar(40),
	price float,
	quantity int
)


create table bill(
	id int Identity(1,1) primary key,
	createAt DateTime,
	dateBook DateTime,
	checkIn time,
	checkOut time,
	total float default null,
	status int default 0,
	idFood int,
	idRoom int,
	idCus int,
	CONSTRAINT FK_bill_food FOREIGN KEY (idFood)REFERENCES food(id),
	CONSTRAINT FK_bill_room FOREIGN KEY (idRoom)REFERENCES room(id),
	CONSTRAINT FK_bill_cus FOREIGN KEY (idCus)REFERENCES customer(id)
)

create table billDetail(
	idFood int,
	idBill int,
	quantuty int,
	CONSTRAINT FK_billdetail_food FOREIGN KEY (idFood)REFERENCES food(id),
	CONSTRAINT FK_billdetail_bill FOREIGN KEY (idBill)REFERENCES bill(id),
)

insert into users values('admin','admin','0','admin','admin'),
('NV1',N'Phạm Duy Thái','2','paduta','paduta'),
('KT1',N'Trần Phú Quý','1','quy123','quy123')


insert into customer values('ABC','091231233','abc123','abc123')


insert into room values('Phong 1',0,60000,1),
('Phong 2',0,35000,3),
('Phong 3',0,40000,2),
('Phong 4',0,60000,1),
('Phong 5',0,40000,2),
('Phong 6',1,40000,2)


insert into food values(N'Cá Chưng',250000,20),
(N'Bia',35000,100),
('StrongBow',32000,132),
('Coca',25000,200),
('Snack',20000,100)


insert into bill values('13-07-2021','15-07-2021','12:00:00','15:00:00',0,0,1,1,1),
('22-07-2021','25-07-2021','12:00:00','15:00:00',0,0,3,1,1)

insert into billDetail values(1,1,3)
insert into billDetail values(2,1,10)




















































































































































































