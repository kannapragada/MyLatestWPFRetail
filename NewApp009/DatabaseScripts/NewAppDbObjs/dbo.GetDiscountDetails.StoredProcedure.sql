USE [NewAppDb]
GO
/****** Object:  StoredProcedure [dbo].[GetDiscountDetails]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
-- Author:  <Author,,Name>  
-- Create date: <Create Date,,>  
-- Description: <Description,,>  
-- =============================================  
CREATE PROCEDURE [dbo].[GetDiscountDetails]  
 @DiscCode  varchar(15)  
AS  
  
BEGIN  
 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
 SET NOCOUNT ON;  
  
 IF (@DiscCode = 'ALL')    
 BEGIN
	 SELECT Disc_Code, Disc_Name, Disc_Description, Disc_Kind_Id, Disc_Type_Id, Disc_Value,  
		Disc_Start_Date, Disc_End_Date, Disc_Create_Date, Disc_Last_Mod_Date  
	 FROM Discounts   
	  
	 SELECT DiscI_Item_Id, DiscI_Disc_Code, DiscI_Item_Batch_Id, DiscI_Start_Date,  
		  DiscI_End_Date, DiscI_Remarks Remarks, DiscI_Create_Date, DiscI_Last_Mod_Date  
	 FROM DiscItemBatch  
 END
 ELSE
 BEGIN
	 SELECT Disc_Code, Disc_Name, Disc_Description, Disc_Kind_Id, Disc_Type_Id, Disc_Value,  
		Disc_Start_Date, Disc_End_Date, Disc_Create_Date, Disc_Last_Mod_Date  
	 FROM Discounts   
	 where Disc_Code = @DiscCode  
	  
	 SELECT DiscI_Item_Id, DiscI_Disc_Code, DiscI_Item_Batch_Id, DiscI_Start_Date,  
		  DiscI_End_Date, DiscI_Remarks Remarks, DiscI_Create_Date, DiscI_Last_Mod_Date  
	 FROM DiscItemBatch  
	 where DiscI_Disc_Code = @DiscCode  
 END
END
GO
