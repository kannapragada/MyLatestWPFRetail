USE [NewAppDb]
GO
/****** Object:  StoredProcedure [dbo].[GetManufSaleDetails]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================                
      
-- Author:  <Author,,Name>                
      
-- Create date: <Create Date,,>                
      
-- Description: <Description,,>                
      
-- =============================================                
      
CREATE PROCEDURE [dbo].[GetManufSaleDetails]                
 @Sales_Id   varchar(15) = null,                
 @Manuf_Id   varchar(15) = null,                
 @Manuf_Name   varchar(50) = null,     
 @Sale_Start_Date datetime = null,                
 @Sale_End_Date  datetime = null            
 AS                
    
BEGIN                
    
 SET NOCOUNT ON;                
      
 declare @SQL varchar(max)                
 declare @SQL1 varchar(max)                
 declare @tmpstr varchar(max)                
      
 IF (@Manuf_Id = 'ALL')                
 BEGIN                
     
  SELECT distinct sd.SD_Sales_Id SalesId, sd.SD_Serial_Numb,               
  sd.SD_Item_Id, i.Item_Name, sd.SD_Batch_Id, ib.IB_Manuf_Id, ib.IB_Vendor_Id, sd.SD_Qty_Sold, sd.SD_Weight,             
  sd.SD_Item_Amt_Per_Unit, sd.SD_Disc_Amt_Per_Unit, sd.SD_Tax_Amt_Per_Unit, sd.SD_Total_Item_Amt, sd.SD_Disc_Item_Amt,      
  sd.SD_Tax_Item_Amt, sd.SD_Final_Item_Amt, sd.SD_Create_Date, sd.SD_Last_Mod_Date            
  INTO #TMP1    
  FROM SalesDetails AS sd             
  INNER JOIN Sales AS s ON s.Sales_Id = sd.SD_Sales_Id             
  INNER JOIN ItemBatch AS ib ON IB.IB_Item_Id = SD.SD_Item_Id AND ib.IB_Batch_Id = sd.SD_Batch_Id    
  INNER JOIN Items AS i ON i.Item_Id = ib.IB_Item_Id         
      
  SELECT distinct s.Sales_Id, s.Sales_Cust_Id, s.Sales_Total_Sales_Amt, s.Sales_Total_Disc_Amt,           
  s.Sales_Total_Tax_Amt, s.Sales_Final_Sales_Amt, s.Sales_Amt_Paid, s.Sales_Balance_Amt,           
  s.Sales_Create_Date, s.Sales_Last_Mod_Date, c.Cust_Name          
  FROM Sales AS s           
  INNER JOIN SalesDetails AS sd ON s.Sales_Id = sd.SD_Sales_Id           
  INNER JOIN Customer AS c ON s.Sales_Cust_Id = c.Cust_Id            
  where Sales_Id in (select SalesId from #TMP1)    
  
  select * from #TMP1    
      
 END                
 ELSE                
 BEGIN            
     
  SELECT distinct sd.SD_Sales_Id SalesId, sd.SD_Serial_Numb,               
  sd.SD_Item_Id, i.Item_Name, sd.SD_Batch_Id, ib.IB_Manuf_Id, ib.IB_Vendor_Id, sd.SD_Qty_Sold, sd.SD_Weight,             
  sd.SD_Item_Amt_Per_Unit, sd.SD_Disc_Amt_Per_Unit, sd.SD_Tax_Amt_Per_Unit, sd.SD_Total_Item_Amt, sd.SD_Disc_Item_Amt,      
  sd.SD_Tax_Item_Amt, sd.SD_Final_Item_Amt, sd.SD_Create_Date, sd.SD_Last_Mod_Date    
  INTO #TMP2       
  FROM SalesDetails AS sd             
  INNER JOIN Sales AS s ON s.Sales_Id = sd.SD_Sales_Id             
  INNER JOIN ItemBatch AS ib ON IB.IB_Item_Id = SD.SD_Item_Id AND ib.IB_Batch_Id = sd.SD_Batch_Id    
  INNER JOIN Items AS i ON i.Item_Id = ib.IB_Item_Id        
  WHERE sd.SD_Sales_Id = '1'    
     
  SET @SQL = 'SELECT distinct sd.SD_Sales_Id SalesId, sd.SD_Serial_Numb,               
     sd.SD_Item_Id, i.Item_Name, sd.SD_Batch_Id, ib.IB_Manuf_Id, ib.IB_Vendor_Id, sd.SD_Qty_Sold, sd.SD_Weight,             
     sd.SD_Item_Amt_Per_Unit, sd.SD_Disc_Amt_Per_Unit, sd.SD_Tax_Amt_Per_Unit, sd.SD_Total_Item_Amt, sd.SD_Disc_Item_Amt,      
     sd.SD_Tax_Item_Amt, sd.SD_Final_Item_Amt, sd.SD_Create_Date, sd.SD_Last_Mod_Date    
     FROM SalesDetails AS sd             
     INNER JOIN Sales AS s ON s.Sales_Id = sd.SD_Sales_Id             
     INNER JOIN ItemBatch AS ib ON IB.IB_Item_Id = SD.SD_Item_Id AND ib.IB_Batch_Id = sd.SD_Batch_Id    
     INNER JOIN Items AS i ON i.Item_Id = ib.IB_Item_Id    
     INNER JOIN Manufacturer AS m ON m.Manuf_Id = ib.IB_Manuf_Id'    
     
      
  set @tmpstr = ''                
      
  IF (@Sales_Id IS NOT NULL AND LEN(@Sales_Id) > 0)                
  BEGIN                
   SET @tmpstr = ' WHERE (s.Sales_Id = ''' + CAST(@Sales_Id AS NVARCHAR(15)) + ''')'                
  END                
       
  IF (@Manuf_Id IS NOT NULL AND LEN(@Manuf_Id) > 0)                  
  BEGIN                  
   if (LEN(@tmpstr) > 0)                  
   SET @tmpstr = @tmpstr + ' AND '                  
   else                  
   SET @tmpstr = ' WHERE '                  
   set @tmpstr = @tmpstr + '(m.Manuf_Id = ''' + CAST(@Manuf_Id AS NVARCHAR(15))  + ''')'                    
  END              
      
  IF (@Manuf_Name IS NOT NULL AND LEN(@Manuf_Name) > 0)                  
  BEGIN                  
   if (LEN(@tmpstr) > 0)                  
   SET @tmpstr = @tmpstr + ' AND '                  
   else         
   SET @tmpstr = ' WHERE '                  
    
   set @tmpstr = @tmpstr + '(m.Manuf_Name = ''' + CAST(@Manuf_Name AS NVARCHAR(15))  + ''')'                      
  END                  
       
  IF ((IsDate(@Sale_Start_Date) = 1) AND (IsDate(@Sale_End_Date) = 1))                
  BEGIN                
   if (LEN(@tmpstr) > 0)                
   SET @tmpstr = @tmpstr + ' AND '                
   else              
   SET @tmpstr = ' WHERE '                
   set @tmpstr = @tmpstr + '(Sales_Create_Date BETWEEN ''' + CONVERT(varchar(30), @Sale_Start_Date, 126)     
   set @tmpstr = @tmpstr + ''' AND ''' + CONVERT(varchar(30), @Sale_End_Date, 126) + ''')'                
  END             
      
     
  SET @SQL = @SQL + @tmpstr             
    
  INSERT INTO #TMP2 EXECUTE(@SQL)       
      
  SELECT distinct s.Sales_Id, s.Sales_Cust_Id, s.Sales_Total_Sales_Amt, s.Sales_Total_Disc_Amt,           
  s.Sales_Total_Tax_Amt, s.Sales_Final_Sales_Amt, s.Sales_Amt_Paid, s.Sales_Balance_Amt,           
  s.Sales_Create_Date, s.Sales_Last_Mod_Date, c.Cust_Name          
  FROM Sales AS s           
  INNER JOIN SalesDetails AS sd ON s.Sales_Id = sd.SD_Sales_Id           
  INNER JOIN Customer AS c ON s.Sales_Cust_Id = c.Cust_Id            
  where Sales_Id in (select SalesId from #TMP2)    
  
    
    
  select * from #TMP2  
    
      
 -- /* Execute Transact-SQL String */                
      
 END                
END
GO
