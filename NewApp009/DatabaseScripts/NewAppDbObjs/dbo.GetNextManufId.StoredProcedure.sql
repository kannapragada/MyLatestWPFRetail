USE [NewAppDb]
GO
/****** Object:  StoredProcedure [dbo].[GetNextManufId]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================    
-- Author:  <Author,,Name>    
-- Create date: <Create Date,,>    
-- Description: <Description,,>    
-- =============================================    
CREATE PROCEDURE [dbo].[GetNextManufId]    
@ManufId varchar(30) output    
AS    
    
BEGIN    
 -- SET NOCOUNT ON added to prevent extra result sets from    
 -- interfering with SELECT statements.    
 SET NOCOUNT ON;    
     
   declare @CurrManufId decimal(10,0),  
   @ManufPrefixStr varchar(15)  
     
    begin    
  SELECT @ManufPrefixStr = ManufPrefixStr, @CurrManufId = CurrManufId FROM NextIds    
      
  set @ManufId = @ManufPrefixStr + Convert(varchar, @CurrManufId)  
    
  update NextIds set CurrManufId = CurrManufId + 1;    
    
  select @ManufId  
 end    
END
GO
