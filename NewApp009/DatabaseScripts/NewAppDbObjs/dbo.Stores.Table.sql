USE [NewAppDb]
GO
/****** Object:  Table [dbo].[Stores]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Stores](
	[Store_Id] [int] NOT NULL,
	[Store_Name] [varchar](50) NOT NULL,
	[Store_Address] [varchar](100) NOT NULL,
	[Store_City] [varchar](15) NOT NULL,
	[Store_State] [varchar](15) NULL,
	[Store_Country] [varchar](15) NULL,
	[Store_Reg_Number] [varchar](15) NOT NULL,
	[Store_RegDate_Start] [datetime] NOT NULL,
	[Store_RegDate_Exp] [datetime] NULL,
	[Store_Reg_Owner] [varchar](50) NOT NULL,
	[Store_Create_Date] [datetime] NOT NULL,
	[Store_Last_Mod_Date] [datetime] NULL,
 CONSTRAINT [PK_Stores] PRIMARY KEY CLUSTERED 
(
	[Store_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
