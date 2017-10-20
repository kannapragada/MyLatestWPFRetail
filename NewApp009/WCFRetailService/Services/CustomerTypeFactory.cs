using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using WCFRetailService;
using NewApp.BusinessTier.Models;



namespace WCFRetailService
{
    public class CustomerTypeFactory : CustomerType, ICustomerTypeFactory
    {
        SQLDataAccess DBObj;


        public CustomerTypeFactory()
        {
        }

        public bool GetAllCustomerTypes(out CustomerType[] CustomerTypeObjArray, out string errorMessage)
        {
            bool retval = false;

            errorMessage = null;
            CustomerTypeObjArray = null;
            List<CustomerType> CustomerTypeListObj = null;

            if (GetAllCustomerTypesFromDB(out CustomerTypeListObj, out errorMessage) == true)
            {
                CustomerTypeObjArray = CustomerTypeListObj.ToArray<CustomerType>();
                retval = true;
            }

            return retval;
        }

        public bool GetCustomerType(int CustomerTypeId, out CustomerType CustomerType, out string errorMessage)
        {
            bool retval = false;

            errorMessage = null;
            CustomerType = null;

            List<CustomerType> CustomerTypeListObj = new List<CustomerType>();

            try
            {
                if (GetAllCustomerTypesFromDB(out CustomerTypeListObj, out errorMessage) == true)
                {
                    if (CustomerTypeListObj != null)
                        if (CustomerTypeListObj.Count > 0)
                        {
                            foreach (CustomerType TmpCustomerType in CustomerTypeListObj)
                            {
                                if (CustomerTypeId == TmpCustomerType.CustomerTypeId)
                                {
                                    CustomerType = TmpCustomerType;
                                    retval = true;
                                }
                            }
                        }
                }
            }
            catch (Exception ex1)
            {
                errorMessage = ex1.Message;
            }

            return retval;
        }

        private bool GetAllCustomerTypesFromDB(out List<CustomerType> AllCustomerTypeListObj, out string errorMessage)
        {
            errorMessage = null;
            bool retval = false;
            AllCustomerTypeListObj = null;

            DataSet Ds = new DataSet();

            try
            {
                DBObj = SQLDataAccess.GetDBObj(out errorMessage);
            }
            catch (Exception ex1)
            {
                errorMessage = ex1.Message;
            }

            if (DBObj.ExecuteSPNoParamsWithRespDS(out Ds, out errorMessage, "NewAppDb..GetAllCustomerTypes") == false)
            {
                if (errorMessage != null)
                    return retval;
            }
            else
            {
                CustomerType CustomerType;

                AllCustomerTypeListObj = new List<CustomerType>();

                foreach (DataRow dr in Ds.Tables[0].Rows)
                {
                    CustomerType = new CustomerType();

                    CustomerType.CustomerTypeId = Convert.ToInt32(dr[0].ToString());
                    CustomerType.CustomerTypeValue = dr[1].ToString();
                    CustomerType.CustomerTypeDescription = dr[2].ToString();

                    AllCustomerTypeListObj.Add(CustomerType);
                }
                retval = true;
            }

            return retval;
        }
    }
}
