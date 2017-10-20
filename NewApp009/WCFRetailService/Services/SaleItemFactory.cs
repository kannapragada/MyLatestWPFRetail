using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.ComponentModel;
using System.Data.SqlClient;
using NewApp.BusinessTier.Models;
using WCFRetailService;


namespace WCFRetailService
{
    public class SaleItemFactory : ISaleItemFactory
    {
        //Declarations
        # region Declarations

            SQLDataAccess DBObj;

        #endregion

        //Constructor
        public SaleItemFactory()
        {
        }

        //Member Methods 
        #region MemberMethods

        public bool GetSaleItems(string SaleId, out SaleItem[] SaleItemObjArray, out string errorMessage)
        {
            DataSet Ds;
            DataRow Dr;
            SaleItem SaleItemObj;

            bool retval = false;

            errorMessage = null;

            List<SaleItem> SaleItemObjList = new List<SaleItem>();
            SaleItemObjArray = null;

            try
            {
                DBObj = SQLDataAccess.GetDBObj(out errorMessage);

                if (errorMessage != null)
                    return retval;


                if (DBObj.ExecuteSPForSearch(out Ds, out errorMessage, "NewAppDb..GetSaleItems", SaleId) == false)
                {
                    if (errorMessage != null)
                        return retval;
                }
                else
                {
                    if (Ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < Ds.Tables[0].Rows.Count; i++)
                        {
                            SaleItemObj = new SaleItem();

                            Dr = Ds.Tables[0].Rows[i];

                            SaleItemObj.SalesId = Dr[0].ToString();
                            SaleItemObj.SerialNumber = Convert.ToInt32(Dr[1].ToString());
                            SaleItemObj.ItemId = Dr[2].ToString();
                            SaleItemObj.BatchId = Dr[3].ToString();
                            SaleItemObj.StoreItemDetail.ManufacturerInfo.ManufacturerId = Dr[4].ToString();
                            ManufacturerFactory ManufFactory = new ManufacturerFactory();
                            Manufacturer ManufObj = new Manufacturer();
                            if (ManufFactory.GetManufacturerBasicDetails(Convert.ToString(Dr[4].ToString()), out errorMessage, out ManufObj) == true)
                                SaleItemObj.StoreItemDetail.ManufacturerInfo = ManufObj;
                            else
                                return retval;
                            VendorFactory VendorFactory = new VendorFactory();
                            Vendor VendorObj = new Vendor();
                            if (VendorFactory.GetVendorBasicDetails(Convert.ToString(Dr[5].ToString()), out errorMessage, out VendorObj) == true)
                                SaleItemObj.StoreItemDetail.VendorInfo = VendorObj;
                            else
                                return retval;
                            SaleItemObj.Quantity = Convert.ToInt32(Dr[6].ToString());
                            SaleItemObj.Weight = Convert.ToDouble(Dr[7].ToString());
                            SaleItemObj.ItemAmtPerUnit = Convert.ToDouble(Dr[8].ToString());
                            SaleItemObj.DiscountAmountPerUnit = Convert.ToDouble(Dr[9].ToString());
                            SaleItemObj.TotalItemAmt = Convert.ToDouble(Dr[10].ToString());
                            SaleItemObj.SaleCreateDate = Convert.ToDateTime(Dr[11].ToString());
                            SaleItemObj.SaleLastModDate = Convert.ToDateTime(Dr[12].ToString());

                            SaleItemObjList.Add(SaleItemObj);
                        }

                        SaleItemObjArray = SaleItemObjList.ToArray<SaleItem>();
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

        public bool UpdateQuantity(string SaleId, int SerialNumb, string ItemId, string BatchId, int Quantity, char Operation, out string errorMessage)
        {
            bool retval = false;
            errorMessage = null;

            try
            {
                SqlParameter[] SQLParamArray = new SqlParameter[6];

                SqlParameter parameter = new SqlParameter("@Sale_Id", SqlDbType.VarChar);
                parameter.Value = (object)SaleId ?? DBNull.Value;
                SQLParamArray[0] = parameter;

                parameter = new SqlParameter("@Serial_Numb", SqlDbType.Int);
                parameter.Value = (object)SerialNumb ?? DBNull.Value;
                SQLParamArray[1] = parameter;

                parameter = new SqlParameter("@IB_Item_Id", SqlDbType.VarChar);
                parameter.Value = (object)ItemId ?? DBNull.Value;
                SQLParamArray[2] = parameter;

                parameter = new SqlParameter("@IB_Batch_Id", SqlDbType.VarChar);
                parameter.Value = (object)BatchId ?? DBNull.Value;
                SQLParamArray[3] = parameter;

                parameter = new SqlParameter("@Quantity", SqlDbType.Int);
                parameter.Value = Quantity;
                SQLParamArray[4] = parameter;

                parameter = new SqlParameter("@Operation", SqlDbType.VarChar);
                parameter.Value = Operation;
                SQLParamArray[5] = parameter;

                DataSet ds = new DataSet();

                DBObj = SQLDataAccess.GetDBObj(out errorMessage);

                if (errorMessage != null)
                    return retval;

                if (DBObj.ExecuteSPWithParamsNoResp(out errorMessage, "NewAppDb..UpdateItemQty", SQLParamArray) == false)
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

        public bool ComputeSaleItemAmounts(SaleItem SaleItemObj, out SaleItem NewSaleItemObj, out string errorMessage)
        {
            bool retval = false;

            errorMessage = null;

            try
            {
                SaleItemObj.ItemAmtPerUnit = SaleItemObj.StoreItemDetail.SellingPricePerUnit;
                SaleItemObj.TotalItemAmt = SaleItemObj.StoreItemDetail.SellingPricePerUnit * SaleItemObj.Quantity;
                SaleItemObj.DiscountAmountPerUnit = SaleItemObj.StoreItemDetail.DiscountAmountPerUnit;
                SaleItemObj.TaxAmountPerUnit = SaleItemObj.StoreItemDetail.TaxAmountPerUnit;
                SaleItemObj.ItemDiscAmount = SaleItemObj.DiscountAmountPerUnit * SaleItemObj.Quantity;
                SaleItemObj.ItemTaxAmount = SaleItemObj.TaxAmountPerUnit * SaleItemObj.Quantity;
                SaleItemObj.FinalItemAmt = SaleItemObj.TotalItemAmt + SaleItemObj.TaxAmountPerUnit - SaleItemObj.ItemDiscAmount;

                retval = true;
            }
            catch (Exception ex1)
            {
                errorMessage = ex1.Message;
            }

            NewSaleItemObj = SaleItemObj;
            return retval;
        }

        #endregion
    }
}
