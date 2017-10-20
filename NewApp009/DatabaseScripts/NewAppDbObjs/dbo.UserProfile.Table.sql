USE [NewAppDb]
GO
/****** Object:  Table [dbo].[UserProfile]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserProfile](
	[User_Id] [varchar](15) NOT NULL,
	[User_Name] [varchar](50) NOT NULL,
	[User_Status] [varchar](10) NOT NULL,
	[User_DateofBirth] [datetime] NOT NULL,
	[User_Gender] [int] NULL,
	[User_PresentAddress] [varchar](100) NULL,
	[User_PresentCity] [varchar](50) NULL,
	[User_PresentPinCode] [varchar](15) NULL,
	[User_PresentPhone] [varchar](15) NULL,
	[User_Mobile] [varchar](15) NULL,
	[User_EMailId] [varchar](50) NULL,
	[User_IsPresentPermAddressSame] [int] NULL,
	[User_PermanentAddress] [varchar](100) NULL,
	[User_PermanentCity] [varchar](50) NULL,
	[User_PermanentPinCode] [varchar](15) NULL,
	[User_PermanentPhone] [varchar](15) NULL,
	[User_IDProof_Type_Id] [int] NULL,
	[User_IDProof_Value] [varchar](50) NULL,
	[User_Relationship_StartDate] [datetime] NOT NULL,
	[User_Relationship_EndDate] [datetime] NULL,
	[User_Amt_To_be_Paid] [decimal](10, 2) NOT NULL,
	[User_Amt_Paid_YTD] [decimal](10, 2) NOT NULL,
	[User_Create_Date] [datetime] NOT NULL,
	[User_Last_Mod_Date] [datetime] NULL,
	[User_Picture] [image] NULL,
	[User_ThemeId] [int] NULL,
	[User_Pwd] [varchar](15) NULL,
	[User_SecretQueryId] [int] NULL,
	[User_SecretAnswer] [varchar](50) NULL,
 CONSTRAINT [PK_UserId] PRIMARY KEY CLUSTERED 
(
	[User_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
