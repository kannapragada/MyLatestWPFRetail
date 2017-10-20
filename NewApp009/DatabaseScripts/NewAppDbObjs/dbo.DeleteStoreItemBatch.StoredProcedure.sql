USE [NewAppDb]
GO
/****** Object:  StoredProcedure [dbo].[DeleteStoreItemBatch]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DeleteStoreItemBatch]
	@Item_Id			varchar(15),
	@IB_Batch_Id		varchar(15),
	@sqlerror			varchar(MAX) output
AS

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SET @sqlerror = ''

	BEGIN TRANSACTION
		BEGIN TRY
			delete from ItemBatch
			where
				IB_Item_Id = @Item_Id
			and IB_Batch_Id = @IB_Batch_Id

			COMMIT TRANSACTION
		END TRY

		BEGIN CATCH
		-- Whoops, there was an error
				BEGIN
					ROLLBACK TRANSACTION
					SET @sqlerror = 'Error while deleting an existing Item Batch. ' + ERROR_MESSAGE()
				END
		END CATCH
END
GO
