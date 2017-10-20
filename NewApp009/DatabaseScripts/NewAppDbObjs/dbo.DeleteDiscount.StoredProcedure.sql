USE [NewAppDb]
GO
/****** Object:  StoredProcedure [dbo].[DeleteDiscount]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DeleteDiscount]
	@Disc_Code		varchar(15)
AS

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	declare @sqlerror	varchar(max)

	SET @sqlerror = ''

		BEGIN TRANSACTION
		BEGIN TRY
			IF ((select COUNT(DiscI_Item_Id) from DiscItemBatch 
				where DiscI_Disc_Code = @Disc_Code) = 0)
			BEGIN
				delete from Discounts where Disc_Code = @Disc_Code
				
				delete from DiscItemBatch where DiscI_Disc_Code = @Disc_Code
			END
			ELSE
			BEGIN
				SET @sqlerror = 'Cannot Delete This Discount As Items Are Linked' 
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
