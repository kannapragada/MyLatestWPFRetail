USE [NewAppDb]
GO
/****** Object:  Table [dbo].[VendorStatus]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[VendorStatus](
	[Vendor_Status_Id] [int] NULL,
	[Vendor_Status_Value] [varchar](15) NULL,
	[Vendor_Status_Description] [varchar](100) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
