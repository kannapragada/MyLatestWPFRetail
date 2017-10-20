USE [NewAppDb]
GO
/****** Object:  StoredProcedure [dbo].[GetRepExpiringItems]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================        
-- Author:  <Author,,Name>        
-- Create date: <Create Date,,>        
-- Description: <Description,,>        
-- =============================================        
CREATE PROCEDURE [dbo].[GetRepExpiringItems]        
@Mode varchar(15),  
@Date datetime = null,  
@Month int = null,  
@Year int = null  
AS        
        
BEGIN        
    
    
 -- SET NOCOUNT ON added to prevent extra result sets from        
 -- interfering with SELECT statements.        
 SET NOCOUNT ON;        
    
 declare @tmp1 table (ItemId varchar(15), ItemName varchar(100), BatchId varchar(15), QtyAvailable bigint,  
 QtyProcured bigint, PriceProcured numeric(18, 2), PriceSell numeric(18, 2),  
 DateProcured datetime, DateManuf datetime, DateExp datetime, AsOnDate datetime null, AsOnMonth varchar(15) null, AsOnYear int null)    
    
     
 if @Mode = 'D'    
 BEGIN    
 insert into @tmp1    
 select a.Item_Id ItemId, a.Item_Name ItemName, b.IB_Batch_Id BatchId, b.IB_Qty_Available QtyAvailable,  
   b.IB_Qty_Procured QtyProcured, b.IB_Price_Procured PriceProcured, b.IB_Price_Sell PriceSell,  
   b.IB_Date_Procured DateProcured, b.IB_Date_Manuf DateManuf, b.IB_Date_Exp DateExp, @Date AsOnDate, null AsOnMonth, null AsOnYear  
 from Items a join ItemBatch b on a.Item_Id = b.IB_Item_Id  
 where CONVERT(date, b.IB_Date_Exp) <= CONVERT(date, @Date)  
     
 END    
   
 if @Mode = 'M'    
 BEGIN    
 insert into @tmp1    
 select a.Item_Id ItemId, a.Item_Name ItemName, b.IB_Batch_Id BatchId, b.IB_Qty_Available QtyAvailable,  
   b.IB_Qty_Procured QtyProcured, b.IB_Price_Procured PriceProcured, b.IB_Price_Sell PriceSell,  
   b.IB_Date_Procured DateProcured, b.IB_Date_Manuf DateManuf, b.IB_Date_Exp DateExp, null AsOnDate,   
   DateName( month , DateAdd(month, datepart(mm,IB_Date_Exp) , 0 ) - 1 ) AsOnMonth, @Year AsOnYear  
 from Items a join ItemBatch b on a.Item_Id = b.IB_Item_Id  
 where datepart(mm,b.IB_Date_Exp) <= @Month and datepart(yyyy,b.IB_Date_Exp) <= @Year   
     
 END    
   
 if @Mode = 'Y'    
 BEGIN    
   insert into @tmp1    
   select a.Item_Id ItemId, a.Item_Name ItemName, b.IB_Batch_Id BatchId, b.IB_Qty_Available QtyAvailable,  
   b.IB_Qty_Procured QtyProcured, b.IB_Price_Procured PriceProcured, b.IB_Price_Sell PriceSell,  
   b.IB_Date_Procured DateProcured, b.IB_Date_Manuf DateManuf, b.IB_Date_Exp DateExp, null AsOnDate,   
   null AsOnMonth, @Year AsOnYear            
 from Items a join ItemBatch b on a.Item_Id = b.IB_Item_Id  
 where datepart(yyyy,b.IB_Date_Exp) <= @Year   
        
 END    
   
 select * from @tmp1 order by DateProcured, DateManuf, DateExp  
    
END
GO
