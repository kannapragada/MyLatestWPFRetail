using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewApp.BusinessTier.Models;
using NewApp.DiscountTypeFactorySvc;
using NewApp.DiscountKindFactorySvc;
using NewApp.CustomerStatusFactorySvc;
using NewApp.CustomerTypeFactorySvc;
using NewApp.GenderTypeFactorySvc;
using NewApp.IDProofTypeFactorySvc;
using NewApp.ManufStatusFactorySvc;
using NewApp.UserStatusFactorySvc;
using NewApp.UserTypeFactorySvc;
using NewApp.TaxKindFactorySvc;
using NewApp.TaxTypeFactorySvc;
using NewApp.VendorStatusFactorySvc;
using NewApp.UserFactorySvc;




namespace NewApp.BusinessTier.Common
{
    public class AppBusinessTierState
    {
        public AppBusinessTierState()
        {
        }

        public bool LoadTypeParameters(out string errorMessage)
        {
            bool retval = false;

            errorMessage = null;

            //Loading Type Parameters
            #region Loading Type Parameters


            try
            {
                DiscountTypeFactoryClient DiscountTypeFactoryClient = new DiscountTypeFactoryClient();
                DiscountKindFactoryClient DiscountKindFactoryClient = new DiscountKindFactoryClient();
                GenderTypeFactoryClient GenderTypeFactoryClient = new GenderTypeFactoryClient();
                IDProofTypeFactoryClient IDProofTypeFactoryClient = new IDProofTypeFactoryClient();
                TaxTypeFactoryClient TaxTypeFactoryClient = new TaxTypeFactoryClient();
                TaxKindFactoryClient TaxKindFactoryClient = new TaxKindFactoryClient();
                CustomerTypeFactoryClient CustomerTypeFactoryClient = new CustomerTypeFactoryClient();
                UserTypeFactoryClient UserTypeFactoryClient = new UserTypeFactoryClient();


                List<DiscountType> DiscTypeList = new List<DiscountType>();
                DiscountType[] DiscTypeArray;
                if (DiscountTypeFactoryClient.GetAllDiscountTypes(out DiscTypeArray, out errorMessage) == true)
                    if (DiscTypeArray != null)
                        DiscTypeList = DiscTypeArray.ToList<DiscountType>();
                    else
                        return retval;

                List<DiscountKind> DiscKindList = new List<DiscountKind>();
                DiscountKind[] DiscKindArray;
                if (DiscountKindFactoryClient.GetAllDiscountKinds(out DiscKindArray, out errorMessage) == true)
                    if (DiscKindArray != null)
                        DiscKindList = DiscKindArray.ToList<DiscountKind>();
                    else
                        return retval;

                List<GenderType> GenderTypeList = new List<GenderType>();
                GenderType[] GenderTypeArray;
                if (GenderTypeFactoryClient.GetAllGenderTypes(out GenderTypeArray, out errorMessage) == true)
                    if (GenderTypeArray != null)
                        GenderTypeList = GenderTypeArray.ToList<GenderType>();
                else
                    return retval;

                List<IDProofType> IDProofTypeList = new List<IDProofType>();
                IDProofType[] IDProofTypeArray;
                if (IDProofTypeFactoryClient.GetAllIDProofTypes(out IDProofTypeArray, out errorMessage) == true)
                    if (IDProofTypeArray != null)
                        IDProofTypeList = IDProofTypeArray.ToList<IDProofType>();
                else
                    return retval;

                List<TaxType> TaxTypeList = new List<TaxType>();
                TaxType[] TaxTypeArray;
                if (TaxTypeFactoryClient.GetAllTaxTypes(out TaxTypeArray, out errorMessage) == true)
                    if (TaxTypeArray != null)
                        TaxTypeList = TaxTypeArray.ToList<TaxType>();
                else
                    return retval;

                List<TaxKind> TaxKindList = new List<TaxKind>();
                TaxKind[] TaxKindArray;
                if (TaxKindFactoryClient.GetAllTaxKinds(out TaxKindArray, out errorMessage) == true)
                    if (TaxKindArray != null)
                        TaxKindList = TaxKindArray.ToList<TaxKind>();
                else
                    return retval;

                List<UserType> UserTypeList = new List<UserType>();
                UserType[] UserTypeArray;
                if (UserTypeFactoryClient.GetAllUserTypes(out UserTypeArray, out errorMessage) == true)
                    if (UserTypeArray != null)
                        UserTypeList = UserTypeArray.ToList<UserType>();
                else
                    return retval;

                BusinessTierState.SetValue("DiscountTypeList", DiscTypeList);
                BusinessTierState.SetValue("GenderTypeList", GenderTypeList);
                BusinessTierState.SetValue("IDProofTypeList", IDProofTypeList);
                BusinessTierState.SetValue("TaxTypeList", TaxTypeList);
                BusinessTierState.SetValue("TaxKindList", TaxKindList);
                //BusinessTierState.SetValue("CustomerTypeList", CustomerTypeList);
                BusinessTierState.SetValue("UserTypeList", UserTypeList);

                retval = true;
            }
            catch (Exception ex1)
            {
                errorMessage = ex1.Message.ToString();
            }

            #endregion


            return retval;
        }

        public bool LoadStatusParameters(out string errorMessage)
        {
            bool retval = false;

            errorMessage = null;

            //Loading Status Parameters  
            #region Loading Status Parameters


            try
            {
                CustomerStatusFactoryClient CustomerStatusFactoryClient = new CustomerStatusFactoryClient();
                ManufacturerStatusFactoryClient ManufStatusFactoryClient = new ManufacturerStatusFactoryClient();
                UserStatusFactoryClient UserStatusFactoryClient = new UserStatusFactoryClient();
                VendorStatusFactoryClient VendorStatusFactoryClient = new VendorStatusFactoryClient();


                List<CustomerStatus> CustomerStatusList = new List<CustomerStatus>();
                CustomerStatus[] CustomerStatusArray;
                if (CustomerStatusFactoryClient.GetAllCustomerStatus(out CustomerStatusArray, out errorMessage) == true)
                    if (CustomerStatusArray != null)
                        CustomerStatusList = CustomerStatusArray.ToList<CustomerStatus>();
                else
                    return retval;

                List<ManufacturerStatus> ManufacturerStatusList = new List<ManufacturerStatus>();
                ManufacturerStatus[] ManufacturerStatusArray;
                if (ManufStatusFactoryClient.GetAllManufacturerStatus(out ManufacturerStatusArray, out errorMessage) == true)
                    if (ManufacturerStatusArray != null)
                        ManufacturerStatusList = ManufacturerStatusArray.ToList<ManufacturerStatus>();
                else
                    return retval;

                List<UserStatus> UserStatusList = new List<UserStatus>();
                UserStatus[] UserStatusArray;
                if (UserStatusFactoryClient.GetAllUserStatus(out UserStatusArray, out errorMessage) == true)
                    if (UserStatusArray != null)
                        UserStatusList = UserStatusArray.ToList<UserStatus>();
                else
                    return retval;


                List<VendorStatus> VendorStatusList = new List<VendorStatus>();
                VendorStatus[] VendorStatusArray;
                if (VendorStatusFactoryClient.GetAllVendorStatus(out VendorStatusArray, out errorMessage) == true)
                    if (VendorStatusArray != null)
                        VendorStatusList = VendorStatusArray.ToList<VendorStatus>();
                else
                    return retval;

                BusinessTierState.SetValue("CustomerStatusList", CustomerStatusList);
                BusinessTierState.SetValue("ManufacturerStatusList", ManufacturerStatusList);
                BusinessTierState.SetValue("UserStatusList", UserStatusList);
                BusinessTierState.SetValue("VendorStatusList", VendorStatusList);

                retval = true;
            }
            catch (Exception ex1)
            {
                errorMessage = ex1.Message.ToString();
            }
            #endregion



            return retval;
        }

        public bool AuthenticateUser(string userid, string password, out string errorMessage)
        {
            bool retval = false;

            errorMessage = null;


            #region Authenticate User


            try
            {
                UserFactoryClient UserFactoryClientObj = new UserFactoryClient();


                if (UserFactoryClientObj.AuthenticateUser(out errorMessage, userid, password) == true)
                    retval = true;
            }
            catch (Exception ex1)
            {
                errorMessage = ex1.Message.ToString();
            }

            #endregion


            return retval;
        }

        public bool LoadUserProfileInformation(out string errorMessage)
        {
            bool retval = false;

            errorMessage = null;

            //Loading User Profile Information  
            #region User Profile Information


            #endregion


            retval = true;


            return retval;
        }
    }
}
