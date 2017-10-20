using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using Microsoft.Reporting.WinForms;
using NewApp.UI.UserControls;
using NewApp.BusinessTier.Common;
using NewApp.BusinessTier.Models;
using NewApp.SaleFactorySvc;
using NewApp.UI.Windows;
using NewApp.GenderTypeFactorySvc;
using NewApp.IDProofTypeFactorySvc;




namespace NewApp.Reports
{
    public partial class BillReportCtrl : UserControl
    {
        public delegate void NotifyUserControl(object sender, UserCtrlArgs UserCtrlArgs);
        string _mode;
        string _invokingCtrlMode;
        string _invokingCtrlName;


        BillReport BillReport;


        public BillReportCtrl()
        {InitializeComponent();}

        public string Mode
        {
            get { return _mode; }
            set { _mode = value; }
        }

        public string InvokingControlMode
        {
            get { return _invokingCtrlMode; }
            set { _invokingCtrlMode = value; }
        }

        public string InvokingControlName
        {
            get { return _invokingCtrlName; }
            set { _invokingCtrlName = value; }
        }

        private void btn_Select_Click(object sender, RoutedEventArgs e)
        {
            if (BillReport != null)
            {dockpanel_docpanel.Children.Remove(BillReport);}

            if (lb_SearchResults.Items.Count > 0)
                if (lb_SearchResults.SelectedIndex > -1)
                {
                    Sale Sale = (Sale)lb_SearchResults.Items[lb_SearchResults.SelectedIndex];

                    BillReport = new BillReport(Sale.SalesId, false);

                    dockpanel_docpanel.Children.Add(BillReport);
                }
        }

        private void btn_Search_Click(object sender, RoutedEventArgs e)
        {
            string errorMessage = null;
            SaleFactoryClient SaleFactoryClient = new SaleFactoryClient();
            Sale[] SaleObjArray;
            List<Sale> SaleObjList = new List<Sale>();

          
            dockpanel_SearchResults.Visibility = Visibility.Visible;

            if (SaleFactoryClient.SearchSaleDetails(out SaleObjArray, out errorMessage, txt_SaleId.Text, txt_CustId.Text, txt_CustName.Text, Convert.ToDateTime(date_StartDateofSale.SelectedDate), Convert.ToDateTime(date_EndDateofSale.SelectedDate)) == true)
                if (SaleObjArray != null)
                {
                    SaleObjList = SaleObjArray.ToList<Sale>();
                    lb_SearchResults.ItemsSource = SaleObjList;
                }
                else
                {
                    WinMessageBox WinMsgBox = new WinMessageBox("No Records Found!", "Daily Sales Report", 0, "INFORMATION");
                    WinMsgBox.ShowDialog();
                }
            else
            {
                WinMessageBox WinMsgBox = new WinMessageBox(errorMessage, "Bill Report", 0, "ERROR");
                WinMsgBox.ShowDialog();
            }
        }

        private void btn_Reset_Click(object sender, RoutedEventArgs e)
        {
            txt_SaleId.Text = "";
            txt_CustId.Text = "";
            txt_CustName.Text = "";
            lb_SearchResults.ItemsSource = null;
            dockpanel_SearchResults.Visibility = Visibility.Collapsed;
        }
    }
}

