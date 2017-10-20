USE [NewAppDb]
GO
/****** Object:  StoredProcedure [dbo].[AddItemToTaxCode]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
-- Author:  <Author,,Name>  
-- Create date: <Create Date,,>  
-- Description: <Description,,>  
-- =============================================  
CREATE PROCEDURE [dbo].[AddItemToTaxCode]  
  @Tax_Code  varchar(15),  
  @Item_Id   varchar(15),  
  @Batch_Id varchar(15),  
  @Remarks    varchar(100),  
  @Start_Date  datetime,  
  @End_Date   datetime,  
  @Create_Date  datetime  
AS  
  
BEGIN  
 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
 SET NOCOUNT ON;  
  
 declare @sqlerror varchar(max)  
  
 SET @sqlerror = ''  
  
 BEGIN TRANSACTION  
  BEGIN TRY  
   insert into TaxItemDtls(TI_Item_Id, TI_Tax_Code, TI_Batch_Id,  
    TI_Start_Date, TI_End_Date, TI_Remarks, TI_Create_Date, TI_Last_Mod_Date)  
   values (@Item_Id, @Tax_Code, @Batch_Id, @Start_Date, @End_Date, @Remarks, @Create_Date, null)  
  
   COMMIT TRANSACTION  
  END TRY  
  
  BEGIN CATCH  
  -- Whoops, there was an error  
    BEGIN  
     ROLLBACK TRANSACTION  
     SET @sqlerror = 'Error while adding an Item to Tax Code. ' + ERROR_MESSAGE()  
     RAISERROR (@sqlerror, 11, 1)  
    END  
  END CATCH  
END
GO
