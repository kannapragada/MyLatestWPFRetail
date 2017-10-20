USE [NewAppDb]
GO
/****** Object:  StoredProcedure [dbo].[AddCustomerImg]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AddCustomerImg]
	@ID					int,
	@Name				varchar(30),
	@Picture			image
AS

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	declare @sqlerror	varchar(max)

	SET @sqlerror = ''

	BEGIN TRANSACTION
		BEGIN TRY
			insert into tblImgData(ID, Name, Picture)
 			values (@ID, @Name, @Picture)

			COMMIT TRANSACTION
		END TRY

		BEGIN CATCH
		-- Whoops, there was an error
				BEGIN
					ROLLBACK TRANSACTION
					SET @sqlerror = 'Error While Adding New Customer Image. ' + ERROR_MESSAGE()
					RAISERROR (@sqlerror, 11, 1)
				END
		END CATCH
END
GO
