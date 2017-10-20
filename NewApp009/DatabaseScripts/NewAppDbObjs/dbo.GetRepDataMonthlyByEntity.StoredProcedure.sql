USE [NewAppDb]
GO
/****** Object:  StoredProcedure [dbo].[GetRepDataMonthlyByEntity]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================      
-- Author:  <Author,,Name>      
-- Create date: <Create Date,,>      
-- Description: <Description,,>      
-- =============================================      
CREATE PROCEDURE [dbo].[GetRepDataMonthlyByEntity]      
@Entity varchar(15)  
AS      
      
BEGIN      
  
  
 -- SET NOCOUNT ON added to prevent extra result sets from      
 -- interfering with SELECT statements.      
 SET NOCOUNT ON;      
  
--Entities  
--Sales - 'S'  
--Customers - 'C'  
--Items - 'I'  
--Vendors - 'V'  
  
 declare @tmp0 table (MonthNo int)  
   insert @tmp0 values (1)  
   insert @tmp0 values (2)  
   insert @tmp0 values (3)  
   insert @tmp0 values (4)  
   insert @tmp0 values (5)  
   insert @tmp0 values (6)  
   insert @tmp0 values (7)  
   insert @tmp0 values (8)  
   insert @tmp0 values (9)  
   insert @tmp0 values (10)  
   insert @tmp0 values (11)  
   insert @tmp0 values (12)  
     
 declare @tmp1 table (MonthNo int, MName varchar(15), NoOfCustomers int, NoOfSales int, NoOfItems int, NoOfVendors int)  
  
   
 if @Entity = 'S'  
 BEGIN  
   insert into @tmp1  
   select datepart(mm,Sales_Create_Date) MonthNo, DateName( month , DateAdd( month , datepart(mm,Sales_Create_Date) , 0 ) - 1 ) MName,   
   0 NoOfCustomers, COUNT(*) NoOfSales, 0 NoOfItems, 0 NoOfVendors from Sales group by  datepart(mm,Sales_Create_Date)          
 END  
  
 if @Entity = 'C'  
 BEGIN  
   insert into @tmp1  
   select datepart(mm,Cust_Create_Date) MonthNo, DateName( month , DateAdd( month , datepart(mm,Cust_Create_Date) , 0 ) - 1 ) MName,   
   COUNT(*) NoOfCustomers, 0 NoOfSales, 0 NoOfItems, 0 NoOfVendors from Customer group by datepart(mm,Cust_Create_Date)  
 END  
  
 if @Entity = 'I'  
 BEGIN  
   insert into @tmp1  
   select datepart(mm,Item_Create_Date) MonthNo, DateName( month , DateAdd( month , datepart(mm,Item_Create_Date) , 0 ) - 1 ) MName,   
   0 NoOfCustomers, 0 NoOfSales, COUNT(*) NoOfItems, 0 NoOfVendors from Items group by  datepart(mm,Item_Create_Date)  
 END  
  
 if @Entity = 'V'  
 BEGIN  
   insert into @tmp1  
   select datepart(mm,Vendor_Create_Date) MonthNo, DateName( month , DateAdd( month , datepart(mm,Vendor_Create_Date) , 0 ) - 1 ) MName,   
   0 NoOfCustomers, 0 NoOfSales, 0 NoOfItems, COUNT(*) NoOfVendors from Vendor group by  datepart(mm,Vendor_Create_Date)  
 END  
 
   insert into @tmp1  
   select MonthNo MonthNo, DateName(month, DateAdd(month, MonthNo, 0)- 1) MName,   
   0 NoOfCustomers, 0 NoOfSales, 0 NoOfItems, 0 NoOfVendors from @tmp0  
   where MonthNo Not In (select MonthNo from @tmp1)   
     
   select * from @tmp1 order by MonthNo  

END
GO
