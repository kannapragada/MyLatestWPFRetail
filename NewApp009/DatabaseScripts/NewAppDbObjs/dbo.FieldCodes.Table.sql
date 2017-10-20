USE [NewAppDb]
GO
/****** Object:  Table [dbo].[FieldCodes]    Script Date: 04/16/2015 18:34:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FieldCodes](
	[Field_ColumnName] [varchar](50) NULL,
	[Field_Code] [varchar](15) NULL,
	[Field_Description] [varchar](100) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
