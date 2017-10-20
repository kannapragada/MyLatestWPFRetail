USE [NewAppDb]
GO
/****** Object:  StoredProcedure [dbo].[GetUsers]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================    
-- Author:  <Author,,Name>    
-- Create date: <Create Date,,>    
-- Description: <Description,,>    
-- =============================================    
CREATE PROCEDURE [dbo].[GetUsers]    
 @User_Id    varchar(15) = null,  
 @User_Name    varchar(50) = null  
AS    
    
BEGIN    
 -- SET NOCOUNT ON added to prevent extra result sets from    
 -- interfering with SELECT statements.    
 SET NOCOUNT ON;    
   
 declare @SQL varchar(max)  
 declare @SQL1 varchar(max)  
 declare @tmpstr varchar(max)  
  
  
  IF (@User_Id IS NOT NULL AND LEN(@User_Id) > 0)  
  BEGIN  
   SET @tmpstr = ' WHERE (User_Id like ''%' + CAST(@User_Id AS NVARCHAR(15)) + '%''' + ')'  
  END  
    
  IF (@User_Name IS NOT NULL AND LEN(@User_Name) > 0)  
  BEGIN  
   if (LEN(@tmpstr) > 0)  
    SET @tmpstr = @tmpstr + ' OR '  
   else  
    SET @tmpstr = ' WHERE '  
    
    set @tmpstr = @tmpstr + '(User_Name like ''%' + CAST(@User_Name AS NVARCHAR(15)) + '%''' + ')'  
  END  
    
  SET @SQL = 'SELECT User_Id UserId
      ,User_Name UserName
      ,User_Status UserStatus
      ,User_DateofBirth UserDateofBirth
      ,User_Gender UserGender
      ,User_PresentAddress UserPresentAddress
      ,User_PresentCity UserPresentCity
      ,User_PresentPinCode UserPresentPinCode
      ,User_PresentPhone UserPresentPhone
      ,User_Mobile UserPresentMobile
      ,User_EMailId UserEMailId
	  ,User_IsPresentPermAddressSame UserIsPresentPermAddressSame
      ,User_PermanentAddress UserPermanentAddress
      ,User_PermanentCity UserPermanentCity
      ,User_PermanentPinCode UserPermanentPinCode
      ,User_PermanentPhone UserPermanentPhone
      ,User_IDProof_Type_Id UserIDProofTypeId
      ,User_IDProof_Value UserIDProofValue
      ,User_Relationship_StartDate MembershipStartDate
      ,User_Relationship_EndDate MembershipExpiryDate
      ,User_Amt_To_be_Paid UserAmtTobePaid
      ,User_Amt_Paid_YTD UserAmtPaidYTD
      ,User_Create_Date UserCreateDate
      ,User_Last_Mod_Date UserLastModDate
      ,User_Picture UserPicture
      ,User_ThemeId	UserThemeId
      ,User_Pwd	UserPassword
      ,User_SecretQueryId UserSecretQueryId
      ,User_SecretAnswer UserSecretAnswer
  FROM Userprofile'  + @tmpstr     


  print @SQL  
  EXECUTE(@SQL)  
    
END
GO
