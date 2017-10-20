USE [NewAppDb]
GO
/****** Object:  StoredProcedure [dbo].[GetStoreItemBatch]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================        
-- Author:  <Author,,Name>        
-- Create date: <Create Date,,>        
-- Description: <Description,,>        
-- =============================================        
CREATE PROCEDURE [dbo].[GetStoreItemBatch]        
 @Item_Id  varchar(35),          
 @IB_Batch_Id varchar(15)        
AS        
        
BEGIN        
 -- SET NOCOUNT ON added to prevent extra result sets from        
 -- interfering with SELECT statements.        
 SET NOCOUNT ON;        
        
  SELECT b.IB_Item_Id ItemId, a.Item_Name ItemName, a.Item_Description ItemDescription,         
  b.IB_Batch_Id BatchId, b.IB_Manuf_Id ManufacturerId, b.IB_Qty_Procured QtyProcured, b.IB_Price_Procured PriceProcured,   
  b.IB_MRP MRP, b.IB_Date_Procured, b.IB_Date_Manuf DateManuf, b.IB_Date_Exp DateExp,         
  b.IB_Weight_Procured WeightProcured, b.IB_Weight_Available WeightAvailable, b.IB_BarCode BarCode,         
  b.IB_Price_Sell PriceSell, b.IB_Qty_Available QtyAvailable,        
  a.Item_Create_Date ItemCreateDate, a.Item_Last_Mod_Date ItemLastModDate,        
  b.IB_Create_Date BatchCreateDate, b.IB_Last_Mod_Date BatchLastModDate, b.IB_Vendor_Id VendorId  
  FROM Items a   
  JOIN ItemBatch b ON a.Item_Id = b.IB_Item_Id        
  WHERE a.Item_Id = @Item_Id   
  AND b.IB_Batch_Id = @IB_Batch_Id        
  
  SELECT DiscI_Item_Id ItemId, DiscI_Item_Batch_Id BatchId, DiscI_Disc_Code DiscCode,         
  DiscI_Start_Date DiscStartDate, DiscI_End_Date DiscEndDate,        
  DiscI_Remarks DiscRemarks, DiscI_Create_Date DiscCreateDate,       
  DiscI_Last_Mod_Date DiscLastModDate      
  FROM DiscItemBatch      
  WHERE DiscI_Item_Id = @Item_Id        
  AND DiscI_Item_Batch_Id = @IB_Batch_Id        
  
  SELECT Disc_Code DiscCode, Disc_Name DiscName, Disc_Description DiscDescription,       
  Disc_Kind_Id DiscKindId, Disc_Type_Id DiscTypeId, Disc_Value DiscValue, Disc_Remarks DiscRemarks,    
  Disc_Start_Date DiscStartDate, Disc_End_Date DiscEndDate,       
  Disc_Create_Date DiscCreateDate, Disc_Last_Mod_Date DiscLastModDate      
  FROM Discounts a       
  JOIN DiscItemBatch b ON a.Disc_Code = b.DiscI_Disc_Code      
  JOIN Items c ON b.DiscI_Item_Id = c.Item_Id      
  WHERE c.Item_Id  = @Item_Id        
  AND b.DiscI_Item_Batch_Id = @IB_Batch_Id     
  
  SELECT TI_Item_Id ItemId, TI_Tax_Code TaxCode, TI_Batch_Id BatchId,   
  TI_Start_Date StartDate, TI_End_Date EndDate, TI_Remarks Remarks, TI_Create_Date CreateDate,   
  TI_Last_Mod_Date LastModDate  
  FROM TaxItemDtls  
  WHERE TI_Item_Id = @Item_Id        
  AND TI_Batch_Id = @IB_Batch_Id   
  
  SELECT Tax_Code TaxCode,Tax_Name TaxName,Tax_Description TaxDescription,Tax_Kind_Id TaxKindId,  
  Tax_Type_Id TaxTypeId,Tax_Value TaxValue,Tax_Start_Date StartDate,Tax_End_Date EndDate,  
  Tax_Create_Date CreateDate,Tax_Last_Mod_Date LastModDate  
  FROM Tax a   
  JOIN TaxItemDtls b ON a.Tax_Code = b.TI_Tax_Code  
  JOIN Items c ON c.Item_Id = @Item_Id AND b.TI_Batch_Id = @IB_Batch_Id     
        
END
GO
