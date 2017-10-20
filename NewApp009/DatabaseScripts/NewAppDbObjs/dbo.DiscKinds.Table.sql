USE [NewAppDb]
GO
/****** Object:  Table [dbo].[DiscKinds]    Script Date: 04/16/2015 18:34:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DiscKinds](
	[Disc_Kind_Id] [int] NULL,
	[Disc_Kind_Name] [varchar](15) NULL,
	[Disc_Kind_Description] [varchar](100) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
