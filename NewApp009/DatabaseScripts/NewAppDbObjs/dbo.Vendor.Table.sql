USE [NewAppDb]
GO
/****** Object:  Table [dbo].[Vendor]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Vendor](
	[Vendor_Id] [varchar](15) NOT NULL,
	[Vendor_Name] [varchar](50) NOT NULL,
	[Vendor_Status] [varchar](10) NOT NULL,
	[Vendor_DateofBirth] [datetime] NOT NULL,
	[Vendor_Gender] [int] NULL,
	[Vendor_PresentAddress] [varchar](100) NULL,
	[Vendor_PresentCity] [varchar](50) NULL,
	[Vendor_PresentPinCode] [varchar](15) NULL,
	[Vendor_PresentPhone] [varchar](15) NULL,
	[Vendor_Mobile] [varchar](15) NULL,
	[Vendor_EMailId] [varchar](50) NULL,
	[Vendor_IsPresentPermAddressSame] [int] NULL,
	[Vendor_PermanentAddress] [varchar](100) NULL,
	[Vendor_PermanentCity] [varchar](50) NULL,
	[Vendor_PermanentPinCode] [varchar](15) NULL,
	[Vendor_PermanentPhone] [varchar](15) NULL,
	[Vendor_IDProof_Type_Id] [int] NULL,
	[Vendor_IDProof_Value] [varchar](50) NULL,
	[Vendor_Relationship_StartDate] [datetime] NOT NULL,
	[Vendor_Relationship_EndDate] [datetime] NULL,
	[Vendor_Amt_To_be_Paid] [decimal](10, 2) NOT NULL,
	[Vendor_Amt_Paid_YTD] [decimal](10, 2) NOT NULL,
	[Vendor_Create_Date] [datetime] NOT NULL,
	[Vendor_Last_Mod_Date] [datetime] NULL,
	[Vendor_Picture] [image] NULL,
 CONSTRAINT [PK_Vendor] PRIMARY KEY CLUSTERED 
(
	[Vendor_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
