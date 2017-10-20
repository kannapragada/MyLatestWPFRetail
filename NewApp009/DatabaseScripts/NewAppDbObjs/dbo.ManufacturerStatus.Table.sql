USE [NewAppDb]
GO
/****** Object:  Table [dbo].[ManufacturerStatus]    Script Date: 04/16/2015 18:34:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ManufacturerStatus](
	[Manuf_Status_Id] [int] NULL,
	[Manuf_Status_Value] [varchar](15) NULL,
	[Manuf_Status_Description] [varchar](100) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
