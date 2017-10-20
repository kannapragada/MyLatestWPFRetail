USE [NewAppDb]
GO
/****** Object:  Table [dbo].[Manufacturer]    Script Date: 04/16/2015 18:34:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Manufacturer](
	[Manuf_Id] [varchar](15) NOT NULL,
	[Manuf_Name] [varchar](50) NOT NULL,
	[Manuf_Status] [varchar](10) NOT NULL,
	[Manuf_DateofBirth] [datetime] NOT NULL,
	[Manuf_Gender] [int] NULL,
	[Manuf_PresentAddress] [varchar](100) NULL,
	[Manuf_PresentCity] [varchar](50) NULL,
	[Manuf_PresentPinCode] [varchar](15) NULL,
	[Manuf_PresentPhone] [varchar](15) NULL,
	[Manuf_Mobile] [varchar](15) NULL,
	[Manuf_EMailId] [varchar](50) NULL,
	[Manuf_IsPresentPermAddressSame] [int] NULL,
	[Manuf_PermanentAddress] [varchar](100) NULL,
	[Manuf_PermanentCity] [varchar](50) NULL,
	[Manuf_PermanentPinCode] [varchar](15) NULL,
	[Manuf_PermanentPhone] [varchar](15) NULL,
	[Manuf_IDProof_Type_Id] [int] NULL,
	[Manuf_IDProof_Value] [varchar](50) NULL,
	[Manuf_Relationship_StartDate] [datetime] NOT NULL,
	[Manuf_Relationship_EndDate] [datetime] NULL,
	[Manuf_Amt_To_be_Paid] [decimal](10, 2) NOT NULL,
	[Manuf_Amt_Paid_YTD] [decimal](10, 2) NOT NULL,
	[Manuf_Create_Date] [datetime] NOT NULL,
	[Manuf_Last_Mod_Date] [datetime] NULL,
	[Manuf_Picture] [image] NULL,
 CONSTRAINT [PK_ManufId] PRIMARY KEY CLUSTERED 
(
	[Manuf_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
