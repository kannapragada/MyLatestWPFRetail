USE [NewAppDb]
GO
/****** Object:  StoredProcedure [dbo].[My_XMLOrder]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[My_XMLOrder]  
(@OrderDoc  ntext)  
as  
set nocount on  

declare  
  @idoc    int

-- pick up handle to XML document  
exec sp_xml_preparedocument @idoc output, @OrderDoc  

SELECT *
FROM OPENXML (@idoc, '/ROOT/Sale/ItemDetails',1)
      WITH (SaleID		int			'../@SaleID',
            CustomerID	varchar(10) '../@CustomerID',
            ItemCode	int			'@ItemCode',
            Qty			int			'@Quantity')
GO
