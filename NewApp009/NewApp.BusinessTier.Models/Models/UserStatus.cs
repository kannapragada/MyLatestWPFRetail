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
    public class UserStatus
    {
        int     _userStatusId;
        string  _userStatusValue;
        string  _userStatusDescription;


        public UserStatus()
        {
        }

        [DataMember]
        public int UserStatusId
        {
            get { return _userStatusId; }
            set { _userStatusId = value; }
        }

        [DataMember]
        public string UserStatusValue
        {
            get { return _userStatusValue; }
            set { _userStatusValue = value; }
        }

        [DataMember]
        public string UserStatusDescription
        {
            get { return _userStatusDescription; }
            set { _userStatusDescription = value; }
        }
    }
}
