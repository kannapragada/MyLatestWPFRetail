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
    public class DiscountType
    {
        int     _discountTypeId;
        string  _discountValue;
        string  _discountDescription;


        public DiscountType()
        {
        }

        [DataMember]
        public int DiscountTypeId
        {
            get{return _discountTypeId;}
            set{_discountTypeId = value;}
        }

        [DataMember]
        public string DiscountTypeValue
        {
            get{return _discountValue;}
            set{_discountValue = value;}
        }

        [DataMember]
        public string DiscountTypeDescription
        {
            get { return _discountDescription; }
            set { _discountDescription = value; }
        }
    }
}
