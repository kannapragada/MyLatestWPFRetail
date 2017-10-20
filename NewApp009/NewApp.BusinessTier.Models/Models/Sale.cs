using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Xml;
using System.Data;
using System.ComponentModel;
using System.Runtime.Serialization;


namespace NewApp.BusinessTier.Models
{
    [DataContract]
    public class Sale
    {
        //Declarations
        # region Declarations

            

            private string          _sales_Id;    
            private int             _sale_Status;
            private DateTime        _saleDate;
            private double          _totalTaxAmt;
            private double          _totalDiscAmt;
            private double          _totalSaleAmt;
            private double          _finalSaleAmt;
            private double          _paidAmt;
            private double          _balanceAmt;
            private double          _returnedChangeAmt;
            private DateTime        _lastModDate;

            private string          _mode;

            private List<SaleItem>  _saleItemListObj;
            private Customer        _customerInfo;
            private User            _userInfo;
            private List<Discount>  _DiscList;
            private List<Tax>       _taxList;

        #endregion

        //Constructor
        public Sale()
        {
            _customerInfo       = new Customer();
            _saleItemListObj    = new List<SaleItem>();
            _DiscList           = new List<Discount>();
            _taxList            = new List<Tax>();
            _userInfo           = new User();
        }

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
            get{return _sales_Id;}
            set{_sales_Id = value;}
        }

        [DataMember]
        public Customer CustomerInfo
            {
                get{return _customerInfo;}
                set{_customerInfo = value;}
            }

        [DataMember]
        public User UserInfo
        {
            get { return _userInfo; }
            set { _userInfo = value; }
        }

        [DataMember]
        public List<SaleItem> SaleItemsList
        {
            get { return _saleItemListObj; }
            set { _saleItemListObj = value; }
        }

        [DataMember]
        public List<Discount> DiscountList
        {
            get { return _DiscList; }
            set { _DiscList = value; }
        }

        
        [DataMember]
        public List<Tax> TaxList
        {
            get {return _taxList;}
            set { _taxList = value; }
        }

        [DataMember]
        public DateTime SaleDate
        {
            get{return _saleDate;}
            set{_saleDate = value;}
        }

        [DataMember]
        public double FinalSaleAmount
        {
            get{return _finalSaleAmt;}
            set{_finalSaleAmt = value;}
        }

        [DataMember]
        public double TotalTaxAmount
        {
            get{return _totalTaxAmt;}
            set{_totalTaxAmt = value;}
        }

        [DataMember]
        public double TotalDiscountAmount
        {
            get{return _totalDiscAmt;}
            set{_totalDiscAmt = value;}
        }

        [DataMember]
        public double TotalSaleAmount
        {
            get{return _totalSaleAmt;}
            set{_totalSaleAmt = value;}
        }

        [DataMember]
        public double PaidAmount
        {
            get{return _paidAmt;}
            set{_paidAmt = value;}
        }

        [DataMember]
        public double BalanceAmount
        {
            get{ return _balanceAmt;}
            set{_balanceAmt = value; }
        }

        [DataMember]
        public double ReturnedChangeAmount
        {
            get{return _returnedChangeAmt;}
            set{_returnedChangeAmt = value;}
        }

        [DataMember]
        public int SaleStatus
        {
            get{return _sale_Status;}
            set{_sale_Status = value;}
        }

        [DataMember]
        public DateTime LastModificationDate
        {
            get { return _lastModDate; }
            set { _lastModDate = value; }
        }


        #endregion
    }
}
