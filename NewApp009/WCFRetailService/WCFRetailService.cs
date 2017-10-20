using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using NewApp.BusinessTier.Models;
using WCFRetailService;

namespace WCFRetailService
{
    [ServiceContract]
    public interface IDataAccessService
    {
        // TODO: Add your service operations here
        [OperationContract]
        bool ConnectDB(out string errorMessage);

        [OperationContract]
        bool OpenDB(out string errorMessage);

        [OperationContract]
        bool CloseDB(string errorMessage, out string closeErrorMessage);

        [OperationContract]
        bool ExecuteSqlNonQry(out string errorMessage, string SqlStr);

        [OperationContract]
        bool ExecuteSqlQry(string SqlStr, out DataSet Ds, out string errorMessage);

        [OperationContract]
        bool ExecuteSPWithParamsRespDS(out DataSet Ds, out string errorMessage, string StoredProcName, params SqlParameter[] SPParameters);

        [OperationContract]
        bool ExecuteSPWithParamsNoResp(out string errorMessage, string StoredProcName, params SqlParameter[] SPParameters);

        [OperationContract]
        bool ExecuteSPForSearch(out DataSet Ds, out string errorMessage, string StoredProcName, string SearchStr);

        [OperationContract]
        bool ExecuteSPNoParamsWithRespDS(out DataSet Ds, out string errorMessage, string StoredProcName);

        [OperationContract]
        bool CheckDataAccess(out string errorMessage);

        [OperationContract]
        DataTable GetNormalTable();

        [OperationContract]
        bool GetNormalDataSet(out DataSet Ds);
    }

    [ServiceContract]
    public interface ICustomerFactory
    {
        [OperationContract]
        bool GetCustomers(string CustomerIdOrName, out string errorMessage, out Customer[] CustObjArray);

        [OperationContract(Name = "GetCustomersByCriteria")]
        bool GetCustomers(string CustId, string CustName, string Address, int IDProofTypeId, string IDProofValue, DateTime FromDateofBirth, DateTime ToDateofBirth, DateTime FromDateofStartRelationship, DateTime ToDateofStartRelationship, DateTime FromDateofExpiryRelationship, DateTime ToDateofExpiryRelationship, out string errorMessage, out Customer[] CustObjArray);

        [OperationContract]
        bool GetCustomerDetails(string CustomerIdOrName, out string errorMessage, out Customer CustObj);

        [OperationContract]
        bool GetCustomerBasicDetails(string CustomerIdOrName, out string errorMessage, out Customer CustObj);

        [OperationContract]
        bool AddCustomerDetails(Customer CustObj, out string errorMessage);

        [OperationContract]
        void ModifyCustomerDetails();

        [OperationContract]
        bool DeleteCustomerDetails(out string errorMessage, Customer CustObj);
    }

    [ServiceContract]
    public interface IDiscountFactory
    {
        [OperationContract]
        bool GetDiscountDetails(string DiscCode, out string errorMessage, out Discount DiscObj);

        [OperationContract]
        bool GetDiscountList(string DiscCode, out string errorMessage, out Discount[] DiscObjArray);

        [OperationContract]
        bool AddNewDiscount(Discount DiscObj, out string errorMessage);

        [OperationContract]
        void ModifyDiscDetails();

        [OperationContract]
        bool DeleteDiscDetails(Discount DiscObj, out string errorMessage);
    }

    [ServiceContract]
    public interface IDiscountItemFactory
    {
        [OperationContract]
        bool AddItemToDiscount(out string errorMessage, DiscountItem DiscItemObj);

        [OperationContract]
        void ModifyDiscountDetails();

        [OperationContract]
        bool DeleteDiscountDetails(out string errorMessage, DiscountItem DiscItemObj);

        [OperationContract]
        bool GetItemsNotLnkdToSpecificDiscCode(out DataTable ItemBatchList, out string errorMessage);
    }

    [ServiceContract]
    public interface ICustomerStatusFactory
    {
        [OperationContract]
        bool GetAllCustomerStatus(out CustomerStatus[] CustomerStatusObjArray, out string errorMessage);

        [OperationContract]
        bool GetCustomerStatus(int CustomerStatusId, out CustomerStatus CustomerStatus, out string errorMessage);
    }

    [ServiceContract]
    public interface ICustomerTypeFactory
    {
        [OperationContract]
        bool GetAllCustomerTypes(out CustomerType[] CustomerTypeObjArray, out string errorMessage);

        [OperationContract]
        bool GetCustomerType(int CustomerTypeId, out CustomerType CustomerType, out string errorMessage);
    }

    [ServiceContract]
    public interface IDiscountTypeFactory
    {
        [OperationContract]
        bool GetAllDiscountTypes(out DiscountType[] DiscountTypeObjArray, out string errorMessage);

        [OperationContract]
        bool GetDiscountType(int DiscountTypeId, out DiscountType DiscountType, out string errorMessage);
    }

    [ServiceContract]
    public interface IDiscountKindFactory
    {
        [OperationContract]
        bool GetAllDiscountKinds(out DiscountKind[] DiscountKindObjArray, out string errorMessage);

        [OperationContract]
        bool GetDiscountKind(int DiscountKindId, out DiscountKind DiscountKind, out string errorMessage);
    }

    [ServiceContract]
    public interface IGenderTypeFactory
    {
        [OperationContract]
        bool GetAllGenderTypes(out GenderType[] GenderTypeObjArray, out string errorMessage);

        [OperationContract]
        bool GetGenderType(int GenderTypeId, out GenderType GenderType, out string errorMessage);
    }

    [ServiceContract]
    public interface IIDProofTypeFactory
    {
        [OperationContract]
        bool GetAllIDProofTypes(out IDProofType[] IDProofTypeObjArray, out string errorMessage);

        [OperationContract]
        bool GetIDProofType(int IDProofTypeId, out IDProofType IDProofType, out string errorMessage);
    }

    [ServiceContract]
    public interface IManufacturerFactory
    {
        [OperationContract]
        bool GetManufacturers(string Manufacturer, out string errorMessage, out Manufacturer[] ManufObjArray);

        [OperationContract(Name = "GetManufacturersByCriteria")]
        bool GetManufacturers(string ManufId, string ManufName, string Address, string IDProofTypeId, string IDProofValue, DateTime FromDateofBirth, DateTime ToDateofBirth, DateTime FromDateofStartRelationship, DateTime ToDateofStartRelationship, DateTime FromDateofExpiryRelationship, DateTime ToDateofExpiryRelationship, out string errorMessage, out Manufacturer[] ManufObjArray);

        [OperationContract]
        bool GetManufacturerDetails(string Manufacturer, out string errorMessage, out Manufacturer ManufObj);

        [OperationContract]
        bool AddManufacturerDetails(out string errorMessage, Manufacturer ManufObj);

        [OperationContract]
        void ModifyManufacturerDetails();

        [OperationContract]
        bool DeleteManufacturerDetails(out string errorMessage, Manufacturer ManufObj);

        [OperationContract]
        bool AddManufacturerPhoto(int Manufid, out string errorMessage, Manufacturer ManufObj);

        [OperationContract]
        bool GetManufacturerPhoto(int Manufid, out byte[] imgBytes, out string errorMessage);
    }

    [ServiceContract]
    public interface IManufacturerStatusFactory
    {
        [OperationContract]
        bool GetAllManufacturerStatus(out ManufacturerStatus[] ManufacturerStatusObjArray, out string errorMessage);

        [OperationContract]
        bool GetManufacturerStatus(int ManufacturerStatusId, out ManufacturerStatus ManufStatus, out string errorMessage);
    }

    [ServiceContract]
    public interface ISaleFactory
    {
        [OperationContract]
        bool GetNewSaleId(out string Sale_Id, out string errorMessage);

        [OperationContract]
        bool CreateSale(Sale Sale, out string errorMessage);

        [OperationContract]
        bool GetSaleDetails(string SalesId, string CustId, string CustName, DateTime StartDateOfSale, DateTime EndDateOfSale, out Sale[] SaleObjArray, out string errorMessage);

        [OperationContract]
        bool GetUserSaleDetails(string SalesId, string UserId, string UserName, DateTime StartDateOfSale, DateTime EndDateOfSale, out Sale[] SaleObjArray, out string errorMessage);

        [OperationContract]
        bool SearchSaleDetails(string SalesId, string CustId, string CustName, DateTime StartDateOfSale, DateTime EndDateOfSale, out Sale[] SaleObjArray, out string errorMessage);

        [OperationContract]
        bool ComputeSaleAmounts(Sale SaleObj, out Sale NewSaleObj, out string errorMessage);
    }

    [ServiceContract]
    public interface ISaleItemFactory
    {
        [OperationContract]
        bool GetSaleItems(string SaleId, out SaleItem[] SaleItemObjArray, out string errorMessage);

        [OperationContract]
        bool ComputeSaleItemAmounts(SaleItem SaleItemObj, out SaleItem NewSaleItemObj, out string errorMessage);

        [OperationContract]
        bool UpdateQuantity(string SaleId, int SerialNumb, string ItemId, string BatchId, int Quantity, char Operation, out string errorMessage);
    }

    [ServiceContract]
    public interface IStoreItemFactory
    {
        [OperationContract]
        bool AddStoreItem(out string errorMessage, StoreItem StoreItemObj);

        [OperationContract]
        bool ModifyStoreItem(out string errorMessage);

        [OperationContract]
        bool DeleteStoreItem(out string errorMessage, StoreItem StoreItemObj);

        [OperationContract]
        bool DeleteStoreItemBatch(out string errorMessage, StoreItem StoreItemObj);

        [OperationContract(Name = "GetStoreItemByCriteria")]
        bool GetStoreItems(string ItemId, string ItemName, string BatchId, DateTime StartDateOfManuf, DateTime EndDateOfManuf, DateTime StartDateOfExp, DateTime EndDateOfExp, out string errorMessage, out StoreItem[] StoreItemObjArray);

        [OperationContract(Name = "GetStoreItemByItemIdOrName")]
        bool GetStoreItems(string ItemIdOrName, out string errorMessage, out StoreItem[] StoreItemObjArray);

        [OperationContract]
        bool GetStoreItemDetails(string ItemId, string BatchId, out string errorMessage, out StoreItem StoreItemObj);
        

        //[OperationContract]
        //bool ComputeAmountsPerUnit(out StoreItem StoreItemObj, out string errorMessage);
    }

    [ServiceContract]
    public interface ITaxFactory
    {
        [OperationContract]
        bool GetTaxDetails(string TaxCode, out string errorMessage, out Tax TaxObj);

        [OperationContract]
        bool GetTaxList(string TaxCode, out string errorMessage, out Tax[] TaxObjArray);

        [OperationContract]
        bool GetGlobalTaxesList(out string errorMessage, out Tax[] TaxObjArray);

        [OperationContract]
        bool AddNewTax(out string errorMessage, Tax TaxObj);

        [OperationContract]
        void ModifyTaxDetails();

        [OperationContract]
        bool DeleteTaxDetails(out string errorMessage, Tax TaxObj);
    }

    [ServiceContract]
    public interface ITaxItemFactory
    {
        [OperationContract]
        bool GetTaxItemList(string TaxCode, out string errorMessage, out TaxItem[] TaxItemObjArray);

        [OperationContract]
        bool AddItemToTaxCode(out string errorMessage, TaxItem TaxItemObj);

        [OperationContract]
        void ModifyTaxItemDetails();

        [OperationContract]
        bool DeleteTaxItemDetails(out string errorMessage, TaxItem TaxItemObj);

        [OperationContract]
        bool GetItemsNotLnkdToSpecificTaxCode(out DataTable ItemBatchList, out string errorMessage);
    }

    [ServiceContract]
    public interface ITaxKindFactory
    {
        [OperationContract]
        bool GetAllTaxKinds(out TaxKind[] TaxKindObjArray, out string errorMessage);

        [OperationContract]
        bool GetTaxKind(int TaxKindId, out TaxKind TaxKind, out string errorMessage);
    }

    [ServiceContract]
    public interface ITaxTypeFactory
    {
        [OperationContract]
        bool GetAllTaxTypes(out TaxType[] TaxTypeObjArray, out string errorMessage);

        [OperationContract]
        bool GetTaxType(int TaxTypeId, out TaxType TaxType, out string errorMessage);
    }

    [ServiceContract]
    public interface IUserFactory
    {
        [OperationContract]
        bool GetUsers(string User, out string errorMessage, out User[] UserObjArray);

        [OperationContract(Name = "GetUsersByCriteria")]
        bool GetUsers(string UserId, string UserName, string Address, string IDProofTypeId, string IDProofValue, DateTime FromDateofBirth, DateTime ToDateofBirth, DateTime FromDateofStartRelationship, DateTime ToDateofStartRelationship, DateTime FromDateofExpiryRelationship, DateTime ToDateofExpiryRelationship, out string errorMessage, out User[] UserObjArray);

        [OperationContract]
        bool GetUserSaleDetails(string UserId, out string errorMessage, out Sale[] SaleObjArray);

        [OperationContract]
        bool GetUserBasicDetails(string User, out string errorMessage, out User UserObj);

        [OperationContract]
        bool AddUserDetails(out string errorMessage, User UserObj);

        [OperationContract]
        void ModifyUserDetails();

        [OperationContract]
        bool DeleteUserDetails(out string errorMessage, User UserObj);

        [OperationContract]
        bool AddUserPhoto(int Userid, out string errorMessage, User UserObj);

        [OperationContract]
        bool GetUserPhoto(int Userid, out byte[] imgBytes, out string errorMessage);

        [OperationContract]
        bool AuthenticateUser(string User, string Password, out string errorMessage);
    }

    [ServiceContract]
    public interface IUserStatusFactory
    {
        [OperationContract]
        bool GetAllUserStatus(out UserStatus[] UserStatusObjArray, out string errorMessage);

        [OperationContract]
        bool GetUserStatus(int UserStatusId, out UserStatus UserStatus, out string errorMessage);
    }

    [ServiceContract]
    public interface IUserTypeFactory
    {
        [OperationContract]
        bool GetAllUserTypes(out UserType[] UserTypeObjArray, out string errorMessage);

        [OperationContract]
        bool GetUserType(int UserTypeId, out UserType UserType, out string errorMessage);
    }

    [ServiceContract]
    public interface IVendorFactory
    {
        [OperationContract]
        bool GetVendors(string Vendor, out string errorMessage, out Vendor[] VendorObjArray);

        [OperationContract(Name = "GetVendorsByCriteria")]
        bool GetVendors(string VendorId, string VendorName, string Address, string IDProofTypeId, string IDProofValue, DateTime FromDateofBirth, DateTime ToDateofBirth, DateTime FromDateofStartRelationship, DateTime ToDateofStartRelationship, DateTime FromDateofExpiryRelationship, DateTime ToDateofExpiryRelationship, out string errorMessage, out Vendor[] VendorObjArray);

        [OperationContract]
        bool GetVendorDetails(string Vendor, out string errorMessage, out Vendor VendorObj);

        [OperationContract]
        bool AddVendorDetails(out string errorMessage, Vendor VendorObj);

        [OperationContract]
        void ModifyVendorDetails();

        [OperationContract]
        bool DeleteVendorDetails(out string errorMessage, Vendor VendorObj);

        [OperationContract]
        bool AddVendorPhoto(int Vendorid, out string errorMessage, Vendor VendorObj);

        [OperationContract]
        bool GetVendorPhoto(int Vendorid, out byte[] imgBytes, out string errorMessage);
    }

    [ServiceContract]
    public interface IVendorStatusFactory
    {
        [OperationContract]
        bool GetAllVendorStatus(out VendorStatus[] VendorStatusObjArray, out string errorMessage);

        [OperationContract]
        bool GetVendorStatus(int VendorStatusId, out VendorStatus VendorStatus, out string errorMessage);
    }
}
