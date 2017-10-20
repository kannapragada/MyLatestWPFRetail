USE [NewAppDb]
GO
/****** Object:  Table [dbo].[GenderTypes]    Script Date: 04/16/2015 18:34:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GenderTypes](
	[Gender_Type_Id] [int] NULL,
	[Gender_Type_Value] [varchar](15) NULL,
	[Gender_Type_Description] [varchar](100) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
