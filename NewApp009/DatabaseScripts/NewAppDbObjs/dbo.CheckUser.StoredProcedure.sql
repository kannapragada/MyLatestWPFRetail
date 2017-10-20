USE [NewAppDb]
GO
/****** Object:  StoredProcedure [dbo].[CheckUser]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CheckUser]
	@User_Id		varchar(15),
	@Password		varchar(15)
AS

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	declare @sqlerror	varchar(max)


	SET @sqlerror = ''

		BEGIN TRY		
				SELECT User_Id UserId
					  ,User_Name UserName
					  ,User_Status UserStatus
					  ,User_DateofBirth UserDateofBirth
					  ,User_Gender UserGender
					  ,User_PresentAddress UserPresentAddress
					  ,User_PresentCity UserPresentCity
					  ,User_PresentPinCode UserPresentPinCode
					  ,User_PresentPhone UserPresentPhone
					  ,User_Mobile UserMobile
					  ,User_EMailId UserEMailId
					  ,User_PermanentAddress UserPermanentAddress
					  ,User_PermanentCity UserPermanentCity
					  ,User_PermanentPinCode UserPermanentPinCode
					  ,User_PermanentPhone UserPermanentPhone
					  ,User_IDProof_Type_Id UserIDProofTypeId
					  ,User_IDProof_Value UserIDProofValue
					  ,User_Relationship_StartDate UserRelationshipStartDate
					  ,User_Relationship_EndDate UserRelationshipEndDate
					  ,User_Amt_To_be_Paid UserAmtTobePaid
					  ,User_Amt_Paid_YTD UserAmtPaidYTD
					  ,User_Create_Date UserCreateDate
					  ,User_Last_Mod_Date UserLastModDate
					  ,User_Picture UserPicture
					  ,User_ThemeId UserThemeId
					  ,User_Pwd UserPwd
					  ,User_SecretQueryId UserSecretQueryId
					  ,User_SecretAnswer UserSecretAnswer
				into #tmp1 
				FROM UserProfile where User_Id = @User_Id

			if @@ROWCOUNT = 0
			begin
				set @sqlerror = 'User Does Not Exist! '
				RAISERROR (@sqlerror, 11, 1)
			end

		
			select * from #tmp1 where UserId = @User_Id and UserPwd = @Password
			
			if @@ROWCOUNT = 0
			begin
				set @sqlerror = 'Invalid Password! '
				RAISERROR (@sqlerror, 11, 1)
			end
		

		END TRY

		-- Whoops, there was an error
		BEGIN CATCH
					SET @sqlerror = ERROR_MESSAGE()
					RAISERROR (@sqlerror, 11, 1)
		END CATCH
END
GO
