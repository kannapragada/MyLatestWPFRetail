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
    public class DiscountKindFactory: IDiscountKindFactory
    {
        SQLDataAccess DBObj;

        public DiscountKindFactory()
        {
        }

        public bool GetAllDiscountKinds(out DiscountKind[] DiscountKindObjArray, out string errorMessage)
        {
            bool retval = false;

            errorMessage = null;
            DiscountKindObjArray = null;
            List<DiscountKind> DiscountKindListObj = null;

            if (GetAllDiscountKindsFromDB(out DiscountKindListObj, out errorMessage) == true)
            {
                DiscountKindObjArray = DiscountKindListObj.ToArray<DiscountKind>();
                retval = true;
            }

            return retval;
        }

        public bool GetDiscountKind(int DiscountKindId, out DiscountKind DiscountKind, out string errorMessage)
        {
            bool retval = false;

            errorMessage = null;
            DiscountKind = null;

            List<DiscountKind> DiscountKindListObj = new List<DiscountKind>();

            try
            {
                if (GetAllDiscountKindsFromDB(out DiscountKindListObj, out errorMessage) == true)
                {
                    if (DiscountKindListObj != null)
                        if (DiscountKindListObj.Count > 0)
                        {
                            foreach (DiscountKind TmpDiscountKind in DiscountKindListObj)
                            {
                                if (DiscountKindId == TmpDiscountKind.DiscountKindId)
                                {
                                    DiscountKind = TmpDiscountKind;
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

        private bool GetAllDiscountKindsFromDB(out List<DiscountKind> AllDiscountKindListObj, out string errorMessage)
        {
            errorMessage = null;
            bool retval = false;
            AllDiscountKindListObj = null;

            DataSet Ds = new DataSet();

            try
            {
                DBObj = SQLDataAccess.GetDBObj(out errorMessage);
            }
            catch (Exception ex1)
            {
                errorMessage = ex1.Message;
            }

            if (DBObj.ExecuteSPNoParamsWithRespDS(out Ds, out errorMessage, "NewAppDb..GetAllDiscountKinds") == false)
            {
                if (errorMessage != null)
                    return retval;
            }
            else
            {
                DiscountKind DiscountKind;

                AllDiscountKindListObj = new List<DiscountKind>();

                foreach (DataRow dr in Ds.Tables[0].Rows)
                {
                    DiscountKind = new DiscountKind();

                    DiscountKind.DiscountKindId = Convert.ToInt32(dr[0].ToString());
                    DiscountKind.DiscountKindValue = dr[1].ToString();
                    DiscountKind.DiscountKindDescription = dr[2].ToString();

                    AllDiscountKindListObj.Add(DiscountKind);
                }
                retval = true;
            }

            return retval;
        }
    }
}
