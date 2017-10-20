USE [NewAppDb]
GO
/****** Object:  Table [dbo].[SalesTaxes]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SalesTaxes](
	[Sale_Id] [varchar](15) NOT NULL,
	[Tax_Id] [varchar](15) NOT NULL,
	[Tax_Type] [int] NOT NULL,
	[Item_Id] [varchar](15) NOT NULL,
	[Tax_Value] [decimal](10, 2) NOT NULL,
 CONSTRAINT [PK_SalesTaxes] PRIMARY KEY CLUSTERED 
(
	[Sale_Id] ASC,
	[Tax_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
