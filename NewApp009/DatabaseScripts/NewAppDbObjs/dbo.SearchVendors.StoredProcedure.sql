USE [NewAppDb]
GO
/****** Object:  StoredProcedure [dbo].[SearchVendors]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================        
-- Author:  <Author,,Name>        
-- Create date: <Create Date,,>        
-- Description: <Description,,>        
-- =============================================        
CREATE PROCEDURE [dbo].[SearchVendors]        
 @Vendor_Id							varchar(15) = null,      
 @Vendor_Name							varchar(50) = null,      
 @Vendor_Address						varchar(100) = null,      
 @Vendor_IDProofTypeId				int = null,      
 @Vendor_IDProofValue					varchar(30) = null,      
 @Vendor_FromDateofBirth				datetime = null,      
 @Vendor_ToDateofBirth				datetime = null,      
 @Vendor_FromDateofStartRelationship	datetime = null,      
 @Vendor_ToDateofStartRelationship		datetime = null,      
 @Vendor_FromDateofExpiryRelationship	datetime = null,      
 @Vendor_ToDateofExpiryRelationship		datetime = null      
AS        
        
BEGIN        
 -- SET NOCOUNT ON added to prevent extra result sets from        
 -- interfering with SELECT statements.        
 SET NOCOUNT ON;        
       
 declare @SQL varchar(max)      
 declare @SQL1 varchar(max)      
 declare @tmpstr varchar(max)      
      
      
  IF (@Vendor_Id IS NOT NULL AND LEN(@Vendor_Id) > 0)      
  BEGIN      
   SET @tmpstr = ' WHERE (Vendor_Id like ''%' + CAST(@Vendor_Id AS NVARCHAR(15)) + '%''' + ')'      
  END      
        
  IF (@Vendor_Name IS NOT NULL AND LEN(@Vendor_Name) > 0)      
  BEGIN      
   if (LEN(@tmpstr) > 0)      
    SET @tmpstr = @tmpstr + ' AND '      
   else      
    SET @tmpstr = ' WHERE '      
        
    set @tmpstr = @tmpstr + '(Vendor_Name like ''%' + CAST(@Vendor_Name AS NVARCHAR(15)) + '%''' + ')'      
  END      
        
  IF (@Vendor_Address IS NOT NULL AND LEN(@Vendor_Address) > 0)      
  BEGIN      
   if (LEN(@tmpstr) > 0)      
    SET @tmpstr = @tmpstr + ' AND '      
   else      
    SET @tmpstr = ' WHERE '      
        
    set @tmpstr = @tmpstr + '(Vendor_Address like ''%' + CAST(@Vendor_Name AS NVARCHAR(15)) + '%''' + ')'      
  END      
        
  IF (@Vendor_IDProofTypeId IS NOT NULL)      
  BEGIN      
   IF ((@Vendor_IDProofValue IS NOT NULL AND LEN(@Vendor_IDProofValue) > 0))      
   BEGIN      
    if (LEN(@tmpstr) > 0)      
     SET @tmpstr = @tmpstr + ' AND '      
    else      
     SET @tmpstr = ' WHERE '      
         
     set @tmpstr = @tmpstr + '(Vendor_IDProof_Value like ''%' + CAST(@Vendor_IDProofValue AS NVARCHAR(15)) + '%''' + ')'      
   END      
  END      
        
  IF ((IsDate(@Vendor_FromDateofBirth) = 1) AND (IsDate(@Vendor_ToDateofBirth) = 1))      
  BEGIN      
   if (LEN(@tmpstr) > 0)      
    SET @tmpstr = @tmpstr + ' AND '      
   else      
    SET @tmpstr = ' WHERE '      
      
   set @tmpstr = @tmpstr + '(Vendor_DateofBirth BETWEEN ''' + CONVERT(varchar(30), @Vendor_FromDateofBirth, 126) + ''' AND ''' + CONVERT(varchar(30), @Vendor_ToDateofBirth, 126) + ''')'      
  END      
        
  IF ((IsDate(@Vendor_FromDateofStartRelationship) = 1) AND (IsDate(@Vendor_FromDateofStartRelationship) = 1))      
  BEGIN      
   if (LEN(@tmpstr) > 0)      
    SET @tmpstr = @tmpstr + ' AND '      
   else      
    SET @tmpstr = ' WHERE '      
      
   set @tmpstr = @tmpstr + '(Vendor_Relationship_StartDate BETWEEN ''' + CONVERT(varchar(30), @Vendor_FromDateofStartRelationship, 126) + ''' AND ''' + CONVERT(varchar(30), @Vendor_FromDateofStartRelationship, 126) + ''')'      
  END      
        
  IF ((IsDate(@Vendor_FromDateofExpiryRelationship) = 1) AND (IsDate(@Vendor_ToDateofExpiryRelationship) = 1))      
  BEGIN      
   if (LEN(@tmpstr) > 0)      
    SET @tmpstr = @tmpstr + ' AND '      
   else      
    SET @tmpstr = ' WHERE '      
      
   set @tmpstr = @tmpstr + '(Vendor_Relationship_ExpiryDate BETWEEN ''' + CONVERT(varchar(30), @Vendor_FromDateofExpiryRelationship, 126) + ''' AND ''' + CONVERT(varchar(30), @Vendor_ToDateofExpiryRelationship, 126) + ''')'      
  END      
       
       
     SET @SQL = 'SELECT Vendor_Id VendorId  
      ,Vendor_Name VendorName  
      ,Vendor_Status VendorStatus  
      ,Vendor_DateofBirth VendorDateofBirth  
      ,Vendor_Gender VendorGender  
      ,Vendor_PresentAddress VendorPresentAddress  
      ,Vendor_PresentCity VendorPresentCity  
      ,Vendor_PresentPinCode VendorPresentPinCode  
      ,Vendor_PresentPhone VendorPresentPhone  
      ,Vendor_Mobile VendorPresentMobile  
      ,Vendor_EMailId VendorEMailId  
	  ,Vendor_IsPresentPermAddressSame VendorIsPresentPermAddressSame
      ,Vendor_PermanentAddress VendorPermanentAddress  
      ,Vendor_PermanentCity VendorPermanentCity  
      ,Vendor_PermanentPinCode VendorPermanentPinCode  
      ,Vendor_PermanentPhone VendorPermanentPhone  
      ,Vendor_IDProof_Type_Id VendorIDProofTypeId  
      ,Vendor_IDProof_Value VendorIDProofValue  
      ,Vendor_Relationship_StartDate RelationshipStartDate  
      ,Vendor_Relationship_EndDate RelationshipExpiryDate  
      ,Vendor_Amt_To_be_Paid VendorAmtTobePaid  
      ,Vendor_Amt_Paid_YTD VendorAmtPaidYTD  
      ,Vendor_Create_Date VendorCreateDate  
      ,Vendor_Last_Mod_Date VendorLastModDate  
      ,Vendor_Picture VendorPicture  
  FROM Vendor'  + @tmpstr      
       
        
  print @SQL      
  EXECUTE(@SQL)      
        
END
GO
