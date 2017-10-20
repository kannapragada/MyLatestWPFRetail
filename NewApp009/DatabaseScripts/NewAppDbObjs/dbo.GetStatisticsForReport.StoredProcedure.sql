USE [NewAppDb]
GO
/****** Object:  StoredProcedure [dbo].[GetStatisticsForReport]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================      
-- Author:  <Author,,Name>      
-- Create date: <Create Date,,>      
-- Description: <Description,,>      
-- =============================================      
CREATE PROCEDURE [dbo].[GetStatisticsForReport]      
@Mode  varchar(15),  
@Value  bigint = 0,  
@Year  bigint = 0  
AS      
      
BEGIN      
  
declare @NoOfNewCustomers bigint  
declare @NoOfSales bigint  
declare @NoOfNewItems bigint  
declare @NoOfNewVendors bigint  
declare @FinalSalesAmt Numeric(18, 0)  
declare @SalesBalanceAmt Numeric(18, 0)  
  
  
  
  
 -- SET NOCOUNT ON added to prevent extra result sets from      
 -- interfering with SELECT statements.      
 SET NOCOUNT ON;      
   
  
 if @Mode = 'D'  
 begin  
 select @NoOfNewCustomers = COUNT(*) from Customer where datepart(dd,Cust_Create_Date) = datepart(dd,GETDATE())  
 select @NoOfSales = COUNT(*) from Sales where datepart(dd,Sales_Create_Date) = datepart(dd,GETDATE())  
 select @NoOfNewItems = COUNT(*) from Items where datepart(dd,Item_Create_Date) = datepart(dd,GETDATE())  
 select @NoOfNewVendors = COUNT(*) from Vendor where datepart(dd,Vendor_Create_Date) = datepart(dd,GETDATE())  
 select @FinalSalesAmt = SUM(Sales_Final_Sales_Amt) from Sales where datepart(dd,Sales_Create_Date) = datepart(dd,GETDATE())  
 select @SalesBalanceAmt = SUM(Sales_Balance_Amt) from Sales where datepart(dd,Sales_Create_Date) = datepart(dd,GETDATE())  
 end  
 else  
 if @Mode = 'M'  
 begin  
  select @NoOfNewCustomers = COUNT(*) from Customer where datepart(mm,Cust_Create_Date) = @Value and datepart(yyyy,Cust_Create_Date) = @Year
  select @NoOfSales = COUNT(*) from Sales where datepart(mm,Sales_Create_Date) = @Value  and datepart(yyyy,Sales_Create_Date) = @Year
  select @NoOfNewItems = COUNT(*) from Items where datepart(mm,Item_Create_Date) = @Value  and datepart(yyyy,Item_Create_Date) = @Year
  select @NoOfNewVendors = COUNT(*) from Vendor where datepart(mm,Vendor_Create_Date) = @Value  and datepart(yyyy,Vendor_Create_Date) = @Year
  select @FinalSalesAmt = SUM(Sales_Final_Sales_Amt) from Sales where datepart(mm,Sales_Create_Date) = @Value  and datepart(yyyy,Sales_Create_Date) = @Year
  select @SalesBalanceAmt = SUM(Sales_Balance_Amt) from Sales where datepart(mm,Sales_Create_Date) = @Value  and datepart(yyyy,Sales_Create_Date) = @Year
 end  
 else  
 if @Mode = 'Y'  
 begin  
  select @NoOfNewCustomers = COUNT(*) from Customer where datepart(yyyy,Cust_Create_Date) = @Value  
  select @NoOfSales = COUNT(*) from Sales where datepart(yyyy,Sales_Create_Date) = @Value  
  select @NoOfNewItems = COUNT(*) from Items where datepart(yyyy,Item_Create_Date) = @Value  
  select @NoOfNewVendors = COUNT(*) from Vendor where datepart(yyyy,Vendor_Create_Date) = @Value  
  select @FinalSalesAmt = SUM(Sales_Final_Sales_Amt) from Sales where datepart(yyyy,Sales_Create_Date) = @Value  
  select @SalesBalanceAmt = SUM(Sales_Balance_Amt) from Sales where datepart(yyyy,Sales_Create_Date) = @Value  
 end  
   
 truncate table StatisticalData  
   
 INSERT INTO StatisticalData VALUES(@NoOfNewCustomers, @NoOfSales, @NoOfNewItems, @NoOfNewVendors, @FinalSalesAmt, @SalesBalanceAmt, GETDATE())  
   
 select * from StatisticalData  
   
END
GO
