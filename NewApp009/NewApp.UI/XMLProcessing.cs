using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace NewApp
{
    public class XMLProcessing
    {
        public void WriteXML()
        {

            XmlDocument xmlDoc = new XmlDocument();

            XmlNode rootNode = xmlDoc.CreateElement("ROOT");
            xmlDoc.AppendChild(rootNode);

            XmlNode empNode = xmlDoc.CreateElement("Employee");

            XmlNode empidNode1 = xmlDoc.CreateElement("EmpId");
            empidNode1.InnerText = "1";

            XmlNode empnameNode1 = xmlDoc.CreateElement("EmpName");
            empnameNode1.InnerText = "Hari";

            empNode.AppendChild(empidNode1);
            empNode.AppendChild(empnameNode1);

            XmlNode empdetailsNode = xmlDoc.CreateElement("EmpDetails");

            XmlNode empidNode1_1 = xmlDoc.CreateElement("EmpId");
            empidNode1_1.InnerText = "1";

            XmlNode empdeptidNode1 = xmlDoc.CreateElement("DeptId");
            empdeptidNode1.InnerText = "1";

            XmlNode empsalaryNode1 = xmlDoc.CreateElement("Salary");
            empsalaryNode1.InnerText = "1000";

            XmlNode empcreatedateNode1 = xmlDoc.CreateElement("CreateDate");
            empcreatedateNode1.InnerText = "2001-01-01";

            XmlNode emplastmoddateNode1 = xmlDoc.CreateElement("LastModDate");
            emplastmoddateNode1.InnerText = "";

            empdetailsNode.AppendChild(empidNode1_1);
            empdetailsNode.AppendChild(empdeptidNode1);
            empdetailsNode.AppendChild(empsalaryNode1);
            empdetailsNode.AppendChild(empcreatedateNode1);
            empdetailsNode.AppendChild(emplastmoddateNode1);

            empNode.AppendChild(empdetailsNode);

            rootNode.AppendChild(empNode);

            string text = xmlDoc.InnerXml.ToString();

        }
    }
}

