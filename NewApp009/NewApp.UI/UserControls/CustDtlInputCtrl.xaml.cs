using Microsoft.Win32;
using NewApp.BusinessTier.Common;
using NewApp.BusinessTier.Models;
using NewApp.CustomerFactorySvc;
using NewApp.UI.Windows;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;



namespace NewApp.UI.UserControls
{
    /// <summary>
    /// Interaction logic for SalesCtrl.xaml
    /// </summary>
    public partial class CustDtlInputCtrl : UserControl
    {
        Customer CustObj;

        private Customer _customer;

        public static readonly RoutedEvent CustDtlInputWithCustomArgsEvent = EventManager.RegisterRoutedEvent("CustDtlInputWithCustomArgs", RoutingStrategy.Bubble, typeof(CustDtlInputWithCustomArgsEventHandler), typeof(CustDtlInputCtrl));
        public delegate void CustDtlInputWithCustomArgsEventHandler(object sender, ProcessEventArgs e);


        public delegate void ProcessHandler(object sender, ProcessEventArgs data);
        string _mode;
        string _invokingCtrlMode;
        string _invokingCtrlName;
        string _CtrlTabName;

        UserCtrlArgs UCArgs;


        public CustDtlInputCtrl()
        {
            InitializeComponent();

            _customer = new Customer();
        }

        public string Mode
        {
            get{return _mode;}
            set
            {
                _mode = value;

                if (_mode == "A")
                {
                    tb_CustAmtToBePaid.Visibility = Visibility.Collapsed;
                    tb_CustAmtPaidYTD.Visibility = Visibility.Collapsed;
                    tb_CreateDate.Visibility = Visibility.Collapsed;
                    tb_LastModDate.Visibility = Visibility.Collapsed;
                    txt_CustAmtToBePaid.Visibility = Visibility.Collapsed;
                    txt_CustAmtPaidYTD.Visibility = Visibility.Collapsed;
                    txt_CreateDate.Visibility = Visibility.Collapsed;
                    txt_LastModDate.Visibility = Visibility.Collapsed;
                    ClearControls();
                }
                else
                {
                    tb_CustAmtToBePaid.Visibility = Visibility.Visible;
                    tb_CustAmtPaidYTD.Visibility = Visibility.Visible;
                    tb_CreateDate.Visibility = Visibility.Visible;
                    tb_LastModDate.Visibility = Visibility.Visible;
                    txt_CustAmtToBePaid.Visibility = Visibility.Visible;
                    txt_CustAmtPaidYTD.Visibility = Visibility.Visible;
                    txt_CreateDate.Visibility = Visibility.Visible;
                    txt_LastModDate.Visibility = Visibility.Visible;
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

        public event CustDtlInputWithCustomArgsEventHandler CustDtlInputWithCustomArgs
        {
            add { AddHandler(CustDtlInputWithCustomArgsEvent, value); }
            remove { RemoveHandler(CustDtlInputWithCustomArgsEvent, value); }
        }

        public void DisplayCustomer(Customer Customer)
        {
            txt_CustId.Text = CustObj.CustomerId.ToString();
            txt_CustName.Text = CustObj.CustomerName.ToString();
            cmb_CustomerStatus.SelectedItem = CustObj.Status.CustomerStatusValue.ToString();
            cmb_CustGender.SelectedItem = CustObj.GenderType.GenderTypeValue.ToString();
            date_CustDateofBirth.Text = CustObj.DateofBirth.ToString();
            date_CustStartDateofRelationship.Text = Convert.ToDateTime(CustObj.RelationshipStartDate).ToLongDateString();
            if (CustObj.RelationshipExpiryDate != null)
                date_CustEndDateofRelationship.Text = Convert.ToDateTime(CustObj.RelationshipExpiryDate).ToLongDateString();
            else
                date_CustEndDateofRelationship.Text = "";
            txt_PresentAddress.Text = CustObj.PresentAddress.ToString();
            txt_PresentCity.Text = CustObj.PresentCity.ToString();
            txt_PresentPinCode.Text = CustObj.PresentPinCode.ToString();
            txt_PresentPhone.Text = CustObj.PresentPhoneNo.ToString();
            txt_PresentMobile.Text = CustObj.Mobile.ToString();
            txt_PresentEMail.Text = CustObj.EMailId.ToString();
            chk_IsPresentandPermAddSame.IsChecked = CustObj.IsPresentPermAddressSame;
            txt_PermanentAddress.Text = CustObj.PermanentAddress.ToString();
            txt_PermanentCity.Text = CustObj.PermanentCity.ToString();
            txt_PermanentPinCode.Text = CustObj.PermanentPinCode.ToString();
            txt_PermanentPhone.Text = CustObj.PermanentPhoneNo.ToString();
            cmb_CustIDProofType.SelectedItem = CustObj.IDProofType.IDProofTypeValue.ToString();
            txt_CustIDProof.Text = CustObj.IDProofValue.ToString();
            txt_UploadPhoto.Text = "";
            txt_CustAmtToBePaid.Text = CustObj.AmountTobePaid.ToString();
            txt_CustAmtPaidYTD.Text = CustObj.AmountPaidYTD.ToString();
            txt_CreateDate.Text = CustObj.CreateDate.ToString();
            if (CustObj.LastModDate != null)
                txt_LastModDate.Text = Convert.ToDateTime(CustObj.LastModDate).ToLongDateString();
            else
                txt_LastModDate.Text = "";

            image1.Source = null;

            MemoryStream stream;
            BitmapImage Img = new BitmapImage();


            if (CustObj.Image != null)
            {
                stream = new MemoryStream((byte[])CustObj.Image);

                Img.BeginInit();
                Img.StreamSource = stream;
                Img.EndInit();
            }
            else
            {
                Uri relativeUri = new Uri("/images/NoImage.jpg", UriKind.Relative);

                Img.BeginInit();
                Img.UriSource = relativeUri;
                Img.EndInit();
            }

            // Assign the Source property of your image
            image1.Source = Img;


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
                WinMessageBox WinMsgBox = new WinMessageBox("Are you sure you want to cancel?", "Customer Details - Add", 1, "QUESTION");
                WinMsgBox.ShowDialog();
                if (WinMsgBox.YesNoCancelValue == 0)
                {
                    e.Handled = true;
                    ProcessEventArgs myargs = new ProcessEventArgs(CustDtlInputWithCustomArgsEvent, UCArgs.InvokingControlName, UCArgs.InvokingControlMode, UCArgs.TargetControlName, UCArgs.TargetControlMode, null, null, UCArgs.ControlTabName);
                    RaiseEvent(myargs);
                }
            }
            else
            {
                e.Handled = true;
                ProcessEventArgs myargs = new ProcessEventArgs(CustDtlInputWithCustomArgsEvent, UCArgs.InvokingControlName, UCArgs.InvokingControlMode, UCArgs.TargetControlName, UCArgs.TargetControlMode, null, null, UCArgs.ControlTabName);
                RaiseEvent(myargs);
            }
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ClearControls();
            if (this.IsVisible == true)
                grid_CustDtlInputCtrl.DataContext = _customer;
            else
            {
                grid_CustDtlInputCtrl.DataContext = null;
            }
        }

        public void ClearControls()
        {
            txt_CustId.Text = "";
            txt_CustName.Text = "";
            cmb_CustomerStatus.Text = "";
            date_CustDateofBirth.SelectedDate = DateTime.Today;
            date_CustStartDateofRelationship.SelectedDate = DateTime.Today;
            date_CustEndDateofRelationship.SelectedDate = null;
            cmb_CustGender.Text = "";
            txt_PresentAddress.Text = "";
            txt_PresentCity.Text = "";
            txt_PresentPinCode.Text = "";
            txt_PresentPhone.Text = "";
            txt_PresentMobile.Text = "";
            txt_PresentEMail.Text = "";
            txt_PermanentAddress.Text = "";
            txt_PermanentCity.Text = "";
            txt_PermanentPinCode.Text = "";
            txt_PermanentPhone.Text = "";
            txt_CustAmtToBePaid.Text = "";
            txt_CustAmtPaidYTD.Text = "";
            cmb_CustIDProofType.Text = "";
            txt_CustIDProof.Text = "";
            txt_UploadPhoto.Text = "";
            txt_CreateDate.Text = "";
            txt_LastModDate.Text = "";
        }

        private void btn_Upload_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BitmapImage logo = new BitmapImage();

                if (txt_UploadPhoto.Text.Length > 0)
                {
                    logo.BeginInit();
                    logo.UriSource = new Uri(txt_UploadPhoto.Text);
                    logo.EndInit();

                    image1.Source = logo;
                }
            }
            catch (Exception ex1)
            {
                WinMessageBox MsgBox = new WinMessageBox(ex1.Message.ToString(), "Customer Details - Add", 0, "ERROR");
                MsgBox.ShowDialog();
            }
        }

        private void btn_Browse_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openDialog = new OpenFileDialog();

                if (openDialog.ShowDialog().Value)
                    txt_UploadPhoto.Text = openDialog.FileName;
            }
            catch (Exception ex1)
            {
                WinMessageBox MsgBox = new WinMessageBox(ex1.Message.ToString(), "Customer Details - Add", 0, "ERROR");
                MsgBox.ShowDialog();
            }
        }

        private void btn_CustomerCommit_Click(object sender, RoutedEventArgs e)
        {
            string errorMessage = null;
            
            CustObj = new Customer();


            if (this.Mode == "A")
            {
                try
                {
                    CustObj.CustomerId = txt_CustId.Text;
                    CustObj.CustomerName = txt_CustName.Text;
                    List<CustomerStatus> CustomerStatusList = new List<CustomerStatus>();
                    if (BusinessTierState.GetValue<List<CustomerStatus>>("CustomerStatusList", out CustomerStatusList, out errorMessage) == true)
                        CustObj.Status = CustomerStatusList.Find(CustomerStatus => CustomerStatus.CustomerStatusId == Convert.ToInt32(cmb_CustomerStatus.SelectedValue));
                    else
                    {
                        WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Customer Details - Add", 0, "ERROR");
                        MsgBox.ShowDialog();
                        return;
                    }
                    CustObj.DateofBirth = date_CustDateofBirth.SelectedDate.Value;

                    List<GenderType> GenderTypeList = new List<GenderType>();
                    if (BusinessTierState.GetValue<List<GenderType>>("GenderTypeList", out GenderTypeList, out errorMessage) == true)
                        CustObj.GenderType = GenderTypeList.Find(GenderType => GenderType.GenderTypeId == Convert.ToInt32(cmb_CustGender.SelectedValue));
                    else
                    {
                        WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Customer Details - Add", 0, "ERROR");
                        MsgBox.ShowDialog();
                        return;
                    }
                    CustObj.AmountTobePaid = 0;
                    CustObj.AmountPaidYTD = 0;

                    List<IDProofType> IDProofTypeList = new List<IDProofType>();
                    if (BusinessTierState.GetValue<List<IDProofType>>("IDProofTypeList", out IDProofTypeList, out errorMessage) == true)
                        CustObj.IDProofType = IDProofTypeList.Find(IDProofType => IDProofType.IDProofTypeId == Convert.ToInt32(cmb_CustIDProofType.SelectedValue));
                    else
                    {
                        WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Customer Details - Add", 0, "ERROR");
                        MsgBox.ShowDialog();
                        return;
                    }
                    CustObj.IDProofValue = txt_CustIDProof.Text;
                    CustObj.RelationshipStartDate = DateTime.Now;
                    CustObj.CreateDate = DateTime.Now;
                    CustObj.LastModDate = DateTime.Now;
                    if (txt_UploadPhoto.Text.Length > 0)
                    {
                        System.Drawing.Image image = System.Drawing.Image.FromFile(txt_UploadPhoto.Text, true);

                        MemoryStream ms = new MemoryStream();
                        image.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);

                        byte[] a = (byte[])ms.ToArray();

                        CustObj.Image = (byte[])a;
                    }
                    else
                        CustObj.Image = null;
                    CustObj.PresentAddress = txt_PresentAddress.Text;
                    CustObj.PresentCity = txt_PresentCity.Text;
                    CustObj.PresentPinCode = txt_PresentPinCode.Text;
                    CustObj.PresentPhoneNo = txt_PresentPhone.Text;
                    CustObj.Mobile = txt_PresentMobile.Text;
                    CustObj.EMailId = txt_PresentEMail.Text;
                    if (chk_IsPresentandPermAddSame.IsChecked == true)
                        CustObj.IsPresentPermAddressSame = true;
                    else
                        CustObj.IsPresentPermAddressSame = false;
                    CustObj.PermanentAddress = txt_PermanentAddress.Text;
                    CustObj.PermanentCity = txt_PermanentCity.Text;
                    CustObj.PermanentPinCode = txt_PermanentPinCode.Text;
                    CustObj.PermanentPhoneNo = txt_PermanentPhone.Text;

                    CustomerFactoryClient CustomerFactoryClient = new CustomerFactoryClient();

                    if (CustomerFactoryClient.AddCustomerDetails(out errorMessage, CustObj) == true)
                    {
                        WinMessageBox MsgBox = new WinMessageBox("New Customer Record Added Successfully!", "Customer Details - Add", 0, "SUCCESS");
                        MsgBox.ShowDialog();

                        _customer = null;
                        ClearControls();


                        UCArgs = new UserCtrlArgs();
                        UCArgs.InvokingControlName = this.Name;
                        UCArgs.InvokingControlMode = this.Mode;
                        UCArgs.TargetControlName = this.InvokingControlName;
                        UCArgs.TargetControlMode = this.InvokingControlMode;
                        UCArgs.ControlTabName = this.ControlTabName;

                        ProcessEventArgs myargs = new ProcessEventArgs(CustDtlInputWithCustomArgsEvent, UCArgs.InvokingControlName, UCArgs.InvokingControlMode, UCArgs.TargetControlName, UCArgs.TargetControlMode, null, null, UCArgs.ControlTabName);
                        RaiseEvent(myargs);
                    }
                    else
                    {
                        WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Customer Details - Add", 0, "ERROR");
                        MsgBox.ShowDialog();
                        return;
                    }
                }
                catch (Exception ex1)
                {
                    WinMessageBox MsgBox = new WinMessageBox(ex1.Message.ToString(), "Customer Details - Add", 0, "ERROR");
                    MsgBox.ShowDialog();
                }
            }
            else if (this.Mode == "U")
            {
                WinMessageBox MsgBox = new WinMessageBox("Functionality Yet to be Added", "Customer Details - Modify", 0, "INFORMATION");
                MsgBox.ShowDialog();
            }
        }

        private void dg_TxnResults_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string errorMessage = null;

            try
            {
                if (dg_TxnResults.SelectedIndex > -1)
                {
                    Sale NewSaleObj = new Sale();
                    NewSaleObj = (Sale)dg_TxnResults.Items[dg_TxnResults.SelectedIndex];

                    Sale SaleObj = new Sale();
                    if (ApplicationState.GetValue<Sale>("SaleObj", out SaleObj, out errorMessage) == true)
                        ApplicationState.DeleteValue("SaleObj");

                    ApplicationState.SetValue("SaleObj", NewSaleObj);
                    Window.GetWindow(this).WindowStyle = System.Windows.WindowStyle.None;
                    Window.GetWindow(this).Opacity = 0.25;

                    WinSaleDetailView NewSaleDetailView = new WinSaleDetailView();

                    NewSaleDetailView.SideHeaderText = "Sales Details For Sale Id: " + NewSaleObj.SalesId;
                    NewSaleDetailView.ShowDialog();

                    Window.GetWindow(this).WindowStyle = System.Windows.WindowStyle.None;
                    Window.GetWindow(this).Opacity = 1;
                }
            }
            catch (Exception ex1)
            {
                WinMessageBox MsgBox = new WinMessageBox(ex1.Message.ToString(), "Customer Details - Transactions", 0, "ERROR");
                MsgBox.ShowDialog();
            }
        }

        private void chk_IsPresentandPermAddSame_Checked(object sender, RoutedEventArgs e)
        {
            if (chk_IsPresentandPermAddSame.IsChecked == true)
            {
                txt_PermanentAddress.Text = txt_PresentAddress.Text;
                txt_PermanentCity .Text = txt_PresentCity.Text;
                txt_PermanentAddress .Text = txt_PresentAddress.Text;
                txt_PermanentPinCode.Text = txt_PresentPinCode.Text;
                txt_PermanentPhone.Text = txt_PresentPhone.Text;
            }
            else
            {
                txt_PermanentAddress.Text = "";
                txt_PermanentCity.Text = "";
                txt_PermanentAddress.Text = "";
                txt_PermanentPinCode.Text = "";
                txt_PermanentPhone.Text = "";
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                grid_CustDtlInputCtrl.DataContext = _customer;

                date_CustStartDateofRelationship.SelectedDate = DateTime.Now;
                date_CustEndDateofRelationship.SelectedDate = DateTime.Now;

                BitmapImage logo = new BitmapImage();

                Uri relativeUri = new Uri("/images/NoImage.jpg", UriKind.Relative);

                logo.BeginInit();
                logo.UriSource = relativeUri;
                logo.EndInit();

                image1.Source = logo;

                string errorMessage = null;

                List<CustomerStatus> CustomerStatusList = new List<CustomerStatus>();
                if (BusinessTierState.GetValue<List<CustomerStatus>>("CustomerStatusList", out CustomerStatusList, out errorMessage) == true)
                    cmb_CustomerStatus.ItemsSource = CustomerStatusList;
                else
                {
                    WinMessageBox MsgBox = new WinMessageBox("Unable to Load Status Parameters", "Customer Details - Static Data", 0, "ERROR");
                    MsgBox.ShowDialog();
                }

                List<GenderType> GenderTypeList = new List<GenderType>();
                if (BusinessTierState.GetValue<List<GenderType>>("GenderTypeList", out GenderTypeList, out errorMessage) == true)
                    cmb_CustGender.ItemsSource = GenderTypeList;
                else
                {
                    WinMessageBox MsgBox = new WinMessageBox("Unable to Load Gender Parameters", "Customer Details - Static Data", 0, "ERROR");
                    MsgBox.ShowDialog();
                }

                List<IDProofType> IDProofTypeList = new List<IDProofType>();
                if (BusinessTierState.GetValue<List<IDProofType>>("IDProofTypeList", out IDProofTypeList, out errorMessage) == true)
                    cmb_CustIDProofType.ItemsSource = IDProofTypeList;
                else
                {
                    WinMessageBox MsgBox = new WinMessageBox("Unable to Load ID Proof Parameters", "Customer Details - Static Data", 0, "ERROR");
                    MsgBox.ShowDialog();
                }
            }
            catch (Exception ex1)
            {
                WinMessageBox MsgBox = new WinMessageBox(ex1.Message.ToString(), "Customer Details - Static Data", 0, "ERROR");
                MsgBox.ShowDialog();
            }
        }
    }
}

