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
using Microsoft.Win32;
using System.Windows.Shapes;
using System.Threading;
using System.IO;
using System.Drawing;
using NewApp.BusinessTier.Common;
using NewApp.BusinessTier.Models;
using NewApp.GenderTypeFactorySvc;
using NewApp.IDProofTypeFactorySvc;
using NewApp.ManufFactorySvc;
using NewApp.UI.Windows;



namespace NewApp.UI.UserControls
{
    /// <summary>
    /// Interaction logic for SalesCtrl.xaml
    /// </summary>
    public partial class ManufDtlInputCtrl : UserControl
    {
        Manufacturer ManufObj;


        private Manufacturer _Manufacturer = new Manufacturer();

        public static readonly RoutedEvent ManufDtlInputWithCustomArgsEvent = EventManager.RegisterRoutedEvent("ManufDtlInputWithCustomArgs", RoutingStrategy.Bubble, typeof(ManufDtlInputWithCustomArgsEventHandler), typeof(ManufDtlInputCtrl));
        public delegate void ManufDtlInputWithCustomArgsEventHandler(object sender, ProcessEventArgs e);


        public delegate void ProcessHandler(object sender, ProcessEventArgs data);
        string _mode;
        string _invokingCtrlMode;
        string _invokingCtrlName;
        string _CtrlTabName;

        UserCtrlArgs UCArgs;

        public ManufDtlInputCtrl()
        {
            InitializeComponent();
            grid_ManufDtlInputCtrl.DataContext = _Manufacturer;
        }

        public string Mode
        {
            get{return _mode;}
            set
            {
                _mode = value;

                if (_mode == "A")
                {
                    tb_ManufAmtToBePaid.Visibility = Visibility.Collapsed;
                    tb_ManufAmtPaidYTD.Visibility = Visibility.Collapsed;
                    tb_CreateDate.Visibility = Visibility.Collapsed;
                    tb_LastModDate.Visibility = Visibility.Collapsed;
                    txt_ManufAmtToBePaid.Visibility = Visibility.Collapsed;
                    txt_ManufAmtPaidYTD.Visibility = Visibility.Collapsed;
                    txt_CreateDate.Visibility = Visibility.Collapsed;
                    txt_LastModDate.Visibility = Visibility.Collapsed;
                    ClearControls();
                }
                else
                {
                    tb_ManufAmtToBePaid.Visibility = Visibility.Visible;
                    tb_ManufAmtPaidYTD.Visibility = Visibility.Visible;
                    tb_CreateDate.Visibility = Visibility.Visible;
                    tb_LastModDate.Visibility = Visibility.Visible;
                    txt_ManufAmtToBePaid.Visibility = Visibility.Visible;
                    txt_ManufAmtPaidYTD.Visibility = Visibility.Visible;
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

        public event ManufDtlInputWithCustomArgsEventHandler ManufDtlInputWithCustomArgs
        {
            add { AddHandler(ManufDtlInputWithCustomArgsEvent, value); }
            remove { RemoveHandler(ManufDtlInputWithCustomArgsEvent, value); }
        }

        public void DisplayManufacturer(Manufacturer Manufacturer)
        {
            txt_ManufId.Text = ManufObj.ManufacturerId.ToString();
            txt_ManufName.Text = ManufObj.ManufacturerName.ToString();
            cmb_ManufStatus.SelectedItem = ManufObj.Status.ManufacturerStatusValue.ToString();
            cmb_ManufGender.SelectedItem = ManufObj.GenderType.GenderTypeValue.ToString();
            date_ManufDateofBirth.Text = ManufObj.DateofBirth.ToString();
            date_ManufStartDateofRelationship.Text = ManufObj.RelationshipStartDate.ToString();
            date_ManufEndDateofRelationship.Text = ManufObj.RelationshipExpiryDate.ToString();
            txt_PresentAddress.Text = ManufObj.PresentAddress.ToString();
            txt_PresentCity.Text = ManufObj.PresentCity.ToString();
            txt_PresentPinCode.Text = ManufObj.PresentPinCode.ToString();
            txt_PresentPhone.Text = ManufObj.PresentPhoneNo.ToString();
            txt_PresentMobile.Text = ManufObj.Mobile.ToString();
            txt_PresentEMail.Text = ManufObj.EMailId.ToString();
            chk_IsPresentandPermAddSame.IsChecked = ManufObj.IsPresentPermAddressSame;
            txt_PermanentAddress.Text = ManufObj.PermanentAddress.ToString();
            txt_PermanentCity.Text = ManufObj.PermanentCity.ToString();
            txt_PermanentPinCode.Text = ManufObj.PermanentPinCode.ToString();
            txt_PermanentPhone.Text = ManufObj.PermanentPhoneNo.ToString();
            cmb_ManufIDProofType.SelectedItem = ManufObj.IDProofType.IDProofTypeValue.ToString();
            txt_ManufIDProof.Text = ManufObj.IDProofValue.ToString();
            txt_UploadPhoto.Text = "";
            txt_ManufAmtToBePaid.Text = ManufObj.AmountTobePaid.ToString();
            txt_ManufAmtPaidYTD.Text = ManufObj.AmountPaidYTD.ToString();
            txt_CreateDate.Text = ManufObj.CreateDate.ToString();
            txt_LastModDate.Text = ManufObj.LastModDate.ToString();

            image1.Source = null;

            MemoryStream stream;
            BitmapImage Img = new BitmapImage();


            if (ManufObj.Image != null)
            {
                stream = new MemoryStream((byte[])ManufObj.Image);

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
                    ProcessEventArgs myargs = new ProcessEventArgs(ManufDtlInputWithCustomArgsEvent, UCArgs.InvokingControlName, UCArgs.InvokingControlMode, UCArgs.TargetControlName, UCArgs.TargetControlMode, null, null, UCArgs.ControlTabName);
                    RaiseEvent(myargs);
                }
            }
            else
            {
                e.Handled = true;
                ProcessEventArgs myargs = new ProcessEventArgs(ManufDtlInputWithCustomArgsEvent, UCArgs.InvokingControlName, UCArgs.InvokingControlMode, UCArgs.TargetControlName, UCArgs.TargetControlMode, null, null, UCArgs.ControlTabName);
                RaiseEvent(myargs);
            }
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ClearControls();
            if (this.IsVisible == true)
                grid_ManufDtlInputCtrl.DataContext = _Manufacturer;
            else
            {
                grid_ManufDtlInputCtrl.DataContext = null;
            }
        }

        public void ClearControls()
        {
            txt_ManufId.Text = "";
            txt_ManufName.Text = "";
            cmb_ManufStatus.Text = "";
            date_ManufDateofBirth.SelectedDate = DateTime.Today;
            date_ManufStartDateofRelationship.SelectedDate = DateTime.Today;
            date_ManufEndDateofRelationship.SelectedDate = DateTime.Today;
            cmb_ManufGender.Text = "";
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
            txt_ManufAmtToBePaid.Text = "";
            txt_ManufAmtPaidYTD.Text = "";
            cmb_ManufIDProofType.Text = "";
            txt_ManufIDProof.Text = "";
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

        private void btn_ManufCommit_Click(object sender, RoutedEventArgs e)
        {
            string errorMessage = null;

            ManufObj = new Manufacturer();

            if (this.Mode == "A")
            {
                ManufObj.ManufacturerId = txt_ManufId.Text;
                ManufObj.ManufacturerName = txt_ManufName.Text;
                List<ManufacturerStatus> ManufacturerStatusList = new List<ManufacturerStatus>();
                if (BusinessTierState.GetValue<List<ManufacturerStatus>>("ManufacturerStatusList", out ManufacturerStatusList, out errorMessage) == true)
                    ManufObj.Status = ManufacturerStatusList.Find(ManufacturerStatus => ManufacturerStatus.ManufacturerStatusId == Convert.ToInt32(this.cmb_ManufStatus.SelectedValue));
                else
                {
                    WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Add Manufacturer", 0, "ERROR");
                    MsgBox.ShowDialog();
                    return;
                }
                ManufObj.DateofBirth = date_ManufDateofBirth.SelectedDate.Value;

                List<GenderType> GenderTypeList = new List<GenderType>();
                if (BusinessTierState.GetValue<List<GenderType>>("GenderTypeList", out GenderTypeList, out errorMessage) == true)
                    ManufObj.GenderType = GenderTypeList.Find(GenderType => GenderType.GenderTypeId == Convert.ToInt32(cmb_ManufGender.SelectedValue));
                else
                {
                    WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Add Manufacturer", 0, "ERROR");
                    MsgBox.ShowDialog();
                    return;
                }
                ManufObj.PresentAddress = txt_PresentAddress.Text;
                ManufObj.PresentCity = txt_PresentCity.Text;
                ManufObj.PresentPinCode = txt_PresentPinCode.Text;
                ManufObj.PresentPhoneNo = txt_PresentPhone.Text;
                ManufObj.Mobile = txt_PresentMobile.Text;
                ManufObj.EMailId = txt_PresentEMail.Text;
                if (chk_IsPresentandPermAddSame.IsChecked == true)
                    ManufObj.IsPresentPermAddressSame = true;
                else
                    ManufObj.IsPresentPermAddressSame = false;
                ManufObj.PermanentAddress = txt_PermanentAddress.Text;
                ManufObj.PermanentCity = txt_PermanentCity.Text;
                ManufObj.PermanentPinCode = txt_PermanentPinCode.Text;
                ManufObj.PermanentPhoneNo = txt_PermanentPhone.Text;
                ManufObj.AmountTobePaid = 0;
                ManufObj.AmountPaidYTD = 0;

                List<IDProofType> IDProofTypeList = new List<IDProofType>();
                if (BusinessTierState.GetValue<List<IDProofType>>("IDProofTypeList", out IDProofTypeList, out errorMessage) == true)
                    ManufObj.IDProofType = IDProofTypeList.Find(IDProofType => IDProofType.IDProofTypeId == Convert.ToInt32(cmb_ManufIDProofType.SelectedValue));
                else
                {
                    WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Add Manufacturer", 0, "ERROR");
                    MsgBox.ShowDialog();
                    return;
                }
                ManufObj.IDProofValue = txt_ManufIDProof.Text;

                if ((date_ManufStartDateofRelationship.SelectedDate > DateTime.MinValue) && (date_ManufStartDateofRelationship.SelectedDate < DateTime.MaxValue))
                    ManufObj.RelationshipStartDate = date_ManufStartDateofRelationship.SelectedDate.Value;
                else
                {
                    WinMessageBox MsgBox = new WinMessageBox("InValid Start Date of Relationship", "Add Manufacturer", 0, "ERROR");
                    MsgBox.ShowDialog();
                    return;
                }

                if ((date_ManufEndDateofRelationship.SelectedDate <= DateTime.MaxValue) && (date_ManufEndDateofRelationship.SelectedDate <= DateTime.MinValue))
                    ManufObj.RelationshipExpiryDate = date_ManufEndDateofRelationship.SelectedDate.Value;

                ManufObj.CreateDate = DateTime.Now;

                if (txt_UploadPhoto.Text.Length > 0)
                {
                    System.Drawing.Image image = System.Drawing.Image.FromFile(txt_UploadPhoto.Text, true);

                    MemoryStream ms = new MemoryStream();
                    image.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);

                    byte[] a = (byte[])ms.ToArray();

                    ManufObj.Image = (byte[])a;
                }
                else
                    ManufObj.Image = null;

                ManufacturerFactoryClient ManufFactoryClient = new ManufacturerFactoryClient();


                if (ManufFactoryClient.AddManufacturerDetails(out errorMessage, ManufObj) == true)
                {
                    WinMessageBox MsgBox = new WinMessageBox("New Manufacturer Record Added Successfully!", "Add Manufacturer", 0, "SUCCESS");
                    MsgBox.ShowDialog();

                    _Manufacturer = null;
                    ClearControls();

                    UCArgs = new UserCtrlArgs();
                    UCArgs.InvokingControlName = this.Name;
                    UCArgs.InvokingControlMode = this.Mode;
                    UCArgs.TargetControlName = this.InvokingControlName;
                    UCArgs.TargetControlMode = this.InvokingControlMode;
                    UCArgs.ControlTabName = this.ControlTabName;

                    ProcessEventArgs myargs = new ProcessEventArgs(ManufDtlInputWithCustomArgsEvent, UCArgs.InvokingControlName, UCArgs.InvokingControlMode, UCArgs.TargetControlName, UCArgs.TargetControlMode, null, null, UCArgs.ControlTabName);
                    RaiseEvent(myargs);
                }
                else
                {
                    WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Add Manufacturer", 0, "ERROR");
                    MsgBox.ShowDialog();
                    return;
                }
            }
            else if (this.Mode == "U")
            {
                WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Modify Manufacturer", 0, "INFORMATION");
                MsgBox.ShowDialog();
                return;
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
                //grpbox_PermAddressDtls.IsEnabled = false;
                txt_PermanentAddress.Text = txt_PresentAddress.Text;
                txt_PermanentCity.Text = txt_PresentCity.Text;
                txt_PermanentAddress.Text = txt_PresentAddress.Text;
                txt_PermanentPinCode.Text = txt_PresentPinCode.Text;
                txt_PermanentPhone.Text = txt_PresentPhone.Text;
            }
            else
            {
                //grpbox_PermAddressDtls.IsEnabled = false;
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
                grid_ManufDtlInputCtrl.DataContext = _Manufacturer;

                date_ManufStartDateofRelationship.SelectedDate = DateTime.Now;
                date_ManufEndDateofRelationship.SelectedDate = DateTime.Now;

                BitmapImage logo = new BitmapImage();

                Uri relativeUri = new Uri("/images/NoImage.jpg", UriKind.Relative);

                logo.BeginInit();
                logo.UriSource = relativeUri;
                logo.EndInit();

                image1.Source = logo;

                string errorMessage = null;

                List<ManufacturerStatus> ManufacturerStatusList = new List<ManufacturerStatus>();
                if (BusinessTierState.GetValue<List<ManufacturerStatus>>("ManufacturerStatusList", out ManufacturerStatusList, out errorMessage) == true)
                    cmb_ManufStatus.ItemsSource = ManufacturerStatusList;
                else
                {
                    WinMessageBox MsgBox = new WinMessageBox("Unable to Load Status Parameters", "Manufacturer Main", 0, "ERROR");
                    MsgBox.ShowDialog();
                }

                List<GenderType> GenderTypeList = new List<GenderType>();
                if (BusinessTierState.GetValue<List<GenderType>>("GenderTypeList", out GenderTypeList, out errorMessage) == true)
                    cmb_ManufGender.ItemsSource = GenderTypeList;
                else
                {
                    WinMessageBox MsgBox = new WinMessageBox("Unable to Load Gender Parameters", "Manufacturer Main", 0, "ERROR");
                    MsgBox.ShowDialog();
                }

                List<IDProofType> IDProofTypeList = new List<IDProofType>();
                if (BusinessTierState.GetValue<List<IDProofType>>("IDProofTypeList", out IDProofTypeList, out errorMessage) == true)
                    cmb_ManufIDProofType.ItemsSource = IDProofTypeList;
                else
                {
                    WinMessageBox MsgBox = new WinMessageBox("Unable to Load ID Proof Parameters", "Manufacturer Main", 0, "ERROR");
                    MsgBox.ShowDialog();
                }
            }
            catch (Exception ex1)
            {
                WinMessageBox MsgBox = new WinMessageBox(ex1.Message.ToString(), "Manufacturer Main", 0, "ERROR");
                MsgBox.ShowDialog();
            }
        }
    }
}

