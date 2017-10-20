USE [NewAppDb]
GO
/****** Object:  StoredProcedure [dbo].[GetNextSaleId]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
-- Author:  <Author,,Name>  
-- Create date: <Create Date,,>  
-- Description: <Description,,>  
-- =============================================  
CREATE PROCEDURE [dbo].[GetNextSaleId]  
@SaleId varchar(30) output  
AS  
  
BEGIN  
 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
 SET NOCOUNT ON;  
   
   declare	@CurrSaleId decimal(10,0),
			@SalePrefixStr varchar(15)
   
    begin  
		SELECT @SalePrefixStr = SalePrefixStr, @CurrSaleId = CurrSaleId FROM NextIds  
    
		set @SaleId = @SalePrefixStr + Convert(varchar, @CurrSaleId)
		
		update NextIds set CurrSaleId = CurrSaleId + 1;  
		
		select @SaleId
	end  
END
GO
