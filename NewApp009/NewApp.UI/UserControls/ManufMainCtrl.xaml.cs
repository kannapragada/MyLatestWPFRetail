using NewApp.BusinessTier.Models;
using NewApp.ManufFactorySvc;
using NewApp.UI.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace NewApp.UI.UserControls
{
    /// <summary>
    /// Interaction logic for SalesMainCtrl.xaml
    /// </summary>
    public partial class ManufMainCtrl : UserControl
    {
        List<Manufacturer> ManufObjList;

        public static readonly RoutedEvent ManufMainWithCustomArgsEvent = EventManager.RegisterRoutedEvent("ManufMainWithCustomArgs", RoutingStrategy.Bubble, typeof(ManufMainWithCustomArgsEventHandler), typeof(ManufMainCtrl));
        public delegate void ManufMainWithCustomArgsEventHandler(object sender, ProcessEventArgs e);


        public delegate void ProcessHandler(object sender, ProcessEventArgs data);
        string _mode;
        string _invokingCtrlMode;
        string _invokingCtrlName;
        string _CtrlTabName;

        UserCtrlArgs UCArgs;

        public ManufMainCtrl()
        {
            InitializeComponent();

            uc_ManufDetails.Visibility = Visibility.Collapsed;
        }

        public string Mode
        {
            get{return _mode;}
            set
            {
                _mode = value;

                if (_mode == "S")
                    btn_SelManuf.Visibility = Visibility.Visible;
                else
                    btn_SelManuf.Visibility = Visibility.Collapsed;
            }
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

        public event ManufMainWithCustomArgsEventHandler ManufMainWithCustomArgs
        {
            add { AddHandler(ManufMainWithCustomArgsEvent, value); }
            remove { RemoveHandler(ManufMainWithCustomArgsEvent, value); }
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
            ProcessEventArgs myargs = new ProcessEventArgs(ManufMainWithCustomArgsEvent, UCArgs.InvokingControlName, UCArgs.InvokingControlMode, UCArgs.TargetControlName, UCArgs.TargetControlMode, null, null, UCArgs.ControlTabName);
            RaiseEvent(myargs);
        }

        private void txt_ManufNameId_TextChanged(object sender, TextChangedEventArgs e)
        {
            dg_ManufMain.ItemsSource = "";
            if (txt_ManufNameId.Text != "")
            {
                SearchManufacturers(txt_ManufNameId.Text);
                if (dg_ManufMain.Items.Count > 0)
                {
                    dg_ManufMain.SelectedIndex = 0;
                    if ((ManufObjList != null) && (ManufObjList.Count > 0) && (dg_ManufMain.SelectedIndex > -1))
                    {
                        this.uc_ManufDetails.ClearControls();
                        this.uc_ManufDetails.DisplayManufacturer((Manufacturer)ManufObjList[dg_ManufMain.SelectedIndex]);
                        this.uc_ManufDetails.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        this.uc_ManufDetails.ClearControls();
                        this.uc_ManufDetails.Visibility = Visibility.Collapsed;
                    }
                }
            }
            else
            {
                dg_ManufMain.ItemsSource = "";
            }

            this.Focus();
        }

        public void SearchManufacturers(string Str)
        {
            string errorMessage = null;

            ManufacturerFactoryClient ManufFactoryClient = new ManufacturerFactoryClient();
            Manufacturer[] ManufObjArray;
            ManufObjList = new List<Manufacturer>();

            if (ManufFactoryClient.GetManufacturers(out errorMessage, out ManufObjArray, Str) == true)
            {
                if (ManufObjArray != null)
                {
                    ManufObjList = ManufObjArray.ToList<Manufacturer>();
                    this.DataContext = ManufObjList;
                    if (ManufObjList.Count > 0)
                        dg_ManufMain.ItemsSource = ManufObjList;
                    else
                        dg_ManufMain.ItemsSource = "";
                }
            }
            else
            {
                if ((errorMessage != "") && (errorMessage != null))
                {
                    WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Search Manufacturers", 0, "ERROR");
                    MsgBox.ShowDialog();
                    return;
                }
            }
        }

        private void btn_AddManufacturer_Click(object sender, RoutedEventArgs e)
        {
            UCArgs = new UserCtrlArgs();
            UCArgs.InvokingControlName = this.Name;
            UCArgs.InvokingControlMode = this.Mode;
            UCArgs.TargetControlName = "MyManufDtlInputCtrl";
            UCArgs.TargetControlMode = "A";
            UCArgs.ControlTabName = this.ControlTabName;

            ProcessEventArgs myargs = new ProcessEventArgs(ManufMainWithCustomArgsEvent, UCArgs.InvokingControlName, UCArgs.InvokingControlMode, UCArgs.TargetControlName, UCArgs.TargetControlMode, null, null, UCArgs.ControlTabName);
            RaiseEvent(myargs);
        }

        private void btn_ModifyManufacturer_Click(object sender, RoutedEventArgs e)
        {
            WinMessageBox MsgBox = new WinMessageBox("Functionality Yet to be Added", "Modify Tax", 0, "INFORMATION");
            MsgBox.ShowDialog();
        }

        private void btn_DeleteManufacturer_Click(object sender, RoutedEventArgs e)
        {
            string errorMessage = null;

            if (dg_ManufMain.SelectedIndex > -1)
            {
                Manufacturer ManufObj = (Manufacturer)ManufObjList[dg_ManufMain.SelectedIndex];

                ManufacturerFactoryClient ManufFactoryClient = new ManufacturerFactoryClient();

                if (ManufFactoryClient.DeleteManufacturerDetails(out errorMessage, ManufObj) == true)
                {
                    WinMessageBox MsgBox = new WinMessageBox("Manufacturer Record Deleted Successfully!", "Delete Manufacturer", 0, "SUCCESS");
                    MsgBox.ShowDialog();

                    SearchManufacturers(txt_ManufNameId.Text);
                }
                else
                {
                    WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Delete Manufacturer", 0, "ERROR");
                    MsgBox.ShowDialog();
                    return;
                }
            }
        }

        private void btn_SelManufacturer_Click(object sender, RoutedEventArgs e)
        {
            string errorMessage = null;

            if (dg_ManufMain.SelectedIndex > -1)
            {
                Manufacturer ManufObj = new Manufacturer();

                if (ApplicationState.GetValue<Manufacturer>("ManufObj", out ManufObj, out errorMessage) == true)
                    ApplicationState.DeleteValue("ManufObj");

                ManufObj = (Manufacturer)dg_ManufMain.SelectedItem;

                ApplicationState.SetValue("ManufObj", ManufObj);
            }
        }

        private void dg_ManufMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((ManufObjList != null) && (ManufObjList.Count > 0) && (dg_ManufMain.SelectedIndex > -1))
            {
                this.uc_ManufDetails.Mode = "V";

                uc_ManufDetails.dg_TxnResults.ItemsSource = null;
                uc_ManufDetails.dg_TxnResults.Items.Clear();

                Manufacturer SelectedManufacturer = new Manufacturer();
                SelectedManufacturer = (Manufacturer)ManufObjList[dg_ManufMain.SelectedIndex];
                this.uc_ManufDetails.ClearControls();
                this.uc_ManufDetails.DisplayManufacturer(SelectedManufacturer);

                #region Search Manufacturers

                    string errorMessage = null;

                    Manufacturer ManufObj = new Manufacturer();
                    ManufacturerFactoryClient ManufFactoryClient = new ManufacturerFactoryClient();

                    DateTime dt = DateTime.Now;

                    uc_ManufDetails.dg_TxnResults.Columns.Clear();

                    if (ManufFactoryClient.GetManufacturerDetails(out errorMessage, out ManufObj, SelectedManufacturer.ManufacturerId) == true)
                    {
                        if (ManufObj.SalesDetails.Count > 0)
                        {
                            uc_ManufDetails.dg_TxnResults.ItemsSource = ManufObj.SalesDetails;

                            DataGridTextColumn NewColumn;

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Sale Id";
                            NewColumn.Binding = new Binding("SalesId");
                            NewColumn.Width = 65;
                            uc_ManufDetails.dg_TxnResults.Columns.Add(NewColumn);

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Sale Date";
                            NewColumn.Binding = new Binding("SaleDate");
                            NewColumn.Width = 125;
                            NewColumn.Binding.StringFormat = "{0:f}";
                            uc_ManufDetails.dg_TxnResults.Columns.Add(NewColumn);

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Total Amt";
                            NewColumn.Binding = new Binding("TotalSaleAmount");
                            NewColumn.Binding.StringFormat = "{0:C2}";
                            NewColumn.Width = 90;
                            uc_ManufDetails.dg_TxnResults.Columns.Add(NewColumn);

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Discount";
                            NewColumn.Binding = new Binding("TotalDiscountAmount");
                            NewColumn.Binding.StringFormat = "{0:C2}";
                            NewColumn.Width = 85;
                            uc_ManufDetails.dg_TxnResults.Columns.Add(NewColumn);

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Tax";
                            NewColumn.Binding = new Binding("TotalTaxAmount");
                            NewColumn.Binding.StringFormat = "{0:C2}";
                            NewColumn.Width = 90;
                            uc_ManufDetails.dg_TxnResults.Columns.Add(NewColumn);

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Final Amt";
                            NewColumn.Binding = new Binding("FinalSaleAmount");
                            NewColumn.Binding.StringFormat = "{0:C2}";
                            NewColumn.Width = 90;
                            uc_ManufDetails.dg_TxnResults.Columns.Add(NewColumn);

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Amt Paid";
                            NewColumn.Binding = new Binding("PaidAmount");
                            NewColumn.Binding.StringFormat = "{0:C2}";
                            NewColumn.Width = 90;
                            uc_ManufDetails.dg_TxnResults.Columns.Add(NewColumn);

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Balance";
                            NewColumn.Binding = new Binding("BalanceAmount");
                            NewColumn.Binding.StringFormat = "{0:C2}";
                            NewColumn.Width = 90;
                            uc_ManufDetails.dg_TxnResults.Columns.Add(NewColumn);
                        }
                    }
                    else
                        if ((errorMessage != "") && (errorMessage != null))
                        {
                            WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Manufacturer Details", 0, "ERROR");
                            MsgBox.ShowDialog();
                            return;
                        }
                #endregion



                this.uc_ManufDetails.Visibility = Visibility.Visible;
            }
            else
            {
                this.uc_ManufDetails.ClearControls();
                this.uc_ManufDetails.Visibility = Visibility.Collapsed;
            }
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.Visibility == Visibility.Visible)
            {
                if (txt_ManufNameId.Text != "")
                    SearchManufacturers(txt_ManufNameId.Text);
                else
                    dg_ManufMain.ItemsSource = "";
            }
        }

        private void dg_ManufMain_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (dg_ManufMain.SelectedIndex > -1)
                if (ManufObjList != null)
                    if ((ManufObjList.Count > 0) && (dg_ManufMain.SelectedIndex > -1))
                    {
                        this.uc_ManufDetails.ClearControls();
                        this.uc_ManufDetails.DisplayManufacturer((Manufacturer)ManufObjList[dg_ManufMain.SelectedIndex]);
                        this.uc_ManufDetails.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        this.uc_ManufDetails.ClearControls();
                        this.uc_ManufDetails.Visibility = Visibility.Collapsed;
                    }

        }

        private void uc_ManufDetails_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
