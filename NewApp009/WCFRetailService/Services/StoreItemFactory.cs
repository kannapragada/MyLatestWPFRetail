using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Xml;
using NewApp.BusinessTier.Models;
using WCFRetailService;




namespace WCFRetailService
{
    public class StoreItemFactory : StoreItem, IStoreItemFactory
    {
        //Declarations
        # region Declarations
            SQLDataAccess DBObj;

        #endregion

        //Constructor
        public StoreItemFactory()
        {
        }


        //Member Methods 
        #region MemberMethods

        public bool AddStoreItem(out string errorMessage, StoreItem StoreItemObj)
        {
            bool retval = false;
            errorMessage = null;

            try
            {
                DBObj = SQLDataAccess.GetDBObj(out errorMessage);

                if (errorMessage != null)
                    return retval;


                SqlParameter[] SQLParamArray = new SqlParameter[1];

                SqlParameter parameter = new SqlParameter("@ItemId", SqlDbType.VarChar,30);
                parameter.Value = "";
                parameter.Direction = ParameterDirection.Output;
                SQLParamArray[0] = parameter;

                if (DBObj.ExecuteSPWithParamsNoResp(out errorMessage, "NewAppDb..GetNextItemId", SQLParamArray) == false)
                {
                    errorMessage = "Error While Generating Item Id";
                    return retval;
                }


                StoreItemObj.ItemId = SQLParamArray[0].Value.ToString();

                SQLParamArray = new SqlParameter[18];

                parameter = new SqlParameter("@Item_Id", SqlDbType.VarChar);
                parameter.Value = (object)StoreItemObj.ItemId ?? DBNull.Value; 
                SQLParamArray[0] = parameter;  

                parameter = new SqlParameter("@Item_Name", SqlDbType.VarChar);
                parameter.Value = (object)StoreItemObj.ItemName ?? DBNull.Value; 
                SQLParamArray[1] = parameter;  

                parameter = new SqlParameter("@Item_Description", SqlDbType.VarChar);
                parameter.Value = (object)StoreItemObj.ItemDescription ?? DBNull.Value; 
                SQLParamArray[2] = parameter;

                parameter = new SqlParameter("@IB_Batch_Id", SqlDbType.VarChar);
                parameter.Value = (object)StoreItemObj.BatchId ?? DBNull.Value;
                SQLParamArray[3] = parameter;

                parameter = new SqlParameter("@IB_Manuf_Id", SqlDbType.VarChar);
                parameter.Value = (object)StoreItemObj.ManufacturerInfo.ManufacturerId ?? DBNull.Value;
                SQLParamArray[4] = parameter;

                parameter = new SqlParameter("@IB_Vendor_Id", SqlDbType.VarChar);
                parameter.Value = (object)StoreItemObj.VendorInfo.VendorId ?? DBNull.Value;
                SQLParamArray[5] = parameter;

                parameter = new SqlParameter("@IB_Qty_Procured", SqlDbType.BigInt);
                parameter.Value = (object)StoreItemObj.QtyProcured ?? DBNull.Value;
                SQLParamArray[6] = parameter;

                parameter = new SqlParameter("@IB_Price_Procured", SqlDbType.Decimal);
                parameter.Value = (object)StoreItemObj.ProcuredPricePerUnit ?? DBNull.Value;
                SQLParamArray[7] = parameter;

                parameter = new SqlParameter("@IB_MRP", SqlDbType.Decimal);
                parameter.Value = (object)StoreItemObj.MRP ?? DBNull.Value;
                SQLParamArray[8] = parameter;

                parameter = new SqlParameter("@IB_Date_Procured", SqlDbType.DateTime);
                parameter.Value = (object)StoreItemObj.DateProcured ?? DBNull.Value;
                SQLParamArray[9] = parameter;
                
                parameter = new SqlParameter("@IB_Date_Manuf", SqlDbType.DateTime);
                parameter.Value = (object)StoreItemObj.DateOfManuf ?? DBNull.Value;
                SQLParamArray[10] = parameter;

                parameter = new SqlParameter("@IB_Date_Exp", SqlDbType.DateTime);
                parameter.Value = (object)StoreItemObj.DateOfExp ?? DBNull.Value;
                SQLParamArray[11] = parameter;

                parameter = new SqlParameter("@IB_Weight_Procured", SqlDbType.Decimal);
                parameter.Value = (object)StoreItemObj.WeightProcured ?? DBNull.Value;
                SQLParamArray[12] = parameter;

                parameter = new SqlParameter("@IB_Weight_Available", SqlDbType.Decimal);
                parameter.Value = (object)StoreItemObj.WeightAvailable ?? DBNull.Value;
                SQLParamArray[13] = parameter;

                parameter = new SqlParameter("@IB_BarCode", SqlDbType.NVarChar);
                parameter.Value = (object)StoreItemObj.BarCode ?? DBNull.Value;
                SQLParamArray[14] = parameter;

                parameter = new SqlParameter("@IB_Price_Sell", SqlDbType.Decimal);
                parameter.Value = (object)StoreItemObj.SellingPricePerUnit ?? DBNull.Value;
                SQLParamArray[15] = parameter;

                parameter = new SqlParameter("@IB_Qty_Available", SqlDbType.BigInt);
                parameter.Value = (object)StoreItemObj.QtyAvailable ?? DBNull.Value;
                SQLParamArray[16] = parameter;

                parameter = new SqlParameter("@Item_Create_Date", SqlDbType.DateTime);
                parameter.Value = (object)StoreItemObj.ItemCreateDate ?? DBNull.Value; 
                SQLParamArray[17] = parameter;

                if (DBObj.ExecuteSPWithParamsNoResp(out errorMessage, "NewAppDb..AddStoreItem", SQLParamArray) == false)
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

        public bool ModifyStoreItem(out string errorMessage)
        {
            bool retval = false;
            errorMessage = null;
            
            SqlParameter[] SQLParamArray = new SqlParameter[2];

            SqlParameter parameter = new SqlParameter("@Item_Id", SqlDbType.VarChar);
            parameter.Value = (object)this.ItemId ?? DBNull.Value;
            SQLParamArray[0] = parameter;

            parameter = new SqlParameter("@IB_Old_Batch_Id", SqlDbType.VarChar);
            parameter.Value = (object)this.OldBatchId ?? DBNull.Value;
            SQLParamArray[0] = parameter;

            try
            {
                DBObj = SQLDataAccess.GetDBObj(out errorMessage);

                if (errorMessage != null)
                    return retval;


                if (DBObj.ExecuteSPWithParamsNoResp(out errorMessage, "NewAppDb..ModifyStoreItem", SQLParamArray) == false)
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

        public bool DeleteStoreItem(out string errorMessage, StoreItem StoreItemObj)
        {
        bool retval = false;
        errorMessage = null;

        SqlParameter[] SQLParamArray = new SqlParameter[1];

        SqlParameter parameter = new SqlParameter("@Item_Id", SqlDbType.VarChar);
        parameter.Value = (object)StoreItemObj.ItemId ?? DBNull.Value;
        SQLParamArray[0] = parameter;

        try
        {
            DBObj = SQLDataAccess.GetDBObj(out errorMessage);

            if (errorMessage != null)
                return retval;

            if (DBObj.ExecuteSPWithParamsNoResp(out errorMessage, "NewAppDb..DeleteStoreItem", SQLParamArray) == false)
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

        public bool DeleteStoreItemBatch(out string errorMessage, StoreItem StoreItemObj)
        {
            bool retval = false;
            errorMessage = null;

            SqlParameter[] SQLParamArray = new SqlParameter[2];

            SqlParameter parameter = new SqlParameter("@Item_Id", SqlDbType.VarChar);
            parameter.Value = (object)StoreItemObj.ItemId ?? DBNull.Value;
            SQLParamArray[0] = parameter;

            parameter = new SqlParameter("@IB_Batch_Id", SqlDbType.VarChar);
            parameter.Value = (object)StoreItemObj.BatchId ?? DBNull.Value;
            SQLParamArray[0] = parameter;

            try
            {
                DBObj = SQLDataAccess.GetDBObj(out errorMessage);

                if (errorMessage != null)
                    return retval;


                if (DBObj.ExecuteSPWithParamsNoResp(out errorMessage, "NewAppDb..DeleteStoreItemBatch", SQLParamArray) == false)
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

        public bool GetStoreItems(string ItemId, string ItemName, string BatchId, DateTime StartDateOfManuf, DateTime EndDateOfManuf, DateTime StartDateOfExp, DateTime EndDateOfExp, out string errorMessage, out StoreItem[] StoreItemObjArray)
        {
            bool retval = false;

            errorMessage = null;

            List<StoreItem> StoreItemObjList = new List<StoreItem>();
            StoreItemObjArray = null;

            try
            {
                if (GetStoreItemsFromDB(ItemId, ItemName, BatchId, StartDateOfManuf, EndDateOfManuf, StartDateOfExp, EndDateOfExp, out errorMessage, out StoreItemObjArray) == true)
                {
                    StoreItemObjList = StoreItemObjArray.ToList<StoreItem>();

                    StoreItem StoreItemObj;

                    for (int i = 0; i < StoreItemObjList.Count; i++)
                    {
                        StoreItemObj = new StoreItem();

                        if (ComputeAmountsPerUnit(StoreItemObj, out StoreItemObj, out errorMessage) == false)
                            if ((errorMessage != null) && (errorMessage != ""))
                                return retval;
                    }

                    StoreItemObjArray = StoreItemObjList.ToArray<StoreItem>();
                    retval = true;
                }
            }
            catch (Exception ex1)
            {
                errorMessage = ex1.Message;
            }


            return retval;
        }

        public bool GetStoreItems(string ItemIdOrName, out string errorMessage, out StoreItem[] StoreItemObjArray)
        {
            bool retval = false;

            errorMessage = null;

            List<StoreItem> StoreItemObjList = new List<StoreItem>();
            StoreItemObjArray = null;

            StoreItem StoreItemObj;

            try
            {
                if (GetStoreItemsFromDB(ItemIdOrName, out errorMessage, out StoreItemObjArray) == true)
                {
                    StoreItemObjList = StoreItemObjArray.ToList<StoreItem>();

                    for (int i = 0; i < StoreItemObjList.Count; i++)
                    {
                        StoreItemObj = new StoreItem();
                        StoreItemObj = (StoreItem)StoreItemObjList[i];

                        if (ComputeAmountsPerUnit(StoreItemObj, out StoreItemObj, out errorMessage) == false)
                        {
                            if ((errorMessage != null) && (errorMessage != ""))
                                return retval;
                        }
                        else
                            StoreItemObjList[i] = StoreItemObj;
                    }

                    StoreItemObjArray = StoreItemObjList.ToArray<StoreItem>();
                    retval = true;
                }
            }
            catch (Exception ex1)
            {
                errorMessage = ex1.Message;
            }


            return retval;
        }

        public bool GetStoreItemDetails(string ItemId, string BatchId, out string errorMessage, out StoreItem StoreItemObj)
        {
            bool retval = false;

            errorMessage = null;

            StoreItemObj = null;

            try
            {
                if (GetStoreItemDetailsFromDB(ItemId, BatchId, out errorMessage, out StoreItemObj) == true)
                    retval = true;
            }
            catch (Exception ex1)
            {
                errorMessage = ex1.Message;
            }


            return retval;
        }

        private bool GetStoreItemsFromDB(string ItemId, string ItemName, string BatchId, DateTime StartDateOfManuf, DateTime EndDateOfManuf, DateTime StartDateOfExp, DateTime EndDateOfExp, out string errorMessage, out StoreItem[] StoreItemObjArray)
        {
            DataSet Ds;
            DataRow Dr;
            StoreItem StoreItemObj;
            bool retval = false;

            errorMessage = null;

            List<StoreItem> StoreItemObjList = new List<StoreItem>();
            StoreItemObjArray = null;

            try
            {

                StoreItemObjList = new List<StoreItem>();

                SqlParameter[] SQLParamArray = new SqlParameter[7];

                SqlParameter parameter = new SqlParameter("@Item_Id", SqlDbType.VarChar);
                parameter.Value = (object)ItemId ?? DBNull.Value;
                SQLParamArray[0] = parameter;

                parameter = new SqlParameter("@Item_Name", SqlDbType.VarChar);
                parameter.Value = (object)ItemName ?? DBNull.Value;
                SQLParamArray[1] = parameter;

                parameter = new SqlParameter("@IB_Batch_Id", SqlDbType.VarChar);
                parameter.Value = (object)BatchId ?? DBNull.Value;
                SQLParamArray[2] = parameter;

                parameter = new SqlParameter("@IB_Start_Date_Manuf", SqlDbType.DateTime);
                if ((StartDateOfManuf.Year > 1900) && (StartDateOfManuf.Year < 2500))
                    parameter.Value = (object)StartDateOfManuf;
                else
                    parameter.Value = DBNull.Value;
                SQLParamArray[3] = parameter;

                parameter = new SqlParameter("@IB_End_Date_Manuf", SqlDbType.DateTime);
                if ((EndDateOfManuf.Year > 1900) && (EndDateOfManuf.Year < 2500))
                    parameter.Value = (object)EndDateOfManuf;
                else
                    parameter.Value = DBNull.Value;
                SQLParamArray[4] = parameter;

                parameter = new SqlParameter("@IB_Start_Date_Exp", SqlDbType.DateTime);
                if ((StartDateOfExp.Year > 1900) && (StartDateOfExp.Year < 2500))
                    parameter.Value = (object)EndDateOfManuf;
                else
                    parameter.Value = DBNull.Value;
                SQLParamArray[5] = parameter;

                parameter = new SqlParameter("@IB_End_Date_Exp", SqlDbType.DateTime);
                if ((EndDateOfExp.Year > 1900) && (EndDateOfExp.Year < 2500))
                    parameter.Value = (object)EndDateOfManuf;
                else
                    parameter.Value = DBNull.Value;

                SQLParamArray[6] = parameter;

                DBObj = SQLDataAccess.GetDBObj(out errorMessage);

                if (errorMessage != null)
                    return retval;

                Ds = null;

                if (DBObj.ExecuteSPWithParamsRespDS(out Ds, out errorMessage, "NewAppDb..SearchStoreItems", SQLParamArray) == false)
                {
                    if (errorMessage != null)
                        return retval;
                }
                else
                {
                    if (Ds.Tables.Count > 0)
                    {
                        if (Ds.Tables[0] != null)
                        {
                            StoreItemObjArray = new StoreItem[Ds.Tables[0].Rows.Count];

                            List<Discount> DiscList = new List<Discount>();
                            List<DiscountItem> DiscItemList = new List<DiscountItem>();

                            ManufacturerFactory ManufFactory = new ManufacturerFactory();
                            VendorFactory VendorFactory = new VendorFactory();

                            Manufacturer ManufObj;
                            Vendor VendorObj;

                            for (int i = 0; i < Ds.Tables[0].Rows.Count; i++)
                            {
                                StoreItemObj = new StoreItem();

                                Dr = Ds.Tables[0].Rows[i];

                                StoreItemObj.ItemId = Dr[0].ToString();
                                StoreItemObj.ItemName = Dr[1].ToString();
                                StoreItemObj.ItemDescription = Dr[2].ToString();
                                StoreItemObj.BatchId = Dr[3].ToString();
                                StoreItemObj.BarCode = Dr[4].ToString();
                                StoreItemObj.MRP = Convert.ToDouble(Dr[5].ToString());
                                StoreItemObj.QtyProcured = Convert.ToInt32(Dr[6].ToString());
                                StoreItemObj.ProcuredPricePerUnit = Convert.ToDouble(Dr[7].ToString());
                                StoreItemObj.DateProcured = Convert.ToDateTime(Dr[8].ToString());
                                StoreItemObj.DateOfManuf = Convert.ToDateTime(Dr[9].ToString());
                                StoreItemObj.DateOfExp = Convert.ToDateTime(Dr[10].ToString());
                                StoreItemObj.WeightProcured = Convert.ToDouble(Dr[11].ToString());
                                StoreItemObj.WeightAvailable = Convert.ToDouble(Dr[12].ToString());
                                StoreItemObj.SellingPricePerUnit = Convert.ToDouble(Dr[13].ToString());
                                StoreItemObj.QtyAvailable = Convert.ToInt32(Dr[14].ToString());
                                StoreItemObj.ItemCreateDate = Convert.ToDateTime(Dr[15].ToString());
                                if ((Dr[16].ToString() != null) && (Dr[16].ToString() != "")) StoreItemObj.ItemLastModDate = Convert.ToDateTime(Dr[16].ToString());
                                StoreItemObj.BatchCreateDate = Convert.ToDateTime(Dr[17].ToString());
                                if ((Dr[18].ToString() != null) && (Dr[18].ToString() != "")) StoreItemObj.BatchLastModDate = Convert.ToDateTime(Dr[18].ToString());
                                ManufObj = new Manufacturer();
                                if (ManufFactory.GetManufacturerBasicDetails(Convert.ToString(Dr[19].ToString()), out errorMessage, out ManufObj) == true)
                                    StoreItemObj.ManufacturerInfo = ManufObj;
                                else
                                    return retval;
                                VendorObj = new Vendor();
                                if (VendorFactory.GetVendorBasicDetails(Convert.ToString(Dr[20].ToString()), out errorMessage, out VendorObj) == true)
                                    StoreItemObj.VendorInfo = VendorObj;
                                else
                                    return retval;

                                StoreItemObjList.Add(StoreItemObj);
                            }

                            if (Ds.Tables[1] != null)
                            {
                                DiscountItem DiscItem;

                                foreach (DataRow Dr1 in Ds.Tables[1].Rows)
                                {
                                    DiscItem = new DiscountItem();

                                    DiscItem.DiscItemId = Dr1["ItemId"].ToString();
                                    DiscItem.DiscItemBatchId = Dr1["BatchId"].ToString();
                                    DiscItem.DiscountCode = Dr1["DiscCode"].ToString();
                                    DiscItem.DiscItemStartDate = Convert.ToDateTime(Dr1["DiscStartDate"].ToString());
                                    DiscItem.DiscItemEndDate = Convert.ToDateTime(Dr1["DiscEndDate"].ToString());
                                    DiscItem.DiscRemarks = Dr1["DiscRemarks"].ToString();
                                    DiscItem.DiscCreateDate = Convert.ToDateTime(Dr1["DiscCreateDate"].ToString());
                                    if ((Dr1["DiscLastModDate"].ToString() != null) && (Dr1["DiscLastModDate"].ToString() != "")) DiscItem.DiscLastModDate = Convert.ToDateTime(Dr1["DiscLastModDate"].ToString());

                                    DiscItemList.Add(DiscItem);
                                }
                            }

                            if (Ds.Tables[2] != null)
                            {
                                Discount Disc;
                                DiscountKind DiscKind;
                                DiscountKindFactory DiscKindFactory = new DiscountKindFactory();
                                DiscountType DiscType;
                                DiscountTypeFactory DiscTypeFactory = new DiscountTypeFactory();


                                foreach (DataRow Dr1 in Ds.Tables[2].Rows)
                                {
                                    Disc = new Discount();

                                    Disc.DiscountCode = Dr1["DiscCode"].ToString();
                                    Disc.DiscountName = Dr1["DiscName"].ToString();
                                    Disc.DiscountDescription = Dr1["DiscDescription"].ToString();
                                    DiscKind = new DiscountKind();
                                    if (DiscKindFactory.GetDiscountKind(Convert.ToInt32(Dr1["DiscKindId"].ToString()), out DiscKind, out errorMessage) == true)
                                        Disc.DiscountKind = DiscKind;
                                    DiscType = new DiscountType();
                                    if (DiscTypeFactory.GetDiscountType(Convert.ToInt32(Dr1["DiscTypeId"].ToString()), out DiscType, out errorMessage) == true)
                                        Disc.DiscountType = DiscType;
                                    Disc.DiscountValue = Convert.ToDouble(Dr1["DiscValue"].ToString());
                                    Disc.DiscStartDate = Convert.ToDateTime(Dr1["DiscStartDate"].ToString());
                                    Disc.DiscEndDate = Convert.ToDateTime(Dr1["DiscEndDate"].ToString());
                                    Disc.DiscCreateDate = Convert.ToDateTime(Dr1["DiscCreateDate"].ToString());
                                    if ((Dr1["DiscLastModDate"].ToString() != null) && (Dr1["DiscLastModDate"].ToString() != "")) Disc.DiscLastModDate = Convert.ToDateTime(Dr1["DiscLastModDate"].ToString());

                                    DiscList.Add(Disc);
                                }
                            }

                            if ((DiscItemList != null) && (DiscList != null))
                            {
                                foreach (DiscountItem DiscItemObj in DiscItemList)
                                {
                                    foreach (Discount DiscObj in DiscList)
                                    {
                                        if (DiscItemObj.DiscountCode == DiscObj.DiscountCode)
                                            DiscItemObj.DiscountDetail = DiscObj;
                                    }
                                }

                                foreach (StoreItem SIObj in StoreItemObjList)
                                {
                                    foreach (DiscountItem DiscItemObj in DiscItemList)
                                    {
                                        if ((SIObj.ItemId == DiscItemObj.DiscItemId) && (SIObj.BatchId == DiscItemObj.DiscItemBatchId))
                                            SIObj.DiscountList.Add(DiscItemObj);
                                    }
                                }
                            }


                            if (Ds.Tables[3] != null)
                            {
                                TaxItem TaxItem;

                                foreach (DataRow Dr1 in Ds.Tables[3].Rows)
                                {
                                    TaxItem = new TaxItem();

                                    TaxItem.ItemId = Dr1["ItemId"].ToString();
                                    TaxItem.TaxCode = Dr1["TaxCode"].ToString();
                                    TaxItem.BatchId = Dr1["BatchId"].ToString();
                                    TaxItem.StartDate = Convert.ToDateTime(Dr1["StartDate"].ToString());
                                    TaxItem.EndDate = Convert.ToDateTime(Dr1["EndDate"].ToString());
                                    TaxItem.Remarks = Dr1["Remarks"].ToString();
                                    TaxItem.CreateDate = Convert.ToDateTime(Dr1["CreateDate"].ToString());
                                    if ((Dr1["LastModDate"].ToString() != null) && (Dr1["LastModDate"].ToString() != "")) TaxItem.LastModDate = Convert.ToDateTime(Dr1["LastModDate"].ToString());


                                    foreach (StoreItem SIObj in StoreItemObjList)
                                    {
                                        if ((SIObj.ItemId == TaxItem.ItemId) && (SIObj.BatchId == TaxItem.BatchId))
                                            SIObj.TaxList.Add(TaxItem);
                                    }
                                }
                            }

                            if (Ds.Tables[4] != null)
                            {
                                Tax Tax;
                                TaxKind TaxKind;
                                TaxType TaxType;
                                TaxKindFactory TaxKindFactory = new TaxKindFactory();
                                TaxTypeFactory TaxTypeFactory = new TaxTypeFactory();

                                foreach (DataRow Dr1 in Ds.Tables[4].Rows)
                                {
                                    Tax = new Tax();
                                    Tax.TaxCode = Dr1["TaxCode"].ToString();
                                    Tax.TaxName = Dr1["TaxName"].ToString();
                                    Tax.TaxDescription = Dr1["TaxDescription"].ToString();
                                    TaxKind = new TaxKind();
                                    if (TaxKindFactory.GetTaxKind(Convert.ToInt32(Dr1["TaxKindId"].ToString()), out TaxKind, out errorMessage) == true)
                                        Tax.TaxKind = TaxKind;
                                    TaxType = new TaxType();
                                    if (TaxTypeFactory.GetTaxType(Convert.ToInt32(Dr1["TaxTypeId"].ToString()), out TaxType, out errorMessage) == true)
                                        Tax.TaxType = TaxType;
                                    Tax.TaxValue = Convert.ToDouble(Dr1["TaxValue"].ToString());
                                    Tax.StartDate = Convert.ToDateTime(Dr1["StartDate"].ToString());
                                    Tax.EndDate = Convert.ToDateTime(Dr1["EndDate"].ToString());
                                    Tax.CreateDate = Convert.ToDateTime(Dr1["CreateDate"].ToString());
                                    if ((Dr1["LastModDate"].ToString() != null) && (Dr1["LastModDate"].ToString() != "")) Tax.LastModDate = Convert.ToDateTime(Dr1["LastModDate"].ToString());

                                    foreach (StoreItem SIObj in StoreItemObjList)
                                    {
                                        int i = 0;
                                        while (i < this.TaxList.Count)
                                        {
                                            if (SIObj.TaxList[i].TaxCode == Tax.TaxCode)
                                                SIObj.TaxList[i].TaxDetail = Tax;

                                            i = i + 1;
                                        }
                                    }
                                }
                            }

                            StoreItemObjArray = StoreItemObjList.ToArray<StoreItem>();
                            retval = true;
                        }
                        else
                            errorMessage = "No Records Found!";
                    }
                }
            }
            catch (Exception ex1)
            {
                errorMessage = ex1.Message;
            }


            return retval;
        }

        private bool GetStoreItemsFromDB(string ItemIdOrName, out string errorMessage, out StoreItem[] StoreItemObjArray)
        {
            DataSet Ds;
            DataRow Dr;
            StoreItem StoreItemObj;
            bool retval = false;

            errorMessage = null;

            List<StoreItem> StoreItemObjList = new List<StoreItem>();
            StoreItemObjArray = null;

            try
            {
                StoreItemObjList = new List<StoreItem>();

                SqlParameter[] SQLParamArray = new SqlParameter[1];

                SqlParameter parameter = new SqlParameter("@ItemIdOrName", SqlDbType.VarChar);
                parameter.Value = (object)ItemIdOrName ?? DBNull.Value;
                SQLParamArray[0] = parameter;

                DBObj = SQLDataAccess.GetDBObj(out errorMessage);

                if (errorMessage != null)
                    return retval;

                Ds = null;

                if (DBObj.ExecuteSPWithParamsRespDS(out Ds, out errorMessage, "NewAppDb..GetStoreItems", SQLParamArray) == false)
                {
                    if (errorMessage != null)
                        return retval;
                }
                else
                {
                    if (Ds.Tables.Count > 0)
                    {
                        StoreItemObjArray = new StoreItem[Ds.Tables[0].Rows.Count];

                        if (Ds.Tables[0].Rows.Count > 0)
                        {
                            ManufacturerFactory ManufFactory = new ManufacturerFactory();
                            VendorFactory VendorFactory = new VendorFactory();

                            Manufacturer ManufObj;
                            Vendor VendorObj;


                            for (int i = 0; i < Ds.Tables[0].Rows.Count; i++)
                            {
                                StoreItemObj = new StoreItem();

                                Dr = Ds.Tables[0].Rows[i];

                                StoreItemObj.ItemId = Dr[0].ToString();
                                StoreItemObj.ItemName = Dr[1].ToString();
                                StoreItemObj.ItemDescription = Dr[2].ToString();
                                StoreItemObj.BatchId = Dr[3].ToString();
                                StoreItemObj.BarCode = Dr[4].ToString();
                                StoreItemObj.MRP = Convert.ToDouble(Dr[5].ToString());
                                StoreItemObj.QtyProcured = Convert.ToInt32(Dr[6].ToString());
                                StoreItemObj.ProcuredPricePerUnit = Convert.ToDouble(Dr[7].ToString());
                                StoreItemObj.DateProcured = Convert.ToDateTime(Dr[8].ToString());
                                StoreItemObj.DateOfManuf = Convert.ToDateTime(Dr[9].ToString());
                                StoreItemObj.DateOfExp = Convert.ToDateTime(Dr[10].ToString());
                                StoreItemObj.WeightProcured = Convert.ToDouble(Dr[11].ToString());
                                StoreItemObj.WeightAvailable = Convert.ToDouble(Dr[12].ToString());
                                StoreItemObj.SellingPricePerUnit = Convert.ToDouble(Dr[13].ToString());
                                StoreItemObj.QtyAvailable = Convert.ToInt32(Dr[14].ToString());
                                StoreItemObj.ItemCreateDate = Convert.ToDateTime(Dr[15].ToString());
                                if ((Dr[16].ToString() != null) && (Dr[16].ToString() != "")) StoreItemObj.ItemLastModDate = Convert.ToDateTime(Dr[16].ToString());
                                StoreItemObj.BatchCreateDate = Convert.ToDateTime(Dr[17].ToString());
                                if ((Dr[18].ToString() != null) && (Dr[18].ToString() != "")) StoreItemObj.BatchLastModDate = Convert.ToDateTime(Dr[18].ToString());
                                ManufObj = new Manufacturer();
                                if (ManufFactory.GetManufacturerBasicDetails(Convert.ToString(Dr[19].ToString()), out errorMessage, out ManufObj) == true)
                                    StoreItemObj.ManufacturerInfo = ManufObj;
                                else
                                    return retval;
                                VendorObj = new Vendor();
                                if (VendorFactory.GetVendorBasicDetails(Convert.ToString(Dr[20].ToString()), out errorMessage, out VendorObj) == true)
                                    StoreItemObj.VendorInfo = VendorObj;
                                else
                                    return retval;

                                StoreItemObjList.Add(StoreItemObj);
                            }


                            if (Ds.Tables[1] != null)
                            {
                                DiscountItem DiscItem;

                                foreach (DataRow Dr1 in Ds.Tables[1].Rows)
                                {
                                    DiscItem = new DiscountItem();

                                    DiscItem.DiscItemId = Dr1["ItemId"].ToString();
                                    DiscItem.DiscItemBatchId = Dr1["BatchId"].ToString();
                                    DiscItem.DiscountCode = Dr1["DiscCode"].ToString();
                                    DiscItem.DiscItemStartDate = Convert.ToDateTime(Dr1["DiscStartDate"].ToString());
                                    DiscItem.DiscItemEndDate = Convert.ToDateTime(Dr1["DiscEndDate"].ToString());
                                    DiscItem.DiscRemarks = Dr1["DiscRemarks"].ToString();
                                    DiscItem.DiscCreateDate = Convert.ToDateTime(Dr1["DiscCreateDate"].ToString());
                                    if ((Dr1["DiscLastModDate"].ToString() != null) && (Dr1["DiscLastModDate"].ToString() != "")) DiscItem.DiscLastModDate = Convert.ToDateTime(Dr1["DiscLastModDate"].ToString());

                                    foreach (StoreItem SIObj in StoreItemObjList)
                                    {
                                        if ((SIObj.ItemId == DiscItem.DiscItemId) && (SIObj.BatchId == DiscItem.DiscItemBatchId))
                                            SIObj.DiscountList.Add(DiscItem);
                                    }
                                }
                            }

                            if (Ds.Tables[2] != null)
                            {
                                Discount Disc;
                                DiscountKind DiscKind;
                                DiscountKindFactory DiscKindFactory = new DiscountKindFactory();
                                DiscountType DiscType;
                                DiscountTypeFactory DiscTypeFactory = new DiscountTypeFactory();


                                foreach (DataRow Dr1 in Ds.Tables[2].Rows)
                                {
                                    Disc = new Discount();

                                    Disc.DiscountCode = Dr1["DiscCode"].ToString();
                                    Disc.DiscountName = Dr1["DiscName"].ToString();
                                    Disc.DiscountDescription = Dr1["DiscDescription"].ToString();
                                    DiscKind = new DiscountKind();
                                    if (DiscKindFactory.GetDiscountKind(Convert.ToInt32(Dr1["DiscKindId"].ToString()), out DiscKind, out errorMessage) == true)
                                        Disc.DiscountKind = DiscKind;
                                    DiscType = new DiscountType();
                                    if (DiscTypeFactory.GetDiscountType(Convert.ToInt32(Dr1["DiscTypeId"].ToString()), out DiscType, out errorMessage) == true)
                                        Disc.DiscountType = DiscType;
                                    Disc.DiscountValue = Convert.ToDouble(Dr1["DiscValue"].ToString());
                                    Disc.DiscStartDate = Convert.ToDateTime(Dr1["DiscStartDate"].ToString());
                                    Disc.DiscEndDate = Convert.ToDateTime(Dr1["DiscEndDate"].ToString());
                                    Disc.DiscCreateDate = Convert.ToDateTime(Dr1["DiscCreateDate"].ToString());
                                    if ((Dr1["DiscLastModDate"].ToString() != null) && (Dr1["DiscLastModDate"].ToString() != "")) Disc.DiscLastModDate = Convert.ToDateTime(Dr1["DiscLastModDate"].ToString());

                                    int i;

                                    foreach (StoreItem SIObj in StoreItemObjList)
                                    {
                                        i = 0;

                                        while (i < SIObj.DiscountList.Count)
                                        {
                                            if (SIObj.DiscountList[i].DiscountCode == Disc.DiscountCode)
                                            {
                                                SIObj.DiscountList[i].DiscountDetail = Disc;
                                            }
                                            i = i + 1;
                                        }
                                    }
                                }
                            }

                            if (Ds.Tables[3] != null)
                            {
                                TaxItem TaxItem;

                                foreach (DataRow Dr1 in Ds.Tables[3].Rows)
                                {
                                    TaxItem = new TaxItem();

                                    TaxItem.ItemId = Dr1["ItemId"].ToString();
                                    TaxItem.TaxCode = Dr1["TaxCode"].ToString();
                                    TaxItem.BatchId = Dr1["BatchId"].ToString();
                                    TaxItem.StartDate = Convert.ToDateTime(Dr1["StartDate"].ToString());
                                    TaxItem.EndDate = Convert.ToDateTime(Dr1["EndDate"].ToString());
                                    TaxItem.Remarks = Dr1["Remarks"].ToString();
                                    TaxItem.CreateDate = Convert.ToDateTime(Dr1["CreateDate"].ToString());
                                    if ((Dr1["LastModDate"].ToString() != null) && (Dr1["LastModDate"].ToString() != "")) TaxItem.LastModDate = Convert.ToDateTime(Dr1["LastModDate"].ToString());


                                    foreach (StoreItem SIObj in StoreItemObjList)
                                    {
                                        if ((SIObj.ItemId == TaxItem.ItemId) && (SIObj.BatchId == TaxItem.BatchId))
                                            SIObj.TaxList.Add(TaxItem);
                                    }
                                }
                            }

                            if (Ds.Tables[4] != null)
                            {
                                Tax Tax;
                                TaxKind TaxKind;
                                TaxType TaxType;
                                TaxKindFactory TaxKindFactory = new TaxKindFactory();
                                TaxTypeFactory TaxTypeFactory = new TaxTypeFactory();


                                foreach (DataRow Dr1 in Ds.Tables[4].Rows)
                                {
                                    Tax = new Tax();

                                    Tax.TaxCode = Dr1["TaxCode"].ToString();
                                    Tax.TaxName = Dr1["TaxName"].ToString();
                                    Tax.TaxDescription = Dr1["TaxDescription"].ToString();
                                    TaxKind = new TaxKind();
                                    if (TaxKindFactory.GetTaxKind(Convert.ToInt32(Dr1["TaxKindId"].ToString()), out TaxKind, out errorMessage) == true)
                                        Tax.TaxKind = TaxKind;
                                    TaxType = new TaxType();
                                    if (TaxTypeFactory.GetTaxType(Convert.ToInt32(Dr1["TaxTypeId"].ToString()), out TaxType, out errorMessage) == true)
                                        Tax.TaxType = TaxType; Tax.TaxValue = Convert.ToDouble(Dr1["TaxValue"].ToString());
                                    Tax.StartDate = Convert.ToDateTime(Dr1["StartDate"].ToString());
                                    Tax.EndDate = Convert.ToDateTime(Dr1["EndDate"].ToString());
                                    Tax.CreateDate = Convert.ToDateTime(Dr1["CreateDate"].ToString());
                                    if ((Dr1["LastModDate"].ToString() != null) && (Dr1["LastModDate"].ToString() != "")) Tax.LastModDate = Convert.ToDateTime(Dr1["LastModDate"].ToString());

                                    int i;

                                    if (Tax.TaxKind.TaxKindId == 1)
                                    {
                                        foreach (StoreItem SIObj in StoreItemObjList)
                                        {
                                            i = 0;

                                            while (i < SIObj.TaxList.Count)
                                            {
                                                if (SIObj.TaxList[i].TaxCode == Tax.TaxCode)
                                                {
                                                    SIObj.TaxList[i].TaxDetail = Tax;
                                                }
                                                i = i + 1;
                                            }
                                        }
                                    }
                                }
                            }

                            StoreItemObjArray = StoreItemObjList.ToArray<StoreItem>();
                            retval = true;
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

        private bool GetStoreItemDetailsFromDB(string ItemId, string BatchId, out string errorMessage, out StoreItem StoreItemObj)
        {
            DataSet Ds;
            DataRow Dr;
            bool retval = false;

            errorMessage = null;

            StoreItemObj = null;

            try
            {
                SqlParameter[] SQLParamArray = new SqlParameter[2];

                SqlParameter parameter = new SqlParameter("@Item_Id", SqlDbType.VarChar);
                parameter.Value = (object)ItemId ?? DBNull.Value;
                SQLParamArray[0] = parameter;

                parameter = new SqlParameter("@IB_Batch_Id", SqlDbType.VarChar);
                parameter.Value = (object)BatchId ?? DBNull.Value;
                SQLParamArray[1] = parameter;

                DBObj = SQLDataAccess.GetDBObj(out errorMessage);

                if (errorMessage != null)
                    return retval;

                Ds = null;

                if (DBObj.ExecuteSPWithParamsRespDS(out Ds, out errorMessage, "NewAppDb..GetStoreItemBatch", SQLParamArray) == false)
                {
                    if (errorMessage != null)
                        return retval;
                }
                else
                {
                    if (Ds.Tables.Count > 0)
                    {
                        if (Ds.Tables[0].Rows.Count > 0)
                        {
                            ManufacturerFactory ManufFactory = new ManufacturerFactory();
                            VendorFactory VendorFactory = new VendorFactory();

                            Manufacturer ManufObj;
                            Vendor VendorObj;

                            StoreItemObj = new StoreItem();

                            Dr = Ds.Tables[0].Rows[0];

                            StoreItemObj.ItemId = Dr[0].ToString();
                            StoreItemObj.ItemName = Dr[1].ToString();
                            StoreItemObj.ItemDescription = Dr[2].ToString();
                            StoreItemObj.BatchId = Dr[3].ToString();
                            ManufObj = new Manufacturer();
                            if (ManufFactory.GetManufacturerBasicDetails(Convert.ToString(Dr[4].ToString()), out errorMessage, out ManufObj) == true)
                                StoreItemObj.ManufacturerInfo = ManufObj;
                            else
                                return retval;
                            StoreItemObj.QtyProcured = Convert.ToInt32(Dr[5].ToString());
                            StoreItemObj.ProcuredPricePerUnit = Convert.ToDouble(Dr[6].ToString());
                            StoreItemObj.MRP = Convert.ToDouble(Dr[7].ToString());
                            StoreItemObj.DateProcured = Convert.ToDateTime(Dr[8].ToString());
                            StoreItemObj.DateOfManuf = Convert.ToDateTime(Dr[9].ToString());
                            StoreItemObj.DateOfExp = Convert.ToDateTime(Dr[10].ToString());
                            StoreItemObj.WeightProcured = Convert.ToDouble(Dr[11].ToString());
                            StoreItemObj.WeightAvailable = Convert.ToDouble(Dr[12].ToString());
                            StoreItemObj.BarCode = Dr[13].ToString();
                            StoreItemObj.SellingPricePerUnit = Convert.ToDouble(Dr[14].ToString());
                            StoreItemObj.QtyAvailable = Convert.ToInt32(Dr[15].ToString());
                            StoreItemObj.ItemCreateDate = Convert.ToDateTime(Dr[16].ToString());
                            if ((Dr[17].ToString() != null) && (Dr[17].ToString() != "")) StoreItemObj.ItemLastModDate = Convert.ToDateTime(Dr[17].ToString());
                            StoreItemObj.BatchCreateDate = Convert.ToDateTime(Dr[18].ToString());
                            if ((Dr[19].ToString() != null) && (Dr[19].ToString() != "")) StoreItemObj.BatchLastModDate = Convert.ToDateTime(Dr[19].ToString());
                            
                            VendorObj = new Vendor();
                            if (VendorFactory.GetVendorBasicDetails(Convert.ToString(Dr[20].ToString()), out errorMessage, out VendorObj) == true)
                                StoreItemObj.VendorInfo = VendorObj;
                            else
                                return retval;


                            if (Ds.Tables[1] != null)
                            {
                                DiscountItem DiscItem;

                                foreach (DataRow Dr1 in Ds.Tables[1].Rows)
                                {
                                    DiscItem = new DiscountItem();

                                    DiscItem.DiscItemId = Dr1["ItemId"].ToString();
                                    DiscItem.DiscItemBatchId = Dr1["BatchId"].ToString();
                                    DiscItem.DiscountCode = Dr1["DiscCode"].ToString();
                                    DiscItem.DiscItemStartDate = Convert.ToDateTime(Dr1["DiscStartDate"].ToString());
                                    DiscItem.DiscItemEndDate = Convert.ToDateTime(Dr1["DiscEndDate"].ToString());
                                    DiscItem.DiscRemarks = Dr1["DiscRemarks"].ToString();
                                    DiscItem.DiscCreateDate = Convert.ToDateTime(Dr1["DiscCreateDate"].ToString());
                                    if ((Dr1["DiscLastModDate"].ToString() != null) && (Dr1["DiscLastModDate"].ToString() != "")) DiscItem.DiscLastModDate = Convert.ToDateTime(Dr1["DiscLastModDate"].ToString());

                                    StoreItemObj.DiscountList.Add(DiscItem);


                                }
                            }

                            if (Ds.Tables[2] != null)
                            {
                                Discount Disc;
                                DiscountKind DiscKind;
                                DiscountKindFactory DiscKindFactory = new DiscountKindFactory();
                                DiscountType DiscType;
                                DiscountTypeFactory DiscTypeFactory = new DiscountTypeFactory();


                                foreach (DataRow Dr1 in Ds.Tables[2].Rows)
                                {
                                    Disc = new Discount();

                                    Disc.DiscountCode = Dr1["DiscCode"].ToString();
                                    Disc.DiscountName = Dr1["DiscName"].ToString();
                                    Disc.DiscountDescription = Dr1["DiscDescription"].ToString();
                                    DiscKind = new DiscountKind();
                                    if (DiscKindFactory.GetDiscountKind(Convert.ToInt32(Dr1["DiscKindId"].ToString()), out DiscKind, out errorMessage) == true)
                                        Disc.DiscountKind = DiscKind;
                                    DiscType = new DiscountType();
                                    if (DiscTypeFactory.GetDiscountType(Convert.ToInt32(Dr1["DiscTypeId"].ToString()), out DiscType, out errorMessage) == true)
                                        Disc.DiscountType = DiscType;
                                    Disc.DiscountValue = Convert.ToDouble(Dr1["DiscValue"].ToString());
                                    Disc.DiscStartDate = Convert.ToDateTime(Dr1["DiscStartDate"].ToString());
                                    Disc.DiscEndDate = Convert.ToDateTime(Dr1["DiscEndDate"].ToString());
                                    Disc.DiscCreateDate = Convert.ToDateTime(Dr1["DiscCreateDate"].ToString());
                                    if ((Dr1["DiscLastModDate"].ToString() != null) && (Dr1["DiscLastModDate"].ToString() != "")) Disc.DiscLastModDate = Convert.ToDateTime(Dr1["DiscLastModDate"].ToString());

                                    StoreItemObj.DiscountList[0].DiscountDetail = Disc;
                                }
                            }

                            if (Ds.Tables[3] != null)
                            {
                                TaxItem TaxItem;

                                foreach (DataRow Dr1 in Ds.Tables[3].Rows)
                                {
                                    TaxItem = new TaxItem();

                                    TaxItem.ItemId = Dr1["ItemId"].ToString();
                                    TaxItem.TaxCode = Dr1["TaxCode"].ToString();
                                    TaxItem.BatchId = Dr1["BatchId"].ToString();
                                    TaxItem.StartDate = Convert.ToDateTime(Dr1["StartDate"].ToString());
                                    TaxItem.EndDate = Convert.ToDateTime(Dr1["EndDate"].ToString());
                                    TaxItem.Remarks = Dr1["Remarks"].ToString();
                                    TaxItem.CreateDate = Convert.ToDateTime(Dr1["CreateDate"].ToString());
                                    if ((Dr1["LastModDate"].ToString() != null) && (Dr1["LastModDate"].ToString() != "")) TaxItem.LastModDate = Convert.ToDateTime(Dr1["LastModDate"].ToString());


                                    StoreItemObj.TaxList.Add(TaxItem);
                                }
                            }

                            if (Ds.Tables[4] != null)
                            {
                                Tax Tax;
                                TaxKind TaxKind;
                                TaxType TaxType;
                                TaxKindFactory TaxKindFactory = new TaxKindFactory();
                                TaxTypeFactory TaxTypeFactory = new TaxTypeFactory();


                                foreach (DataRow Dr1 in Ds.Tables[4].Rows)
                                {
                                    Tax = new Tax();

                                    Tax.TaxCode = Dr1["TaxCode"].ToString();
                                    Tax.TaxName = Dr1["TaxName"].ToString();
                                    Tax.TaxDescription = Dr1["TaxDescription"].ToString();
                                    TaxKind = new TaxKind();
                                    if (TaxKindFactory.GetTaxKind(Convert.ToInt32(Dr1["TaxKindId"].ToString()), out TaxKind, out errorMessage) == true)
                                        Tax.TaxKind = TaxKind;
                                    TaxType = new TaxType();
                                    if (TaxTypeFactory.GetTaxType(Convert.ToInt32(Dr1["TaxTypeId"].ToString()), out TaxType, out errorMessage) == true)
                                        Tax.TaxType = TaxType; Tax.TaxValue = Convert.ToDouble(Dr1["TaxValue"].ToString());
                                    Tax.StartDate = Convert.ToDateTime(Dr1["StartDate"].ToString());
                                    Tax.EndDate = Convert.ToDateTime(Dr1["EndDate"].ToString());
                                    Tax.CreateDate = Convert.ToDateTime(Dr1["CreateDate"].ToString());
                                    if ((Dr1["LastModDate"].ToString() != null) && (Dr1["LastModDate"].ToString() != "")) Tax.LastModDate = Convert.ToDateTime(Dr1["LastModDate"].ToString());

                                    StoreItemObj.TaxList[0].TaxDetail = Tax;
                                }
                            }
                            retval = true;
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

        private bool ComputeAmountsPerUnit(StoreItem StoreItemObj, out StoreItem NewStoreItemObj, out string errorMessage)
        {
            bool retval = false;


            errorMessage = null;
            double _disc_Amt_Per_Unit = 0;
            double _tax_Amt_Per_Unit = 0;

            NewStoreItemObj = new StoreItem();

            try
            {
                //Calculate Item Discount Amount Per Unit
                if (StoreItemObj.DiscountList != null)
                {
                    foreach (DiscountItem DiscIObj in StoreItemObj.DiscountList)
                    {
                        if (DiscIObj.DiscountDetail.DiscountType.DiscountTypeId == 0)
                            _disc_Amt_Per_Unit = _disc_Amt_Per_Unit + DiscIObj.DiscountDetail.DiscountValue;
                        else if (DiscIObj.DiscountDetail.DiscountType.DiscountTypeId == 1)
                            _disc_Amt_Per_Unit = _disc_Amt_Per_Unit + (StoreItemObj.SellingPricePerUnit * (DiscIObj.DiscountDetail.DiscountValue) / 100);
                    }

                    StoreItemObj.DiscountAmountPerUnit = _disc_Amt_Per_Unit;
                }

                //Calculate Item Tax Amount Per Unit
                if (StoreItemObj.TaxList != null)
                {
                    foreach (TaxItem TaxItemObj in StoreItemObj.TaxList)
                    {
                        if (TaxItemObj.TaxDetail.TaxType.TaxTypeId == 0)
                            _tax_Amt_Per_Unit = _tax_Amt_Per_Unit + TaxItemObj.TaxDetail.TaxValue;
                        else if (TaxItemObj.TaxDetail.TaxType.TaxTypeId == 1)
                            _tax_Amt_Per_Unit = _tax_Amt_Per_Unit + (StoreItemObj.SellingPricePerUnit * (TaxItemObj.TaxDetail.TaxValue) / 100);
                    }

                    StoreItemObj.TaxAmountPerUnit = _tax_Amt_Per_Unit;
                }

                NewStoreItemObj = StoreItemObj;

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
