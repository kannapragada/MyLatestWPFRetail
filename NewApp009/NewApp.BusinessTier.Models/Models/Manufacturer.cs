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
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Runtime.Serialization;




namespace NewApp.BusinessTier.Models
{
    [DataContract]
    public class Manufacturer
    {
        //Declarations
        # region Declarations

            public Thread FilePickerThread;

            private string          _manufId;
            private string          _manufName;
            private DateTime        _manufDateofBirth;
            private string          _manufPresentAddress;
            private string          _manufPresentCity;
            private string          _manufPresentPinCode;
            private string          _manufPresentPhone;
            private string          _manufMobile;
            private string          _manufEMailId;
            private bool            _manufPresentPermAddressSame;
            private string          _manufPermanentAddress;
            private string          _manufPermanentCity;
            private string          _manufPermanentPinCode;
            private string          _manufPermanentPhone;
            private string          _manufIDProofValue;
            private double          _manufAmtTobePaid;
            private double          _manufAmtPaidYTD;
            private DateTime        _manufRelationshipStartDate;
            private DateTime        _manufRelationshipExpiryDate;
            private DateTime        _manufCreateDate;
            private DateTime        _manuf_LastModdate;
            private byte[]          _manufImg;


            private ManufacturerStatus  _manufStatus;
            private GenderType          _manufGender;
            private IDProofType         _manufIDProofType;
            private List<Sale>          _saleListObj;

        #endregion

        //Constructor
        public Manufacturer()
        {
            _manufStatus = new ManufacturerStatus();
            _manufIDProofType = new IDProofType();
            _manufGender = new GenderType();
            _saleListObj = new List<Sale>();
        }

        //Properties  
        #region MemberProperties

        [DataMember]
        public string ManufacturerId
            {
                get{return _manufId;}
                set{_manufId = value;}
            }

        [DataMember]    
        public string ManufacturerName
            {
                get{return _manufName;}
                set{_manufName = value;}
            }

        [DataMember]    
        public ManufacturerStatus Status
            {
                get { return _manufStatus; }
                set { _manufStatus = value; }
            }

        [DataMember]    
        public DateTime DateofBirth
            {
                get{return _manufDateofBirth;}
                set{_manufDateofBirth = value;}
            }

        [DataMember]    
        public GenderType GenderType
            {
                get{return _manufGender;}
                set{_manufGender = value;}
            }

        [DataMember]    
        public string PresentAddress
            {
                get { return _manufPresentAddress; }
                set { _manufPresentAddress = value; }
            }

        [DataMember]    
        public string PresentCity
            {
                get { return _manufPresentCity; }
                set { _manufPresentCity = value; }
            }

        [DataMember]    
        public string PresentPinCode
            {
                get { return _manufPresentPinCode; }
                set { _manufPresentPinCode = value; }
            }

        [DataMember]    
        public string PresentPhoneNo
            {
                get { return _manufPresentPhone; }
                set { _manufPresentPhone = value; }
            }

        [DataMember]    
        public string Mobile
            {
                get { return _manufMobile; }
                set { _manufMobile = value; }
            }

        [DataMember]    
        public string EMailId
            {
                get { return _manufEMailId; }
                set { _manufEMailId = value; }
            }

        [DataMember]
        public bool IsPresentPermAddressSame
        {
            get { return _manufPresentPermAddressSame; }
            set { _manufPresentPermAddressSame = value; }
        }
        
        [DataMember]    
        public string PermanentAddress
            {
                get { return _manufPermanentAddress; }
                set { _manufPermanentAddress = value; }
            }

        [DataMember]    
        public string PermanentCity
            {
                get { return _manufPermanentCity; }
                set { _manufPermanentCity = value; }
            }

        [DataMember]    
        public string PermanentPinCode
            {
                get { return _manufPermanentPinCode; }
                set { _manufPermanentPinCode = value; }
            }

        [DataMember]    
        public string PermanentPhoneNo
            {
                get { return _manufPermanentPhone; }
                set { _manufPermanentPhone = value; }
            }

        [DataMember]    
        public IDProofType IDProofType
            {
                get{return _manufIDProofType;}
                set{_manufIDProofType = value;}
            }

        [DataMember]    
        public string IDProofValue
            {
                get{return _manufIDProofValue;}
                set{_manufIDProofValue = value;}
            }

        [DataMember]    
        public byte[] Image
            {
                get { return _manufImg; }
                set { _manufImg = value; }
            }

        [DataMember]    
        public DateTime RelationshipStartDate
            {
                get { return _manufRelationshipStartDate; }
                set { _manufRelationshipStartDate = value; }
            }

        [DataMember]    
        public DateTime RelationshipExpiryDate
            {
                get { return _manufRelationshipExpiryDate; }
                set { _manufRelationshipExpiryDate = value; }
            }

        [DataMember]    
        public double AmountTobePaid
            {
                get { return _manufAmtTobePaid; }
                set { _manufAmtTobePaid = value; }
            }

        [DataMember]    
        public double AmountPaidYTD
            {
                get { return _manufAmtPaidYTD; }
                set { _manufAmtPaidYTD = value; }
            }

        [DataMember]    
        public List<Sale> SalesDetails
            {
                get { return _saleListObj; }
                set { _saleListObj = value; }
            }

        [DataMember]    
        public DateTime CreateDate
            {
                get { return _manufCreateDate; }
                set { _manufCreateDate = value; }
            }

        [DataMember]    
        public DateTime LastModDate
            {
                get { return _manuf_LastModdate; }
                set { _manuf_LastModdate = value; }
            }
        #endregion
    }
}



