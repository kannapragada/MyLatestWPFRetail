USE [NewAppDb]
GO
/****** Object:  StoredProcedure [dbo].[GetDiscountList]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================      
-- Author:  <Author,,Name>      
-- Create date: <Create Date,,>      
-- Description: <Description,,>      
-- =============================================      
CREATE PROCEDURE [dbo].[GetDiscountList]      
 @DiscCode  varchar(35)      
AS      
      
BEGIN      
 -- SET NOCOUNT ON added to prevent extra result sets from      
 -- interfering with SELECT statements.      
 SET NOCOUNT ON;      
  
 IF (@DiscCode = 'ALL')      
 BEGIN  
  SELECT Disc_Code, Disc_Name, Disc_Description, Disc_Kind_Id, Disc_Type_Id, Disc_Value, Disc_Remarks,    
  Disc_Start_Date, Disc_End_Date, Disc_Create_Date, Disc_Last_Mod_Date      
  INTO #Temp1      
  FROM Discounts       
       
  SELECT a.DiscI_Disc_Code, a.DiscI_Item_Id, c.IB_Batch_Id, a.DiscI_Start_Date,      
    a.DiscI_End_Date, a.DiscI_Remarks, a.DiscI_Create_Date, a.DiscI_Last_Mod_Date      
  INTO #Temp2      
  FROM DiscItemBatch a       
  JOIN Items b ON a.DiscI_Item_Id = b.Item_Id      
  JOIN ItemBatch c ON a.DiscI_Item_Id = c.IB_Item_Id      
        
  select * from #Temp1      
  select * from #Temp2      
 END  
 ELSE  
 BEGIN  
  SELECT Disc_Code, Disc_Name, Disc_Description, Disc_Kind_Id, Disc_Type_Id, Disc_Value, Disc_Remarks,    
  Disc_Start_Date, Disc_End_Date, Disc_Create_Date, Disc_Last_Mod_Date      
  INTO #Temp3      
  FROM Discounts       
  where Disc_Code like '%' + @DiscCode + '%'      
       
  SELECT a.DiscI_Disc_Code, a.DiscI_Item_Id, c.IB_Batch_Id, a.DiscI_Start_Date,      
    a.DiscI_End_Date, a.DiscI_Remarks, a.DiscI_Create_Date, a.DiscI_Last_Mod_Date      
  INTO #Temp4      
  FROM DiscItemBatch a       
  JOIN Items b ON a.DiscI_Item_Id = b.Item_Id      
  JOIN ItemBatch c ON a.DiscI_Item_Id = c.IB_Item_Id      
  where DiscI_Disc_Code IN (SELECT Disc_Code FROM #Temp3)      
        
  select * from #Temp3      
  select * from #Temp4      
 END  
       
END
GO
