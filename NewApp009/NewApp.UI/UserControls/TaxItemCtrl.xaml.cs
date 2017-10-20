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
using System.Collections;
using System.Data;
using NewApp.UI.UserControls;
using NewApp.BusinessTier.Common;
using NewApp.BusinessTier.Models;
using NewApp.UI.Windows;
using NewApp.GenderTypeFactorySvc;
using NewApp.IDProofTypeFactorySvc;
using NewApp.TaxFactorySvc;
using NewApp.TaxItemFactorySvc;
using NewApp.TaxKindFactorySvc;
using NewApp.TaxTypeFactorySvc;


namespace NewApp.UI.UserControls
{
    /// <summary>
    /// Interaction logic for SalesMainCtrl.xaml
    /// </summary>
    public partial class TaxItemCtrl : UserControl
    {
        DataTable ItemList;


        public static readonly RoutedEvent TaxItemWithCustomArgsEvent = EventManager.RegisterRoutedEvent("TaxItemWithCustomArgs", RoutingStrategy.Bubble, typeof(TaxItemWithCustomArgsEventHandler), typeof(TaxItemCtrl));
        public delegate void TaxItemWithCustomArgsEventHandler(object sender, ProcessEventArgs e);


        public delegate void ProcessHandler(object sender, ProcessEventArgs data);
        string _mode;
        string _invokingCtrlMode;
        string _invokingCtrlName;
        string _CtrlTabName;

        UserCtrlArgs UCArgs;

        public TaxItemCtrl()
        {
            InitializeComponent();
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

        public event TaxItemWithCustomArgsEventHandler TaxItemWithCustomArgs
        {
            add { AddHandler(TaxItemWithCustomArgsEvent, value); }
            remove { RemoveHandler(TaxItemWithCustomArgsEvent, value); }
        }

        private void Btn_Close_Click(object sender, RoutedEventArgs e)
        {
            UCArgs = new UserCtrlArgs();
            UCArgs.InvokingControlName = this.Name;
            UCArgs.InvokingControlMode = this.Mode;
            UCArgs.TargetControlName = this.InvokingControlName;
            UCArgs.TargetControlMode = this.InvokingControlMode;
            UCArgs.ControlTabName = this.ControlTabName;

            if ((_mode == "A") || (_mode == "U"))
            {
                WinMessageBox WinMsgBox = new WinMessageBox("Are you sure you want to cancel?", "Customer Details - Add", 1, "QUESTION");
                WinMsgBox.ShowDialog();
                if (WinMsgBox.YesNoCancelValue == 0)
                {
                    e.Handled = true;
                    ProcessEventArgs myargs = new ProcessEventArgs(TaxItemWithCustomArgsEvent, UCArgs.InvokingControlName, UCArgs.InvokingControlMode, UCArgs.TargetControlName, UCArgs.TargetControlMode, null, null, UCArgs.ControlTabName);
                    RaiseEvent(myargs);
                }
            }
            else
            {
                e.Handled = true;
                ProcessEventArgs myargs = new ProcessEventArgs(TaxItemWithCustomArgsEvent, UCArgs.InvokingControlName, UCArgs.InvokingControlMode, UCArgs.TargetControlName, UCArgs.TargetControlMode, null, null, UCArgs.ControlTabName);
                RaiseEvent(myargs);
            }
        }

        public void ClearControls()
        {
            tb_TaxCode.Text = "";
            cmb_ItemId.Text = "";
            tb_ItemName.Text = "";
            cmb_BatchId.Text = "";
            date_StartDate.Text = "";
            date_EndDate.Text = "";
            tb_CreateDate.Text = "";
            tb_LastModDate.Text = "";
        }

        private void btn_Commit_Click(object sender, RoutedEventArgs e)
        {
            string errorMessage = null;

            if ((this.Mode == "A") || (this.Mode == "U"))
            {
                TaxItem newitem = new TaxItem();
                TaxItemFactoryClient TaxItemFactoryClient = new TaxItemFactoryClient();

                newitem.TaxCode = tb_TaxCode.Text;
                newitem.ItemId = cmb_ItemId.Text;
                newitem.BatchId = cmb_BatchId.Text;
                newitem.Remarks = txt_Remarks.Text;
                newitem.StartDate = Convert.ToDateTime(this.date_StartDate.Text);
                newitem.EndDate = Convert.ToDateTime(this.date_EndDate.Text);
                newitem.CreateDate = DateTime.Today;
                newitem.LastModDate = DateTime.Today;

                if (this.Mode == "A")
                {
                    if (TaxItemFactoryClient.AddItemToTaxCode(out errorMessage, newitem) == true)
                    {
                        this.Visibility = Visibility.Visible;
                        WinMessageBox MsgBox = new WinMessageBox("Item Linked to Tax Code Successfully!", "Item Linked To Tax", 0, "SUCCESS");
                        MsgBox.ShowDialog();


                        e.Handled = true;

                        UCArgs = new UserCtrlArgs();
                        UCArgs.InvokingControlName = this.Name;
                        UCArgs.InvokingControlMode = this.Mode;
                        UCArgs.TargetControlName = this.InvokingControlName;
                        UCArgs.TargetControlMode = this.InvokingControlMode;
                        UCArgs.ControlTabName = this.ControlTabName;

                        ProcessEventArgs myargs = new ProcessEventArgs(TaxItemWithCustomArgsEvent, UCArgs.InvokingControlName, UCArgs.InvokingControlMode, UCArgs.TargetControlName, UCArgs.TargetControlMode, null, null, UCArgs.ControlTabName);
                        RaiseEvent(myargs);
                    }
                    else
                    {
                        this.Visibility = Visibility.Visible;
                        WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Item Linked To Tax", 0, "ERROR");
                        MsgBox.ShowDialog();
                    }
                }
                else if (this.Mode == "U")
                {
                    WinMessageBox MsgBox = new WinMessageBox("Functionality Yet to be Added", "Modify Tax Item", 0, "INFORMATION");
                    MsgBox.ShowDialog();
                }
            }
        }

        public void DisplayTaxItem(Tax Tax, TaxItem TaxItem, out string errorMessage)
        {
            errorMessage = null;

            if (Mode == "A")
            {
                tb_TaxCode.Text = Tax.TaxCode.ToString();

                TaxItem TaxItemObj = new TaxItem();

                TaxItemFactoryClient TaxItemFactoryClient = new TaxItemFactoryClient();

                TaxItemObj.TaxCode = tb_TaxCode.Text;
                if (TaxItemFactoryClient.GetItemsNotLnkdToSpecificTaxCode(out ItemList, out errorMessage) == false)
                {
                    if ((errorMessage != "") && (errorMessage != null))
                    {
                        WinMessageBox MsgBox = new WinMessageBox(errorMessage, "View Tax", 0, "ERROR");
                        MsgBox.ShowDialog();
                        return;
                    }
                }

                cmb_ItemId.ItemsSource = ItemList.DefaultView;
            }
            else if (Mode == "U")
            {
                tb_TaxCode.Text = TaxItem.TaxCode;
                cmb_ItemId.Text = TaxItem.ItemId;
                date_StartDate.Text = TaxItem.StartDate.ToString();
                date_EndDate.Text = TaxItem.EndDate.ToString();
            }
            else if (Mode == "V")
            {
                tb_TaxCode.Text = TaxItem.TaxCode;
                cmb_ItemId.Text = TaxItem.ItemId;
                date_StartDate.Text = TaxItem.StartDate.ToString();
                date_EndDate.Text = TaxItem.EndDate.ToString();
            }
        }

        private void cmb_ItemId_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void cmb_ItemId_LostFocus(object sender, RoutedEventArgs e)
        {
            string str = "";

            str = "ItemId = '" + cmb_ItemId.Text + "'";

            if (ItemList != null)
            {
                if (ItemList.Rows.Count > 0)
                {
                    DataTable newTable;
                    newTable = ItemList.Clone();

                    DataRow[] dr = ItemList.Select(str);

                    foreach (DataRow row in dr)
                    {
                        newTable.ImportRow(row);
                    }

                    cmb_BatchId.ItemsSource = newTable.DefaultView;
                }
            }
        }
    }
}
