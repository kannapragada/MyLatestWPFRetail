USE [NewAppDb]
GO
/****** Object:  StoredProcedure [dbo].[GetStoreItems]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================          
-- Author:  <Author,,Name>          
-- Create date: <Create Date,,>          
-- Description: <Description,,>          
-- =============================================          
CREATE PROCEDURE [dbo].[GetStoreItems]          
 @ItemIdOrName varchar(35) = null          
AS          
          
BEGIN          
 -- SET NOCOUNT ON added to prevent extra result sets from          
 -- interfering with SELECT statements.          
 SET NOCOUNT ON;          
           
           
 declare @SQL varchar(max)          
 declare @SQL1 varchar(max)          
 declare @tmpstr varchar(max)          
          
 IF @ItemIdOrName = 'ALL'
 BEGIN          
  SELECT b.IB_Item_Id ItemId, a.Item_Name ItemName, a.Item_Description ItemDescription,           
      b.IB_Batch_Id BatchId, b.IB_BarCode BarCode, b.IB_MRP MRP, b.IB_Qty_Procured QtyProcured,          
      b.IB_Price_Procured PriceProcured, b.IB_Date_Procured, b.IB_Date_Manuf DateManuf, b.IB_Date_Exp DateExp,           
      b.IB_Weight_Procured WeightProcured, b.IB_Weight_Available WeightAvailable,           
      b.IB_Price_Sell PriceSell, b.IB_Qty_Available QtyAvailable,          
      a.Item_Create_Date ItemCreateDate, a.Item_Last_Mod_Date ItemLastModDate,          
      b.IB_Create_Date BatchCreateDate, b.IB_Last_Mod_Date BatchLastModDate, b.IB_Manuf_Id ManufId, b.IB_Vendor_Id VendorId        
  INTO #TMP1          
  FROM Items a JOIN ItemBatch b ON a.Item_Id = b.IB_Item_Id          
  WHERE a.Item_Id = '1'          
            
  SET @SQL = 'SELECT b.IB_Item_Id ItemId, a.Item_Name ItemName, a.Item_Description ItemDescription,           
      b.IB_Batch_Id BatchId, b.IB_BarCode BarCode, b.IB_MRP MRP, b.IB_Qty_Procured QtyProcured,          
      b.IB_Price_Procured PriceProcured, b.IB_Date_Procured DateProcured, b.IB_Date_Manuf DateManuf, b.IB_Date_Exp DateExp,           
      b.IB_Weight_Procured WeightProcured, b.IB_Weight_Available WeightAvailable,           
      b.IB_Price_Sell PriceSell, b.IB_Qty_Available QtyAvailable,          
      a.Item_Create_Date ItemCreateDate, a.Item_Last_Mod_Date ItemLastModDate,          
      b.IB_Create_Date BatchCreateDate, b.IB_Last_Mod_Date BatchLastModDate, b.IB_Manuf_Id ManufId, b.IB_Vendor_Id VendorId        
     INTO #tmp2 FROM Items a JOIN ItemBatch b ON a.Item_Id = b.IB_Item_Id'          

  SET @SQL = @SQL + ' SELECT * FROM #tmp2'          
          
  /* Execute Transact-SQL String */          
  print @SQL          
  INSERT INTO #TMP1 EXECUTE(@SQL)          
            
  select * from #TMP1          
            
  SELECT a.DiscI_Item_Id ItemId, a.DiscI_Item_Batch_Id BatchId, a.DiscI_Disc_Code DiscCode,             
    a.DiscI_Start_Date DiscStartDate, a.DiscI_End_Date DiscEndDate,            
    a.DiscI_Remarks DiscRemarks, a.DiscI_Create_Date DiscCreateDate,           
    a.DiscI_Last_Mod_Date DiscLastModDate          
  FROM DiscItemBatch a         
  JOIN #tmp1 b ON a.DiscI_Item_Id = b.ItemId          
  JOIN Discounts c ON c.Disc_Code = a.DiscI_Disc_Code          
  AND a.DiscI_Item_Batch_Id = b.BatchId        
  WHERE CONVERT(date, a.DiscI_Start_Date) <= CONVERT(date, GETDATE())         
  AND CONVERT(date, a.DiscI_End_Date) >= CONVERT(date, GETDATE())         
  AND CONVERT(date, c.Disc_Start_Date) <= CONVERT(date, GETDATE())         
  AND CONVERT(date, c.Disc_End_Date) >= CONVERT(date, GETDATE())         
  AND CONVERT(date, a.DiscI_Start_Date) >= CONVERT(date, c.Disc_Start_Date)         
  AND CONVERT(date, a.DiscI_End_Date) <= CONVERT(date, c.Disc_End_Date)         
            
             
  SELECT distinct c.Disc_Code DiscCode, c.Disc_Name DiscName, c.Disc_Description DiscDescription,           
	c.Disc_Kind_Id DiscKindId, c.Disc_Type_Id DiscTypeId, c.Disc_Value DiscValue,           
    c.Disc_Start_Date DiscStartDate, c.Disc_End_Date DiscEndDate,           
    c.Disc_Create_Date DiscCreateDate, c.Disc_Last_Mod_Date DiscLastModDate          
  FROM DiscItemBatch a         
  JOIN #tmp1 b ON a.DiscI_Item_Id = b.ItemId          
  JOIN Discounts c ON c.Disc_Code = a.DiscI_Disc_Code          
  AND a.DiscI_Item_Batch_Id = b.BatchId        
  WHERE CONVERT(date, a.DiscI_Start_Date) <= CONVERT(date, GETDATE())         
  AND CONVERT(date, a.DiscI_End_Date) >= CONVERT(date, GETDATE())         
  AND CONVERT(date, c.Disc_Start_Date) <= CONVERT(date, GETDATE())         
  AND CONVERT(date, c.Disc_End_Date) >= CONVERT(date, GETDATE())         
  AND CONVERT(date, a.DiscI_Start_Date) >= CONVERT(date, c.Disc_Start_Date)         
  AND CONVERT(date, a.DiscI_End_Date) <= CONVERT(date, c.Disc_End_Date)         
          
          
  SELECT a.TI_Item_Id ItemId, a.TI_Tax_Code TaxCode, a.TI_Batch_Id BatchId,           
  a.TI_Start_Date StartDate, a.TI_End_Date EndDate, a.TI_Remarks Remarks, a.TI_Create_Date CreateDate,           
  a.TI_Last_Mod_Date LastModDate          
  FROM TaxItemDtls a JOIN #TMP1 b ON a.TI_Item_Id = b.ItemId          
  AND a.TI_Batch_Id = b.BatchId          
  WHERE CONVERT(date, a.TI_Start_Date) <= CONVERT(date, GETDATE())         
  AND CONVERT(date, a.TI_End_Date) >= CONVERT(date, GETDATE())         
          
  SELECT distinct a.Tax_Code TaxCode, a.Tax_Name TaxName, a.Tax_Description TaxDescription, a.Tax_Kind_Id TaxKindId,          
  a.Tax_Type_Id TaxTypeId, a.Tax_Value TaxValue, a.Tax_Start_Date StartDate, a.Tax_End_Date EndDate,          
  a.Tax_Create_Date CreateDate, a.Tax_Last_Mod_Date LastModDate          
  FROM Tax a           
  JOIN TaxItemDtls b ON a.Tax_Code = b.TI_Tax_Code          
  JOIN #TMP1 c ON b.TI_Item_Id = c.ItemId AND b.TI_Batch_Id = c.BatchId          
  WHERE CONVERT(date, a.Tax_Start_Date) <= CONVERT(date, GETDATE())         
  AND CONVERT(date, a.Tax_End_Date) >= CONVERT(date, GETDATE())         
 END          
 ELSE
 BEGIN          
  SELECT b.IB_Item_Id ItemId, a.Item_Name ItemName, a.Item_Description ItemDescription,           
      b.IB_Batch_Id BatchId, b.IB_BarCode BarCode, b.IB_MRP MRP, b.IB_Qty_Procured QtyProcured,          
      b.IB_Price_Procured PriceProcured, b.IB_Date_Procured, b.IB_Date_Manuf DateManuf, b.IB_Date_Exp DateExp,           
      b.IB_Weight_Procured WeightProcured, b.IB_Weight_Available WeightAvailable,           
      b.IB_Price_Sell PriceSell, b.IB_Qty_Available QtyAvailable,          
      a.Item_Create_Date ItemCreateDate, a.Item_Last_Mod_Date ItemLastModDate,          
      b.IB_Create_Date BatchCreateDate, b.IB_Last_Mod_Date BatchLastModDate, b.IB_Manuf_Id ManufId, b.IB_Vendor_Id VendorId        
  INTO #TMP3          
  FROM Items a JOIN ItemBatch b ON a.Item_Id = b.IB_Item_Id          
  WHERE a.Item_Id = '1'          
            
  SET @SQL = 'SELECT b.IB_Item_Id ItemId, a.Item_Name ItemName, a.Item_Description ItemDescription,           
      b.IB_Batch_Id BatchId, b.IB_BarCode BarCode, b.IB_MRP MRP, b.IB_Qty_Procured QtyProcured,          
      b.IB_Price_Procured PriceProcured, b.IB_Date_Procured DateProcured, b.IB_Date_Manuf DateManuf, b.IB_Date_Exp DateExp,           
      b.IB_Weight_Procured WeightProcured, b.IB_Weight_Available WeightAvailable,           
      b.IB_Price_Sell PriceSell, b.IB_Qty_Available QtyAvailable,          
      a.Item_Create_Date ItemCreateDate, a.Item_Last_Mod_Date ItemLastModDate,          
      b.IB_Create_Date BatchCreateDate, b.IB_Last_Mod_Date BatchLastModDate, b.IB_Manuf_Id ManufId, b.IB_Vendor_Id VendorId        
     INTO #tmp4 FROM Items a JOIN ItemBatch b ON a.Item_Id = b.IB_Item_Id'          
            
  SET @SQL = @SQL + ' SELECT * FROM #tmp4'          
          
  /* Execute Transact-SQL String */          
  print @SQL          
  INSERT INTO #TMP3 EXECUTE(@SQL)          
            
  select * from #TMP3          
            
  SELECT a.DiscI_Item_Id ItemId, a.DiscI_Item_Batch_Id BatchId, a.DiscI_Disc_Code DiscCode,             
    a.DiscI_Start_Date DiscStartDate, a.DiscI_End_Date DiscEndDate,            
    a.DiscI_Remarks DiscRemarks, a.DiscI_Create_Date DiscCreateDate,           
    a.DiscI_Last_Mod_Date DiscLastModDate          
  FROM DiscItemBatch a         
  JOIN #tmp3 b ON a.DiscI_Item_Id = b.ItemId          
  JOIN Discounts c ON c.Disc_Code = a.DiscI_Disc_Code          
  AND a.DiscI_Item_Batch_Id = b.BatchId        
  WHERE CONVERT(date, a.DiscI_Start_Date) <= CONVERT(date, GETDATE())         
  AND CONVERT(date, a.DiscI_End_Date) >= CONVERT(date, GETDATE())         
  AND CONVERT(date, c.Disc_Start_Date) <= CONVERT(date, GETDATE())         
  AND CONVERT(date, c.Disc_End_Date) >= CONVERT(date, GETDATE())         
  AND CONVERT(date, a.DiscI_Start_Date) >= CONVERT(date, c.Disc_Start_Date)         
  AND CONVERT(date, a.DiscI_End_Date) <= CONVERT(date, c.Disc_End_Date)         
            
             
  SELECT distinct c.Disc_Code DiscCode, c.Disc_Name DiscName, c.Disc_Description DiscDescription,           
	c.Disc_Kind_Id DiscKindId, c.Disc_Type_Id DiscTypeId, c.Disc_Value DiscValue,           
    c.Disc_Start_Date DiscStartDate, c.Disc_End_Date DiscEndDate,           
    c.Disc_Create_Date DiscCreateDate, c.Disc_Last_Mod_Date DiscLastModDate          
  FROM DiscItemBatch a         
  JOIN #tmp3 b ON a.DiscI_Item_Id = b.ItemId          
  JOIN Discounts c ON c.Disc_Code = a.DiscI_Disc_Code          
  AND a.DiscI_Item_Batch_Id = b.BatchId        
  WHERE CONVERT(date, a.DiscI_Start_Date) <= CONVERT(date, GETDATE())         
  AND CONVERT(date, a.DiscI_End_Date) >= CONVERT(date, GETDATE())         
  AND CONVERT(date, c.Disc_Start_Date) <= CONVERT(date, GETDATE())         
  AND CONVERT(date, c.Disc_End_Date) >= CONVERT(date, GETDATE())         
  AND CONVERT(date, a.DiscI_Start_Date) >= CONVERT(date, c.Disc_Start_Date)         
  AND CONVERT(date, a.DiscI_End_Date) <= CONVERT(date, c.Disc_End_Date)         
          
          
  SELECT a.TI_Item_Id ItemId, a.TI_Tax_Code TaxCode, a.TI_Batch_Id BatchId,           
  a.TI_Start_Date StartDate, a.TI_End_Date EndDate, a.TI_Remarks Remarks, a.TI_Create_Date CreateDate,           
  a.TI_Last_Mod_Date LastModDate          
  FROM TaxItemDtls a JOIN #TMP3 b ON a.TI_Item_Id = b.ItemId          
  AND a.TI_Batch_Id = b.BatchId          
  WHERE CONVERT(date, a.TI_Start_Date) <= CONVERT(date, GETDATE())         
  AND CONVERT(date, a.TI_End_Date) >= CONVERT(date, GETDATE())         
          
  SELECT distinct a.Tax_Code TaxCode, a.Tax_Name TaxName, a.Tax_Description TaxDescription, a.Tax_Kind_Id TaxKindId,          
  a.Tax_Type_Id TaxTypeId, a.Tax_Value TaxValue, a.Tax_Start_Date StartDate, a.Tax_End_Date EndDate,          
  a.Tax_Create_Date CreateDate, a.Tax_Last_Mod_Date LastModDate          
  FROM Tax a           
  JOIN TaxItemDtls b ON a.Tax_Code = b.TI_Tax_Code          
  JOIN #TMP3 c ON b.TI_Item_Id = c.ItemId AND b.TI_Batch_Id = c.BatchId          
  WHERE CONVERT(date, a.Tax_Start_Date) <= CONVERT(date, GETDATE())         
  AND CONVERT(date, a.Tax_End_Date) >= CONVERT(date, GETDATE())         
 END          
END
GO
