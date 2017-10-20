USE [NewAppDb]
GO
/****** Object:  StoredProcedure [dbo].[WPFRetailxml]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[WPFRetailxml]  
(@Empxml  xml)  
as  
set nocount on  

declare  
	@idoc			int,
	@mydatetime		datetime

SET @mydatetime = GETDATE()

--execute WPFRetailxml
--'<ROOT>
--<Employee>
--<EmpId>1</EmpId>
--<EmpName>Hari</EmpName>
--<EmpDetails>
--	<EmpId>1</EmpId>
--	<DeptId>1</DeptId>
--	<Salary>1000</Salary>
--	<CreateDate>2001-01-01</CreateDate>
--	<LastModDate>2001-01-01</LastModDate>
--</EmpDetails>
--</Employee>
--</ROOT>'

select * from Employee
select * from EmpDetails

declare @tmpstr	varchar(max)

SET @tmpstr = CONVERT(VARCHAR(max), @Empxml);
SELECT @tmpstr; 

--return

 --pick up handle to XML document  
exec sp_xml_preparedocument @idoc output, @Empxml  

INSERT INTO Employee
SELECT * FROM OPENXML(@idoc, '/ROOT/Employee', 2)       
         WITH (EmpId int, EmpName VARCHAR(15))

INSERT INTO EmpDetails
SELECT * FROM OPENXML (@idoc, '/ROOT/Employee/EmpDetails',2)
      WITH (EmpId int, DeptId	int, Salary	decimal(18, 0), CreateDate datetime, LastModDate datetime)


--truncate table Employee
--truncate table EmpDetails
GO
