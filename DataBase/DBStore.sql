USE [DBStore]
GO
/****** Object:  Table [dbo].[CustomerProduct]    Script Date: 5/8/2023 12:36:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerProduct](
	[IDCustomerProduct] [int] IDENTITY(1,1) NOT NULL,
	[FKCustomer] [int] NOT NULL,
	[FKProduct] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
	[IsAvailable] [bit] NOT NULL,
 CONSTRAINT [ID_CustomerProduct] PRIMARY KEY CLUSTERED 
(
	[IDCustomerProduct] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 5/8/2023 12:36:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[IDCustomer] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[Address] [varchar](50) NULL,
	[User] [varchar](50) NOT NULL,
	[Password] [varbinary](max) NULL,
	[FKRole] [int] NOT NULL,
	[IsAvailable] [bit] NOT NULL,
 CONSTRAINT [ID_Customers] PRIMARY KEY CLUSTERED 
(
	[IDCustomer] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 5/8/2023 12:36:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[IDProduct] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](50) NOT NULL,
	[Description] [varchar](max) NULL,
	[Price] [decimal](8, 2) NOT NULL,
	[Image] [varchar](max) NOT NULL,
	[Stock] [int] NOT NULL,
	[IsAvailable] [bit] NOT NULL,
 CONSTRAINT [ID_Products] PRIMARY KEY CLUSTERED 
(
	[IDProduct] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Productstore]    Script Date: 5/8/2023 12:36:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Productstore](
	[IDProductstore] [int] IDENTITY(1,1) NOT NULL,
	[FKProduct] [int] NOT NULL,
	[FKStore] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
	[IsAvailable] [bit] NOT NULL,
 CONSTRAINT [ID_Productstore] PRIMARY KEY CLUSTERED 
(
	[IDProductstore] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 5/8/2023 12:36:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[IDRole] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [varchar](100) NOT NULL,
	[IsAvailable] [bit] NOT NULL,
 CONSTRAINT [ID_Roles] PRIMARY KEY CLUSTERED 
(
	[IDRole] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Stores]    Script Date: 5/8/2023 12:36:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stores](
	[IDStore] [int] IDENTITY(1,1) NOT NULL,
	[Subsidiary] [varchar](50) NOT NULL,
	[Address] [varchar](100) NULL,
	[IsAvailable] [bit] NOT NULL,
 CONSTRAINT [ID_Stores] PRIMARY KEY CLUSTERED 
(
	[IDStore] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CustomerProduct] ADD  CONSTRAINT [DF_CustomerProduct_Date]  DEFAULT (getdate()) FOR [Date]
GO
ALTER TABLE [dbo].[CustomerProduct] ADD  CONSTRAINT [DF_CustomerProduct_IsAvailable]  DEFAULT ((1)) FOR [IsAvailable]
GO
ALTER TABLE [dbo].[Customers] ADD  CONSTRAINT [DF_Customers_IsAvailable]  DEFAULT ((1)) FOR [IsAvailable]
GO
ALTER TABLE [dbo].[Products] ADD  CONSTRAINT [DF_Products_IsAvailable]  DEFAULT ((1)) FOR [IsAvailable]
GO
ALTER TABLE [dbo].[Productstore] ADD  CONSTRAINT [DF_Productstore_Date]  DEFAULT (getdate()) FOR [Date]
GO
ALTER TABLE [dbo].[Productstore] ADD  CONSTRAINT [DF_Productstore_IsAvailable]  DEFAULT ((1)) FOR [IsAvailable]
GO
ALTER TABLE [dbo].[Roles] ADD  CONSTRAINT [DF_Roles_IsAvailable]  DEFAULT ((1)) FOR [IsAvailable]
GO
ALTER TABLE [dbo].[Stores] ADD  CONSTRAINT [DF_Stores_IsAvailable]  DEFAULT ((1)) FOR [IsAvailable]
GO
ALTER TABLE [dbo].[CustomerProduct]  WITH CHECK ADD  CONSTRAINT [FK_CustomerProduct_Customer] FOREIGN KEY([FKCustomer])
REFERENCES [dbo].[Customers] ([IDCustomer])
GO
ALTER TABLE [dbo].[CustomerProduct] CHECK CONSTRAINT [FK_CustomerProduct_Customer]
GO
ALTER TABLE [dbo].[CustomerProduct]  WITH CHECK ADD  CONSTRAINT [FK_CustomerProduct_Products] FOREIGN KEY([FKProduct])
REFERENCES [dbo].[Products] ([IDProduct])
GO
ALTER TABLE [dbo].[CustomerProduct] CHECK CONSTRAINT [FK_CustomerProduct_Products]
GO
ALTER TABLE [dbo].[Customers]  WITH CHECK ADD  CONSTRAINT [FK_Customers_Roles] FOREIGN KEY([FKRole])
REFERENCES [dbo].[Roles] ([IDRole])
GO
ALTER TABLE [dbo].[Customers] CHECK CONSTRAINT [FK_Customers_Roles]
GO
ALTER TABLE [dbo].[Productstore]  WITH CHECK ADD  CONSTRAINT [FK_Productstore_Products] FOREIGN KEY([FKProduct])
REFERENCES [dbo].[Products] ([IDProduct])
GO
ALTER TABLE [dbo].[Productstore] CHECK CONSTRAINT [FK_Productstore_Products]
GO
ALTER TABLE [dbo].[Productstore]  WITH CHECK ADD  CONSTRAINT [FK_Productstore_Stores] FOREIGN KEY([FKStore])
REFERENCES [dbo].[Stores] ([IDStore])
GO
ALTER TABLE [dbo].[Productstore] CHECK CONSTRAINT [FK_Productstore_Stores]
GO
