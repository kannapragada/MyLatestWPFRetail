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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using Microsoft.Reporting.WinForms;
using NewApp.BusinessTier.Common;
using NewApp.BusinessTier.Models;
using NewApp.GenderTypeFactorySvc;
using NewApp.IDProofTypeFactorySvc;
using NewApp.UI.UserControls;




namespace NewApp.Reports
{
    public partial class BillReport : UserControl
    {
        public delegate void NotifyUserControl(object sender, UserCtrlArgs UserCtrlArgs);
        //public event NotifyUserControl NotifyedUserControl;
        string _mode;
        string _invokingCtrlMode;
        string _invokingCtrlName;

        private string m_sale_id = "";
        private bool m_autoprint = false;


        int SwitchSubReport = 1;

        public BillReport()
        {
            InitializeComponent();
            _reportViewer.Load += ReportViewer_Load;
        }

        public BillReport(string sale_id, bool AutoPrint)
        {
            InitializeComponent();

            m_sale_id = sale_id;
            m_autoprint = AutoPrint;
            _reportViewer.Load += ReportViewer_Load;
        }


        public string Mode
        {
            get{return _mode;}
            set{_mode = value;}
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

        private void ReportViewer_Load(object sender, EventArgs e)
        {
            this._reportViewer.LocalReport.ReportPath = "../../Reports/Rdlcs/Regular/BillRepMain.rdlc";

            _reportViewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SubreportProcessingEventHandler);

            _reportViewer.RefreshReport();
            _reportViewer.LocalReport.Refresh();
        }

        void SubreportProcessingEventHandler(object sender, SubreportProcessingEventArgs e)
        {
            BillDataSet resds = new BillDataSet();

            switch (SwitchSubReport)
            {
                case 1:
                    {
                        ReportDataSource reportDataSourceBasic = new ReportDataSource("BillBasic", resds.Tables[0]);
                        reportDataSourceBasic.Name = "BillDataSet";
                        reportDataSourceBasic.Value = (DataTable)resds.Tables[0];
                        e.DataSources.Add(reportDataSourceBasic);

                        BillDataSetTableAdapters.BillBasicTableDataTableAdapter salesadptr = new BillDataSetTableAdapters.BillBasicTableDataTableAdapter();
                        salesadptr.ClearBeforeFill = true;
                        salesadptr.Fill((BillDataSet.BillBasicDataTableDataTable)resds.Tables[0], m_sale_id);
                        SwitchSubReport = 2;
                        break;
                    }
                case 2:
                    {
                        ReportDataSource reportDataSourceDetails = new ReportDataSource("BillDetail", resds.Tables[1]);
                        reportDataSourceDetails.Name = "BillDataSet";
                        reportDataSourceDetails.Value = (DataTable)resds.Tables[1];
                        e.DataSources.Add(reportDataSourceDetails);

                        BillDataSetTableAdapters.BillDetailTableDataTableAdapter salesdtladptr = new BillDataSetTableAdapters.BillDetailTableDataTableAdapter();
                        salesdtladptr.ClearBeforeFill = true;
                        salesdtladptr.Fill((BillDataSet.BillDetailDataTableDataTable)resds.Tables[1], m_sale_id);
                        break;
                    }
            }
        }

        private void btn_Get_Click(object sender, RoutedEventArgs e)
        {

        }

        private void WindowsFormsHost_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void _reportViewer_RenderingComplete(object sender, RenderingCompleteEventArgs e)
        {
            if (m_autoprint == true)
                _reportViewer.PrintDialog();
        }
    }
}
