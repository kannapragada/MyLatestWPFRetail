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
    public class Discount
    {
        //Declarations
        # region Declarations

            public Thread FilePickerThread;

            private string          _disc_Code;
            private string          _disc_Name;
            private string          _disc_Desc;
            private double          _disc_Value;
            private string          _disc_Remarks;
            private DateTime        _disc_StartDate;
            private DateTime        _disc_EndDate;
            private DateTime        _disc_CreateDate;
            private DateTime        _disc_LastModDate;

            DiscountKind                _disc_Kind;    
            DiscountType                _disc_Type;
            private List<DiscountItem>  _discItemList;

        #endregion

        //Constructor
        public Discount()
        {
            _disc_Kind = new DiscountKind();
            _disc_Type = new DiscountType();
            _discItemList = new List<DiscountItem>();
        }

        //Properties  
        #region MemberProperties

        [DataMember]    
        public string DiscountCode
            {
                get{return _disc_Code;}
                set{_disc_Code = value;}
            }

        [DataMember]    
        public string DiscountName
            {
                get{return _disc_Name;}
                set{_disc_Name = value;}
            }

        [DataMember]    
        public string DiscountDescription
            {
                get{return _disc_Desc;}
                set{_disc_Desc = value;}
            }

        [DataMember]
        public DiscountKind DiscountKind
        {
            get { return _disc_Kind; }
            set { _disc_Kind = value; }
        }

        [DataMember]    
        public DiscountType DiscountType
            {
                get{return _disc_Type;}
                set{_disc_Type = value;}
            }

        [DataMember]    
        public double DiscountValue
            {
                get{return _disc_Value;}
                set{_disc_Value = value;}
            }

        [DataMember]    
        public string DiscountRemarks
            {
                get{return _disc_Remarks;}
                set{_disc_Remarks = value;}
            }

        [DataMember]    
        public DateTime DiscStartDate
            {
                get { return _disc_StartDate; }
                set { _disc_StartDate = value; }
            }

        [DataMember]    
        public DateTime DiscEndDate
            {
                get { return _disc_EndDate; }
                set { _disc_EndDate = value; }
            }

        [DataMember]    
        public DateTime DiscCreateDate
            {
                get { return _disc_CreateDate; }
                set { _disc_CreateDate = value; }
            }

        [DataMember]    
        public DateTime DiscLastModDate
            {
                get { return _disc_LastModDate; }
                set { _disc_LastModDate = value; }
            }

        [DataMember]    
        public List<DiscountItem> DiscountItemsList
            {
                get{return _discItemList;}
                set{_discItemList = value;}
            }

        #endregion
    }
}
