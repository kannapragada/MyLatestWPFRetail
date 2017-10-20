USE [NewAppDb]
GO
/****** Object:  StoredProcedure [dbo].[AddVendorDetails]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
-- Author:  Author,,Name>  
-- Create date: Create Date,,>  
-- Description: Description,,>  
-- =============================================  
CREATE PROCEDURE [dbo].[AddVendorDetails]  
 @Vendor_Id      varchar(15),  
 @Vendor_Name      varchar(30),  
 @Vendor_Status     varchar(10),  
 @Vendor_DateofBirth    datetime,  
 @Vendor_Gender     int,  
 @Vendor_PresentAddress    varchar(100) = null,  
 @Vendor_PresentCity    varchar(50),  
 @Vendor_PresentPinCode   varchar(15) = null,  
 @Vendor_PresentPhone    varchar(15) = null,  
 @Vendor_Mobile    varchar(15) = null,  
 @Vendor_EMailId    varchar(50) = null,    
 @Vendor_IsPresentPermAddressSame int,
 @Vendor_PermanentAddress    varchar(100) = null,  
 @Vendor_PermanentCity    varchar(50),  
 @Vendor_PermanentPinCode    varchar(15) = null,  
 @Vendor_PermanentPhone   varchar(15) = null,  
 @Vendor_IDProofTypeId    int,  
 @Vendor_IDProofValue    varchar(50),  
 @Vendor_RelationshipStartDate  datetime,  
 @Vendor_AmtTobePaid    decimal(10, 2) = 0,  
 @Vendor_AmtPaidYTD    decimal(10, 2) = 0,  
 @Vendor_CreateDate    datetime,  
 @Vendor_Picture     Image = null  
AS  
  
BEGIN  
 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
 SET NOCOUNT ON;  
   
 declare @sqlerror varchar(max)  
  
 SET @sqlerror = ''  
  
 BEGIN TRANSACTION  
  BEGIN TRY  
INSERT INTO Vendor  
           (Vendor_Id  
           ,Vendor_Name  
           ,Vendor_Status  
           ,Vendor_DateofBirth  
           ,Vendor_Gender  
           ,Vendor_PresentAddress  
           ,Vendor_PresentCity  
           ,Vendor_PresentPinCode  
           ,Vendor_PresentPhone  
           ,Vendor_Mobile  
           ,Vendor_EMailId
		   ,Vendor_IsPresentPermAddressSame
           ,Vendor_PermanentAddress  
           ,Vendor_PermanentCity  
           ,Vendor_PermanentPinCode  
           ,Vendor_PermanentPhone  
           ,Vendor_IDProof_Type_Id  
           ,Vendor_IDProof_Value  
           ,Vendor_Relationship_StartDate  
           ,Vendor_Relationship_EndDate  
           ,Vendor_Amt_To_be_Paid  
           ,Vendor_Amt_Paid_YTD  
           ,Vendor_Create_Date  
           ,Vendor_Last_Mod_Date  
           ,Vendor_Picture)  
     VALUES  
           (@Vendor_Id,  
           @Vendor_Name,  
           @Vendor_Status,  
           @Vendor_DateofBirth,  
           @Vendor_Gender,  
           @Vendor_PresentAddress,  
           @Vendor_PresentCity,  
           @Vendor_PresentPinCode,  
           @Vendor_PresentPhone,  
           @Vendor_Mobile,  
           @Vendor_EMailId,    
		   @Vendor_IsPresentPermAddressSame,
           @Vendor_PermanentAddress,  
           @Vendor_PermanentCity,  
           @Vendor_PermanentPinCode,  
           @Vendor_PermanentPhone,  
           @Vendor_IDProofTypeId,  
           @Vendor_IDProofValue,  
           @Vendor_RelationshipStartDate,  
           NULL,  
           @Vendor_AmtTobePaid,  
           @Vendor_AmtPaidYTD,  
           @Vendor_CreateDate,  
           NULL,  
           @Vendor_Picture)  
  
   COMMIT TRANSACTION  
  END TRY  
  
  BEGIN CATCH  
  -- Whoops, there was an error  
    BEGIN  
     ROLLBACK TRANSACTION  
     SET @sqlerror = 'Error While Adding New Vendor. ' + ERROR_MESSAGE()  
     RAISERROR (@sqlerror, 11, 1)  
    END  
  END CATCH  
END
GO
