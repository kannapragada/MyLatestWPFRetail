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
    public class VendorStatusFactory: IVendorStatusFactory
    {
        SQLDataAccess DBObj;

        public VendorStatusFactory()
        {
        }

        public bool GetAllVendorStatus(out VendorStatus[] VendorStatusObjArray, out string errorMessage)
        {
            bool retval = false;

            errorMessage = null;
            VendorStatusObjArray = null;
            List<VendorStatus> VendorStatusListObj = null;

            if (GetAllVendorStatusFromDB(out VendorStatusListObj, out errorMessage) == true)
            {
                VendorStatusObjArray = VendorStatusListObj.ToArray<VendorStatus>();
                retval = true;
            }

            return retval;
        }

        public bool GetVendorStatus(int VendorStatusId, out VendorStatus VendorStatus, out string errorMessage)
        {
            bool retval = false;

            errorMessage = null;
            VendorStatus = null;

            List<VendorStatus> VendorStatusListObj = new List<VendorStatus>();

            try
            {
                if (GetAllVendorStatusFromDB(out VendorStatusListObj, out errorMessage) == true)
                {
                    if (VendorStatusListObj != null)
                        if (VendorStatusListObj.Count > 0)
                        {
                            foreach (VendorStatus TmpVendorStatus in VendorStatusListObj)
                            {
                                if (VendorStatusId == TmpVendorStatus.VendorStatusId)
                                {
                                    VendorStatus = TmpVendorStatus;
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

        private bool GetAllVendorStatusFromDB(out List<VendorStatus> AllVendorStatusListObj, out string errorMessage)
        {
            errorMessage = null;
            bool retval = false;
            AllVendorStatusListObj = null;

            DataSet Ds = new DataSet();

            try
            {
                DBObj = SQLDataAccess.GetDBObj(out errorMessage);
            }
            catch (Exception ex1)
            {
                errorMessage = ex1.Message;
            }

            if (DBObj.ExecuteSPNoParamsWithRespDS(out Ds, out errorMessage, "NewAppDb..GetAllVendorStatus") == false)
            {
                if (errorMessage != null)
                    return retval;
            }
            else
            {
                VendorStatus VendorStatus;

                AllVendorStatusListObj = new List<VendorStatus>();

                foreach (DataRow dr in Ds.Tables[0].Rows)
                {
                    VendorStatus = new VendorStatus();

                    VendorStatus.VendorStatusId = Convert.ToInt32(dr[0].ToString());
                    VendorStatus.VendorStatusValue = dr[1].ToString();
                    VendorStatus.VendorStatusDescription = dr[2].ToString();

                    AllVendorStatusListObj.Add(VendorStatus);
                }
                retval = true;
            }

            return retval;
        }
    }
}
