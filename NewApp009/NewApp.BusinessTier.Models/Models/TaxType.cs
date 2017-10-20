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
    public class TaxType
    {
        int     _taxTypeId;
        string  _taxTypeValue;
        string  _taxTypeDescription;

        public TaxType()
        {
        }

        [DataMember]
        public int TaxTypeId
        {
            get{return _taxTypeId;}
            set{_taxTypeId = value;}
        }

        [DataMember]
        public string TaxTypeValue
        {
            get{return _taxTypeValue;}
            set{_taxTypeValue = value;}
        }

        [DataMember]
        public string TaxTypeDescription
        {
            get { return _taxTypeDescription; }
            set { _taxTypeDescription = value; }
        }
    }
}
