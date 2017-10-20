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
using System.Data.SqlClient;
using NewApp.UI.UserControls;
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
    /// Interaction logic for ItemMainCtrl.xaml
    /// </summary>
    public partial class ItemMainCtrl : UserControl
    {
        List<StoreItem> StoreItemObjList;

        public static readonly RoutedEvent ItemMainWithCustomArgsEvent = EventManager.RegisterRoutedEvent("ItemMainWithCustomArgs", RoutingStrategy.Bubble, typeof(ItemMainWithCustomArgsEventHandler), typeof(ItemMainCtrl));
        public delegate void ItemMainWithCustomArgsEventHandler(object sender, ProcessEventArgs e);


        public delegate void ProcessHandler(object sender, ProcessEventArgs data);
        string _mode;
        string _invokingCtrlMode;
        string _invokingCtrlName;
        string _previnvokingCtrlMode;
        string _previnvokingCtrlName;
        string _CtrlTabName;

        UserCtrlArgs UCArgs;

        public ItemMainCtrl()
        {
            InitializeComponent();
        }

        public string Mode
        {
            get
            {
                return _mode;
            }
            set
            {
                _mode = value;

                if (_mode == "S")
                {
                    this.btn_SelItem.Visibility = Visibility.Visible;
                    this.btn_Add.Visibility = Visibility.Collapsed;
                    this.btn_Modify.Visibility = Visibility.Collapsed;
                    this.btn_Delete.Visibility = Visibility.Collapsed;
                    this.txt_QtySold.Visibility = Visibility.Visible;
                    this.tb_QtySold.Visibility = Visibility.Visible;
                }
                else
                {
                    this.btn_SelItem.Visibility = Visibility.Collapsed;
                    this.btn_Add.Visibility = Visibility.Visible;
                    this.btn_Modify.Visibility = Visibility.Visible;
                    this.btn_Delete.Visibility = Visibility.Visible;
                    this.txt_QtySold.Visibility = Visibility.Collapsed;
                    this.tb_QtySold.Visibility = Visibility.Collapsed;
                }
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

        public string PrevInvokingControlMode
        {
            get { return _previnvokingCtrlMode; }
            set { _previnvokingCtrlMode = value; }
        }

        public string PrevInvokingControlName
        {
            get { return _previnvokingCtrlName; }
            set { _previnvokingCtrlName = value; }
        }

        public event ItemMainWithCustomArgsEventHandler ItemMainWithCustomArgs
        {
            add { AddHandler(ItemMainWithCustomArgsEvent, value); }
            remove { RemoveHandler(ItemMainWithCustomArgsEvent, value); }
        }

        public string ControlTabName
        {
            get { return _CtrlTabName; }
            set { _CtrlTabName = value; }
        }

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            UCArgs = new UserCtrlArgs();
            UCArgs.InvokingControlName = this.Name;
            UCArgs.InvokingControlMode = this.Mode;
            UCArgs.TargetControlName = "MyStoreItemCtrl";
            UCArgs.TargetControlMode = "A";
            UCArgs.ControlTabName = this.ControlTabName;

            ProcessEventArgs myargs = new ProcessEventArgs(ItemMainWithCustomArgsEvent, UCArgs.InvokingControlName, UCArgs.InvokingControlMode, UCArgs.TargetControlName, UCArgs.TargetControlMode, null, null, UCArgs.ControlTabName);
            RaiseEvent(myargs);
        }

        private void btn_Modify_Click(object sender, RoutedEventArgs e)
        {
            WinMessageBox MsgBox = new WinMessageBox("Functionality Yet to be Added", "Modify Item", 0, "INFORMATION");
            MsgBox.ShowDialog();
        }

        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            string errorMessage = null;

            if (dg_ItemList.SelectedIndex > -1)
            {
                StoreItem StoreItem = (StoreItem)StoreItemObjList[dg_ItemList.SelectedIndex];

                StoreItemFactoryClient StoreItemFactoryClient = new StoreItemFactoryClient();

                if (StoreItemFactoryClient.DeleteStoreItem(out errorMessage, StoreItem) == true)
                {
                    WinMessageBox MsgBox = new WinMessageBox("Item Record Deleted Successfully!", "Delete Item", 0, "SUCCESS");
                    MsgBox.ShowDialog();

                    dg_ItemList.ItemsSource = "";
                    SearchStoreItems(txt_ItemNameNo.Text);
                }
                else
                {
                    WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Delete Item", 0, "ERROR");
                    MsgBox.ShowDialog();
                    return;
                }
            }
        }

        private void btn_View_Click(object sender, RoutedEventArgs e)
        {
            string errorMessage = null;

            if (dg_ItemList.SelectedIndex > -1)
            {
                StoreItem StoreItem = (StoreItem)StoreItemObjList[dg_ItemList.SelectedIndex];


                if (StoreItem != null)
                {
                    StoreItem NewStoreItemObj = new StoreItem();

                    if (ApplicationState.GetValue<StoreItem>("NewStoreItemObj", out NewStoreItemObj, out errorMessage) == true)
                        ApplicationState.DeleteValue("NewStoreItemObj");

                    ApplicationState.SetValue("NewStoreItemObj", StoreItem);
                }

                UCArgs = new UserCtrlArgs();
                UCArgs.InvokingControlName = this.Name;
                UCArgs.InvokingControlMode = this.Mode;
                UCArgs.TargetControlName = "MyStoreItemCtrl";
                UCArgs.TargetControlMode = "V";
                UCArgs.ControlTabName = this.ControlTabName;

                ProcessEventArgs myargs = new ProcessEventArgs(ItemMainCtrl.ItemMainWithCustomArgsEvent, UCArgs.InvokingControlName, UCArgs.InvokingControlMode, UCArgs.TargetControlName, UCArgs.TargetControlMode, null, null, UCArgs.ControlTabName);
                RaiseEvent(myargs);
            }
        }

        public void SearchStoreItems(string Str)
        {
            string errorMessage = null;

            if (Str.ToString().Length > 0)
            {
                StoreItem[] StoreItemObjArray;
                StoreItemObjList = new List<StoreItem>();

                DateTime dt = DateTime.Now;

                StoreItemFactoryClient StoreItemFactoryClient = new StoreItemFactoryClient();

                if (StoreItemFactoryClient.GetStoreItemByItemIdOrName(out errorMessage, out StoreItemObjArray, Str) == true)
                {
                    if (StoreItemObjArray != null)
                    {
                        StoreItemObjList = StoreItemObjArray.ToList<StoreItem>();
                        dg_ItemList.ItemsSource = StoreItemObjList;
                    }
                }
                else
                {
                    if ((errorMessage != "") && (errorMessage != null))
                    {
                        WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Search Items", 0, "ERROR");
                        MsgBox.ShowDialog();
                        return;
                    }
                }
            }
        }

        private void txt_ItemNameNo_TextChanged(object sender, TextChangedEventArgs e)
        {           
            dg_ItemList.ItemsSource = "";
            txt_QtySold.Text = "";
            if (txt_ItemNameNo.Text != "")
                SearchStoreItems(txt_ItemNameNo.Text);
            else
                dg_ItemList.ItemsSource = "";

            this.Focus();
        }

        private void Btn_Close_Click(object sender, RoutedEventArgs e)
        {
            UCArgs = new UserCtrlArgs();
            UCArgs.InvokingControlName = this.InvokingControlName;
            UCArgs.InvokingControlMode = this.Mode;
            UCArgs.TargetControlName = this.InvokingControlName;
            UCArgs.TargetControlMode = this.InvokingControlMode;
            UCArgs.ControlTabName = this.ControlTabName;

            
            if (UCArgs.ControlTabName != "MyItemMainTabItemCtrl")
            {
                e.Handled = true;
                ProcessEventArgs myargs = new ProcessEventArgs(ItemMainWithCustomArgsEvent, UCArgs.InvokingControlName, UCArgs.InvokingControlMode, UCArgs.TargetControlName, UCArgs.TargetControlMode, null, null, UCArgs.ControlTabName);
                RaiseEvent(myargs);
            }
        }

        private void btn_SelItem_Click(object sender, RoutedEventArgs e)
        {
            string errorMessage = null;

            StoreItem StoreItemObj = new StoreItem();
            SaleItem NewSaleItemObj = new SaleItem();
            Sale NewSaleObj = new Sale();

            if ((StoreItem)dg_ItemList.SelectedItem != null)
            {
                StoreItemObj = (StoreItem)dg_ItemList.SelectedItem;

                NewSaleItemObj.ItemId = StoreItemObj.ItemId;
                NewSaleItemObj.BatchId = StoreItemObj.BatchId;
                NewSaleItemObj.ItemName = StoreItemObj.ItemName;
                NewSaleItemObj.StoreItemDetail = StoreItemObj;
                if ((txt_QtySold.Text != null) && (txt_QtySold.Text != ""))
                {
                    if (Convert.ToInt32(txt_QtySold.Text) <= StoreItemObj.QtyAvailable)
                    {
                        NewSaleItemObj.Quantity = Convert.ToInt32(txt_QtySold.Text);

                        if (ApplicationState.GetValue<Sale>("NewSaleObj", out NewSaleObj, out errorMessage) == false)
                        {
                            if ((errorMessage != "") && (errorMessage != null))
                            {
                                WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Select Item", 0, "ERROR");
                                MsgBox.ShowDialog();
                                return;
                            }
                        }

                        NewSaleItemObj.SalesId = NewSaleObj.SalesId.ToString();
                        NewSaleItemObj.SerialNumber = NewSaleObj.SaleItemsList.Count + 1;

                        SaleItemFactoryClient SaleItemFactoryClient = new SaleItemFactoryClient();

                        if (SaleItemFactoryClient.UpdateQuantity(out errorMessage, NewSaleObj.SalesId, NewSaleItemObj.SerialNumber, NewSaleItemObj.ItemId, NewSaleItemObj.BatchId, Convert.ToInt32(NewSaleItemObj.Quantity), Convert.ToChar("S")) == false)
                        {
                            if ((errorMessage != "") && (errorMessage != null))
                            {
                                WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Update Quantity", 0, "ERROR");
                                MsgBox.ShowDialog();
                                return;
                            }
                        }

                        if (SaleItemFactoryClient.ComputeSaleItemAmounts(out NewSaleItemObj, out errorMessage, NewSaleItemObj) == false)
                        {
                            if ((errorMessage != "") && (errorMessage != null))
                            {
                                WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Select Item", 0, "ERROR");
                                MsgBox.ShowDialog();
                                return;
                            }
                        }

                        NewSaleObj.SaleItemsList.Add(NewSaleItemObj);

                        SaleFactoryClient SaleFactoryClient = new SaleFactoryClient();

                        if (SaleFactoryClient.ComputeSaleAmounts(out NewSaleObj, out errorMessage, NewSaleObj) == false)
                        {
                            if ((errorMessage != "") && (errorMessage != null))
                            {
                                WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Select Item", 0, "ERROR");
                                MsgBox.ShowDialog();
                                return;
                            }
                        }

                        ApplicationState.DeleteValue("NewSaleObj");
                        ApplicationState.SetValue("NewSaleObj", NewSaleObj);

                        UCArgs = new UserCtrlArgs();
                        UCArgs.InvokingControlName = this.Name;
                        UCArgs.InvokingControlMode = this.Mode;
                        UCArgs.TargetControlName = this.InvokingControlName;
                        UCArgs.TargetControlMode = this.InvokingControlMode;
                        UCArgs.ControlTabName = this.ControlTabName;

                        e.Handled = true;
                        ProcessEventArgs myargs = new ProcessEventArgs(ItemMainWithCustomArgsEvent, UCArgs.InvokingControlName, UCArgs.InvokingControlMode, UCArgs.TargetControlName, UCArgs.TargetControlMode, null, null, UCArgs.ControlTabName);
                        RaiseEvent(myargs);
                    }
                    else
                    {
                        WinMessageBox MsgBox = new WinMessageBox("The Quantity Entered Cannot be Greater Than Quantity Available", "Update Quantity", 0, "VALIDATION");
                        MsgBox.ShowDialog();
                    }
                }
            }
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.Visibility == Visibility.Visible)
            {
                dg_ItemList.ItemsSource = "";
                if (txt_ItemNameNo.Text != "")
                    SearchStoreItems(txt_ItemNameNo.Text);
                else
                    dg_ItemList.ItemsSource = "";
            }
        }

        public void ClearControls()
        {
            txt_ItemNameNo.Text = "";
        }

        private void dg_ItemList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
