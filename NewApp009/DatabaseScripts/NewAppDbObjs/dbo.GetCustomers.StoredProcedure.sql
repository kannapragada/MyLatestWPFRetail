USE [NewAppDb]
GO
/****** Object:  StoredProcedure [dbo].[GetCustomers]    Script Date: 04/16/2015 18:34:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================      
-- Author:  <Author,,Name>      
-- Create date: <Create Date,,>      
-- Description: <Description,,>      
-- =============================================      
CREATE PROCEDURE [dbo].[GetCustomers]      
 @Cust_Id    varchar(15) = null,    
 @Cust_Name    varchar(50) = null    
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
    SET @tmpstr = @tmpstr + ' OR '    
   else    
    SET @tmpstr = ' WHERE '    
      
    set @tmpstr = @tmpstr + '(Cust_Name like ''%' + CAST(@Cust_Name AS NVARCHAR(15)) + '%''' + ')'    
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
      ,Cust_Relationship_StartDate MembershipStartDate  
      ,Cust_Relationship_EndDate MembershipExpiryDate  
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
