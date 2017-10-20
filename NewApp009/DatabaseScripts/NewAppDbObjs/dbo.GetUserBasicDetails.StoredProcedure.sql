USE [NewAppDb]
GO
/****** Object:  StoredProcedure [dbo].[GetUserBasicDetails]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetUserBasicDetails]
	@User_Id		varchar(15)
AS

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	declare @sqlerror	varchar(max)


	SET @sqlerror = ''

		BEGIN TRY		
				SELECT User_Id
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
					  ,User_SecretAnswer
				FROM UserProfile where User_Id = @User_Id


		END TRY

		-- Whoops, there was an error
		BEGIN CATCH
					SET @sqlerror = 'Error while getting the User basic details. ' + ERROR_MESSAGE()
					RAISERROR (@sqlerror, 11, 1)
		END CATCH
END
GO
