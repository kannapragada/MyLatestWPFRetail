USE [NewAppDb]
GO
/****** Object:  StoredProcedure [dbo].[CleanUpDatabase]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================        
-- Author:  <Author,,Name>        
-- Create date: <Create Date,,>        
-- Description: <Description,,>        
-- =============================================        
CREATE PROCEDURE [dbo].[CleanUpDatabase]        
AS        
        
BEGIN        
 -- SET NOCOUNT ON added to prevent extra result sets from        
 -- interfering with SELECT statements.        
SET NOCOUNT ON;        
     
declare @sqlerror varchar(max)        
    
SET @sqlerror = ''        
        
BEGIN TRANSACTION        
BEGIN TRY          
	print 'truncate table Customer'  
	truncate table Customer        
		           
	print 'truncate table DiscItemBatch'  
	truncate table DiscItemBatch        
	           
	print 'truncate table Discounts'  
	truncate table Discounts        
	           
	print 'truncate table ItemBatch'  
	truncate table ItemBatch        
	           
	print 'truncate table Items'  
	truncate table Items        
	 
	print 'truncate table Manufacturer'  
	truncate table Manufacturer        
           
	print 'truncate table Sales'  
	truncate table Sales        
	           
	print 'truncate table SalesDetails'  
	truncate table SalesDetails        
	           
	print 'truncate table SalesDiscounts'  
	truncate table SalesDiscounts        
	           
	print 'truncate table SalesTaxes'  
	truncate table SalesTaxes        
	       
	print 'truncate table SaleTmpDateTime'  
	truncate table SaleTmpDateTime    
	       
	print 'truncate table SaleTmpItems'  
	truncate table SaleTmpItems    
           
	print 'truncate table Stores'  
	truncate table Stores        
	           
	print 'truncate table Tax'  
	truncate table Tax        
	           
	print 'truncate table TaxItemDtls'  
	truncate table TaxItemDtls        
	       
	print 'truncate table Vendor'  
	truncate table Vendor    
	
	print 'truncate table UserProfile'  
	delete from UserProfile where USER_ID <> 'admin'

	print 'truncate table NextIds'  
	truncate table NextIds    
	       
	print 'Resetting values for NextIds'  
	INSERT INTO NextIds VALUES ('CUST','ITEM','SALE','DISC','TAX','BILL', 'MANUF', 'VENDOR','USER',1,1,1,1,1,1,1,1,1)    
	      
	print 'commit'    
	COMMIT TRANSACTION        
END TRY        


BEGIN CATCH        
-- Whoops, there was an error        
	BEGIN        
		ROLLBACK TRANSACTION        
		SET @sqlerror = 'Error while CleanUp. ' + ERROR_MESSAGE()        
		RAISERROR (@sqlerror, 11, 1)        
	END        
END CATCH        
END
GO
