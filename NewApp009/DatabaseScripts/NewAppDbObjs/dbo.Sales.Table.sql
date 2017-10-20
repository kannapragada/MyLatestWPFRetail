USE [NewAppDb]
GO
/****** Object:  Table [dbo].[Sales]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Sales](
	[Sales_Id] [varchar](15) NOT NULL,
	[Sales_Status_Id] [int] NOT NULL,
	[Sales_Cust_Id] [varchar](15) NOT NULL,
	[Sales_Total_Sales_Amt] [decimal](10, 2) NOT NULL,
	[Sales_Total_Disc_Amt] [decimal](10, 2) NOT NULL,
	[Sales_Total_Tax_Amt] [decimal](10, 2) NOT NULL,
	[Sales_Final_Sales_Amt] [decimal](10, 2) NOT NULL,
	[Sales_Amt_Paid] [decimal](10, 2) NOT NULL,
	[Sales_Balance_Amt] [decimal](10, 2) NOT NULL,
	[Sales_Create_Date] [datetime] NOT NULL,
	[Sales_Last_Mod_Date] [datetime] NULL,
	[Sales_User_Id] [varchar](15) NULL,
	[Sales_Guest_Name] [varchar](50) NULL,
 CONSTRAINT [PK_Sales] PRIMARY KEY CLUSTERED 
(
	[Sales_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
