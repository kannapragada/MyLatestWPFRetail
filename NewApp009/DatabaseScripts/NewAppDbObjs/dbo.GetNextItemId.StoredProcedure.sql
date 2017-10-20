USE [NewAppDb]
GO
/****** Object:  StoredProcedure [dbo].[GetNextItemId]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================  
-- Author:  <Author,,Name>  
-- Create date: <Create Date,,>  
-- Description: <Description,,>  
-- =============================================  
CREATE PROCEDURE [dbo].[GetNextItemId]  
 @ItemId varchar(30) output  
AS  
  
BEGIN  
 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
 SET NOCOUNT ON;  
  
    -- Insert statements for procedure here  
    declare @CurrItemId decimal(10,0),  
			@ItemPrefixStr varchar(15)  
     
 begin    
	 SELECT @ItemPrefixStr = ItemPrefixStr, @CurrItemId = CurrItemId FROM NextIds    
	      
	 SET @ItemId = @ItemPrefixStr + Convert(varchar, @CurrItemId)  
	    
	 UPDATE NextIds SET CurrItemId = CurrItemId + 1  
	    
	 SELECT @ItemId  
 end    
      
   
END
GO
