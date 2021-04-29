GO
INSERT [dbo].[PurchaseLimit] ([Currency], [AmountLimit]) VALUES (N'USD', CAST(200.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[PurchaseLimit] ([Currency], [AmountLimit]) VALUES (N'BRL', CAST(300.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Purchases] ([Amount], [Currency], [Rate], [UserId], [AmountResult], [Date]) VALUES (CAST(41.00 AS Decimal(18, 2)), N'USD', CAST(92.50 AS Decimal(18, 2)), 1, CAST(3792.50 AS Decimal(18, 2)), CAST(N'2021-04-29T09:39:09.520' AS DateTime))
GO
INSERT [dbo].[Purchases] ([Amount], [Currency], [Rate], [UserId], [AmountResult], [Date]) VALUES (CAST(41.00 AS Decimal(18, 2)), N'USD', CAST(92.50 AS Decimal(18, 2)), 1, CAST(3792.50 AS Decimal(18, 2)), CAST(N'2021-04-29T09:41:08.590' AS DateTime))
GO
INSERT [dbo].[Purchases] ([Amount], [Currency], [Rate], [UserId], [AmountResult], [Date]) VALUES (CAST(10.00 AS Decimal(18, 2)), N'USD', CAST(92.50 AS Decimal(18, 2)), 1, CAST(925.00 AS Decimal(18, 2)), CAST(N'2021-04-29T09:42:22.583' AS DateTime))
GO
INSERT [dbo].[Purchases] ([Amount], [Currency], [Rate], [UserId], [AmountResult], [Date]) VALUES (CAST(100.00 AS Decimal(18, 2)), N'USD', CAST(92.50 AS Decimal(18, 2)), 1, CAST(9250.00 AS Decimal(18, 2)), CAST(N'2021-04-29T09:44:54.880' AS DateTime))
GO
INSERT [dbo].[Users] ([UserName], [FirstName], [LastName], [Email], [CreatedDate]) VALUES (N'JPolanco', N'Jarlyn', N'Polanco', N'jarlynpolanco@gmail.com', CAST(N'2021-04-28T20:30:36.887' AS DateTime))
GO
INSERT [dbo].[Users] ([UserName], [FirstName], [LastName], [Email], [CreatedDate]) VALUES (N'DBerroa', N'David', N'Berroa', N'davidberroa@gmail.com', CAST(N'2021-04-28T20:30:36.890' AS DateTime))
GO
INSERT [dbo].[Users] ([UserName], [FirstName], [LastName], [Email], [CreatedDate]) VALUES (N'YPolanco', N'Yumarx', N'Polanco', N'yumarxpolanco@outlook.com', CAST(N'2021-04-28T20:30:36.890' AS DateTime))
GO
INSERT [dbo].[Users] ([UserName], [FirstName], [LastName], [Email], [CreatedDate]) VALUES (N'FMatos', N'Federico', N'Matos', N'fedematos@outlook.com', CAST(N'2021-04-28T20:30:36.890' AS DateTime))
GO
INSERT [dbo].[Users] ([UserName], [FirstName], [LastName], [Email], [CreatedDate]) VALUES (N'GMontero', N'Genesis', N'Montero', N'genemontc@outlook.com', CAST(N'2021-04-28T20:30:36.890' AS DateTime))
GO