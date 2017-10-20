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
using NewApp.CustomerFactorySvc;
using NewApp.UI.Windows;



namespace NewApp.Reports
{
    public partial class CustOutstandingRepCtrl : UserControl
    {
        public static readonly RoutedEvent CustOutstandingWithCustomArgsEvent = EventManager.RegisterRoutedEvent("CustOutstandingWithCustomArgs", RoutingStrategy.Bubble, typeof(CustOutstandingWithCustomArgsEventHandler), typeof(CustOutstandingRepCtrl));
        public delegate void CustOutstandingWithCustomArgsEventHandler(object sender, ProcessEventArgs e);


        public delegate void ProcessHandler(object sender, ProcessEventArgs data);
        string _mode;
        string _invokingCtrlMode;
        string _invokingCtrlName;
        string _CtrlTabName;

        UserCtrlArgs UCArgs;


        Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1;
        NewAppDataSet MonthYearRepDataSet;
        NewAppDataSetTableAdapters.GetRepCustOutstandingDtlsTableAdapter CustOutstandingDtlsTableAdapter;


        //BillReport BillReport;


        public CustOutstandingRepCtrl()
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

        public event CustOutstandingWithCustomArgsEventHandler CustOutstandingWithCustomArgs
        {
            add { AddHandler(CustOutstandingWithCustomArgsEvent, value); }
            remove { RemoveHandler(CustOutstandingWithCustomArgsEvent, value); }
        }


        private void btn_Select_Click(object sender, RoutedEventArgs e)
        {ReportViewer_Load();}

        private void ReportViewer_Load()
        {
            reportDataSource1 = null;
            reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();

            MonthYearRepDataSet = null;
            MonthYearRepDataSet = new NewAppDataSet();

            MonthYearRepDataSet.BeginInit();

            reportDataSource1.Name = "NewAppDataSet";
            reportDataSource1.Value = (DataTable)MonthYearRepDataSet.Tables[4];


            ReportViewer _reportViewer = new ReportViewer();

            _reportViewer.LocalReport.DataSources.Add(reportDataSource1);

            _reportViewer.LocalReport.ReportPath = "../../Reports/Rdlcs/Regular/CustOutstanding.rdlc";

            MonthYearRepDataSet.EndInit();

            //fill data into NewAppDataSet
            CustOutstandingDtlsTableAdapter = null;
            CustOutstandingDtlsTableAdapter = new NewAppDataSetTableAdapters.GetRepCustOutstandingDtlsTableAdapter();

            CustOutstandingDtlsTableAdapter.ClearBeforeFill = true;
            CustOutstandingDtlsTableAdapter.Fill((NewAppDataSet.GetRepCustOutstandingDtlsDataTable)MonthYearRepDataSet.Tables[4],"U", null, "8", 2013);
            _reportViewer.RefreshReport();

            winFormHost_reportViewer = _reportViewer;
        }

        private void btn_Search_Click(object sender, RoutedEventArgs e)
        {
            string errorMessage = null;
            CustomerFactoryClient CustomerFactoryClient = new CustomerFactoryClient();
            Customer[] CustObjArray;
            List<Customer> CustObjList = new List<Customer>();

          
            dockpanel_SearchResults.Visibility = Visibility.Visible;

            //if (CustomerFactoryClient.GetCustomersByCriteria(out errorMessage, out CustObjArray, txt_CustId.Text, txt_CustName.Text, null, Convert.ToString(cmb_IDProofType.SelectedValue.ToString()), txt_IDProofValue.Text, Convert.ToDateTime(date_StartDateofBirth.SelectedDate), Convert.ToDateTime(date_EndDateofBirth.SelectedDate), Convert.ToDateTime(this.date_StartDateofRelationship.SelectedDate), Convert.ToDateTime(date_EndDateofRelationship.SelectedDate), Convert.ToDateTime(date_StartDateofExpiryRelationship.SelectedDate), Convert.ToDateTime(date_StartDateofExpiryRelationship.SelectedDate)) == true)
            if (CustomerFactoryClient.GetCustomersByCriteria(out errorMessage, out CustObjArray, txt_CustId.Text, txt_CustName.Text, null, Convert.ToInt32(cmb_IDProofType.Tag), txt_IDProofValue.Text, Convert.ToDateTime(date_StartDateofBirth.SelectedDate), Convert.ToDateTime(date_EndDateofBirth.SelectedDate), Convert.ToDateTime(this.date_StartDateofRelationship.SelectedDate), Convert.ToDateTime(date_EndDateofRelationship.SelectedDate), Convert.ToDateTime(date_StartDateofExpiryRelationship.SelectedDate), Convert.ToDateTime(date_StartDateofExpiryRelationship.SelectedDate)) == true)
                if (CustObjArray != null)
                {
                    CustObjList = CustObjArray.ToList<Customer>();
                    lb_SearchResults.ItemsSource = CustObjList;
                }
                else
                {
                    WinMessageBox WinMsgBox = new WinMessageBox("No Records Found!", "Customer Outstanding Report", 0, "INFORMATION");
                    WinMsgBox.ShowDialog();
                }
            else
            {
                WinMessageBox WinMsgBox = new WinMessageBox(errorMessage, "Customer Outstanding Report", 0, "ERROR");
                WinMsgBox.ShowDialog();
            }
        }

        private void btn_Reset_Click(object sender, RoutedEventArgs e)
        {
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
            ProcessEventArgs myargs = new ProcessEventArgs(CustOutstandingWithCustomArgsEvent, UCArgs.InvokingControlName, UCArgs.InvokingControlMode, UCArgs.TargetControlName, UCArgs.TargetControlMode, null, null, UCArgs.ControlTabName);
            RaiseEvent(myargs);
        }
    }
}

