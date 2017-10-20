USE [NewAppDb]
GO
/****** Object:  StoredProcedure [dbo].[GetCustomerImg]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetCustomerImg]
	@ID					int
AS

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	declare @sqlerror	varchar(max)

	SET @sqlerror = ''
	
		BEGIN TRY
			select * from tblImgData
			where ID = @ID
		END TRY

		BEGIN CATCH
		-- Whoops, there was an error
				BEGIN
					SET @sqlerror = 'Error While Getting Customer Image. ' + ERROR_MESSAGE()
					RAISERROR (@sqlerror, 11, 1)
				END
		END CATCH
END
GO
