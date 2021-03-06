USE [MRP]
GO
/****** Object:  Table [dbo].[Process]    Script Date: 2021-07-01 오후 2:02:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Process](
	[PrcIdx] [int] IDENTITY(1,1) NOT NULL,
	[SchIdx] [bigint] NOT NULL,
	[PrcCode] [char](14) NOT NULL,
	[PrcDate] [date] NOT NULL,
	[PrcLoadTime] [int] NULL,
	[PrcStartTime] [time](7) NULL,
	[PrcEndTime] [time](7) NULL,
	[PrcFacilityID] [char](8) NULL,
	[PrcResult] [bit] NOT NULL,
	[RegDate] [nchar](10) NULL,
	[RegID] [varchar](20) NULL,
	[ModDate] [datetime] NULL,
	[ModID] [varchar](20) NULL,
 CONSTRAINT [PK_Process] PRIMARY KEY CLUSTERED 
(
	[PrcIdx] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Process] ON 

INSERT [dbo].[Process] ([PrcIdx], [SchIdx], [PrcCode], [PrcDate], [PrcLoadTime], [PrcStartTime], [PrcEndTime], [PrcFacilityID], [PrcResult], [RegDate], [RegID], [ModDate], [ModID]) VALUES (1, 6, N'PRC20210630001', CAST(N'2021-06-30' AS Date), 5, CAST(N'09:00:00' AS Time), CAST(N'17:00:00' AS Time), N'FAC10002', 1, N'2021-06-30', N'MRP', NULL, NULL)
INSERT [dbo].[Process] ([PrcIdx], [SchIdx], [PrcCode], [PrcDate], [PrcLoadTime], [PrcStartTime], [PrcEndTime], [PrcFacilityID], [PrcResult], [RegDate], [RegID], [ModDate], [ModID]) VALUES (2, 6, N'PRC20210630002', CAST(N'2021-06-30' AS Date), 5, CAST(N'09:00:00' AS Time), CAST(N'17:00:00' AS Time), N'FAC10002', 1, N'2021-06-30', N'MRP', NULL, NULL)
INSERT [dbo].[Process] ([PrcIdx], [SchIdx], [PrcCode], [PrcDate], [PrcLoadTime], [PrcStartTime], [PrcEndTime], [PrcFacilityID], [PrcResult], [RegDate], [RegID], [ModDate], [ModID]) VALUES (3, 6, N'PRC20210630003', CAST(N'2021-06-30' AS Date), 5, CAST(N'09:00:00' AS Time), CAST(N'17:00:00' AS Time), N'FAC10002', 1, N'2021-06-30', N'MRP', NULL, NULL)
INSERT [dbo].[Process] ([PrcIdx], [SchIdx], [PrcCode], [PrcDate], [PrcLoadTime], [PrcStartTime], [PrcEndTime], [PrcFacilityID], [PrcResult], [RegDate], [RegID], [ModDate], [ModID]) VALUES (4, 6, N'PRC20210630004', CAST(N'2021-06-30' AS Date), 5, CAST(N'09:00:00' AS Time), CAST(N'17:00:00' AS Time), N'FAC10002', 1, N'2021-06-30', N'MRP', NULL, NULL)
INSERT [dbo].[Process] ([PrcIdx], [SchIdx], [PrcCode], [PrcDate], [PrcLoadTime], [PrcStartTime], [PrcEndTime], [PrcFacilityID], [PrcResult], [RegDate], [RegID], [ModDate], [ModID]) VALUES (5, 6, N'PRC20210630005', CAST(N'2021-06-30' AS Date), 5, CAST(N'09:00:00' AS Time), CAST(N'17:00:00' AS Time), N'FAC10002', 1, N'2021-06-30', N'MRP', NULL, NULL)
INSERT [dbo].[Process] ([PrcIdx], [SchIdx], [PrcCode], [PrcDate], [PrcLoadTime], [PrcStartTime], [PrcEndTime], [PrcFacilityID], [PrcResult], [RegDate], [RegID], [ModDate], [ModID]) VALUES (6, 6, N'PRC20210630006', CAST(N'2021-06-30' AS Date), 5, CAST(N'09:00:00' AS Time), CAST(N'17:00:00' AS Time), N'FAC10002', 1, N'2021-06-30', N'MRP', NULL, NULL)
INSERT [dbo].[Process] ([PrcIdx], [SchIdx], [PrcCode], [PrcDate], [PrcLoadTime], [PrcStartTime], [PrcEndTime], [PrcFacilityID], [PrcResult], [RegDate], [RegID], [ModDate], [ModID]) VALUES (7, 6, N'PRC20210630007', CAST(N'2021-06-30' AS Date), 5, CAST(N'09:00:00' AS Time), CAST(N'17:00:00' AS Time), N'FAC10002', 1, N'2021-06-30', N'MRP', NULL, NULL)
INSERT [dbo].[Process] ([PrcIdx], [SchIdx], [PrcCode], [PrcDate], [PrcLoadTime], [PrcStartTime], [PrcEndTime], [PrcFacilityID], [PrcResult], [RegDate], [RegID], [ModDate], [ModID]) VALUES (8, 6, N'PRC20210630008', CAST(N'2021-06-30' AS Date), 5, CAST(N'09:00:00' AS Time), CAST(N'17:00:00' AS Time), N'FAC10002', 1, N'2021-06-30', N'MRP', NULL, NULL)
INSERT [dbo].[Process] ([PrcIdx], [SchIdx], [PrcCode], [PrcDate], [PrcLoadTime], [PrcStartTime], [PrcEndTime], [PrcFacilityID], [PrcResult], [RegDate], [RegID], [ModDate], [ModID]) VALUES (9, 6, N'PRC20210630009', CAST(N'2021-06-30' AS Date), 5, CAST(N'09:00:00' AS Time), CAST(N'17:00:00' AS Time), N'FAC10002', 0, N'2021-06-30', N'MRP', NULL, NULL)
INSERT [dbo].[Process] ([PrcIdx], [SchIdx], [PrcCode], [PrcDate], [PrcLoadTime], [PrcStartTime], [PrcEndTime], [PrcFacilityID], [PrcResult], [RegDate], [RegID], [ModDate], [ModID]) VALUES (10, 6, N'PRC20210630010', CAST(N'2021-06-30' AS Date), 5, CAST(N'09:00:00' AS Time), CAST(N'17:00:00' AS Time), N'FAC10002', 0, N'2021-06-30', N'MRP', NULL, NULL)
INSERT [dbo].[Process] ([PrcIdx], [SchIdx], [PrcCode], [PrcDate], [PrcLoadTime], [PrcStartTime], [PrcEndTime], [PrcFacilityID], [PrcResult], [RegDate], [RegID], [ModDate], [ModID]) VALUES (11, 6, N'PRC20210630011', CAST(N'2021-06-30' AS Date), 5, CAST(N'09:00:00' AS Time), CAST(N'17:00:00' AS Time), N'FAC10002', 0, N'2021-06-30', N'MRP', NULL, NULL)
INSERT [dbo].[Process] ([PrcIdx], [SchIdx], [PrcCode], [PrcDate], [PrcLoadTime], [PrcStartTime], [PrcEndTime], [PrcFacilityID], [PrcResult], [RegDate], [RegID], [ModDate], [ModID]) VALUES (12, 6, N'PRC20210630012', CAST(N'2021-06-30' AS Date), 5, CAST(N'09:00:00' AS Time), CAST(N'17:00:00' AS Time), N'FAC10002', 0, N'2021-06-30', N'MRP', NULL, NULL)
INSERT [dbo].[Process] ([PrcIdx], [SchIdx], [PrcCode], [PrcDate], [PrcLoadTime], [PrcStartTime], [PrcEndTime], [PrcFacilityID], [PrcResult], [RegDate], [RegID], [ModDate], [ModID]) VALUES (13, 6, N'PRC20210630013', CAST(N'2021-06-30' AS Date), 5, CAST(N'09:00:00' AS Time), CAST(N'17:00:00' AS Time), N'FAC10002', 0, N'2021-06-30', N'MRP', NULL, NULL)
INSERT [dbo].[Process] ([PrcIdx], [SchIdx], [PrcCode], [PrcDate], [PrcLoadTime], [PrcStartTime], [PrcEndTime], [PrcFacilityID], [PrcResult], [RegDate], [RegID], [ModDate], [ModID]) VALUES (14, 6, N'PRC20210630014', CAST(N'2021-06-30' AS Date), 5, CAST(N'09:00:00' AS Time), CAST(N'17:00:00' AS Time), N'FAC10002', 0, N'2021-06-30', N'MRP', NULL, NULL)
INSERT [dbo].[Process] ([PrcIdx], [SchIdx], [PrcCode], [PrcDate], [PrcLoadTime], [PrcStartTime], [PrcEndTime], [PrcFacilityID], [PrcResult], [RegDate], [RegID], [ModDate], [ModID]) VALUES (15, 6, N'PRC20210630015', CAST(N'2021-06-30' AS Date), 5, CAST(N'09:00:00' AS Time), CAST(N'17:00:00' AS Time), N'FAC10002', 0, N'2021-06-30', N'MRP', NULL, NULL)
INSERT [dbo].[Process] ([PrcIdx], [SchIdx], [PrcCode], [PrcDate], [PrcLoadTime], [PrcStartTime], [PrcEndTime], [PrcFacilityID], [PrcResult], [RegDate], [RegID], [ModDate], [ModID]) VALUES (16, 6, N'PRC20210630016', CAST(N'2021-06-30' AS Date), 5, CAST(N'09:00:00' AS Time), CAST(N'17:00:00' AS Time), N'FAC10002', 0, N'2021-06-30', N'MRP', NULL, NULL)
INSERT [dbo].[Process] ([PrcIdx], [SchIdx], [PrcCode], [PrcDate], [PrcLoadTime], [PrcStartTime], [PrcEndTime], [PrcFacilityID], [PrcResult], [RegDate], [RegID], [ModDate], [ModID]) VALUES (17, 6, N'PRC20210630017', CAST(N'2021-06-30' AS Date), 5, CAST(N'09:00:00' AS Time), CAST(N'17:00:00' AS Time), N'FAC10002', 0, N'2021-06-30', N'MRP', NULL, NULL)
INSERT [dbo].[Process] ([PrcIdx], [SchIdx], [PrcCode], [PrcDate], [PrcLoadTime], [PrcStartTime], [PrcEndTime], [PrcFacilityID], [PrcResult], [RegDate], [RegID], [ModDate], [ModID]) VALUES (18, 7, N'PRC20210701001', CAST(N'2021-07-01' AS Date), 5, CAST(N'09:00:00' AS Time), CAST(N'17:00:00' AS Time), N'FAC10002', 0, N'2021-07-01', N'MRP', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Process] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UK_Process_PrcCode]    Script Date: 2021-07-01 오후 2:02:12 ******/
ALTER TABLE [dbo].[Process] ADD  CONSTRAINT [UK_Process_PrcCode] UNIQUE NONCLUSTERED 
(
	[PrcCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Process] ADD  CONSTRAINT [DF_Process_PrcResult]  DEFAULT ((0)) FOR [PrcResult]
GO
ALTER TABLE [dbo].[Process] ADD  CONSTRAINT [DF_Process_RegDate]  DEFAULT (getdate()) FOR [RegDate]
GO
ALTER TABLE [dbo].[Process]  WITH NOCHECK ADD  CONSTRAINT [FK_Process_Schedules] FOREIGN KEY([SchIdx])
REFERENCES [dbo].[Schedules] ([SchIdx])
GO
ALTER TABLE [dbo].[Process] NOCHECK CONSTRAINT [FK_Process_Schedules]
GO
ALTER TABLE [dbo].[Process]  WITH CHECK ADD  CONSTRAINT [FK_Process_Settings] FOREIGN KEY([PrcFacilityID])
REFERENCES [dbo].[Settings] ([BasicCode])
GO
ALTER TABLE [dbo].[Process] CHECK CONSTRAINT [FK_Process_Settings]
GO
