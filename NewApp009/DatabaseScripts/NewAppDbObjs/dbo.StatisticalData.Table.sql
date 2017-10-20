USE [NewAppDb]
GO
/****** Object:  Table [dbo].[StatisticalData]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StatisticalData](
	[No_Of_New_Customers] [bigint] NULL,
	[No_Of_Sales] [bigint] NULL,
	[No_Of_New_Items] [bigint] NULL,
	[No_Of_New_Vendors] [bigint] NULL,
	[Final_Sales_Amt] [numeric](18, 0) NULL,
	[Sales_Balance_Amt] [numeric](18, 0) NULL,
	[Create_Date] [datetime] NULL
) ON [PRIMARY]
GO
