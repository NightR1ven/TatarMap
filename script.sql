USE [TatarCulturDb]
GO
/****** Object:  Table [dbo].[Comment]    Script Date: 15.01.2021 15:12:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comment](
	[IdComment] [int] IDENTITY(1,1) NOT NULL,
	[IdUser] [int] NULL,
	[IdObject] [int] NULL,
	[Comment] [nvarchar](max) NULL,
	[Star] [int] NULL,
 CONSTRAINT [PK_Comment] PRIMARY KEY CLUSTERED 
(
	[IdComment] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Event]    Script Date: 15.01.2021 15:12:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Event](
	[IdEvent] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nchar](50) NOT NULL,
	[Coin] [int] NULL,
	[DateStarEvent] [date] NULL,
	[DateEndEvent] [date] NULL,
 CONSTRAINT [PK_Event] PRIMARY KEY CLUSTERED 
(
	[IdEvent] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Object]    Script Date: 15.01.2021 15:12:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Object](
	[IdObject] [int] IDENTITY(1,1) NOT NULL,
	[IdType] [int] NOT NULL,
	[Name] [nvarchar](100) NULL,
	[ObjectPhoto] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Latitude] [float] NULL,
	[Longitude] [float] NULL,
 CONSTRAINT [PK_Object] PRIMARY KEY CLUSTERED 
(
	[IdObject] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Type]    Script Date: 15.01.2021 15:12:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Type](
	[IdType] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Type] PRIMARY KEY CLUSTERED 
(
	[IdType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 15.01.2021 15:12:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[IdUser] [int] IDENTITY(1,1) NOT NULL,
	[Login] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[IdEvent] [int] NULL,
	[Coin] [int] NULL,
	[IdRols] [int] NULL,
	[UserPhoto] [nvarchar](max) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[IdUser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRols]    Script Date: 15.01.2021 15:12:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRols](
	[IdRols] [int] IDENTITY(1,1) NOT NULL,
	[NameRols] [nvarchar](50) NULL,
 CONSTRAINT [PK_UserRols] PRIMARY KEY CLUSTERED 
(
	[IdRols] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Comment] ON 

INSERT [dbo].[Comment] ([IdComment], [IdUser], [IdObject], [Comment], [Star]) VALUES (1, 2, 1, N'Cooll', 5)
INSERT [dbo].[Comment] ([IdComment], [IdUser], [IdObject], [Comment], [Star]) VALUES (2, 3, 1, N'Nice', 3)
SET IDENTITY_INSERT [dbo].[Comment] OFF
GO
SET IDENTITY_INSERT [dbo].[Object] ON 

INSERT [dbo].[Object] ([IdObject], [IdType], [Name], [ObjectPhoto], [Description], [Latitude], [Longitude]) VALUES (1, 1, N'Кул Шариф', N'11KulShar.jpg', N'cdfgseb', 55.799500914697, 49.106063271515069)
INSERT [dbo].[Object] ([IdObject], [IdType], [Name], [ObjectPhoto], [Description], [Latitude], [Longitude]) VALUES (2, 2, N'Мак', NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Object] OFF
GO
SET IDENTITY_INSERT [dbo].[Type] ON 

INSERT [dbo].[Type] ([IdType], [Name]) VALUES (1, N'Памятник                                          ')
INSERT [dbo].[Type] ([IdType], [Name]) VALUES (2, N'Ресторан                                          ')
SET IDENTITY_INSERT [dbo].[Type] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([IdUser], [Login], [Password], [IdEvent], [Coin], [IdRols], [UserPhoto]) VALUES (1, N'admin', N'admin', NULL, NULL, 1, NULL)
INSERT [dbo].[User] ([IdUser], [Login], [Password], [IdEvent], [Coin], [IdRols], [UserPhoto]) VALUES (2, N'editor', N'editor', NULL, NULL, 2, NULL)
INSERT [dbo].[User] ([IdUser], [Login], [Password], [IdEvent], [Coin], [IdRols], [UserPhoto]) VALUES (3, N'azat165', N'azat165', NULL, NULL, 3, N'1KulShar.jpg')
INSERT [dbo].[User] ([IdUser], [Login], [Password], [IdEvent], [Coin], [IdRols], [UserPhoto]) VALUES (4, N'azlot', N'12', NULL, NULL, 1, NULL)
INSERT [dbo].[User] ([IdUser], [Login], [Password], [IdEvent], [Coin], [IdRols], [UserPhoto]) VALUES (5, N'321', N'123', NULL, NULL, NULL, NULL)
INSERT [dbo].[User] ([IdUser], [Login], [Password], [IdEvent], [Coin], [IdRols], [UserPhoto]) VALUES (6, N'ba', N'123', NULL, NULL, NULL, NULL)
INSERT [dbo].[User] ([IdUser], [Login], [Password], [IdEvent], [Coin], [IdRols], [UserPhoto]) VALUES (7, N'cas', N'cas', NULL, NULL, NULL, NULL)
INSERT [dbo].[User] ([IdUser], [Login], [Password], [IdEvent], [Coin], [IdRols], [UserPhoto]) VALUES (8, N'vqa', N'axc', NULL, NULL, 3, NULL)
INSERT [dbo].[User] ([IdUser], [Login], [Password], [IdEvent], [Coin], [IdRols], [UserPhoto]) VALUES (9, N'bac', N'bac', NULL, NULL, 3, N'11KulShar.jpg')
SET IDENTITY_INSERT [dbo].[User] OFF
GO
SET IDENTITY_INSERT [dbo].[UserRols] ON 

INSERT [dbo].[UserRols] ([IdRols], [NameRols]) VALUES (1, N'Admin')
INSERT [dbo].[UserRols] ([IdRols], [NameRols]) VALUES (2, N'Redacktor')
INSERT [dbo].[UserRols] ([IdRols], [NameRols]) VALUES (3, N'Client')
SET IDENTITY_INSERT [dbo].[UserRols] OFF
GO
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_Object] FOREIGN KEY([IdObject])
REFERENCES [dbo].[Object] ([IdObject])
GO
ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_Comment_Object]
GO
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_User] FOREIGN KEY([IdUser])
REFERENCES [dbo].[User] ([IdUser])
GO
ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_Comment_User]
GO
ALTER TABLE [dbo].[Object]  WITH CHECK ADD  CONSTRAINT [FK_Object_Type] FOREIGN KEY([IdType])
REFERENCES [dbo].[Type] ([IdType])
GO
ALTER TABLE [dbo].[Object] CHECK CONSTRAINT [FK_Object_Type]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Event] FOREIGN KEY([IdEvent])
REFERENCES [dbo].[Event] ([IdEvent])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Event]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_UserRols] FOREIGN KEY([IdRols])
REFERENCES [dbo].[UserRols] ([IdRols])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_UserRols]
GO
