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
    public class DiscountFactory : Discount, IDiscountFactory
    {
        //Declarations
        SQLDataAccess DBObj;

        //Constructor
        public DiscountFactory()
        {
        }

        //Member Methods  
        #region MemberMethods

            public bool GetDiscountDetails(string DiscCode, out string errorMessage, out Discount DiscObj)
            {
                DataSet Ds;
                DataRow Dr;
                DiscountItem DiscItemObj;
                
                DiscObj = null;
                Ds = null;
                errorMessage = null;

                bool retval = false;

                SqlParameter[] SQLParamArray = new SqlParameter[1];

                SqlParameter parameter = new SqlParameter("@DiscCode", SqlDbType.VarChar);
                parameter.Value = DiscCode;
                SQLParamArray[0] = parameter;

                try
                {
                    DBObj = SQLDataAccess.GetDBObj(out errorMessage);

                    if (errorMessage != null)
                        return retval;

                    Ds = new DataSet();


                    if (DBObj.ExecuteSPWithParamsRespDS(out Ds, out errorMessage, "NewAppDb..GetDiscountDetails", SQLParamArray) == false)
                    {
                        if (errorMessage != null)
                            return retval;
                    }
                    else
                    {
                        if (Ds.Tables[0].Rows.Count > 0)
                        {
                            DiscObj = new Discount();

                            Dr = Ds.Tables[0].Rows[0];

                            DiscObj.DiscountCode = Dr[0].ToString();
                            DiscObj.DiscountName = Dr[1].ToString();
                            DiscObj.DiscountDescription = Dr[2].ToString();
                            DiscountKindFactory DiscKindFactory = new DiscountKindFactory();
                            DiscountKind DiscKind = new DiscountKind();
                            if (DiscKindFactory.GetDiscountKind(Convert.ToInt32(Dr[3].ToString()), out DiscKind, out errorMessage) == true)
                                DiscObj.DiscountKind = DiscKind;
                            else
                                return retval;
                            DiscountTypeFactory DiscTypeFactory = new DiscountTypeFactory();
                            DiscountType DiscType = new DiscountType();
                            if (DiscTypeFactory.GetDiscountType(Convert.ToInt32(Dr[4].ToString()), out DiscType, out errorMessage) == true)
                                DiscObj.DiscountType = DiscType;
                            else
                                return retval;
                            DiscObj.DiscountValue = Convert.ToDouble(Dr[5].ToString());
                            DiscObj.DiscountRemarks = Dr[6].ToString();
                            if ((Dr[7].ToString() != null) && (Dr[7].ToString() != ""))
                                DiscObj.DiscStartDate = Convert.ToDateTime(Dr[7].ToString());
                            if ((Dr[8].ToString() != null) && (Dr[8].ToString() != ""))
                                DiscObj.DiscEndDate = Convert.ToDateTime(Dr[8].ToString());
                            if ((Dr[9].ToString() != null) && (Dr[9].ToString() != ""))
                                DiscObj.DiscCreateDate = Convert.ToDateTime(Dr[9].ToString());
                            if ((Dr[10].ToString() != null) && (Dr[10].ToString() != ""))
                                DiscObj.DiscLastModDate = Convert.ToDateTime(Dr[10].ToString());

                            if (Ds.Tables[1].Rows.Count > 0)
                            {
                                for (int i = 0; i < Ds.Tables[1].Rows.Count; i++)
                                {
                                    DiscItemObj = new DiscountItem();

                                    Dr = Ds.Tables[0].Rows[i];

                                    DiscItemObj.DiscountCode = Dr[0].ToString();
                                    DiscItemObj.DiscItemId = Dr[1].ToString();
                                    DiscItemObj.DiscItemBatchId = Dr[2].ToString();
                                    if ((Dr[3].ToString() != null) && (Dr[3].ToString() != ""))
                                        DiscItemObj.DiscItemStartDate = Convert.ToDateTime(Dr[3].ToString());
                                    if ((Dr[4].ToString() != null) && (Dr[4].ToString() != ""))
                                        DiscItemObj.DiscItemEndDate = Convert.ToDateTime(Dr[4].ToString());
                                    DiscItemObj.DiscRemarks = Dr[5].ToString();
                                    if ((Dr[6].ToString() != null) && (Dr[6].ToString() != ""))
                                        DiscItemObj.DiscCreateDate = Convert.ToDateTime(Dr[6].ToString());
                                    if ((Dr[7].ToString() != null) && (Dr[7].ToString() != ""))
                                        DiscItemObj.DiscLastModDate = Convert.ToDateTime(Dr[7].ToString());

                                    DiscObj.DiscountItemsList.Add(DiscItemObj);
                                }
                            }

                            retval = true;
                        }
                    }
                    if (Ds != null)
                        Ds.Dispose();
                }
                catch (Exception ex1)
                {
                    Ds.Dispose();
                    errorMessage = ex1.Message;
                }
                finally
                {
                    if (Ds != null)
                        Ds.Dispose();
                }

                return retval;
            }

            public bool GetDiscountList(string DiscCode, out string errorMessage, out Discount[] DiscObjArray)
            {
                DataSet Ds;
                DataRow Dr, Dr1;
                Discount DiscObj;
                DiscountItem DiscItemObj;

                errorMessage = null;
                Ds = null;

                bool retval = false;

                List<Discount> DiscObjList = new List<Discount>();
                DiscObjArray = null;

                SqlParameter[] SQLParamArray = new SqlParameter[1];

                SqlParameter parameter = new SqlParameter("@DiscCode", SqlDbType.VarChar);
                parameter.Value = DiscCode;
                SQLParamArray[0] = parameter;

                try
                {
                    DBObj = SQLDataAccess.GetDBObj(out errorMessage);

                    if (errorMessage != null)
                        return retval;
                
                    Ds = new DataSet();


                    if (DBObj.ExecuteSPWithParamsRespDS(out Ds, out errorMessage, "NewAppDb..GetDiscountList", SQLParamArray) == false)
                        return retval;

                    if (Ds.Tables.Count <= 0)
                        return retval;

                    DiscountType DiscType;

                    for (int i = 0; i < Ds.Tables[0].Rows.Count; i++)
                    {
                        DiscObj = new Discount();

                        Dr = Ds.Tables[0].Rows[i];

                        DiscObj.DiscountCode = Dr[0].ToString();
                        DiscObj.DiscountName = Dr[1].ToString();
                        DiscObj.DiscountDescription = Dr[2].ToString();
                        DiscountKindFactory DiscKindFactory = new DiscountKindFactory();
                        DiscountKind DiscKind = new DiscountKind();
                        if (DiscKindFactory.GetDiscountKind(Convert.ToInt32(Dr[3].ToString()), out DiscKind, out errorMessage) == true)
                            DiscObj.DiscountKind = DiscKind;
                        else
                            return retval;
                        DiscountTypeFactory DiscTypeFactory = new DiscountTypeFactory();
                        DiscType = new DiscountType();
                        if (DiscTypeFactory.GetDiscountType(Convert.ToInt32(Dr[4].ToString()), out DiscType, out errorMessage) == true)
                            DiscObj.DiscountType = DiscType;
                        else
                            return retval;
                        DiscObj.DiscountValue = Convert.ToDouble(Dr[5].ToString());
                        DiscObj.DiscountRemarks = Dr[6].ToString();
                        if ((Dr[7].ToString() != null) && (Dr[7].ToString() != ""))
                            DiscObj.DiscStartDate = Convert.ToDateTime(Dr[7].ToString());
                        if ((Dr[8].ToString() != null) && (Dr[8].ToString() != ""))
                            DiscObj.DiscEndDate = Convert.ToDateTime(Dr[8].ToString());
                        if ((Dr[9].ToString() != null) && (Dr[9].ToString() != ""))
                            DiscObj.DiscCreateDate = Convert.ToDateTime(Dr[9].ToString());
                        if ((Dr[10].ToString() != null) && (Dr[10].ToString() != ""))
                            DiscObj.DiscLastModDate = Convert.ToDateTime(Dr[10].ToString());

                        if (Ds.Tables.Count > 1)
                        {
                            for (int j = 0; j < Ds.Tables[1].Rows.Count; j++)
                            {
                                Dr1 = Ds.Tables[1].Rows[j];

                                if (Dr[0].ToString() == Dr1[0].ToString())
                                {
                                    DiscItemObj = new DiscountItem();

                                    DiscItemObj.DiscountCode = Dr1[0].ToString();
                                    DiscItemObj.DiscItemId = Dr1[1].ToString();
                                    DiscItemObj.DiscItemBatchId = Dr1[2].ToString();
                                    if ((Dr1[3].ToString() != null) && (Dr1[3].ToString() != ""))
                                        DiscItemObj.DiscItemStartDate = Convert.ToDateTime(Dr1[3].ToString());
                                    if ((Dr1[4].ToString() != null) && (Dr1[4].ToString() != ""))
                                        DiscItemObj.DiscItemEndDate = Convert.ToDateTime(Dr1[4].ToString());
                                    DiscItemObj.DiscRemarks = Dr1[5].ToString();
                                    if ((Dr1[6].ToString() != null) && (Dr1[6].ToString() != ""))
                                        DiscItemObj.DiscCreateDate = Convert.ToDateTime(Dr1[6].ToString());
                                    if ((Dr1[7].ToString() != null) && (Dr1[7].ToString() != ""))
                                        DiscItemObj.DiscLastModDate = Convert.ToDateTime(Dr1[7].ToString());

                                    StoreItemFactory MyStoreItemFactory = new StoreItemFactory();
                                    StoreItem StoreItemObj = new StoreItem();

                                    if (MyStoreItemFactory.GetStoreItemDetails(DiscItemObj.DiscItemId, DiscItemObj.DiscItemBatchId, out errorMessage, out StoreItemObj) == true)
                                        DiscItemObj.StoreItemDetail = StoreItemObj;

                                    DiscObj.DiscountItemsList.Add(DiscItemObj);
                                }
                            }
                        }
                        DiscObjList.Add(DiscObj);
                    }

                    DiscObjArray = DiscObjList.ToArray<Discount>();

                    Ds = null;

                    retval = true;
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

            public bool GetGlobalDiscountList(out string errorMessage, out Discount[] GlobalDiscObjArray)
            {
                DataSet Ds;
                DataRow Dr;
                Discount DiscObj;


                errorMessage = null;

                bool retval = false;

                List<Discount> DiscObjList = new List<Discount>();
                GlobalDiscObjArray = null;

                try
                {
                    DBObj = SQLDataAccess.GetDBObj(out errorMessage);

                    if (errorMessage != null)
                        return retval;

                    Ds = new DataSet();

                    if (DBObj.ExecuteSPNoParamsWithRespDS(out Ds, out errorMessage, "NewAppDb..GetGlobalDiscountList") == false)
                        return retval;

                    if (Ds.Tables.Count > 0)
                    {
                        for (int i = 0; i < Ds.Tables[0].Rows.Count; i++)
                        {
                            DiscObj = new Discount();

                            Dr = Ds.Tables[0].Rows[i];

                            DiscObj.DiscountCode = Dr[0].ToString();
                            DiscObj.DiscountName = Dr[1].ToString();
                            DiscObj.DiscountDescription = Dr[2].ToString();
                            DiscountKindFactory DiscountKindFactory = new DiscountKindFactory();
                            DiscountKind DiscountKind = new DiscountKind();
                            if (DiscountKindFactory.GetDiscountKind(Convert.ToInt32(Dr[3].ToString()), out DiscountKind, out errorMessage) == true)
                                DiscObj.DiscountKind = DiscountKind;
                            DiscountTypeFactory DiscountTypeFactory = new DiscountTypeFactory();
                            DiscountType DiscountType = new DiscountType();
                            if (DiscountTypeFactory.GetDiscountType(Convert.ToInt32(Dr[4].ToString()), out DiscountType, out errorMessage) == true)
                                DiscObj.DiscountType = DiscountType;
                            DiscObj.DiscountValue = Convert.ToDouble(Dr[5].ToString());
                            DiscObj.DiscStartDate = Convert.ToDateTime(Dr[6].ToString());
                            DiscObj.DiscEndDate = Convert.ToDateTime(Dr[7].ToString());
                            DiscObj.DiscCreateDate = Convert.ToDateTime(Dr[8].ToString());
                            if ((Dr[9].ToString() != null) && (Dr[9].ToString() != ""))
                                DiscObj.DiscLastModDate = Convert.ToDateTime(Dr[9].ToString());

                            if (DiscObj.DiscountKind.DiscountKindId == 0)
                                DiscObjList.Add(DiscObj);
                        }

                        GlobalDiscObjArray = DiscObjList.ToArray<Discount>();
                        retval = true;
                    }
                }
                catch (Exception ex1)
                {
                    errorMessage = ex1.Message;
                }


                return retval;
            }

            public bool AddNewDiscount(Discount DiscObj, out string errorMessage)
            {
                bool retval = false;
                errorMessage = null;

                try
                {
                    DBObj = SQLDataAccess.GetDBObj(out errorMessage);

                    if (errorMessage != null)
                        return retval;
               
                    SqlParameter[] SQLParamArray = new SqlParameter[1];

                    SqlParameter parameter = new SqlParameter("@DiscCode", SqlDbType.VarChar, 30);
                    parameter.Value = "";
                    parameter.Direction = ParameterDirection.Output;
                    SQLParamArray[0] = parameter;

                    if (DBObj.ExecuteSPWithParamsNoResp(out errorMessage, "NewAppDb..GetNextDiscCode", SQLParamArray) == false)
                    {
                        errorMessage = "Error While Generating Discount Code" + errorMessage;
                        return retval;
                    }

                    DiscObj.DiscountCode = SQLParamArray[0].Value.ToString();

                    SQLParamArray = new SqlParameter[11];

                    parameter = new SqlParameter("@Disc_Code", SqlDbType.VarChar);
                    parameter.Value = (object)DiscObj.DiscountCode ?? DBNull.Value;
                    SQLParamArray[0] = parameter;

                    parameter = new SqlParameter("@Disc_Name", SqlDbType.VarChar);
                    parameter.Value = (object)DiscObj.DiscountName ?? DBNull.Value;
                    SQLParamArray[1] = parameter;

                    parameter = new SqlParameter("@Disc_Description", SqlDbType.VarChar);
                    parameter.Value = (object)DiscObj.DiscountDescription ?? DBNull.Value;
                    SQLParamArray[2] = parameter;

                    parameter = new SqlParameter("@Disc_Kind_Id", SqlDbType.Int);
                    parameter.Value = (object)DiscObj.DiscountKind.DiscountKindId ?? DBNull.Value;
                    SQLParamArray[3] = parameter;

                    parameter = new SqlParameter("@Disc_Type_Id", SqlDbType.Int);
                    parameter.Value = (object)DiscObj.DiscountType.DiscountTypeId ?? DBNull.Value;
                    SQLParamArray[4] = parameter;

                    parameter = new SqlParameter("@Disc_Value", SqlDbType.Decimal);
                    parameter.Value = (object)DiscObj.DiscountValue ?? DBNull.Value;
                    SQLParamArray[5] = parameter;

                    parameter = new SqlParameter("@Disc_Start_Date", SqlDbType.DateTime);
                    parameter.Value = (object)DiscObj.DiscStartDate ?? DBNull.Value;
                    SQLParamArray[6] = parameter;

                    parameter = new SqlParameter("@Disc_End_Date", SqlDbType.DateTime);
                    parameter.Value = (object)DiscObj.DiscEndDate ?? DBNull.Value;
                    SQLParamArray[7] = parameter;

                    parameter = new SqlParameter("@Disc_Remarks", SqlDbType.VarChar);
                    parameter.Value = (object)DiscObj.DiscountRemarks ?? DBNull.Value;
                    SQLParamArray[8] = parameter;

                    parameter = new SqlParameter("@Disc_Create_Date", SqlDbType.DateTime);
                    parameter.Value = (object)DiscObj.DiscCreateDate ?? DBNull.Value;
                    SQLParamArray[9] = parameter;

                    parameter = new SqlParameter("@Disc_Last_Mod_Date", SqlDbType.DateTime);
                    parameter.Value = (object)DiscObj.DiscLastModDate ?? DBNull.Value;
                    SQLParamArray[10] = parameter;

                    errorMessage = null;

                    if (DBObj.ExecuteSPWithParamsNoResp(out errorMessage, "NewAppDb..AddNewDiscount", SQLParamArray) == false)
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

            public void ModifyDiscDetails()
            {
                throw new System.NotSupportedException ();
            }

            public bool DeleteDiscDetails(Discount DiscObj, out string errorMessage)
            {
                bool retval = false;
                errorMessage = null;

                try
                {
                    DBObj = SQLDataAccess.GetDBObj(out errorMessage);

                    if (errorMessage != null)
                        return retval;
               
                    SqlParameter[] SQLParamArray = new SqlParameter[1];

                    SqlParameter parameter = new SqlParameter("@Disc_Code", SqlDbType.VarChar);
                    parameter.Value = (object)DiscObj.DiscountCode ?? DBNull.Value;
                    SQLParamArray[0] = parameter;

                    if (DBObj.ExecuteSPWithParamsNoResp(out errorMessage, "NewAppDb..DeleteDiscount", SQLParamArray) == false)
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
