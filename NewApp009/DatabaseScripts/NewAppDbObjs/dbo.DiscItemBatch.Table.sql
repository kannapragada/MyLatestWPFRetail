USE [NewAppDb]
GO
/****** Object:  Table [dbo].[DiscItemBatch]    Script Date: 04/16/2015 18:34:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DiscItemBatch](
	[DiscI_Item_Id] [varchar](15) NOT NULL,
	[DiscI_Disc_Code] [varchar](15) NOT NULL,
	[DiscI_Item_Batch_Id] [varchar](15) NOT NULL,
	[DiscI_Start_Date] [datetime] NOT NULL,
	[DiscI_End_Date] [datetime] NOT NULL,
	[DiscI_Remarks] [varchar](255) NULL,
	[DiscI_Create_Date] [datetime] NOT NULL,
	[DiscI_Last_Mod_Date] [datetime] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
