using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Serialization;


namespace NewApp.BusinessTier.Models
{
    [DataContract]
    public class UserType
    {
        int     _userTypeId;
        string  _userValue;
        string  _userDescription;

        public UserType()
        {
        }

        [DataMember]
        public int UserTypeId
        {
            get{return _userTypeId;}
            set{_userTypeId = value;}
        }

        [DataMember]
        public string UserTypeValue
        {
            get{return _userValue;}
            set{_userValue = value;}
        }

        [DataMember]
        public string UserTypeDescription
        {
            get { return _userDescription; }
            set { _userDescription = value; }
        }
    }
}
