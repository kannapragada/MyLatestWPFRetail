USE [NewAppDb]
GO
/****** Object:  StoredProcedure [dbo].[GetRepDataYearlyByEntity]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================    
-- Author:  <Author,,Name>    
-- Create date: <Create Date,,>    
-- Description: <Description,,>    
-- =============================================    
CREATE PROCEDURE [dbo].[GetRepDataYearlyByEntity]    
@Entity	varchar(15)
AS    
    
BEGIN    


 -- SET NOCOUNT ON added to prevent extra result sets from    
 -- interfering with SELECT statements.    
	SET NOCOUNT ON;    

--Entitys
--Sales - 'S'
--Customers - 'C'
--Items - 'I'
--Vendors - 'V'

	declare @tmp0 table (YearNo int)
			insert @tmp0 values (2010)
			insert @tmp0 values (2011)
			insert @tmp0 values (2012)
			insert @tmp0 values (2013)

			
	declare @tmp1 table (YearNo int, NoOfCustomers int, NoOfSales int, NoOfItems int, NoOfVendors int)  
 
	if @Entity = 'S'
	BEGIN		
		insert into @tmp1  
		select datepart(yyyy,Sales_Create_Date) YearNo, 0 NoOfCustomers, COUNT(*) NoOfSales, 0 NoOfItems, 0 NoOfVendors 
		from Sales group by  datepart(yyyy,Sales_Create_Date)         
	END

	if @Entity = 'C'
	BEGIN		
		insert into @tmp1  
		select datepart(yyyy,Cust_Create_Date) YearNo, COUNT(*) NoOfCustomers, 0 NoOfSales, 0 NoOfItems, 0 NoOfVendors 
		from Customer group by  datepart(yyyy,Cust_Create_Date)          
	END

	if @Entity = 'I'
	BEGIN		
		insert into @tmp1  
		select datepart(yyyy,Item_Create_Date) YearNo, 0 NoOfCustomers, 0 NoOfSales, COUNT(*) NoOfItems, 0 NoOfVendors 
		from Items group by  datepart(yyyy,Item_Create_Date)     
	END

	if @Entity = 'V'
	BEGIN		
		insert into @tmp1  
		select datepart(yyyy,Vendor_Create_Date) YearNo, 0 NoOfCustomers, 0 NoOfSales, 0 NoOfItems, COUNT(*) NoOfVendors 
		from Vendor group by  datepart(yyyy,Vendor_Create_Date)     
	END
	
	insert into @tmp1  
	select YearNo YearNo, 0 NoOfCustomers, 0 NoOfSales, 0 NoOfItems, 0 NoOfVendors from @tmp0  
	where YearNo Not In (select YearNo from @tmp0)   
     
	select * from @tmp1 order by YearNo  

END
GO
