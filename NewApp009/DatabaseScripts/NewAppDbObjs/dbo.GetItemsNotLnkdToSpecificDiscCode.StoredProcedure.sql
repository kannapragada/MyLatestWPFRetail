USE [NewAppDb]
GO
/****** Object:  StoredProcedure [dbo].[GetItemsNotLnkdToSpecificDiscCode]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetItemsNotLnkdToSpecificDiscCode]
	@DiscCode		varchar(35)
AS

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT	a.Item_Id ItemId, a.Item_Name ItemName, b.IB_Batch_Id BatchId, 
		a.Item_Category ItemCategory, b.IB_Qty_Available QtyAvailable
	INTO #Temp	
	FROM Items a 
	JOIN ItemBatch b ON a.Item_Id = b.IB_Item_Id
	where a.Item_Id NOT IN (SELECT DiscI_Item_Id 
							FROM DiscItemBatch
							where DiscI_Disc_Code = @DiscCode)
	
	select * from #Temp
	
END
GO
