USE [NewAppDb]
GO
/****** Object:  StoredProcedure [dbo].[GetNextBillNumber]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================    
-- Author:  <Author,,Name>    
-- Create date: <Create Date,,>    
-- Description: <Description,,>    
-- =============================================    
CREATE PROCEDURE [dbo].[GetNextBillNumber]    
@BillNumber varchar(30) output    
AS    
    
BEGIN    
 -- SET NOCOUNT ON added to prevent extra result sets from    
 -- interfering with SELECT statements.    
 SET NOCOUNT ON;    
     
   declare	@BillNumb decimal(10,0),  
			@BillPrefixStr varchar(15)  
     
    begin    
  SELECT @BillPrefixStr = BillPrefixStr, @BillNumb = CurrBillNumb FROM NextIds    
      
  set @BillNumber = @BillPrefixStr + Convert(varchar, @BillNumb)  
    
  update NextIds set CurrBillNumb = CurrBillNumb + 1;    
    
  select @BillNumber  
 end    
END
GO
