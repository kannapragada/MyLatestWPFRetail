USE [NewAppDb]
GO
/****** Object:  StoredProcedure [dbo].[AddNewTax]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AddNewTax]
		@Tax_Code				varchar(15),
		@Tax_Name				varchar(50),
		@Tax_Description		varchar(100),
		@Tax_Kind_Id			int,
		@Tax_Type_Id			int,
		@Tax_Value				decimal(10, 2),
		@Tax_Start_Date			datetime,
		@Tax_End_Date			datetime,
		@Tax_Create_Date		datetime
AS

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	declare @sqlerror	varchar(max)

	SET @sqlerror = ''

	BEGIN TRANSACTION
		BEGIN TRY
			insert into Tax(Tax_Code, Tax_Name, Tax_Description, Tax_Kind_Id, Tax_Type_Id, 
			Tax_Value, Tax_Start_Date, Tax_End_Date, Tax_Create_Date, Tax_Last_Mod_Date)
			values (@Tax_Code, @Tax_Name, @Tax_Description, @Tax_Kind_Id, @Tax_Type_Id, @Tax_Value, @Tax_Start_Date,
			@Tax_End_Date, @Tax_Create_Date, null)

			COMMIT TRANSACTION
		END TRY

		BEGIN CATCH
		-- Whoops, there was an error
				BEGIN
					ROLLBACK TRANSACTION
					SET @sqlerror = 'Error while adding a New Tax. ' + ERROR_MESSAGE()
					RAISERROR (@sqlerror, 11, 1)
				END
		END CATCH
END
GO
