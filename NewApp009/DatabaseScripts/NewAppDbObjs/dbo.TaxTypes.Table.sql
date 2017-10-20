USE [NewAppDb]
GO
/****** Object:  Table [dbo].[TaxTypes]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TaxTypes](
	[Tax_Type_Id] [int] NULL,
	[Tax_Type_Name] [varchar](15) NULL,
	[Tax_Type_Description] [varchar](100) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
