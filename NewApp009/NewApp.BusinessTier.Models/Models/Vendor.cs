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
    public class Vendor
    {
        //Declarations
        # region Declarations

            public Thread FilePickerThread;

            private string          _vendorId;
            private string          _vendorName;
            private DateTime        _vendorDateofBirth;
            private string          _vendorPresentAddress;
            private string          _vendorPresentCity;
            private string          _vendorPresentPinCode;
            private string          _vendorPresentPhone;
            private string          _vendorMobile;
            private string          _vendorEMailId;
            private bool            _vendorPresentPermAddressSame;
            private string          _vendorPermanentAddress;
            private string          _vendorPermanentCity;
            private string          _vendorPermanentPinCode;
            private string          _vendorPermanentPhone;
            private string          _vendorIDProofValue;
            private double          _vendorAmtTobePaid;
            private double          _vendorAmtPaidYTD;
            private DateTime        _vendorRelationshipStartDate;
            private DateTime        _vendorRelationshipExpiryDate;
            private DateTime        _vendorCreateDate;
            private DateTime        _vendor_LastModdate;
            private byte[]          _vendorImg;


            private VendorStatus    _vendorStatus;
            private GenderType      _vendorGender;
            private IDProofType     _vendorIDProofType;
            private List<Sale>      _saleListObj;

        #endregion

        //Constructor
        public Vendor()
        {
            _vendorStatus = new VendorStatus();
            _vendorIDProofType = new IDProofType();
            _vendorGender = new GenderType();
            _saleListObj = new List<Sale>();
        }

        #region MemberProperties

        [DataMember]
        public string VendorId
            {
                get{return _vendorId;}
                set{_vendorId = value;}
            }

        [DataMember]    
        public string VendorName
            {
                get{return _vendorName;}
                set{_vendorName = value;}
            }

        [DataMember]    
        public VendorStatus Status
            {
                get { return _vendorStatus; }
                set { _vendorStatus = value; }
            }

        [DataMember]    
        public DateTime DateofBirth
            {
                get{return _vendorDateofBirth;}
                set{_vendorDateofBirth = value;}
            }

        [DataMember]    
        public GenderType GenderType
            {
                get{return _vendorGender;}
                set{_vendorGender = value;}
            }

        [DataMember]    
        public string PresentAddress
            {
                get { return _vendorPresentAddress; }
                set { _vendorPresentAddress = value; }
            }

        [DataMember]    
        public string PresentCity
            {
                get { return _vendorPresentCity; }
                set { _vendorPresentCity = value; }
            }

        [DataMember]    
        public string PresentPinCode
            {
                get { return _vendorPresentPinCode; }
                set { _vendorPresentPinCode = value; }
            }

        [DataMember]    
        public string PresentPhoneNo
            {
                get { return _vendorPresentPhone; }
                set { _vendorPresentPhone = value; }
            }

        [DataMember]    
        public string Mobile
            {
                get { return _vendorMobile; }
                set { _vendorMobile = value; }
            }

        [DataMember]    
        public string EMailId
            {
                get { return _vendorEMailId; }
                set { _vendorEMailId = value; }
            }

        [DataMember]    
        public string PermanentAddress
            {
                get { return _vendorPermanentAddress; }
                set { _vendorPermanentAddress = value; }
            }

        [DataMember]    
        public string PermanentCity
            {
                get { return _vendorPermanentCity; }
                set { _vendorPermanentCity = value; }
            }

        [DataMember]    
        public string PermanentPinCode
            {
                get { return _vendorPermanentPinCode; }
                set { _vendorPermanentPinCode = value; }
            }

        [DataMember]
        public bool IsPresentPermAddressSame
        {
            get { return _vendorPresentPermAddressSame; }
            set { _vendorPresentPermAddressSame = value; }
        }

        [DataMember]    
        public string PermanentPhoneNo
            {
                get { return _vendorPermanentPhone; }
                set { _vendorPermanentPhone = value; }
            }

        [DataMember]    
        public IDProofType IDProofType
            {
                get{return _vendorIDProofType;}
                set{_vendorIDProofType = value;}
            }

        [DataMember]    
        public string IDProofValue
            {
                get{return _vendorIDProofValue;}
                set{_vendorIDProofValue = value;}
            }

        [DataMember]    
        public byte[] Image
            {
                get { return _vendorImg; }
                set { _vendorImg = value; }
            }

        [DataMember]    
        public DateTime RelationshipStartDate
            {
                get { return _vendorRelationshipStartDate; }
                set { _vendorRelationshipStartDate = value; }
            }

        [DataMember]    
        public DateTime RelationshipExpiryDate
            {
                get { return _vendorRelationshipExpiryDate; }
                set { _vendorRelationshipExpiryDate = value; }
            }

        [DataMember]    
        public double AmountTobePaid
            {
                get { return _vendorAmtTobePaid; }
                set { _vendorAmtTobePaid = value; }
            }

        [DataMember]    
        public double AmountPaidYTD
            {
                get { return _vendorAmtPaidYTD; }
                set { _vendorAmtPaidYTD = value; }
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
                get { return _vendorCreateDate; }
                set { _vendorCreateDate = value; }
            }

        [DataMember]    
        public DateTime LastModDate
            {
                get { return _vendor_LastModdate; }
                set { _vendor_LastModdate = value; }
            }
        #endregion
    }
}



