USE [MRP]
GO
/****** Object:  Table [dbo].[Settings]    Script Date: 2021-07-01 오후 2:02:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Settings](
	[BasicCode] [char](8) NOT NULL,
	[CodeName] [nvarchar](100) NOT NULL,
	[CodeDesc] [nvarchar](max) NULL,
	[RegDate] [datetime] NULL,
	[RegID] [varchar](20) NULL,
	[ModDate] [datetime] NULL,
	[ModID] [varchar](20) NULL,
 CONSTRAINT [PK_Settings] PRIMARY KEY CLUSTERED 
(
	[BasicCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[Settings] ([BasicCode], [CodeName], [CodeDesc], [RegDate], [RegID], [ModDate], [ModID]) VALUES (N'FAC10002', N'설비2', N'생산설비2', CAST(N'2021-06-25T10:12:00.000' AS DateTime), N'MRP', CAST(N'2021-06-25T10:12:39.787' AS DateTime), N'SYS')
INSERT [dbo].[Settings] ([BasicCode], [CodeName], [CodeDesc], [RegDate], [RegID], [ModDate], [ModID]) VALUES (N'PC010001', N'수원공장1', N'수원공장(코드)', CAST(N'2021-06-24T11:22:10.000' AS DateTime), N'SYS', CAST(N'2021-06-28T10:44:54.973' AS DateTime), N'MRP')
INSERT [dbo].[Settings] ([BasicCode], [CodeName], [CodeDesc], [RegDate], [RegID], [ModDate], [ModID]) VALUES (N'PC010002', N'부산공장1', N'부산공장!', CAST(N'2021-06-24T13:58:20.787' AS DateTime), N'MRP', CAST(N'2021-06-28T10:44:49.857' AS DateTime), N'MRP')
INSERT [dbo].[Settings] ([BasicCode], [CodeName], [CodeDesc], [RegDate], [RegID], [ModDate], [ModID]) VALUES (N'PC010004', N'대전공장1', N'대전공장', CAST(N'2021-06-24T13:57:57.267' AS DateTime), N'MRP', CAST(N'2021-06-28T10:44:59.990' AS DateTime), N'MRP')
GO
ALTER TABLE [dbo].[Settings] ADD  CONSTRAINT [DF_Settings_RegDate]  DEFAULT (getdate()) FOR [RegDate]
GO
ALTER TABLE [dbo].[Settings] ADD  CONSTRAINT [DF_Settings_RegID]  DEFAULT ('SYS') FOR [RegID]
GO
ALTER TABLE [dbo].[Settings] ADD  CONSTRAINT [DF_Settings_ModDate]  DEFAULT (getdate()) FOR [ModDate]
GO
ALTER TABLE [dbo].[Settings] ADD  CONSTRAINT [DF_Settings_ModID]  DEFAULT ('SYS') FOR [ModID]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Settings', @level2type=N'COLUMN',@level2name=N'BasicCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Settings', @level2type=N'COLUMN',@level2name=N'CodeName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Settings', @level2type=N'COLUMN',@level2name=N'CodeDesc'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Settings', @level2type=N'COLUMN',@level2name=N'RegDate'
GO
