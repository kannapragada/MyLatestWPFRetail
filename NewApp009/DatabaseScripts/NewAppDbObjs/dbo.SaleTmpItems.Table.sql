USE [NewAppDb]
GO
/****** Object:  Table [dbo].[SaleTmpItems]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SaleTmpItems](
	[Sale_Id] [varchar](15) NOT NULL,
	[Serial_Numb] [int] NOT NULL,
	[Item_Id] [varchar](15) NOT NULL,
	[Batch_Id] [varchar](15) NOT NULL,
	[Qty] [int] NULL,
 CONSTRAINT [PK_SaleTmpItems1] PRIMARY KEY CLUSTERED 
(
	[Sale_Id] ASC,
	[Serial_Numb] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
