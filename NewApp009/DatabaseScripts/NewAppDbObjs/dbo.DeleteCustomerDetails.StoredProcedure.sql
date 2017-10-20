USE [NewAppDb]
GO
/****** Object:  StoredProcedure [dbo].[DeleteCustomerDetails]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DeleteCustomerDetails]
	@Cust_Id		varchar(15)
AS

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	declare @sqlerror	varchar(max)


	SET @sqlerror = ''

		BEGIN TRY		
			if (select COUNT(sales_Id) from Sales where Sales_Cust_Id = @Cust_Id) = 0
			BEGIN
				delete from Customer where Cust_Id = @Cust_Id
			END
			ELSE
			BEGIN
				SET @sqlerror = 'Cannot Delete This Customer As Sales Were By Done By This Customer' 
				RAISERROR (@sqlerror, 11, 1)
			END
		END TRY
		BEGIN CATCH
		-- Whoops, there was an error
			BEGIN
				SET @sqlerror = 'Error while deleting Customer. ' + ERROR_MESSAGE()
				RAISERROR (@sqlerror, 11, 1)
			END
		END CATCH
END
GO
