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
    public class IDProofTypeFactory : IDProofType, IIDProofTypeFactory
    {
        SQLDataAccess DBObj;

        public IDProofTypeFactory()
        {
        }

        public bool GetAllIDProofTypes(out IDProofType[] IDProofTypeObjArray, out string errorMessage)
        {
            bool retval = false;

            errorMessage = null;
            IDProofTypeObjArray = null;
            List<IDProofType> IDProofTypeListObj = null;

            if (GetAllIDProofTypesFromDB(out IDProofTypeListObj, out errorMessage) == true)
            {
                IDProofTypeObjArray = IDProofTypeListObj.ToArray<IDProofType>();
                retval = true;
            }

            return retval;
        }

        public bool GetIDProofType(int IDProofTypeId, out IDProofType IDProofType, out string errorMessage)
        {
            bool retval = false;

            errorMessage = null;
            IDProofType = null;

            List<IDProofType> IDProofTypeListObj = new List<IDProofType>();

            try
            {
                if (GetAllIDProofTypesFromDB(out IDProofTypeListObj, out errorMessage) == true)
                {
                    if (IDProofTypeListObj != null)
                        if (IDProofTypeListObj.Count > 0)
                        {
                            foreach (IDProofType TmpIDProofType in IDProofTypeListObj)
                            {
                                if (IDProofTypeId == TmpIDProofType.IDProofTypeId)
                                {
                                    IDProofType = TmpIDProofType;
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

        private bool GetAllIDProofTypesFromDB(out List<IDProofType> AllIDProofTypeListObj, out string errorMessage)
        {
            errorMessage = null;
            bool retval = false;
            AllIDProofTypeListObj = null;

            DataSet Ds = new DataSet();

            try
            {
                DBObj = SQLDataAccess.GetDBObj(out errorMessage);
            }
            catch (Exception ex1)
            {
                errorMessage = ex1.Message;
            }

            if (DBObj.ExecuteSPNoParamsWithRespDS(out Ds, out errorMessage, "NewAppDb..GetAllIDProofTypes") == false)
            {
                if (errorMessage != null)
                    return retval;
            }
            else
            {
                IDProofType IDProofType;

                AllIDProofTypeListObj = new List<IDProofType>();

                foreach (DataRow dr in Ds.Tables[0].Rows)
                {
                    IDProofType = new IDProofType();

                    IDProofType.IDProofTypeId = Convert.ToInt32(dr[0].ToString());
                    IDProofType.IDProofTypeValue = dr[1].ToString();
                    IDProofType.IDProofTypeDescription = dr[2].ToString();

                    AllIDProofTypeListObj.Add(IDProofType);
                }
                retval = true;
            }

            return retval;
        }
    }
}
