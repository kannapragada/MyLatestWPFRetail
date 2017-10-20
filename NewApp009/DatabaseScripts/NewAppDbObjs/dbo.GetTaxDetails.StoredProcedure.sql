USE [NewAppDb]
GO
/****** Object:  StoredProcedure [dbo].[GetTaxDetails]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================    
-- Author:  <Author,,Name>    
-- Create date: <Create Date,,>    
-- Description: <Description,,>    
-- =============================================    
CREATE PROCEDURE [dbo].[GetTaxDetails]    
 @TaxCode  varchar(15)    
AS    
    
BEGIN    
 -- SET NOCOUNT ON added to prevent extra result sets from    
 -- interfering with SELECT statements.    
 SET NOCOUNT ON;    
    
  IF (@TaxCode = 'ALL')
  BEGIN
	  SELECT Tax_Code TaxCode,Tax_Name TaxName,Tax_Description TaxDescription,Tax_Kind_Id TaxKindId,  
	  Tax_Type_Id TaxTypeId,Tax_Value TaxValue,Tax_Start_Date TaxStartDate,Tax_End_Date TaxEndDate,    
	  Tax_Create_Date TaxCreateDate,Tax_Last_Mod_Date TaxLastModDate    
	  FROM Tax order by  Tax_Code   
	    
	  SELECT TI_Item_Id ItemId, TI_Tax_Code TaxCode, TI_Batch_Id BatchId,     
	  TI_Start_Date StartDate, TI_End_Date EndDate, TI_Remarks Remarks, TI_Create_Date CreateDate,     
	  TI_Last_Mod_Date LastModDate    
	  FROM TaxItemDtls order by  TI_Tax_Code       
  END
  ELSE
  BEGIN
	  SELECT Tax_Code TaxCode,Tax_Name TaxName,Tax_Description TaxDescription,Tax_Kind_Id TaxKindId,  
	  Tax_Type_Id TaxTypeId,Tax_Value TaxValue,Tax_Start_Date TaxStartDate,Tax_End_Date TaxEndDate,    
	  Tax_Create_Date TaxCreateDate,Tax_Last_Mod_Date TaxLastModDate    
	  FROM Tax WHERE Tax_Code = @TaxCode    
	    
	  SELECT TI_Item_Id ItemId, TI_Tax_Code TaxCode, TI_Batch_Id BatchId,     
	  TI_Start_Date StartDate, TI_End_Date EndDate, TI_Remarks Remarks, TI_Create_Date CreateDate,     
	  TI_Last_Mod_Date LastModDate    
	  FROM TaxItemDtls WHERE TI_Tax_Code = @TaxCode      
  END
END
GO
