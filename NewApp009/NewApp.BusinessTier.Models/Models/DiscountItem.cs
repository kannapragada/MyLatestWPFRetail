using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.ComponentModel;
using System.Runtime.Serialization;



namespace NewApp.BusinessTier.Models
{
    [DataContract]
    public class DiscountItem
    {
        //Declarations
        # region Declarations

            public Thread FilePickerThread;

            private string          _disc_ItemId;
            private string          _disc_ItemBatchId;
            private string          _disc_DiscCode;
            private DateTime        _disc_ItemStartDate;
            private DateTime        _disc_ItemEndDate;
            private string          _disc_Remarks;
            private DateTime        _disc_CreateDate;
            private DateTime        _disc_LastModDate;

            private Discount _discDtlObj;
            private StoreItem _disc_StoreItemDtlObj;

        #endregion

        //Constructor
        public DiscountItem()
        {
            _discDtlObj = new Discount();
            _disc_StoreItemDtlObj = new StoreItem();
        }

        //Properties  
        #region MemberProperties

        [DataMember]    
        public string DiscItemId
        {
            get{return _disc_ItemId;}
            set{_disc_ItemId = value;}
        }

        [DataMember]    
        public string DiscItemBatchId
        {
            get{return _disc_ItemBatchId;}
            set{_disc_ItemBatchId = value;}
        }

        [DataMember]    
        public string DiscountCode
        {
            get{return _disc_DiscCode;}
            set{_disc_DiscCode = value;}
        }

        [DataMember]    
        public DateTime DiscItemStartDate
        {
            get { return _disc_ItemStartDate; }
            set { _disc_ItemStartDate = value; }
        }

        [DataMember]    
        public DateTime DiscItemEndDate
        {
            get { return _disc_ItemEndDate; }
            set { _disc_ItemEndDate = value; }
        }

        [DataMember]    
        public string DiscRemarks
        {
            get{return _disc_Remarks;}
            set{_disc_Remarks = value;}
        }

        [DataMember]    
        public Discount DiscountDetail
        {
            get {return _discDtlObj;}
            set { _discDtlObj = value; }
        }

        [DataMember]    
        public StoreItem StoreItemDetail
        {
            get {return _disc_StoreItemDtlObj; }
            set { _disc_StoreItemDtlObj = value; }
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

        #endregion
    }
}
