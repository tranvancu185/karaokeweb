USE [web_karaoke]
GO
/****** Object:  Table [dbo].[bill]    Script Date: 08/01/2022 10:19:10 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bill](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[createAt] [datetime] NULL,
	[dateBook] [datetime] NULL,
	[checkIn] [time](7) NULL,
	[checkOut] [time](7) NULL,
	[total] [float] NULL,
	[status] [int] NULL,
	[idRoom] [int] NULL,
	[idCus] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[billDetail]    Script Date: 08/01/2022 10:19:10 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[billDetail](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idFood] [int] NULL,
	[idBill] [int] NULL,
	[quantity] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[category]    Script Date: 08/01/2022 10:19:10 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[category](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[customer]    Script Date: 08/01/2022 10:19:10 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[customer](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[hoten] [nvarchar](50) NULL,
	[phone] [char](11) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[food]    Script Date: 08/01/2022 10:19:10 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[food](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](40) NULL,
	[price] [float] NULL,
	[quantity] [int] NULL,
	[image] [nvarchar](100) NULL,
	[idCategory] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[role]    Script Date: 08/01/2022 10:19:10 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[role](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[room]    Script Date: 08/01/2022 10:19:10 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[room](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](40) NULL,
	[status] [int] NULL,
	[price] [float] NULL,
	[typeRoom] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[users]    Script Date: 08/01/2022 10:19:10 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users](
	[id] [char](20) NOT NULL,
	[hoten] [nvarchar](50) NOT NULL,
	[role] [int] NULL,
	[avatar] [nvarchar](100) NULL,
	[username] [char](20) NULL,
	[password] [char](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[bill] ON 

INSERT [dbo].[bill] ([id], [createAt], [dateBook], [checkIn], [checkOut], [total], [status], [idRoom], [idCus]) VALUES (233, CAST(N'2022-01-08T20:41:09.817' AS DateTime), CAST(N'2022-01-10T00:00:00.000' AS DateTime), CAST(N'06:00:00' AS Time), CAST(N'08:00:00' AS Time), 1193000, 1, 9, 9)
INSERT [dbo].[bill] ([id], [createAt], [dateBook], [checkIn], [checkOut], [total], [status], [idRoom], [idCus]) VALUES (234, CAST(N'2022-01-08T20:42:12.133' AS DateTime), CAST(N'2022-01-09T00:00:00.000' AS DateTime), CAST(N'18:00:00' AS Time), CAST(N'20:00:00' AS Time), 403000, 0, 15, 8)
INSERT [dbo].[bill] ([id], [createAt], [dateBook], [checkIn], [checkOut], [total], [status], [idRoom], [idCus]) VALUES (235, CAST(N'2022-01-08T20:42:45.720' AS DateTime), CAST(N'2022-01-09T00:00:00.000' AS DateTime), CAST(N'18:00:00' AS Time), CAST(N'21:00:00' AS Time), 473000, 0, 21, 36)
SET IDENTITY_INSERT [dbo].[bill] OFF
GO
SET IDENTITY_INSERT [dbo].[billDetail] ON 

INSERT [dbo].[billDetail] ([id], [idFood], [idBill], [quantity]) VALUES (254, 14, 234, 1)
INSERT [dbo].[billDetail] ([id], [idFood], [idBill], [quantity]) VALUES (255, 13, 234, 1)
INSERT [dbo].[billDetail] ([id], [idFood], [idBill], [quantity]) VALUES (256, 12, 234, 1)
INSERT [dbo].[billDetail] ([id], [idFood], [idBill], [quantity]) VALUES (257, 16, 234, 1)
INSERT [dbo].[billDetail] ([id], [idFood], [idBill], [quantity]) VALUES (258, 14, 235, 1)
INSERT [dbo].[billDetail] ([id], [idFood], [idBill], [quantity]) VALUES (259, 13, 235, 1)
INSERT [dbo].[billDetail] ([id], [idFood], [idBill], [quantity]) VALUES (260, 12, 235, 1)
INSERT [dbo].[billDetail] ([id], [idFood], [idBill], [quantity]) VALUES (261, 16, 235, 1)
INSERT [dbo].[billDetail] ([id], [idFood], [idBill], [quantity]) VALUES (262, 25, 233, 15)
INSERT [dbo].[billDetail] ([id], [idFood], [idBill], [quantity]) VALUES (263, 15, 233, 1)
INSERT [dbo].[billDetail] ([id], [idFood], [idBill], [quantity]) VALUES (264, 20, 233, 10)
INSERT [dbo].[billDetail] ([id], [idFood], [idBill], [quantity]) VALUES (265, 16, 233, 3)
INSERT [dbo].[billDetail] ([id], [idFood], [idBill], [quantity]) VALUES (266, 30, 233, 2)
SET IDENTITY_INSERT [dbo].[billDetail] OFF
GO
SET IDENTITY_INSERT [dbo].[category] ON 

INSERT [dbo].[category] ([id], [name]) VALUES (1, N'Nước giải khát')
INSERT [dbo].[category] ([id], [name]) VALUES (2, N'Món ăn chính')
INSERT [dbo].[category] ([id], [name]) VALUES (3, N'Món ăn vặt')
INSERT [dbo].[category] ([id], [name]) VALUES (4, N'Các loại rượu bia')
INSERT [dbo].[category] ([id], [name]) VALUES (5, N'Trái cây')
SET IDENTITY_INSERT [dbo].[category] OFF
GO
SET IDENTITY_INSERT [dbo].[customer] ON 

INSERT [dbo].[customer] ([id], [hoten], [phone]) VALUES (8, N'Trần Phú Quý', N'0965135278 ')
INSERT [dbo].[customer] ([id], [hoten], [phone]) VALUES (9, N'Phạm Duy Thái', N'0797559163 ')
INSERT [dbo].[customer] ([id], [hoten], [phone]) VALUES (33, N'Lê Minh Hiếu', N'0902733336 ')
INSERT [dbo].[customer] ([id], [hoten], [phone]) VALUES (34, N'Trần Mỹ Kim', N'0984854642 ')
INSERT [dbo].[customer] ([id], [hoten], [phone]) VALUES (35, N'Trần Mỹ Ngọc', N'0902733337 ')
INSERT [dbo].[customer] ([id], [hoten], [phone]) VALUES (36, N'Phùng Thanh', N'0909889192 ')
SET IDENTITY_INSERT [dbo].[customer] OFF
GO
SET IDENTITY_INSERT [dbo].[food] ON 

INSERT [dbo].[food] ([id], [name], [price], [quantity], [image], [idCategory]) VALUES (12, N'Sụn gà rang muối', 98000, 99, N'Sun-ga-2048x136520221341729.jpg', 3)
INSERT [dbo].[food] ([id], [name], [price], [quantity], [image], [idCategory]) VALUES (13, N'Xúc xích nướng tiêu', 65000, 150, N'Xuc-xich-2048x136520221408490.jpg', 3)
INSERT [dbo].[food] ([id], [name], [price], [quantity], [image], [idCategory]) VALUES (14, N'Chả giò hải sản', 55000, 34, N'chả-giò-hs20221439235.jpg', 3)
INSERT [dbo].[food] ([id], [name], [price], [quantity], [image], [idCategory]) VALUES (15, N'Cơm chiên hải sản', 78000, 23, N'Com-chien-2048x136520221530572.jpg', 2)
INSERT [dbo].[food] ([id], [name], [price], [quantity], [image], [idCategory]) VALUES (16, N'Đậu hũ xóc tỏi ớt', 45000, 32, N'87941499_3054042021324847_620747474352472064_n-2048x167020221605291.jpg', 3)
INSERT [dbo].[food] ([id], [name], [price], [quantity], [image], [idCategory]) VALUES (17, N'Mì xào hải sản', 198000, 30, N'Mì-xào-hải-sản20221710323.jpg', 2)
INSERT [dbo].[food] ([id], [name], [price], [quantity], [image], [idCategory]) VALUES (18, N'Snack khoai tảo biển', 15000, 50, N'snack-khoai-tay-vi-tao-bien-nori-poca-goi-52g-20191107152004577320221836572.jpg', 3)
INSERT [dbo].[food] ([id], [name], [price], [quantity], [image], [idCategory]) VALUES (19, N'Snack khoai tây vị bít tết', 15000, 50, N'kaah8gxnc36ts20221918855.jpg', 3)
INSERT [dbo].[food] ([id], [name], [price], [quantity], [image], [idCategory]) VALUES (20, N'Coca cola', 20000, 100, N'f6a87bd170eab0d27850ca866bcbc66e20222030586.jpg', 1)
INSERT [dbo].[food] ([id], [name], [price], [quantity], [image], [idCategory]) VALUES (21, N'Pepsi', 20000, 100, N'893458801222820222049294.jpg', 1)
INSERT [dbo].[food] ([id], [name], [price], [quantity], [image], [idCategory]) VALUES (22, N'Pepsi chanh không calo', 20000, 100, N'nuoc-ngot-pepsi-khong-calo-vi-chanh-320ml-20210714205817872220222122658.jpg', 1)
INSERT [dbo].[food] ([id], [name], [price], [quantity], [image], [idCategory]) VALUES (23, N'Nước suối Aquafina', 15000, 200, N'nuoc-suoi-aquafina-500ml-09b27a2f-6dee-409c-b420-0800c46994e220222937097.jpg', 1)
INSERT [dbo].[food] ([id], [name], [price], [quantity], [image], [idCategory]) VALUES (24, N'Nước suối Dasen', 15000, 200, N'Nuoc-suoi20222954965.png', 1)
INSERT [dbo].[food] ([id], [name], [price], [quantity], [image], [idCategory]) VALUES (25, N'Bia Bubweiser', 28000, 500, N'893609429120320223059131.jpg', 4)
INSERT [dbo].[food] ([id], [name], [price], [quantity], [image], [idCategory]) VALUES (26, N'Bia tiger nâu', 25000, 500, N'8934822101336-20223124804.jpg', 4)
INSERT [dbo].[food] ([id], [name], [price], [quantity], [image], [idCategory]) VALUES (27, N'Bia Heniken', 25000, 5000, N'893482221233920223239134.jpg', 4)
INSERT [dbo].[food] ([id], [name], [price], [quantity], [image], [idCategory]) VALUES (28, N'Rượu Soju Táo', 78000, 100, N'ruou-soju-han-quoc20223344880.jpeg', 4)
INSERT [dbo].[food] ([id], [name], [price], [quantity], [image], [idCategory]) VALUES (29, N'Rượu Soju classic', 70000, 100, N'2b_9ef18ff2127d4ec79c9c9db73b04e95b20223431656.png', 4)
INSERT [dbo].[food] ([id], [name], [price], [quantity], [image], [idCategory]) VALUES (30, N'Trái cây đĩa', 80000, 40, N'Mat-lanh-trai-cay-dia-o-Sai-Gon_620223535323.jpg', 5)
SET IDENTITY_INSERT [dbo].[food] OFF
GO
SET IDENTITY_INSERT [dbo].[role] ON 

INSERT [dbo].[role] ([id], [name]) VALUES (1, N'admin')
INSERT [dbo].[role] ([id], [name]) VALUES (2, N'Kế toán')
INSERT [dbo].[role] ([id], [name]) VALUES (3, N'Nhân viên')
INSERT [dbo].[role] ([id], [name]) VALUES (4, N'Quản lý')
SET IDENTITY_INSERT [dbo].[role] OFF
GO
SET IDENTITY_INSERT [dbo].[room] ON 

INSERT [dbo].[room] ([id], [name], [status], [price], [typeRoom]) VALUES (9, N'Phòng VIP1', NULL, 100000, 1)
INSERT [dbo].[room] ([id], [name], [status], [price], [typeRoom]) VALUES (10, N'Phòng VIP2', 0, 105000, 1)
INSERT [dbo].[room] ([id], [name], [status], [price], [typeRoom]) VALUES (11, N'Phong VIP3', 0, 100000, 1)
INSERT [dbo].[room] ([id], [name], [status], [price], [typeRoom]) VALUES (12, N'Phòng VIP4', 0, 100000, 1)
INSERT [dbo].[room] ([id], [name], [status], [price], [typeRoom]) VALUES (13, N'Phòng VIP5', 0, 100000, 1)
INSERT [dbo].[room] ([id], [name], [status], [price], [typeRoom]) VALUES (14, N'Phòng CC1', NULL, 70000, 2)
INSERT [dbo].[room] ([id], [name], [status], [price], [typeRoom]) VALUES (15, N'Phòng CC2', 0, 70000, 2)
INSERT [dbo].[room] ([id], [name], [status], [price], [typeRoom]) VALUES (16, N'Phòng CC3', 0, 70000, 2)
INSERT [dbo].[room] ([id], [name], [status], [price], [typeRoom]) VALUES (17, N'Phòng CC5', 0, 70000, 2)
INSERT [dbo].[room] ([id], [name], [status], [price], [typeRoom]) VALUES (18, N'Phòng T1', 0, 50000, 3)
INSERT [dbo].[room] ([id], [name], [status], [price], [typeRoom]) VALUES (19, N'Phòng T2', 0, 50000, 3)
INSERT [dbo].[room] ([id], [name], [status], [price], [typeRoom]) VALUES (20, N'Phòng T3', 0, 50000, 3)
INSERT [dbo].[room] ([id], [name], [status], [price], [typeRoom]) VALUES (21, N'Phòng T4', 0, 50000, 3)
INSERT [dbo].[room] ([id], [name], [status], [price], [typeRoom]) VALUES (22, N'Phòng VIP1', 0, 120000, 1)
SET IDENTITY_INSERT [dbo].[room] OFF
GO
INSERT [dbo].[users] ([id], [hoten], [role], [avatar], [username], [password]) VALUES (N'admin               ', N'admin', 1, N'images20221428649.png', N'admin               ', N'admin     ')
INSERT [dbo].[users] ([id], [hoten], [role], [avatar], [username], [password]) VALUES (N'NV0001              ', N'Phạm Duy Thái', 3, N'7879-smile20221347326.png', N'thai.pd             ', N'admin     ')
INSERT [dbo].[users] ([id], [hoten], [role], [avatar], [username], [password]) VALUES (N'QLCN-TPHCM01        ', N'Trần Phú Quý', 2, N'download20221354782.png', N'quy.tp              ', N'admin     ')
GO
ALTER TABLE [dbo].[bill] ADD  DEFAULT (NULL) FOR [total]
GO
ALTER TABLE [dbo].[bill] ADD  DEFAULT ((0)) FOR [status]
GO
ALTER TABLE [dbo].[room] ADD  DEFAULT ((0)) FOR [status]
GO
ALTER TABLE [dbo].[bill]  WITH CHECK ADD  CONSTRAINT [FK_bill_cus] FOREIGN KEY([idCus])
REFERENCES [dbo].[customer] ([id])
GO
ALTER TABLE [dbo].[bill] CHECK CONSTRAINT [FK_bill_cus]
GO
ALTER TABLE [dbo].[bill]  WITH CHECK ADD  CONSTRAINT [FK_bill_room] FOREIGN KEY([idRoom])
REFERENCES [dbo].[room] ([id])
GO
ALTER TABLE [dbo].[bill] CHECK CONSTRAINT [FK_bill_room]
GO
ALTER TABLE [dbo].[billDetail]  WITH CHECK ADD  CONSTRAINT [FK_billdetail_bill] FOREIGN KEY([idBill])
REFERENCES [dbo].[bill] ([id])
GO
ALTER TABLE [dbo].[billDetail] CHECK CONSTRAINT [FK_billdetail_bill]
GO
ALTER TABLE [dbo].[billDetail]  WITH CHECK ADD  CONSTRAINT [FK_billdetail_food] FOREIGN KEY([idFood])
REFERENCES [dbo].[food] ([id])
GO
ALTER TABLE [dbo].[billDetail] CHECK CONSTRAINT [FK_billdetail_food]
GO
ALTER TABLE [dbo].[food]  WITH CHECK ADD  CONSTRAINT [FK_food_category] FOREIGN KEY([idCategory])
REFERENCES [dbo].[category] ([id])
GO
ALTER TABLE [dbo].[food] CHECK CONSTRAINT [FK_food_category]
GO
ALTER TABLE [dbo].[users]  WITH CHECK ADD  CONSTRAINT [FK_user_role] FOREIGN KEY([role])
REFERENCES [dbo].[role] ([id])
GO
ALTER TABLE [dbo].[users] CHECK CONSTRAINT [FK_user_role]
GO
