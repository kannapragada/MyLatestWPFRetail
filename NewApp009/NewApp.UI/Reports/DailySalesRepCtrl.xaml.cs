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
using NewApp.SaleFactorySvc;
using NewApp.GenderTypeFactorySvc;
using NewApp.IDProofTypeFactorySvc;




namespace NewApp.Reports
{
    public partial class DailySalesRepCtrl : UserControl
    {
        public static readonly RoutedEvent DailySalesWithCustomArgsEvent = EventManager.RegisterRoutedEvent("DailySalesWithCustomArgs", RoutingStrategy.Bubble, typeof(DailySalesWithCustomArgsEventHandler), typeof(DailySalesRepCtrl));
        public delegate void DailySalesWithCustomArgsEventHandler(object sender, ProcessEventArgs e);
        
        public delegate void ProcessHandler(object sender, ProcessEventArgs data);
        string _mode;
        string _invokingCtrlMode;
        string _invokingCtrlName;
        string _CtrlTabName;

        UserCtrlArgs UCArgs;

        Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1;
        NewAppDataSet DailySalesRepDataSet;
        NewAppDataSetTableAdapters.GetRepDataDailySalesTableAdapter GetRepDataDailySalesTableAdapter;



        public DailySalesRepCtrl()
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

        public event DailySalesWithCustomArgsEventHandler DailySalesWithCustomArgs
        {
            add { AddHandler(DailySalesWithCustomArgsEvent, value); }
            remove { RemoveHandler(DailySalesWithCustomArgsEvent, value); }
        }

        private void btn_Reset_Click(object sender, RoutedEventArgs e)
        {
            this.date_StartDateofSale.SelectedDate = DateTime.Now;
            this.date_EndDateofSale.SelectedDate = DateTime.Now;
        }

        private void ReportViewer_Load()
        {
            reportDataSource1 = null;
            reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();

            DailySalesRepDataSet = null;
            DailySalesRepDataSet = new NewAppDataSet();

            DailySalesRepDataSet.BeginInit();

            reportDataSource1.Name = "DailySalesRepDataSet";
            reportDataSource1.Value = (DataTable)DailySalesRepDataSet.Tables["GetRepDataDailySales"];


            winFormHost_reportViewer.Clear();
            winFormHost_reportViewer.LocalReport.DataSources.Add(reportDataSource1);
            winFormHost_reportViewer.LocalReport.ReportPath = "../../Reports/Rdlcs/Regular/DailySalesRep.rdlc";

            DailySalesRepDataSet.EndInit();

            //fill data into NewAppDataSet
            GetRepDataDailySalesTableAdapter = null;
            GetRepDataDailySalesTableAdapter = new NewAppDataSetTableAdapters.GetRepDataDailySalesTableAdapter();

            GetRepDataDailySalesTableAdapter.ClearBeforeFill = true;
            GetRepDataDailySalesTableAdapter.Fill((NewAppDataSet.GetRepDataDailySalesDataTable)DailySalesRepDataSet.Tables["GetRepDataDailySales"], date_StartDateofSale.SelectedDate, date_EndDateofSale.SelectedDate);
            winFormHost_reportViewer.RefreshReport();
        }

        private void btn_Select_Click(object sender, RoutedEventArgs e)
        {
            ReportViewer_Load();
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
            ProcessEventArgs myargs = new ProcessEventArgs(DailySalesWithCustomArgsEvent, UCArgs.InvokingControlName, UCArgs.InvokingControlMode, UCArgs.TargetControlName, UCArgs.TargetControlMode, null, null, UCArgs.ControlTabName);
            RaiseEvent(myargs);
        }
    }
}

