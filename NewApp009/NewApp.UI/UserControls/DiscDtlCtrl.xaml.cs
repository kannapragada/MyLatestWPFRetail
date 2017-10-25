using NewApp.BusinessTier.Common;
using NewApp.BusinessTier.Models;
using NewApp.DiscountFactorySvc;
using NewApp.UI.Windows;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;



namespace NewApp.UI.UserControls
{
    /// <summary>
    /// Interaction logic for SalesMainCtrl.xaml
    /// </summary>
    public partial class DiscDtlCtrl : UserControl
    {
        Discount DiscObj;

        private Discount _discount = new Discount();

        public static readonly RoutedEvent DiscDtlWithCustomArgsEvent = EventManager.RegisterRoutedEvent("DiscDtlWithCustomArgs", RoutingStrategy.Bubble, typeof(DiscDtlWithCustomArgsEventHandler), typeof(DiscDtlCtrl));
        public delegate void DiscDtlWithCustomArgsEventHandler(object sender, ProcessEventArgs e);


        public delegate void ProcessHandler(object sender, ProcessEventArgs data);
        string _mode;
        string _invokingCtrlMode;
        string _invokingCtrlName;
        string _CtrlTabName;

        UserCtrlArgs UCArgs;



        public DiscDtlCtrl()
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

        public event DiscDtlWithCustomArgsEventHandler DiscDtlWithCustomArgs
        {
            add { AddHandler(DiscDtlWithCustomArgsEvent, value); }
            remove { RemoveHandler(DiscDtlWithCustomArgsEvent, value); }
        }

        public void DisplayDiscount(Discount Discount)
        {
            tb_DiscCode.Text = Discount.DiscountCode.ToString();
            txt_DiscName.Text = Discount.DiscountName.ToString();
            txt_DiscDescription.Text = Discount.DiscountDescription.ToString();
            cmb_DiscKind.Text = Discount.DiscountKind.DiscountKindValue.ToString();
            cmb_DiscKind.Tag = Convert.ToInt32(Discount.DiscountKind.DiscountKindId);
            cmb_DiscType.Text = Discount.DiscountType.DiscountTypeValue.ToString();
            cmb_DiscType.Tag = Convert.ToInt32(Discount.DiscountType.DiscountTypeId);
            txt_DiscValue.Text = Discount.DiscountValue.ToString();
            date_DiscStartDate.Text = Discount.DiscStartDate.ToString();
            date_DiscEndDate.Text = Discount.DiscEndDate.ToString();
            txt_CreateDate.Text = Discount.DiscCreateDate.ToString();
            txt_LastModDate.Text = Discount.DiscLastModDate.ToString();
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
                    ProcessEventArgs myargs = new ProcessEventArgs(DiscDtlWithCustomArgsEvent, UCArgs.InvokingControlName, UCArgs.InvokingControlMode, UCArgs.TargetControlName, UCArgs.TargetControlMode, null, null, UCArgs.ControlTabName);
                    RaiseEvent(myargs);
                }
            }
            else
            {
                e.Handled = true;
                ProcessEventArgs myargs = new ProcessEventArgs(DiscDtlWithCustomArgsEvent, UCArgs.InvokingControlName, UCArgs.InvokingControlMode, UCArgs.TargetControlName, UCArgs.TargetControlMode, null, null, UCArgs.ControlTabName);
                RaiseEvent(myargs);
            }
        }

        public void ClearControls()
        {
            this.tb_DiscCode.Text = "";
            this.txt_DiscName.Text = "";
            this.txt_DiscDescription.Text = "";
            this.cmb_DiscKind.Text = "";
            this.cmb_DiscType.Text = "";
            this.txt_DiscValue.Text = "";
            this.txt_DiscRemarks.Text = "";
            this.date_DiscStartDate.Text = "";
            this.date_DiscEndDate.Text = "";
            this.txt_CreateDate.Text = "";
            this.txt_LastModDate.Text = "";
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ClearControls();
        }

        private void btn_DiscCommit_Click(object sender, RoutedEventArgs e)
        {
            DiscObj = new Discount();
            //grid_DiscDtlInput1.DataContext = _discount;

            string errorMessage = null;

            if (this.Mode == "A")
            {
                try
                {
                    DiscObj.DiscountCode = tb_DiscCode.Text;
                    DiscObj.DiscountName = txt_DiscName.Text;
                    DiscObj.DiscountDescription = txt_DiscDescription.Text;

                    List<DiscountKind> DiscKindList = new List<DiscountKind>();
                    if (BusinessTierState.GetValue<List<DiscountKind>>("DiscKindList", out DiscKindList, out errorMessage) == true)
                        DiscObj.DiscountKind = DiscKindList.Find(DiscountKind => DiscountKind.DiscountKindId == Convert.ToInt32(cmb_DiscKind.SelectedValue));
                    else
                    {
                        WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Add Discount", 0, "ERROR");
                        MsgBox.ShowDialog();
                        return;
                    }

                    List<DiscountType> DiscTypeList = new List<DiscountType>();
                    if (BusinessTierState.GetValue<List<DiscountType>>("DiscTypeList", out DiscTypeList, out errorMessage) == true)
                        DiscObj.DiscountType = DiscTypeList.Find(DiscountType => DiscountType.DiscountTypeId == Convert.ToInt32(cmb_DiscType.SelectedValue));
                    else
                    {
                        WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Add Discount", 0, "ERROR");
                        MsgBox.ShowDialog();
                        return;
                    }

                    DiscObj.DiscountValue = Convert.ToDouble(txt_DiscValue.Text);
                    DiscObj.DiscStartDate = Convert.ToDateTime(date_DiscStartDate.Text);
                    DiscObj.DiscEndDate = Convert.ToDateTime(date_DiscEndDate.Text);
                    DiscObj.DiscCreateDate = DateTime.Now;
                    DiscObj.DiscLastModDate = DateTime.Now;


                    DiscountFactoryClient DiscountFactoryClient = new DiscountFactoryClient();

                    if (DiscountFactoryClient.AddNewDiscount(out errorMessage, DiscObj) == true)
                    {
                        WinMessageBox MsgBox = new WinMessageBox("New Discount Record Added Successfully!", "Add Discount", 0, "SUCCESS");
                        MsgBox.ShowDialog();

                        ClearControls();

                        UCArgs = new UserCtrlArgs();
                        UCArgs.InvokingControlName = this.Name;
                        UCArgs.InvokingControlMode = this.Mode;
                        UCArgs.TargetControlName = this.InvokingControlName;
                        UCArgs.TargetControlMode = this.InvokingControlMode;

                        ProcessEventArgs myargs = new ProcessEventArgs(DiscDtlWithCustomArgsEvent, UCArgs.InvokingControlName, UCArgs.InvokingControlMode, UCArgs.TargetControlName, UCArgs.TargetControlMode, null, null, UCArgs.ControlTabName);
                        RaiseEvent(myargs);
                    }
                    else
                    {
                        WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Add Discount", 0, "ERROR");
                        MsgBox.ShowDialog();
                        return;
                    }
                }
                catch (Exception ex1)
                {
                    WinMessageBox MsgBox = new WinMessageBox(ex1.Message.ToString(), "Add Discount", 0, "ERROR");
                    MsgBox.ShowDialog();
                }
            }
            else if (this.Mode == "U")
            {
                WinMessageBox MsgBox = new WinMessageBox("Functionality Yet to be Added", "Modify Discount", 0, "INFORMATION");
                MsgBox.ShowDialog();
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            string errorMessage = null;

            List<DiscountType> DiscountTypeList = new List<DiscountType>();
            if (BusinessTierState.GetValue<List<DiscountType>>("DiscountTypeList", out DiscountTypeList, out errorMessage) == true)
                cmb_DiscType.ItemsSource = DiscountTypeList;
            else
            {
                WinMessageBox MsgBox = new WinMessageBox("Unable to Load Discount Type Parameters", "Discount Main", 0, "ERROR");
                MsgBox.ShowDialog();
            }
        }
    }
}
