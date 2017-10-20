USE [NewAppDb]
GO
/****** Object:  StoredProcedure [dbo].[DeleteStoreItem]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DeleteStoreItem]
	@Item_Id			varchar(15)
AS

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	declare @sqlerror	varchar(max)

	SET @sqlerror = ''

	BEGIN TRANSACTION
		BEGIN TRY
			delete from Items
			where Item_Id = @Item_Id

			delete from ItemBatch
			where
				IB_Item_Id = @Item_Id

			COMMIT TRANSACTION
		END TRY

		BEGIN CATCH
		-- Whoops, there was an error
				BEGIN
					ROLLBACK TRANSACTION
					SET @sqlerror = 'Error while deleting an Item. ' + ERROR_MESSAGE()
					RAISERROR (@sqlerror, 11, 1)
				END
		END CATCH
END
GO
