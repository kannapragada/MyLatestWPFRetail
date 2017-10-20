USE [NewAppDb]
GO
/****** Object:  StoredProcedure [dbo].[DeleteVendorDetails]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DeleteVendorDetails]
	@Vendor_Id		varchar(15)
AS

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	declare @sqlerror	varchar(max)


	SET @sqlerror = ''

		BEGIN TRY		
			if (select COUNT(IB_Vendor_Id) from ItemBatch where IB_Vendor_Id = @Vendor_Id) = 0
			BEGIN
				delete from Vendor where Vendor_Id = @Vendor_Id
			END
			ELSE
			BEGIN
				SET @sqlerror = 'Cannot Delete This Vendor As Items Are Available From This Vendor' 
				RAISERROR (@sqlerror, 11, 1)
			END
		END TRY
		BEGIN CATCH
		-- Whoops, there was an error
			BEGIN
				SET @sqlerror = 'Error while deleting Vendor. ' + ERROR_MESSAGE()
				RAISERROR (@sqlerror, 11, 1)
			END
		END CATCH
END
GO
