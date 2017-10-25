using Microsoft.Win32;
using NewApp.BusinessTier.Common;
using NewApp.BusinessTier.Models;
using NewApp.UI.Windows;
using NewApp.VendorFactorySvc;
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
    public partial class VendorDtlInputCtrl : UserControl
    {
        Vendor VendorObj;


        private Vendor _vendor;

        public static readonly RoutedEvent VendorDtlInputWithCustomArgsEvent = EventManager.RegisterRoutedEvent("VendorDtlInputWithCustomArgs", RoutingStrategy.Bubble, typeof(VendorDtlInputWithCustomArgsEventHandler), typeof(VendorDtlInputCtrl));
        public delegate void VendorDtlInputWithCustomArgsEventHandler(object sender, ProcessEventArgs e);


        public delegate void ProcessHandler(object sender, ProcessEventArgs data);
        string _mode;
        string _invokingCtrlMode;
        string _invokingCtrlName;
        string _CtrlTabName;

        UserCtrlArgs UCArgs;

        public VendorDtlInputCtrl()
        {
            InitializeComponent();

            _vendor = new Vendor();
            grid_VendorDtlInputCtrl.DataContext = _vendor;
        }

        public string Mode
        {
            get{return _mode;}
            set
            {
                _mode = value;

                if (_mode == "A")
                {
                    tb_VendorAmtToBePaid.Visibility = Visibility.Collapsed;
                    tb_VendorAmtPaidYTD.Visibility = Visibility.Collapsed;
                    tb_CreateDate.Visibility = Visibility.Collapsed;
                    tb_LastModDate.Visibility = Visibility.Collapsed;
                    txt_VendorAmtToBePaid.Visibility = Visibility.Collapsed;
                    txt_VendorAmtPaidYTD.Visibility = Visibility.Collapsed;
                    txt_CreateDate.Visibility = Visibility.Collapsed;
                    txt_LastModDate.Visibility = Visibility.Collapsed;
                    ClearControls();
                }
                else
                {
                    tb_VendorAmtToBePaid.Visibility = Visibility.Visible;
                    tb_VendorAmtPaidYTD.Visibility = Visibility.Visible;
                    tb_CreateDate.Visibility = Visibility.Visible;
                    tb_LastModDate.Visibility = Visibility.Visible;
                    txt_VendorAmtToBePaid.Visibility = Visibility.Visible;
                    txt_VendorAmtPaidYTD.Visibility = Visibility.Visible;
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

        public event VendorDtlInputWithCustomArgsEventHandler VendorDtlInputWithCustomArgs
        {
            add { AddHandler(VendorDtlInputWithCustomArgsEvent, value); }
            remove { RemoveHandler(VendorDtlInputWithCustomArgsEvent, value); }
        }

        public void DisplayVendor(Vendor Vendor)
        {
            txt_VendorId.Text = VendorObj.VendorId.ToString();
            txt_VendorName.Text = VendorObj.VendorName.ToString();
            cmb_VendorStatus.SelectedItem = VendorObj.Status.VendorStatusValue.ToString();
            cmb_VendorGender.SelectedItem = VendorObj.GenderType.GenderTypeValue.ToString();
            date_VendorDateofBirth.Text = VendorObj.DateofBirth.ToString();
            date_VendorStartDateofRelationship.Text = VendorObj.RelationshipStartDate.ToString();
            date_VendorEndDateofRelationship.Text = VendorObj.RelationshipExpiryDate.ToString();
            txt_PresentAddress.Text = VendorObj.PresentAddress.ToString();
            txt_PresentCity.Text = VendorObj.PresentCity.ToString();
            txt_PresentPinCode.Text = VendorObj.PresentPinCode.ToString();
            txt_PresentPhone.Text = VendorObj.PresentPhoneNo.ToString();
            txt_PresentMobile.Text = VendorObj.Mobile.ToString();
            txt_PresentEMail.Text = VendorObj.EMailId.ToString();
            chk_IsPresentandPermAddSame.IsChecked = VendorObj.IsPresentPermAddressSame;
            txt_PermanentAddress.Text = VendorObj.PermanentAddress.ToString();
            txt_PermanentCity.Text = VendorObj.PermanentCity.ToString();
            txt_PermanentPinCode.Text = VendorObj.PermanentPinCode.ToString();
            txt_PermanentPhone.Text = VendorObj.PermanentPhoneNo.ToString();
            cmb_VendorIDProofType.SelectedItem = VendorObj.IDProofType.ToString();
            txt_VendorIDProof.Text = VendorObj.IDProofValue.ToString();
            txt_UploadPhoto.Text = "";
            txt_VendorAmtToBePaid.Text = VendorObj.AmountTobePaid.ToString();
            txt_VendorAmtPaidYTD.Text = VendorObj.AmountPaidYTD.ToString();
            txt_CreateDate.Text = VendorObj.CreateDate.ToString();
            txt_LastModDate.Text = VendorObj.LastModDate.ToString();

            image1.Source = null;

            MemoryStream stream;
            BitmapImage Img = new BitmapImage();


            if (VendorObj.Image != null)
            {
                stream = new MemoryStream((byte[])VendorObj.Image);

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
                    ProcessEventArgs myargs = new ProcessEventArgs(VendorDtlInputWithCustomArgsEvent, UCArgs.InvokingControlName, UCArgs.InvokingControlMode, UCArgs.TargetControlName, UCArgs.TargetControlMode, null, null, UCArgs.ControlTabName);
                    RaiseEvent(myargs);
                }
            }
            else
            {
                e.Handled = true;
                ProcessEventArgs myargs = new ProcessEventArgs(VendorDtlInputWithCustomArgsEvent, UCArgs.InvokingControlName, UCArgs.InvokingControlMode, UCArgs.TargetControlName, UCArgs.TargetControlMode, null, null, UCArgs.ControlTabName);
                RaiseEvent(myargs);
            }
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ClearControls();
            if (this.IsVisible == true)
                grid_VendorDtlInputCtrl.DataContext = _vendor;
            else
            {
                grid_VendorDtlInputCtrl.DataContext = null;
            }
        }

        public void ClearControls()
        {
            txt_VendorId.Text = "";
            txt_VendorName.Text = "";
            cmb_VendorStatus.Text = "";
            date_VendorDateofBirth.SelectedDate = DateTime.Today;
            date_VendorStartDateofRelationship.SelectedDate = DateTime.Today;
            date_VendorEndDateofRelationship.SelectedDate = DateTime.Today;
            cmb_VendorGender.Text = "";
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
            txt_VendorAmtToBePaid.Text = "";
            txt_VendorAmtPaidYTD.Text = "";
            cmb_VendorIDProofType.Text = "";
            txt_VendorIDProof.Text = "";
            txt_UploadPhoto.Text = "";
            txt_CreateDate.Text = "";
            txt_LastModDate.Text = "";
        }

        private void btn_Upload_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Image finalImage = new System.Windows.Controls.Image();
            
            BitmapImage logo = new BitmapImage();

            if (txt_UploadPhoto.Text.Length > 0)
            {
                logo.BeginInit();
                logo.UriSource = new Uri(txt_UploadPhoto.Text);
                logo.EndInit();

                image1.Source = logo;
            }
        }

        private void btn_Browse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();

            if (openDialog.ShowDialog().Value)
            {
                txt_UploadPhoto.Text = openDialog.FileName;
            }
        }

        private void btn_VendorCommit_Click(object sender, RoutedEventArgs e)
        {
            string errorMessage = null;

            
            VendorObj = new Vendor();

            if (this.Mode == "A")
            {
                VendorObj.VendorId = txt_VendorId.Text;
                VendorObj.VendorName = txt_VendorName.Text;
                VendorObj.Status.VendorStatusId = Convert.ToInt32(cmb_VendorStatus.SelectedValue.ToString());
                List<VendorStatus> VendorStatusList = new List<VendorStatus>();
                if (BusinessTierState.GetValue<List<VendorStatus>>("VendorStatusList", out VendorStatusList, out errorMessage) == true)
                    VendorObj.Status = VendorStatusList.Find(VendorStatus => VendorStatus.VendorStatusId == Convert.ToInt32(cmb_VendorStatus.SelectedValue));
                else
                {
                    WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Add Vendor", 0, "ERROR");
                    MsgBox.ShowDialog();
                    return;
                }
                VendorObj.DateofBirth = date_VendorDateofBirth.SelectedDate.Value;

                List<GenderType> GenderTypeList = new List<GenderType>();
                if (BusinessTierState.GetValue<List<GenderType>>("GenderTypeList", out GenderTypeList, out errorMessage) == true)
                    VendorObj.GenderType = GenderTypeList.Find(GenderType => GenderType.GenderTypeId == Convert.ToInt32(cmb_VendorGender.SelectedValue));
                else
                {
                    WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Add Vendor", 0, "ERROR");
                    MsgBox.ShowDialog();
                    return;
                } 
                VendorObj.PresentAddress = txt_PresentAddress.Text;
                VendorObj.PresentCity = txt_PresentCity.Text;
                VendorObj.PresentPinCode = txt_PresentPinCode.Text;
                VendorObj.PresentPhoneNo = txt_PresentPhone.Text;
                VendorObj.Mobile = txt_PresentMobile.Text;
                VendorObj.EMailId = txt_PresentEMail.Text;
                if (chk_IsPresentandPermAddSame.IsChecked == true)
                    VendorObj.IsPresentPermAddressSame = true;
                else
                    VendorObj.IsPresentPermAddressSame = false;
                VendorObj.PermanentAddress = txt_PermanentAddress.Text;
                VendorObj.PermanentCity = txt_PermanentCity.Text;
                VendorObj.PermanentPinCode = txt_PermanentPinCode.Text;
                VendorObj.PermanentPhoneNo = txt_PermanentPhone.Text;
                VendorObj.AmountTobePaid = 0;
                VendorObj.AmountPaidYTD = 0;  


                List<IDProofType> IDProofTypeList = new List<IDProofType>();
                if (BusinessTierState.GetValue<List<IDProofType>>("IDProofTypeList", out IDProofTypeList, out errorMessage) == true)
                    VendorObj.IDProofType = IDProofTypeList.Find(IDProofType => IDProofType.IDProofTypeId == Convert.ToInt32(cmb_VendorIDProofType.SelectedValue));
                else
                {
                    WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Add Vendor", 0, "ERROR");
                    MsgBox.ShowDialog();
                    return;
                }
                VendorObj.IDProofValue = txt_VendorIDProof.Text;
                VendorObj.RelationshipStartDate = DateTime.Now;
                VendorObj.CreateDate = DateTime.Now;
                VendorObj.LastModDate = DateTime.Now;


                if (txt_UploadPhoto.Text.Length > 0)
                {
                    System.Drawing.Image image = System.Drawing.Image.FromFile(txt_UploadPhoto.Text, true);

                    MemoryStream ms = new MemoryStream();
                    image.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);

                    byte[] a = (byte[])ms.ToArray();

                    VendorObj.Image = (byte[])a;
                }
                else
                    VendorObj.Image = null;


                VendorFactoryClient VendorFactoryClient =  new VendorFactoryClient();

                if (VendorFactoryClient.AddVendorDetails(out errorMessage, VendorObj) == true)
                {
                    WinMessageBox MsgBox = new WinMessageBox("New Vendor Record Added Successfully!", "Add Vendor", 0, "SUCCESS");
                    MsgBox.ShowDialog();

                    _vendor = null;
                    ClearControls();

                    UCArgs = new UserCtrlArgs();
                    UCArgs.InvokingControlName = this.Name;
                    UCArgs.InvokingControlMode = this.Mode;
                    UCArgs.TargetControlName = this.InvokingControlName;
                    UCArgs.TargetControlMode = this.InvokingControlMode;
                    UCArgs.ControlTabName = this.ControlTabName;

                    ProcessEventArgs myargs = new ProcessEventArgs(VendorDtlInputWithCustomArgsEvent, UCArgs.InvokingControlName, UCArgs.InvokingControlMode, UCArgs.TargetControlName, UCArgs.TargetControlMode, null, null, UCArgs.ControlTabName);
                    RaiseEvent(myargs);
                }
                else
                {
                    WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Add Vendor", 0, "ERROR");
                    MsgBox.ShowDialog();
                }
            }
            else if (this.Mode == "U")
            {
                WinMessageBox MsgBox = new WinMessageBox("Functionality Yet to be Added", "Modify Vendor", 0, "INFORMATION");
                MsgBox.ShowDialog();
            }


        }

        private void dg_TxnResults_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string errorMessage = null;

            if (dg_TxnResults.SelectedIndex > -1)
            {
                Sale SaleObj = new Sale();
                SaleObj = (Sale)dg_TxnResults.Items[dg_TxnResults.SelectedIndex];

                Sale NewSaleObj = new Sale();
                if (ApplicationState.GetValue<Sale>("SaleObj", out NewSaleObj, out errorMessage) == true)
                    ApplicationState.DeleteValue("SaleObj");

                ApplicationState.SetValue("SaleObj", SaleObj);
                Window.GetWindow(this).WindowStyle = System.Windows.WindowStyle.None;
                Window.GetWindow(this).Opacity = 0.25;

                WinSaleDetailView NewSaleDetailView = new WinSaleDetailView();

                NewSaleDetailView.SideHeaderText = "Sales Details For Sale Id: " + SaleObj.SalesId;
                NewSaleDetailView.ShowDialog();

                Window.GetWindow(this).WindowStyle = System.Windows.WindowStyle.None;
                Window.GetWindow(this).Opacity = 1;
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
                grid_VendorDtlInputCtrl.DataContext = _vendor;

                date_VendorStartDateofRelationship.SelectedDate = DateTime.Now;
                date_VendorEndDateofRelationship.SelectedDate = DateTime.Now;

                BitmapImage logo = new BitmapImage();

                Uri relativeUri = new Uri("/images/NoImage.jpg", UriKind.Relative);

                logo.BeginInit();
                logo.UriSource = relativeUri;
                logo.EndInit();

                image1.Source = logo;

                string errorMessage = null;

                List<VendorStatus> VendorStatusList = new List<VendorStatus>();
                if (BusinessTierState.GetValue<List<VendorStatus>>("VendorStatusList", out VendorStatusList, out errorMessage) == true)
                    cmb_VendorStatus.ItemsSource = VendorStatusList;
                else
                {
                    WinMessageBox MsgBox = new WinMessageBox("Unable to Load Status Parameters", "Vendor Main", 0, "ERROR");
                    MsgBox.ShowDialog();
                }

                List<GenderType> GenderTypeList = new List<GenderType>();
                if (BusinessTierState.GetValue<List<GenderType>>("GenderTypeList", out GenderTypeList, out errorMessage) == true)
                    cmb_VendorGender.ItemsSource = GenderTypeList;
                else
                {
                    WinMessageBox MsgBox = new WinMessageBox("Unable to Load Gender Parameters", "Vendor Main", 0, "ERROR");
                    MsgBox.ShowDialog();
                }

                List<IDProofType> IDProofTypeList = new List<IDProofType>();
                if (BusinessTierState.GetValue<List<IDProofType>>("IDProofTypeList", out IDProofTypeList, out errorMessage) == true)
                    cmb_VendorIDProofType.ItemsSource = IDProofTypeList;
                else
                {
                    WinMessageBox MsgBox = new WinMessageBox("Unable to Load ID Proof Type Parameters", "Vendor Main", 0, "ERROR");
                    MsgBox.ShowDialog();
                }
            }
            catch (Exception ex1)
            {
                WinMessageBox MsgBox = new WinMessageBox(ex1.Message.ToString(), "Vendor Main", 0, "ERROR");
                MsgBox.ShowDialog();
            }
        }
    }
}

