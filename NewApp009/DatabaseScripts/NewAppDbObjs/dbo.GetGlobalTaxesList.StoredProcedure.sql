USE [NewAppDb]
GO
/****** Object:  StoredProcedure [dbo].[GetGlobalTaxesList]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetGlobalTaxesList]
AS

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT  Tax_Code, Tax_Name, Tax_Description, Tax_Kind_Id, Tax_Type_Id, Tax_Value, Tax_Start_Date,
			Tax_End_Date, Tax_Create_Date, Tax_Last_Mod_Date
	INTO #Temp1
	FROM Tax 
	where Tax_Kind_Id = 0

	select * from #Temp1
	
END
GO
