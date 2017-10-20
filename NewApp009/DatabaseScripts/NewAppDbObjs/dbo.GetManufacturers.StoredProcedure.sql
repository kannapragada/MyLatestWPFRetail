USE [NewAppDb]
GO
/****** Object:  StoredProcedure [dbo].[GetManufacturers]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================      

-- Author:  <Author,,Name>      

-- Create date: <Create Date,,>      

-- Description: <Description,,>      

-- =============================================      

CREATE PROCEDURE [dbo].[GetManufacturers]      

 @Manuf_Id    varchar(15) = null,    

 @Manuf_Name    varchar(50) = null    

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

    SET @tmpstr = @tmpstr + ' OR '    

   else    

    SET @tmpstr = ' WHERE '    

      

    set @tmpstr = @tmpstr + '(Manuf_Name like ''%' + CAST(@Manuf_Name AS NVARCHAR(15)) + '%''' + ')'    

  END    

  

  if @Manuf_Id = 'ALL'

	SET @tmpstr = ''



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

	  ,Manuf_IsPresentPermAddressSame

      ,Manuf_PermanentAddress ManufPermanentAddress  

      ,Manuf_PermanentCity ManufPermanentCity  

      ,Manuf_PermanentPinCode ManufPermanentPinCode  

      ,Manuf_PermanentPhone ManufPermanentPhone  

      ,Manuf_IDProof_Type_Id ManufIDProofTypeId  

      ,Manuf_IDProof_Value ManufIDProofValue  

      ,Manuf_Relationship_StartDate MembershipStartDate  

      ,Manuf_Relationship_EndDate MembershipExpiryDate  

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
