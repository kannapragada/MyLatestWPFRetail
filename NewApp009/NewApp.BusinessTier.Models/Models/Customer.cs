using System;
using System.Collections.Generic;
using System.Collections;
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
    public class Customer
    {
        //Declarations
        # region Declarations

            public Thread FilePickerThread;

            private string          _custId;
            private string          _custName;
            private DateTime        _custDateofBirth;
            private string          _custPresentAddress;
            private string          _custPresentCity;
            private string          _custPresentPinCode;
            private string          _custPresentPhone;
            private string          _custMobile;
            private string          _custEMailId;
            private bool            _custPresentPermAddressSame;
            private string          _custPermanentAddress;
            private string          _custPermanentCity;
            private string          _custPermanentPinCode;
            private string          _custPermanentPhone;
            private string          _custIDProofValue;
            private double          _custAmtTobePaid;
            private double          _custAmtPaidYTD;
            private DateTime        _custRelationshipStartDate;
            private DateTime        _custRelationshipExpiryDate;
            private DateTime        _custCreateDate;
            private DateTime        _cust_LastModdate;
            private byte[]          _custImg;
            private string          _custGuestName;


        private IDProofType     _custIDProofType;
        private GenderType      _custGender;
        private CustomerStatus  _customerStatus;
        private List<Sale>      _saleListObj;


        #endregion

        //Constructor
        public Customer()
        {
            _customerStatus = new CustomerStatus();
            _custIDProofType = new IDProofType();
            _custGender = new GenderType();
            _saleListObj = new List<Sale>();
        }

    #region MemberProperties
        [DataMember]
        public string CustomerId
            {
                get{return _custId;}
                set{_custId = value;}
            }

        [DataMember]
        public string CustomerName
            {
                get{return _custName;}
                set{_custName = value;}
            }

        [DataMember]
        public CustomerStatus Status
            {
                get { return _customerStatus; }
                set { _customerStatus = value; }
            }

        [DataMember]        
        public DateTime DateofBirth
            {
                get{return _custDateofBirth;}
                set{_custDateofBirth = value;}
            }

        [DataMember]
        public GenderType GenderType
            {
                get{return _custGender;}
                set{_custGender = value;}
            }

        [DataMember]    
        public string PresentAddress
            {
                get { return _custPresentAddress; }
                set { _custPresentAddress = value; }
            }

        [DataMember]        
        public string PresentCity
            {
                get { return _custPresentCity; }
                set { _custPresentCity = value; }
            }

        [DataMember]    
        public string PresentPinCode
            {
                get { return _custPresentPinCode; }
                set { _custPresentPinCode = value; }
            }

        [DataMember]    
        public string PresentPhoneNo
            {
                get { return _custPresentPhone; }
                set { _custPresentPhone = value; }
            }

        [DataMember]    
        public string Mobile
            {
                get { return _custMobile; }
                set { _custMobile = value; }
            }

        [DataMember]    
        public string EMailId
            {
                get { return _custEMailId; }
                set { _custEMailId = value; }
            }

        [DataMember]
        public bool IsPresentPermAddressSame
            {
                get { return _custPresentPermAddressSame; }
                set { _custPresentPermAddressSame = value; }
            }

        [DataMember]    
        public string PermanentAddress
            {
                get { return _custPermanentAddress; }
                set { _custPermanentAddress = value; }
            }

        [DataMember]    
        public string PermanentCity
            {
                get { return _custPermanentCity; }
                set { _custPermanentCity = value; }
            }

        [DataMember]    
        public string PermanentPinCode
            {
                get { return _custPermanentPinCode; }
                set { _custPermanentPinCode = value; }
            }

        [DataMember]    
        public string PermanentPhoneNo
            {
                get { return _custPermanentPhone; }
                set { _custPermanentPhone = value; }
            }

        [DataMember]    
        public IDProofType IDProofType
            {
                get{return _custIDProofType;}
                set{_custIDProofType = value;}
            }

        [DataMember]    
        public string IDProofValue
            {
                get{return _custIDProofValue;}
                set{_custIDProofValue = value;}
            }

        [DataMember]    
        public byte[] Image
            {
                get { return _custImg; }
                set { _custImg = value; }
            }

        [DataMember]    
        public DateTime RelationshipStartDate
            {
                get { return _custRelationshipStartDate; }
                set { _custRelationshipStartDate = value; }
            }

        [DataMember]    
        public DateTime RelationshipExpiryDate
            {
                get { return _custRelationshipExpiryDate; }
                set { _custRelationshipExpiryDate = value; }
            }

        [DataMember]    
        public double AmountTobePaid
            {
                get { return _custAmtTobePaid; }
                set { _custAmtTobePaid = value; }
            }

        [DataMember]    
        public double AmountPaidYTD
            {
                get { return _custAmtPaidYTD; }
                set { _custAmtPaidYTD = value; }
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
                get { return _custCreateDate; }
                set { _custCreateDate = value; }
            }

        [DataMember]    
        public DateTime LastModDate
            {
                get { return _cust_LastModdate; }
                set { _cust_LastModdate = value; }
            }

        [DataMember]    
        public string GuestName
            {
                get { return _custGuestName; }
                set { _custGuestName = value; }
            }

        #endregion
    }
}



