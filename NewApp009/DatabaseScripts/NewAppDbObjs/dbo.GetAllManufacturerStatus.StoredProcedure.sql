USE [NewAppDb]
GO
/****** Object:  StoredProcedure [dbo].[GetAllManufacturerStatus]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================    
-- Author:  <Author,,Name>    
-- Create date: <Create Date,,>    
-- Description: <Description,,>    
-- =============================================    
CREATE PROCEDURE [dbo].[GetAllManufacturerStatus]    
AS    
    
BEGIN    
 -- SET NOCOUNT ON added to prevent extra result sets from    
 -- interfering with SELECT statements.    
 SET NOCOUNT ON;    
     
select * from ManufacturerStatus order by Manuf_Status_Id


 
END
GO
