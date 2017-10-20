USE [NewAppDb]
GO
/****** Object:  StoredProcedure [dbo].[SetupStaticData]    Script Date: 04/16/2015 18:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================        
-- Author:  <Author,,Name>        
-- Create date: <Create Date,,>        
-- Description: <Description,,>        
-- =============================================        
CREATE PROCEDURE [dbo].[SetupStaticData]        
AS        
        
BEGIN        
 -- SET NOCOUNT ON added to prevent extra result sets from        
 -- interfering with SELECT statements.        
 SET NOCOUNT ON;        

print 'Starting Cleanup Existing Static Data'
truncate table CustomerStatus
truncate table DiscTypes
truncate table GenderTypes
truncate table IDProofTypes
truncate table ManufacturerStatus
truncate table TaxKinds
truncate table TaxTypes
truncate table UserStatus
truncate table VendorStatus
print 'Completed Cleanup Existing Static Data'

 
print 'Starting Static Data Setup'
--Add Static Data
INSERT [dbo].[CustomerStatus] ([Cust_Status_Id], [Cust_Status_Value], [Cust_Status_Description]) VALUES (0, N'New', N'New Customer')
INSERT [dbo].[CustomerStatus] ([Cust_Status_Id], [Cust_Status_Value], [Cust_Status_Description]) VALUES (1, N'Active', N'Active Customer')
INSERT [dbo].[CustomerStatus] ([Cust_Status_Id], [Cust_Status_Value], [Cust_Status_Description]) VALUES (2, N'InActive', N'InActive Customer')
INSERT [dbo].[CustomerStatus] ([Cust_Status_Id], [Cust_Status_Value], [Cust_Status_Description]) VALUES (3, N'Barred', N'Barred Customer')
INSERT [dbo].[CustomerStatus] ([Cust_Status_Id], [Cust_Status_Value], [Cust_Status_Description]) VALUES (4, N'Closed', N'Closed Customer')

INSERT [DiscTypes] ([Disc_Type_Id], [Disc_Type_Name], [Disc_Type_Description]) VALUES (0, N'Amount', N'Amount')
INSERT [DiscTypes] ([Disc_Type_Id], [Disc_Type_Name], [Disc_Type_Description]) VALUES (1, N'Percentage', N'Percentage')

INSERT [dbo].[GenderTypes] ([Gender_Type_Id], [Gender_Type_Value], [Gender_Type_Description]) VALUES (0, N'Male', N'Male')
INSERT [dbo].[GenderTypes] ([Gender_Type_Id], [Gender_Type_Value], [Gender_Type_Description]) VALUES (1, N'Female', N'Female')

INSERT [dbo].[IDProofTypes] ([IDProof_Type_Id], [IDProof_Type_Value], [IDProof_Type_Description]) VALUES (0, N'PAN Card', N'PAN Card')
INSERT [dbo].[IDProofTypes] ([IDProof_Type_Id], [IDProof_Type_Value], [IDProof_Type_Description]) VALUES (1, N'Adhaar Card', N'Adhaar Card')
INSERT [dbo].[IDProofTypes] ([IDProof_Type_Id], [IDProof_Type_Value], [IDProof_Type_Description]) VALUES (2, N'Ration Card', N'Ration Card')
INSERT [dbo].[IDProofTypes] ([IDProof_Type_Id], [IDProof_Type_Value], [IDProof_Type_Description]) VALUES (3, N'Voter Id', N'Voter Id')
INSERT [dbo].[IDProofTypes] ([IDProof_Type_Id], [IDProof_Type_Value], [IDProof_Type_Description]) VALUES (4, N'Passport No', N'Passport No')

INSERT [dbo].[ManufacturerStatus] ([Manuf_Status_Id], [Manuf_Status_Value], [Manuf_Status_Description]) VALUES (0, N'Active', N'Active Manufacturer')
INSERT [dbo].[ManufacturerStatus] ([Manuf_Status_Id], [Manuf_Status_Value], [Manuf_Status_Description]) VALUES (1, N'InActive', N'InActive Manufacturer')
INSERT [dbo].[ManufacturerStatus] ([Manuf_Status_Id], [Manuf_Status_Value], [Manuf_Status_Description]) VALUES (2, N'Barred', N'Barred Manufacturer')
INSERT [dbo].[ManufacturerStatus] ([Manuf_Status_Id], [Manuf_Status_Value], [Manuf_Status_Description]) VALUES (3, N'Closed', N'Closed Manufacturer')

INSERT [TaxKinds] ([Tax_Kind_Id], [Tax_Kind_Name], [Tax_Kind_Description]) VALUES (0, N'Global', N'Global')
INSERT [TaxKinds] ([Tax_Kind_Id], [Tax_Kind_Name], [Tax_Kind_Description]) VALUES (1, N'Itemwise', N'Itemwise')

INSERT [TaxTypes] ([Tax_Type_Id], [Tax_Type_Name], [Tax_Type_Description]) VALUES (0, N'Amount', N'Amount')
INSERT [TaxTypes] ([Tax_Type_Id], [Tax_Type_Name], [Tax_Type_Description]) VALUES (1, N'Percentage', N'Percentage')

INSERT [dbo].[UserStatus] ([User_Status_Id], [User_Status_Value], [User_Status_Description]) VALUES (0, N'New', N'New User')
INSERT [dbo].[UserStatus] ([User_Status_Id], [User_Status_Value], [User_Status_Description]) VALUES (1, N'Active', N'Active User')
INSERT [dbo].[UserStatus] ([User_Status_Id], [User_Status_Value], [User_Status_Description]) VALUES (2, N'InActive', N'InActive User')
INSERT [dbo].[UserStatus] ([User_Status_Id], [User_Status_Value], [User_Status_Description]) VALUES (3, N'Barred', N'Barred User')
INSERT [dbo].[UserStatus] ([User_Status_Id], [User_Status_Value], [User_Status_Description]) VALUES (4, N'Closed', N'Closed User')

INSERT [dbo].[VendorStatus] ([Vendor_Status_Id], [Vendor_Status_Value], [Vendor_Status_Description]) VALUES (0, N'Active', N'Active Vendor')
INSERT [dbo].[VendorStatus] ([Vendor_Status_Id], [Vendor_Status_Value], [Vendor_Status_Description]) VALUES (1, N'InActive', N'InActive Vendor')
INSERT [dbo].[VendorStatus] ([Vendor_Status_Id], [Vendor_Status_Value], [Vendor_Status_Description]) VALUES (2, N'Barred', N'Barred Vendor')
INSERT [dbo].[VendorStatus] ([Vendor_Status_Id], [Vendor_Status_Value], [Vendor_Status_Description]) VALUES (3, N'Closed', N'Closed Vendor')

print 'Completed Static Data Setup'

END
GO
