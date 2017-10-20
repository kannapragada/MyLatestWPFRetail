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
    public class CustomerType
    {
        int     _customerTypeId;
        string  _customerValue;
        string  _customerDescription;

        public CustomerType()
        {
        }

        [DataMember]
        public int CustomerTypeId
        {
            get{return _customerTypeId;}
            set{_customerTypeId = value;}
        }

        [DataMember]
        public string CustomerTypeValue
        {
            get{return _customerValue;}
            set{_customerValue = value;}
        }

        [DataMember]
        public string CustomerTypeDescription
        {
            get { return _customerDescription; }
            set { _customerDescription = value; }
        }
    }
}
