USE [NewAppDb]
GO
/****** Object:  StoredProcedure [dbo].[GetCustomerDetails]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetCustomerDetails]
	@Cust_Id		varchar(15)
AS

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	declare @sqlerror	varchar(max)


	SET @sqlerror = ''

		BEGIN TRY		
				select * from Customer where Cust_Id = @Cust_Id
				
				SELECT	distinct s.Sales_Id, s.Sales_Cust_Id, s.Sales_Total_Sales_Amt, s.Sales_Total_Disc_Amt, 
				s.Sales_Total_Tax_Amt, s.Sales_Final_Sales_Amt, s.Sales_Amt_Paid, s.Sales_Balance_Amt, 
				s.Sales_Create_Date, s.Sales_Last_Mod_Date, c.Cust_Name
		FROM	Sales AS s INNER JOIN
				SalesDetails AS sd ON s.Sales_Id = sd.SD_Sales_Id INNER JOIN
				Customer AS c ON s.Sales_Cust_Id = c.Cust_Id INNER JOIN
				Items AS i ON sd.SD_Item_Id = i.Item_Id
		WHERE	s.Sales_Cust_Id = @Cust_Id
	
		SELECT	sd.SD_Sales_Id, sd.SD_Serial_Numb, 
				sd.SD_Item_Id, sd.SD_Batch_Id, sd.SD_Qty_Sold, sd.SD_Weight, 
				sd.SD_Item_Amt_Per_Unit, sd.SD_Disc_Amt_Per_Unit, sd.SD_Total_Item_Amt, sd.SD_Final_Item_Amt,
				sd.SD_Create_Date, sd.SD_Last_Mod_Date, i.Item_Name, c.Cust_Name
		FROM	Sales AS s INNER JOIN
				SalesDetails AS sd ON s.Sales_Id = sd.SD_Sales_Id INNER JOIN
				Customer AS c ON s.Sales_Cust_Id = c.Cust_Id INNER JOIN
				Items AS i ON sd.SD_Item_Id = i.Item_Id
		WHERE	s.Sales_Cust_Id = @Cust_Id
		END TRY

		-- Whoops, there was an error
		BEGIN CATCH
					SET @sqlerror = 'Error while getting the Customer details. ' + ERROR_MESSAGE()
					RAISERROR (@sqlerror, 11, 1)
		END CATCH
END
GO
