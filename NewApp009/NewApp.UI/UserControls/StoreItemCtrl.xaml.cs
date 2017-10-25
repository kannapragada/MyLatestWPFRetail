using NewApp.BusinessTier.Models;
using NewApp.ManufFactorySvc;
using NewApp.StoreItemFactorySvc;
using NewApp.UI.Windows;
using NewApp.VendorFactorySvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;



namespace NewApp.UI.UserControls
{
    /// <summary>
    /// Interaction logic for SalesCtrl.xaml
    /// </summary>
    public partial class StoreItemCtrl : UserControl
    {
        private StoreItem _storeItem = new StoreItem();

        public static readonly RoutedEvent StoreItemWithCustomArgsEvent = EventManager.RegisterRoutedEvent("StoreItemCustomArgs", RoutingStrategy.Bubble, typeof(StoreItemWithCustomArgsEventHandler), typeof(StoreItemCtrl));
        public delegate void StoreItemWithCustomArgsEventHandler(object sender, ProcessEventArgs e);


        public delegate void ProcessHandler(object sender, ProcessEventArgs data);
        string _mode;
        string _invokingCtrlMode;
        string _invokingCtrlName;
        string _CtrlTabName;

        UserCtrlArgs UCArgs;


        public StoreItemCtrl()
        {
            InitializeComponent();
        }

        public string Mode
        {
            get{return _mode;}
            set
            {
                _mode = value;

                if (_mode == "V")
                {
                    btn_ItemCommit.Visibility = Visibility.Collapsed;
                    btn_Discounts.Visibility = Visibility.Collapsed;
                }
                else
                {
                    btn_ItemCommit.Visibility = Visibility.Visible;
                    btn_Discounts.Visibility = Visibility.Visible;
                }
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

        public event StoreItemWithCustomArgsEventHandler StoreItemWithCustomArgs
        {
            add { AddHandler(StoreItemWithCustomArgsEvent, value); }
            remove { RemoveHandler(StoreItemWithCustomArgsEvent, value); }
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            string errorMessage = null;

            if ((this.Visibility == Visibility.Visible) && (this.Mode == "V"))
            {
                StoreItem NewStoreItemObj = new StoreItem();

                if (ApplicationState.GetValue<StoreItem>("NewStoreItemObj", out NewStoreItemObj, out errorMessage) == true)
                    DisplayStoreItem(NewStoreItemObj);
            }
            else
            {
                Manufacturer[] ManufObjArray;
                List<Manufacturer> ManufObjList = new List<Manufacturer>();

                ManufacturerFactoryClient ManufacturerFactoryClient = new ManufacturerFactoryClient();

                if (ManufacturerFactoryClient.GetManufacturers(out errorMessage, out ManufObjArray, "ALL") == true)
                {
                    if (ManufObjArray != null)
                    {
                        ManufObjList = ManufObjArray.ToList<Manufacturer>();
                        cmb_ItemManufId.ItemsSource = ManufObjList;
                    }
                }

                Vendor[] VendorObjArray;
                List<Vendor> VendorObjList = new List<Vendor>();


                VendorFactoryClient VendorFactoryClient = new VendorFactoryClient();

                if (VendorFactoryClient.GetVendors(out errorMessage, out VendorObjArray, "ALL") == true)
                {
                    if (VendorObjArray != null)
                    {
                        VendorObjList = VendorObjArray.ToList<Vendor>();
                        cmb_ItemVendorId.ItemsSource = VendorObjList;
                    }
                }
            }
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            UCArgs = new UserCtrlArgs();
            UCArgs.InvokingControlName = this.Name;
            UCArgs.InvokingControlMode = this.Mode;
            UCArgs.TargetControlName = this.InvokingControlName;
            UCArgs.TargetControlMode = this.InvokingControlMode;
            UCArgs.ControlTabName = this.ControlTabName;

            if ((_mode == "A") || (_mode == "U"))
            {
                WinMessageBox WinMsgBox = new WinMessageBox("Are you sure you want to cancel?", "Manufacturer Main", 1, "QUESTION");
                WinMsgBox.ShowDialog();
                if (WinMsgBox.YesNoCancelValue == 0)
                {
                    e.Handled = true;
                    ProcessEventArgs myargs = new ProcessEventArgs(StoreItemWithCustomArgsEvent, UCArgs.InvokingControlName, UCArgs.InvokingControlMode, UCArgs.TargetControlName, UCArgs.TargetControlMode, null, null, UCArgs.ControlTabName);
                    RaiseEvent(myargs);
                }
            }
            else
            {
                e.Handled = true;
                ProcessEventArgs myargs = new ProcessEventArgs(StoreItemWithCustomArgsEvent, UCArgs.InvokingControlName, UCArgs.InvokingControlMode, UCArgs.TargetControlName, UCArgs.TargetControlMode, null, null, UCArgs.ControlTabName);
                RaiseEvent(myargs);
            }
        }

        public void DisplayStoreItem(StoreItem StoreItem)
        {
            txt_ItemId.Text = StoreItem.ItemId.ToString();
            txt_ItemName.Text = StoreItem.ItemName.ToString();
            txt_ItemDesc.Text = StoreItem.ItemDescription.ToString();
            txt_ItemBarCode.Text = StoreItem.BarCode.ToString();
            txt_ItemBatchNumb.Text = StoreItem.BatchId.ToString();
            txt_ItemMRP.Text = String.Format("{0:C2}", StoreItem.MRP);
            txt_ItemPriceProcured.Text = String.Format("{0:C2}", StoreItem.ProcuredPricePerUnit);
            txt_ItemSellingPrice.Text = String.Format("{0:C2}", StoreItem.SellingPricePerUnit);
            txt_ItemWeightPerUnitProcured.Text = StoreItem.WeightProcured.ToString();
            txt_ItemWeightPerUnitAvailable.Text = StoreItem.WeightAvailable.ToString();
            Date_ItemDateProcured.Text = String.Format("{0:f}", StoreItem.DateProcured);
            Date_ItemDateOfManuf.Text = String.Format("{0:f}", StoreItem.DateOfManuf);
            Date_ItemDateOfExpiry.Text = String.Format("{0:f}", StoreItem.DateOfExp);
            txt_ItemQtyAvailable.Text = StoreItem.QtyAvailable.ToString();
            txt_ItemQtyProcured.Text = StoreItem.QtyProcured.ToString();

            if (Mode == "V")
            {
                List<Manufacturer> ManufObjList = new List<Manufacturer>();
                ManufObjList.Add(StoreItem.ManufacturerInfo);
                cmb_ItemManufId.ItemsSource = ManufObjList;
                cmb_ItemManufId.SelectedItem = ManufObjList[0];

                List<Vendor> VendorObjList = new List<Vendor>();
                VendorObjList.Add(StoreItem.VendorInfo);
                cmb_ItemVendorId.ItemsSource = VendorObjList;
                cmb_ItemVendorId.SelectedItem = VendorObjList[0];
            }
        }

        public string ItemName
        {
            get{return txt_ItemName.Text;}
            set{txt_ItemName.Text = value;}
        }

        public void ClearControls()
        {
            this.txt_ItemId.Text = "";
            this.txt_ItemName.Text = "";
            this.txt_ItemDesc.Text = "";
            this.txt_ItemMRP.Text = "";
            this.txt_ItemPriceProcured.Text = "";
            this.txt_ItemQtyAvailable.Text = "";
            this.txt_ItemQtyProcured.Text = "";
            this.txt_ItemSellingPrice.Text = "";
            this.txt_ItemWeightPerUnitAvailable.Text = "";
            this.txt_ItemWeightPerUnitProcured.Text = "";
            this.txt_ItemBarCode.Text = "";
            this.txt_ItemBatchNumb.Text = "";
            this.Date_ItemDateProcured.SelectedDate = null;
            this.Date_ItemDateOfManuf.SelectedDate = null;
            this.Date_ItemDateOfExpiry.SelectedDate = null;
        }

        private void btn_Discounts_Click(object sender, RoutedEventArgs e)
        {
            WinMessageBox MsgBox = new WinMessageBox("Functionality Yet to be Added", "Modify Store Item", 0, "INFORMATION");
            MsgBox.ShowDialog();

        }

        private void btn_ItemCommit_Click(object sender, RoutedEventArgs e)
        {
            string errorMessage = null;

            if ((this.Mode == "A") || (this.Mode == "U"))
            {
                StoreItem newitem = new StoreItem();

                try
                {
                    newitem.ItemName = txt_ItemName.Text;
                    newitem.ItemDescription = txt_ItemDesc.Text;
                    newitem.BarCode = txt_ItemBarCode.Text;
                    newitem.BatchId = txt_ItemBatchNumb.Text;
                    newitem.MRP = Convert.ToDouble(txt_ItemMRP.Text);
                    newitem.QtyProcured = Convert.ToInt32(this.txt_ItemQtyProcured.Text);
                    newitem.ProcuredPricePerUnit = Convert.ToDouble(this.txt_ItemPriceProcured.Text);
                    newitem.DateProcured = Convert.ToDateTime(this.Date_ItemDateProcured.Text);
                    newitem.DateOfManuf = Convert.ToDateTime(this.Date_ItemDateOfManuf.Text);
                    newitem.DateOfExp = Convert.ToDateTime(this.Date_ItemDateOfExpiry.Text);
                    newitem.WeightProcured = Convert.ToDouble(this.txt_ItemWeightPerUnitProcured.Text);
                    newitem.WeightAvailable = Convert.ToInt32(this.txt_ItemWeightPerUnitAvailable.Text);
                    newitem.SellingPricePerUnit = Convert.ToDouble(this.txt_ItemSellingPrice.Text);
                    newitem.QtyAvailable = Convert.ToInt32(this.txt_ItemQtyAvailable.Text);
                    newitem.ManufacturerInfo.ManufacturerId = cmb_ItemManufId.SelectedValue.ToString();
                    newitem.ManufacturerInfo.ManufacturerName = cmb_ItemManufId.SelectedItem.ToString();
                    newitem.VendorInfo.VendorId = cmb_ItemVendorId.SelectedValue.ToString();
                    newitem.VendorInfo.VendorName = cmb_ItemVendorId.SelectedItem.ToString();
                    newitem.ItemCreateDate = DateTime.Today;
                    newitem.BatchCreateDate = DateTime.Today;
                }
                catch (Exception ex1)
                {
                    errorMessage = ex1.Message;
                }

                if (this.Mode == "A")
                {
                    StoreItemFactoryClient StoreItemFactoryClient = new StoreItemFactoryClient();

                    if (StoreItemFactoryClient.AddStoreItem(out errorMessage, newitem) == true)
                    {
                        this.Visibility = Visibility.Visible;
                        WinMessageBox MsgBox = new WinMessageBox("New Item Record Added Successfully!", "Add Store Item", 0, "SUCCESS");
                        MsgBox.ShowDialog();


                        ClearControls();

                        UCArgs = new UserCtrlArgs();
                        UCArgs.InvokingControlName = this.Name;
                        UCArgs.InvokingControlMode = this.Mode;
                        UCArgs.TargetControlName = this.InvokingControlName;
                        UCArgs.TargetControlMode = this.InvokingControlMode;
                        UCArgs.ControlTabName = this.ControlTabName;

                        ProcessEventArgs myargs = new ProcessEventArgs(StoreItemWithCustomArgsEvent, UCArgs.InvokingControlName, UCArgs.InvokingControlMode, UCArgs.TargetControlName, UCArgs.TargetControlMode, null, null, UCArgs.ControlTabName);
                        RaiseEvent(myargs);
                    }
                    else
                    {
                        this.Visibility = Visibility.Visible;
                        WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Add Store Item", 0, "ERROR");
                        MsgBox.ShowDialog();
                    }
                }
                else if (this.Mode == "U")
                {
                    WinMessageBox MsgBox = new WinMessageBox("Functionality Yet to be Added", "Modify Store Item", 0, "INFORMATION");
                    MsgBox.ShowDialog();


                    ClearControls();

                    e.Handled = true;

                    UCArgs = new UserCtrlArgs();
                    UCArgs.InvokingControlName = this.Name;
                    UCArgs.InvokingControlMode = this.Mode;
                    UCArgs.TargetControlName = this.InvokingControlName;
                    UCArgs.TargetControlMode = this.InvokingControlMode;
                    UCArgs.ControlTabName = this.ControlTabName;

                    ProcessEventArgs myargs = new ProcessEventArgs(StoreItemWithCustomArgsEvent, UCArgs.InvokingControlName, UCArgs.InvokingControlMode, UCArgs.TargetControlName, UCArgs.TargetControlMode, null, null, UCArgs.ControlTabName);
                    RaiseEvent(myargs);
                }
            }
        }
    }
}
