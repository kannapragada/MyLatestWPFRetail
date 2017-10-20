USE [NewAppDb]
GO
/****** Object:  Table [dbo].[ItemBatch]    Script Date: 04/16/2015 18:34:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ItemBatch](
	[IB_Item_Id] [varchar](15) NOT NULL,
	[IB_Batch_Id] [varchar](15) NOT NULL,
	[IB_Qty_Procured] [bigint] NOT NULL,
	[IB_Price_Procured] [decimal](10, 2) NOT NULL,
	[IB_MRP] [decimal](10, 2) NOT NULL,
	[IB_Date_Procured] [datetime] NULL,
	[IB_Date_Manuf] [datetime] NOT NULL,
	[IB_Date_Exp] [datetime] NOT NULL,
	[IB_Weight_Procured] [decimal](10, 2) NULL,
	[IB_Weight_Available] [decimal](10, 2) NULL,
	[IB_BarCode] [nvarchar](20) NULL,
	[IB_Disc_Code] [varchar](15) NULL,
	[IB_Price_Sell] [decimal](10, 2) NULL,
	[IB_Qty_Available] [bigint] NULL,
	[IB_Create_Date] [datetime] NOT NULL,
	[IB_Last_Mod_Date] [datetime] NULL,
	[IB_Vendor_Id] [varchar](15) NULL,
	[IB_Manuf_Id] [varchar](15) NULL,
 CONSTRAINT [PK_ItemBatch] PRIMARY KEY CLUSTERED 
(
	[IB_Item_Id] ASC,
	[IB_Batch_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
