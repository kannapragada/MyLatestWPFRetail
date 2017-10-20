USE [NewAppDb]
GO
/****** Object:  Table [dbo].[tblImgData]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblImgData](
	[ID] [int] NOT NULL,
	[Name] [varchar](50) NULL,
	[Picture] [image] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
