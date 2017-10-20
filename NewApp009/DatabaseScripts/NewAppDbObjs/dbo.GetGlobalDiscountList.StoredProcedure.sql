USE [NewAppDb]
GO
/****** Object:  StoredProcedure [dbo].[GetGlobalDiscountList]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetGlobalDiscountList]
AS

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT  Disc_Code, Disc_Name, Disc_Description, Disc_Kind_Id, Disc_Type_Id, Disc_Value, Disc_Start_Date,
			Disc_End_Date, Disc_Create_Date, Disc_Last_Mod_Date
	INTO #Temp1
	FROM Discounts 
	where Disc_Kind_Id = 0

	select * from #Temp1
	
END
GO
