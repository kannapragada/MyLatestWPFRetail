USE [NewAppDb]
GO
/****** Object:  Table [dbo].[NextIds]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[NextIds](
	[CustPrefixStr] [varchar](10) NULL,
	[ItemPrefixStr] [varchar](10) NULL,
	[SalePrefixStr] [varchar](10) NULL,
	[DiscPrefixStr] [varchar](10) NULL,
	[TaxPrefixStr] [varchar](10) NULL,
	[BillPrefixStr] [varchar](10) NULL,
	[ManufPrefixStr] [varchar](10) NULL,
	[VendorPrefixStr] [varchar](10) NULL,
	[UserPrefixStr] [varchar](10) NULL,
	[CurrCustId] [numeric](10, 0) NULL,
	[CurrItemId] [numeric](10, 0) NULL,
	[CurrSaleId] [numeric](10, 0) NULL,
	[CurrDiscCode] [numeric](10, 0) NULL,
	[CurrTaxId] [numeric](10, 0) NULL,
	[CurrBillNumb] [numeric](10, 0) NULL,
	[CurrManufId] [numeric](10, 0) NULL,
	[CurrVendorId] [numeric](10, 0) NULL,
	[CurrUserId] [numeric](10, 0) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
