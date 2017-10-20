USE [NewAppDb]
GO
/****** Object:  StoredProcedure [dbo].[GetTaxList]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================    
-- Author:  <Author,,Name>    
-- Create date: <Create Date,,>    
-- Description: <Description,,>    
-- =============================================    
CREATE PROCEDURE [dbo].[GetTaxList]    
 @TaxCode  varchar(35)    
AS    
    
BEGIN    
 -- SET NOCOUNT ON added to prevent extra result sets from    
 -- interfering with SELECT statements.    
 SET NOCOUNT ON;    
    
 IF (@TaxCode = 'ALL')  
 BEGIN 
	 SELECT  Tax_Code, Tax_Name, Tax_Description, Tax_Kind_Id, Tax_Type_Id, Tax_Value, Tax_Start_Date,    
	   Tax_End_Date, Tax_Create_Date, Tax_Last_Mod_Date    
	 INTO #Temp1    
	 FROM Tax     
	    
	 SELECT a.TI_Tax_Code, a.TI_Item_Id, c.IB_Batch_Id, a.TI_Start_Date,    
		  a.TI_End_Date, a.TI_Remarks, a.TI_Create_Date, a.TI_Last_Mod_Date    
		INTO #Temp2    
	 FROM TaxItemDtls a     
	 JOIN Items b ON a.TI_Item_Id = b.Item_Id    
	 JOIN ItemBatch c ON a.TI_Item_Id = c.IB_Item_Id    
	 
	 select * from #Temp1    
	 select * from #Temp2    
 END
 ELSE
 BEGIN
	 SELECT  Tax_Code, Tax_Name, Tax_Description, Tax_Kind_Id, Tax_Type_Id, Tax_Value, Tax_Start_Date,    
	   Tax_End_Date, Tax_Create_Date, Tax_Last_Mod_Date    
	 INTO #Temp3    
	 FROM Tax     
	 where Tax_Code like '%' + @TaxCode + '%'    
	    
	 SELECT a.TI_Tax_Code, a.TI_Item_Id, c.IB_Batch_Id, a.TI_Start_Date,    
		  a.TI_End_Date, a.TI_Remarks, a.TI_Create_Date, a.TI_Last_Mod_Date    
		INTO #Temp4    
	 FROM TaxItemDtls a     
	 JOIN Items b ON a.TI_Item_Id = b.Item_Id    
	 JOIN ItemBatch c ON a.TI_Item_Id = c.IB_Item_Id    
	 where TI_Tax_Code IN (SELECT Tax_Code FROM #Temp3)  
	 
	 select * from #Temp3    
	 select * from #Temp4    
 END      
END
GO
