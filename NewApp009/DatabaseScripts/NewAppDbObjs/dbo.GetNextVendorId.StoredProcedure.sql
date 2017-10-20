USE [NewAppDb]
GO
/****** Object:  StoredProcedure [dbo].[GetNextVendorId]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================    
-- Author:  <Author,,Name>    
-- Create date: <Create Date,,>    
-- Description: <Description,,>    
-- =============================================    
CREATE PROCEDURE [dbo].[GetNextVendorId]    
@VendorId varchar(30) output    
AS    
    
BEGIN    
 -- SET NOCOUNT ON added to prevent extra result sets from    
 -- interfering with SELECT statements.    
 SET NOCOUNT ON;    
     
   declare @CurrVendorId decimal(10,0),  
   @VendorPrefixStr varchar(15)  
     
    begin    
  SELECT @VendorPrefixStr = VendorPrefixStr, @CurrVendorId = CurrVendorId FROM NextIds    
      
  set @VendorId = @VendorPrefixStr + Convert(varchar, @CurrVendorId)  
    
  update NextIds set CurrVendorId = CurrVendorId + 1;    
    
  select @VendorId  
 end    
END
GO
