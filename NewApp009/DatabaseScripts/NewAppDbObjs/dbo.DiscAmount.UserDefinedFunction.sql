USE [NewAppDb]
GO
/****** Object:  UserDefinedFunction [dbo].[DiscAmount]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[DiscAmount](@ItemId varchar(15), @BatchId varchar(15))
RETURNS NUMERIC(18, 2)

AS 
   BEGIN

       DECLARE @DiscAmt1 AS DECIMAL(18, 2)
       DECLARE @DiscAmt2 AS DECIMAL(18, 2)

        SELECT  @DiscAmt1 = SUM(ISNULL(Disc_Value,0))
        FROM    WPFRetail..DiscItemBatch a 
        JOIN	WPFRetail..Discounts b ON a.DiscI_Disc_Code = b.Disc_Code        
        WHERE   a.DiscI_Item_Id =  @ItemId 
        AND		DiscI_Item_Batch_Id = @BatchId
        AND		B.Disc_Type_Id = 0

		SELECT  @DiscAmt2 = (ISNULL(Disc_Value,0) * ISNULL(IB_Price_Sell,0))/100
        FROM    WPFRetail..DiscItemBatch a 
        JOIN	WPFRetail..Discounts b ON a.DiscI_Disc_Code = b.Disc_Code   
        JOIN	WPFRetail..ItemBatch c ON c.IB_Item_Id = a.DiscI_Item_Id   
        AND		c.IB_Batch_Id = a.DiscI_Item_Batch_Id  
        WHERE   a.DiscI_Item_Id =  @ItemId 
        AND		DiscI_Item_Batch_Id = @BatchId
        AND		B.Disc_Type_Id = 1


	return ISNULL(@DiscAmt1,0) + ISNULL(@DiscAmt2,0)

	END
GO
