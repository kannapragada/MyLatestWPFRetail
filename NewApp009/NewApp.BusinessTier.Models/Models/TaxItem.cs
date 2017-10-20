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
    public class TaxItem
    {
        //Declarations
        # region Declarations

            public Thread FilePickerThread;
        
            private string          _TI_itemId;
            private string          _TI_taxCode;
            private string          _TI_batchId;
            private DateTime        _TI_StartDate;
            private DateTime        _TI_endDate;
            private string          _TI_remarks;
            private DateTime        _TI_createDate;
            private DateTime        _TI_lastModDate;

            private Tax             _tax_TaxDtlObj;
            private StoreItem       _tax_StoreItemDtlObj;

        #endregion

        //Constructor
        public TaxItem() 
        {
            _tax_TaxDtlObj = new Tax();
            _tax_StoreItemDtlObj = new StoreItem();
        }

        //Properties  
        #region MemberProperties

        [DataMember]    
        public string ItemId
            {
                get { return _TI_itemId; }
                set { _TI_itemId = value; }
            }

        [DataMember]    
        public string TaxCode
            {
                get { return _TI_taxCode; }
                set { _TI_taxCode = value; }
            }

        [DataMember]    
        public string BatchId
            {
                get { return _TI_batchId; }
                set { _TI_batchId = value; }
            }

        [DataMember]    
        public DateTime StartDate
            {
                get { return _TI_StartDate; }
                set { _TI_StartDate = value; }
            }

        [DataMember]    
        public DateTime EndDate
            {
                get { return _TI_endDate; }
                set { _TI_endDate = value; }
            }

        [DataMember]    
        public string Remarks
            {
                get { return _TI_remarks; }
                set { _TI_remarks = value; }
            }

        [DataMember]    
        public Tax TaxDetail
            {
                get { return _tax_TaxDtlObj; }
                set { _tax_TaxDtlObj = value; }
            }

        [DataMember]    
        public StoreItem StoreItemDetail
            {
                get{return _tax_StoreItemDtlObj;}
                set { _tax_StoreItemDtlObj = value; }
            }

        [DataMember]    
        public DateTime CreateDate
            {
                get { return _TI_createDate; }
                set { _TI_createDate = value; }
            }

        [DataMember]    
        public DateTime LastModDate
            {
                get { return _TI_lastModDate; }
                set { _TI_lastModDate = value; }
            }

        #endregion
    }
}
