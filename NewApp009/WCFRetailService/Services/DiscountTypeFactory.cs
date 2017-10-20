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
    public class DiscountTypeFactory: IDiscountTypeFactory
    {
        SQLDataAccess DBObj;

        public DiscountTypeFactory()
        {
        }

        public bool GetAllDiscountTypes(out DiscountType[] DiscountTypeObjArray, out string errorMessage)
        {
            bool retval = false;

            errorMessage = null;
            DiscountTypeObjArray = null;
            List<DiscountType> DiscountTypeListObj = null;

            if (GetAllDiscountTypesFromDB(out DiscountTypeListObj, out errorMessage) == true)
            {
                DiscountTypeObjArray = DiscountTypeListObj.ToArray<DiscountType>();
                retval = true;
            }

            return retval;
        }

        public bool GetDiscountType(int DiscountTypeId, out DiscountType DiscountType, out string errorMessage)
        {
            bool retval = false;

            errorMessage = null;
            DiscountType = null;

            List<DiscountType> DiscountTypeListObj = new List<DiscountType>();

            try
            {
                if (GetAllDiscountTypesFromDB(out DiscountTypeListObj, out errorMessage) == true)
                {
                    if (DiscountTypeListObj != null)
                        if (DiscountTypeListObj.Count > 0)
                        {
                            foreach (DiscountType TmpDiscountType in DiscountTypeListObj)
                            {
                                if (DiscountTypeId == TmpDiscountType.DiscountTypeId)
                                {
                                    DiscountType = TmpDiscountType;
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

        private bool GetAllDiscountTypesFromDB(out List<DiscountType> AllDiscountTypeListObj, out string errorMessage)
        {
            errorMessage = null;
            bool retval = false;
            AllDiscountTypeListObj = null;

            DataSet Ds = new DataSet();

            try
            {
                DBObj = SQLDataAccess.GetDBObj(out errorMessage);
            }
            catch (Exception ex1)
            {
                errorMessage = ex1.Message;
            }

            if (DBObj.ExecuteSPNoParamsWithRespDS(out Ds, out errorMessage, "NewAppDb..GetAllDiscountTypes") == false)
            {
                if (errorMessage != null)
                    return retval;
            }
            else
            {
                DiscountType DiscountType;

                AllDiscountTypeListObj = new List<DiscountType>();

                foreach (DataRow dr in Ds.Tables[0].Rows)
                {
                    DiscountType = new DiscountType();

                    DiscountType.DiscountTypeId = Convert.ToInt32(dr[0].ToString());
                    DiscountType.DiscountTypeValue = dr[1].ToString();
                    DiscountType.DiscountTypeDescription = dr[2].ToString();

                    AllDiscountTypeListObj.Add(DiscountType);
                }
                retval = true;
            }

            return retval;
        }
    }
}
