USE [NewAppDb]
GO
/****** Object:  StoredProcedure [dbo].[SearchCustomers]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================        
-- Author:  <Author,,Name>        
-- Create date: <Create Date,,>        
-- Description: <Description,,>        
-- =============================================        
CREATE PROCEDURE [dbo].[SearchCustomers]        
 @Cust_Id							varchar(15) = null,      
 @Cust_Name							varchar(50) = null,      
 @Cust_Address						varchar(100) = null,      
 @Cust_IDProofTypeId				int = null,      
 @Cust_IDProofValue					varchar(30) = null,      
 @Cust_FromDateofBirth				datetime = null,      
 @Cust_ToDateofBirth				datetime = null,      
 @Cust_FromDateofStartRelationship	datetime = null,      
 @Cust_ToDateofStartRelationship		datetime = null,      
 @Cust_FromDateofExpiryRelationship	datetime = null,      
 @Cust_ToDateofExpiryRelationship		datetime = null      
AS        
        
BEGIN        
 -- SET NOCOUNT ON added to prevent extra result sets from        
 -- interfering with SELECT statements.        
 SET NOCOUNT ON;        
       
 declare @SQL varchar(max)      
 declare @SQL1 varchar(max)      
 declare @tmpstr varchar(max)      
      
      
  IF (@Cust_Id IS NOT NULL AND LEN(@Cust_Id) > 0)      
  BEGIN      
   SET @tmpstr = ' WHERE (Cust_Id like ''%' + CAST(@Cust_Id AS NVARCHAR(15)) + '%''' + ')'      
  END      
        
  IF (@Cust_Name IS NOT NULL AND LEN(@Cust_Name) > 0)      
  BEGIN      
   if (LEN(@tmpstr) > 0)      
    SET @tmpstr = @tmpstr + ' AND '      
   else      
    SET @tmpstr = ' WHERE '      
        
    set @tmpstr = @tmpstr + '(Cust_Name like ''%' + CAST(@Cust_Name AS NVARCHAR(15)) + '%''' + ')'      
  END      
        
  IF (@Cust_Address IS NOT NULL AND LEN(@Cust_Address) > 0)      
  BEGIN      
   if (LEN(@tmpstr) > 0)      
    SET @tmpstr = @tmpstr + ' AND '      
   else      
    SET @tmpstr = ' WHERE '      
        
    set @tmpstr = @tmpstr + '(Cust_Address like ''%' + CAST(@Cust_Name AS NVARCHAR(15)) + '%''' + ')'      
  END      
        
  IF (@Cust_IDProofTypeId IS NOT NULL)      
  BEGIN      
   IF ((@Cust_IDProofValue IS NOT NULL AND LEN(@Cust_IDProofValue) > 0))      
   BEGIN      
    if (LEN(@tmpstr) > 0)      
     SET @tmpstr = @tmpstr + ' AND '      
    else      
     SET @tmpstr = ' WHERE '      
         
     set @tmpstr = @tmpstr + '(Cust_IDProof_Value like ''%' + CAST(@Cust_IDProofValue AS NVARCHAR(15)) + '%''' + ')'      
   END      
  END      
        
  IF ((IsDate(@Cust_FromDateofBirth) = 1) AND (IsDate(@Cust_ToDateofBirth) = 1))      
  BEGIN      
   if (LEN(@tmpstr) > 0)      
    SET @tmpstr = @tmpstr + ' AND '      
   else      
    SET @tmpstr = ' WHERE '      
      
   set @tmpstr = @tmpstr + '(Cust_DateofBirth BETWEEN ''' + CONVERT(varchar(30), @Cust_FromDateofBirth, 126) + ''' AND ''' + CONVERT(varchar(30), @Cust_ToDateofBirth, 126) + ''')'      
  END      
        
  IF ((IsDate(@Cust_FromDateofStartRelationship) = 1) AND (IsDate(@Cust_FromDateofStartRelationship) = 1))      
  BEGIN      
   if (LEN(@tmpstr) > 0)      
    SET @tmpstr = @tmpstr + ' AND '      
   else      
    SET @tmpstr = ' WHERE '      
      
   set @tmpstr = @tmpstr + '(Cust_Relationship_StartDate BETWEEN ''' + CONVERT(varchar(30), @Cust_FromDateofStartRelationship, 126) + ''' AND ''' + CONVERT(varchar(30), @Cust_FromDateofStartRelationship, 126) + ''')'      
  END      
        
  IF ((IsDate(@Cust_FromDateofExpiryRelationship) = 1) AND (IsDate(@Cust_ToDateofExpiryRelationship) = 1))      
  BEGIN      
   if (LEN(@tmpstr) > 0)      
    SET @tmpstr = @tmpstr + ' AND '      
   else      
    SET @tmpstr = ' WHERE '      
      
   set @tmpstr = @tmpstr + '(Cust_Relationship_ExpiryDate BETWEEN ''' + CONVERT(varchar(30), @Cust_FromDateofExpiryRelationship, 126) + ''' AND ''' + CONVERT(varchar(30), @Cust_ToDateofExpiryRelationship, 126) + ''')'      
  END      
       
       
     SET @SQL = 'SELECT Cust_Id CustId  
      ,Cust_Name CustName  
      ,Cust_Status CustStatus  
      ,Cust_DateofBirth CustDateofBirth  
      ,Cust_Gender CustGender  
      ,Cust_PresentAddress CustPresentAddress  
      ,Cust_PresentCity CustPresentCity  
      ,Cust_PresentPinCode CustPresentPinCode  
      ,Cust_PresentPhone CustPresentPhone  
      ,Cust_Mobile CustPresentMobile  
      ,Cust_EMailId CustEMailId  
	  ,Cust_IsPresentPermAddressSame CustIsPresentPermAddressSame
      ,Cust_PermanentAddress CustPermanentAddress  
      ,Cust_PermanentCity CustPermanentCity  
      ,Cust_PermanentPinCode CustPermanentPinCode  
      ,Cust_PermanentPhone CustPermanentPhone  
      ,Cust_IDProof_Type_Id CustIDProofTypeId  
      ,Cust_IDProof_Value CustIDProofValue  
      ,Cust_Relationship_StartDate RelationshipStartDate  
      ,Cust_Relationship_EndDate RelationshipExpiryDate  
      ,Cust_Amt_To_be_Paid CustAmtTobePaid  
      ,Cust_Amt_Paid_YTD CustAmtPaidYTD  
      ,Cust_Create_Date CustCreateDate  
      ,Cust_Last_Mod_Date CustLastModDate  
      ,Cust_Picture CustPicture  
  FROM Customer'  + @tmpstr      
       
        
  print @SQL      
  EXECUTE(@SQL)      
        
END
GO
