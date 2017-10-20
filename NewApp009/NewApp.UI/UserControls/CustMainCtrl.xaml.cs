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
using NewApp.UI;
using NewApp.UI.UserControls;
using System.IO;
using System.Drawing;
using NewApp.BusinessTier.Common;
using NewApp.BusinessTier.Models;
using NewApp.UI.Windows;
using NewApp.GenderTypeFactorySvc;
using NewApp.IDProofTypeFactorySvc;
using NewApp.CustomerFactorySvc;
using NewApp.DiscountFactorySvc;
using NewApp.DiscountItemFactorySvc;
using NewApp.ManufFactorySvc;
using NewApp.SaleFactorySvc;
using NewApp.SaleItemFactorySvc;
using NewApp.StoreItemFactorySvc;
using NewApp.TaxFactorySvc;
using NewApp.TaxItemFactorySvc;
using NewApp.VendorFactorySvc;


namespace NewApp.UI.UserControls
{
    /// <summary>
    /// Interaction logic for SalesMainCtrl.xaml
    /// </summary>
    public partial class CustMainCtrl : UserControl
    {
        List<Customer> CustObjList;

        public static readonly RoutedEvent CustMainWithCustomArgsEvent = EventManager.RegisterRoutedEvent("CustMainWithCustomArgs", RoutingStrategy.Bubble, typeof(CustMainWithCustomArgsEventHandler), typeof(CustMainCtrl));
        public delegate void CustMainWithCustomArgsEventHandler(object sender, ProcessEventArgs e);


        public delegate void ProcessHandler(object sender, ProcessEventArgs data);
        string _mode;
        string _invokingCtrlMode;
        string _invokingCtrlName;
        string _CtrlTabName;

        UserCtrlArgs UCArgs;

        public CustMainCtrl()
        {
            InitializeComponent();

            uc_CustDetails.Visibility = Visibility.Collapsed;
        }
        
        public string Mode
        {
            get{return _mode;}
            set
            {
                _mode = value;

                if (_mode == "S")
                    btn_SelCust.Visibility = Visibility.Visible;
                else
                    btn_SelCust.Visibility = Visibility.Collapsed;
            }
        }

        public string InvokingControlMode
        {
            get{return _invokingCtrlMode;}
            set{_invokingCtrlMode = value;}
        }

        public string InvokingControlName
        {
            get{return _invokingCtrlName;}
            set{_invokingCtrlName = value;}
        }

        public string ControlTabName
        {
            get { return _CtrlTabName; }
            set { _CtrlTabName = value; }
        }

        public event CustMainWithCustomArgsEventHandler CustMainWithCustomArgs
        {
            add { AddHandler(CustMainWithCustomArgsEvent, value); }
            remove { RemoveHandler(CustMainWithCustomArgsEvent, value); }
        }
       
        private void Btn_Close_Click(object sender, RoutedEventArgs e)
        {
            UCArgs = new UserCtrlArgs();
            UCArgs.InvokingControlName = this.Name;
            UCArgs.InvokingControlMode = this.Mode;
            UCArgs.TargetControlName = this.InvokingControlName;
            UCArgs.TargetControlMode = this.InvokingControlMode;
            UCArgs.ControlTabName = this.ControlTabName;

            e.Handled = true;
            ProcessEventArgs myargs = new ProcessEventArgs(CustMainWithCustomArgsEvent, UCArgs.InvokingControlName, UCArgs.InvokingControlMode, UCArgs.TargetControlName, UCArgs.TargetControlMode, null, null, UCArgs.ControlTabName);
            RaiseEvent(myargs);
        }

        private void txt_CustNameId_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                dg_CustMain.ItemsSource = "";
                if (txt_CustNameId.Text != "")
                {
                    SearchCustomers(txt_CustNameId.Text);
                    if (dg_CustMain.Items.Count > 0)
                    {
                        dg_CustMain.SelectedIndex = 0;
                        if ((CustObjList != null) && (CustObjList.Count > 0) && (dg_CustMain.SelectedIndex > -1))
                        {
                            this.uc_CustDetails.ClearControls();
                            this.uc_CustDetails.DisplayCustomer((Customer)CustObjList[dg_CustMain.SelectedIndex]);
                            this.uc_CustDetails.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            this.uc_CustDetails.ClearControls();
                            this.uc_CustDetails.Visibility = Visibility.Collapsed;
                        }
                    }
                }
                else
                {
                    dg_CustMain.ItemsSource = "";
                }

                this.Focus();
            }
            catch (Exception ex1)
            {
                WinMessageBox MsgBox = new WinMessageBox(ex1.Message.ToString(), "Customer Main", 0, "ERROR");
                MsgBox.ShowDialog();
            }
        }

        public void SearchCustomers(string CustomerIdOrName)
        {
            string errorMessage = null;

            try
            {
                Customer[] CustObjArray;
                CustObjList = new List<Customer>();

                CustomerFactoryClient CustomerFactoryClient = new CustomerFactoryClient();

                if (CustomerFactoryClient.GetCustomers(out errorMessage, out CustObjArray, CustomerIdOrName) == true)
                {
                    if (CustObjArray != null)
                    {
                        CustObjList = CustObjArray.ToList<Customer>();
                        this.DataContext = CustObjList;
                        if (CustObjList.Count > 0)
                            dg_CustMain.ItemsSource = CustObjList;
                        else
                            dg_CustMain.ItemsSource = "";
                    }
                }
                else
                {
                    if ((errorMessage != "") && (errorMessage != null))
                    {
                        WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Customer Main - Search", 0, "ERROR");
                        MsgBox.ShowDialog();
                        return;
                    }
                }
            }
            catch (Exception ex1)
            {
                WinMessageBox MsgBox = new WinMessageBox(ex1.Message.ToString(), "Customer Main - Search", 0, "ERROR");
                MsgBox.ShowDialog();
            }
        }

        private void btn_AddCust_Click(object sender, RoutedEventArgs e)
        {
            UCArgs = new UserCtrlArgs();
            UCArgs.InvokingControlName = this.Name;
            UCArgs.InvokingControlMode = this.Mode;
            UCArgs.TargetControlName = "MyCustDtlInputCtrl";
            UCArgs.TargetControlMode = "A";
            UCArgs.ControlTabName = this.ControlTabName;

            ProcessEventArgs myargs = new ProcessEventArgs(CustMainWithCustomArgsEvent, UCArgs.InvokingControlName, UCArgs.InvokingControlMode, UCArgs.TargetControlName, UCArgs.TargetControlMode, null, null, UCArgs.ControlTabName);
            RaiseEvent(myargs);
        }

        private void btn_ModifyCust_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dg_CustMain.SelectedIndex > -1)
                {
                    if (this.Parent != null && this.Parent is StackPanel)
                    {
                        StackPanel parentControl = this.Parent as StackPanel;
                        foreach (UIElement child in parentControl.Children)
                        {
                            if (child is CustDtlInputCtrl)
                            {
                                UserCtrlArgs Args = new UserCtrlArgs();
                                Args.TargetControlName = "CustDtlInputCtrl";
                                Args.TargetControlMode = "U";

                                Args.InvokingControlName = this.Name;
                                Args.InvokingControlMode = this.Mode;

                                Customer Customer = (Customer)CustObjList[dg_CustMain.SelectedIndex];
                                ((CustDtlInputCtrl)child).ClearControls();
                                ((CustDtlInputCtrl)child).DisplayCustomer(Customer);

                                //this.NotifyedUserControl(this, Args);
                            }
                        }
                    }
                }
            }
            catch (Exception ex1)
            {
                WinMessageBox MsgBox = new WinMessageBox(ex1.Message.ToString(), "Customer Main - Modify", 0, "ERROR");
                MsgBox.ShowDialog();
            }
        }

        private void btn_DeleteCust_Click(object sender, RoutedEventArgs e)
        {
            string errorMessage = null;

            try
            {
                if (dg_CustMain.SelectedIndex > -1)
                {
                    Customer CustObj = (Customer)CustObjList[dg_CustMain.SelectedIndex];

                    CustomerFactoryClient CustomerFactoryClient = new CustomerFactoryClient();

                    if (CustomerFactoryClient.DeleteCustomerDetails(out errorMessage, CustObj) == true)
                    {
                        WinMessageBox MsgBox = new WinMessageBox("New Customer Record Deleted Successfully!", "Customer Main - Delete", 0, "SUCCESS");
                        MsgBox.ShowDialog();

                        SearchCustomers(txt_CustNameId.Text);
                    }
                    else
                    {
                        WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Customer Main - Delete", 0, "ERROR");
                        MsgBox.ShowDialog();
                        return;
                    }
                }
            }
            catch (Exception ex1)
            {
                WinMessageBox MsgBox = new WinMessageBox(ex1.Message.ToString(), "Customer Main - Delete", 0, "ERROR");
                MsgBox.ShowDialog();
            }
        }

        private void btn_SelCust_Click(object sender, RoutedEventArgs e)
        {
            string errorMessage = null;

            try
            {
                if (dg_CustMain.SelectedIndex > -1)
                {
                    Customer CustObj = new Customer();

                    if (ApplicationState.GetValue<Customer>("CustObj", out CustObj, out errorMessage) == true)
                        ApplicationState.DeleteValue("CustObj");

                    CustObj = (Customer)dg_CustMain.SelectedItem;

                    ApplicationState.SetValue("CustObj", CustObj);

                    Sale NewSaleObj;

                    if (ApplicationState.GetValue<Sale>("NewSaleObj", out NewSaleObj, out errorMessage) == true)
                    {
                        NewSaleObj.CustomerInfo = CustObj;

                        UCArgs = new UserCtrlArgs();
                        UCArgs.InvokingControlName = this.Name;
                        UCArgs.InvokingControlMode = this.Mode;
                        UCArgs.TargetControlName = "MySaleMainCtrl";
                        UCArgs.TargetControlMode = "A";
                        UCArgs.ControlTabName = this.ControlTabName;

                        ProcessEventArgs myargs = new ProcessEventArgs(CustMainWithCustomArgsEvent, UCArgs.InvokingControlName, UCArgs.InvokingControlMode, UCArgs.TargetControlName, UCArgs.TargetControlMode, null, null, UCArgs.ControlTabName);
                        RaiseEvent(myargs);
                    }
                    else if ((errorMessage != "") && (errorMessage != null))
                    {
                        WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Customer Main - Select", 0, "ERROR");
                        MsgBox.ShowDialog();
                        return;
                    }
                }
            }
            catch (Exception ex1)
            {
                WinMessageBox MsgBox = new WinMessageBox(ex1.Message.ToString(), "Customer Main - Select", 0, "ERROR");
                MsgBox.ShowDialog();
            }
        }

        private void dg_CustMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string errorMessage = null;

            if ((CustObjList != null) && (CustObjList.Count > 0) && (dg_CustMain.SelectedIndex > -1))
            {
                try
                {
                    this.uc_CustDetails.Mode = "V";

                    uc_CustDetails.dg_TxnResults.ItemsSource = null;
                    uc_CustDetails.dg_TxnResults.Items.Clear();

                    Customer SelectedCustomer = new Customer();
                    SelectedCustomer = (Customer)CustObjList[dg_CustMain.SelectedIndex];
                    this.uc_CustDetails.ClearControls();
                    this.uc_CustDetails.DisplayCustomer(SelectedCustomer);

                    uc_CustDetails.dg_TxnResults.Columns.Clear();

                    SaleFactoryClient SaleFactoryClient = new SaleFactoryClient();
                    Sale[] SaleObjArray;

                    if (SaleFactoryClient.GetSaleDetails(out SaleObjArray, out errorMessage, null, SelectedCustomer.CustomerId, null, DateTime.MinValue, DateTime.MinValue) == true)
                    {
                        if (SaleObjArray != null)
                        {
                            SelectedCustomer.SalesDetails = SaleObjArray.ToList<Sale>();

                            uc_CustDetails.dg_TxnResults.ItemsSource = SelectedCustomer.SalesDetails;

                            DataGridTextColumn NewColumn;

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Sale Id";
                            NewColumn.Binding = new Binding("SalesId");
                            NewColumn.Width = 65;
                            uc_CustDetails.dg_TxnResults.Columns.Add(NewColumn);

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Sale Date";
                            NewColumn.Binding = new Binding("SaleDate");
                            NewColumn.Width = 125;
                            NewColumn.Binding.StringFormat = "{0:f}";
                            uc_CustDetails.dg_TxnResults.Columns.Add(NewColumn);

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Total Amt";
                            NewColumn.Binding = new Binding("TotalSaleAmount");
                            NewColumn.Binding.StringFormat = "{0:C2}";
                            NewColumn.Width = 90;
                            uc_CustDetails.dg_TxnResults.Columns.Add(NewColumn);

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Discount";
                            NewColumn.Binding = new Binding("TotalDiscountAmount");
                            NewColumn.Binding.StringFormat = "{0:C2}";
                            NewColumn.Width = 85;
                            uc_CustDetails.dg_TxnResults.Columns.Add(NewColumn);

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Tax";
                            NewColumn.Binding = new Binding("TotalTaxAmount");
                            NewColumn.Binding.StringFormat = "{0:C2}";
                            NewColumn.Width = 90;
                            uc_CustDetails.dg_TxnResults.Columns.Add(NewColumn);

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Final Amt";
                            NewColumn.Binding = new Binding("FinalSaleAmount");
                            NewColumn.Binding.StringFormat = "{0:C2}";
                            NewColumn.Width = 90;
                            uc_CustDetails.dg_TxnResults.Columns.Add(NewColumn);

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Amt Paid";
                            NewColumn.Binding = new Binding("PaidAmount");
                            NewColumn.Binding.StringFormat = "{0:C2}";
                            NewColumn.Width = 90;
                            uc_CustDetails.dg_TxnResults.Columns.Add(NewColumn);

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Balance";
                            NewColumn.Binding = new Binding("BalanceAmount");
                            NewColumn.Binding.StringFormat = "{0:C2}";
                            NewColumn.Width = 90;
                            uc_CustDetails.dg_TxnResults.Columns.Add(NewColumn);
                        }
                    }
                    else
                        if ((errorMessage != "") && (errorMessage != null))
                        {
                            WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Customer Main", 0, "ERROR");
                            MsgBox.ShowDialog();
                            return;
                        }

                    this.uc_CustDetails.Visibility = Visibility.Visible;
                }
                catch (Exception ex1)
                {
                    WinMessageBox MsgBox = new WinMessageBox(ex1.Message.ToString(), "Customer Main", 0, "ERROR");
                    MsgBox.ShowDialog();
                }
            }
            else
            {
                this.uc_CustDetails.ClearControls();
                this.uc_CustDetails.Visibility = Visibility.Collapsed;
            }
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.Visibility == Visibility.Visible)
            {
                if (txt_CustNameId.Text != "")
                    SearchCustomers(txt_CustNameId.Text);
                else
                    dg_CustMain.ItemsSource = "";
            }
        }

        private void dg_CustMain_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (dg_CustMain.SelectedIndex > -1)
                    if (CustObjList != null)
                        if ((CustObjList.Count > 0) && (dg_CustMain.SelectedIndex > -1))
                        {
                            this.uc_CustDetails.ClearControls();
                            this.uc_CustDetails.DisplayCustomer((Customer)CustObjList[dg_CustMain.SelectedIndex]);
                            this.uc_CustDetails.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            this.uc_CustDetails.ClearControls();
                            this.uc_CustDetails.Visibility = Visibility.Collapsed;
                        }
            }
            catch (Exception ex1)
            {
                WinMessageBox MsgBox = new WinMessageBox(ex1.Message.ToString(), "Customer Main", 0, "ERROR");
                MsgBox.ShowDialog();
            }

        }
    }
}
