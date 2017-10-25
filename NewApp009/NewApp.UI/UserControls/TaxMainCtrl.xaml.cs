using NewApp.BusinessTier.Models;
using NewApp.TaxFactorySvc;
using NewApp.UI.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace NewApp.UI.UserControls
{
    /// <summary>
    /// Interaction logic for SalesMainCtrl.xaml
    /// </summary>
    public partial class TaxMainCtrl : UserControl
    {
        List<Tax> TaxObjList;

        public static readonly RoutedEvent TaxMainWithCustomArgsEvent = EventManager.RegisterRoutedEvent("TaxMainWithCustomArgs", RoutingStrategy.Bubble, typeof(TaxMainWithCustomArgsEventHandler), typeof(TaxMainCtrl));
        public delegate void TaxMainWithCustomArgsEventHandler(object sender, ProcessEventArgs e);


        public delegate void ProcessHandler(object sender, ProcessEventArgs data);
        string _mode;
        string _invokingCtrlMode;
        string _invokingCtrlName;
        string _CtrlTabName;

        UserCtrlArgs UCArgs;

        public TaxMainCtrl()
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

        public event TaxMainWithCustomArgsEventHandler TaxMainWithCustomArgs
        {
            add { AddHandler(TaxMainWithCustomArgsEvent, value); }
            remove { RemoveHandler(TaxMainWithCustomArgsEvent, value); }
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
            ProcessEventArgs myargs = new ProcessEventArgs(TaxMainWithCustomArgsEvent, UCArgs.InvokingControlName, UCArgs.InvokingControlMode, UCArgs.TargetControlName, UCArgs.TargetControlMode, null, null, UCArgs.ControlTabName);
            RaiseEvent(myargs);
        }

        private void btn_AddTax_Click(object sender, RoutedEventArgs e)
        {
            UserCtrlArgs Args = new UserCtrlArgs();
            UCArgs = new UserCtrlArgs();
            UCArgs.InvokingControlName = this.Name;
            UCArgs.InvokingControlMode = this.Mode;
            UCArgs.TargetControlName = "MyTaxDtlCtrl";
            UCArgs.TargetControlMode = "A";
            UCArgs.ControlTabName = this.ControlTabName;

            ProcessEventArgs myargs = new ProcessEventArgs(TaxMainWithCustomArgsEvent, UCArgs.InvokingControlName, UCArgs.InvokingControlMode, UCArgs.TargetControlName, UCArgs.TargetControlMode, null, null, UCArgs.ControlTabName);
            RaiseEvent(myargs);
        }

        private void btn_LinkItems_Click(object sender, RoutedEventArgs e)
        {
            string errorMessage;  

            UserCtrlArgs Args = new UserCtrlArgs();

            if (dg_TaxList.SelectedIndex > -1)
            {
                if (this.Parent != null && this.Parent is StackPanel)
                {
                    StackPanel parentControl = this.Parent as StackPanel;
                    foreach (UIElement child in parentControl.Children)
                    {
                        if (child is TaxItemCtrl)
                        {
                            UCArgs = new UserCtrlArgs();
                            UCArgs.InvokingControlName = this.Name;
                            UCArgs.InvokingControlMode = this.Mode;
                            UCArgs.TargetControlName = "MyTaxItemCtrl";
                            UCArgs.TargetControlMode = "A";
                            UCArgs.ControlTabName = this.ControlTabName;

                            ProcessEventArgs myargs = new ProcessEventArgs(TaxMainWithCustomArgsEvent, UCArgs.InvokingControlName, UCArgs.InvokingControlMode, UCArgs.TargetControlName, UCArgs.TargetControlMode, null, null, UCArgs.ControlTabName);
                            RaiseEvent(myargs);

                            Tax Tax = (Tax)TaxObjList[dg_TaxList.SelectedIndex];
                            TaxItem TaxItem = new TaxItem();

                            ((TaxItemCtrl)child).DisplayTaxItem(Tax, TaxItem, out errorMessage);

                            if ((errorMessage != "") && (errorMessage != null))
                            {
                                WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Link Item To Tax", 0, "ERROR");
                                MsgBox.ShowDialog();
                            }
                        }
                    }
                }
            }
        }

        private void btn_ModifyTax_Click(object sender, RoutedEventArgs e)
        {
            WinMessageBox MsgBox = new WinMessageBox("Functionality Yet to be Added", "Modify Tax", 0, "INFORMATION");
            MsgBox.ShowDialog();

        }

        private void btn_DeleteTax_Click(object sender, RoutedEventArgs e)
        {
            string errorMessage = null;


            if (dg_TaxList.SelectedIndex > -1)
            {
                Tax Tax = (Tax)TaxObjList[dg_TaxList.SelectedIndex];

                TaxFactoryClient TaxFactoryClient = new TaxFactoryClient();

                if (TaxFactoryClient.DeleteTaxDetails(out errorMessage, Tax) == true)
                {
                    WinMessageBox MsgBox = new WinMessageBox("Tax Details Deleted Successfully!", "Delete Tax", 0, "SUCCESS");
                    MsgBox.ShowDialog();


                    dg_TaxList.ItemsSource = "";
                    SearchTaxs(txt_TaxCode.Text);
                }
                else
                {
                    WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Delete Tax", 0, "ERROR");
                    MsgBox.ShowDialog();
                }
            }
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.Visibility == Visibility.Visible)
            {
                dg_TaxList.ItemsSource = "";
                if (txt_TaxCode.Text != "")
                    SearchTaxs(txt_TaxCode.Text);
                else
                    dg_TaxList.ItemsSource = "";
            }
        }

        private void btn_ModLinkDtls_Click(object sender, RoutedEventArgs e)
        {
            WinMessageBox MsgBox = new WinMessageBox("Functionality Yet to be Added", "Modify Tax Link to Item", 0, "INFORMATION");
            MsgBox.ShowDialog();
        }

        private void btn_ViewLinkDetails_Click(object sender, RoutedEventArgs e)
        {
            UserCtrlArgs Args = new UserCtrlArgs();

            if (dg_TaxItems.SelectedIndex > -1)
            {
                if (this.Parent != null && this.Parent is StackPanel)
                {
                    StackPanel parentControl = this.Parent as StackPanel;
                    foreach (UIElement child in parentControl.Children)
                    {
                        if (child is TaxItemCtrl)
                        {
                            UCArgs = new UserCtrlArgs();
                            UCArgs.InvokingControlName = this.Name;
                            UCArgs.InvokingControlMode = this.Mode;
                            UCArgs.TargetControlName = "MyTaxItemCtrl";
                            UCArgs.TargetControlMode = "A";
                            UCArgs.ControlTabName = this.ControlTabName;

                            ProcessEventArgs myargs = new ProcessEventArgs(TaxMainWithCustomArgsEvent, UCArgs.InvokingControlName, UCArgs.InvokingControlMode, UCArgs.TargetControlName, UCArgs.TargetControlMode, null, null, UCArgs.ControlTabName);
                            RaiseEvent(myargs);

                            TaxItem Tax = (TaxItem)TaxObjList[dg_TaxList.SelectedIndex].TaxItemList[dg_TaxItems.SelectedIndex];
                        }
                    }
                }
            }
        }

        private void txt_TaxCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            dg_TaxList.ItemsSource = "";
            if (txt_TaxCode.Text != "")
            {
                SearchTaxs(txt_TaxCode.Text);
                if (dg_TaxList.Items.Count > 0)
                    dg_TaxList.SelectedIndex = 0;
            }
            else
                dg_TaxList.ItemsSource = "";

            this.Focus();
        }

        public void SearchTaxs(string Str)
        {
            string errorMessage = null;

            btn_LinkItems.IsEnabled = false;
            dg_TaxList.ItemsSource = "";
            dg_TaxItems.ItemsSource = "";

            Tax Tax = new Tax();
            Tax[] TaxObjArray;
            TaxObjList = new List<Tax>();

            TaxFactoryClient TaxFactoryClient = new TaxFactoryClient();

            if (TaxFactoryClient.GetTaxList(out errorMessage, out TaxObjArray, Str) == true)
            {
                if (TaxObjArray != null)
                {
                    TaxObjList = TaxObjArray.ToList<Tax>();
                    this.DataContext = TaxObjList;
                    if (TaxObjList.Count > 0)
                    {
                        dg_TaxList.ItemsSource = TaxObjList;
                        dg_TaxList.SelectedIndex = 0;
                        if (TaxObjList[dg_TaxList.SelectedIndex].TaxKind.TaxKindId == 1)
                            btn_LinkItems.IsEnabled = true;
                    }
                }
            }
            else
                if ((errorMessage != "") && (errorMessage != null))
                {
                    WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Search Taxes", 0, "ERROR");
                    MsgBox.ShowDialog();
                }
        }

        private void dg_TaxList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg_TaxList.SelectedIndex >= 0)
            {
                if (TaxObjList[dg_TaxList.SelectedIndex] != null)
                {
                    this.DataContext = TaxObjList;
                    if (TaxObjList.Count > 0)
                        dg_TaxItems.ItemsSource = TaxObjList[dg_TaxList.SelectedIndex].TaxItemList;
                    else
                        dg_TaxItems.ItemsSource = "";

                    if (TaxObjList[dg_TaxList.SelectedIndex].TaxKind.TaxKindId == 1)
                        btn_LinkItems.IsEnabled = true;
                    else
                    {
                        btn_LinkItems.IsEnabled = false;
                        dg_TaxItems.ItemsSource = "";
                    }
                }
            }
        }

        private void btn_DelinkItemsToTax_Click(object sender, RoutedEventArgs e)
        {
            WinMessageBox MsgBox = new WinMessageBox("Functionality Yet to be Added", "Modify Tax Link to Item", 0, "INFORMATION");
            MsgBox.ShowDialog();
        }
    }
}
