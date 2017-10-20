USE [NewAppDb]
GO
/****** Object:  StoredProcedure [dbo].[GetRepCustOutstandingDtls]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================      
-- Author:  <Author,,Name>      
-- Create date: <Create Date,,>      
-- Description: <Description,,>      
-- =============================================      
CREATE PROCEDURE [dbo].[GetRepCustOutstandingDtls]      
@Mode varchar(15),
@Date datetime = null,
@Month varchar(15) = null,
@Year int = null
AS      
      
BEGIN      
  
  
 -- SET NOCOUNT ON added to prevent extra result sets from      
 -- interfering with SELECT statements.      
 SET NOCOUNT ON;      
  
 declare @tmp1 table (CustomerId varchar(15), CustomerName varchar(100), AsOnDate datetime, AsOnMonth varchar(15), AsOnYear int,
 SalesAmtPaid numeric(18, 2), SalesBalanceAmt  numeric(18, 2))  
  
   
 if @Mode = 'D'  
 BEGIN  
   insert into @tmp1  
   select a.Cust_Id CustomerId, a.Cust_Name CustomerName, @Date AsOnDate, 0 AsOnMonth, 0 AsOnYear, 
   SUM(b.Sales_Amt_Paid) SalesAmtPaid, SUM(b.Sales_Balance_Amt) SalesBalanceAmt 
   from Customer a join Sales b on a.Cust_Id = b.Sales_Cust_Id
   where CONVERT(varchar(30), b.Sales_Create_Date, 103) <= CONVERT(varchar(30), @Date, 103) 
   group by a.Cust_Id, a.Cust_Name
 END  
 
 if @Mode = 'M'  
 BEGIN  
   insert into @tmp1  
   select a.Cust_Id CustomerId, a.Cust_Name CustomerName, 0 AsOnDate, DateName( month , DateAdd( month , 
   datepart(mm,Cust_Create_Date) , 0 ) - 1 ) AsOnMonth, @Year AsOnYear, 
   SUM(b.Sales_Amt_Paid) SalesAmtPaid, SUM(b.Sales_Balance_Amt) SalesBalanceAmt 
   from Customer a join Sales b on a.Cust_Id = b.Sales_Cust_Id
   where datepart(mm,b.Sales_Create_Date) = @Month 
   group by a.Cust_Id, a.Cust_Name,
   DateName(month, DateAdd(month, datepart(mm,Cust_Create_Date), 0) - 1)
 END  
 
 if @Mode = 'Y'  
 BEGIN  
   insert into @tmp1  
   select a.Cust_Id CustomerId, a.Cust_Name CustomerName, 0 AsOnDate, 0 AsOnMonth, @Year AsOnYear, 
   SUM(b.Sales_Amt_Paid) SalesAmtPaid, SUM(b.Sales_Balance_Amt) SalesBalanceAmt 
   from Customer a join Sales b on a.Cust_Id = b.Sales_Cust_Id
   where datepart(yyyy,b.Sales_Create_Date) = @Year 
   group by a.Cust_Id, a.Cust_Name
 END  
 
 select * from @tmp1 where SalesBalanceAmt > 0
  
END
GO
