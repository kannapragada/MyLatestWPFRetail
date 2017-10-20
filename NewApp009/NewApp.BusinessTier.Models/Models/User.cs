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
    public class User
    {
        //Declarations
        # region Declarations

            public Thread FilePickerThread;

            private string          _userId;
            private string          _userName;
            private DateTime        _userDateofBirth;
            private string          _userPresentAddress;
            private string          _userPresentCity;
            private string          _userPresentPinCode;
            private string          _userPresentPhone;
            private string          _userMobile;
            private string          _userEMailId;
            private bool            _userPresentPermAddressSame;
            private string          _userPermanentAddress;
            private string          _userPermanentCity;
            private string          _userPermanentPinCode;
            private string          _userPermanentPhone;
            private string          _userIDProofValue;
            private double          _userAmtTobePaid;
            private double          _userAmtPaidYTD;
            private DateTime        _userRelationshipStartDate;
            private DateTime        _userRelationshipExpiryDate;
            private DateTime        _userCreateDate;
            private DateTime        _user_LastModdate;
            private byte[]          _userImg;
            private int             _userThemeId;
            private string          _userPwd;
            private int             _userSecretQueryId;
            private string          _userSecretAnswer;


            private UserStatus      _userStatus;
            private GenderType      _userGender;
            private IDProofType     _userIDProofType;
            private List<Sale>      _saleListObj;

        #endregion

        //Constructor
        public User()
        {
            _userStatus = new UserStatus();
            _userIDProofType = new IDProofType();
            _userGender = new GenderType();
            _saleListObj = new List<Sale>();
        }

        //Properties  
        #region MemberProperties
        [DataMember]    
        public string UserId
            {
                get{return _userId;}
                set{_userId = value;}
            }

        [DataMember]    
        public string UserName
            {
                get{return _userName;}
                set{_userName = value;}
            }

        [DataMember]    
        public UserStatus Status
            {
                get { return _userStatus; }
                set { _userStatus = value; }
            }

        [DataMember]    
        public DateTime DateofBirth
            {
                get{return _userDateofBirth;}
                set{_userDateofBirth = value;}
            }

        [DataMember]    
        public GenderType GenderType
            {
                get{return _userGender;}
                set{_userGender = value;}
            }

        [DataMember]    
        public string PresentAddress
            {
                get { return _userPresentAddress; }
                set { _userPresentAddress = value; }
            }

        [DataMember]    
        public string PresentCity
            {
                get { return _userPresentCity; }
                set { _userPresentCity = value; }
            }

        [DataMember]    
        public string PresentPinCode
            {
                get { return _userPresentPinCode; }
                set { _userPresentPinCode = value; }
            }

        [DataMember]    
        public string PresentPhoneNo
            {
                get { return _userPresentPhone; }
                set { _userPresentPhone = value; }
            }

        [DataMember]    
        public string Mobile
            {
                get { return _userMobile; }
                set { _userMobile = value; }
            }

        [DataMember]    
        public string EMailId
            {
                get { return _userEMailId; }
                set { _userEMailId = value; }
            }

        [DataMember]
        public bool IsPresentPermAddressSame
        {
            get { return _userPresentPermAddressSame; }
            set { _userPresentPermAddressSame = value; }
        }

        [DataMember]    
        public string PermanentAddress
            {
                get { return _userPermanentAddress; }
                set { _userPermanentAddress = value; }
            }

        [DataMember]    
        public string PermanentCity
            {
                get { return _userPermanentCity; }
                set { _userPermanentCity = value; }
            }

        [DataMember]    
        public string PermanentPinCode
            {
                get { return _userPermanentPinCode; }
                set { _userPermanentPinCode = value; }
            }

        [DataMember]    
        public string PermanentPhoneNo
            {
                get { return _userPermanentPhone; }
                set { _userPermanentPhone = value; }
            }

        [DataMember]    
        public IDProofType IDProofType
            {
                get{return _userIDProofType;}
                set{_userIDProofType = value;}
            }

        [DataMember]    
        public string IDProofValue
            {
                get{return _userIDProofValue;}
                set{_userIDProofValue = value;}
            }

        [DataMember]    
        public byte[] Image
            {
                get { return _userImg; }
                set { _userImg = value; }
            }

        [DataMember]    
        public DateTime RelationshipStartDate
            {
                get { return _userRelationshipStartDate; }
                set { _userRelationshipStartDate = value; }
            }

        [DataMember]    
        public DateTime RelationshipExpiryDate
            {
                get { return _userRelationshipExpiryDate; }
                set { _userRelationshipExpiryDate = value; }
            }

        [DataMember]    
        public double AmountTobePaid
            {
                get { return _userAmtTobePaid; }
                set { _userAmtTobePaid = value; }
            }

        [DataMember]    
        public double AmountPaidYTD
            {
                get { return _userAmtPaidYTD; }
                set { _userAmtPaidYTD = value; }
            }

        [DataMember]    
        public int UserThemeId
            {
                get { return _userThemeId; }
                set { _userThemeId = value; }
            }

        [DataMember]    
        public string UserPassword
            {
                get { return _userPwd; }
                set { _userPwd = value; }
            }

        [DataMember]    
        public int UserSecretQueryId
            {
                get { return _userSecretQueryId; }
                set { _userSecretQueryId = value; }
            }

        [DataMember]    
        public string UserSecretAnswer
            {
                get { return _userSecretAnswer; }
                set { _userSecretAnswer = value; }
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
                get { return _userCreateDate; }
                set { _userCreateDate = value; }
            }

        [DataMember]    
        public DateTime LastModDate
            {
                get { return _user_LastModdate; }
                set { _user_LastModdate = value; }
            }
        #endregion
    }
}



