using NewApp.BusinessTier.Models;
using NewApp.DiscountFactorySvc;
using NewApp.DiscountItemFactorySvc;
using NewApp.UI.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace NewApp.UI.UserControls
{
    /// <summary>
    /// Interaction logic for SalesMainCtrl.xaml
    /// </summary>
    public partial class DiscMainCtrl : UserControl
    {
        List<Discount> DiscObjList;

        public static readonly RoutedEvent DiscMainWithCustomArgsEvent = EventManager.RegisterRoutedEvent("DiscMainWithCustomArgs", RoutingStrategy.Bubble, typeof(DiscMainWithCustomArgsEventHandler), typeof(DiscMainCtrl));
        public delegate void DiscMainWithCustomArgsEventHandler(object sender, ProcessEventArgs e);


        public delegate void ProcessHandler(object sender, ProcessEventArgs data);
        string _mode;
        string _invokingCtrlMode;
        string _invokingCtrlName;
        string _CtrlTabName;

        UserCtrlArgs UCArgs;

        public DiscMainCtrl()
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

        public event DiscMainWithCustomArgsEventHandler DiscMainWithCustomArgs
        {
            add { AddHandler(DiscMainWithCustomArgsEvent, value); }
            remove { RemoveHandler(DiscMainWithCustomArgsEvent, value); }
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
            ProcessEventArgs myargs = new ProcessEventArgs(DiscMainWithCustomArgsEvent, UCArgs.InvokingControlName, UCArgs.InvokingControlMode, UCArgs.TargetControlName, UCArgs.TargetControlMode, null, null, UCArgs.ControlTabName);
            RaiseEvent(myargs);
        }

        private void btn_AddDisc_Click(object sender, RoutedEventArgs e)
        {
            UCArgs = new UserCtrlArgs();
            UCArgs.InvokingControlName = this.Name;
            UCArgs.InvokingControlMode = this.Mode;
            UCArgs.TargetControlName = "MyDiscDtlCtrl";
            UCArgs.TargetControlMode = "A";
            UCArgs.ControlTabName = this.ControlTabName;

            ProcessEventArgs myargs = new ProcessEventArgs(DiscMainWithCustomArgsEvent, UCArgs.InvokingControlName, UCArgs.InvokingControlMode, UCArgs.TargetControlName, UCArgs.TargetControlMode, null, null, UCArgs.ControlTabName);
            RaiseEvent(myargs);
        }

        private void btn_ModifyDisc_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dg_DiscMain.SelectedIndex > -1)
                {
                    if (this.Parent != null && this.Parent is StackPanel)
                    {
                        StackPanel parentControl = this.Parent as StackPanel;
                        foreach (UIElement child in parentControl.Children)
                        {
                            if (child is DiscDtlCtrl)
                            {
                                UserCtrlArgs Args = new UserCtrlArgs();
                                Args.TargetControlName = "DiscDtlCtrl";
                                Args.TargetControlMode = "U";

                                Args.InvokingControlName = this.Name;
                                Args.InvokingControlMode = this.Mode;

                                Discount DiscObj = (Discount)DiscObjList[dg_DiscMain.SelectedIndex];
                                ((DiscDtlCtrl)child).ClearControls();
                                ((DiscDtlCtrl)child).DisplayDiscount(DiscObj);

                                //this.NotifyedUserControl(this, Args);
                            }
                        }
                    }
                }
            }
            catch (Exception ex1)
            {
                WinMessageBox MsgBox = new WinMessageBox(ex1.Message.ToString(), "Discount Main - Modify", 0, "ERROR");
                MsgBox.ShowDialog();
            }

        }

        private void btn_DeleteDisc_Click(object sender, RoutedEventArgs e)
        {
            string errorMessage = null;

            try
            {
                if (dg_DiscMain.SelectedIndex > -1)
                {
                    Discount DiscObj = (Discount)DiscObjList[dg_DiscMain.SelectedIndex];

                    DiscountFactoryClient DiscountFactoryClient = new DiscountFactoryClient();

                    if (DiscountFactoryClient.DeleteDiscDetails(out errorMessage, DiscObj) == true)
                    {
                        WinMessageBox MsgBox = new WinMessageBox("New Discount Record Deleted Successfully!", "Discount Main - Delete", 0, "SUCCESS");
                        MsgBox.ShowDialog();

                        SearchDiscounts(txt_DiscCode.Text);
                    }
                    else
                    {
                        WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Discount Main - Delete", 0, "ERROR");
                        MsgBox.ShowDialog();
                        return;
                    }
                }
            }
            catch (Exception ex1)
            {
                WinMessageBox MsgBox = new WinMessageBox(ex1.Message.ToString(), "Discount Main - Delete", 0, "ERROR");
                MsgBox.ShowDialog();
            }
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.Visibility == Visibility.Visible)
            {
                dg_DiscMain.ItemsSource = "";
                if (txt_DiscCode.Text != "")
                    SearchDiscounts(txt_DiscCode.Text);
                else
                    dg_DiscMain.ItemsSource = "";
            }
        }

        private void btn_LinkItems_Click(object sender, RoutedEventArgs e)
        {
            UserCtrlArgs Args = new UserCtrlArgs();

            if (dg_DiscMain.SelectedIndex > -1)
            {
                if (this.Parent != null && this.Parent is StackPanel)
                {
                    StackPanel parentControl = this.Parent as StackPanel;
                    foreach (UIElement child in parentControl.Children)
                    {
                        if (child is DiscItemCtrl)
                        {
                            UCArgs = new UserCtrlArgs();
                            UCArgs.InvokingControlName = this.Name;
                            UCArgs.InvokingControlMode = this.Mode;
                            UCArgs.TargetControlName = "MyDiscItemCtrl";
                            UCArgs.TargetControlMode = "A";
                            UCArgs.ControlTabName = this.ControlTabName;

                            ProcessEventArgs myargs = new ProcessEventArgs(DiscMainWithCustomArgsEvent, UCArgs.InvokingControlName, UCArgs.InvokingControlMode, UCArgs.TargetControlName, UCArgs.TargetControlMode, null, null, UCArgs.ControlTabName);
                            RaiseEvent(myargs);

                            Discount Disc = (Discount)DiscObjList[dg_DiscMain.SelectedIndex];
                            DiscountItem DiscItem = new DiscountItem();

                            DiscItem.DiscountCode = Disc.DiscountCode;

                            ((DiscItemCtrl)child).DisplayDiscItem(DiscItem);
                        }
                    }
                }
            }
        }

        private void btn_ModLinkDtls_Click(object sender, RoutedEventArgs e)
        {
            WinMessageBox MsgBox = new WinMessageBox("Functionality Yet to be Added", "Modify Discount", 0, "INFORMATION");
            MsgBox.ShowDialog();

        }

        private void btn_DelinkItemsToDisc_Click(object sender, RoutedEventArgs e)
        {
            string errorMessage = null;

            if (dg_DiscItems.SelectedIndex > -1)
            {
                DiscountItem DiscItem = (DiscountItem)dg_DiscItems.Items[dg_DiscItems.SelectedIndex];

                DiscountItemFactoryClient DiscountItemFactoryClient = new DiscountItemFactoryClient();

                if (DiscountItemFactoryClient.DeleteDiscountDetails(out errorMessage, DiscItem) == true)
                {
                    WinMessageBox MsgBox = new WinMessageBox("Discount Item Record Deleted Successfully!", "Delete Discount Item", 0, "SUCCESS");
                    MsgBox.ShowDialog();


                    dg_DiscItems.ItemsSource = "";
                    SearchDiscounts(txt_DiscCode.Text);
                }
                else
                {
                    WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Delete Discount Item", 0, "ERROR");
                    MsgBox.ShowDialog();
                    return;
                }
            }

        }

        private void btn_ViewLinkDetails_Click(object sender, RoutedEventArgs e)
        {
            if (dg_DiscItems.SelectedIndex > -1)
            {
                if (this.Parent != null && this.Parent is StackPanel)
                {
                    StackPanel parentControl = this.Parent as StackPanel;
                    foreach (UIElement child in parentControl.Children)
                    {
                        if (child is DiscItemCtrl)
                        {
                            UCArgs = new UserCtrlArgs();
                            UCArgs.InvokingControlName = this.Name;
                            UCArgs.InvokingControlMode = this.Mode;
                            UCArgs.TargetControlName = "MyDiscItemCtrl";
                            UCArgs.TargetControlMode = "A";
                            UCArgs.ControlTabName = this.ControlTabName;

                            ProcessEventArgs myargs = new ProcessEventArgs(DiscMainWithCustomArgsEvent, UCArgs.InvokingControlName, UCArgs.InvokingControlMode, UCArgs.TargetControlName, UCArgs.TargetControlMode, null, null, UCArgs.ControlTabName);
                            RaiseEvent(myargs);

                            DiscountItem DiscItem = (DiscountItem)dg_DiscItems.Items[dg_DiscItems.SelectedIndex];
                        }
                    }
                }
            }
        }

        private void txt_DiscCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            dg_DiscMain.ItemsSource = "";
            if (txt_DiscCode.Text != "")
            {
                SearchDiscounts(txt_DiscCode.Text);
                if (dg_DiscMain.Items.Count > 0)
                    dg_DiscMain.SelectedIndex = 0;
            }
            else
                dg_DiscMain.ItemsSource = "";

            this.Focus();
        }

        private void dg_DiscMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg_DiscMain.SelectedIndex >= 0)
            {
                if (DiscObjList[dg_DiscMain.SelectedIndex] != null)
                {
                    this.DataContext = DiscObjList;
                    if (DiscObjList.Count > 0)
                        dg_DiscItems.ItemsSource = DiscObjList[dg_DiscMain.SelectedIndex].DiscountItemsList;
                    else
                        dg_DiscItems.ItemsSource = "";
                }
            }
        }

        public void SearchDiscounts(string Str)
        {
            string errorMessage = null;

            Discount[] DiscObjArray;
            DiscObjList = new List<Discount>();

            DiscountFactoryClient DiscountFactoryClient = new DiscountFactoryClient();

            if (DiscountFactoryClient.GetDiscountList(out errorMessage, out DiscObjArray, Str) == true)
            {
                if (DiscObjArray != null)
                {
                    DiscObjList = DiscObjArray.ToList<Discount>();
                    this.DataContext = DiscObjList;
                    if (DiscObjList.Count > 0)
                        dg_DiscMain.ItemsSource = DiscObjList;
                    else
                        dg_DiscMain.ItemsSource = "";
                }
            }
            else
            {
                if ((errorMessage != "") && (errorMessage != null))
                {
                    WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Search Discount", 0, "ERROR");
                    MsgBox.ShowDialog();
                    return;
                }
            }
        }

        public void ClearControls()
        {
            txt_DiscCode.Text = "";
        }
    }
}
