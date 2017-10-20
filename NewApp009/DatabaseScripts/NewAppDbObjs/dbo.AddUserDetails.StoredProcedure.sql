USE [NewAppDb]
GO
/****** Object:  StoredProcedure [dbo].[AddUserDetails]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
-- Author:  Author,,Name>  
-- Create date: Create Date,,>  
-- Description: Description,,>  
-- =============================================  
CREATE PROCEDURE [dbo].[AddUserDetails]  
 @User_Id						varchar(15),  
 @User_Name						varchar(30),  
 @User_Status					varchar(10),  
 @User_DateofBirth				datetime,  
 @User_Gender					int,  
 @User_PresentAddress			varchar(100) = null,  
 @User_PresentCity				varchar(50),  
 @User_PresentPinCode			varchar(15) = null,  
 @User_PresentPhone				varchar(15) = null,  
 @User_Mobile					varchar(15) = null,  
 @User_EMailId					varchar(50) = null,    
 @User_IsPresentPermAddressSame int,
 @User_PermanentAddress			varchar(100) = null,  
 @User_PermanentCity			varchar(50),  
 @User_PermanentPinCode			varchar(15) = null,  
 @User_PermanentPhone			varchar(15) = null,  
 @User_IDProofTypeId			int,  
 @User_IDProofValue				varchar(50),  
 @User_RelationshipStartDate	datetime,  
 @User_AmtTobePaid				decimal(10, 2) = 0,  
 @User_AmtPaidYTD				decimal(10, 2) = 0,  
 @User_CreateDate				datetime,  
 @User_Picture					Image = null,
 @User_ThemeId					int  = 0,  
 @User_Pwd						varchar(15) = null,  
 @User_SecretQueryId			int  = 0,  
 @User_SecretAnswer				varchar(255) = null
AS  
  
BEGIN  
 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
 SET NOCOUNT ON;  
   
 declare @sqlerror varchar(max)  
  
 SET @sqlerror = ''  
 
 if @User_Pwd is null
	set @User_Pwd = 'password@12345'
  
 BEGIN TRANSACTION  
  BEGIN TRY  
INSERT INTO UserProfile 
           (User_Id  
           ,User_Name  
           ,User_Status  
           ,User_DateofBirth  
           ,User_Gender  
           ,User_PresentAddress  
           ,User_PresentCity  
           ,User_PresentPinCode  
           ,User_PresentPhone  
           ,User_Mobile  
           ,User_EMailId
		   ,User_IsPresentPermAddressSame
           ,User_PermanentAddress  
           ,User_PermanentCity  
           ,User_PermanentPinCode  
           ,User_PermanentPhone  
           ,User_IDProof_Type_Id  
           ,User_IDProof_Value  
           ,User_Relationship_StartDate  
           ,User_Relationship_EndDate  
           ,User_Amt_To_be_Paid  
           ,User_Amt_Paid_YTD  
           ,User_Create_Date  
           ,User_Last_Mod_Date  
           ,User_Picture
           ,User_ThemeId
           ,User_Pwd
           ,User_SecretQueryId
           ,User_SecretAnswer)  
     VALUES  
           (@User_Id,  
           @User_Name,  
           @User_Status,  
           @User_DateofBirth,  
           @User_Gender,  
           @User_PresentAddress,  
           @User_PresentCity,  
           @User_PresentPinCode,  
           @User_PresentPhone,  
           @User_Mobile,  
           @User_EMailId,    
		   @User_IsPresentPermAddressSame,
           @User_PermanentAddress,  
           @User_PermanentCity,  
           @User_PermanentPinCode,  
           @User_PermanentPhone,  
           @User_IDProofTypeId,  
           @User_IDProofValue,  
           @User_RelationshipStartDate,  
           NULL,  
           @User_AmtTobePaid,  
           @User_AmtPaidYTD,  
           @User_CreateDate,  
           NULL,  
           @User_Picture,
           @User_ThemeId,  
		   @User_Pwd,  
           @User_SecretQueryId,  
           @User_SecretAnswer)  
  
   COMMIT TRANSACTION  
  END TRY  
  
  BEGIN CATCH  
  -- Whoops, there was an error  
    BEGIN  
     ROLLBACK TRANSACTION  
     SET @sqlerror = 'Error While Adding New User. ' + ERROR_MESSAGE()  
     RAISERROR (@sqlerror, 11, 1)  
    END  
  END CATCH  
END
GO
