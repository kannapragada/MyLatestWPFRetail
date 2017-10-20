USE [NewAppDb]
GO
/****** Object:  Table [dbo].[SalesDetails]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SalesDetails](
	[SD_Sales_Id] [varchar](15) NOT NULL,
	[SD_Serial_Numb] [int] NOT NULL,
	[SD_Item_Id] [varchar](15) NOT NULL,
	[SD_Batch_Id] [varchar](15) NOT NULL,
	[SD_Qty_Sold] [bigint] NOT NULL,
	[SD_Weight] [decimal](18, 0) NULL,
	[SD_Item_Amt_Per_Unit] [decimal](10, 2) NOT NULL,
	[SD_Total_Item_Amt] [decimal](10, 2) NOT NULL,
	[SD_Disc_Amt_Per_Unit] [decimal](10, 2) NULL,
	[SD_Disc_Item_Amt] [decimal](10, 2) NULL,
	[SD_Tax_Amt_Per_Unit] [decimal](10, 2) NULL,
	[SD_Tax_Item_Amt] [decimal](10, 2) NULL,
	[SD_Final_Item_Amt] [decimal](10, 2) NOT NULL,
	[SD_Create_Date] [datetime] NOT NULL,
	[SD_Last_Mod_Date] [datetime] NULL,
 CONSTRAINT [PK_SalesDetails_1] PRIMARY KEY CLUSTERED 
(
	[SD_Sales_Id] ASC,
	[SD_Serial_Numb] ASC,
	[SD_Item_Id] ASC,
	[SD_Batch_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
