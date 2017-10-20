USE [NewAppDb]
GO
/****** Object:  StoredProcedure [dbo].[SearchStoreItems]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================        
-- Author:  <Author,,Name>        
-- Create date: <Create Date,,>        
-- Description: <Description,,>        
-- =============================================        
CREATE PROCEDURE [dbo].[SearchStoreItems]        
 @Item_Id    varchar(35) = null,        
 @Item_Name    varchar(30) = null,        
 @IB_Batch_Id   varchar(15) = null,        
 @IB_Vendor_Id   varchar(15) = null,        
 @IB_Start_Date_Manuf datetime = null,        
 @IB_End_Date_Manuf  datetime = null,        
 @IB_Start_Date_Exp  datetime = null,        
 @IB_End_Date_Exp  datetime = null        
AS        
        
BEGIN        
 -- SET NOCOUNT ON added to prevent extra result sets from        
 -- interfering with SELECT statements.        
 SET NOCOUNT ON;        
         
         
 declare @SQL varchar(max)        
 declare @SQL1 varchar(max)        
 declare @tmpstr varchar(max)        
        
 IF (@Item_Id = 'ALL')        
 BEGIN        
  SELECT b.IB_Item_Id ItemId, a.Item_Name ItemName, a.Item_Description ItemDescription,     
      b.IB_Batch_Id BatchId, b.IB_BarCode BarCode, b.IB_MRP MRP, b.IB_Qty_Procured QtyProcured,    
      b.IB_Price_Procured PriceProcured, b.IB_Date_Procured, b.IB_Date_Manuf DateManuf, b.IB_Date_Exp DateExp,     
      b.IB_Weight_Procured WeightProcured, b.IB_Weight_Available WeightAvailable,     
      b.IB_Price_Sell PriceSell, b.IB_Qty_Available QtyAvailable,    
      a.Item_Create_Date ItemCreateDate, a.Item_Last_Mod_Date ItemLastModDate,    
      b.IB_Create_Date BatchCreateDate, b.IB_Last_Mod_Date BatchLastModDate, b.IB_Manuf_Id ManufId, b.IB_Vendor_Id VendorId         
  INTO #tmp1        
  FROM Items a JOIN ItemBatch b ON a.Item_Id = b.IB_Item_Id        
          
          
  SELECT * FROM #tmp1        
         
  SELECT a.DiscI_Item_Id ItemId, a.DiscI_Item_Batch_Id BatchId, a.DiscI_Disc_Code DiscCode,           
    a.DiscI_Start_Date DiscStartDate, a.DiscI_End_Date DiscEndDate,          
    a.DiscI_Remarks DiscRemarks, a.DiscI_Create_Date DiscCreateDate,         
    a.DiscI_Last_Mod_Date DiscLastModDate        
  FROM DiscItemBatch a JOIN #tmp1 b ON a.DiscI_Item_Id = b.ItemId        
  AND a.DiscI_Item_Batch_Id = b.BatchId        
        
           
  SELECT a.Disc_Code DiscCode, a.Disc_Name DiscName, a.Disc_Description DiscDescription,         
    a.Disc_Type_Id DiscTypeId, a.Disc_Value DiscValue,         
    a.Disc_Start_Date DiscStartDate, a.Disc_End_Date DiscEndDate,         
    a.Disc_Create_Date DiscCreateDate, a.Disc_Last_Mod_Date DiscLastModDate        
  FROM Discounts a         
  JOIN DiscItemBatch b ON a.Disc_Code = b.DiscI_Disc_Code        
  JOIN #tmp1 c ON b.DiscI_Item_Id = c.ItemId AND b.DiscI_Item_Batch_Id = c.BatchId        
        
  SELECT a.TI_Item_Id ItemId, a.TI_Tax_Code TaxCode, a.TI_Batch_Id BatchId,         
  a.TI_Start_Date StartDate, a.TI_End_Date EndDate, a.TI_Remarks Remarks, a.TI_Create_Date CreateDate,         
  a.TI_Last_Mod_Date LastModDate        
  FROM TaxItemDtls a JOIN #tmp1 b ON a.TI_Item_Id = b.ItemId        
  AND a.TI_Batch_Id = b.BatchId        
  
  SELECT a.Tax_Code TaxCode, a.Tax_Name TaxName, a.Tax_Description TaxDescription, a.Tax_Kind_Id TaxKindId,        
  a.Tax_Type_Id TaxTypeId, a.Tax_Value TaxValue, a.Tax_Start_Date StartDate, a.Tax_End_Date EndDate,        
  a.Tax_Create_Date CreateDate, a.Tax_Last_Mod_Date LastModDate        
  FROM Tax a         
  JOIN TaxItemDtls b ON a.Tax_Code = b.TI_Tax_Code        
  JOIN #tmp1 c ON b.TI_Item_Id = c.ItemId AND b.TI_Batch_Id = c.BatchId        
          
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
  INTO #TMP        
  FROM Items a JOIN ItemBatch b ON a.Item_Id = b.IB_Item_Id        
  WHERE a.Item_Id = '1'        
          
  SET @SQL = 'SELECT b.IB_Item_Id ItemId, a.Item_Name ItemName, a.Item_Description ItemDescription,     
      b.IB_Batch_Id BatchId, b.IB_BarCode BarCode, b.IB_MRP MRP, b.IB_Qty_Procured QtyProcured,    
      b.IB_Price_Procured PriceProcured, b.IB_Date_Procured, b.IB_Date_Manuf DateManuf, b.IB_Date_Exp DateExp,     
      b.IB_Weight_Procured WeightProcured, b.IB_Weight_Available WeightAvailable,     
      b.IB_Price_Sell PriceSell, b.IB_Qty_Available QtyAvailable,    
      a.Item_Create_Date ItemCreateDate, a.Item_Last_Mod_Date ItemLastModDate,    
      b.IB_Create_Date BatchCreateDate, b.IB_Last_Mod_Date BatchLastModDate, b.IB_Manuf_Id ManufId, b.IB_Vendor_Id VendorId         
     INTO #tmp2 FROM Items a JOIN ItemBatch b ON a.Item_Id = b.IB_Item_Id'        
          
  set @tmpstr = ''        
          
  IF (@Item_Id IS NOT NULL AND LEN(@Item_Id) > 0)        
  BEGIN        
   SET @tmpstr = ' WHERE (a.Item_Id like ''%' + CAST(@Item_Id AS NVARCHAR(15)) + '%''' + ')'        
  END        
          
  IF (@Item_Name IS NOT NULL AND LEN(@Item_Name) > 0)        
  BEGIN        
   if (LEN(@tmpstr) > 0)        
    SET @tmpstr = @tmpstr + ' AND '        
   else        
    SET @tmpstr = ' WHERE '        
          
    set @tmpstr = @tmpstr + '(a.Item_Name like ''%' + CAST(@Item_Name AS NVARCHAR(15)) + '%''' + ')'        
  END        
          
  IF (@IB_Batch_Id IS NOT NULL AND LEN(@IB_Batch_Id) > 0)        
  BEGIN        
   if (LEN(@tmpstr) > 0)        
    SET @tmpstr = @tmpstr + ' AND '        
   else        
    SET @tmpstr = ' WHERE '        
          
   set @tmpstr = @tmpstr + '(b.IB_Batch_Id like ''%' + CAST(@IB_Batch_Id AS NVARCHAR(15)) + '%''' + ')'        
  END        
          
  IF (@IB_Vendor_Id IS NOT NULL AND LEN(@IB_Vendor_Id) > 0)        
  BEGIN        
   if (LEN(@tmpstr) > 0)        
    SET @tmpstr = @tmpstr + ' AND '        
   else        
    SET @tmpstr = ' WHERE '        
          
   set @tmpstr = @tmpstr + '(b.@IB_Vendor_Id like ''%' + CAST(@IB_Vendor_Id AS NVARCHAR(15)) + '%''' + ')'        
  END        
          
  IF ((IsDate(@IB_Start_Date_Manuf) = 1) AND (IsDate(@IB_End_Date_Manuf) = 1))        
  BEGIN        
   if (LEN(@tmpstr) > 0)        
    SET @tmpstr = @tmpstr + ' AND '        
   else        
    SET @tmpstr = ' WHERE '        
        
   set @tmpstr = @tmpstr + '(b.IB_Date_Manuf BETWEEN ''' + CONVERT(varchar(30), @IB_Start_Date_Manuf, 126) + ''' AND ''' + CONVERT(varchar(30), @IB_End_Date_Manuf, 126) + ''')'        
  END        
        
  IF ((IsDate(@IB_Start_Date_Exp) = 1) AND (IsDate(@IB_End_Date_Exp) = 1))        
  BEGIN        
   if (@tmpstr <> '')        
    SET @tmpstr = @tmpstr + ' AND '        
   else        
   SET @tmpstr = ' WHERE '        
          
   set @tmpstr = @tmpstr + '(b.IB_Date_Exp BETWEEN ''' + CONVERT(varchar(30), @IB_Start_Date_Exp, 126) + ''' AND ''' + CONVERT(varchar(30), @IB_End_Date_Exp, 126) + ''')'        
  END        
        
  SET @SQL = @SQL + @tmpstr        
  SET @SQL = @SQL + ' SELECT * FROM #tmp2'        
        
  /* Execute Transact-SQL String */        
  print @SQL        
  INSERT INTO #TMP EXECUTE(@SQL)        
          
  select * from #TMP        
        
  SELECT a.DiscI_Item_Id ItemId, a.DiscI_Item_Batch_Id BatchId, a.DiscI_Disc_Code DiscCode,           
    a.DiscI_Start_Date DiscStartDate, a.DiscI_End_Date DiscEndDate,          
    a.DiscI_Remarks DiscRemarks, a.DiscI_Create_Date DiscCreateDate,         
    a.DiscI_Last_Mod_Date DiscLastModDate        
  FROM DiscItemBatch a JOIN #TMP b ON a.DiscI_Item_Id = b.ItemId        
  AND a.DiscI_Item_Batch_Id = b.BatchId        
        
           
  SELECT a.Disc_Code DiscCode, a.Disc_Name DiscName, a.Disc_Description DiscDescription,         
    a.Disc_Type_Id DiscTypeId, a.Disc_Value DiscValue,         
    a.Disc_Start_Date DiscStartDate, a.Disc_End_Date DiscEndDate,         
    a.Disc_Create_Date DiscCreateDate, a.Disc_Last_Mod_Date DiscLastModDate        
  FROM Discounts a         
  JOIN DiscItemBatch b ON a.Disc_Code = b.DiscI_Disc_Code        
  JOIN #TMP c ON b.DiscI_Item_Id = c.ItemId AND b.DiscI_Item_Batch_Id = c.BatchId        
        
        
  SELECT a.TI_Item_Id ItemId, a.TI_Tax_Code TaxCode, a.TI_Batch_Id BatchId,         
  a.TI_Start_Date StartDate, a.TI_End_Date EndDate, a.TI_Remarks Remarks, a.TI_Create_Date CreateDate,         
  a.TI_Last_Mod_Date LastModDate        
  FROM TaxItemDtls a JOIN #TMP b ON a.TI_Item_Id = b.ItemId        
  AND a.TI_Batch_Id = b.BatchId        
  
  SELECT a.Tax_Code TaxCode, a.Tax_Name TaxName, a.Tax_Description TaxDescription, a.Tax_Kind_Id TaxKindId,        
  a.Tax_Type_Id TaxTypeId, a.Tax_Value TaxValue, a.Tax_Start_Date StartDate, a.Tax_End_Date EndDate,        
  a.Tax_Create_Date CreateDate, a.Tax_Last_Mod_Date LastModDate        
  FROM Tax a         
  JOIN TaxItemDtls b ON a.Tax_Code = b.TI_Tax_Code        
  JOIN #TMP c ON b.TI_Item_Id = c.ItemId AND b.TI_Batch_Id = c.BatchId        
        
 END        
END
GO
