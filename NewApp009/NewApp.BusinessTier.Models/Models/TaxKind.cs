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
    public class TaxKind
    {
        int     _taxKindId;
        string  _taxKindValue;
        string  _taxKindDescription;


        public TaxKind()
        {
        }

        [DataMember]
        public int TaxKindId
        {
            get{return _taxKindId;}
            set{_taxKindId = value;}
        }

        [DataMember]
        public string TaxKindValue
        {
            get{return _taxKindValue;}
            set{_taxKindValue = value;}
        }

        [DataMember]
        public string TaxKindDescription
        {
            get { return _taxKindDescription; }
            set { _taxKindDescription = value; }
        }
    }
}
