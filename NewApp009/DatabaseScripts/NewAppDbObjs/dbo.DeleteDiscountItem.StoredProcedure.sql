USE [NewAppDb]
GO
/****** Object:  StoredProcedure [dbo].[DeleteDiscountItem]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DeleteDiscountItem]
	@Disc_Code		varchar(15),
	@Item_Id		varchar(15),
	@Batch_Id		varchar(15)
AS

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	declare @sqlerror	varchar(max)

	SET @sqlerror = ''

		BEGIN TRANSACTION
		BEGIN TRY
			IF ((select count(*) from DiscItemBatch where DiscI_Disc_Code = @Disc_Code 
				AND DiscI_Item_Id = @Item_Id AND DiscI_Item_Batch_Id = @Batch_Id) > 0)
			BEGIN
				delete from DiscItemBatch where DiscI_Disc_Code = @Disc_Code 
				AND DiscI_Item_Id = @Item_Id AND DiscI_Item_Batch_Id = @Batch_Id
			END
			ELSE
			BEGIN
				SET @sqlerror = 'Cannot Delete This Discount Item Link' 
				RAISERROR (@sqlerror, 11, 1)
			END

			COMMIT TRANSACTION
		END TRY

		BEGIN CATCH
		-- Whoops, there was an error
				BEGIN
					ROLLBACK TRANSACTION
					SET @sqlerror = 'Error while deleting This Discount. ' + ERROR_MESSAGE()
					RAISERROR (@sqlerror, 11, 1)
				END
		END CATCH
END
GO
