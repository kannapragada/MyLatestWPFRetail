USE [NewAppDb]
GO
/****** Object:  StoredProcedure [dbo].[SearchManufacturers]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================          
-- Author:  <Author,,Name>          
-- Create date: <Create Date,,>          
-- Description: <Description,,>          
-- =============================================          
CREATE PROCEDURE [dbo].[SearchManufacturers]          
 @Manuf_Id       varchar(15) = null,        
 @Manuf_Name       varchar(50) = null,        
 @Manuf_Address      varchar(100) = null,        
 @Manuf_IDProofTypeId    int = null,        
 @Manuf_IDProofValue     varchar(30) = null,        
 @Manuf_FromDateofBirth    datetime = null,        
 @Manuf_ToDateofBirth    datetime = null,        
 @Manuf_FromDateofStartRelationship datetime = null,        
 @Manuf_ToDateofStartRelationship  datetime = null,        
 @Manuf_FromDateofExpiryRelationship datetime = null,        
 @Manuf_ToDateofExpiryRelationship  datetime = null        
AS          
          
BEGIN          
 -- SET NOCOUNT ON added to prevent extra result sets from          
 -- interfering with SELECT statements.          
 SET NOCOUNT ON;          
         
 declare @SQL varchar(max)        
 declare @SQL1 varchar(max)        
 declare @tmpstr varchar(max)        
        
        
  IF (@Manuf_Id IS NOT NULL AND LEN(@Manuf_Id) > 0)        
  BEGIN        
   SET @tmpstr = ' WHERE (Manuf_Id like ''%' + CAST(@Manuf_Id AS NVARCHAR(15)) + '%''' + ')'        
  END        
          
  IF (@Manuf_Name IS NOT NULL AND LEN(@Manuf_Name) > 0)        
  BEGIN        
   if (LEN(@tmpstr) > 0)        
    SET @tmpstr = @tmpstr + ' AND '        
   else        
    SET @tmpstr = ' WHERE '        
          
    set @tmpstr = @tmpstr + '(Manuf_Name like ''%' + CAST(@Manuf_Name AS NVARCHAR(15)) + '%''' + ')'        
  END        
          
  IF (@Manuf_Address IS NOT NULL AND LEN(@Manuf_Address) > 0)        
  BEGIN        
   if (LEN(@tmpstr) > 0)        
    SET @tmpstr = @tmpstr + ' AND '        
   else        
    SET @tmpstr = ' WHERE '        
          
    set @tmpstr = @tmpstr + '(Manuf_Address like ''%' + CAST(@Manuf_Name AS NVARCHAR(15)) + '%''' + ')'        
  END        
          
  IF (@Manuf_IDProofTypeId IS NOT NULL)        
  BEGIN        
   IF ((@Manuf_IDProofValue IS NOT NULL AND LEN(@Manuf_IDProofValue) > 0))        
   BEGIN        
    if (LEN(@tmpstr) > 0)        
     SET @tmpstr = @tmpstr + ' AND '        
    else        
     SET @tmpstr = ' WHERE '        
           
     set @tmpstr = @tmpstr + '(Manuf_IDProof_Value like ''%' + CAST(@Manuf_IDProofValue AS NVARCHAR(15)) + '%''' + ')'        
   END        
  END        
          
  IF ((IsDate(@Manuf_FromDateofBirth) = 1) AND (IsDate(@Manuf_ToDateofBirth) = 1))        
  BEGIN        
   if (LEN(@tmpstr) > 0)        
    SET @tmpstr = @tmpstr + ' AND '        
   else        
    SET @tmpstr = ' WHERE '        
        
   set @tmpstr = @tmpstr + '(Manuf_DateofBirth BETWEEN ''' + CONVERT(varchar(30), @Manuf_FromDateofBirth, 126) + ''' AND ''' + CONVERT(varchar(30), @Manuf_ToDateofBirth, 126) + ''')'        
  END        
          
  IF ((IsDate(@Manuf_FromDateofStartRelationship) = 1) AND (IsDate(@Manuf_FromDateofStartRelationship) = 1))        
  BEGIN        
   if (LEN(@tmpstr) > 0)        
    SET @tmpstr = @tmpstr + ' AND '        
   else        
    SET @tmpstr = ' WHERE '        
        
   set @tmpstr = @tmpstr + '(Manuf_Relationship_StartDate BETWEEN ''' + CONVERT(varchar(30), @Manuf_FromDateofStartRelationship, 126) + ''' AND ''' + CONVERT(varchar(30), @Manuf_FromDateofStartRelationship, 126) + ''')'        
  END        
          
  IF ((IsDate(@Manuf_FromDateofExpiryRelationship) = 1) AND (IsDate(@Manuf_ToDateofExpiryRelationship) = 1))        
  BEGIN        
   if (LEN(@tmpstr) > 0)        
    SET @tmpstr = @tmpstr + ' AND '        
   else        
    SET @tmpstr = ' WHERE '        
        
   set @tmpstr = @tmpstr + '(Manuf_Relationship_ExpiryDate BETWEEN ''' + CONVERT(varchar(30), @Manuf_FromDateofExpiryRelationship, 126) + ''' AND ''' + CONVERT(varchar(30), @Manuf_ToDateofExpiryRelationship, 126) + ''')'        
  END        
         
         
     SET @SQL = 'SELECT Manuf_Id ManufId    
      ,Manuf_Name ManufName    
      ,Manuf_Status ManufStatus    
      ,Manuf_DateofBirth ManufDateofBirth    
      ,Manuf_Gender ManufGender    
      ,Manuf_PresentAddress ManufPresentAddress    
      ,Manuf_PresentCity ManufPresentCity    
      ,Manuf_PresentPinCode ManufPresentPinCode    
      ,Manuf_PresentPhone ManufPresentPhone    
      ,Manuf_Mobile ManufPresentMobile    
      ,Manuf_EMailId ManufEMailId    
   ,Manuf_IsPresentPermAddressSame ManufIsPresentPermAddressSame  
      ,Manuf_PermanentAddress ManufPermanentAddress    
      ,Manuf_PermanentCity ManufPermanentCity    
      ,Manuf_PermanentPinCode ManufPermanentPinCode    
      ,Manuf_PermanentPhone ManufPermanentPhone    
      ,Manuf_IDProof_Type_Id ManufIDProofTypeId    
      ,Manuf_IDProof_Value ManufIDProofValue    
      ,Manuf_Relationship_StartDate RelationshipStartDate    
      ,Manuf_Relationship_EndDate RelationshipExpiryDate    
      ,Manuf_Amt_To_be_Paid ManufAmtTobePaid    
      ,Manuf_Amt_Paid_YTD ManufAmtPaidYTD    
      ,Manuf_Create_Date ManufCreateDate    
      ,Manuf_Last_Mod_Date ManufLastModDate    
      ,Manuf_Picture ManufPicture    
  FROM Manufacturer'  + @tmpstr        
         
          
  print @SQL        
  EXECUTE(@SQL)        
          
END
GO
