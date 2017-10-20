using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.ComponentModel;
using NewApp.BusinessTier.Models;
using WCFRetailService;




namespace WCFRetailService
{
    public class DiscountItemFactory : DiscountItem, IDiscountItemFactory
    {
        //Declarations
        # region Declarations



            SQLDataAccess DBObj;

        #endregion

        //Constructor
        public DiscountItemFactory()
        {
        }

        //Member Methods 
        #region MemberMethods

            public bool AddItemToDiscount(out string errorMessage, DiscountItem DiscItemObj)
            {
                bool retval = false;
                errorMessage = null;

                try
                {
                    DBObj = SQLDataAccess.GetDBObj(out errorMessage);

                    if (errorMessage != null)
                        return retval;

                    SqlParameter[] SQLParamArray = new SqlParameter[7];
                    SqlParameter parameter = new SqlParameter();

                    parameter = new SqlParameter("@DiscI_Disc_Code", SqlDbType.VarChar);
                    parameter.Value = (object)DiscItemObj.DiscountCode ?? DBNull.Value;
                    SQLParamArray[0] = parameter;

                    parameter = new SqlParameter("@DiscI_Item_Id", SqlDbType.VarChar);
                    parameter.Value = (object)DiscItemObj.DiscItemId ?? DBNull.Value;
                    SQLParamArray[1] = parameter;

                    parameter = new SqlParameter("@DiscI_Item_Batch_Id", SqlDbType.VarChar);
                    parameter.Value = (object)DiscItemObj.DiscItemBatchId ?? DBNull.Value;
                    SQLParamArray[2] = parameter;

                    parameter = new SqlParameter("@DiscI_Start_Date", SqlDbType.DateTime);
                    parameter.Value = (object)DiscItemObj.DiscItemStartDate ?? DBNull.Value;
                    SQLParamArray[3] = parameter;

                    parameter = new SqlParameter("@DiscI_End_Date", SqlDbType.DateTime);
                    parameter.Value = (object)DiscItemObj.DiscItemEndDate ?? DBNull.Value;
                    SQLParamArray[4] = parameter;

                    parameter = new SqlParameter("@DiscI_Remarks", SqlDbType.VarChar);
                    parameter.Value = (object)DiscItemObj.DiscRemarks ?? DBNull.Value;
                    SQLParamArray[5] = parameter;

                    parameter = new SqlParameter("@DiscI_Create_Date", SqlDbType.DateTime);
                    parameter.Value = (object)DiscItemObj.DiscCreateDate ?? DBNull.Value;
                    SQLParamArray[6] = parameter;

                    if (DBObj.ExecuteSPWithParamsNoResp(out errorMessage, "NewAppDb..AddItemToDiscount", SQLParamArray) == false)
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

            public void ModifyDiscountDetails()
            {
                throw new System.NotSupportedException ();
            }

            public bool DeleteDiscountDetails(out string errorMessage, DiscountItem DiscItemObj)
            {

                bool retval = false;
                errorMessage = null;

                try
                {
                    DBObj = SQLDataAccess.GetDBObj(out errorMessage);

                    if (errorMessage != null)
                        return retval;

                    SqlParameter[] SQLParamArray = new SqlParameter[3];

                    SqlParameter parameter = new SqlParameter("@Disc_Code", SqlDbType.VarChar);
                    parameter.Value = (object)DiscItemObj.DiscountCode ?? DBNull.Value;
                    SQLParamArray[0] = parameter;

                    parameter = new SqlParameter("@Item_Id", SqlDbType.VarChar);
                    parameter.Value = (object)DiscItemObj.DiscItemId ?? DBNull.Value;
                    SQLParamArray[1] = parameter;

                    parameter = new SqlParameter("@Batch_Id", SqlDbType.VarChar);
                    parameter.Value = (object)DiscItemObj.DiscItemBatchId ?? DBNull.Value;
                    SQLParamArray[2] = parameter;

                    if (DBObj.ExecuteSPWithParamsNoResp(out errorMessage, "NewAppDb..DeleteDiscountItem", SQLParamArray) == false)
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

            public bool GetItemsNotLnkdToSpecificDiscCode(out DataTable ItemBatchList, out string errorMessage)
            {
                errorMessage = null;
                bool retval = false;

                DataSet Ds =  new DataSet();

                Ds = null;

                ItemBatchList = new DataTable();

                try
                {
                    DBObj = SQLDataAccess.GetDBObj(out errorMessage);

                    if (errorMessage != null)
                        return retval;

                    SqlParameter[] SQLParamArray = new SqlParameter[1];

                    SqlParameter parameter = new SqlParameter("@DiscCode", SqlDbType.VarChar);
                    parameter.Value = (object)this.DiscountCode ?? DBNull.Value;
                    SQLParamArray[0] = parameter;

                    if (DBObj.ExecuteSPWithParamsRespDS(out Ds, out errorMessage, "NewAppDb..GetItemsNotLnkdToSpecificDiscCode", SQLParamArray) == false)
                    {
                        if (errorMessage != null)
                            return retval;
                    }
                    else
                    {
                        ItemBatchList = (DataTable)Ds.Tables[0];
                        retval = true;
                    }

                    Ds = null;
                }
                catch (Exception ex1)
                {
                    errorMessage = ex1.Message;
                }
                finally
                {
                    if (Ds != null)
                        Ds.Dispose();
                }

                return retval;
            }

        #endregion
    }
}
