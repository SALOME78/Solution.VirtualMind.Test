USE [VirtualMind]
GO
/****** Object:  Table [dbo].[Documents]    Script Date: 11/20/2020 5:03:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Documents](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](50) NULL,
 CONSTRAINT [PK_Documents] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ExchangeCurrency]    Script Date: 11/20/2020 5:03:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExchangeCurrency](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nchar](10) NULL,
	[Description] [nvarchar](50) NULL,
	[Limit] [int] NULL,
 CONSTRAINT [PK_ExchangeRate] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transactions]    Script Date: 11/20/2020 5:03:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transactions](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IDUser] [int] NULL,
	[IDDocument] [int] NULL,
	[IDExchangeCurrency] [int] NULL,
	[Description] [nvarchar](50) NULL,
	[Amount] [int] NULL,
	[ExchangeRate] [decimal](10, 3) NULL,
	[SubTotal] [decimal](10, 2) NULL,
	[CreateDate] [datetime] NULL,
	[ModifyDate] [datetime] NULL,
	[IsEnabled] [bit] NULL,
 CONSTRAINT [PK_Transactions] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 11/20/2020 5:03:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nchar](10) NULL,
	[Password] [nvarchar](50) NULL,
	[FullName] [nvarchar](50) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Documents] ON 

INSERT [dbo].[Documents] ([ID], [Description]) VALUES (1, N'CURRENCY PURCHASE')
SET IDENTITY_INSERT [dbo].[Documents] OFF
GO
SET IDENTITY_INSERT [dbo].[ExchangeCurrency] ON 

INSERT [dbo].[ExchangeCurrency] ([ID], [Code], [Description], [Limit]) VALUES (1, N'USD-A     ', N'AMARICAN DOLLAR (USD)', 200)
INSERT [dbo].[ExchangeCurrency] ([ID], [Code], [Description], [Limit]) VALUES (2, N'BRL       ', N'BRAZILIAN REAL (BRL)', 300)
INSERT [dbo].[ExchangeCurrency] ([ID], [Code], [Description], [Limit]) VALUES (3, N'USD-C     ', N'CANADIAN DOLLAR (USD)', 0)
SET IDENTITY_INSERT [dbo].[ExchangeCurrency] OFF
GO
SET IDENTITY_INSERT [dbo].[Transactions] ON 

INSERT [dbo].[Transactions] ([ID], [IDUser], [IDDocument], [IDExchangeCurrency], [Description], [Amount], [ExchangeRate], [SubTotal], [CreateDate], [ModifyDate], [IsEnabled]) VALUES (1, 1, 1, 1, N'TRANSACTION OF CURRENCY PURCHASE', 100, CAST(79.250 AS Decimal(10, 3)), CAST(1.26 AS Decimal(10, 2)), CAST(N'2020-11-18T12:23:58.310' AS DateTime), NULL, 1)
INSERT [dbo].[Transactions] ([ID], [IDUser], [IDDocument], [IDExchangeCurrency], [Description], [Amount], [ExchangeRate], [SubTotal], [CreateDate], [ModifyDate], [IsEnabled]) VALUES (2, 2, 1, 2, N'TRANSACTION OF CURRENCY PURCHASE', 250, CAST(19.813 AS Decimal(10, 3)), CAST(12.62 AS Decimal(10, 2)), CAST(N'2020-11-18T12:42:05.847' AS DateTime), NULL, 1)
INSERT [dbo].[Transactions] ([ID], [IDUser], [IDDocument], [IDExchangeCurrency], [Description], [Amount], [ExchangeRate], [SubTotal], [CreateDate], [ModifyDate], [IsEnabled]) VALUES (3, 5, 1, 1, N'TRANSACTION OF CURRENCY PURCHASE', 50, CAST(79.500 AS Decimal(10, 3)), CAST(0.63 AS Decimal(10, 2)), CAST(N'2020-11-20T16:17:43.510' AS DateTime), NULL, 1)
INSERT [dbo].[Transactions] ([ID], [IDUser], [IDDocument], [IDExchangeCurrency], [Description], [Amount], [ExchangeRate], [SubTotal], [CreateDate], [ModifyDate], [IsEnabled]) VALUES (4, 4, 1, 2, N'TRANSACTION OF CURRENCY PURCHASE', 150, CAST(19.875 AS Decimal(10, 3)), CAST(7.55 AS Decimal(10, 2)), CAST(N'2020-11-20T16:28:19.893' AS DateTime), NULL, 1)
SET IDENTITY_INSERT [dbo].[Transactions] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([ID], [UserName], [Password], [FullName]) VALUES (1, N'OPERATOR1 ', N'AXj7JLztyTu2ERW3/IVg0Q==', N'USER OPERATOR 1')
INSERT [dbo].[Users] ([ID], [UserName], [Password], [FullName]) VALUES (2, N'OPERATOR2 ', N'AXj7JLztyTu2ERW3/IVg0Q==', N'USER OPERATOR 2')
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
/****** Object:  StoredProcedure [dbo].[sp_Insert_CurrencyPurchase]    Script Date: 11/20/2020 5:03:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[sp_Insert_CurrencyPurchase]
  @UserID INT = 0,
  @DocumentID INT = 0,   
  @ExchangeCurrencyID INT = 0,
  @Description nvarchar(50) = NULL,
  @Amount INT = 0,
  @ExchangeRate decimal(10, 3) = NULL,
  @SubTotal decimal(10, 2) = NULL

AS
BEGIN
 DECLARE @ID INT;

 INSERT INTO Transactions VALUES(@UserID,@DocumentID,@ExchangeCurrencyID,@Description,@Amount,@ExchangeRate,@SubTotal,GETDATE(),NULL,1);

END
GO
