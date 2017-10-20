USE [NewAppDb]
GO
/****** Object:  StoredProcedure [dbo].[CreateSale]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[CreateSale]  
	@SaleId			varchar(15),
	@OrderDoc		xml,
	@SaleStatus	int	output
as  

BEGIN

	set nocount on  

	declare  
		@idoc			int,
		@mydatetime		datetime,
		@sqlerror		varchar(max)

	SET @mydatetime = GETDATE()

	SET @SaleStatus = 1
	SET @sqlerror = ''

	BEGIN TRY
		-- pick up handle to XML document  
		exec sp_xml_preparedocument @idoc output, @OrderDoc  

		--INSERT INTO Sales
		--SELECT * 
		SELECT * INTO #TEMP1
		FROM OPENXML (@idoc, '/ROOT/Sale',2)
		WITH (
				SalesId					varchar(15),
				SalesStatusId			int,
				SalesCustId				varchar(15),
				SalesTotalSalesAmt		decimal(10, 2),
				SalesTotalDiscAmt		decimal(10, 2),
				SalesTotalTaxAmt		decimal(10, 2),
				SalesFinalSalesAmt		decimal(10, 2),
				SalesAmtPaid			decimal(10, 2),
				SalesBalanceAmt			decimal(10, 2),
				SalesCreateDate			datetime,
				SalesLastModDate		datetime,
				SalesUserId				varchar(15),
				SalesGuestName			varchar(15)
			)

		--INSERT INTO SalesDetails
		--SELECT * 
		SELECT * INTO #TEMP2
		FROM OPENXML (@idoc, '/ROOT/Sale/ItemDetails',2)
		WITH (
				SalesId				varchar(15),
				SerialNumb			int,
				ItemId				varchar(15),
				BatchId				varchar(15),
				QuantitySold		bigint,
				Weight				decimal(18, 0),
				ItemAmtPerUnit		decimal(10, 2),
				TotalItemAmt		decimal(10, 2),
				DiscAmtPerUnit		decimal(10, 2),
				DiscItemAmt			decimal(10, 2),
				TaxAmtPerUnit		decimal(10, 2),
				TaxItemAmt			decimal(10, 2),
				FinalItemAmt		decimal(10, 2),
				SDCreateDate		datetime,
				SDLastModDate		datetime
			)
    END TRY
    
	BEGIN CATCH
	-- Whoops, there was an error
		BEGIN
			SET @SaleStatus = 2
			ROLLBACK TRANSACTION
			SET @sqlerror = 'Error while adding a New Item. ' + ERROR_MESSAGE()
			RAISERROR (@sqlerror, 11, 1)
		END
	END CATCH

	BEGIN TRY
		BEGIN TRANSACTION

			INSERT INTO Sales
			select * from #temp1

			INSERT INTO SalesDetails
			select * from #temp2
			
			declare @CustId					varchar(15)
			declare @SalesAmtPaid			decimal(10, 2)
			declare @SalesBalanceAmt		decimal(10, 2)
			
			SELECT	@SalesAmtPaid = SalesAmtPaid,
					@SalesBalanceAmt = SalesBalanceAmt,
					@CustId = SalesCustId
			FROM #TEMP1
			
			update Customer
			set Cust_Amt_To_be_Paid = Cust_Amt_To_be_Paid + @SalesBalanceAmt,
			Cust_Amt_Paid_YTD = Cust_Amt_Paid_YTD + @SalesAmtPaid
			where Cust_Id = @CustId
			
			delete from SaleTmpDateTime where Sale_Id = @SaleId         
			delete from SaleTmpItems where Sale_Id = @SaleId 

		COMMIT TRANSACTION

		SET @SaleStatus = 3
		UPDATE Sales SET Sales_Status_Id = @SaleStatus
		WHERE Sales_Id = @SaleId

	END TRY

	BEGIN CATCH
	-- Whoops, there was an error
		BEGIN
			SET @SaleStatus = 2
			ROLLBACK TRANSACTION
			SET @sqlerror = 'Error while adding a New Item. ' + ERROR_MESSAGE()
			RAISERROR (@sqlerror, 11, 1)
		END
	END CATCH
END
GO
