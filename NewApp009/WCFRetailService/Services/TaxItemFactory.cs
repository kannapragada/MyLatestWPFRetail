using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
using NewApp.BusinessTier.Models;
using WCFRetailService;




namespace WCFRetailService
{
    public class TaxItemFactory : TaxItem, ITaxItemFactory
    {
        //Declarations
        # region Declarations


            SQLDataAccess DBObj;

        #endregion

        //Constructor
        public TaxItemFactory() 
        {
        }

        //Member Methods  
        #region MemberMethods

            public bool GetTaxItemList(string TaxCode, out string errorMessage, out TaxItem[] TaxItemObjArray)
            {
                DataSet Ds;
                DataRow Dr;
                TaxItem TaxItemObj;

                errorMessage = null;

                bool retval = false;

                List<TaxItem> TaxItemObjList = null;
                TaxItemObjArray = null;

                SqlParameter[] SQLParamArray = new SqlParameter[1];

                SqlParameter parameter = new SqlParameter("@TaxCode", SqlDbType.VarChar);
                parameter.Value = TaxCode;
                SQLParamArray[0] = parameter;

                try
                {
                    DBObj = SQLDataAccess.GetDBObj(out errorMessage);

                    if (errorMessage != null)
                        return retval;

                    Ds = new DataSet();

                    if (DBObj.ExecuteSPWithParamsRespDS(out Ds, out errorMessage, "NewAppDb..GetTaxItemList", SQLParamArray) == false)
                    {
                        if (errorMessage != null)
                            return retval;
                    }

                    if (Ds.Tables.Count <= 0)
                        return retval;

                    for (int i = 0; i < Ds.Tables[1].Rows.Count; i++)
                    {
                        Dr = Ds.Tables[0].Rows[i];

                        if (Dr[0].ToString() == Dr[0].ToString())
                        {
                            TaxItemObj = new TaxItem();

                            TaxItemObj.TaxCode = Dr[0].ToString();
                            TaxItemObj.ItemId = Dr[1].ToString();
                            TaxItemObj.BatchId = Dr[2].ToString();
                            TaxItemObj.StartDate = Convert.ToDateTime(Dr[4].ToString());
                            TaxItemObj.EndDate = Convert.ToDateTime(Dr[5].ToString());
                            TaxItemObj.Remarks = Dr[6].ToString();
                            TaxItemObj.CreateDate = Convert.ToDateTime(Dr[7].ToString());
                            if ((Dr[8].ToString() != null) && (Dr[8].ToString() != ""))
                                TaxItemObj.LastModDate = Convert.ToDateTime(Dr[8].ToString());

                            TaxItemObjList.Add(TaxItemObj);
                        }
                    }

                    retval = true;
                }
                catch (Exception ex1)
                {
                    errorMessage = ex1.Message;
                }


                return retval;
            }

            public bool AddItemToTaxCode(out string errorMessage, TaxItem TaxItemObj)
            {
                bool retval = false;
                errorMessage = null;

                try
                {
                    DBObj = SQLDataAccess.GetDBObj(out errorMessage);

                    if (errorMessage != null)
                        return retval;

                    SqlParameter[] SQLParamArray = new SqlParameter[7];


                    SqlParameter parameter = new SqlParameter("@Tax_Code", SqlDbType.VarChar);
                    parameter.Value = (object)TaxItemObj.TaxCode ?? DBNull.Value;
                    SQLParamArray[0] = parameter;

                    parameter = new SqlParameter("@Item_Id", SqlDbType.VarChar);
                    parameter.Value = (object)TaxItemObj.ItemId ?? DBNull.Value;
                    SQLParamArray[1] = parameter;

                    parameter = new SqlParameter("@Batch_Id", SqlDbType.VarChar);
                    parameter.Value = (object)TaxItemObj.BatchId ?? DBNull.Value;
                    SQLParamArray[2] = parameter;

                    parameter = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    parameter.Value = (object)TaxItemObj.Remarks ?? DBNull.Value;
                    SQLParamArray[3] = parameter;

                    parameter = new SqlParameter("@Start_Date", SqlDbType.DateTime);
                    parameter.Value = (object)TaxItemObj.StartDate ?? DBNull.Value;
                    SQLParamArray[4] = parameter;

                    parameter = new SqlParameter("@End_Date", SqlDbType.DateTime);
                    parameter.Value = (object)TaxItemObj.EndDate ?? DBNull.Value;
                    SQLParamArray[5] = parameter;

                    parameter = new SqlParameter("@Create_Date", SqlDbType.DateTime);
                    parameter.Value = (object)TaxItemObj.CreateDate ?? DBNull.Value;
                    SQLParamArray[6] = parameter;

                    DataSet ds = new DataSet();

                    errorMessage = null;

                    if (DBObj.ExecuteSPWithParamsNoResp(out errorMessage, "NewAppDb..AddItemToTaxCode", SQLParamArray) == false)
                    {
                        if (errorMessage != null)
                            return retval;
                    }
                    else
                        retval = true;

                }
                catch (Exception ex1)
                {
                    errorMessage = ex1.Message;
                }


                return retval;
            }

            public void ModifyTaxItemDetails(){throw new System.NotSupportedException ();}

            public bool DeleteTaxItemDetails(out string errorMessage, TaxItem TaxItemObj)
            {

                bool retval = false;
                errorMessage = null;

                try
                {
                    DBObj = SQLDataAccess.GetDBObj(out errorMessage);
                }
                catch (Exception ex1)
                {
                    errorMessage = ex1.Message;
                }

                SqlParameter[] SQLParamArray = new SqlParameter[3];

                SqlParameter parameter = new SqlParameter("@Tax_Code", SqlDbType.VarChar);
                parameter.Value = (object)TaxItemObj.TaxCode ?? DBNull.Value;
                SQLParamArray[0] = parameter;

                parameter = new SqlParameter("@Item_Id", SqlDbType.VarChar);
                parameter.Value = (object)TaxItemObj.ItemId ?? DBNull.Value;
                SQLParamArray[1] = parameter;

                parameter = new SqlParameter("@Batch_Id", SqlDbType.VarChar);
                parameter.Value = (object)TaxItemObj.BatchId ?? DBNull.Value;
                SQLParamArray[2] = parameter;

                if (DBObj.ExecuteSPWithParamsNoResp(out errorMessage, "NewAppDb..DeleteTaxItem", SQLParamArray) == false)
                {
                    if (errorMessage != null)
                        return retval;
                }
                else
                    retval = true;

                return retval;
            }

            public bool GetItemsNotLnkdToSpecificTaxCode(out DataTable ItemBatchList, out string errorMessage)
            {
                errorMessage = null;
                bool retval = false;

                DataSet Ds = new DataSet();

                ItemBatchList = new DataTable();

                try
                {
                    DBObj = SQLDataAccess.GetDBObj(out errorMessage);
                }
                catch (Exception ex1)
                {
                    errorMessage = ex1.Message;
                }

                SqlParameter[] SQLParamArray = new SqlParameter[1];

                SqlParameter parameter = new SqlParameter("@TaxCode", SqlDbType.VarChar);
                parameter.Value = (object)this.TaxCode ?? DBNull.Value;
                SQLParamArray[0] = parameter;

                if (DBObj.ExecuteSPWithParamsRespDS(out Ds, out errorMessage, "NewAppDb..GetItemsNotLnkdToSpecificTaxCode", SQLParamArray) == false)
                {
                    if (errorMessage != null)
                        return retval;
                }
                else
                {
                    ItemBatchList = (DataTable)Ds.Tables[0];
                    retval = true;
                }

                return retval;
            }

        #endregion
    }
}
