USE [NewAppDb]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 04/16/2015 18:34:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Customer](
	[Cust_Id] [varchar](15) NOT NULL,
	[Cust_Name] [varchar](50) NOT NULL,
	[Cust_Status] [varchar](10) NOT NULL,
	[Cust_DateofBirth] [datetime] NOT NULL,
	[Cust_Gender] [int] NULL,
	[Cust_PresentAddress] [varchar](100) NULL,
	[Cust_PresentCity] [varchar](50) NULL,
	[Cust_PresentPinCode] [varchar](15) NULL,
	[Cust_PresentPhone] [varchar](15) NULL,
	[Cust_Mobile] [varchar](15) NULL,
	[Cust_EMailId] [varchar](50) NULL,
	[Cust_IsPresentPermAddressSame] [int] NULL,
	[Cust_PermanentAddress] [varchar](100) NULL,
	[Cust_PermanentCity] [varchar](50) NULL,
	[Cust_PermanentPinCode] [varchar](15) NULL,
	[Cust_PermanentPhone] [varchar](15) NULL,
	[Cust_IDProof_Type_Id] [int] NULL,
	[Cust_IDProof_Value] [varchar](50) NULL,
	[Cust_Relationship_StartDate] [datetime] NOT NULL,
	[Cust_Relationship_EndDate] [datetime] NULL,
	[Cust_Amt_To_be_Paid] [decimal](10, 2) NOT NULL,
	[Cust_Amt_Paid_YTD] [decimal](10, 2) NOT NULL,
	[Cust_Create_Date] [datetime] NOT NULL,
	[Cust_Last_Mod_Date] [datetime] NULL,
	[Cust_Picture] [image] NULL,
 CONSTRAINT [PK_CustId] PRIMARY KEY CLUSTERED 
(
	[Cust_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
