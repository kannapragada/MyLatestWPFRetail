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
using NewApp.UI.Windows;
using NewApp.SQLDataAccessFactorySvc;
using NewApp.SaleFactorySvc;
using NewApp.GenderTypeFactorySvc;
using NewApp.IDProofTypeFactorySvc;




namespace NewApp.Reports
{
    public partial class ExpiringItemsRepCtrl : UserControl
    {
        public static readonly RoutedEvent ExpiringItemsWithCustomArgsEvent = EventManager.RegisterRoutedEvent("ExpiringItemsWithCustomArgs", RoutingStrategy.Bubble, typeof(ExpiringItemsWithCustomArgsEventHandler), typeof(ExpiringItemsRepCtrl));
        public delegate void ExpiringItemsWithCustomArgsEventHandler(object sender, ProcessEventArgs e);


        public delegate void ProcessHandler(object sender, ProcessEventArgs data);
        string _mode;
        string _invokingCtrlMode;
        string _invokingCtrlName;
        string _CtrlTabName;

        UserCtrlArgs UCArgs;

        Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1;
        NewAppDataSet MonthYearRepDataSet;
        NewAppDataSetTableAdapters.GetRepExpiringItemsTableAdapter ExpiringItemsTableAdapter;


        DataAccessServiceClient Service = new DataAccessServiceClient();


        //BillReport BillReport;


        public ExpiringItemsRepCtrl()
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

        public string ControlTabName
        {
            get { return _CtrlTabName; }
            set { _CtrlTabName = value; }
        }

        public event ExpiringItemsWithCustomArgsEventHandler ExpiringItemsWithCustomArgs
        {
            add { AddHandler(ExpiringItemsWithCustomArgsEvent, value); }
            remove { RemoveHandler(ExpiringItemsWithCustomArgsEvent, value); }
        }


        private void btn_Select_Click(object sender, RoutedEventArgs e)
        {ReportViewer_Load();}

        private void ReportViewer_Load()
        {
            try
            {
                reportDataSource1 = null;
                reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();

                MonthYearRepDataSet = null;
                MonthYearRepDataSet = new NewAppDataSet();

                MonthYearRepDataSet.BeginInit();

                reportDataSource1.Name = "NewAppDataSet";
                reportDataSource1.Value = (DataTable)MonthYearRepDataSet.Tables[5];


                ReportViewer _reportViewer = new ReportViewer();

                _reportViewer.LocalReport.DataSources.Add(reportDataSource1);

                _reportViewer.LocalReport.ReportPath = "../../Reports/Rdlcs/Regular/ExpiringItems.rdlc";

                MonthYearRepDataSet.EndInit();

                //fill data into NewAppDataSet
                ExpiringItemsTableAdapter = null;
                ExpiringItemsTableAdapter = new NewAppDataSetTableAdapters.GetRepExpiringItemsTableAdapter();

                ExpiringItemsTableAdapter.ClearBeforeFill = true;
                ExpiringItemsTableAdapter.Fill((NewAppDataSet.GetRepExpiringItemsDataTable)MonthYearRepDataSet.Tables["GetRepExpiringItems"], "U", null, 8, 2013);
                _reportViewer.RefreshReport();

                winFormHost_reportViewer = _reportViewer;
            }
            catch (Exception ex1)
            {
                WinMessageBox WinMsgBox = new WinMessageBox(ex1.Message, "Items Expiring Report", 0, "ERROR");
                WinMsgBox.ShowDialog();
            }


        }

        private void btn_Search_Click(object sender, RoutedEventArgs e)
        {
            string errorMessage = null;


            dockpanel_SearchResults.Visibility = Visibility.Visible;

            SaleFactoryClient SaleFactoryClient = new SaleFactoryClient();

            Sale[] SaleObjArray;
            List<Sale> SaleObjList = new List<Sale>();
            if (SaleFactoryClient.SearchSaleDetails(out SaleObjArray, out errorMessage, txt_SaleId.Text, txt_CustId.Text, txt_CustName.Text, Convert.ToDateTime(date_StartDateofSale.SelectedDate), Convert.ToDateTime(date_EndDateofSale.SelectedDate)) == true)
                if (SaleObjArray != null)
                {
                    SaleObjList = SaleObjArray.ToList<Sale>();
                    lb_SearchResults.ItemsSource = SaleObjList;
                }
                else
                {
                    WinMessageBox WinMsgBox = new WinMessageBox("No Records Found!", "Items Expiring Report", 0, "INFORMATION");
                    WinMsgBox.ShowDialog();
                }
            else
            {
                WinMessageBox WinMsgBox = new WinMessageBox(errorMessage, "Items Expiring Report", 0, "ERROR");
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

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            UCArgs = new UserCtrlArgs();
            UCArgs.InvokingControlName = this.Name;
            UCArgs.InvokingControlMode = this.Mode;
            UCArgs.TargetControlName = this.InvokingControlName;
            UCArgs.TargetControlMode = this.InvokingControlMode;
            UCArgs.ControlTabName = this.ControlTabName;

            e.Handled = true;
            ProcessEventArgs myargs = new ProcessEventArgs(ExpiringItemsWithCustomArgsEvent, UCArgs.InvokingControlName, UCArgs.InvokingControlMode, UCArgs.TargetControlName, UCArgs.TargetControlMode, null, null, UCArgs.ControlTabName);
            RaiseEvent(myargs);
        }
    }
}

