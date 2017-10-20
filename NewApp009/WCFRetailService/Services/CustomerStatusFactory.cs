using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using NewApp.BusinessTier.Models;
using WCFRetailService;




namespace WCFRetailService
{
    public class CustomerStatusFactory : ICustomerStatusFactory
    {
        SQLDataAccess DBObj;

        public CustomerStatusFactory()
        {
        }

        public bool GetAllCustomerStatus(out CustomerStatus[] CustomerStatusObjArray, out string errorMessage)
        {
            bool retval = false;

            errorMessage = null;
            CustomerStatusObjArray = null;
            List<CustomerStatus> CustomerStatusListObj;

            if (GetAllCustomerStatusFromDB(out CustomerStatusListObj, out errorMessage) == true)
            {
                CustomerStatusObjArray = CustomerStatusListObj.ToArray<CustomerStatus>();
                retval = true;
            }

            return retval;
        }

        public bool GetCustomerStatus(int CustomerStatusId, out CustomerStatus CustomerStatus, out string errorMessage)
        {
            bool retval = false;

            errorMessage = null;
            CustomerStatus = null;

            List<CustomerStatus> CustomerStatusListObj = new List<CustomerStatus>();

            try
            {
                if (GetAllCustomerStatusFromDB(out CustomerStatusListObj, out errorMessage) == true)
                {
                    if (CustomerStatusListObj != null)
                        if (CustomerStatusListObj.Count > 0)
                        {
                            foreach (CustomerStatus TmpCustomerStatus in CustomerStatusListObj)
                            {
                                if (CustomerStatusId == TmpCustomerStatus.CustomerStatusId)
                                {
                                    CustomerStatus = TmpCustomerStatus;
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

        private bool GetAllCustomerStatusFromDB(out List<CustomerStatus> AllCustomerStatusListObj, out string errorMessage)
        {
            errorMessage = null;
            bool retval = false;
            AllCustomerStatusListObj = null;

            DataSet Ds = new DataSet();

            try
            {
                DBObj = SQLDataAccess.GetDBObj(out errorMessage);
            }
            catch (Exception ex1)
            {
                errorMessage = ex1.Message;
            }

            if (DBObj.ExecuteSPNoParamsWithRespDS(out Ds, out errorMessage, "NewAppDb..GetAllCustomerStatus") == false)
            {
                if (errorMessage != null)
                    return retval;
            }
            else
            {
                CustomerStatus CustomerStatus;

                AllCustomerStatusListObj = new List<CustomerStatus>();

                foreach (DataRow dr in Ds.Tables[0].Rows)
                {
                    CustomerStatus = new CustomerStatus();

                    CustomerStatus.CustomerStatusId = Convert.ToInt32(dr[0].ToString());
                    CustomerStatus.CustomerStatusValue = dr[1].ToString();
                    CustomerStatus.CustomerStatusDescription = dr[2].ToString();

                    AllCustomerStatusListObj.Add(CustomerStatus);
                }
                retval = true;
            }

            return retval;
        }
    }
}
