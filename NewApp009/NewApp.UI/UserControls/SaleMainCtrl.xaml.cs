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
using System.Threading;
using System.ComponentModel;
using System.IO;
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
    /// Interaction logic for SalesMainCtrl.xaml
    /// </summary>
    public partial class SaleMainCtrl : UserControl
    {

        public Thread PrinterThread;



        public static readonly RoutedEvent SaleMainWithCustomArgsEvent = EventManager.RegisterRoutedEvent("SaleMainWithCustomArgs", RoutingStrategy.Bubble, typeof(SaleMainWithCustomArgsEventHandler), typeof(SaleMainCtrl));
        public delegate void SaleMainWithCustomArgsEventHandler(object sender, ProcessEventArgs e);


        public delegate void ProcessHandler(object sender, ProcessEventArgs data);
        string _mode;
        string _invokingCtrlMode;
        string _invokingCtrlName;
        string _CtrlTabName;

        UserCtrlArgs UCArgs;


        public SaleMainCtrl()
        {
            InitializeComponent();
        }

        public string Mode
        {
            get { return _mode; }
            set {_mode = value;}
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

        public event SaleMainWithCustomArgsEventHandler SaleMainWithCustomArgs
        {
            add { AddHandler(SaleMainWithCustomArgsEvent, value); }
            remove { RemoveHandler(SaleMainWithCustomArgsEvent, value); }
        }

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            UCArgs = new UserCtrlArgs();
            UCArgs.InvokingControlName = this.Name;
            UCArgs.InvokingControlMode = this.Mode;
            UCArgs.TargetControlName = "MySaleItemMainCtrl";
            UCArgs.TargetControlMode = "S";
            UCArgs.ControlTabName = this.ControlTabName;

            e.Handled = true;
            ProcessEventArgs myargs = new ProcessEventArgs(SaleMainWithCustomArgsEvent, UCArgs.InvokingControlName, UCArgs.InvokingControlMode, UCArgs.TargetControlName, UCArgs.TargetControlMode, null, null, UCArgs.ControlTabName);
            RaiseEvent(myargs);
        }

        private void btn_SaleCommit_Click(object sender, RoutedEventArgs e)
        {
            Sale NewSaleObj;
            string errorMessage = null;

            if (ApplicationState.GetValue<Sale>("NewSaleObj", out NewSaleObj, out errorMessage) == true)
            {
                NewSaleObj.UserInfo.UserId = "admin";
                NewSaleObj.CustomerInfo.GuestName = "";


                if (NewSaleObj.CustomerInfo.CustomerId == null)
                {
                    WinMessageBox WinMsgBox = new WinMessageBox("Do You Want to Select 'Guest' For Customer?", "New Sale", 1, "QUESTION");
                    WinMsgBox.ShowDialog();
                    if (WinMsgBox.YesNoCancelValue == 0)
                    {
                        Window.GetWindow(this).WindowStyle = System.Windows.WindowStyle.None;
                        Window.GetWindow(this).Opacity = 0.25;

                        WinInputBox NewInputBox = new WinInputBox("Enter Name:");
                        NewInputBox.ShowDialog();

                        Window.GetWindow(this).WindowStyle = System.Windows.WindowStyle.None;
                        Window.GetWindow(this).Opacity = 1;

                        string result = "";

                        if (ApplicationState.GetValue<string>("InputBoxResult", out result, out errorMessage) == true)
                        {
                            NewSaleObj.CustomerInfo.CustomerId = "GUEST";
                            NewSaleObj.CustomerInfo.CustomerName = result;
                        }
                        else
                        {
                            //BusyBar.IsBusy = false;
                            WinMsgBox = new WinMessageBox(errorMessage, "Sales", 0, "ERROR");
                            WinMsgBox.ShowDialog();
                        }
                    }
                    else
                        return;
                }

                SaleFactoryClient SaleFactoryClient = new SaleFactoryClient();

                if (SaleFactoryClient.CreateSale(out errorMessage, NewSaleObj) == true)
                {

                    PrintBill();

                    if (ApplicationState.GetValue<Sale>("NewSaleObj", out NewSaleObj, out errorMessage) == true)
                        ApplicationState.DeleteValue("NewSaleObj");
                    else
                    {
                        WinMessageBox WinMsgBox = new WinMessageBox(errorMessage, "Sales", 0, "ERROR");
                        WinMsgBox.ShowDialog();
                    }

                    ClearControls();
                }
                else
                {
                    WinMessageBox WinMsgBox = new WinMessageBox(errorMessage, "Sales", 0, "ERROR");
                    WinMsgBox.ShowDialog();
                }
            }
            else if ((errorMessage != "") && (errorMessage != null))
            {
                WinMessageBox WinMsgBox = new WinMessageBox(errorMessage, "Sales", 0, "ERROR");
                WinMsgBox.ShowDialog();
                return;
            }
        }

        private void Txt_ItemNameNo_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Txt_ItemNameNo.Text != "")
            {
                UCArgs = new UserCtrlArgs();
                UCArgs.InvokingControlName = this.Name;
                UCArgs.InvokingControlMode = this.Mode;
                UCArgs.TargetControlName = "MySaleItemMainCtrl";
                UCArgs.TargetControlMode = "S";
                UCArgs.ControlTabName = this.ControlTabName;

                e.Handled = true;
                ProcessEventArgs myargs = new ProcessEventArgs(SaleMainWithCustomArgsEvent, UCArgs.InvokingControlName, UCArgs.InvokingControlMode, UCArgs.TargetControlName, UCArgs.TargetControlMode, null, null, UCArgs.ControlTabName);
                RaiseEvent(myargs);
            }
        }

        private void btn_Modify_Click(object sender, RoutedEventArgs e)
        {
            string errorMessage = null;

            StoreItem StoreItemObj = new StoreItem();
            SaleItem NewSaleItemObj = new SaleItem();
            Sale NewSaleObj = new Sale();

            if (ApplicationState.GetValue<Sale>("NewSaleObj", out NewSaleObj, out errorMessage) == true)
            {
                int NewQty = 0;

                Window.GetWindow(this).WindowStyle = System.Windows.WindowStyle.None;
                Window.GetWindow(this).Opacity = 0.25;

                WinInputBox NewInputBox = new WinInputBox("Enter New Quantity:");
                NewInputBox.ShowDialog();

                Window.GetWindow(this).WindowStyle = System.Windows.WindowStyle.None;
                Window.GetWindow(this).Opacity = 1;

                string result = "";

                if (ApplicationState.GetValue<string>("InputBoxResult", out result, out errorMessage) == true)
                {
                    NewQty = Convert.ToInt32(result);
                    ApplicationState.DeleteValue("InputBoxResult");
                }
                else if ((errorMessage != "") && (errorMessage != null))
                {
                    WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Sales", 0, "ERROR");
                    MsgBox.ShowDialog();
                    return;
                }
                else if (NewQty == 0)
                {
                    WinMessageBox WinMsgBox = new WinMessageBox("New Quantity Cannot be 0", "Sales", 0, "VALIDATION");
                    WinMsgBox.ShowDialog();
                    return;
                }

                if ((SaleItem)dg_SalesItems.SelectedItem != null)
                {
                    NewSaleItemObj = (SaleItem)dg_SalesItems.SelectedItem;

                    SaleItemFactoryClient SaleItemFactoryClient = new SaleItemFactoryClient();

                    if (SaleItemFactoryClient.UpdateQuantity(out errorMessage, NewSaleItemObj.SalesId, NewSaleItemObj.SerialNumber, NewSaleItemObj.ItemId, NewSaleItemObj.BatchId, Convert.ToInt32(NewQty), Convert.ToChar("U")) == true)
                    {
                        NewSaleItemObj.Quantity = NewQty;
                        NewSaleObj.SaleItemsList.Remove(NewSaleItemObj);
                        ApplicationState.DeleteValue("NewSaleItemObj");

                        NewSaleItemObj.Quantity = NewQty;

                        NewSaleObj.SaleItemsList.Add(NewSaleItemObj);

                        ApplicationState.DeleteValue("NewSaleObj");
                        ApplicationState.SetValue("NewSaleObj", NewSaleObj);

                        dg_SalesItems.ItemsSource = "";
                        dg_DiscDtls.ItemsSource = "";
                        dg_TaxDtls.ItemsSource = "";
                        dg_SalesItems.ItemsSource = NewSaleObj.SaleItemsList;
                    }
                    else if ((errorMessage != "") && (errorMessage != null))
                    {
                        WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Sales", 0, "ERROR");
                        MsgBox.ShowDialog();
                        return;
                    }
                }
            }
        }

        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            string errorMessage = null;

            StoreItem StoreItemObj = new StoreItem();
            SaleItem NewSaleItemObj = new SaleItem();
            Sale NewSaleObj = new Sale();

            if (ApplicationState.GetValue<Sale>("NewSaleObj", out NewSaleObj, out errorMessage) == true)
            {
                if ((SaleItem)dg_SalesItems.SelectedItem != null)
                {
                    NewSaleItemObj = (SaleItem)dg_SalesItems.SelectedItem;

                    if (Convert.ToInt32(NewSaleItemObj.Quantity) > 0)
                    {
                        SaleItemFactoryClient SaleItemFactoryClient = new SaleItemFactoryClient();

                        if (SaleItemFactoryClient.UpdateQuantity(out errorMessage, NewSaleItemObj.SalesId, NewSaleItemObj.SerialNumber, NewSaleItemObj.ItemId, NewSaleItemObj.BatchId, Convert.ToInt32(NewSaleItemObj.Quantity), Convert.ToChar("D")) == true)              
                        {
                            NewSaleObj.SaleItemsList.Remove(NewSaleItemObj);
                            ApplicationState.DeleteValue("NewSaleItemObj");

                            ApplicationState.DeleteValue("NewSaleObj");
                            ApplicationState.SetValue("NewSaleObj", NewSaleObj);

                            dg_SalesItems.ItemsSource = "";
                            dg_DiscDtls.ItemsSource = "";
                            dg_TaxDtls.ItemsSource = "";
                            dg_SalesItems.ItemsSource = NewSaleObj.SaleItemsList;
                        }
                        else if ((errorMessage != "") && (errorMessage != null))
                        {
                            WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Sales", 0, "ERROR");
                            MsgBox.ShowDialog();
                            return;
                        }
                    }
                }
            }
        }

        private void btn_AddToCustomer_Click(object sender, RoutedEventArgs e)
        {
            Sale NewSaleObj;
            string errorMessage = null;

            if (ApplicationState.GetValue<Sale>("NewSaleObj", out NewSaleObj, out errorMessage) == true)
            {
                UCArgs = new UserCtrlArgs();
                UCArgs.InvokingControlName = this.Name;
                UCArgs.InvokingControlMode = this.Mode;
                UCArgs.TargetControlName = "MyCustMainCtrl";
                UCArgs.TargetControlMode = "S";
                UCArgs.ControlTabName = this.ControlTabName;

                e.Handled = true;
                ProcessEventArgs myargs = new ProcessEventArgs(SaleMainWithCustomArgsEvent, UCArgs.InvokingControlName, UCArgs.InvokingControlMode, UCArgs.TargetControlName, UCArgs.TargetControlMode, null, null, UCArgs.ControlTabName);
                RaiseEvent(myargs);
            }
            else if ((errorMessage != "") && (errorMessage != null))
            {
                WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Sales", 0, "ERROR");
                MsgBox.ShowDialog();
                return;
            }
        }

        private void txt_TotalAmtPaid_TextChanged(object sender, TextChangedEventArgs e)
        {
            Sale NewSaleObj;
            string errorMessage = null;

            if (ApplicationState.GetValue<Sale>("NewSaleObj", out NewSaleObj, out errorMessage) == true)
                if (NewSaleObj.SaleItemsList != null)
                    if (NewSaleObj.SaleItemsList.Count > 0)
                        UpdateSaleObjectValues();
        }

        private void  UpdateSaleObjectValues()
        {
            Sale NewSaleObj;
            string errorMessage = null;

            if (ApplicationState.GetValue<Sale>("NewSaleObj", out NewSaleObj, out errorMessage) == true)
            {
                if ((txt_TotalAmtPaid.Text != null) && (txt_TotalAmtPaid.Text != ""))
                {
                    NewSaleObj.PaidAmount = Convert.ToDouble(txt_TotalAmtPaid.Text);
                    NewSaleObj.BalanceAmount = NewSaleObj.FinalSaleAmount - NewSaleObj.PaidAmount;

                }
                tb_Balance.Text = NewSaleObj.BalanceAmount.ToString();

                ApplicationState.DeleteValue("NewSaleObj");
                ApplicationState.SetValue("NewSaleObj", NewSaleObj);
            }
            else if ((errorMessage != "") && (errorMessage != null))
            {
                WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Sales", 0, "ERROR");
                MsgBox.ShowDialog();
                return;
            }
        }

        private void txt_TotalAmtPayable_TextChanged(object sender, TextChangedEventArgs e)
        {
            Sale NewSaleObj;
            string errorMessage = null;

            if (ApplicationState.GetValue<Sale>("NewSaleObj", out NewSaleObj, out errorMessage) == true)
                if (NewSaleObj.SaleItemsList != null)
                    if (NewSaleObj.SaleItemsList.Count > 0)
                        UpdateSaleObjectValues();
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Sale NewSaleObj;
            string errorMessage = null;


            if (this.Visibility == Visibility.Visible)
            {
                if (ApplicationState.GetValue<Sale>("NewSaleObj", out NewSaleObj, out errorMessage) == true)
                    RefreshData();
                else
                {
                    string Sale_Id = null;
                    NewSaleObj = new Sale();

                    SaleFactoryClient SaleFactoryClient = new SaleFactoryClient();

                    if (SaleFactoryClient.GetNewSaleId(out Sale_Id, out errorMessage) == true)
                    {
                        NewSaleObj.SalesId = Sale_Id;
                        this.txt_SaleId.Text = NewSaleObj.SalesId;

                        ApplicationState.SetValue("NewSaleObj", NewSaleObj);
                    }
                }
            }
        }

        private void RefreshData()
        {
            Sale NewSaleObj;
            string errorMessage = null;

            if (this.Visibility == Visibility.Visible)
            {
                if (ApplicationState.GetValue<Sale>("NewSaleObj", out NewSaleObj, out errorMessage) == true)
                {
                    txt_TotalSaleAmount.Text = NewSaleObj.TotalSaleAmount.ToString();
                    txt_FinalAmtPayable.Text = NewSaleObj.FinalSaleAmount.ToString();
                    txt_TotalDiscount.Text = Convert.ToString(NewSaleObj.TotalDiscountAmount.ToString());
                    

                    if (NewSaleObj.CustomerInfo.CustomerId != null)
                    {
                        txt_CustNameId.Text = NewSaleObj.CustomerInfo.CustomerName.ToString() + "(" + NewSaleObj.CustomerInfo.CustomerId.ToString() + ")";
                    }

                    ApplicationState.DeleteValue("NewSaleObj");
                    ApplicationState.SetValue("NewSaleObj", NewSaleObj);

                    dg_SalesItems.ItemsSource = "";
                    dg_DiscDtls.ItemsSource = "";
                    dg_TaxDtls.ItemsSource = "";
                    dg_SalesItems.ItemsSource = NewSaleObj.SaleItemsList;

                    List<DiscountItem> discItemList = new List<DiscountItem>();

                    if (NewSaleObj.DiscountList != null)
                    {
                        foreach (Discount disc in NewSaleObj.DiscountList)
                        {
                            DiscountItem discItem = new DiscountItem();

                            discItem.DiscountCode = disc.DiscountCode;
                            discItem.DiscountDetail = disc;
                            discItem.DiscItemId = "";
                            discItem.DiscItemBatchId = "";

                            discItemList.Add(discItem);
                        }
                    }

                    foreach (SaleItem SaleItem in NewSaleObj.SaleItemsList)
                    {
                        foreach (DiscountItem discItem in SaleItem.StoreItemDetail.DiscountList)
                            discItemList.Add(discItem);
                    }

                    List<DiscountItem>discDistinctlist = new List<DiscountItem>();
                    var distinctdiscitems = discItemList.GroupBy(x => new { x.DiscountCode, x.DiscItemId, x.DiscItemBatchId }).Select(s => s.First());

                    foreach (var discitem in distinctdiscitems)
                        discDistinctlist.Add(discitem);

                    dg_DiscDtls.ItemsSource = discDistinctlist;

                    txt_TotalDiscount.Text = Convert.ToString(NewSaleObj.TotalDiscountAmount.ToString());



                    List<TaxItem> taxitemlist = new List<TaxItem>();

                    if (NewSaleObj.TaxList != null)
                    {
                        foreach (Tax tax in NewSaleObj.TaxList)
                        {
                            TaxItem taxItem = new TaxItem();

                            taxItem.TaxCode = tax.TaxCode;
                            taxItem.TaxDetail = tax;
                            taxItem.ItemId = "";
                            taxItem.BatchId = "";

                            taxitemlist.Add(taxItem);
                        }
                    }

                    foreach (SaleItem SaleItem in NewSaleObj.SaleItemsList)
                    {
                        foreach (TaxItem taxItem in SaleItem.StoreItemDetail.TaxList)
                            taxitemlist.Add(taxItem);
                    }

                    List<TaxItem> taxDistinctlist = new List<TaxItem>();
                    var distincttaxitems = taxitemlist.GroupBy(x => new { x.TaxCode, x.ItemId, x.BatchId }).Select(s => s.First());

                    foreach (var taxitem in distincttaxitems)
                        taxDistinctlist.Add(taxitem);

                    dg_TaxDtls.ItemsSource = distincttaxitems;

                    txt_TotalTaxAmt.Text = Convert.ToString(NewSaleObj.TotalTaxAmount.ToString());
                }
                else if ((errorMessage != "") && (errorMessage != null))
                {
                    WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Sales", 0, "ERROR");
                    MsgBox.ShowDialog();
                    return;
                }
            }
        }

        public void ClearControls()
        {
            txt_SaleId.Text = "";
            Txt_ItemNameNo.Text = "";
            txt_CustNameId.Text = "";
            Txt_ItemNameNo.Text = "";
            txt_TotalAmtPaid.Text = "0";
            txt_TotalSaleAmount.Text = "0";
            txt_TotalSaleAmount.Text = "0";
            txt_FinalAmtPayable.Text = "0";
            txt_TotalDiscount.Text = "0";
            tb_Balance.Text = "0";
            txt_TotalTaxAmt.Text = "0";
            dg_SalesItems.ItemsSource = null;
            dg_DiscDtls.ItemsSource = null;
            dg_TaxDtls.ItemsSource = null;
        }

        private void btn_Reset_Click(object sender, RoutedEventArgs e)
        {
            Sale NewSaleObj;
            string errorMessage = null;



            ClearControls();

            if (ApplicationState.GetValue<Sale>("NewSaleObj", out NewSaleObj, out errorMessage) == true)
                ApplicationState.DeleteValue("NewSaleObj");

            NewSaleObj = new Sale();
            ApplicationState.SetValue("NewSaleObj", NewSaleObj);
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Sale NewSaleObj;
            string errorMessage = null;
            
            ClearControls();

            if (ApplicationState.GetValue<Sale>("NewSaleObj", out NewSaleObj, out errorMessage) == true)
                ApplicationState.DeleteValue("NewSaleObj");

            UCArgs = new UserCtrlArgs();
            UCArgs.InvokingControlName = this.Name;
            UCArgs.InvokingControlMode = this.Mode;
            UCArgs.TargetControlName = this.InvokingControlName;
            UCArgs.TargetControlMode = this.InvokingControlMode;
            UCArgs.ControlTabName = this.ControlTabName;

            e.Handled = true;
            ProcessEventArgs myargs = new ProcessEventArgs(SaleMainWithCustomArgsEvent, UCArgs.InvokingControlName, UCArgs.InvokingControlMode, UCArgs.TargetControlName, UCArgs.TargetControlMode, null, null, UCArgs.ControlTabName);
            RaiseEvent(myargs);
        }

        private void PrintBill()
        {
            Sale NewSaleObj;
            string errorMessage = null;

            if (ApplicationState.GetValue<Sale>("NewSaleObj", out NewSaleObj, out errorMessage) == true)
            {
                WinBill WinBill = new WinBill(NewSaleObj, true, "A");

                try
                {
                    WinBill.ShowDialog();
                }
                catch (Exception ex1)
                {
                    errorMessage = ex1.Message.ToString();
                    return;
                }
            }
        }

        private void StartBusyBar()
        {
            Sale NewSaleObj;
            string errorMessage = null;


            if (ApplicationState.GetValue<Sale>("NewSaleObj", out NewSaleObj, out errorMessage) == true)
            {
                //tb_BusyBarText.Text = "Sales Details Saved Successfully. Sale Id: " + NewSaleObj.SalesId.ToString();
                //BusyBar.IsBusy = true;
            }
        }

        private void StopBusyBar()
        {
            //BusyBar.IsBusy = false;
        }
    }
}
