using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.ComponentModel;
using System.Runtime.Serialization;



namespace NewApp.BusinessTier.Models
{
    [DataContract]
    public class SaleItem
    {
        //Declarations
        # region Declarations


            private string      _sales_Id;
            private int         _serial_Numb;
            private string      _item_Id;
            private string      _item_Name;
            private string      _Batch_Id;
            private int         _qty;
            private double      _weight;
            private double      _item_Amt_Per_Unit;
            private double      _disc_Amt_Per_Unit;
            private double      _tax_Amt_Per_Unit;
            private double      _disc_Item_Amt;
            private double      _tax_Item_Amt;
            private double      _total_Item_Amt;
            private double      _final_Item_Amt;
            private DateTime    _sale_Create_Date;
            private DateTime    _sale_Last_Mod_Date;

            private string      _mode;

            private Manufacturer _manufacturer;
            private StoreItem _storeItem_Detail;


        #endregion

        //Constructor
        public SaleItem()
        {
            _manufacturer       = new Manufacturer();
            _storeItem_Detail   = new StoreItem();
        }

        //Properties  
        #region MemberProperties

        [DataMember]
        public string Mode
        {
            get{ return _mode; }
            set{_mode = value;}
        }

        [DataMember]
        public string SalesId
        {
            get { return _sales_Id; }
            set { _sales_Id = value; }
        }

        [DataMember]
        public string ItemId
        {
            get { return _item_Id; }
            set { _item_Id = value;}
        }

        [DataMember]
        public string BatchId
        {
            get { return _Batch_Id; }
            set { _Batch_Id = value; }
        }

        [DataMember]
        public string ItemName
        {
            get { return _item_Name; }
            set { _item_Name = value; }
        }

        [DataMember]
        public StoreItem StoreItemDetail
        {
            get {return _storeItem_Detail;}
            set { _storeItem_Detail = value; }
        }

        [DataMember]
        public int SerialNumber
        {
            get { return _serial_Numb; }
            set { _serial_Numb = value; }
        }

        [DataMember]
        public Manufacturer Manufacturer
        {
            get { return _manufacturer; }
            set { _manufacturer = value; }
        }

        [DataMember]
        public int Quantity
        {
            get{return _qty;}
            set {_qty = value;}
        }

        [DataMember]
        public double Weight
        {
            get { return _weight; }
            set { _weight = value; }
        }

        [DataMember]
        public double ItemAmtPerUnit
        {
            get {return _item_Amt_Per_Unit; }
            set { _item_Amt_Per_Unit = value; }
        }

        [DataMember]
        public double DiscountAmountPerUnit
        {
            get { return _disc_Amt_Per_Unit; }
            set { _disc_Amt_Per_Unit = value; }
        }

        [DataMember]
        public double TaxAmountPerUnit
        {
            get { return _tax_Amt_Per_Unit; }
            set { _tax_Amt_Per_Unit = value; }
        }

        [DataMember]
        public double ItemDiscAmount
        {
            set { _disc_Item_Amt = value; }
            get {return _disc_Item_Amt;}
        }

        [DataMember]
        public double ItemTaxAmount
        {
            set { _tax_Item_Amt = value; }
            get {return _tax_Item_Amt;}
        }

        [DataMember]
        public double TotalItemAmt
        {
        get { return _total_Item_Amt; }
        set { _total_Item_Amt = value;}
        }

        [DataMember]
        public double FinalItemAmt
        {
            get { return _final_Item_Amt; }
            set {_final_Item_Amt = value;}
        }

        [DataMember]
        public DateTime SaleCreateDate
        {
        get { return _sale_Create_Date; }
        set { _sale_Create_Date = value; }
        }

        [DataMember]
        public DateTime SaleLastModDate
        {
        get { return _sale_Last_Mod_Date; }
        set { _sale_Last_Mod_Date = value; }
        }

        #endregion
    }
}
