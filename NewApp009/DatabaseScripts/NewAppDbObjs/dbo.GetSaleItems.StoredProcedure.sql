USE [NewAppDb]
GO
/****** Object:  StoredProcedure [dbo].[GetSaleItems]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
-- Author:  <Author,,Name>  
-- Create date: <Create Date,,>  
-- Description: <Description,,>  
-- =============================================  
CREATE PROCEDURE [dbo].[GetSaleItems]  
 @SaleId  varchar(15)  
AS  
  
BEGIN  
 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
 SET NOCOUNT ON;  
  
 select S.Sales_Id, SD.SD_Serial_Numb, SD.SD_Item_Id, SD.SD_Batch_Id, ib.IB_Manuf_Id, ib.IB_Vendor_Id, SD.SD_Qty_Sold, SD.SD_Weight, SD.SD_Item_Amt_Per_Unit,   
   SD.SD_Disc_Amt_Per_Unit, SD.SD_Total_Item_Amt, SD.SD_Final_Item_Amt, SD.SD_Create_Date, SD.SD_Last_Mod_Date   
 into #temp  
 from Sales S join SalesDetails SD on S.Sales_Id = SD.SD_Sales_Id  
 INNER JOIN ItemBatch AS ib ON sd.SD_Item_Id = ib.IB_Item_Id and sd.SD_Batch_Id = ib.IB_Batch_Id 
 where S.Sales_Id = @SaleId  
  
  
 select distinct * from #temp;  
  
END
GO
