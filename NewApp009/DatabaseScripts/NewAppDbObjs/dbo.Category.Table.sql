USE [NewAppDb]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 04/16/2015 18:34:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Category](
	[Category] [int] NOT NULL,
	[Category_Description] [varchar](100) NOT NULL,
	[Create_Date] [datetime] NOT NULL,
	[Last_Mod_Date] [datetime] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
