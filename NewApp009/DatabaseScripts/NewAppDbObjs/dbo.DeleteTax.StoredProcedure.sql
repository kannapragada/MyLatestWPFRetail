USE [NewAppDb]
GO
/****** Object:  StoredProcedure [dbo].[DeleteTax]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DeleteTax]
	@Tax_Code		varchar(15)
AS

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	declare @sqlerror	varchar(max)

	SET @sqlerror = ''

	BEGIN TRANSACTION
		BEGIN TRY
			BEGIN
				delete from Tax where Tax_Code = @Tax_Code
				
				delete from TaxItemDtls where TI_Tax_Code = @Tax_Code
			END
			COMMIT TRANSACTION
		END TRY

		BEGIN CATCH
		-- Whoops, there was an error
				BEGIN
					ROLLBACK TRANSACTION
					SET @sqlerror = 'Error while deleting This Tax. ' + ERROR_MESSAGE()
					RAISERROR (@sqlerror, 11, 1)
				END
		END CATCH
END
GO
