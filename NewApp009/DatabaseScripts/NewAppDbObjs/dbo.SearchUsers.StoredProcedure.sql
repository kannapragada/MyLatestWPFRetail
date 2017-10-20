USE [NewAppDb]
GO
/****** Object:  StoredProcedure [dbo].[SearchUsers]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================        
-- Author:  <Author,,Name>        
-- Create date: <Create Date,,>        
-- Description: <Description,,>        
-- =============================================        
CREATE PROCEDURE [dbo].[SearchUsers]        
 @User_Id							varchar(15) = null,      
 @User_Name							varchar(50) = null,      
 @User_Address						varchar(100) = null,      
 @User_IDProofTypeId				int = null,      
 @User_IDProofValue					varchar(30) = null,      
 @User_FromDateofBirth				datetime = null,      
 @User_ToDateofBirth				datetime = null,      
 @User_FromDateofStartRelationship	datetime = null,      
 @User_ToDateofStartRelationship		datetime = null,      
 @User_FromDateofExpiryRelationship	datetime = null,      
 @User_ToDateofExpiryRelationship		datetime = null      
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
    SET @tmpstr = @tmpstr + ' AND '      
   else      
    SET @tmpstr = ' WHERE '      
        
    set @tmpstr = @tmpstr + '(User_Name like ''%' + CAST(@User_Name AS NVARCHAR(15)) + '%''' + ')'      
  END      
        
  IF (@User_Address IS NOT NULL AND LEN(@User_Address) > 0)      
  BEGIN      
   if (LEN(@tmpstr) > 0)      
    SET @tmpstr = @tmpstr + ' AND '      
   else      
    SET @tmpstr = ' WHERE '      
        
    set @tmpstr = @tmpstr + '(User_Address like ''%' + CAST(@User_Name AS NVARCHAR(15)) + '%''' + ')'      
  END      
        
  IF (@User_IDProofTypeId IS NOT NULL)      
  BEGIN      
   IF ((@User_IDProofValue IS NOT NULL AND LEN(@User_IDProofValue) > 0))      
   BEGIN      
    if (LEN(@tmpstr) > 0)      
     SET @tmpstr = @tmpstr + ' AND '      
    else      
     SET @tmpstr = ' WHERE '      
         
     set @tmpstr = @tmpstr + '(User_IDProof_Value like ''%' + CAST(@User_IDProofValue AS NVARCHAR(15)) + '%''' + ')'      
   END      
  END      
        
  IF ((IsDate(@User_FromDateofBirth) = 1) AND (IsDate(@User_ToDateofBirth) = 1))      
  BEGIN      
   if (LEN(@tmpstr) > 0)      
    SET @tmpstr = @tmpstr + ' AND '      
   else      
    SET @tmpstr = ' WHERE '      
      
   set @tmpstr = @tmpstr + '(User_DateofBirth BETWEEN ''' + CONVERT(varchar(30), @User_FromDateofBirth, 126) + ''' AND ''' + CONVERT(varchar(30), @User_ToDateofBirth, 126) + ''')'      
  END      
        
  IF ((IsDate(@User_FromDateofStartRelationship) = 1) AND (IsDate(@User_FromDateofStartRelationship) = 1))      
  BEGIN      
   if (LEN(@tmpstr) > 0)      
    SET @tmpstr = @tmpstr + ' AND '      
   else      
    SET @tmpstr = ' WHERE '      
      
   set @tmpstr = @tmpstr + '(User_Relationship_StartDate BETWEEN ''' + CONVERT(varchar(30), @User_FromDateofStartRelationship, 126) + ''' AND ''' + CONVERT(varchar(30), @User_FromDateofStartRelationship, 126) + ''')'      
  END      
        
  IF ((IsDate(@User_FromDateofExpiryRelationship) = 1) AND (IsDate(@User_ToDateofExpiryRelationship) = 1))      
  BEGIN      
   if (LEN(@tmpstr) > 0)      
    SET @tmpstr = @tmpstr + ' AND '      
   else      
    SET @tmpstr = ' WHERE '      
      
   set @tmpstr = @tmpstr + '(User_Relationship_ExpiryDate BETWEEN ''' + CONVERT(varchar(30), @User_FromDateofExpiryRelationship, 126) + ''' AND ''' + CONVERT(varchar(30), @User_ToDateofExpiryRelationship, 126) + ''')'      
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
      ,User_Relationship_StartDate RelationshipStartDate  
      ,User_Relationship_EndDate RelationshipExpiryDate  
      ,User_Amt_To_be_Paid UserAmtTobePaid  
      ,User_Amt_Paid_YTD UserAmtPaidYTD  
      ,User_Create_Date UserCreateDate  
      ,User_Last_Mod_Date UserLastModDate  
      ,User_Picture UserPicture  
  FROM UserProfile'  + @tmpstr      
       
        
  print @SQL      
  EXECUTE(@SQL)      
        
END
GO
