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
    public class DiscountKind
    {
        int     _discountKindId;
        string  _discountKindValue;
        string  _discountKindDescription;


        public DiscountKind()
        {
        }

        [DataMember]
        public int DiscountKindId
        {
            get{return _discountKindId;}
            set{_discountKindId = value;}
        }

        [DataMember]
        public string DiscountKindValue
        {
            get{return _discountKindValue;}
            set{_discountKindValue = value;}
        }

        [DataMember]
        public string DiscountKindDescription
        {
            get { return _discountKindDescription; }
            set { _discountKindDescription = value; }
        }
    }
}
