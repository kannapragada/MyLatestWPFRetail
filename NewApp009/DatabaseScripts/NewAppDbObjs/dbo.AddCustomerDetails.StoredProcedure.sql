USE [NewAppDb]
GO
/****** Object:  StoredProcedure [dbo].[AddCustomerDetails]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
-- Author:  Author,,Name>  
-- Create date: Create Date,,>  
-- Description: Description,,>  
-- =============================================  
CREATE PROCEDURE [dbo].[AddCustomerDetails]  
 @Cust_Id      varchar(15),  
 @Cust_Name      varchar(30),  
 @Cust_Status     varchar(10),  
 @Cust_DateofBirth    datetime,  
 @Cust_Gender     int,  
 @Cust_PresentAddress    varchar(100) = null,  
 @Cust_PresentCity    varchar(50),  
 @Cust_PresentPinCode   varchar(15) = null,  
 @Cust_PresentPhone    varchar(15) = null,  
 @Cust_Mobile    varchar(15) = null,  
 @Cust_EMailId    varchar(50) = null,  
 @Cust_IsPresentPermAddressSame int,
 @Cust_PermanentAddress    varchar(100) = null,  
 @Cust_PermanentCity    varchar(50),  
 @Cust_PermanentPinCode    varchar(15) = null,  
 @Cust_PermanentPhone   varchar(15) = null,  
 @Cust_IDProofTypeId    int,  
 @Cust_IDProofValue    varchar(50),  
 @Cust_RelationshipStartDate  datetime,  
 @Cust_AmtTobePaid    decimal(10, 2) = 0,  
 @Cust_AmtPaidYTD    decimal(10, 2) = 0,  
 @Cust_CreateDate    datetime,  
 @Cust_Picture     Image = null  
AS  
  
BEGIN  
 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
 SET NOCOUNT ON;  
   
 declare @sqlerror varchar(max)  
  
 SET @sqlerror = ''  
  
 BEGIN TRANSACTION  
  BEGIN TRY  
INSERT INTO Customer  
           (Cust_Id  
           ,Cust_Name  
           ,Cust_Status  
           ,Cust_DateofBirth  
           ,Cust_Gender  
           ,Cust_PresentAddress  
           ,Cust_PresentCity  
           ,Cust_PresentPinCode  
           ,Cust_PresentPhone  
           ,Cust_Mobile  
           ,Cust_EMailId
           ,Cust_IsPresentPermAddressSame
           ,Cust_PermanentAddress  
           ,Cust_PermanentCity  
           ,Cust_PermanentPinCode  
           ,Cust_PermanentPhone  
           ,Cust_IDProof_Type_Id  
           ,Cust_IDProof_Value  
           ,Cust_Relationship_StartDate  
           ,Cust_Relationship_EndDate  
           ,Cust_Amt_To_be_Paid  
           ,Cust_Amt_Paid_YTD  
           ,Cust_Create_Date  
           ,Cust_Last_Mod_Date  
           ,Cust_Picture)  
     VALUES  
           (@Cust_Id,  
           @Cust_Name,  
           @Cust_Status,  
           @Cust_DateofBirth,  
           @Cust_Gender,  
           @Cust_PresentAddress,  
           @Cust_PresentCity,  
           @Cust_PresentPinCode,  
           @Cust_PresentPhone,  
           @Cust_Mobile,  
           @Cust_EMailId,  
           @Cust_IsPresentPermAddressSame,
           @Cust_PermanentAddress,  
           @Cust_PermanentCity,  
           @Cust_PermanentPinCode,  
           @Cust_PermanentPhone,  
           @Cust_IDProofTypeId,  
           @Cust_IDProofValue,  
           @Cust_RelationshipStartDate,  
           NULL,  
           @Cust_AmtTobePaid,  
           @Cust_AmtPaidYTD,  
           @Cust_CreateDate,  
           NULL,  
           @Cust_Picture)  
  
   COMMIT TRANSACTION  
  END TRY  
  
  BEGIN CATCH  
  -- Whoops, there was an error  
    BEGIN  
     ROLLBACK TRANSACTION  
     SET @sqlerror = 'Error While Adding New Customer. ' + ERROR_MESSAGE()  
     RAISERROR (@sqlerror, 11, 1)  
    END  
  END CATCH  
END
GO
