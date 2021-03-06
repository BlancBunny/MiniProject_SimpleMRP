USE [MRP]
GO
/****** Object:  Table [dbo].[Schedules]    Script Date: 2021-07-01 오후 2:02:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Schedules](
	[SchIdx] [bigint] IDENTITY(1,1) NOT NULL,
	[PlantCode] [char](8) NOT NULL,
	[SchDate] [datetime] NOT NULL,
	[PrcLoadTime] [int] NULL,
	[SchStartTime] [time](7) NULL,
	[SchEndTime] [time](7) NULL,
	[SchFacilityID] [char](8) NULL,
	[SchAmount] [int] NOT NULL,
	[RegDate] [datetime] NULL,
	[RegID] [varchar](20) NULL,
	[ModDate] [datetime] NULL,
	[ModID] [varchar](20) NULL,
 CONSTRAINT [PK_Schedules] PRIMARY KEY CLUSTERED 
(
	[SchIdx] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Schedules] ON 

INSERT [dbo].[Schedules] ([SchIdx], [PlantCode], [SchDate], [PrcLoadTime], [SchStartTime], [SchEndTime], [SchFacilityID], [SchAmount], [RegDate], [RegID], [ModDate], [ModID]) VALUES (1, N'PC010002', CAST(N'2021-06-25T00:00:00.000' AS DateTime), 10, CAST(N'00:00:00' AS Time), CAST(N'17:00:00' AS Time), N'FAC10002', 20, CAST(N'2021-06-24T00:00:00.000' AS DateTime), N'SYS', CAST(N'2021-06-25T11:52:15.420' AS DateTime), N'MRP')
INSERT [dbo].[Schedules] ([SchIdx], [PlantCode], [SchDate], [PrcLoadTime], [SchStartTime], [SchEndTime], [SchFacilityID], [SchAmount], [RegDate], [RegID], [ModDate], [ModID]) VALUES (2, N'PC010001', CAST(N'2021-06-26T00:00:00.000' AS DateTime), 10, CAST(N'09:00:00' AS Time), CAST(N'17:00:00' AS Time), N'FAC10002', 25, CAST(N'2021-06-25T12:34:08.957' AS DateTime), N'MRP', CAST(N'2021-06-25T14:13:17.370' AS DateTime), N'MRP')
INSERT [dbo].[Schedules] ([SchIdx], [PlantCode], [SchDate], [PrcLoadTime], [SchStartTime], [SchEndTime], [SchFacilityID], [SchAmount], [RegDate], [RegID], [ModDate], [ModID]) VALUES (3, N'PC010004', CAST(N'2021-06-28T00:00:00.000' AS DateTime), 10, NULL, NULL, N'FAC10002', 40, CAST(N'2021-06-28T09:19:52.707' AS DateTime), N'MRP', NULL, NULL)
INSERT [dbo].[Schedules] ([SchIdx], [PlantCode], [SchDate], [PrcLoadTime], [SchStartTime], [SchEndTime], [SchFacilityID], [SchAmount], [RegDate], [RegID], [ModDate], [ModID]) VALUES (4, N'PC010002', CAST(N'2021-06-28T00:00:00.000' AS DateTime), 10, CAST(N'09:00:00' AS Time), CAST(N'17:00:00' AS Time), N'FAC10002', 30, CAST(N'2021-06-28T11:39:06.857' AS DateTime), N'MRP', NULL, NULL)
INSERT [dbo].[Schedules] ([SchIdx], [PlantCode], [SchDate], [PrcLoadTime], [SchStartTime], [SchEndTime], [SchFacilityID], [SchAmount], [RegDate], [RegID], [ModDate], [ModID]) VALUES (5, N'PC010002', CAST(N'2021-06-29T00:00:00.000' AS DateTime), 5, CAST(N'09:00:00' AS Time), CAST(N'17:00:00' AS Time), N'FAC10002', 40, CAST(N'2021-06-29T09:15:03.450' AS DateTime), N'MRP', NULL, NULL)
INSERT [dbo].[Schedules] ([SchIdx], [PlantCode], [SchDate], [PrcLoadTime], [SchStartTime], [SchEndTime], [SchFacilityID], [SchAmount], [RegDate], [RegID], [ModDate], [ModID]) VALUES (6, N'PC010002', CAST(N'2021-06-30T00:00:00.000' AS DateTime), 5, CAST(N'09:00:00' AS Time), CAST(N'17:00:00' AS Time), N'FAC10002', 40, CAST(N'2021-06-30T09:23:25.123' AS DateTime), N'MRP', NULL, NULL)
INSERT [dbo].[Schedules] ([SchIdx], [PlantCode], [SchDate], [PrcLoadTime], [SchStartTime], [SchEndTime], [SchFacilityID], [SchAmount], [RegDate], [RegID], [ModDate], [ModID]) VALUES (7, N'PC010002', CAST(N'2021-07-01T00:00:00.000' AS DateTime), 5, CAST(N'09:00:00' AS Time), CAST(N'17:00:00' AS Time), N'FAC10002', 20, CAST(N'2021-07-01T09:19:33.880' AS DateTime), N'MRP', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Schedules] OFF
GO
ALTER TABLE [dbo].[Schedules]  WITH CHECK ADD  CONSTRAINT [FK_Schedules_Settings] FOREIGN KEY([PlantCode])
REFERENCES [dbo].[Settings] ([BasicCode])
GO
ALTER TABLE [dbo].[Schedules] CHECK CONSTRAINT [FK_Schedules_Settings]
GO
ALTER TABLE [dbo].[Schedules]  WITH NOCHECK ADD  CONSTRAINT [FK_Schedules_Settings1] FOREIGN KEY([SchFacilityID])
REFERENCES [dbo].[Settings] ([BasicCode])
GO
ALTER TABLE [dbo].[Schedules] NOCHECK CONSTRAINT [FK_Schedules_Settings1]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'공정계획 순번' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Schedules', @level2type=N'COLUMN',@level2name=N'SchIdx'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'공장코드' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Schedules', @level2type=N'COLUMN',@level2name=N'PlantCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'공정계획일' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Schedules', @level2type=N'COLUMN',@level2name=N'SchDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'로드타임(초)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Schedules', @level2type=N'COLUMN',@level2name=N'PrcLoadTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'시작시간(계획)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Schedules', @level2type=N'COLUMN',@level2name=N'SchStartTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'종료시간(계획)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Schedules', @level2type=N'COLUMN',@level2name=N'SchEndTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'생산설비ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Schedules', @level2type=N'COLUMN',@level2name=N'SchFacilityID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'목표수량(계획)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Schedules', @level2type=N'COLUMN',@level2name=N'SchAmount'
GO
