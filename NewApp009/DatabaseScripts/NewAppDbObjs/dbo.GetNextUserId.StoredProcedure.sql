USE [NewAppDb]
GO
/****** Object:  StoredProcedure [dbo].[GetNextUserId]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================    
-- Author:  <Author,,Name>    
-- Create date: <Create Date,,>    
-- Description: <Description,,>    
-- =============================================    
create PROCEDURE [dbo].[GetNextUserId]    
@UserId varchar(30) output    
AS    
    
BEGIN    
 -- SET NOCOUNT ON added to prevent extra result sets from    
 -- interfering with SELECT statements.    
 SET NOCOUNT ON;    
     
   declare @CurrUserId decimal(10,0),  
   @UserPrefixStr varchar(15)  
     
    begin    
  SELECT @UserPrefixStr = UserPrefixStr, @CurrUserId = CurrUserId FROM NextIds    
      
  set @UserId = @UserPrefixStr + Convert(varchar, @CurrUserId)  
    
  update NextIds set CurrUserId = CurrUserId + 1;    
    
  select @UserId  
 end    
END
GO
