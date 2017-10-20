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
using NewApp.DiscountFactorySvc;
using NewApp.DiscountItemFactorySvc;
using NewApp.DiscountTypeFactorySvc;


namespace NewApp.UI.UserControls
{
    /// <summary>
    /// Interaction logic for SalesMainCtrl.xaml
    /// </summary>
    public partial class DiscItemCtrl : UserControl
    {
        DataTable ItemList;

        public static readonly RoutedEvent DiscItemWithCustomArgsEvent = EventManager.RegisterRoutedEvent("DiscItemWithCustomArgs", RoutingStrategy.Bubble, typeof(DiscItemWithCustomArgsEventHandler), typeof(DiscItemCtrl));
        public delegate void DiscItemWithCustomArgsEventHandler(object sender, ProcessEventArgs e);


        public delegate void ProcessHandler(object sender, ProcessEventArgs data);
        string _mode;
        string _invokingCtrlMode;
        string _invokingCtrlName;
        string _CtrlTabName;

        UserCtrlArgs UCArgs;

        public DiscItemCtrl()
        {
            InitializeComponent();
        }

        public string Mode
        {
            get{return _mode;}
            set{_mode = value;}
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

        public event DiscItemWithCustomArgsEventHandler DiscItemWithCustomArgs
        {
            add { AddHandler(DiscItemWithCustomArgsEvent, value); }
            remove { RemoveHandler(DiscItemWithCustomArgsEvent, value); }
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
                    ProcessEventArgs myargs = new ProcessEventArgs(DiscItemWithCustomArgsEvent, UCArgs.InvokingControlName, UCArgs.InvokingControlMode, UCArgs.TargetControlName, UCArgs.TargetControlMode, null, null, UCArgs.ControlTabName);
                    RaiseEvent(myargs);
                }
            }
            else
            {
                e.Handled = true;
                ProcessEventArgs myargs = new ProcessEventArgs(DiscItemWithCustomArgsEvent, UCArgs.InvokingControlName, UCArgs.InvokingControlMode, UCArgs.TargetControlName, UCArgs.TargetControlMode, null, null, UCArgs.ControlTabName);
                RaiseEvent(myargs);
            }
        }

        private void btn_DiscItemCommit_Click(object sender, RoutedEventArgs e)
        {
            string errorMessage = null;

            if ((this.Mode == "A") || (this.Mode == "U"))
            {
                DiscountItem newitem = new DiscountItem();

                DiscountItemFactoryClient DiscountItemFactoryClient = new DiscountItemFactoryClient();

                newitem.DiscountCode = tb_DiscCode.Text;
                newitem.DiscItemId = cmb_ItemId.Text;
                newitem.DiscItemBatchId = cmb_ItemBatchId.Text;
                newitem.DiscRemarks = txt_Remarks.Text;
                newitem.DiscItemStartDate = Convert.ToDateTime(this.date_DiscStartDate.Text);
                newitem.DiscItemEndDate = Convert.ToDateTime(this.date_DiscEndDate.Text);
                newitem.DiscCreateDate = DateTime.Today;
                newitem.DiscLastModDate = DateTime.Today;

                if (this.Mode == "A")
                {
                    if (DiscountItemFactoryClient.AddItemToDiscount(out errorMessage, newitem) == true)
                    {
                        this.Visibility = Visibility.Visible;
                        string msg = "New Item " + newitem.DiscItemId + " With Batch Id: " + newitem.DiscItemBatchId + " Linked to " + newitem.DiscountCode + " Discount Code Successfully";

                        WinMessageBox MsgBox = new WinMessageBox(msg, "Link Discount to Item", 0, "SUCCESS");
                        MsgBox.ShowDialog();


                        RemoveItemFromItemList();
                        ClearControls();

                        UCArgs = new UserCtrlArgs();
                        UCArgs.InvokingControlName = this.Name;
                        UCArgs.InvokingControlMode = this.Mode;
                        UCArgs.TargetControlName = this.InvokingControlName;
                        UCArgs.TargetControlMode = this.InvokingControlMode;
                        UCArgs.ControlTabName = this.ControlTabName;

                        ProcessEventArgs myargs = new ProcessEventArgs(DiscItemWithCustomArgsEvent, UCArgs.InvokingControlName, UCArgs.InvokingControlMode, UCArgs.TargetControlName, UCArgs.TargetControlMode, null, null, UCArgs.ControlTabName);
                        RaiseEvent(myargs);
                    }
                    else
                    {
                        WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Link Discount to Item", 0, "ERROR");
                        MsgBox.ShowDialog();
                        return;
                    }
                }
                else if (this.Mode == "U")
                {
                    WinMessageBox MsgBox = new WinMessageBox("Functionality Yet to be Added", "Link Discount to Item", 0, "INFORMATION");
                    MsgBox.ShowDialog();
                }
            }
        }

        private void cmb_ItemBatchId_GotFocus(object sender, RoutedEventArgs e)
        {

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

                    int i = 0;
                    foreach (DataRow row in dr)
                    {
                        if (i == 0)
                            tb_ItemName.Text = row[1].ToString();

                        newTable.ImportRow(row);
                    }

                    cmb_ItemBatchId.ItemsSource = newTable.DefaultView;

                    cmb_ItemBatchId.DisplayMemberPath = "BatchId";

                    i = i + 1;
                }
            }
        }
        
        public void ClearControls()
        {
            //tb_DiscCode.Text = "";
            cmb_ItemId.Text = "";
            tb_ItemName.Text = "";
            cmb_ItemBatchId.Text = "";
            date_DiscStartDate.Text = "";
            date_DiscEndDate.Text = "";
            tb_CreateDate.Text = "";
            tb_LastModDate.Text = "";
        }

        public void DisplayDiscItem(DiscountItem DiscItemObj)
        {
            string errorMessage = null;

            if (Mode == "A")
            {
                tb_DiscCode.Text = DiscItemObj.DiscountCode.ToString();

                DiscItemObj = new DiscountItem();

                DiscItemObj.DiscountCode = tb_DiscCode.Text;

                DiscountItemFactoryClient DiscountItemFactoryClient = new DiscountItemFactoryClient();


                if (DiscountItemFactoryClient.GetItemsNotLnkdToSpecificDiscCode(out ItemList, out errorMessage) == false)
                {
                    if ((errorMessage != "") && (errorMessage != null))
                    {
                        WinMessageBox MsgBox = new WinMessageBox(errorMessage, "View Discount Item", 0, "ERROR");
                        MsgBox.ShowDialog();
                        return;
                    }
                }
                
                cmb_ItemId.ItemsSource = ItemList.DefaultView;
                cmb_ItemId.DisplayMemberPath = "ItemId";
            }
            else if (Mode == "U")
            {
                tb_DiscCode.Text = DiscItemObj.DiscountCode;
                cmb_ItemId.Text = DiscItemObj.DiscItemId;
                date_DiscStartDate.Text = DiscItemObj.DiscItemStartDate.ToString();
                date_DiscEndDate.Text = DiscItemObj.DiscItemEndDate.ToString();
            }
            else if (Mode == "V")
            {
                tb_DiscCode.Text = DiscItemObj.DiscountCode;
                cmb_ItemId.Text = DiscItemObj.DiscItemId;
                date_DiscStartDate.Text = DiscItemObj.DiscItemStartDate.ToString();
                date_DiscEndDate.Text = DiscItemObj.DiscItemEndDate.ToString();
            }
        }

        private void RemoveItemFromItemList()
        {
            string str = "";

            str = "ItemId = '" + cmb_ItemId.Text + "' AND BatchId = '" + cmb_ItemBatchId.Text + "'";

            if (ItemList != null)
            {
                if (ItemList.Rows.Count > 0)
                {
                    DataRow[] dr = ItemList.Select(str);
                        
                    foreach (DataRow row in dr)
                    {
                        ItemList.Rows.Remove(row);
                    }

                    cmb_ItemBatchId.ItemsSource = ItemList.DefaultView;
                }
            }
        }
    }
}
