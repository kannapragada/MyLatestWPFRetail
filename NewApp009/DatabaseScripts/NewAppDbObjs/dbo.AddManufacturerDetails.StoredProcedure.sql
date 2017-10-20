USE [NewAppDb]
GO
/****** Object:  StoredProcedure [dbo].[AddManufacturerDetails]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
-- Author:  Author,,Name>  
-- Create date: Create Date,,>  
-- Description: Description,,>  
-- =============================================  
CREATE PROCEDURE [dbo].[AddManufacturerDetails]  
 @Manuf_Id      varchar(15),  
 @Manuf_Name      varchar(30),  
 @Manuf_Status     varchar(10),  
 @Manuf_DateofBirth    datetime,  
 @Manuf_Gender     int,  
 @Manuf_PresentAddress    varchar(100) = null,  
 @Manuf_PresentCity    varchar(50),  
 @Manuf_PresentPinCode   varchar(15) = null,  
 @Manuf_PresentPhone    varchar(15) = null,  
 @Manuf_Mobile    varchar(15) = null,  
 @Manuf_EMailId    varchar(50) = null,
 @Manuf_IsPresentPermAddressSame int,  
 @Manuf_PermanentAddress    varchar(100) = null,  
 @Manuf_PermanentCity    varchar(50),  
 @Manuf_PermanentPinCode    varchar(15) = null,  
 @Manuf_PermanentPhone   varchar(15) = null,  
 @Manuf_IDProofTypeId    int,  
 @Manuf_IDProofValue    varchar(50),  
 @Manuf_RelationshipStartDate  datetime,  
 @Manuf_AmtTobePaid    decimal(10, 2) = 0,  
 @Manuf_AmtPaidYTD    decimal(10, 2) = 0,  
 @Manuf_CreateDate    datetime,  
 @Manuf_Picture     Image = null  
AS  
  
BEGIN  
 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
 SET NOCOUNT ON;  
   
 declare @sqlerror varchar(max)  
  
 SET @sqlerror = ''  
  
 BEGIN TRANSACTION  
  BEGIN TRY  
INSERT INTO Manufacturer  
           (Manuf_Id  
           ,Manuf_Name  
           ,Manuf_Status  
           ,Manuf_DateofBirth  
           ,Manuf_Gender  
           ,Manuf_PresentAddress  
           ,Manuf_PresentCity  
           ,Manuf_PresentPinCode  
           ,Manuf_PresentPhone  
           ,Manuf_Mobile  
           ,Manuf_EMailId
		   ,Manuf_IsPresentPermAddressSame
           ,Manuf_PermanentAddress  
           ,Manuf_PermanentCity  
           ,Manuf_PermanentPinCode  
           ,Manuf_PermanentPhone  
           ,Manuf_IDProof_Type_Id  
           ,Manuf_IDProof_Value  
           ,Manuf_Relationship_StartDate  
           ,Manuf_Relationship_EndDate  
           ,Manuf_Amt_To_be_Paid  
           ,Manuf_Amt_Paid_YTD  
           ,Manuf_Create_Date  
           ,Manuf_Last_Mod_Date  
           ,Manuf_Picture)  
     VALUES  
           (@Manuf_Id,  
           @Manuf_Name,  
           @Manuf_Status,  
           @Manuf_DateofBirth,  
           @Manuf_Gender,  
           @Manuf_PresentAddress,  
           @Manuf_PresentCity,  
           @Manuf_PresentPinCode,  
           @Manuf_PresentPhone,  
           @Manuf_Mobile,  
           @Manuf_EMailId,            
		   @Manuf_IsPresentPermAddressSame,
           @Manuf_PermanentAddress,  
           @Manuf_PermanentCity,  
           @Manuf_PermanentPinCode,  
           @Manuf_PermanentPhone,  
           @Manuf_IDProofTypeId,  
           @Manuf_IDProofValue,  
           @Manuf_RelationshipStartDate,  
           NULL,  
           @Manuf_AmtTobePaid,  
           @Manuf_AmtPaidYTD,  
           @Manuf_CreateDate,  
           NULL,  
           @Manuf_Picture)  
  
   COMMIT TRANSACTION  
  END TRY  
  
  BEGIN CATCH  
  -- Whoops, there was an error  
    BEGIN  
     ROLLBACK TRANSACTION  
     SET @sqlerror = 'Error While Adding New Manufacturer. ' + ERROR_MESSAGE()  
     RAISERROR (@sqlerror, 11, 1)  
    END  
  END CATCH  
END
GO
