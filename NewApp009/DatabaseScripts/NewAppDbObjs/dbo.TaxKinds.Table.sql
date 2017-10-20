USE [NewAppDb]
GO
/****** Object:  Table [dbo].[TaxKinds]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TaxKinds](
	[Tax_Kind_Id] [int] NULL,
	[Tax_Kind_Name] [varchar](15) NULL,
	[Tax_Kind_Description] [varchar](100) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
