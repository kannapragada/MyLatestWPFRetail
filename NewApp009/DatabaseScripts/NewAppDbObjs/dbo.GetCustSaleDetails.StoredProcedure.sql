USE [NewAppDb]
GO
/****** Object:  StoredProcedure [dbo].[GetCustSaleDetails]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================            
  
-- Author:  <Author,,Name>            
  
-- Create date: <Create Date,,>            
  
-- Description: <Description,,>            
  
-- =============================================            
  
create PROCEDURE [dbo].[GetCustSaleDetails]            
 @Sales_Id   varchar(15) = null,            
 @Cust_Id   varchar(15) = null,            
 @Cust_Name   varchar(50) = null, 
 @Sale_Start_Date datetime = null,            
 @Sale_End_Date  datetime = null        
 AS            

BEGIN            

 SET NOCOUNT ON;            
  
 declare @SQL varchar(max)            
 declare @SQL1 varchar(max)            
 declare @tmpstr varchar(max)            
  
 IF (@Sales_Id = 'ALL')            
	BEGIN            
		SELECT distinct s.Sales_Id, s.Sales_Cust_Id, s.Sales_Total_Sales_Amt, s.Sales_Total_Disc_Amt,         
		s.Sales_Total_Tax_Amt, s.Sales_Final_Sales_Amt, s.Sales_Amt_Paid, s.Sales_Balance_Amt,         
		s.Sales_Create_Date, s.Sales_Last_Mod_Date, c.Cust_Name        
		FROM Sales AS s         
		INNER JOIN SalesDetails AS sd ON s.Sales_Id = sd.SD_Sales_Id         
		INNER JOIN Customer AS c ON s.Sales_Cust_Id = c.Cust_Id          

		SELECT sd.SD_Sales_Id, sd.SD_Serial_Numb,           
		sd.SD_Item_Id, i.Item_Name, sd.SD_Batch_Id, ib.IB_Manuf_Id, ib.IB_Vendor_Id, sd.SD_Qty_Sold, sd.SD_Weight,         
		sd.SD_Item_Amt_Per_Unit, sd.SD_Disc_Amt_Per_Unit, sd.SD_Tax_Amt_Per_Unit, sd.SD_Total_Item_Amt, sd.SD_Disc_Item_Amt,  
		sd.SD_Tax_Item_Amt, sd.SD_Final_Item_Amt, sd.SD_Create_Date, sd.SD_Last_Mod_Date      
		FROM Sales AS s         
		INNER JOIN SalesDetails AS sd ON s.Sales_Id = sd.SD_Sales_Id         
		INNER JOIN Items AS i ON sd.SD_Item_Id = i.Item_Id        
		INNER JOIN ItemBatch AS ib ON sd.SD_Item_Id = ib.IB_Item_Id and sd.SD_Batch_Id = ib.IB_Batch_Id      
	END            
 ELSE            
 BEGIN            
    SELECT distinct s.Sales_Id, s.Sales_Cust_Id, s.Sales_Total_Sales_Amt, s.Sales_Total_Disc_Amt,         
    s.Sales_Total_Tax_Amt, s.Sales_Final_Sales_Amt, s.Sales_Amt_Paid, s.Sales_Balance_Amt,         
    s.Sales_Create_Date, s.Sales_Last_Mod_Date, c.Cust_Name        
	INTO #TMP1        
	FROM Sales AS s         
	INNER JOIN SalesDetails AS sd ON s.Sales_Id = sd.SD_Sales_Id         
	INNER JOIN Customer AS c ON s.Sales_Cust_Id = c.Cust_Id             
	WHERE s.Sales_Id = '1'          
  
	SET @SQL = 'SELECT distinct s.Sales_Id, s.Sales_Cust_Id, s.Sales_Total_Sales_Amt, s.Sales_Total_Disc_Amt,           
    s.Sales_Total_Tax_Amt, s.Sales_Final_Sales_Amt, s.Sales_Amt_Paid, s.Sales_Balance_Amt,         
    s.Sales_Create_Date, s.Sales_Last_Mod_Date, c.Cust_Name        
	FROM Sales AS s         
	INNER JOIN SalesDetails AS sd ON s.Sales_Id = sd.SD_Sales_Id         
	INNER JOIN Customer AS c ON s.Sales_Cust_Id = c.Cust_Id'        

	set @tmpstr = ''            
  
	IF (@Sales_Id IS NOT NULL AND LEN(@Sales_Id) > 0)            
	BEGIN            
		SET @tmpstr = ' WHERE (s.Sales_Id = ''' + CAST(@Sales_Id AS NVARCHAR(15)) + ''')'            
	END            
  
	IF (@Cust_Id IS NOT NULL AND LEN(@Cust_Id) > 0)              
	BEGIN              
		if (LEN(@tmpstr) > 0)              
		SET @tmpstr = @tmpstr + ' AND '              
		else              
		SET @tmpstr = ' WHERE '              
		set @tmpstr = @tmpstr + '(c.Cust_Id = ''' + CAST(@Cust_Id AS NVARCHAR(15))  + ''')'                
	END          
  
	IF (@Cust_Name IS NOT NULL AND LEN(@Cust_Name) > 0)              
	BEGIN              
		if (LEN(@tmpstr) > 0)              
		SET @tmpstr = @tmpstr + ' AND '              
		else     
		SET @tmpstr = ' WHERE '              

		set @tmpstr = @tmpstr + '(c.Cust_Name = ''' + CAST(@Cust_Name AS NVARCHAR(15))  + ''')'                  
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
  
	INSERT INTO #TMP1 EXECUTE(@SQL)            
  
	SELECT sd.SD_Sales_Id, sd.SD_Serial_Numb,           
	sd.SD_Item_Id, i.Item_Name, sd.SD_Batch_Id, ib.IB_Manuf_Id, ib.IB_Vendor_Id, sd.SD_Qty_Sold, sd.SD_Weight,         
	sd.SD_Item_Amt_Per_Unit, sd.SD_Disc_Amt_Per_Unit, sd.SD_Tax_Amt_Per_Unit, sd.SD_Total_Item_Amt, sd.SD_Disc_Item_Amt,  
	sd.SD_Tax_Item_Amt, sd.SD_Final_Item_Amt, sd.SD_Create_Date, sd.SD_Last_Mod_Date          
	INTO #TMP2        
	FROM #TMP1 t        
	INNER JOIN Sales AS s ON s.Sales_Id = t.Sales_Id         
	INNER JOIN SalesDetails AS sd ON s.Sales_Id = sd.SD_Sales_Id         
	INNER JOIN Items AS i ON sd.SD_Item_Id = i.Item_Id        
	INNER JOIN ItemBatch AS ib ON sd.SD_Item_Id = ib.IB_Item_Id and sd.SD_Batch_Id = ib.IB_Batch_Id      
  
 -- /* Execute Transact-SQL String */            
  
  print @SQL           
  SELECT * FROM #TMP1        
  SELECT * FROM #TMP2        
 END            
END
GO
