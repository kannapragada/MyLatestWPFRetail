USE [NewAppDb]
GO
/****** Object:  Table [dbo].[Tax]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Tax](
	[Tax_Code] [varchar](15) NOT NULL,
	[Tax_Name] [varchar](50) NOT NULL,
	[Tax_Description] [varchar](100) NOT NULL,
	[Tax_Kind_Id] [int] NULL,
	[Tax_Type_Id] [int] NULL,
	[Tax_Value] [varchar](50) NULL,
	[Tax_Start_Date] [datetime] NOT NULL,
	[Tax_End_Date] [datetime] NULL,
	[Tax_Create_Date] [datetime] NOT NULL,
	[Tax_Last_Mod_Date] [datetime] NULL,
 CONSTRAINT [PK_Tax] PRIMARY KEY CLUSTERED 
(
	[Tax_Code] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
