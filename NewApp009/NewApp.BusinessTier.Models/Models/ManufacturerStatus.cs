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
    public class ManufacturerStatus
    {
        int     _manufacturerStatusId;
        string  _manufacturerStatusValue;
        string  _manufacturerStatusDescription;

        public ManufacturerStatus()
        {
        }

        [DataMember]
        public int ManufacturerStatusId
        {
            get { return _manufacturerStatusId; }
            set { _manufacturerStatusId = value; }
        }

        [DataMember]
        public string ManufacturerStatusValue
        {
            get { return _manufacturerStatusValue; }
            set { _manufacturerStatusValue = value; }
        }

        [DataMember]
        public string ManufacturerStatusDescription
        {
            get { return _manufacturerStatusDescription; }
            set { _manufacturerStatusDescription = value; }
        }
    }
}
