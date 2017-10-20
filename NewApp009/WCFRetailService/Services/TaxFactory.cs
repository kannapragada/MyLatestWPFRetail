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
    public class TaxFactory : Tax, ITaxFactory
    {
        //Declarations
        # region Declarations

        SQLDataAccess DBObj;

        #endregion

        //Constructor
        public TaxFactory()
        {
        }


        //Member Methods  
        #region MemberMethods

            public bool GetTaxDetails(string TaxCode, out string errorMessage, out Tax TaxObj)
            {
                DataSet Ds;
                DataRow Dr;
                TaxItem TaxItemObj;
                
                TaxObj = null;
                errorMessage = null;

                bool retval = false;

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


                    if (DBObj.ExecuteSPWithParamsRespDS(out Ds, out errorMessage, "NewAppDb..GetTaxDetails", SQLParamArray) == false)
                    {
                        if (errorMessage != null)
                            return retval;
                    }
                    else
                    {
                        if (Ds.Tables[0].Rows.Count > 0)
                        {
                            TaxObj = new Tax();

                            Dr = Ds.Tables[0].Rows[0];

                            TaxObj.TaxCode = Dr[0].ToString();
                            TaxObj.TaxName = Dr[1].ToString();
                            TaxObj.TaxDescription = Dr[2].ToString();
                            TaxKindFactory TaxKindFactory = new TaxKindFactory();
                            TaxKind TaxKind = new TaxKind();
                            if (TaxKindFactory.GetTaxKind(Convert.ToInt32(Dr[3].ToString()), out TaxKind, out errorMessage) == true)
                                TaxObj.TaxKind = TaxKind;
                            TaxTypeFactory TaxTypeFactory = new TaxTypeFactory();
                            TaxType TaxType = new TaxType();
                            if (TaxTypeFactory.GetTaxType(Convert.ToInt32(Dr[4].ToString()), out TaxType, out errorMessage) == true)
                                TaxObj.TaxType = TaxType;
                            TaxObj.TaxValue = Convert.ToDouble(Dr[5].ToString());
                            TaxObj.StartDate = Convert.ToDateTime(Dr[6].ToString());
                            TaxObj.EndDate = Convert.ToDateTime(Dr[7].ToString());
                            TaxObj.CreateDate = Convert.ToDateTime(Dr[8].ToString());
                            if ((Dr[9].ToString() != null) && (Dr[9].ToString() != ""))
                                TaxObj.LastModDate = Convert.ToDateTime(Dr[9].ToString());

                            if (Ds.Tables[1].Rows.Count > 0)
                            {
                                for (int i = 0; i < Ds.Tables[1].Rows.Count; i++)
                                {
                                    TaxItemObj = new TaxItem();

                                    Dr = Ds.Tables[0].Rows[i];

                                    TaxItemObj.TaxCode = Dr[0].ToString();
                                    TaxItemObj.ItemId = Dr[1].ToString();
                                    TaxItemObj.BatchId = Dr[2].ToString();
                                    TaxItemObj.StartDate = Convert.ToDateTime(Dr[3].ToString());
                                    TaxItemObj.EndDate = Convert.ToDateTime(Dr[4].ToString());
                                    TaxItemObj.Remarks = Dr[5].ToString();
                                    TaxItemObj.CreateDate = Convert.ToDateTime(Dr[6].ToString());
                                    if ((Dr[7].ToString() != null) && (Dr[7].ToString() != ""))
                                        TaxItemObj.LastModDate = Convert.ToDateTime(Dr[7].ToString());

                                    TaxObj.TaxItemList.Add(TaxItemObj);
                                }
                            }
                            retval = true;
                        }
                    }
                }
                catch (Exception ex1)
                {
                    errorMessage = ex1.Message;
                }

                return retval;
            }

            public bool GetTaxList(string TaxCode, out string errorMessage, out Tax[] TaxObjArray)
            {
                DataSet Ds;
                DataRow Dr, Dr1;
                Tax TaxObj;
                TaxItem TaxItemObj;

                errorMessage = null;

                List<Tax> TaxObjList = null;
                TaxObjArray = null;

                bool retval = false;

                TaxObjList = new List<Tax>();

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

                    if (DBObj.ExecuteSPWithParamsRespDS(out Ds, out errorMessage, "NewAppDb..GetTaxList", SQLParamArray) == false)
                        return retval;

                    if (Ds.Tables.Count > 0)
                    {
                        for (int i = 0; i < Ds.Tables[0].Rows.Count; i++)
                        {
                            TaxObj = new Tax();

                            Dr = Ds.Tables[0].Rows[i];

                            TaxObj.TaxCode = Dr[0].ToString();
                            TaxObj.TaxName = Dr[1].ToString();
                            TaxObj.TaxDescription = Dr[2].ToString();
                            TaxKindFactory TaxKindFactory = new TaxKindFactory();
                            TaxKind TaxKind = new TaxKind();
                            if (TaxKindFactory.GetTaxKind(Convert.ToInt32(Dr[3].ToString()), out TaxKind, out errorMessage) == true)
                                TaxObj.TaxKind = TaxKind;
                            TaxTypeFactory TaxTypeFactory = new TaxTypeFactory();
                            TaxType TaxType = new TaxType();
                            if (TaxTypeFactory.GetTaxType(Convert.ToInt32(Dr[4].ToString()), out TaxType, out errorMessage) == true)
                                TaxObj.TaxType = TaxType;
                            TaxObj.TaxValue = Convert.ToDouble(Dr[5].ToString());
                            TaxObj.StartDate = Convert.ToDateTime(Dr[6].ToString());
                            TaxObj.EndDate = Convert.ToDateTime(Dr[7].ToString());
                            TaxObj.CreateDate = Convert.ToDateTime(Dr[8].ToString());
                            if ((Dr[9].ToString() != null) && (Dr[9].ToString() != ""))
                                TaxObj.LastModDate = Convert.ToDateTime(Dr[9].ToString());

                            if (Ds.Tables.Count > 1)
                            {
                                for (int j = 0; j < Ds.Tables[1].Rows.Count; j++)
                                {
                                    Dr1 = Ds.Tables[1].Rows[j];

                                    if (Dr[0].ToString() == Dr1[0].ToString())
                                    {
                                        TaxItemObj = new TaxItem();

                                        TaxItemObj.TaxCode = Dr1[0].ToString();
                                        TaxItemObj.ItemId = Dr1[1].ToString();
                                        TaxItemObj.BatchId = Dr1[2].ToString();
                                        TaxItemObj.StartDate = Convert.ToDateTime(Dr1[3].ToString());
                                        TaxItemObj.EndDate = Convert.ToDateTime(Dr1[4].ToString());
                                        TaxItemObj.Remarks = Dr1[5].ToString();
                                        TaxItemObj.CreateDate = Convert.ToDateTime(Dr1[6].ToString());
                                        if ((Dr1[7].ToString() != null) && (Dr1[7].ToString() != ""))
                                            TaxItemObj.LastModDate = Convert.ToDateTime(Dr1[7].ToString());

                                        StoreItemFactory MyStoreItemFactory = new StoreItemFactory();
                                        StoreItem StoreItemObj = new StoreItem();

                                        if (MyStoreItemFactory.GetStoreItemDetails(TaxItemObj.ItemId, TaxItemObj.BatchId, out errorMessage, out StoreItemObj) == true)
                                            TaxItemObj.StoreItemDetail = StoreItemObj;

                                        TaxObj.TaxItemList.Add(TaxItemObj);
                                    }
                                }
                            }
                            TaxObjList.Add(TaxObj);
                        }

                        TaxObjArray = TaxObjList.ToArray<Tax>();
                        retval = true;
                    }
                }
                catch (Exception ex1)
                {
                    errorMessage = ex1.Message;
                }


                return retval;
            }

            public bool GetGlobalTaxesList(out string errorMessage, out Tax[] GlobalTaxObjArray)
            {
                DataSet Ds;
                DataRow Dr;
                Tax TaxObj;


                errorMessage = null;

                bool retval = false;

                List<Tax> TaxObjList = new List<Tax>();
                GlobalTaxObjArray = null;

                try
                {
                    DBObj = SQLDataAccess.GetDBObj(out errorMessage);

                    if (errorMessage != null)
                        return retval;

                    Ds = new DataSet();

                    if (DBObj.ExecuteSPNoParamsWithRespDS(out Ds, out errorMessage, "NewAppDb..GetGlobalTaxesList") == false)
                        return retval;

                    if (Ds.Tables.Count > 0)
                    {
                        for (int i = 0; i < Ds.Tables[0].Rows.Count; i++)
                        {
                            TaxObj = new Tax();

                            Dr = Ds.Tables[0].Rows[i];

                            TaxObj.TaxCode = Dr[0].ToString();
                            TaxObj.TaxName = Dr[1].ToString();
                            TaxObj.TaxDescription = Dr[2].ToString();
                            TaxKindFactory TaxKindFactory = new TaxKindFactory();
                            TaxKind TaxKind = new TaxKind();
                            if (TaxKindFactory.GetTaxKind(Convert.ToInt32(Dr[3].ToString()), out TaxKind, out errorMessage) == true)
                                TaxObj.TaxKind = TaxKind;
                            TaxTypeFactory TaxTypeFactory = new TaxTypeFactory();
                            TaxType TaxType = new TaxType();
                            if (TaxTypeFactory.GetTaxType(Convert.ToInt32(Dr[4].ToString()), out TaxType, out errorMessage) == true)
                                TaxObj.TaxType = TaxType;
                            TaxObj.TaxValue = Convert.ToDouble(Dr[5].ToString());
                            TaxObj.StartDate = Convert.ToDateTime(Dr[6].ToString());
                            TaxObj.EndDate = Convert.ToDateTime(Dr[7].ToString());
                            TaxObj.CreateDate = Convert.ToDateTime(Dr[8].ToString());
                            if ((Dr[9].ToString() != null) && (Dr[9].ToString() != ""))
                                TaxObj.LastModDate = Convert.ToDateTime(Dr[9].ToString());

                            if (TaxObj.TaxKind.TaxKindId == 0)
                                TaxObjList.Add(TaxObj);
                        }

                        GlobalTaxObjArray = TaxObjList.ToArray<Tax>();
                        retval = true;
                    }
                }
                catch (Exception ex1)
                {
                    errorMessage = ex1.Message;
                }


                return retval;
            }

            public bool AddNewTax(out string errorMessage, Tax TaxObj)
            {
                bool retval = false;
                errorMessage = null;

                try
                {
                    DBObj = SQLDataAccess.GetDBObj(out errorMessage);

                    if (errorMessage != null)
                        return retval;

                    SqlParameter[] SQLParamArray = new SqlParameter[1];

                    SqlParameter parameter = new SqlParameter("@Tax_Code", SqlDbType.VarChar, 30);
                    parameter.Value = "";
                    parameter.Direction = ParameterDirection.Output;
                    SQLParamArray[0] = parameter;

                    if (DBObj.ExecuteSPWithParamsNoResp(out errorMessage, "NewAppDb..GetNextTaxCode", SQLParamArray) == false)
                    {
                        errorMessage = "Error While Generating Tax Code" + errorMessage;
                        return retval;
                    }

                    TaxObj.TaxCode = SQLParamArray[0].Value.ToString();

                    SQLParamArray = new SqlParameter[9];

                    parameter = new SqlParameter("@Tax_Code", SqlDbType.VarChar);
                    parameter.Value = (object)TaxObj.TaxCode ?? DBNull.Value;
                    SQLParamArray[0] = parameter;

                    parameter = new SqlParameter("@Tax_Name", SqlDbType.VarChar);
                    parameter.Value = (object)TaxObj.TaxName ?? DBNull.Value;
                    SQLParamArray[1] = parameter;

                    parameter = new SqlParameter("@Tax_Description", SqlDbType.VarChar);
                    parameter.Value = (object)TaxObj.TaxDescription ?? DBNull.Value;
                    SQLParamArray[2] = parameter;

                    parameter = new SqlParameter("@Tax_Kind_Id", SqlDbType.Int);
                    parameter.Value = (object)TaxObj.TaxKind.TaxKindId ?? DBNull.Value;
                    SQLParamArray[3] = parameter;

                    parameter = new SqlParameter("@Tax_Type_Id", SqlDbType.Int);
                    parameter.Value = (object)TaxObj.TaxType.TaxTypeId ?? DBNull.Value;
                    SQLParamArray[4] = parameter;

                    parameter = new SqlParameter("@Tax_Value", SqlDbType.Decimal);
                    parameter.Value = (object)TaxObj.TaxValue ?? DBNull.Value;
                    SQLParamArray[5] = parameter;

                    parameter = new SqlParameter("@Tax_Start_Date", SqlDbType.DateTime);
                    parameter.Value = (object)TaxObj.StartDate ?? DBNull.Value;
                    SQLParamArray[6] = parameter;

                    parameter = new SqlParameter("@Tax_End_Date", SqlDbType.DateTime);
                    parameter.Value = (object)TaxObj.EndDate ?? DBNull.Value;
                    SQLParamArray[7] = parameter;

                    parameter = new SqlParameter("@Tax_Create_Date", SqlDbType.DateTime);
                    parameter.Value = (object)TaxObj.CreateDate ?? DBNull.Value;
                    SQLParamArray[8] = parameter;

                    DataSet ds = new DataSet();

                    errorMessage = null;

                    if (DBObj.ExecuteSPWithParamsNoResp(out errorMessage, "NewAppDb..AddNewTax", SQLParamArray) == false)
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

            public void ModifyTaxDetails(){throw new System.NotSupportedException ();}

            public bool DeleteTaxDetails(out string errorMessage, Tax TaxObj)
            {
                bool retval = false;
                errorMessage = null;

                try
                {
                    DBObj = SQLDataAccess.GetDBObj(out errorMessage);

                    if (errorMessage != null)
                        return retval;

                    SqlParameter[] SQLParamArray = new SqlParameter[1];

                    SqlParameter parameter = new SqlParameter("@Tax_Code", SqlDbType.VarChar);
                    parameter.Value = (object)TaxObj.TaxCode ?? DBNull.Value;
                    SQLParamArray[0] = parameter;

                    if (DBObj.ExecuteSPWithParamsNoResp(out errorMessage, "NewAppDb..DeleteTax", SQLParamArray) == false)
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

        #endregion
    }
}
