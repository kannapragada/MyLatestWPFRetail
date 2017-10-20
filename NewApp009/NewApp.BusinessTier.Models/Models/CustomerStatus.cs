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
    public class CustomerStatus
    {
        int     _customerStatusId;
        string  _customerStatusValue;
        string  _customerStatusDescription;


        public CustomerStatus()
        {    
        }

        [DataMember]
        public int CustomerStatusId
        {
            get { return _customerStatusId; }
            set { _customerStatusId = value; }
        }

        [DataMember]
        public string CustomerStatusValue
        {
            get { return _customerStatusValue; }
            set { _customerStatusValue = value; }
        }

        [DataMember]
        public string CustomerStatusDescription
        {
            get { return _customerStatusDescription; }
            set { _customerStatusDescription = value; }
        }
    }

    
}
