USE [NewAppDb]
GO
/****** Object:  Table [dbo].[Discounts]    Script Date: 04/16/2015 18:34:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Discounts](
	[Disc_Code] [varchar](15) NOT NULL,
	[Disc_Name] [varchar](50) NOT NULL,
	[Disc_Description] [varchar](100) NOT NULL,
	[Disc_Kind_Id] [int] NULL,
	[Disc_Type_Id] [int] NULL,
	[Disc_Value] [decimal](10, 2) NOT NULL,
	[Disc_Start_Date] [datetime] NOT NULL,
	[Disc_End_Date] [datetime] NOT NULL,
	[Disc_Create_Date] [datetime] NOT NULL,
	[Disc_Last_Mod_Date] [datetime] NULL,
	[Disc_Remarks] [varchar](100) NULL,
 CONSTRAINT [PK_Discounts] PRIMARY KEY CLUSTERED 
(
	[Disc_Code] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
