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
using System.Runtime.Serialization;



namespace NewApp.BusinessTier.Models
{
    [DataContract]
    public class Tax
    {
        //Declarations
        # region Declarations

            public Thread FilePickerThread;

            private string          _tax_Code;
            private string          _tax_Name;
            private string          _tax_Description;
            private double          _tax_Value;
            private DateTime        _tax_StartDate;
            private DateTime        _tax_EndDate;
            private DateTime        _tax_CreateDate;
            private DateTime        _tax_LastModDate;

            private TaxKind         _tax_Kind;
            private TaxType         _tax_Type;
            private List<TaxItem>  _taxItemList;

        #endregion

        //Constructor
        public Tax()
        {
            _taxItemList    = new List<TaxItem>();
            _tax_Kind       = new TaxKind();
            _tax_Type       = new TaxType();
        }

        //Properties  
        #region MemberProperties

        [DataMember]    
        public string TaxCode
            {
                get{return _tax_Code;}
                set{_tax_Code = value;}
            }

        [DataMember]    
        public string TaxName
            {
                get{return _tax_Name;}
                set{_tax_Name = value;}
            }

        [DataMember]    
        public string TaxDescription
            {
                get{return _tax_Description;}
                set{_tax_Description = value;}
            }

        [DataMember]    
        public TaxKind TaxKind
            {
                get{return _tax_Kind;}
                set{_tax_Kind = value;}
            }

        [DataMember]    
        public TaxType TaxType
            {
                get{return _tax_Type;}
                set{_tax_Type = value;}
            }

        [DataMember]    
        public double TaxValue
            {
                get{return _tax_Value;}
                set{_tax_Value = value;}
            }

        [DataMember]    
        public DateTime StartDate
            {
                get { return _tax_StartDate; }
                set { _tax_StartDate = value; }
            }

        [DataMember]    
        public DateTime EndDate
            {
                get { return _tax_EndDate; }
                set { _tax_EndDate = value; }
            }

        [DataMember]    
        public DateTime CreateDate
            {
                get { return _tax_CreateDate; }
                set { _tax_CreateDate = value; }
            }

        [DataMember]    
        public DateTime LastModDate
            {
                get { return _tax_LastModDate; }
                set { _tax_LastModDate = value; }
            }

        [DataMember]    
        public List<TaxItem> TaxItemList
            {
                get{return _taxItemList;}
                set{_taxItemList = value;}
            }

        #endregion
    }
}
