using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Data;
using NewApp.BusinessTier.Models;
using WCFRetailService;



namespace WCFRetailService
{
    public class XMLProcessing
    {
        public XmlDocument WriteXML(Sale SaleObj)
        {
            XmlDocument xmlDoc = new XmlDocument();

            XmlNode rootNode = xmlDoc.CreateElement("ROOT");
            xmlDoc.AppendChild(rootNode);

            XmlNode saleMainNode = xmlDoc.CreateElement("Sale");

            XmlNode SaleNode = xmlDoc.CreateElement("SalesId");
            SaleNode.InnerText = SaleObj.SalesId.ToString();
            saleMainNode.AppendChild(SaleNode);

            SaleNode = xmlDoc.CreateElement("SalesStatusId");
            SaleNode.InnerText = SaleObj.SaleStatus.ToString();
            saleMainNode.AppendChild(SaleNode);

            SaleNode = xmlDoc.CreateElement("SalesCustId");
            SaleNode.InnerText = SaleObj.CustomerInfo.CustomerId.ToString();
            saleMainNode.AppendChild(SaleNode);

            SaleNode = xmlDoc.CreateElement("SalesTotalSalesAmt");
            SaleNode.InnerText = SaleObj.TotalSaleAmount.ToString();
            saleMainNode.AppendChild(SaleNode);

            SaleNode = xmlDoc.CreateElement("SalesTotalDiscAmt");
            SaleNode.InnerText = SaleObj.TotalDiscountAmount.ToString();
            saleMainNode.AppendChild(SaleNode);

            SaleNode = xmlDoc.CreateElement("SalesTotalTaxAmt");
            SaleNode.InnerText = SaleObj.TotalTaxAmount.ToString();
            saleMainNode.AppendChild(SaleNode);

            SaleNode = xmlDoc.CreateElement("SalesFinalSalesAmt");
            SaleNode.InnerText = SaleObj.FinalSaleAmount.ToString();
            saleMainNode.AppendChild(SaleNode);

            SaleNode = xmlDoc.CreateElement("SalesAmtPaid");
            SaleNode.InnerText = SaleObj.PaidAmount.ToString();
            saleMainNode.AppendChild(SaleNode);

            SaleNode = xmlDoc.CreateElement("SalesBalanceAmt");
            SaleNode.InnerText = SaleObj.BalanceAmount.ToString();
            saleMainNode.AppendChild(SaleNode);

            SaleNode = xmlDoc.CreateElement("SalesCreateDate");
            SaleNode.InnerText = DateTime.Now.ToString("yyyy-MM-dd HH':'mm':'ss");
            saleMainNode.AppendChild(SaleNode);

            SaleNode = xmlDoc.CreateElement("SalesLastModDate");
            SaleNode.InnerText = DateTime.Now.ToString("yyyy-MM-dd HH':'mm':'ss");
            saleMainNode.AppendChild(SaleNode);

            SaleNode = xmlDoc.CreateElement("SalesUserId");
            SaleNode.InnerText = SaleObj.UserInfo.UserId.ToString();
            saleMainNode.AppendChild(SaleNode);

            SaleNode = xmlDoc.CreateElement("SalesGuestName");
            SaleNode.InnerText = SaleObj.CustomerInfo.GuestName.ToString();
            saleMainNode.AppendChild(SaleNode);

            XmlNode saledetailsNode;

            for (int i = 0; i < SaleObj.SaleItemsList.Count; i++)
            {

                saledetailsNode = xmlDoc.CreateElement("ItemDetails");

                XmlNode itemNode = xmlDoc.CreateElement("SalesId");
                itemNode.InnerText = SaleObj.SalesId.ToString();
                saledetailsNode.AppendChild(itemNode);

                itemNode = xmlDoc.CreateElement("SerialNumb");
                itemNode.InnerText = SaleObj.SaleItemsList[i].SerialNumber.ToString();
                saledetailsNode.AppendChild(itemNode);

                itemNode = xmlDoc.CreateElement("ItemId");
                itemNode.InnerText = SaleObj.SaleItemsList[i].ItemId.ToString();
                saledetailsNode.AppendChild(itemNode);  

                itemNode = xmlDoc.CreateElement("BatchId");
                itemNode.InnerText = SaleObj.SaleItemsList[i].BatchId.ToString();
                saledetailsNode.AppendChild(itemNode);

                itemNode = xmlDoc.CreateElement("QuantitySold");
                itemNode.InnerText = SaleObj.SaleItemsList[i].Quantity.ToString();
                saledetailsNode.AppendChild(itemNode);

                itemNode = xmlDoc.CreateElement("Weight");
                itemNode.InnerText = SaleObj.SaleItemsList[i].Weight.ToString();
                saledetailsNode.AppendChild(itemNode);

                itemNode = xmlDoc.CreateElement("ItemAmtPerUnit");
                itemNode.InnerText = SaleObj.SaleItemsList[i].ItemAmtPerUnit.ToString();
                saledetailsNode.AppendChild(itemNode);

                itemNode = xmlDoc.CreateElement("TotalItemAmt");
                itemNode.InnerText = SaleObj.SaleItemsList[i].TotalItemAmt.ToString();
                saledetailsNode.AppendChild(itemNode);

                itemNode = xmlDoc.CreateElement("DiscAmtPerUnit");
                itemNode.InnerText = SaleObj.SaleItemsList[i].DiscountAmountPerUnit.ToString();
                saledetailsNode.AppendChild(itemNode);

                itemNode = xmlDoc.CreateElement("DiscItemAmt");
                itemNode.InnerText = SaleObj.SaleItemsList[i].ItemDiscAmount.ToString();
                saledetailsNode.AppendChild(itemNode);

                itemNode = xmlDoc.CreateElement("TaxAmtPerUnit");
                itemNode.InnerText = SaleObj.SaleItemsList[i].DiscountAmountPerUnit.ToString();
                saledetailsNode.AppendChild(itemNode);

                itemNode = xmlDoc.CreateElement("TaxItemAmt");
                itemNode.InnerText = SaleObj.SaleItemsList[i].ItemTaxAmount.ToString();
                saledetailsNode.AppendChild(itemNode);

                itemNode = xmlDoc.CreateElement("FinalItemAmt");
                itemNode.InnerText = SaleObj.SaleItemsList[i].FinalItemAmt.ToString();
                saledetailsNode.AppendChild(itemNode);

                itemNode = xmlDoc.CreateElement("SDCreateDate");
                itemNode.InnerText = DateTime.Now.ToString("yyyy-MM-dd HH':'mm':'ss");
                saledetailsNode.AppendChild(itemNode);

                itemNode = xmlDoc.CreateElement("SDLastModDate");
                itemNode.InnerText = DateTime.Now.ToString("yyyy-MM-dd HH':'mm':'ss");
                saledetailsNode.AppendChild(itemNode);

                saleMainNode.AppendChild(saledetailsNode);
            }

            rootNode.AppendChild(saleMainNode);

            return xmlDoc;

            //string text = xmlDoc.InnerXml.ToString();

            //return text;
        }
    }
}

