USE [NewAppDb]
GO
/****** Object:  StoredProcedure [dbo].[AddNewDiscount]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
-- Author:  <Author,,Name>  
-- Create date: <Create Date,,>  
-- Description: <Description,,>  
-- =============================================  
CREATE PROCEDURE [dbo].[AddNewDiscount]  
  @Disc_Code    varchar(15),  
  @Disc_Name    varchar(50),  
  @Disc_Description  varchar(100),  
  @Disc_Kind_Id   int,  
  @Disc_Type_Id   int,  
  @Disc_Value    decimal(10, 2),  
  @Disc_Start_Date  datetime,  
  @Disc_End_Date   datetime,  
  @Disc_Remarks   varchar(100),
  @Disc_Create_Date  datetime,  
  @Disc_Last_Mod_Date  datetime  
AS  
  
BEGIN  
 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
 SET NOCOUNT ON;  
  
 declare @sqlerror varchar(max)  
  
 SET @sqlerror = ''  
  
 BEGIN TRANSACTION  
  BEGIN TRY  
   insert into Discounts(Disc_Code, Disc_Name, Disc_Description, Disc_Kind_Id, Disc_Type_Id,   
   Disc_Value, Disc_Start_Date, Disc_End_Date, Disc_Create_Date, Disc_Last_Mod_Date, Disc_Remarks)  
   values (@Disc_Code, @Disc_Name, @Disc_Description, @Disc_Kind_Id, @Disc_Type_Id, @Disc_Value, @Disc_Start_Date,  
   @Disc_End_Date, @Disc_Create_Date, null, @Disc_Remarks)  
  
   COMMIT TRANSACTION  
  END TRY  
  
  BEGIN CATCH  
  -- Whoops, there was an error  
    BEGIN  
     ROLLBACK TRANSACTION  
     SET @sqlerror = 'Error while adding a New Discount. ' + ERROR_MESSAGE()  
     RAISERROR (@sqlerror, 11, 1)  
    END  
  END CATCH  
END
GO
