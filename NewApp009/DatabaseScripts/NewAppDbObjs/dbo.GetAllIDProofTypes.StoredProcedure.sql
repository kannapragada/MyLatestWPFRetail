USE [NewAppDb]
GO
/****** Object:  StoredProcedure [dbo].[GetAllIDProofTypes]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================    
-- Author:  <Author,,Name>    
-- Create date: <Create Date,,>    
-- Description: <Description,,>    
-- =============================================    
CREATE PROCEDURE [dbo].[GetAllIDProofTypes]    
AS    
    
BEGIN    
 -- SET NOCOUNT ON added to prevent extra result sets from    
 -- interfering with SELECT statements.    
 SET NOCOUNT ON;    
     
select * from IDProofTypes order by IDProof_Type_Id


 
END
GO
