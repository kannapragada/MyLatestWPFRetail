USE [NewAppDb]
GO
/****** Object:  StoredProcedure [dbo].[AddItemToDiscount]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
-- Author:  <Author,,Name>  
-- Create date: <Create Date,,>  
-- Description: <Description,,>  
-- =============================================  
CREATE PROCEDURE [dbo].[AddItemToDiscount]  
  @DiscI_Disc_Code  varchar(15),  
  @DiscI_Item_Id   varchar(15),  
  @DiscI_Item_Batch_Id varchar(15),  
  @DiscI_Start_Date  datetime,  
  @DiscI_End_Date   datetime,  
  @DiscI_Remarks    varchar(100),  
  @DiscI_Create_Date  datetime  
AS  
  
BEGIN  
 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
 SET NOCOUNT ON;  
  
 declare @sqlerror varchar(max)  
  
 SET @sqlerror = ''  
  
 BEGIN TRANSACTION  
  BEGIN TRY  
   insert into DiscItemBatch(DiscI_Item_Id, DiscI_Disc_Code, DiscI_Item_Batch_Id,  
    DiscI_Start_Date, DiscI_End_Date, DiscI_Remarks, DiscI_Create_Date, DiscI_Last_Mod_Date)  
   values (@DiscI_Item_Id,@DiscI_Disc_Code,@DiscI_Item_Batch_Id,@DiscI_Start_Date,  
     @DiscI_End_Date,@DiscI_Remarks,@DiscI_Create_Date, null)  
  
   COMMIT TRANSACTION  
  END TRY  
  
  BEGIN CATCH  
  -- Whoops, there was an error  
    BEGIN  
     ROLLBACK TRANSACTION  
     SET @sqlerror = 'Error while adding an Item to Discount. ' + ERROR_MESSAGE()  
     RAISERROR (@sqlerror, 11, 1)  
    END  
  END CATCH  
END
GO
