CREATE DATABASE ExchangeDb
GO
USE [ExchangeDb]
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Logs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Message] [varchar](500) NULL,
	[LogType] [varchar](50) NULL,
 CONSTRAINT [PK_Logs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurchaseLimit]    Script Date: 4/29/2021 11:03:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseLimit](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Currency] [varchar](3) NOT NULL,
	[AmountLimit] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_PurchaseLimit] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Purchases]    Script Date: 4/29/2021 11:03:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Purchases](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Amount] [decimal](18, 2) NULL,
	[Currency] [varchar](3) NULL,
	[Rate] [decimal](18, 2) NULL,
	[UserId] [int] NOT NULL,
	[AmountResult] [decimal](18, 2) NULL,
	[Date] [datetime] NOT NULL,
 CONSTRAINT [PK_Purchases] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 4/29/2021 11:03:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](20) NOT NULL,
	[FirstName] [varchar](100) NOT NULL,
	[LastName] [varchar](100) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Purchases] ADD  CONSTRAINT [DF_Purchases_Date]  DEFAULT (getdate()) FOR [Date]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Purchases]  WITH CHECK ADD  CONSTRAINT [FK_Purchases_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Purchases] CHECK CONSTRAINT [FK_Purchases_Users]
GO
/****** Object:  StoredProcedure [dbo].[SP_INSERT_PURCHASES]    Script Date: 4/29/2021 11:03:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_INSERT_PURCHASES]
	@Amount decimal(18,2),
	@Currency varchar(3),
	@Rate decimal(18, 2),
	@UserId int,

	@Error_Code varchar(50) OUT,
	@Amount_Result DECIMAL(18, 2) OUT
AS
BEGIN
	DECLARE @IsAvailable BIT
	DECLARE @Is_Valid_Currency BIT

	EXEC SP_VAL_USER_PURCHASE_AMOUNT 
		@UserId = @UserId,
		@Amount = @Amount, 
		@Currency = @Currency, 
		@IsAvailable = @IsAvailable OUTPUT

	EXEC SP_VALIDATE_CURRENCY
		@Currency = @Currency,
		@Is_Valid_Currency = @Is_Valid_Currency OUTPUT

	IF(@Is_Valid_Currency = 1)
		BEGIN
		IF(@IsAvailable = 1)
			BEGIN
				SET @Amount_Result = @Amount * @Rate
				INSERT INTO Purchases (Amount, Currency, Rate, UserId, AmountResult) VALUES (@Amount, @Currency, @Rate, @UserId, @Amount_Result)
				SET @Error_Code = 'COD00'
			END
			ELSE
				SET @Error_Code = 'COD01'
			END
	ELSE
		SET @Error_Code = 'COD02'
		
END
GO
/****** Object:  StoredProcedure [dbo].[SP_VAL_USER_PURCHASE_AMOUNT]    Script Date: 4/29/2021 11:03:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_VAL_USER_PURCHASE_AMOUNT]
	@UserId int,
	@Amount decimal(18,2),
	@Currency varchar(3),
	@IsAvailable bit Output
AS
BEGIN
	DECLARE @TotalAmount decimal(18, 2) = 0
	DECLARE @LimitAmount decimal(19, 2) = 0

	select @TotalAmount = SUM(AmountResult) + @Amount 
	from Purchases 
	where 
	UserId = @UserId
	and DATEDIFF(MONTH, Date, GETDATE()) < 1 
	and Currency = @Currency

	select @LimitAmount = AmountLimit from PurchaseLimit where Currency = @Currency
	
	IF(@TotalAmount > @LimitAmount)
		SET @IsAvailable = 0
	ELSE
		SET @IsAvailable = 1

	SET NOCOUNT ON;
END
GO
/****** Object:  StoredProcedure [dbo].[SP_VALIDATE_CURRENCY]    Script Date: 4/29/2021 11:03:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_VALIDATE_CURRENCY]
	@Currency varchar(3),

	@Error_Code varchar(50) OUT,
	@Is_Valid_Currency BIT OUT
AS
BEGIN
	IF(EXISTS(SELECT 1 FROM PurchaseLimit Where Currency = @Currency))
	BEGIN
		SET @Error_Code = 'COD02'
		SET @Is_Valid_Currency = 0
	END
	ELSE
		SET @Error_Code = 'COD03'
		SET @Is_Valid_Currency = 1
END
GO
