USE [NewAppDb]
GO
/****** Object:  Table [dbo].[DiscTypes]    Script Date: 04/16/2015 18:34:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DiscTypes](
	[Disc_Type_Id] [int] NULL,
	[Disc_Type_Name] [varchar](15) NULL,
	[Disc_Type_Description] [varchar](100) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
