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
    public class VendorStatus
    {
        int     _VendorStatusId;
        string  _VendorStatusValue;
        string  _VendorStatusDescription;

        public VendorStatus()
        {
        }

        [DataMember]
        public int VendorStatusId
        {
            get { return _VendorStatusId; }
            set { _VendorStatusId = value; }
        }

        [DataMember]
        public string VendorStatusValue
        {
            get { return _VendorStatusValue; }
            set { _VendorStatusValue = value; }
        }

        [DataMember]
        public string VendorStatusDescription
        {
            get { return _VendorStatusDescription; }
            set { _VendorStatusDescription = value; }
        }
    }
}
