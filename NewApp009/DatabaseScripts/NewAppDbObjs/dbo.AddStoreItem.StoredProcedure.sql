USE [NewAppDb]
GO
/****** Object:  StoredProcedure [dbo].[AddStoreItem]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AddStoreItem]
	@Item_Id				varchar(15),
	@Item_Name				varchar(30),
	@Item_Description		varchar(50),
	@IB_Batch_Id			varchar(15),
	@IB_Manuf_Id			varchar(15),
	@IB_Vendor_Id			varchar(15),
	@IB_Qty_Procured		bigint,
	@IB_Price_Procured		decimal(10, 2),
	@IB_MRP					decimal(10, 2),
	@IB_Date_Procured		datetime,
	@IB_Date_Manuf			datetime,
	@IB_Date_Exp			datetime,
	@IB_Weight_Procured		decimal(10, 2),
	@IB_Weight_Available	decimal(10, 2),
	@IB_BarCode				nvarchar(20),
	@IB_Price_Sell			decimal(10, 2),
	@IB_Qty_Available		bigint,
	@Item_Create_Date		datetime
AS

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	declare @sqlerror	varchar(max)

	SET @sqlerror = ''

	BEGIN TRANSACTION
		BEGIN TRY
			insert into Items(Item_Id, Item_Name, Item_Description, Item_Create_Date, Item_Last_Mod_Date) 
			values (@Item_Id, @Item_Name, @Item_Description, @Item_Create_Date, null)

			insert into ItemBatch(IB_Item_Id, IB_Batch_Id, IB_Manuf_Id, IB_Qty_Procured, IB_Price_Procured, IB_MRP, IB_Date_Procured,  
						IB_Date_Manuf, IB_Date_Exp, IB_Weight_Procured, IB_Weight_Available, IB_BarCode, IB_Price_Sell, IB_Qty_Available, IB_Create_Date, IB_Last_Mod_Date, IB_Vendor_Id)
			values (@Item_Id, @IB_Batch_Id, @IB_Manuf_Id, @IB_Qty_Procured, @IB_Price_Procured, @IB_MRP, @IB_Date_Procured, @IB_Date_Manuf, @IB_Date_Exp, @IB_Weight_Procured, 
					@IB_Weight_Available, @IB_BarCode, @IB_Price_Sell, @IB_Qty_Available, @Item_Create_Date, null, @IB_Vendor_Id)

			COMMIT TRANSACTION
		END TRY

		BEGIN CATCH
		-- Whoops, there was an error
				BEGIN
					ROLLBACK TRANSACTION
					SET @sqlerror = 'Error while adding a New Store Item. ' + ERROR_MESSAGE()
					RAISERROR (@sqlerror, 11, 1)
				END
		END CATCH
END
GO
