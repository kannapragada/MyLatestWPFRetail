USE [NewAppDb]
GO
/****** Object:  StoredProcedure [dbo].[GetNextCustId]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================    
-- Author:  <Author,,Name>    
-- Create date: <Create Date,,>    
-- Description: <Description,,>    
-- =============================================    
CREATE PROCEDURE [dbo].[GetNextCustId]    
@CustId varchar(30) output    
AS    
    
BEGIN    
 -- SET NOCOUNT ON added to prevent extra result sets from    
 -- interfering with SELECT statements.    
 SET NOCOUNT ON;    
     
   declare @CurrCustId decimal(10,0),  
   @CustPrefixStr varchar(15)  
     
    begin    
  SELECT @CustPrefixStr = CustPrefixStr, @CurrCustId = CurrCustId FROM NextIds    
      
  set @CustId = @CustPrefixStr + Convert(varchar, @CurrCustId)  
    
  update NextIds set CurrCustId = CurrCustId + 1;    
    
  select @CustId  
 end    
END
GO
