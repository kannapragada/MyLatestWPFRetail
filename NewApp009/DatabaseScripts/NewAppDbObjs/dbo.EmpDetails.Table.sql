USE [NewAppDb]
GO
/****** Object:  Table [dbo].[EmpDetails]    Script Date: 04/16/2015 18:34:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EmpDetails](
	[EmpId] [int] NULL,
	[EmpName] [varchar](50) NULL,
	[DeptId] [int] NULL,
	[Salary] [numeric](18, 0) NULL,
	[CreateDate] [datetime] NULL,
	[LastModDate] [datetime] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
