USE [NewAppDb]
GO
/****** Object:  Table [dbo].[UserStatus]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserStatus](
	[User_Status_Id] [int] NULL,
	[User_Status_Value] [varchar](15) NULL,
	[User_Status_Description] [varchar](100) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
