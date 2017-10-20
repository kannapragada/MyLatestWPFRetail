using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Xml;
using System.Data;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Collections;
using NewApp.BusinessTier.Models;
using WCFRetailService;




namespace WCFRetailService
{
    public class SaleFactory : ISaleFactory
    {
        //Declarations
        # region Declarations

            SQLDataAccess DBObj;
        #endregion

        //Constructor
        public SaleFactory()
        {
        }

        //Member Methods 
        #region MemberMethods

            public bool GetNewSaleId(out string Sale_Id, out string errorMessage)
        {

            bool retval = false;
            errorMessage = null; 

            Sale_Id = null;
            
            DBObj = SQLDataAccess.GetDBObj(out errorMessage);

            if (errorMessage != null)
                return retval;

            SqlParameter[] SQLParamArray = new SqlParameter[1];

            SqlParameter parameter = new SqlParameter("@SaleId", SqlDbType.VarChar,30);
            parameter.Value = "";
            parameter.Direction = ParameterDirection.Output;
            SQLParamArray[0] = parameter;

            if (DBObj.ExecuteSPWithParamsNoResp(out errorMessage, "NewAppDb..GetNextSaleId", SQLParamArray) == true)
            {
                Sale_Id = SQLParamArray[0].Value.ToString();
                retval = true;
            }

            return retval;
        }

            public bool CreateSale(Sale SaleObj, out string errorMessage)
            {
                bool retval = false;
                errorMessage = null;

                DBObj = SQLDataAccess.GetDBObj(out errorMessage);

                if (errorMessage != null)
                    return retval;

                try
                {
                    XMLProcessing XMLProcessing = new XMLProcessing();
                    XmlDocument SaleXml = XMLProcessing.WriteXML(SaleObj);

                    SqlParameter[] SQLParamArray = new SqlParameter[3];

                    SqlParameter parameter = new SqlParameter("@SaleId", SqlDbType.VarChar);
                    parameter.Value = SaleObj.SalesId;
                    SQLParamArray[0] = parameter;

                    parameter = new SqlParameter("@OrderDoc", SqlDbType.Xml);
                    parameter.Value = SaleXml.InnerXml.ToString(); ;
                    SQLParamArray[1] = parameter;

                    parameter = new SqlParameter("@SaleStatus", SqlDbType.Int);
                    parameter.Value = "";
                    parameter.Direction = ParameterDirection.Output;
                    SQLParamArray[2] = parameter;

                    if (DBObj.ExecuteSPWithParamsNoResp(out errorMessage, "NewAppDb..CreateSale", SQLParamArray) == true)
                    {
                        SaleObj.SaleStatus = Convert.ToInt32(SQLParamArray[2].Value.ToString());

                        if (SaleObj.SaleStatus == 3)
                            retval = true;
                    }
                }
                catch (Exception ex1)
                {
                    errorMessage = ex1.Message;
                }

                return retval;
            }

            public bool GetSaleDetails(string SalesId, string CustId, string CustName, DateTime StartDateOfSale, DateTime EndDateOfSale, out Sale[] SaleObjArray, out string errorMessage)
            {
                DataSet Ds = new DataSet();
                DataRow Dr;

                bool retval = false;

                errorMessage = null;

                SaleObjArray = null;
                List<Sale> SaleObjList = null;

                try
                {
                    DBObj = SQLDataAccess.GetDBObj(out errorMessage);

                    if (errorMessage != null)
                        return retval;

                    SqlParameter[] SQLParamArray = new SqlParameter[5];

                    SqlParameter parameter = new SqlParameter("@Sales_Id", SqlDbType.VarChar);
                    parameter.Value = (object)SalesId ?? DBNull.Value;
                    SQLParamArray[0] = parameter;

                    parameter = new SqlParameter("@Cust_Id", SqlDbType.VarChar);
                    parameter.Value = (object)CustId ?? DBNull.Value;
                    SQLParamArray[1] = parameter;

                    parameter = new SqlParameter("@Cust_Name", SqlDbType.VarChar);
                    parameter.Value = (object)CustName ?? DBNull.Value;
                    SQLParamArray[2] = parameter;

                    parameter = new SqlParameter("@Sale_Start_Date", SqlDbType.DateTime);
                    if ((StartDateOfSale.Year > 1900) && (StartDateOfSale.Year < 2500))
                        parameter.Value = (object)StartDateOfSale;
                    else
                        parameter.Value = DBNull.Value;
                    SQLParamArray[3] = parameter;

                    parameter = new SqlParameter("@Sale_End_Date", SqlDbType.DateTime);
                    if ((EndDateOfSale.Year > 1900) && (EndDateOfSale.Year < 2500))
                        parameter.Value = (object)EndDateOfSale;
                    else
                        parameter.Value = DBNull.Value;
                    SQLParamArray[4] = parameter;


                    DBObj = SQLDataAccess.GetDBObj(out errorMessage);

                    if (DBObj.ExecuteSPWithParamsRespDS(out Ds, out errorMessage, "NewAppDb..GetSaleDetails", SQLParamArray) == false)
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
                                Sale SaleObj = null;
                                SaleObjList = new List<Sale>();
                                List<SaleItem> SaleItemList = new List<SaleItem>();

                                # region looping

                                for (int i = 0; i < Ds.Tables[0].Rows.Count; i++)
                                {
                                    SaleObj = new Sale();

                                    Dr = Ds.Tables[0].Rows[i];

                                    SaleObj.SalesId = Dr[0].ToString();
                                    SaleObj.CustomerInfo.CustomerId = Dr[1].ToString();
                                    SaleObj.TotalSaleAmount = Convert.ToDouble(Dr[2].ToString());
                                    SaleObj.TotalDiscountAmount = Convert.ToDouble(Dr[3].ToString());
                                    SaleObj.TotalTaxAmount = Convert.ToDouble(Dr[4].ToString());
                                    SaleObj.FinalSaleAmount = Convert.ToDouble(Dr[5].ToString());
                                    SaleObj.PaidAmount = Convert.ToDouble(Dr[6].ToString());
                                    SaleObj.BalanceAmount = Convert.ToDouble(Dr[7].ToString());
                                    SaleObj.SaleDate = Convert.ToDateTime(Dr[8].ToString());
                                    SaleObj.LastModificationDate = Convert.ToDateTime(Dr[9].ToString());

                                    if ((SaleObj != null) && (Ds.Tables[1] != null))
                                    {
                                        SaleItem SaleItem;

                                        foreach (DataRow Dr1 in Ds.Tables[1].Rows)
                                        {
                                            SaleItem = new SaleItem();

                                            if (Dr1[0].ToString() == SaleObj.SalesId)
                                            {
                                                SaleItem.SalesId = Dr1[0].ToString();
                                                SaleItem.SerialNumber = Convert.ToInt32(Dr1[1].ToString());
                                                SaleItem.ItemId = Dr1[2].ToString();
                                                SaleItem.ItemName = Dr1[3].ToString();
                                                SaleItem.BatchId = Dr1[4].ToString();
                                                ManufacturerFactory ManufFactory = new ManufacturerFactory();
                                                Manufacturer ManufObj = new Manufacturer();
                                                if (ManufFactory.GetManufacturerBasicDetails(Convert.ToString(Dr1[5].ToString()), out errorMessage, out ManufObj) == true)
                                                    SaleItem.StoreItemDetail.ManufacturerInfo = ManufObj;
                                                else
                                                    return retval;
                                                VendorFactory VendorFactory = new VendorFactory();
                                                Vendor VendorObj = new Vendor();
                                                if (VendorFactory.GetVendorBasicDetails(Convert.ToString(Dr1[6].ToString()), out errorMessage, out VendorObj) == true)
                                                    SaleItem.StoreItemDetail.VendorInfo = VendorObj;
                                                else
                                                    return retval;
                                                SaleItem.Quantity = Convert.ToInt32(Dr1[7].ToString());
                                                SaleItem.Weight = Convert.ToDouble(Dr1[8].ToString());
                                                SaleItem.ItemAmtPerUnit = Convert.ToDouble(Dr1[9].ToString());
                                                SaleItem.DiscountAmountPerUnit = Convert.ToDouble(Dr1[10].ToString());
                                                SaleItem.TaxAmountPerUnit = Convert.ToDouble(Dr1[11].ToString());
                                                SaleItem.TotalItemAmt = Convert.ToDouble(Dr1[12].ToString());
                                                SaleItem.ItemDiscAmount = Convert.ToDouble(Dr1[13].ToString());
                                                SaleItem.ItemTaxAmount = Convert.ToDouble(Dr1[14].ToString());
                                                SaleItem.FinalItemAmt = Convert.ToDouble(Dr1[15].ToString());
                                                SaleItem.SaleCreateDate = Convert.ToDateTime(Dr1[16].ToString());
                                                if ((Dr1[17].ToString() != null) && (Dr1[17].ToString() != "")) SaleItem.SaleLastModDate = Convert.ToDateTime(Dr1[17].ToString());

                                                SaleObj.SaleItemsList.Add(SaleItem);
                                            }
                                        }

                                        SaleObjList.Add(SaleObj);
                                    }
                                }
                                # endregion

                            }
                        }

                        SaleObjArray = SaleObjList.ToArray<Sale>();
                        retval = true;
                    }
                }
                catch (Exception ex1)
                {
                    errorMessage = ex1.Message;
                }

                return retval;
            }

            public bool SearchSaleDetails(string SalesId, string CustId, string CustName, DateTime StartDateOfSale, DateTime EndDateOfSale, out Sale[] SaleObjArray, out string errorMessage)
            {
                DataSet Ds = new DataSet();
                DataRow Dr;

                bool retval = false;

                errorMessage = null;

                List<Sale> SaleObjList = new List<Sale>();
                SaleObjArray = null;

                try
                {
                    DBObj = SQLDataAccess.GetDBObj(out errorMessage);

                    if (errorMessage != null)
                        return retval;

                    SqlParameter[] SQLParamArray = new SqlParameter[5];

                    SqlParameter parameter = new SqlParameter("@Sales_Id", SqlDbType.VarChar);
                    parameter.Value = (object)SalesId ?? DBNull.Value;
                    SQLParamArray[0] = parameter;

                    parameter = new SqlParameter("@Cust_Id", SqlDbType.VarChar);
                    parameter.Value = (object)CustId ?? DBNull.Value;
                    SQLParamArray[1] = parameter;

                    parameter = new SqlParameter("@Cust_Name", SqlDbType.VarChar);
                    parameter.Value = (object)CustName ?? DBNull.Value;
                    SQLParamArray[2] = parameter;

                    parameter = new SqlParameter("@Sale_Start_Date", SqlDbType.DateTime);
                    if ((StartDateOfSale.Year > 1900) && (StartDateOfSale.Year < 2500))
                        parameter.Value = (object)StartDateOfSale;
                    else
                        parameter.Value = DBNull.Value;
                    SQLParamArray[3] = parameter;

                    parameter = new SqlParameter("@Sale_End_Date", SqlDbType.DateTime);
                    if ((EndDateOfSale.Year > 1900) && (EndDateOfSale.Year < 2500))
                        parameter.Value = (object)EndDateOfSale;
                    else
                        parameter.Value = DBNull.Value;
                    SQLParamArray[4] = parameter;


                    DBObj = SQLDataAccess.GetDBObj(out errorMessage);

                    if (DBObj.ExecuteSPWithParamsRespDS(out Ds, out errorMessage, "NewAppDb..SearchSaleDetails", SQLParamArray) == false)
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
                                Sale SaleObj = null;
                                SaleObjList = new List<Sale>();
                                List<SaleItem> SaleItemList = new List<SaleItem>();
                                Customer CustObj;
                                CustomerFactory CustFactory = new CustomerFactory();
                                ManufacturerFactory ManufFactory = new ManufacturerFactory();
                                Manufacturer ManufObj;
                                VendorFactory VendorFactory = new VendorFactory();
                                Vendor VendorObj;

                                # region looping

                                for (int i = 0; i < Ds.Tables[0].Rows.Count; i++)
                                {
                                    SaleObj = new Sale();

                                    Dr = Ds.Tables[0].Rows[i];

                                    SaleObj.SalesId = Dr[0].ToString();
                                    SaleObj.CustomerInfo.CustomerId = Dr[1].ToString();

                                    CustObj = new Customer();
                                    if (CustFactory.GetCustomerDetails(Convert.ToString(Dr[1].ToString()), out errorMessage, out CustObj) == true)
                                        SaleObj.CustomerInfo = CustObj;
                                    else
                                        return retval;
                                    SaleObj.TotalSaleAmount = Convert.ToDouble(Dr[2].ToString());
                                    SaleObj.TotalDiscountAmount = Convert.ToDouble(Dr[3].ToString());
                                    SaleObj.TotalTaxAmount = Convert.ToDouble(Dr[4].ToString());
                                    SaleObj.FinalSaleAmount = Convert.ToDouble(Dr[5].ToString());
                                    SaleObj.PaidAmount = Convert.ToDouble(Dr[6].ToString());
                                    SaleObj.BalanceAmount = Convert.ToDouble(Dr[7].ToString());
                                    SaleObj.SaleDate = Convert.ToDateTime(Dr[8].ToString());
                                    SaleObj.LastModificationDate = Convert.ToDateTime(Dr[9].ToString());

                                    if ((SaleObj != null) && (Ds.Tables[1] != null))
                                    {
                                        SaleItem SaleItem;

                                        foreach (DataRow Dr1 in Ds.Tables[1].Rows)
                                        {
                                            SaleItem = new SaleItem();

                                            if (Dr1[0].ToString() == SaleObj.SalesId)
                                            {
                                                SaleItem.SalesId = Dr1[0].ToString();
                                                SaleItem.SerialNumber = Convert.ToInt32(Dr1[1].ToString());
                                                SaleItem.ItemId = Dr1[2].ToString();
                                                SaleItem.ItemName = Dr1[3].ToString();
                                                SaleItem.BatchId = Dr1[4].ToString();
                                                ManufObj = new Manufacturer();
                                                if (ManufFactory.GetManufacturerBasicDetails(Convert.ToString(Dr1[5].ToString()), out errorMessage, out ManufObj) == true)
                                                    SaleItem.StoreItemDetail.ManufacturerInfo = ManufObj;
                                                else
                                                    return retval;
                                                VendorObj = new Vendor();
                                                if (VendorFactory.GetVendorBasicDetails(Convert.ToString(Dr1[6].ToString()), out errorMessage, out VendorObj) == true)
                                                    SaleItem.StoreItemDetail.VendorInfo = VendorObj;
                                                else
                                                    return retval;
                                                SaleItem.Quantity = Convert.ToInt32(Dr1[7].ToString());
                                                SaleItem.Weight = Convert.ToDouble(Dr1[8].ToString());
                                                SaleItem.ItemAmtPerUnit = Convert.ToDouble(Dr1[9].ToString());
                                                SaleItem.DiscountAmountPerUnit = Convert.ToDouble(Dr1[10].ToString());
                                                SaleItem.TaxAmountPerUnit = Convert.ToDouble(Dr1[11].ToString());
                                                SaleItem.TotalItemAmt = Convert.ToDouble(Dr1[12].ToString());
                                                SaleItem.ItemDiscAmount = Convert.ToDouble(Dr1[13].ToString());
                                                SaleItem.ItemTaxAmount = Convert.ToDouble(Dr1[14].ToString());
                                                SaleItem.FinalItemAmt = Convert.ToDouble(Dr1[15].ToString());
                                                SaleItem.SaleCreateDate = Convert.ToDateTime(Dr1[16].ToString());
                                                if ((Dr1[17].ToString() != null) && (Dr1[17].ToString() != "")) SaleItem.SaleLastModDate = Convert.ToDateTime(Dr1[17].ToString());

                                                SaleObj.SaleItemsList.Add(SaleItem);
                                            }
                                        }

                                        SaleObjList.Add(SaleObj);
                                    }
                                }
                                # endregion

                            }
                        }

                        SaleObjArray = SaleObjList.ToArray<Sale>();
                        retval = true;
                    }
                }
                catch (Exception ex1)
                {
                    errorMessage = ex1.Message;
                }

                return retval;
            }

            public bool GetUserSaleDetails(string SalesId, string UserId, string UserName, DateTime StartDateOfSale, DateTime EndDateOfSale, out Sale[] SaleObjArray, out string errorMessage)
            {
                DataSet Ds = new DataSet();
                DataRow Dr;

                bool retval = false;

                errorMessage = null;

                SaleObjArray = null;
                List<Sale> SaleObjList = null;

                try
                {
                    DBObj = SQLDataAccess.GetDBObj(out errorMessage);

                    if (errorMessage != null)
                        return retval;

                    SqlParameter[] SQLParamArray = new SqlParameter[5];

                    SqlParameter parameter = new SqlParameter("@Sales_Id", SqlDbType.VarChar);
                    parameter.Value = (object)SalesId ?? DBNull.Value;
                    SQLParamArray[0] = parameter;

                    parameter = new SqlParameter("@User_Id", SqlDbType.VarChar);
                    parameter.Value = (object)UserId ?? DBNull.Value;
                    SQLParamArray[1] = parameter;

                    parameter = new SqlParameter("@User_Name", SqlDbType.VarChar);
                    parameter.Value = (object)UserName ?? DBNull.Value;
                    SQLParamArray[2] = parameter;

                    parameter = new SqlParameter("@Sale_Start_Date", SqlDbType.DateTime);
                    if ((StartDateOfSale.Year > 1900) && (StartDateOfSale.Year < 2500))
                        parameter.Value = (object)StartDateOfSale;
                    else
                        parameter.Value = DBNull.Value;
                    SQLParamArray[3] = parameter;

                    parameter = new SqlParameter("@Sale_End_Date", SqlDbType.DateTime);
                    if ((EndDateOfSale.Year > 1900) && (EndDateOfSale.Year < 2500))
                        parameter.Value = (object)EndDateOfSale;
                    else
                        parameter.Value = DBNull.Value;
                    SQLParamArray[4] = parameter;


                    DBObj = SQLDataAccess.GetDBObj(out errorMessage);

                    if (DBObj.ExecuteSPWithParamsRespDS(out Ds, out errorMessage, "NewAppDb..GetUserSaleDetails", SQLParamArray) == false)
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
                                Sale SaleObj = null;
                                SaleObjList = new List<Sale>();
                                List<SaleItem> SaleItemList = new List<SaleItem>();

                                # region looping

                                for (int i = 0; i < Ds.Tables[0].Rows.Count; i++)
                                {
                                    SaleObj = new Sale();

                                    Dr = Ds.Tables[0].Rows[i];

                                    SaleObj.SalesId = Dr[0].ToString();
                                    SaleObj.CustomerInfo.CustomerId = Dr[1].ToString();
                                    SaleObj.TotalSaleAmount = Convert.ToDouble(Dr[2].ToString());
                                    SaleObj.TotalDiscountAmount = Convert.ToDouble(Dr[3].ToString());
                                    SaleObj.TotalTaxAmount = Convert.ToDouble(Dr[4].ToString());
                                    SaleObj.FinalSaleAmount = Convert.ToDouble(Dr[5].ToString());
                                    SaleObj.PaidAmount = Convert.ToDouble(Dr[6].ToString());
                                    SaleObj.BalanceAmount = Convert.ToDouble(Dr[7].ToString());
                                    SaleObj.SaleDate = Convert.ToDateTime(Dr[8].ToString());
                                    SaleObj.LastModificationDate = Convert.ToDateTime(Dr[9].ToString());

                                    if ((SaleObj != null) && (Ds.Tables[1] != null))
                                    {
                                        SaleItem SaleItem;

                                        foreach (DataRow Dr1 in Ds.Tables[1].Rows)
                                        {
                                            SaleItem = new SaleItem();

                                            if (Dr1[0].ToString() == SaleObj.SalesId)
                                            {
                                                SaleItem.SalesId = Dr1[0].ToString();
                                                SaleItem.SerialNumber = Convert.ToInt32(Dr1[1].ToString());
                                                SaleItem.ItemId = Dr1[2].ToString();
                                                SaleItem.ItemName = Dr1[3].ToString();
                                                SaleItem.BatchId = Dr1[4].ToString();
                                                ManufacturerFactory ManufFactory = new ManufacturerFactory();
                                                Manufacturer ManufObj = new Manufacturer();
                                                if (ManufFactory.GetManufacturerBasicDetails(Convert.ToString(Dr1[5].ToString()), out errorMessage, out ManufObj) == true)
                                                    SaleItem.StoreItemDetail.ManufacturerInfo = ManufObj;
                                                else
                                                    return retval;
                                                VendorFactory VendorFactory = new VendorFactory();
                                                Vendor VendorObj = new Vendor();
                                                if (VendorFactory.GetVendorBasicDetails(Convert.ToString(Dr1[6].ToString()), out errorMessage, out VendorObj) == true)
                                                    SaleItem.StoreItemDetail.VendorInfo = VendorObj;
                                                else
                                                    return retval;
                                                SaleItem.Quantity = Convert.ToInt32(Dr1[7].ToString());
                                                SaleItem.Weight = Convert.ToDouble(Dr1[8].ToString());
                                                SaleItem.ItemAmtPerUnit = Convert.ToDouble(Dr1[9].ToString());
                                                SaleItem.DiscountAmountPerUnit = Convert.ToDouble(Dr1[10].ToString());
                                                SaleItem.TaxAmountPerUnit = Convert.ToDouble(Dr1[11].ToString());
                                                SaleItem.TotalItemAmt = Convert.ToDouble(Dr1[12].ToString());
                                                SaleItem.ItemDiscAmount = Convert.ToDouble(Dr1[13].ToString());
                                                SaleItem.ItemTaxAmount = Convert.ToDouble(Dr1[14].ToString());
                                                SaleItem.FinalItemAmt = Convert.ToDouble(Dr1[15].ToString());
                                                SaleItem.SaleCreateDate = Convert.ToDateTime(Dr1[16].ToString());
                                                if ((Dr1[17].ToString() != null) && (Dr1[17].ToString() != "")) SaleItem.SaleLastModDate = Convert.ToDateTime(Dr1[17].ToString());

                                                SaleObj.SaleItemsList.Add(SaleItem);
                                            }
                                        }

                                        SaleObjList.Add(SaleObj);
                                    }
                                }
                                # endregion

                            }
                        }

                        SaleObjArray = SaleObjList.ToArray<Sale>();
                        retval = true;
                    }
                }
                catch (Exception ex1)
                {
                    errorMessage = ex1.Message;
                }

                return retval;
            }

            public bool GetManufSaleDetails(string SalesId, string ManufId, string ManufName, DateTime StartDateOfSale, DateTime EndDateOfSale, out Sale[] SaleObjArray, out string errorMessage)
            {
                DataSet Ds = new DataSet();
                DataRow Dr;

                bool retval = false;

                errorMessage = null;

                SaleObjArray = null;
                List<Sale> SaleObjList = null;

                try
                {
                    DBObj = SQLDataAccess.GetDBObj(out errorMessage);

                    if (errorMessage != null)
                        return retval;

                    SqlParameter[] SQLParamArray = new SqlParameter[5];

                    SqlParameter parameter = new SqlParameter("@Sales_Id", SqlDbType.VarChar);
                    parameter.Value = (object)SalesId ?? DBNull.Value;
                    SQLParamArray[0] = parameter;

                    parameter = new SqlParameter("@Manuf_Id", SqlDbType.VarChar);
                    parameter.Value = (object)ManufId ?? DBNull.Value;
                    SQLParamArray[1] = parameter;

                    parameter = new SqlParameter("@Manuf_Name", SqlDbType.VarChar);
                    parameter.Value = (object)ManufName ?? DBNull.Value;
                    SQLParamArray[2] = parameter;

                    parameter = new SqlParameter("@Sale_Start_Date", SqlDbType.DateTime);
                    if ((StartDateOfSale.Year > 1900) && (StartDateOfSale.Year < 2500))
                        parameter.Value = (object)StartDateOfSale;
                    else
                        parameter.Value = DBNull.Value;
                    SQLParamArray[3] = parameter;

                    parameter = new SqlParameter("@Sale_End_Date", SqlDbType.DateTime);
                    if ((EndDateOfSale.Year > 1900) && (EndDateOfSale.Year < 2500))
                        parameter.Value = (object)EndDateOfSale;
                    else
                        parameter.Value = DBNull.Value;
                    SQLParamArray[4] = parameter;


                    DBObj = SQLDataAccess.GetDBObj(out errorMessage);

                    if (DBObj.ExecuteSPWithParamsRespDS(out Ds, out errorMessage, "NewAppDb..GetManufSaleDetails", SQLParamArray) == false)
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
                                Sale SaleObj = null;
                                SaleObjList = new List<Sale>();
                                List<SaleItem> SaleItemList = new List<SaleItem>();

                                CustomerFactory MyCustomerFactory;
                                Customer CustObj;
                                
                                StoreItemFactory MyStoreItemFactory;
                                List<StoreItem> StoreItemObjList;
                                StoreItem[] StoreItemObjArray;


                                # region looping

                                for (int i = 0; i < Ds.Tables[0].Rows.Count; i++)
                                {
                                    SaleObj = new Sale();

                                    Dr = Ds.Tables[0].Rows[i];

                                    SaleObj.SalesId = Dr[0].ToString();
                                    SaleObj.CustomerInfo.CustomerId = Dr[1].ToString();
                                    MyCustomerFactory = new CustomerFactory();
                                    CustObj =  new Customer();
                                    if (MyCustomerFactory.GetCustomerBasicDetails(SaleObj.CustomerInfo.CustomerId, out errorMessage, out CustObj) == true)
                                        SaleObj.CustomerInfo = CustObj;
                                    else
                                        return retval;
                                    SaleObj.TotalSaleAmount = Convert.ToDouble(Dr[2].ToString());
                                    SaleObj.TotalDiscountAmount = Convert.ToDouble(Dr[3].ToString());
                                    SaleObj.TotalTaxAmount = Convert.ToDouble(Dr[4].ToString());
                                    SaleObj.FinalSaleAmount = Convert.ToDouble(Dr[5].ToString());
                                    SaleObj.PaidAmount = Convert.ToDouble(Dr[6].ToString());
                                    SaleObj.BalanceAmount = Convert.ToDouble(Dr[7].ToString());
                                    SaleObj.SaleDate = Convert.ToDateTime(Dr[8].ToString());
                                    SaleObj.LastModificationDate = Convert.ToDateTime(Dr[9].ToString());

                                    if ((SaleObj != null) && (Ds.Tables[1] != null))
                                    {
                                        SaleItem SaleItem;

                                        foreach (DataRow Dr1 in Ds.Tables[1].Rows)
                                        {
                                            SaleItem = new SaleItem();

                                            if (Dr1[0].ToString() == SaleObj.SalesId)
                                            {
                                                SaleItem.SalesId = Dr1[0].ToString();
                                                SaleItem.SerialNumber = Convert.ToInt32(Dr1[1].ToString());
                                                SaleItem.ItemId = Dr1[2].ToString();
                                                SaleItem.ItemName = Dr1[3].ToString();
                                                SaleItem.BatchId = Dr1[4].ToString();
                                                MyStoreItemFactory = new StoreItemFactory();
                                                StoreItemObjList = new List<StoreItem>();
                                                StoreItemObjArray = null;
                                                if (MyStoreItemFactory.GetStoreItems(SaleItem.ItemId, null, SaleItem.BatchId, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, out errorMessage, out StoreItemObjArray) == true)
                                                {
                                                    if (StoreItemObjArray != null)
                                                        SaleItem.StoreItemDetail = (StoreItem)StoreItemObjArray[0];
                                                }
                                                else
                                                    return retval;
                                                SaleItem.Quantity = Convert.ToInt32(Dr1[7].ToString());
                                                SaleItem.Weight = Convert.ToDouble(Dr1[8].ToString());
                                                SaleItem.ItemAmtPerUnit = Convert.ToDouble(Dr1[9].ToString());
                                                SaleItem.DiscountAmountPerUnit = Convert.ToDouble(Dr1[10].ToString());
                                                SaleItem.TaxAmountPerUnit = Convert.ToDouble(Dr1[11].ToString());
                                                SaleItem.TotalItemAmt = Convert.ToDouble(Dr1[12].ToString());
                                                SaleItem.ItemDiscAmount = Convert.ToDouble(Dr1[13].ToString());
                                                SaleItem.ItemTaxAmount = Convert.ToDouble(Dr1[14].ToString());
                                                SaleItem.FinalItemAmt = Convert.ToDouble(Dr1[15].ToString());
                                                SaleItem.SaleCreateDate = Convert.ToDateTime(Dr1[16].ToString());
                                                if ((Dr1[17].ToString() != null) && (Dr1[17].ToString() != "")) SaleItem.SaleLastModDate = Convert.ToDateTime(Dr1[17].ToString());

                                                SaleObj.SaleItemsList.Add(SaleItem);
                                            }
                                        }

                                        SaleObjList.Add(SaleObj);
                                    }
                                }
                                # endregion

                            }
                        }

                        SaleObjArray = SaleObjList.ToArray<Sale>();
                        retval = true;
                    }
                }
                catch (Exception ex1)
                {
                    errorMessage = ex1.Message;
                }

                return retval;
            }

            public bool GetVendorSaleDetails(string SalesId, string VendorId, string VendorName, DateTime StartDateOfSale, DateTime EndDateOfSale, out Sale[] SaleObjArray, out string errorMessage)
            {
                DataSet Ds = new DataSet();
                DataRow Dr;

                bool retval = false;

                errorMessage = null;

                SaleObjArray = null;
                List<Sale> SaleObjList = null;

                try
                {
                    DBObj = SQLDataAccess.GetDBObj(out errorMessage);

                    if (errorMessage != null)
                        return retval;

                    SqlParameter[] SQLParamArray = new SqlParameter[5];

                    SqlParameter parameter = new SqlParameter("@Sales_Id", SqlDbType.VarChar);
                    parameter.Value = (object)SalesId ?? DBNull.Value;
                    SQLParamArray[0] = parameter;

                    parameter = new SqlParameter("@Vendor_Id", SqlDbType.VarChar);
                    parameter.Value = (object)VendorId ?? DBNull.Value;
                    SQLParamArray[1] = parameter;

                    parameter = new SqlParameter("@Vendor_Name", SqlDbType.VarChar);
                    parameter.Value = (object)VendorName ?? DBNull.Value;
                    SQLParamArray[2] = parameter;

                    parameter = new SqlParameter("@Sale_Start_Date", SqlDbType.DateTime);
                    if ((StartDateOfSale.Year > 1900) && (StartDateOfSale.Year < 2500))
                        parameter.Value = (object)StartDateOfSale;
                    else
                        parameter.Value = DBNull.Value;
                    SQLParamArray[3] = parameter;

                    parameter = new SqlParameter("@Sale_End_Date", SqlDbType.DateTime);
                    if ((EndDateOfSale.Year > 1900) && (EndDateOfSale.Year < 2500))
                        parameter.Value = (object)EndDateOfSale;
                    else
                        parameter.Value = DBNull.Value;
                    SQLParamArray[4] = parameter;


                    DBObj = SQLDataAccess.GetDBObj(out errorMessage);

                    if (DBObj.ExecuteSPWithParamsRespDS(out Ds, out errorMessage, "NewAppDb..GetVendorSaleDetails", SQLParamArray) == false)
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
                                Sale SaleObj = null;
                                SaleObjList = new List<Sale>();
                                List<SaleItem> SaleItemList = new List<SaleItem>();

                                CustomerFactory MyCustomerFactory;
                                Customer CustObj;

                                StoreItemFactory MyStoreItemFactory;
                                List<StoreItem> StoreItemObjList;
                                StoreItem[] StoreItemObjArray;


                                # region looping

                                for (int i = 0; i < Ds.Tables[0].Rows.Count; i++)
                                {
                                    SaleObj = new Sale();

                                    Dr = Ds.Tables[0].Rows[i];

                                    SaleObj.SalesId = Dr[0].ToString();
                                    SaleObj.CustomerInfo.CustomerId = Dr[1].ToString();
                                    MyCustomerFactory = new CustomerFactory();
                                    CustObj = new Customer();
                                    if (MyCustomerFactory.GetCustomerBasicDetails(SaleObj.CustomerInfo.CustomerId, out errorMessage, out CustObj) == true)
                                        SaleObj.CustomerInfo = CustObj;
                                    else
                                        return retval;
                                    SaleObj.TotalSaleAmount = Convert.ToDouble(Dr[2].ToString());
                                    SaleObj.TotalDiscountAmount = Convert.ToDouble(Dr[3].ToString());
                                    SaleObj.TotalTaxAmount = Convert.ToDouble(Dr[4].ToString());
                                    SaleObj.FinalSaleAmount = Convert.ToDouble(Dr[5].ToString());
                                    SaleObj.PaidAmount = Convert.ToDouble(Dr[6].ToString());
                                    SaleObj.BalanceAmount = Convert.ToDouble(Dr[7].ToString());
                                    SaleObj.SaleDate = Convert.ToDateTime(Dr[8].ToString());
                                    SaleObj.LastModificationDate = Convert.ToDateTime(Dr[9].ToString());

                                    if ((SaleObj != null) && (Ds.Tables[1] != null))
                                    {
                                        SaleItem SaleItem;

                                        foreach (DataRow Dr1 in Ds.Tables[1].Rows)
                                        {
                                            SaleItem = new SaleItem();

                                            if (Dr1[0].ToString() == SaleObj.SalesId)
                                            {
                                                SaleItem.SalesId = Dr1[0].ToString();
                                                SaleItem.SerialNumber = Convert.ToInt32(Dr1[1].ToString());
                                                SaleItem.ItemId = Dr1[2].ToString();
                                                SaleItem.ItemName = Dr1[3].ToString();
                                                SaleItem.BatchId = Dr1[4].ToString();
                                                MyStoreItemFactory = new StoreItemFactory();
                                                StoreItemObjList = new List<StoreItem>();
                                                StoreItemObjArray = null;
                                                if (MyStoreItemFactory.GetStoreItems(SaleItem.ItemId, null, SaleItem.BatchId, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, out errorMessage, out StoreItemObjArray) == true)
                                                {
                                                    if (StoreItemObjArray != null)
                                                        SaleItem.StoreItemDetail = (StoreItem)StoreItemObjArray[0];
                                                }
                                                else
                                                    return retval;
                                                SaleItem.Quantity = Convert.ToInt32(Dr1[7].ToString());
                                                SaleItem.Weight = Convert.ToDouble(Dr1[8].ToString());
                                                SaleItem.ItemAmtPerUnit = Convert.ToDouble(Dr1[9].ToString());
                                                SaleItem.DiscountAmountPerUnit = Convert.ToDouble(Dr1[10].ToString());
                                                SaleItem.TaxAmountPerUnit = Convert.ToDouble(Dr1[11].ToString());
                                                SaleItem.TotalItemAmt = Convert.ToDouble(Dr1[12].ToString());
                                                SaleItem.ItemDiscAmount = Convert.ToDouble(Dr1[13].ToString());
                                                SaleItem.ItemTaxAmount = Convert.ToDouble(Dr1[14].ToString());
                                                SaleItem.FinalItemAmt = Convert.ToDouble(Dr1[15].ToString());
                                                SaleItem.SaleCreateDate = Convert.ToDateTime(Dr1[16].ToString());
                                                if ((Dr1[17].ToString() != null) && (Dr1[17].ToString() != "")) SaleItem.SaleLastModDate = Convert.ToDateTime(Dr1[17].ToString());

                                                SaleObj.SaleItemsList.Add(SaleItem);
                                            }
                                        }

                                        SaleObjList.Add(SaleObj);
                                    }
                                }
                                # endregion

                            }
                        }

                        SaleObjArray = SaleObjList.ToArray<Sale>();
                        retval = true;
                    }
                }
                catch (Exception ex1)
                {
                    errorMessage = ex1.Message;
                }

                return retval;
            }

            public bool ComputeSaleAmounts(Sale SaleObj, out Sale NewSaleObj, out string errorMessage)
            {
                bool retval = false;

                errorMessage = null;

                try
                {
                    NewSaleObj = new Sale();

                    //Calculate Total Item Discount Amount
                    int i = 0;
                    double _itemDiscAmt = 0;

                    while (i < SaleObj.SaleItemsList.Count)
                    {
                        _itemDiscAmt = _itemDiscAmt + SaleObj.SaleItemsList[i].ItemDiscAmount;
                        i = i + 1;
                    }


                    //Calculate Total Global Tax Amount
                    DiscountFactory DiscountFactory = new DiscountFactory();
                    List<Discount> GlobalDiscList = new List<Discount>();
                    Discount[] GlobalDiscArray;
                    i = 0;
                    double _globalDiscAmt = 0;

                    if (DiscountFactory.GetGlobalDiscountList(out errorMessage, out GlobalDiscArray) == true)
                    {
                        GlobalDiscList = GlobalDiscArray.ToList<Discount>();
                        SaleObj.DiscountList = GlobalDiscList;
                    }

                    while (i < SaleObj.DiscountList.Count)
                    {
                        if (SaleObj.DiscountList[i].DiscountType.DiscountTypeId == 0)
                            _globalDiscAmt = _globalDiscAmt + SaleObj.DiscountList[i].DiscountValue;
                        else if (SaleObj.DiscountList[i].DiscountType.DiscountTypeId == 1)
                            _globalDiscAmt = _globalDiscAmt + (SaleObj.TotalSaleAmount * (SaleObj.DiscountList[i].DiscountValue) / 100);

                        i = i + 1;
                    }

                    SaleObj.TotalDiscountAmount = _itemDiscAmt + _globalDiscAmt; ;


                    //Calculate Total Item Tax Amount
                    i = 0;
                    double _itemTaxAmt = 0;

                    while (i < SaleObj.SaleItemsList.Count)
                    {
                        _itemTaxAmt = _itemTaxAmt + SaleObj.SaleItemsList[i].ItemTaxAmount;
                        i = i + 1;
                    }

                    //Calculate Total Global Tax Amount
                    TaxFactory TaxFactory = new TaxFactory();
                    List<Tax> GlobalTaxList = new List<Tax>();
                    Tax[] GlobalTaxArray;
                    i = 0;
                    double _globalTaxAmt = 0;  

                    if (TaxFactory.GetGlobalTaxesList(out errorMessage, out GlobalTaxArray) == true)
                    {
                        GlobalTaxList = GlobalTaxArray.ToList<Tax>();
                        SaleObj.TaxList = GlobalTaxList;
                    }

                    while (i < SaleObj.TaxList.Count)
                    {
                        if (SaleObj.TaxList[i].TaxType.TaxTypeId == 0)
                            _globalTaxAmt = _globalTaxAmt + SaleObj.TaxList[i].TaxValue;
                        else if (SaleObj.TaxList[i].TaxType.TaxTypeId == 1)
                                _globalTaxAmt = _globalTaxAmt + (SaleObj.TotalSaleAmount * (SaleObj.TaxList[i].TaxValue) / 100);

                        i = i + 1;
                    }

                    SaleObj.TotalTaxAmount = _itemTaxAmt + _globalTaxAmt;

                    //Calculate Total Sale Amount, Final Sale Amount
                    i = 0;
                    double _totalSaleAmt = 0;

                    while (i < SaleObj.SaleItemsList.Count)
                    {
                        _totalSaleAmt = _totalSaleAmt + SaleObj.SaleItemsList[i].TotalItemAmt;
                        i = i + 1;
                    }

                    SaleObj.TotalSaleAmount = _totalSaleAmt;
                    SaleObj.FinalSaleAmount = SaleObj.TotalSaleAmount + SaleObj.TotalTaxAmount -  SaleObj.TotalDiscountAmount;

                    retval = true;
                }
                catch (Exception ex1)
                {
                    errorMessage = ex1.Message;
                }

                NewSaleObj = SaleObj;
                return retval;
            }

        #endregion
    }
}
