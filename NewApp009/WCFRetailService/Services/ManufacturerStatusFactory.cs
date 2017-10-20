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
    public class ManufacturerStatusFactory: IManufacturerStatusFactory
    {
        SQLDataAccess DBObj;

        public ManufacturerStatusFactory()
        {
        }

        public bool GetAllManufacturerStatus(out ManufacturerStatus[] ManufacturerStatusObjArray, out string errorMessage)
        {
            bool retval = false;

            errorMessage = null;
            List<ManufacturerStatus> ManufacturerStatusListObj = null;
            ManufacturerStatusObjArray = null;

            if (GetAllManufacturerStatusFromDB(out ManufacturerStatusListObj, out errorMessage) == true)
            {
                ManufacturerStatusObjArray = ManufacturerStatusListObj.ToArray<ManufacturerStatus>();
                retval = true;
            }

            return retval;
        }

        public bool GetManufacturerStatus(int ManufacturerStatusId, out ManufacturerStatus ManufStatus, out string errorMessage)
        {
            bool retval = false;

            errorMessage = null;
            ManufStatus = null;

            List<ManufacturerStatus> ManufStatusListObj = new List<ManufacturerStatus>();

            try
            {
                if (GetAllManufacturerStatusFromDB(out ManufStatusListObj, out errorMessage) == true)
                {
                    if (ManufStatusListObj != null)
                        if (ManufStatusListObj.Count > 0)
                        {
                            foreach (ManufacturerStatus TmpManufacturerStatus in ManufStatusListObj)
                            {
                                if (ManufacturerStatusId == TmpManufacturerStatus.ManufacturerStatusId)
                                {
                                    ManufStatus = TmpManufacturerStatus;
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

        private bool GetAllManufacturerStatusFromDB(out List<ManufacturerStatus> AllManufacturerStatusListObj, out string errorMessage)
        {
            errorMessage = null;
            bool retval = false;
            AllManufacturerStatusListObj = null;

            DataSet Ds = new DataSet();

            try
            {
                DBObj = SQLDataAccess.GetDBObj(out errorMessage);
            }
            catch (Exception ex1)
            {
                errorMessage = ex1.Message;
            }

            if (DBObj.ExecuteSPNoParamsWithRespDS(out Ds, out errorMessage, "NewAppDb..GetAllManufacturerStatus") == false)
            {
                if (errorMessage != null)
                    return retval;
            }
            else
            {
                ManufacturerStatus ManufStatus;

                AllManufacturerStatusListObj = new List<ManufacturerStatus>();

                foreach (DataRow dr in Ds.Tables[0].Rows)
                {
                    ManufStatus = new ManufacturerStatus();

                    ManufStatus.ManufacturerStatusId = Convert.ToInt32(dr[0].ToString());
                    ManufStatus.ManufacturerStatusValue = dr[1].ToString();
                    ManufStatus.ManufacturerStatusDescription = dr[2].ToString();

                    AllManufacturerStatusListObj.Add(ManufStatus);
                }
                retval = true;
            }

            return retval;
        }
    }
}
