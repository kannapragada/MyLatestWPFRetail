USE [NewAppDb]
GO
/****** Object:  Table [dbo].[TaxItemDtls]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TaxItemDtls](
	[TI_Tax_Code] [varchar](15) NOT NULL,
	[TI_Item_Id] [varchar](15) NOT NULL,
	[TI_Batch_Id] [varchar](15) NULL,
	[TI_Start_Date] [datetime] NOT NULL,
	[TI_End_Date] [datetime] NULL,
	[TI_Remarks] [varchar](100) NOT NULL,
	[TI_Create_Date] [datetime] NOT NULL,
	[TI_Last_Mod_Date] [datetime] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
