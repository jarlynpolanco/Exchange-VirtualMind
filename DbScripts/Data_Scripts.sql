GO
INSERT [dbo].[Errors] ([Id], [Code], [Description]) VALUES (1, N'COD00', N'The purchase was made correctly')
GO
INSERT [dbo].[Errors] ([Id], [Code], [Description]) VALUES (2, N'COD01', N'The user does not have monthly availability in the specified currency')
GO
INSERT [dbo].[Errors] ([Id], [Code], [Description]) VALUES (3, N'COD02', N'The selected currency is not allowed')
GO
INSERT [dbo].[Errors] ([Id], [Code], [Description]) VALUES (4, N'COD03', N'The currency is valid to make a purchase')
GO
SET IDENTITY_INSERT [dbo].[Errors] OFF
GO
SET IDENTITY_INSERT [dbo].[PurchaseLimit] ON 
GO
INSERT [dbo].[PurchaseLimit] ([Id], [Currency], [AmountLimit]) VALUES (1, N'USD', CAST(200.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[PurchaseLimit] ([Id], [Currency], [AmountLimit]) VALUES (2, N'BRL', CAST(300.00 AS Decimal(18, 2)))
GO
SET IDENTITY_INSERT [dbo].[PurchaseLimit] OFF
GO
SET IDENTITY_INSERT [dbo].[Purchases] ON 
GO
INSERT [dbo].[Purchases] ([Id], [Amount], [Currency], [Rate], [UserId], [AmountResult], [Date]) VALUES (1, CAST(41.00 AS Decimal(18, 2)), N'USD', CAST(92.50 AS Decimal(18, 2)), 1, CAST(3792.50 AS Decimal(18, 2)), CAST(N'2021-04-29T09:39:09.520' AS DateTime))
GO
INSERT [dbo].[Purchases] ([Id], [Amount], [Currency], [Rate], [UserId], [AmountResult], [Date]) VALUES (2, CAST(41.00 AS Decimal(18, 2)), N'USD', CAST(92.50 AS Decimal(18, 2)), 1, CAST(3792.50 AS Decimal(18, 2)), CAST(N'2021-04-29T09:41:08.590' AS DateTime))
GO
INSERT [dbo].[Purchases] ([Id], [Amount], [Currency], [Rate], [UserId], [AmountResult], [Date]) VALUES (3, CAST(10.00 AS Decimal(18, 2)), N'USD', CAST(92.50 AS Decimal(18, 2)), 1, CAST(925.00 AS Decimal(18, 2)), CAST(N'2021-04-29T09:42:22.583' AS DateTime))
GO
INSERT [dbo].[Purchases] ([Id], [Amount], [Currency], [Rate], [UserId], [AmountResult], [Date]) VALUES (4, CAST(100.00 AS Decimal(18, 2)), N'USD', CAST(92.50 AS Decimal(18, 2)), 1, CAST(9250.00 AS Decimal(18, 2)), CAST(N'2021-04-29T09:44:54.880' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Purchases] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 
GO
INSERT [dbo].[Users] ([Id], [UserName], [FirstName], [LastName], [Email], [CreatedDate]) VALUES (1, N'JPolanco', N'Jarlyn', N'Polanco', N'jarlynpolanco@gmail.com', CAST(N'2021-04-28T20:30:36.887' AS DateTime))
GO
INSERT [dbo].[Users] ([Id], [UserName], [FirstName], [LastName], [Email], [CreatedDate]) VALUES (2, N'DBerroa', N'David', N'Berroa', N'davidberroa@gmail.com', CAST(N'2021-04-28T20:30:36.890' AS DateTime))
GO
INSERT [dbo].[Users] ([Id], [UserName], [FirstName], [LastName], [Email], [CreatedDate]) VALUES (3, N'YPolanco', N'Yumarx', N'Polanco', N'yumarxpolanco@outlook.com', CAST(N'2021-04-28T20:30:36.890' AS DateTime))
GO
INSERT [dbo].[Users] ([Id], [UserName], [FirstName], [LastName], [Email], [CreatedDate]) VALUES (4, N'FMatos', N'Federico', N'Matos', N'fedematos@outlook.com', CAST(N'2021-04-28T20:30:36.890' AS DateTime))
GO
INSERT [dbo].[Users] ([Id], [UserName], [FirstName], [LastName], [Email], [CreatedDate]) VALUES (5, N'GMontero', N'Genesis', N'Montero', N'genemontc@outlook.com', CAST(N'2021-04-28T20:30:36.890' AS DateTime))
GO