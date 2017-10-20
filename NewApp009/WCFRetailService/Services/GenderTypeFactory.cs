using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using WCFRetailService;
using NewApp.BusinessTier.Models;



namespace WCFRetailService
{
    public class GenderTypeFactory : GenderType, IGenderTypeFactory
    {
        List<GenderType> GenderTypeListObj;

        SQLDataAccess DBObj;  



        public GenderTypeFactory()
        {
            GenderTypeListObj = new List<GenderType>();
        }

        public bool GetAllGenderTypes(out GenderType[] GenderTypeObjArray, out string errorMessage)
        {
            bool retval = false;

            errorMessage = null;
            List<GenderType> GenderTypeListObj = null;
            GenderTypeObjArray = null;

            if (GetAllGenderTypesFromDB(out GenderTypeListObj, out errorMessage) == true)
            {
                GenderTypeObjArray = GenderTypeListObj.ToArray<GenderType>();
                retval = true;
            }

            return retval;
        }

        public bool GetGenderType(int GenderTypeId, out GenderType GenderType, out string errorMessage)
        {
            bool retval = false;

            errorMessage = null;
            GenderType = null;

            List<GenderType> GenderTypeListObj = new List<GenderType>();

            try
            {
                if (GetAllGenderTypesFromDB(out GenderTypeListObj, out errorMessage) == true)
                {
                    if (GenderTypeListObj != null)
                        if (GenderTypeListObj.Count > 0)
                        {
                            foreach (GenderType TmpGenderType in GenderTypeListObj)
                            {
                                if (GenderTypeId == TmpGenderType.GenderTypeId)
                                {
                                    GenderType = TmpGenderType;
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

        private bool GetAllGenderTypesFromDB(out List<GenderType> AllGenderTypeListObj, out string errorMessage)
        {
            errorMessage = null;
            bool retval = false;
            AllGenderTypeListObj = null;

            DataSet Ds = new DataSet();

            try
            {
                DBObj = SQLDataAccess.GetDBObj(out errorMessage);
            }
            catch (Exception ex1)
            {
                errorMessage = ex1.Message;
            }

            if (DBObj.ExecuteSPNoParamsWithRespDS(out Ds, out errorMessage, "NewAppDb..GetAllGenderTypes") == false)
            {
                if (errorMessage != null)
                    return retval;
            }
            else
            {
                GenderType GenderType;

                AllGenderTypeListObj = new List<GenderType>();

                foreach (DataRow dr in Ds.Tables[0].Rows)
                {
                    GenderType = new GenderType();

                    GenderType.GenderTypeId = Convert.ToInt32(dr[0].ToString());
                    GenderType.GenderTypeValue = dr[1].ToString();
                    GenderType.GenderTypeDescription = dr[2].ToString();

                    AllGenderTypeListObj.Add(GenderType);
                }
                retval = true;
            }

            return retval;
        }
    }
}
