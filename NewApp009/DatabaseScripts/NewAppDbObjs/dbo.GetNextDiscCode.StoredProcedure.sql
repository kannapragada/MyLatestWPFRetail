USE [NewAppDb]
GO
/****** Object:  StoredProcedure [dbo].[GetNextDiscCode]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================    
-- Author:  <Author,,Name>    
-- Create date: <Create Date,,>    
-- Description: <Description,,>    
-- =============================================    
CREATE PROCEDURE [dbo].[GetNextDiscCode]    
@DiscCode varchar(30) output    
AS    
    
BEGIN    
 -- SET NOCOUNT ON added to prevent extra result sets from    
 -- interfering with SELECT statements.    
 SET NOCOUNT ON;    
     
   declare @CurrDiscId decimal(10,0),  
   @DiscPrefixStr varchar(15)  
     
    begin    
  SELECT @DiscPrefixStr = DiscPrefixStr, @CurrDiscId = CurrDiscCode FROM NextIds    
      
  set @DiscCode = @DiscPrefixStr + Convert(varchar, @CurrDiscId)  
    
  update NextIds set CurrDiscCode = CurrDiscCode + 1   

 end    
END
GO
