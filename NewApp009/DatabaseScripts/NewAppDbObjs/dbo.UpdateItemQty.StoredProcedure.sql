USE [NewAppDb]
GO
/****** Object:  StoredProcedure [dbo].[UpdateItemQty]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[UpdateItemQty]    
 @Sale_Id    varchar(15),    
 @Serial_Numb  int,  
 @IB_Item_Id   varchar(15),    
 @IB_Batch_Id  varchar(15),    
 @Quantity     int,    
 @Operation    char(2)    
as      
    
BEGIN    
    
 declare @sqlerror  varchar(max),    
   @OldQuantity int,  
   @CurrQtyInTable int   
    
 set nocount on      
    
 BEGIN TRY    
    
  IF @Operation = 'S'    
  BEGIN    
   begin tran  
    UPDATE  ItemBatch SET    
    IB_Qty_Available = (IB_Qty_Available - @Quantity)    
    WHERE     
    IB_Item_Id = @IB_Item_Id     
    AND IB_Batch_Id = @IB_Batch_Id    
      
    if (select count(*) from SaleTmpDateTime where Sale_Id = @Sale_Id) = 0
		insert into SaleTmpDateTime values(@Sale_Id, GETDATE())  
      
    insert into SaleTmpItems values(@Sale_Id, @Serial_Numb, @IB_Item_Id, @IB_Batch_Id, @Quantity)  
      
   commit tran  
  END    
    
  IF @Operation = 'U'    
  BEGIN    
   select @OldQuantity = qty from SaleTmpItems where Sale_Id = @Sale_Id and Item_Id = @IB_Item_Id     
   AND Batch_Id = @IB_Batch_Id and Serial_Numb = @Serial_Numb  
  
   SELECT @CurrQtyInTable = IB_Qty_Available FROM  ItemBatch WHERE IB_Item_Id = @IB_Item_Id AND IB_Batch_Id = @IB_Batch_Id    
        
   IF (@CurrQtyInTable + @OldQuantity - @Quantity) > 0    
   BEGIN  
    begin tran  
     UPDATE  ItemBatch SET  IB_Qty_Available = (IB_Qty_Available + @OldQuantity) -  @Quantity  
     WHERE   IB_Item_Id = @IB_Item_Id     
     AND IB_Batch_Id = @IB_Batch_Id      
       
       
     update SaleTmpDateTime set Sale_StartDateTime = GETDATE() where Sale_Id = @Sale_Id     
       
     update SaleTmpItems set Qty = @Quantity where Sale_Id = @Sale_Id and Item_Id = @IB_Item_Id     
     AND Batch_Id = @IB_Batch_Id and Serial_Numb = @Serial_Numb  
    commit tran  
   END    
   ELSE    
   BEGIN    
    SET @sqlerror = 'New Quantity Cannot be Greater Than Total Available Quantity'    
    RAISERROR (@sqlerror, 11, 1)    
   END    
  END    
       
  IF @Operation = 'D'    
  BEGIN    
   select @Quantity = Qty from SaleTmpItems where Sale_Id = @Sale_Id and Item_Id = @IB_Item_Id     
    AND Batch_Id = @IB_Batch_Id and Serial_Numb = @Serial_Numb  
     
   begin tran  
    UPDATE  ItemBatch SET    
    IB_Qty_Available = (IB_Qty_Available + @Quantity)    
    WHERE     
    IB_Item_Id = @IB_Item_Id     
    AND IB_Batch_Id = @IB_Batch_Id    
      
    delete from SaleTmpItems where Sale_Id = @Sale_Id and Item_Id = @IB_Item_Id     
    AND Batch_Id = @IB_Batch_Id and Serial_Numb = @Serial_Numb  
   commit tran  
  END    
     
  IF @Operation = 'DA'    
  BEGIN    
   select @Quantity = sum(Qty) from SaleTmpItems where Sale_Id = @Sale_Id and Item_Id = @IB_Item_Id     
   AND Batch_Id = @IB_Batch_Id  
    
   begin tran  
    UPDATE  ItemBatch SET    
    IB_Qty_Available = (IB_Qty_Available + @Quantity)    
    WHERE     
    IB_Item_Id = @IB_Item_Id     
    AND IB_Batch_Id = @IB_Batch_Id   
      
    delete from SaleTmpItems where Sale_Id = @Sale_Id and Item_Id = @IB_Item_Id     
    AND Batch_Id = @IB_Batch_Id  
   commit tran  
  END  
    
    END TRY    
        
 BEGIN CATCH    
 -- Whoops, there was an error    
  BEGIN    
   SET @sqlerror = 'Error while updating Quantity. ' + ERROR_MESSAGE()    
   RAISERROR (@sqlerror, 11, 1)    
  END    
 END CATCH    
    
     
END
GO
