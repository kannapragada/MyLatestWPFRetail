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
    public class IDProofType
    {
        int     _iDProofTypeId;
        string  _iDProofTypeValue;
        string  _iDProofTypeDescription;

        public IDProofType()
        {
        }

        [DataMember]
        public int IDProofTypeId
        {
            get{return _iDProofTypeId;}
            set{_iDProofTypeId = value;}
        }

        [DataMember]
        public string IDProofTypeValue
        {
            get{return _iDProofTypeValue;}
            set{_iDProofTypeValue = value;}
        }

        [DataMember]
        public string IDProofTypeDescription
        {
            get { return _iDProofTypeDescription; }
            set { _iDProofTypeDescription = value; }
        }
    }
}
