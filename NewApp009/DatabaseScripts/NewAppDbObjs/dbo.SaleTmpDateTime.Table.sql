USE [NewAppDb]
GO
/****** Object:  Table [dbo].[SaleTmpDateTime]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SaleTmpDateTime](
	[Sale_Id] [nchar](10) NOT NULL,
	[Sale_StartDateTime] [datetime] NULL,
 CONSTRAINT [PK_SaleDateTime] PRIMARY KEY CLUSTERED 
(
	[Sale_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
