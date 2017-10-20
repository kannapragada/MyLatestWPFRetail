using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Xml;
using System.Runtime.Serialization;



namespace NewApp.BusinessTier.Models
{
    [DataContract]
    public class StoreItem
    {
        //Declarations
        # region Declarations
            private string      _item_Id;
            private string      _item_Name;
            private string      _item_Description;
            private string      _bar_Code;
            private string      _batch_Id;
            private double      _MRP;
            private double      _weight_Procured;
            private double      _price_Procured;
            private DateTime    _date_Procured;
            private double      _disc_Amt_Per_Unit;
            private double      _tax_Amt_Per_Unit;
            private double      _price_Sell;
            private int         _qty_Available;
            private int         _qty_Procured;
            private double      _weight_Available;
            private DateTime    _date_Of_Manuf;
            private DateTime    _date_Of_Exp;
            private DateTime    _item_Create_Date;
            private DateTime    _item_Last_Mod_Date;
            private DateTime    _batch_Create_Date;
            private DateTime    _batch_Last_Mod_Date;
            private string      _old_batch_Id;

            private Manufacturer        _manufInfo;
            private Vendor              _vendorInfo;
            private List<DiscountItem>  _discItemList;
            private List<TaxItem>       _taxItemList;

        #endregion

        //Constructor
        public StoreItem()
        {
            _discItemList = new List<DiscountItem>();
            _taxItemList = new List<TaxItem>();
            _manufInfo = new Manufacturer();
            _vendorInfo = new Vendor();
        }

        //Properties  
        #region MemberProperties

        [DataMember]
        public string ItemId
        {
            get{return _item_Id;}
            set{_item_Id = value;}
        }

        [DataMember]    
        public string ItemName
        {
            get{return _item_Name;}
	        set{_item_Name = value;}
        }

        [DataMember]    
        public string ItemDescription
        {
            get { return _item_Description; }
            set { _item_Description = value; }
        }

        [DataMember]    
        public string BatchId
        {
            get { return _batch_Id; }
            set { _batch_Id = value; }
        }

        [DataMember]    
        public Manufacturer ManufacturerInfo
        {
            get { return _manufInfo; }
            set { _manufInfo = value; }
        }

        [DataMember]    
        public Vendor VendorInfo
        {
            get { return _vendorInfo; }
            set { _vendorInfo = value; }
        }

        [DataMember]    
        public string OldBatchId
        {
            get { return _old_batch_Id; }
            set { _old_batch_Id = value; }
        }

        [DataMember]    
        public string BarCode
        {
            get { return _bar_Code; }
            set { _bar_Code = value; }
        }

        [DataMember]    
        public double MRP
        {
            get { return _MRP; }
            set { _MRP = value; }
        }

        [DataMember]    
        public double ProcuredPricePerUnit
        {
            get { return _price_Procured; }
            set { _price_Procured = value; }
        }

        [DataMember]    
        public double WeightProcured
        {
            get { return _weight_Procured; }
            set { _weight_Procured = value; }
        }

        [DataMember]    
        public int QtyProcured
        {
            get { return _qty_Procured; }
            set { _qty_Procured = value; }
        }

        [DataMember]    
        public DateTime DateProcured
        {
            get { return _date_Procured; }
            set { _date_Procured = value; }
        }

        [DataMember]    
        public Double DiscountAmountPerUnit
        {
            get {return _disc_Amt_Per_Unit;}
            set { _disc_Amt_Per_Unit = value; }
        }

        [DataMember]    
        public List<DiscountItem> DiscountList
        {
            get {return _discItemList; }
            set {_discItemList = value;}
        }

        [DataMember]    
        public double SellingPricePerUnit
        {
            get { return _price_Sell; }
            set { _price_Sell = value; }
        }

        [DataMember]    
        public int QtyAvailable
        {
            get { return _qty_Available; }
            set { _qty_Available = value; }
        }

        [DataMember]    
        public double WeightAvailable
        {
            get { return _weight_Available; }
            set { _weight_Available = value; }
        }

        [DataMember]    
        public DateTime DateOfManuf
        {
            get { return _date_Of_Manuf; }
            set { _date_Of_Manuf = value; }
        }

        [DataMember]    
        public DateTime DateOfExp
        {
            get { return _date_Of_Exp; }
            set { _date_Of_Exp = value; }
        }

        [DataMember]    
        public DateTime ItemCreateDate
        {
            get { return _item_Create_Date; }
            set { _item_Create_Date = value; }
        }

        [DataMember]    
        public DateTime ItemLastModDate
        {
            get { return _item_Last_Mod_Date; }
            set { _item_Last_Mod_Date = value; }
        }

        [DataMember]
        public Double TaxAmountPerUnit
        {
            get{return _tax_Amt_Per_Unit;}
            set { _tax_Amt_Per_Unit = value; }
        }

        [DataMember]    
        public List<TaxItem> TaxList
        {
            get { return _taxItemList; }
            set { _taxItemList = value; }
        }

        [DataMember]    
        public DateTime BatchCreateDate
        {
            get { return _batch_Create_Date; }
            set { _batch_Create_Date = value; }
        }

        [DataMember]    
        public DateTime BatchLastModDate
        {
            get { return _batch_Last_Mod_Date; }
            set { _batch_Last_Mod_Date = value; }
        }

        #endregion
    }
}
