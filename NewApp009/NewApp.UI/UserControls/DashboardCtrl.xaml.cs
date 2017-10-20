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
using NewApp.UI.UserControls;
using NewApp.GenderTypeFactorySvc;
using NewApp.IDProofTypeFactorySvc;





namespace NewApp.UI.UserControls
{
    public partial class DashboardCtrl : UserControl
    {
        Microsoft.Reporting.WinForms.ReportDataSource reportDataSource_Summary;
        Microsoft.Reporting.WinForms.ReportDataSource rds_Customer;
        Microsoft.Reporting.WinForms.ReportDataSource rds_Sales;
        Microsoft.Reporting.WinForms.ReportDataSource rds_Items;
        NewAppDataSet MonthYearCustomerRepDataSet;
        NewAppDataSet MonthYearSalesRepDataSet;
        NewAppDataSet MonthYearItemsRepDataSet;
        NewAppDataSet StatisticsDataSet;
        NewAppDataSetTableAdapters.GetStatisticsForReportTableAdapter StatisticsDataTableAdapter;
        NewAppDataSetTableAdapters.GetRepDataMonthlyByEntityTableAdapter MonthRepCustomerDataTableAdapter;
        NewAppDataSetTableAdapters.GetRepDataMonthlyByEntityTableAdapter MonthRepSalesDataTableAdapter;
        NewAppDataSetTableAdapters.GetRepDataMonthlyByEntityTableAdapter MonthRepItemsDataTableAdapter;
        NewAppDataSetTableAdapters.GetRepDataYearlyByEntityTableAdapter YearRepCustomerDataTableAdapter;
        NewAppDataSetTableAdapters.GetRepDataYearlyByEntityTableAdapter YearRepSalesDataTableAdapter;
        NewAppDataSetTableAdapters.GetRepDataYearlyByEntityTableAdapter YearRepItemsDataTableAdapter;

        public static readonly RoutedEvent DashboardWithCustomArgsEvent = EventManager.RegisterRoutedEvent("DashboardWithCustomArgs", RoutingStrategy.Bubble, typeof(DashboardWithCustomArgsEventHandler), typeof(DashboardCtrl));
        public delegate void DashboardWithCustomArgsEventHandler(object sender, ProcessEventArgs e);


        public delegate void ProcessHandler(object sender, ProcessEventArgs data);
        string _mode;
        string _invokingCtrlMode;
        string _invokingCtrlName;
        string _CtrlTabName;

        public delegate void NotifyUserControl(object sender, UserCtrlArgs UserCtrlArgs);

        bool _isreportLoaded;


        public DashboardCtrl()
        {
            _isreportLoaded = false;


            InitializeComponent();

            tb_Value.Visibility = Visibility.Collapsed;
            cmb_Value.Visibility = Visibility.Collapsed;
            cmb_Value1.Visibility = Visibility.Collapsed;

            rv_Customers.ShowToolBar = false;
            rv_Items.ShowToolBar = false;
            rv_Sales.ShowToolBar = false;
            _reportViewer_summary.ShowToolBar = false;

            rv_Customers.Load += ReportViewer_Customer_Load;
            rv_Items.Load += ReportViewer_Sales_Load;
            rv_Sales.Load += ReportViewer_Items_Load;
            _reportViewer_summary.Load += ReportViewer_Summary_Load;
        }

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

        public event DashboardWithCustomArgsEventHandler DashboardWithCustomArgs
        {
            add { AddHandler(DashboardWithCustomArgsEvent, value); }
            remove { RemoveHandler(DashboardWithCustomArgsEvent, value); }
        }

        private void ReportViewer_Customer_Load(object sender, EventArgs e)
        {
            string EntityOption = "C";
            string PeriodOption = Convert.ToString(((ComboBoxItem)cmb_CperiodOption.SelectedItem).Tag);

            if (EntityOption == "C")
                if (PeriodOption == "M")
                    LoadMISMonthRep_Customer("C");
                else if (PeriodOption == "Y")
                    LoadMISYearRep_Customer("C");
        }

        private void ReportViewer_Sales_Load(object sender, EventArgs e)
        {
            string EntityOption = "S";
            string PeriodOption = Convert.ToString(((ComboBoxItem)cmb_SperiodOption.SelectedItem).Tag);

            if (EntityOption == "S")
                if (PeriodOption == "M")
                    LoadMISMonthRep_Sales("S");
                else if (PeriodOption == "Y")
                    LoadMISYearRep_Sales("S");
        }

        private void ReportViewer_Items_Load(object sender, EventArgs e)
        {
            string EntityOption = "I";
            string PeriodOption = Convert.ToString(((ComboBoxItem)cmb_IperiodOption.SelectedItem).Tag);

            if (EntityOption == "I")
                if (PeriodOption == "M")
                    LoadMISMonthRep_Items("I");
                else if (PeriodOption == "Y")
                    LoadMISYearRep_Items("I");
        }

        private void ReportViewer_Summary_Load(object sender, EventArgs e)
        {LoadReport_Summary("D", 0, 0);}

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {_isreportLoaded = true;}

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            rv_Customers.LocalReport.DataSources.Remove(rds_Customer);
            rv_Sales.LocalReport.DataSources.Remove(rds_Sales);
            rv_Items.LocalReport.DataSources.Remove(rds_Items);

            string EntityOption = "C";
            string PeriodOption = "Y";

            if (PeriodOption == "M")
                LoadMISMonthRep_Customer(EntityOption);
            else if (PeriodOption == "Y")
                    LoadMISYearRep_Customer(EntityOption);
        }

        private void LoadMISMonthRep_Customer(string Entity)
        {
            rds_Customer = null;
            rds_Customer = new Microsoft.Reporting.WinForms.ReportDataSource();

            MonthYearCustomerRepDataSet = null;
            MonthYearCustomerRepDataSet = new NewAppDataSet();

            MonthYearCustomerRepDataSet.BeginInit();

            MonthYearCustomerRepDataSet.GetChanges();

            rds_Customer.Name = "NewAppDataSet";
            rds_Customer.Value = (DataTable)MonthYearCustomerRepDataSet.Tables[2];


            rv_Customers.Clear();
            rv_Customers.LocalReport.DataSources.Add(rds_Customer);
            rv_Customers.LocalReport.ReportPath = "../../Reports/Rdlcs/MIS/MISCustMonthRep.rdlc";

            MonthYearCustomerRepDataSet.EndInit();

            //fill data into NewAppDataSet
            MonthRepCustomerDataTableAdapter = null;
            MonthRepCustomerDataTableAdapter = new NewAppDataSetTableAdapters.GetRepDataMonthlyByEntityTableAdapter();

            MonthRepCustomerDataTableAdapter.ClearBeforeFill = true;
            MonthRepCustomerDataTableAdapter.Fill((NewAppDataSet.GetRepDataMonthlyByEntityDataTable)MonthYearCustomerRepDataSet.Tables[2], Entity);
            rv_Customers.RefreshReport();
        }

        private void LoadMISYearRep_Customer(string Entity)
        {
            rds_Customer = null;
            rds_Customer = new Microsoft.Reporting.WinForms.ReportDataSource();

            MonthYearCustomerRepDataSet = null;
            MonthYearCustomerRepDataSet = new NewAppDataSet();

            MonthYearCustomerRepDataSet.BeginInit();

            MonthYearCustomerRepDataSet.GetChanges();


            rds_Customer.Name = "NewAppDataSet";
            rds_Customer.Value = (DataTable)MonthYearCustomerRepDataSet.Tables[3];


            rv_Customers.Clear();
            rv_Customers.LocalReport.DataSources.Add(rds_Customer);
            rv_Customers.LocalReport.ReportPath = "../../Reports/Rdlcs/MIS/MISCustYearRep.rdlc";

            MonthYearCustomerRepDataSet.EndInit();

            //fill data into NewAppDataSet
            YearRepCustomerDataTableAdapter = null;
            YearRepCustomerDataTableAdapter = new NewAppDataSetTableAdapters.GetRepDataYearlyByEntityTableAdapter();

            YearRepCustomerDataTableAdapter.ClearBeforeFill = true;
            YearRepCustomerDataTableAdapter.Fill((NewAppDataSet.GetRepDataYearlyByEntityDataTable)MonthYearCustomerRepDataSet.Tables[3], Entity);
            rv_Customers.RefreshReport();
        }

        private void LoadMISMonthRep_Sales(string Entity)
        {
            rds_Sales = null;
            rds_Sales = new Microsoft.Reporting.WinForms.ReportDataSource();

            MonthYearSalesRepDataSet = null;
            MonthYearSalesRepDataSet = new NewAppDataSet();

            MonthYearSalesRepDataSet.BeginInit();

            MonthYearSalesRepDataSet.GetChanges();

            rds_Sales.Name = "NewAppDataSet";
            rds_Sales.Value = (DataTable)MonthYearSalesRepDataSet.Tables[2];


            rv_Sales.Clear();
            rv_Sales.LocalReport.DataSources.Add(rds_Sales);
            rv_Sales.LocalReport.ReportPath = "../../Reports/Rdlcs/MIS/MISSalesMonthRep.rdlc";

            MonthYearSalesRepDataSet.EndInit();

            //fill data into NewAppDataSet
            MonthRepSalesDataTableAdapter = null;
            MonthRepSalesDataTableAdapter = new NewAppDataSetTableAdapters.GetRepDataMonthlyByEntityTableAdapter();

            MonthRepSalesDataTableAdapter.ClearBeforeFill = true;
            MonthRepSalesDataTableAdapter.Fill((NewAppDataSet.GetRepDataMonthlyByEntityDataTable)MonthYearSalesRepDataSet.Tables[2], Entity);
            rv_Sales.RefreshReport();
        }

        private void LoadMISYearRep_Sales(string Entity)
        {
            rds_Sales = null;
            rds_Sales = new Microsoft.Reporting.WinForms.ReportDataSource();

            MonthYearSalesRepDataSet = null;
            MonthYearSalesRepDataSet = new NewAppDataSet();

            MonthYearSalesRepDataSet.BeginInit();

            MonthYearSalesRepDataSet.GetChanges();

            rds_Sales.Name = "NewAppDataSet";
            rds_Sales.Value = (DataTable)MonthYearSalesRepDataSet.Tables[3];


            rv_Sales.Clear();
            rv_Sales.LocalReport.DataSources.Add(rds_Sales);
            rv_Sales.LocalReport.ReportPath = "../../Reports/Rdlcs/MIS/MISSalesYearRep.rdlc";

            MonthYearSalesRepDataSet.EndInit();

            //fill data into NewAppDataSet
            YearRepSalesDataTableAdapter = null;
            YearRepSalesDataTableAdapter = new NewAppDataSetTableAdapters.GetRepDataYearlyByEntityTableAdapter();

            YearRepSalesDataTableAdapter.ClearBeforeFill = true;
            YearRepSalesDataTableAdapter.Fill((NewAppDataSet.GetRepDataYearlyByEntityDataTable)MonthYearSalesRepDataSet.Tables[3], Entity);
            rv_Sales.RefreshReport();
        }

        private void LoadMISMonthRep_Items(string Entity)
        {
            rds_Items = null;
            rds_Items = new Microsoft.Reporting.WinForms.ReportDataSource();

            MonthYearItemsRepDataSet = null;
            MonthYearItemsRepDataSet = new NewAppDataSet();

            MonthYearItemsRepDataSet.BeginInit();

            MonthYearItemsRepDataSet.GetChanges();

            rds_Items.Name = "NewAppDataSet";
            rds_Items.Value = (DataTable)MonthYearItemsRepDataSet.Tables[2];


            rv_Items.Clear();
            rv_Items.LocalReport.DataSources.Add(rds_Items);
            rv_Items.LocalReport.ReportPath = "../../Reports/Rdlcs/MIS/MISItemsMonthRep.rdlc";

            MonthYearItemsRepDataSet.EndInit();

            //fill data into NewAppDataSet
            MonthRepItemsDataTableAdapter = null;
            MonthRepItemsDataTableAdapter = new NewAppDataSetTableAdapters.GetRepDataMonthlyByEntityTableAdapter();

            MonthRepItemsDataTableAdapter.ClearBeforeFill = true;
            MonthRepItemsDataTableAdapter.Fill((NewAppDataSet.GetRepDataMonthlyByEntityDataTable)MonthYearItemsRepDataSet.Tables[2], Entity);
            rv_Items.RefreshReport();
        }

        private void LoadMISYearRep_Items(string Entity)
        {
            rds_Items = null;
            rds_Items = new Microsoft.Reporting.WinForms.ReportDataSource();

            MonthYearItemsRepDataSet = null;
            MonthYearItemsRepDataSet = new NewAppDataSet();

            MonthYearItemsRepDataSet.BeginInit();

            MonthYearItemsRepDataSet.GetChanges();

            rds_Items.Name = "NewAppDataSet";
            rds_Items.Value = (DataTable)MonthYearItemsRepDataSet.Tables[3];


            rv_Items.Clear();
            rv_Items.LocalReport.DataSources.Add(rds_Items);
            rv_Items.LocalReport.ReportPath = "../../Reports/Rdlcs/MIS/MISItemsYearRep.rdlc";

            MonthYearItemsRepDataSet.EndInit();

            //fill data into NewAppDataSet
            YearRepItemsDataTableAdapter = null;
            YearRepItemsDataTableAdapter = new NewAppDataSetTableAdapters.GetRepDataYearlyByEntityTableAdapter();

            YearRepItemsDataTableAdapter.ClearBeforeFill = true;
            YearRepItemsDataTableAdapter.Fill((NewAppDataSet.GetRepDataYearlyByEntityDataTable)MonthYearItemsRepDataSet.Tables[3], Entity);
            rv_Items.RefreshReport();
        }

        private void LoadReport_Summary(string Option, int Value, int Year)
        {
            reportDataSource_Summary = null;
            reportDataSource_Summary = new Microsoft.Reporting.WinForms.ReportDataSource();

            StatisticsDataSet = null;
            StatisticsDataSet = new NewAppDataSet();

            StatisticsDataSet.BeginInit();

            reportDataSource_Summary.Name = "NewAppDataSet";
            reportDataSource_Summary.Value = (DataTable)StatisticsDataSet.Tables[1];


            _reportViewer_summary.LocalReport.DataSources.Clear();

            _reportViewer_summary.LocalReport.DataSources.Add(reportDataSource_Summary);
            _reportViewer_summary.LocalReport.ReportPath = "../../Reports/Rdlcs/MIS/MISStatistics.rdlc";

            StatisticsDataSet.EndInit();

            //fill data into NewAppDataSet
            StatisticsDataTableAdapter = null;
            StatisticsDataTableAdapter = new NewAppDataSetTableAdapters.GetStatisticsForReportTableAdapter();

            StatisticsDataTableAdapter.ClearBeforeFill = true;
            StatisticsDataTableAdapter.Fill((NewAppDataSet.GetStatisticsForReportDataTable)StatisticsDataSet.Tables[1], Option, Value, Year);
            _reportViewer_summary.RefreshReport();
        }

        private void cmb_StatisticsOptions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string option;

            if (_isreportLoaded == false)
                return;

            cmb_Value.Items.Clear();
            cmb_Value1.Items.Clear();
            tb_Value.Visibility = Visibility.Collapsed;
            cmb_Value.Visibility = Visibility.Collapsed;
            cmb_Value1.Visibility = Visibility.Collapsed;
            
            option = Convert.ToString(((ComboBoxItem)cmb_StatisticsOptions.SelectedItem).Tag);

            ComboBoxItem NewItem;

            if (option == "M")
            {
                NewItem = new ComboBoxItem();
                NewItem.Content = "January";
                NewItem.Tag = 0;
                cmb_Value.Items.Add(NewItem);

                NewItem = new ComboBoxItem();
                NewItem.Content = "February";
                NewItem.Tag = 1;
                cmb_Value.Items.Add(NewItem);

                NewItem = new ComboBoxItem();
                NewItem.Content = "March";
                NewItem.Tag = 2;
                cmb_Value.Items.Add(NewItem);

                NewItem = new ComboBoxItem();
                NewItem.Content = "April";
                NewItem.Tag = 3;
                cmb_Value.Items.Add(NewItem);

                NewItem = new ComboBoxItem();
                NewItem.Content = "May";
                NewItem.Tag = 4;
                cmb_Value.Items.Add(NewItem);

                NewItem = new ComboBoxItem();
                NewItem.Content = "June";
                NewItem.Tag = 5;
                cmb_Value.Items.Add(NewItem);

                NewItem = new ComboBoxItem();
                NewItem.Content = "July";
                NewItem.Tag = 6;
                cmb_Value.Items.Add(NewItem);

                NewItem = new ComboBoxItem();
                NewItem.Content = "August";
                NewItem.Tag = 7;
                cmb_Value.Items.Add(NewItem);

                NewItem = new ComboBoxItem();
                NewItem.Content = "September";
                NewItem.Tag = 8;
                cmb_Value.Items.Add(NewItem);

                NewItem = new ComboBoxItem();
                NewItem.Content = "October";
                NewItem.Tag = 9;
                cmb_Value.Items.Add(NewItem);

                NewItem = new ComboBoxItem();
                NewItem.Content = "November";
                NewItem.Tag = 10;
                cmb_Value.Items.Add(NewItem);

                NewItem = new ComboBoxItem();
                NewItem.Content = "December";
                NewItem.Tag = 11;
                cmb_Value.Items.Add(NewItem);

                NewItem = new ComboBoxItem();
                NewItem.Content = "2010";
                NewItem.Tag = 2010;
                cmb_Value1.Items.Add(NewItem);

                NewItem = new ComboBoxItem();
                NewItem.Content = "2011";
                NewItem.Tag = 2011;
                cmb_Value1.Items.Add(NewItem);

                NewItem = new ComboBoxItem();
                NewItem.Content = "2012";
                NewItem.Tag = 2012;
                cmb_Value1.Items.Add(NewItem);

                NewItem = new ComboBoxItem();
                NewItem.Content = "2013";
                NewItem.Tag = 2013;
                cmb_Value1.Items.Add(NewItem);

                NewItem = new ComboBoxItem();
                NewItem.Content = "2014";
                NewItem.Tag = 2014;
                cmb_Value1.Items.Add(NewItem);

                tb_Value.Visibility = Visibility.Visible;
                cmb_Value.Visibility = Visibility.Visible;
                cmb_Value1.Visibility = Visibility.Visible;

                cmb_Value.SelectedIndex = DateTime.Now.Month - 1;
                cmb_Value1.SelectedIndex = 3;
            }
            else if (option == "Y")
            {
                NewItem = new ComboBoxItem();
                NewItem.Content = "2010";
                NewItem.Tag = 2010;
                cmb_Value.Items.Add(NewItem);

                NewItem = new ComboBoxItem();
                NewItem.Content = "2011";
                NewItem.Tag = 2011;
                cmb_Value.Items.Add(NewItem);

                NewItem = new ComboBoxItem();
                NewItem.Content = "2012";
                NewItem.Tag = 2012;
                cmb_Value.Items.Add(NewItem);

                NewItem = new ComboBoxItem();
                NewItem.Content = "2013";
                NewItem.Tag = 2013;
                cmb_Value.Items.Add(NewItem);

                NewItem = new ComboBoxItem();
                NewItem.Content = "2014";
                NewItem.Tag = 2014;
                cmb_Value.Items.Add(NewItem);

                tb_Value.Visibility = Visibility.Visible;
                cmb_Value.Visibility = Visibility.Visible;
                cmb_Value.SelectedIndex = 3;
            }

            if (option == "D")
            {
                LoadReport_Summary(option, 0, 0);
            }
            else if ((option != "") && (cmb_Value.SelectedIndex >= 0))
            {
                if ((option == "M") && (cmb_Value1.SelectedIndex >= 0))
                    LoadReport_Summary(option, Convert.ToInt32(((ComboBoxItem)cmb_Value.SelectedItem).Tag) + 1, Convert.ToInt32(((ComboBoxItem)cmb_Value1.SelectedItem).Tag));
                else if (option == "Y")
                    LoadReport_Summary(option, Convert.ToInt32(((ComboBoxItem)cmb_Value.SelectedItem).Tag), 0);
            }
        }

        private void cmb_Value_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string option;

            if (_isreportLoaded == false)
                return;

            option = Convert.ToString(((ComboBoxItem)cmb_StatisticsOptions.SelectedItem).Tag);

            if ((option != "") && (cmb_Value.SelectedIndex >= 0))
            {
                if ((option == "M") && (cmb_Value1.SelectedIndex >= 0))
                    LoadReport_Summary(option, Convert.ToInt32(((ComboBoxItem)cmb_Value.SelectedItem).Tag) + 1, Convert.ToInt32(((ComboBoxItem)cmb_Value1.SelectedItem).Tag));
                else if (option == "Y")
                    LoadReport_Summary(option, Convert.ToInt32(((ComboBoxItem)cmb_Value.SelectedItem).Tag), 0);
            }
        }

        private void cmb_Value1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string option;

            if (_isreportLoaded == false)
                return;

            option = Convert.ToString(((ComboBoxItem)cmb_StatisticsOptions.SelectedItem).Tag);

            if ((option != "") && (cmb_Value.SelectedIndex >= 0))
            {
                if ((option == "M") && (cmb_Value1.SelectedIndex >= 0))
                    LoadReport_Summary(option, Convert.ToInt32(((ComboBoxItem)cmb_Value.SelectedItem).Tag) + 1, Convert.ToInt32(((ComboBoxItem)cmb_Value1.SelectedItem).Tag));
                else if (option == "Y")
                    LoadReport_Summary(option, Convert.ToInt32(((ComboBoxItem)cmb_Value.SelectedItem).Tag), 0);
            }
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            string option;

            if (_isreportLoaded == false)
                return;

            option = Convert.ToString(((ComboBoxItem)cmb_StatisticsOptions.SelectedItem).Tag);

            rv_Customers.LocalReport.DataSources.Remove(rds_Customer);
            string PeriodOption = Convert.ToString(((ComboBoxItem)cmb_CperiodOption.SelectedItem).Tag);

            if (PeriodOption == "M")
                LoadMISMonthRep_Customer("C");
            else if (PeriodOption == "Y")
                LoadMISYearRep_Customer("C");


            rv_Sales.LocalReport.DataSources.Remove(rds_Sales);
            PeriodOption = Convert.ToString(((ComboBoxItem)cmb_SperiodOption.SelectedItem).Tag);

            if (PeriodOption == "M")
                LoadMISMonthRep_Sales("S");
            else if (PeriodOption == "Y")
                LoadMISYearRep_Sales("S");

            rv_Items.LocalReport.DataSources.Remove(rds_Items);
            PeriodOption = Convert.ToString(((ComboBoxItem)cmb_IperiodOption.SelectedItem).Tag);

            if (PeriodOption == "M")
                LoadMISMonthRep_Items("I");
            else if (PeriodOption == "Y")
                LoadMISYearRep_Items("I");

            if (option == "D")
            {
                LoadReport_Summary(option, 0, 0);
            }
            else if ((option != "") && (cmb_Value.SelectedIndex >= 0))
            {
                if ((option == "M") && (cmb_Value1.SelectedIndex >= 0))
                    LoadReport_Summary(option, Convert.ToInt32(((ComboBoxItem)cmb_Value.SelectedItem).Tag) + 1, Convert.ToInt32(((ComboBoxItem)cmb_Value1.SelectedItem).Tag));
                else if (option == "Y")
                    LoadReport_Summary(option, Convert.ToInt32(((ComboBoxItem)cmb_Value.SelectedItem).Tag), 0);
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            string option;

            if (_isreportLoaded == false)
                return;

            _reportViewer_summary.LocalReport.DataSources.Remove(reportDataSource_Summary);

            option = Convert.ToString(((ComboBoxItem)cmb_StatisticsOptions.SelectedItem).Tag);

            if (option == "D")
            {
                LoadReport_Summary(option, 0, 0);
            }
            else if ((option != "") && (cmb_Value.SelectedIndex >= 0))
            {
                if ((option == "M") && (cmb_Value1.SelectedIndex >= 0))
                    LoadReport_Summary(option, Convert.ToInt32(((ComboBoxItem)cmb_Value.SelectedItem).Tag) + 1, Convert.ToInt32(((ComboBoxItem)cmb_Value1.SelectedItem).Tag));
                else if (option == "Y")
                    LoadReport_Summary(option, Convert.ToInt32(((ComboBoxItem)cmb_Value.SelectedItem).Tag), 0);
            }
        }

        private void cmb_SperiodOption_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_isreportLoaded == false)
                return;

            rv_Sales.LocalReport.DataSources.Remove(rds_Sales);

            string EntityOption = "S";
            string PeriodOption = Convert.ToString(((ComboBoxItem)cmb_SperiodOption.SelectedItem).Tag);

            if (PeriodOption == "M")
                LoadMISMonthRep_Sales(EntityOption);
            else if (PeriodOption == "Y")
                LoadMISYearRep_Sales(EntityOption);
        }

        private void cmb_CperiodOption_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_isreportLoaded == false)
                return;

            rv_Customers.LocalReport.DataSources.Remove(rds_Customer);

            string EntityOption = "C";
            string PeriodOption = Convert.ToString(((ComboBoxItem)cmb_CperiodOption.SelectedItem).Tag);

            if (PeriodOption == "M")
                LoadMISMonthRep_Customer(EntityOption);
            else if (PeriodOption == "Y")
                LoadMISYearRep_Customer(EntityOption);
        }

        private void cmb_IperiodOption_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_isreportLoaded == false)
                return;

            rv_Items.LocalReport.DataSources.Remove(rds_Items);

            string EntityOption = "I";
            string PeriodOption = Convert.ToString(((ComboBoxItem)cmb_IperiodOption.SelectedItem).Tag);

            if (PeriodOption == "M")
                LoadMISMonthRep_Items(EntityOption);
            else if (PeriodOption == "Y")
                LoadMISYearRep_Items(EntityOption);
        }
    }
}
