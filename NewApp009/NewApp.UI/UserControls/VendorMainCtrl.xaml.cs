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
using NewApp.UI.UserControls;
using System.IO;
using System.Drawing;
using NewApp.BusinessTier.Common;
using NewApp.BusinessTier.Models;
using NewApp.UI.Windows;
using NewApp.VendorFactorySvc;
using NewApp.VendorStatusFactorySvc;
using NewApp.GenderTypeFactorySvc;
using NewApp.IDProofTypeFactorySvc;


namespace NewApp.UI.UserControls
{
    /// <summary>
    /// Interaction logic for SalesMainCtrl.xaml
    /// </summary>
    public partial class VendorMainCtrl : UserControl
    {
        List<Vendor> VendorObjList;

        public static readonly RoutedEvent VendorMainWithCustomArgsEvent = EventManager.RegisterRoutedEvent("VendorMainWithCustomArgs", RoutingStrategy.Bubble, typeof(VendorMainWithCustomArgsEventHandler), typeof(VendorMainCtrl));
        public delegate void VendorMainWithCustomArgsEventHandler(object sender, ProcessEventArgs e);


        public delegate void ProcessHandler(object sender, ProcessEventArgs data);
        string _mode;
        string _invokingCtrlMode;
        string _invokingCtrlName;
        string _CtrlTabName;

        UserCtrlArgs UCArgs;

        public VendorMainCtrl()
        {
            InitializeComponent();

            uc_VendorDetails.Visibility = Visibility.Collapsed;
        }
        
        public string Mode
        {
            get{return _mode;}
            set
            {
                _mode = value;

                if (_mode == "S")
                    btn_SelVendor.Visibility = Visibility.Visible;
                else
                    btn_SelVendor.Visibility = Visibility.Collapsed;
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

        public event VendorMainWithCustomArgsEventHandler VendorMainWithCustomArgs
        {
            add { AddHandler(VendorMainWithCustomArgsEvent, value); }
            remove { RemoveHandler(VendorMainWithCustomArgsEvent, value); }
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
            ProcessEventArgs myargs = new ProcessEventArgs(VendorMainWithCustomArgsEvent, UCArgs.InvokingControlName, UCArgs.InvokingControlMode, UCArgs.TargetControlName, UCArgs.TargetControlMode, null, null, UCArgs.ControlTabName);
            RaiseEvent(myargs);
        }

        private void txt_VendorNameId_TextChanged(object sender, TextChangedEventArgs e)
        {
            dg_VendorMain.ItemsSource = "";
            if (txt_VendorNameId.Text != "")
            {
                SearchVendors(txt_VendorNameId.Text);
                if (dg_VendorMain.Items.Count > 0)
                {
                    dg_VendorMain.SelectedIndex = 0;
                    if ((VendorObjList != null) && (VendorObjList.Count > 0) && (dg_VendorMain.SelectedIndex > -1))
                    {
                        this.uc_VendorDetails.ClearControls();
                        this.uc_VendorDetails.DisplayVendor((Vendor)VendorObjList[dg_VendorMain.SelectedIndex]);
                        this.uc_VendorDetails.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        this.uc_VendorDetails.ClearControls();
                        this.uc_VendorDetails.Visibility = Visibility.Collapsed;
                    }
                }
            }
            else
            {
                dg_VendorMain.ItemsSource = "";
            }

            this.Focus();
        }

        public void SearchVendors(string Str)
        {
            string errorMessage = null;

            Vendor Vendor = new Vendor();
            VendorObjList = new List<Vendor>();
            Vendor[] VendorObjArray;
            VendorFactoryClient VendorFactoryClient = new VendorFactoryClient();

            if (VendorFactoryClient.GetVendors(out errorMessage, out VendorObjArray, Str) == true)
            {
                if (VendorObjArray != null)
                {
                    VendorObjList = VendorObjArray.ToList<Vendor>();
                    this.DataContext = VendorObjList;
                    if (VendorObjList.Count > 0)
                        dg_VendorMain.ItemsSource = VendorObjList;
                    else
                        dg_VendorMain.ItemsSource = "";
                }
            }
            else
            {
                if ((errorMessage != "") && (errorMessage != null))
                {
                    WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Search Vendors", 0, "ERROR");
                    MsgBox.ShowDialog();
                }
            }
        }

        private void btn_AddVendor_Click(object sender, RoutedEventArgs e)
        {
            UCArgs = new UserCtrlArgs();
            UCArgs.InvokingControlName = this.Name;
            UCArgs.InvokingControlMode = this.Mode;
            UCArgs.TargetControlName = "MyVendorDtlInputCtrl";
            UCArgs.TargetControlMode = "A";
            UCArgs.ControlTabName = this.ControlTabName;

            ProcessEventArgs myargs = new ProcessEventArgs(VendorMainWithCustomArgsEvent, UCArgs.InvokingControlName, UCArgs.InvokingControlMode, UCArgs.TargetControlName, UCArgs.TargetControlMode, null, null, UCArgs.ControlTabName);
            RaiseEvent(myargs);
        }

        private void btn_ModifyVendor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dg_VendorMain.SelectedIndex > -1)
                {
                    if (this.Parent != null && this.Parent is StackPanel)
                    {
                        StackPanel parentControl = this.Parent as StackPanel;
                        foreach (UIElement child in parentControl.Children)
                        {
                            if (child is VendorDtlInputCtrl)
                            {
                                UserCtrlArgs Args = new UserCtrlArgs();
                                Args.TargetControlName = "MyVendorDtlInputCtrl";
                                Args.TargetControlMode = "U";

                                Args.InvokingControlName = this.Name;
                                Args.InvokingControlMode = this.Mode;

                                Vendor Vendor = (Vendor)VendorObjList[dg_VendorMain.SelectedIndex];
                                ((VendorDtlInputCtrl)child).ClearControls();
                                ((VendorDtlInputCtrl)child).DisplayVendor(Vendor);

                                //this.NotifyedUserControl(this, Args);
                            }
                        }
                    }
                }
            }
            catch (Exception ex1)
            {
                WinMessageBox MsgBox = new WinMessageBox(ex1.Message.ToString(), "Vendor Main - Modify", 0, "ERROR");
                MsgBox.ShowDialog();
            }
        }

        private void btn_DeleteVendor_Click(object sender, RoutedEventArgs e)
        {
            string errorMessage = null;

            try
            {
                if (dg_VendorMain.SelectedIndex > -1)
                {
                    Vendor VendorObj = (Vendor)VendorObjList[dg_VendorMain.SelectedIndex];

                    VendorFactoryClient VendorFactoryClient = new VendorFactoryClient();

                    if (VendorFactoryClient.DeleteVendorDetails(out errorMessage, VendorObj) == true)
                    {
                        WinMessageBox MsgBox = new WinMessageBox("New Vendor Record Deleted Successfully!", "Vendor Main - Delete", 0, "SUCCESS");
                        MsgBox.ShowDialog();

                        SearchVendors(txt_VendorNameId.Text);
                    }
                    else
                    {
                        WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Vendor Main - Delete", 0, "ERROR");
                        MsgBox.ShowDialog();
                        return;
                    }
                }
            }
            catch (Exception ex1)
            {
                WinMessageBox MsgBox = new WinMessageBox(ex1.Message.ToString(), "Vendor Main - Delete", 0, "ERROR");
                MsgBox.ShowDialog();
            }
        }

        private void btn_SelVendor_Click(object sender, RoutedEventArgs e)
        {
            string errorMessage = null;

            if (dg_VendorMain.SelectedIndex > -1)
            {
                Vendor VendorObj = new Vendor();

                if (ApplicationState.GetValue<Vendor>("VendorObj", out VendorObj, out errorMessage) == true)
                    ApplicationState.DeleteValue("VendorObj");

                VendorObj = (Vendor)dg_VendorMain.SelectedItem;

                ApplicationState.SetValue("VendorObj", VendorObj);
            }
        }

        private void dg_VendorMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((VendorObjList != null) && (VendorObjList.Count > 0) && (dg_VendorMain.SelectedIndex > -1))
            {
                this.uc_VendorDetails.Mode = "V";

                uc_VendorDetails.dg_TxnResults.ItemsSource = null;
                uc_VendorDetails.dg_TxnResults.Items.Clear();

                Vendor SelectedVendor = new Vendor();
                SelectedVendor = (Vendor)VendorObjList[dg_VendorMain.SelectedIndex];
                this.uc_VendorDetails.ClearControls();
                this.uc_VendorDetails.DisplayVendor(SelectedVendor);

                #region Search Vendors

                    string errorMessage = null;

                    Vendor VendorObj = new Vendor();

                    VendorFactoryClient VendorFactoryClient = new VendorFactoryClient();

                    DateTime dt = DateTime.Now;

                    uc_VendorDetails.dg_TxnResults.Columns.Clear();

                    if (VendorFactoryClient.GetVendorDetails(out errorMessage, out VendorObj, SelectedVendor.VendorId) == true)
                    {
                        if (VendorObj.SalesDetails.Count > 0)
                        {
                            uc_VendorDetails.dg_TxnResults.ItemsSource = VendorObj.SalesDetails;

                            DataGridTextColumn NewColumn;

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Sale Id";
                            NewColumn.Binding = new Binding("SalesId");
                            NewColumn.Width = 65;
                            uc_VendorDetails.dg_TxnResults.Columns.Add(NewColumn);

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Sale Date";
                            NewColumn.Binding = new Binding("SaleDate");
                            NewColumn.Width = 125;
                            NewColumn.Binding.StringFormat = "{0:f}";
                            uc_VendorDetails.dg_TxnResults.Columns.Add(NewColumn);

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Total Amt";
                            NewColumn.Binding = new Binding("TotalSaleAmount");
                            NewColumn.Binding.StringFormat = "{0:C2}";
                            NewColumn.Width = 90;
                            uc_VendorDetails.dg_TxnResults.Columns.Add(NewColumn);

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Discount";
                            NewColumn.Binding = new Binding("TotalDiscountAmount");
                            NewColumn.Binding.StringFormat = "{0:C2}";
                            NewColumn.Width = 85;
                            uc_VendorDetails.dg_TxnResults.Columns.Add(NewColumn);

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Tax";
                            NewColumn.Binding = new Binding("TotalTaxAmount");
                            NewColumn.Binding.StringFormat = "{0:C2}";
                            NewColumn.Width = 90;
                            uc_VendorDetails.dg_TxnResults.Columns.Add(NewColumn);

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Final Amt";
                            NewColumn.Binding = new Binding("FinalSaleAmount");
                            NewColumn.Binding.StringFormat = "{0:C2}";
                            NewColumn.Width = 90;
                            uc_VendorDetails.dg_TxnResults.Columns.Add(NewColumn);

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Amt Paid";
                            NewColumn.Binding = new Binding("PaidAmount");
                            NewColumn.Binding.StringFormat = "{0:C2}";
                            NewColumn.Width = 90;
                            uc_VendorDetails.dg_TxnResults.Columns.Add(NewColumn);

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Balance";
                            NewColumn.Binding = new Binding("BalanceAmount");
                            NewColumn.Binding.StringFormat = "{0:C2}";
                            NewColumn.Width = 90;
                            uc_VendorDetails.dg_TxnResults.Columns.Add(NewColumn);
                        }
                    }
                    else
                        if ((errorMessage != "") && (errorMessage != null))
                        {
                            WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Search Vendors", 0, "ERROR");
                            MsgBox.ShowDialog();
                        }
                #endregion



                this.uc_VendorDetails.Visibility = Visibility.Visible;
            }
            else
            {
                this.uc_VendorDetails.ClearControls();
                this.uc_VendorDetails.Visibility = Visibility.Collapsed;
            }
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.Visibility == Visibility.Visible)
            {
                if (txt_VendorNameId.Text != "")
                    SearchVendors(txt_VendorNameId.Text);
                else
                    dg_VendorMain.ItemsSource = "";
            }
        }

        private void dg_VendorMain_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (dg_VendorMain.SelectedIndex > -1)
                if (VendorObjList != null)
                    if ((VendorObjList.Count > 0) && (dg_VendorMain.SelectedIndex > -1))
                    {
                        this.uc_VendorDetails.ClearControls();
                        this.uc_VendorDetails.DisplayVendor((Vendor)VendorObjList[dg_VendorMain.SelectedIndex]);
                        this.uc_VendorDetails.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        this.uc_VendorDetails.ClearControls();
                        this.uc_VendorDetails.Visibility = Visibility.Collapsed;
                    }

        }

        private void uc_VendorDetails_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
