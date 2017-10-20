using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Runtime.Serialization;



namespace NewApp.BusinessTier.Models
{
    [DataContract]
    public class GenderType
    {
        int     _genderId;
        string  _genderValue;
        string  _genderDescription;

        public GenderType()
        {
        }

        [DataMember]
        public int GenderTypeId
        {
            get{return _genderId;}
            set{_genderId = value;}
        }

        [DataMember]
        public string GenderTypeValue
        {
            get{return _genderValue;}
            set{_genderValue = value;}
        }

        [DataMember]
        public string GenderTypeDescription
        {
            get { return _genderDescription; }
            set { _genderDescription = value; }
        }
    }
}
