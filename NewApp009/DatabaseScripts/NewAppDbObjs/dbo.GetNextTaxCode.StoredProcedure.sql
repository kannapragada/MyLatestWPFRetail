USE [NewAppDb]
GO
/****** Object:  StoredProcedure [dbo].[GetNextTaxCode]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================    
-- Author:  <Author,,Name>    
-- Create date: <Create Date,,>    
-- Description: <Description,,>    
-- =============================================    
CREATE PROCEDURE [dbo].[GetNextTaxCode]    
@Tax_Code varchar(30) output    
AS    
    
BEGIN    
 -- SET NOCOUNT ON added to prevent extra result sets from    
 -- interfering with SELECT statements.    
 SET NOCOUNT ON;    
     
   declare	@CurrTaxCode decimal(10,0),  
			@TaxPrefixStr varchar(15)  
     
    begin    
  SELECT @TaxPrefixStr = TaxPrefixStr, @CurrTaxCode = CurrTaxId FROM NextIds    
      
  set @Tax_Code = @TaxPrefixStr + Convert(varchar, @CurrTaxCode)  
    
  update NextIds set CurrTaxId = @CurrTaxCode + 1;    
    
  select @Tax_Code  
 end    
END
GO
