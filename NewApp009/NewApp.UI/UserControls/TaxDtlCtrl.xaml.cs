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
using NewApp.BusinessTier.Common;
using NewApp.BusinessTier.Models;
using NewApp.UI.UserControls;
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
    /// Interaction logic for TaxDtlCtrl.xaml
    /// </summary>
    public partial class TaxDtlCtrl : UserControl
    {
        Tax TaxObj;

        private Tax _tax = new Tax();

        public static readonly RoutedEvent TaxDtlWithCustomArgsEvent = EventManager.RegisterRoutedEvent("TaxDtlWithCustomArgs", RoutingStrategy.Bubble, typeof(TaxDtlWithCustomArgsEventHandler), typeof(TaxDtlCtrl));
        public delegate void TaxDtlWithCustomArgsEventHandler(object sender, ProcessEventArgs e);


        public delegate void ProcessHandler(object sender, ProcessEventArgs data);
        string _mode;
        string _invokingCtrlMode;
        string _invokingCtrlName;
        string _CtrlTabName;

        UserCtrlArgs UCArgs;


        public TaxDtlCtrl()
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

        public event TaxDtlWithCustomArgsEventHandler TaxDtlWithCustomArgs
        {
            add { AddHandler(TaxDtlWithCustomArgsEvent, value); }
            remove { RemoveHandler(TaxDtlWithCustomArgsEvent, value); }
        }

        public void DisplayTax(Tax Tax)
        {
            tb_TaxCode.Text = Tax.TaxCode.ToString();
            txt_TaxName.Text = Tax.TaxName.ToString();
            txt_TaxDescription.Text = Tax.TaxDescription.ToString();
            cmb_TaxKind.SelectedIndex = Convert.ToInt32(Tax.TaxKind.TaxKindId);
            cmb_TaxType.SelectedIndex = Convert.ToInt32(Tax.TaxType.TaxTypeId);
            txt_TaxValue.Text = Tax.TaxValue.ToString();
            date_TaxStartDate.Text = Tax.StartDate.ToString();
            date_TaxEndDate.Text = Tax.EndDate.ToString();
            txt_CreateDate.Text = Tax.CreateDate.ToString();
            txt_LastModDate.Text = Tax.LastModDate.ToString();
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
                    ProcessEventArgs myargs = new ProcessEventArgs(TaxDtlWithCustomArgsEvent, UCArgs.InvokingControlName, UCArgs.InvokingControlMode, UCArgs.TargetControlName, UCArgs.TargetControlMode, null, null, UCArgs.ControlTabName);
                    RaiseEvent(myargs);
                }
            }
            else
            {
                e.Handled = true;
                ProcessEventArgs myargs = new ProcessEventArgs(TaxDtlWithCustomArgsEvent, UCArgs.InvokingControlName, UCArgs.InvokingControlMode, UCArgs.TargetControlName, UCArgs.TargetControlMode, null, null, UCArgs.ControlTabName);
                RaiseEvent(myargs);
            }
        }

        public void ClearControls()
        {
            this.tb_TaxCode.Text = "";
            this.txt_TaxName.Text = "";
            this.txt_TaxDescription.Text = "";
            this.cmb_TaxKind.Text = "";
            this.cmb_TaxType.Text = "";
            this.txt_TaxValue.Text = "";
            this.date_TaxStartDate.Text = "";
            this.date_TaxEndDate.Text = "";
            this.txt_CreateDate.Text = "";
            this.txt_LastModDate.Text = "";
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ClearControls();
        }

        private void btn_TaxCommit_Click(object sender, RoutedEventArgs e)
        {
            TaxObj = new Tax();
            grid_TaxDtlInput.DataContext = _tax;

            string errorMessage = null;

            if (this.Mode == "A")
            {
                TaxObj.TaxCode = tb_TaxCode.Text;
                TaxObj.TaxName = txt_TaxName.Text;
                TaxObj.TaxDescription = txt_TaxDescription.Text;

                List<TaxKind> TaxKindList = new List<TaxKind>();
                if (BusinessTierState.GetValue<List<TaxKind>>("TaxKindList", out TaxKindList, out errorMessage) == true)
                    TaxObj.TaxKind = TaxKindList.Find(TaxKind => TaxKind.TaxKindId == Convert.ToInt32(cmb_TaxKind.SelectedValue));
                else
                {
                    WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Add Tax", 0, "ERROR");
                    MsgBox.ShowDialog();
                    return;
                }

                List<TaxType> TaxTypeList = new List<TaxType>();
                if (BusinessTierState.GetValue<List<TaxType>>("TaxTypeList", out TaxTypeList, out errorMessage) == true)
                    TaxObj.TaxType = TaxTypeList.Find(TaxType => TaxType.TaxTypeId == Convert.ToInt32(cmb_TaxType.SelectedValue));
                else
                {
                    WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Add Tax", 0, "ERROR");
                    MsgBox.ShowDialog();
                    return;
                }

                TaxObj.TaxValue = Convert.ToDouble(txt_TaxValue.Text);
                if (date_TaxStartDate.Text != "") TaxObj.StartDate = Convert.ToDateTime(date_TaxStartDate.Text);
                if (date_TaxEndDate.Text != "") TaxObj.EndDate = Convert.ToDateTime(date_TaxEndDate.Text);
                TaxObj.CreateDate = DateTime.Now;

                TaxFactoryClient TaxFactoryClient = new TaxFactoryClient();

                if (TaxFactoryClient.AddNewTax(out errorMessage, TaxObj) == true)
                {
                    WinMessageBox MsgBox = new WinMessageBox("New Tax Record Added Successfully!", "Add Tax", 0, "SUCCESS");
                    MsgBox.ShowDialog();

                    ClearControls();

                    UCArgs = new UserCtrlArgs();
                    UCArgs.InvokingControlName = this.Name;
                    UCArgs.InvokingControlMode = this.Mode;
                    UCArgs.TargetControlName = this.InvokingControlName;
                    UCArgs.TargetControlMode = this.InvokingControlMode;
                    UCArgs.ControlTabName = this.ControlTabName;

                    ProcessEventArgs myargs = new ProcessEventArgs(TaxDtlWithCustomArgsEvent, UCArgs.InvokingControlName, UCArgs.InvokingControlMode, UCArgs.TargetControlName, UCArgs.TargetControlMode, null, null, UCArgs.ControlTabName);
                    RaiseEvent(myargs);
                }
                else
                {
                    if ((errorMessage != "") && (errorMessage != null))
                    {
                        WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Add Tax", 0, "ERROR");
                        MsgBox.ShowDialog();
                    }
                }
            }
            else if (this.Mode == "U")
            {
                WinMessageBox MsgBox = new WinMessageBox("Functionality Yet to be Added", "Modify Tax", 0, "INFORMATION");
                MsgBox.ShowDialog();
            }
            e.Handled = true;

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            string errorMessage = null;

            List<TaxKind> TaxKindList = new List<TaxKind>();
            if (BusinessTierState.GetValue<List<TaxKind>>("TaxKindList", out TaxKindList, out errorMessage) == true)
                cmb_TaxKind.ItemsSource = TaxKindList;
            else
            {
                WinMessageBox MsgBox = new WinMessageBox("Unable to Load Tax Kind Parameters", "Tax Main", 0, "ERROR");
                MsgBox.ShowDialog();
            }

            List<TaxType> TaxTypeList = new List<TaxType>();
            if (BusinessTierState.GetValue<List<TaxType>>("TaxTypeList", out TaxTypeList, out errorMessage) == true)
                cmb_TaxType.ItemsSource = TaxTypeList;
            else
            {
                WinMessageBox MsgBox = new WinMessageBox("Unable to Load Tax Type Parameters", "Tax Main", 0, "ERROR");
                MsgBox.ShowDialog();
            }
        }
    }
}
