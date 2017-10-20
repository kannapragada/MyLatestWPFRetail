USE [NewAppDb]
GO
/****** Object:  Table [dbo].[Items]    Script Date: 04/16/2015 18:34:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Items](
	[Item_Id] [varchar](15) NOT NULL,
	[Item_Name] [varchar](30) NOT NULL,
	[Item_Description] [varchar](100) NULL,
	[Item_Category] [varchar](50) NULL,
	[Item_Create_Date] [datetime] NOT NULL,
	[Item_Last_Mod_Date] [datetime] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
