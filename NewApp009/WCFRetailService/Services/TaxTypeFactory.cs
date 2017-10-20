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
    public class TaxTypeFactory : ITaxTypeFactory
    {
        SQLDataAccess DBObj;


        public TaxTypeFactory()
        {
        }

        public bool GetAllTaxTypes(out TaxType[] TaxTypeObjArray, out string errorMessage)
        {
            bool retval = false;

            errorMessage = null;
            TaxTypeObjArray = null;
            List<TaxType> TaxTypeListObj = null;

            if (GetAllTaxTypesFromDB(out TaxTypeListObj, out errorMessage) == true)
            {
                TaxTypeObjArray = TaxTypeListObj.ToArray<TaxType>();
                retval = true;
            }

            return retval;
        }

        public bool GetTaxType(int TaxTypeId, out TaxType TaxType, out string errorMessage)
        {
            bool retval = false;

            errorMessage = null;
            TaxType = null;

            List<TaxType> TaxTypeListObj = new List<TaxType>();

            try
            {
                if (GetAllTaxTypesFromDB(out TaxTypeListObj, out errorMessage) == true)
                {
                    if (TaxTypeListObj != null)
                        if (TaxTypeListObj.Count > 0)
                        {
                            foreach (TaxType TmpTaxType in TaxTypeListObj)
                            {
                                if (TaxTypeId == TmpTaxType.TaxTypeId)
                                {
                                    TaxType = TmpTaxType;
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

        private bool GetAllTaxTypesFromDB(out List<TaxType> AllTaxTypeListObj, out string errorMessage)
        {
            errorMessage = null;
            bool retval = false;
            AllTaxTypeListObj = null;

            DataSet Ds = new DataSet();

            try
            {
                DBObj = SQLDataAccess.GetDBObj(out errorMessage);
            }
            catch (Exception ex1)
            {
                errorMessage = ex1.Message;
            }

            if (DBObj.ExecuteSPNoParamsWithRespDS(out Ds, out errorMessage, "NewAppDb..GetAllTaxTypes") == false)
            {
                if (errorMessage != null)
                    return retval;
            }
            else
            {
                TaxType TaxType;

                AllTaxTypeListObj = new List<TaxType>();

                foreach (DataRow dr in Ds.Tables[0].Rows)
                {
                    TaxType = new TaxType();

                    TaxType.TaxTypeId = Convert.ToInt32(dr[0].ToString());
                    TaxType.TaxTypeValue = dr[1].ToString();
                    TaxType.TaxTypeDescription = dr[2].ToString();

                    AllTaxTypeListObj.Add(TaxType);
                }
                retval = true;
            }

            return retval;
        }
    }
}
