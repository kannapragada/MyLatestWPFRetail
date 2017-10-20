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
    public class TaxKindFactory : TaxKind, ITaxKindFactory
    {
        SQLDataAccess DBObj;

        public TaxKindFactory()
        {
        }

        public bool GetAllTaxKinds(out TaxKind[] TaxKindObjArray, out string errorMessage)
        {
            bool retval = false;

            errorMessage = null;
            TaxKindObjArray = null;
            List<TaxKind> TaxKindListObj = null;

            if (GetAllTaxKindsFromDB(out TaxKindListObj, out errorMessage) == true)
            {
                TaxKindObjArray = TaxKindListObj.ToArray<TaxKind>();
                retval = true;
            }

            return retval;
        }

        public bool GetTaxKind(int TaxKindId, out TaxKind TaxKind, out string errorMessage)
        {
            bool retval = false;

            errorMessage = null;
            TaxKind = null;

            List<TaxKind> TaxKindListObj = new List<TaxKind>();

            try
            {
                if (GetAllTaxKindsFromDB(out TaxKindListObj, out errorMessage) == true)
                {
                    if (TaxKindListObj != null)
                        if (TaxKindListObj.Count > 0)
                        {
                            foreach (TaxKind TmpTaxKind in TaxKindListObj)
                            {
                                if (TaxKindId == TmpTaxKind.TaxKindId)
                                {
                                    TaxKind = TmpTaxKind;
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

        private bool GetAllTaxKindsFromDB(out List<TaxKind> AllTaxKindListObj, out string errorMessage)
        {
            errorMessage = null;
            bool retval = false;
            AllTaxKindListObj = null;

            DataSet Ds = new DataSet();

            try
            {
                DBObj = SQLDataAccess.GetDBObj(out errorMessage);
            }
            catch (Exception ex1)
            {
                errorMessage = ex1.Message;
            }

            if (DBObj.ExecuteSPNoParamsWithRespDS(out Ds, out errorMessage, "NewAppDb..GetAllTaxKinds") == false)
            {
                if (errorMessage != null)
                    return retval;
            }
            else
            {
                TaxKind TaxKind;

                AllTaxKindListObj = new List<TaxKind>();

                foreach (DataRow dr in Ds.Tables[0].Rows)
                {
                    TaxKind = new TaxKind();

                    TaxKind.TaxKindId = Convert.ToInt32(dr[0].ToString());
                    TaxKind.TaxKindValue = dr[1].ToString();
                    TaxKind.TaxKindDescription = dr[2].ToString();

                    AllTaxKindListObj.Add(TaxKind);
                }
                retval = true;
            }

            return retval;
        }
    }
}
