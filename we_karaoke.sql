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
	email varchar(50),
	phone char(11),
	username char(20),
	password char(20)
)

create table room(
	id int Identity(1,1) primary key,
	name nvarchar(40),
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
	idFood int,
	idRoom int,
	idCus int,
	CONSTRAINT FK_bill_food FOREIGN KEY (idFood)REFERENCES food(id),
	CONSTRAINT FK_bill_room FOREIGN KEY (idRoom)REFERENCES room(id),
	CONSTRAINT FK_bill_cus FOREIGN KEY (idCus)REFERENCES customer(id)
)

insert into users values('admin','admin','0','admin','admin'),
('NV1',N'Phạm Duy Thái','2','paduta','paduta'),
('KT1',N'Trần Phú Quý','1','quy123','quy123')



insert into customer values('ABC','abc@gmail.com','091231233','abc123','abc123')


insert into room values('Phong 1',60000,1),('Phong 2',35000,3),('Phong 3',40000,2),('Phong 4',60000,1),('Phong 5',40000,2)


insert into food values(N'Cá Chưng',250000,20),('Phong 2',550000,20),('Phong 3',120000,20),('Phong 4',350000,20),('Phong 5',300000,20)

























































































































































































