USE [NewAppDb]
GO
/****** Object:  Table [dbo].[IDProofTypes]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[IDProofTypes](
	[IDProof_Type_Id] [int] NULL,
	[IDProof_Type_Value] [varchar](15) NULL,
	[IDProof_Type_Description] [varchar](100) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
