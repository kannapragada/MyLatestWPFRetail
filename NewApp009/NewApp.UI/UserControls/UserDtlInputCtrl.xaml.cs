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
using NewApp.UserFactorySvc;
using NewApp.UI.Windows;
using NewApp.GenderTypeFactorySvc;
using NewApp.IDProofTypeFactorySvc;



namespace NewApp.UI.UserControls
{
    /// <summary>
    /// Interaction logic for SalesCtrl.xaml
    /// </summary>
    public partial class UserDtlInputCtrl : UserControl
    {
        User UserObj;

        private User _user;

        public static readonly RoutedEvent UserDtlInputWithCustomArgsEvent = EventManager.RegisterRoutedEvent("UserDtlInputWithCustomArgs", RoutingStrategy.Bubble, typeof(UserDtlInputWithCustomArgsEventHandler), typeof(UserDtlInputCtrl));
        public delegate void UserDtlInputWithCustomArgsEventHandler(object sender, ProcessEventArgs e);


        public delegate void ProcessHandler(object sender, ProcessEventArgs data);
        string _mode;
        string _invokingCtrlMode;
        string _invokingCtrlName;
        string _CtrlTabName;

        UserCtrlArgs UCArgs;

        public UserDtlInputCtrl()
        {
            InitializeComponent();

            _user = new User();
        }

        public string Mode
        {
            get{return _mode;}
            set
            {
                _mode = value;

                if (_mode == "A")
                {
                    tb_UserAmtToBePaid.Visibility = Visibility.Collapsed;
                    tb_UserAmtPaidYTD.Visibility = Visibility.Collapsed;
                    tb_CreateDate.Visibility = Visibility.Collapsed;
                    tb_LastModDate.Visibility = Visibility.Collapsed;
                    txt_UserAmtToBePaid.Visibility = Visibility.Collapsed;
                    txt_UserAmtPaidYTD.Visibility = Visibility.Collapsed;
                    txt_CreateDate.Visibility = Visibility.Collapsed;
                    txt_LastModDate.Visibility = Visibility.Collapsed;
                    ClearControls();
                }
                else
                {
                    tb_UserAmtToBePaid.Visibility = Visibility.Visible;
                    tb_UserAmtPaidYTD.Visibility = Visibility.Visible;
                    tb_CreateDate.Visibility = Visibility.Visible;
                    tb_LastModDate.Visibility = Visibility.Visible;
                    txt_UserAmtToBePaid.Visibility = Visibility.Visible;
                    txt_UserAmtPaidYTD.Visibility = Visibility.Visible;
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

        public event UserDtlInputWithCustomArgsEventHandler UserDtlInputWithCustomArgs
        {
            add { AddHandler(UserDtlInputWithCustomArgsEvent, value); }
            remove { RemoveHandler(UserDtlInputWithCustomArgsEvent, value); }
        }

        public void DisplayUser(User User)
        {
            txt_UserId.Text = UserObj.UserId.ToString();
            txt_UserName.Text = UserObj.UserName.ToString();
            cmb_UserStatus.SelectedItem = UserObj.Status.UserStatusValue.ToString();
            cmb_UserGender.SelectedItem = UserObj.GenderType.GenderTypeValue.ToString();
            date_UserDateofBirth.Text = UserObj.DateofBirth.ToString();
            txt_PresentAddress.Text = UserObj.PresentAddress.ToString();
            txt_PresentCity.Text = UserObj.PresentCity.ToString();
            txt_PresentPinCode.Text = UserObj.PresentPinCode.ToString();
            txt_PresentPhone.Text = UserObj.PresentPhoneNo.ToString();
            txt_PresentMobile.Text = UserObj.Mobile.ToString();
            txt_PresentEMail.Text = UserObj.EMailId.ToString();
            chk_IsPresentandPermAddSame.IsChecked = UserObj.IsPresentPermAddressSame;
            txt_PermanentAddress.Text = UserObj.PermanentAddress.ToString();
            txt_PermanentCity.Text = UserObj.PermanentCity.ToString();
            txt_PermanentPinCode.Text = UserObj.PermanentPinCode.ToString();
            txt_PermanentPhone.Text = UserObj.PermanentPhoneNo.ToString();
            cmb_UserIDProofType.SelectedItem = UserObj.IDProofType.ToString();
            txt_UserIDProof.Text = UserObj.IDProofValue.ToString();
            txt_UploadPhoto.Text = "";
            txt_UserAmtToBePaid.Text = UserObj.AmountTobePaid.ToString();
            txt_UserAmtPaidYTD.Text = UserObj.AmountPaidYTD.ToString();
            txt_CreateDate.Text = UserObj.CreateDate.ToString();
            txt_LastModDate.Text = UserObj.LastModDate.ToString();

            image1.Source = null;

            if (UserObj.Image != null)
            {
                MemoryStream stream = new MemoryStream((byte[])UserObj.Image);
                BitmapImage Img = new BitmapImage();
                Img.BeginInit();
                Img.StreamSource = stream;
                Img.EndInit();

                // Assign the Source property of your image
                image1.Source = Img;
            }
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
                    ProcessEventArgs myargs = new ProcessEventArgs(UserDtlInputWithCustomArgsEvent, UCArgs.InvokingControlName, UCArgs.InvokingControlMode, UCArgs.TargetControlName, UCArgs.TargetControlMode, null, null, UCArgs.ControlTabName);
                    RaiseEvent(myargs);
                }
            }
            else
            {
                e.Handled = true;
                ProcessEventArgs myargs = new ProcessEventArgs(UserDtlInputWithCustomArgsEvent, UCArgs.InvokingControlName, UCArgs.InvokingControlMode, UCArgs.TargetControlName, UCArgs.TargetControlMode, null, null, UCArgs.ControlTabName);
                RaiseEvent(myargs);
            }
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ClearControls();
            if (this.IsVisible == true)
                grid_UserDtlInputCtrl.DataContext = _user;
            else
            {
                grid_UserDtlInputCtrl.DataContext = null;
            }
        }

        public void ClearControls()
        {
            txt_UserId.Text = "";
            txt_UserName.Text = "";
            cmb_UserStatus.Text = "";
            date_UserDateofBirth.SelectedDate = DateTime.Today;
            date_UserStartDateofRelationship.SelectedDate = DateTime.Today;
            date_UserEndDateofRelationship.SelectedDate = DateTime.Today;
            cmb_UserGender.Text = "";
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
            txt_UserAmtToBePaid.Text = "";
            txt_UserAmtPaidYTD.Text = "";
            cmb_UserIDProofType.Text = "";
            txt_UserIDProof.Text = "";
            txt_UploadPhoto.Text = "";
            txt_CreateDate.Text = "";
            txt_LastModDate.Text = "";
        }

        private void btn_Upload_Click(object sender, RoutedEventArgs e)
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

        private void btn_Browse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();

            if (openDialog.ShowDialog().Value)
            {
                txt_UploadPhoto.Text = openDialog.FileName;
            }
        }

        private void btn_UserCommit_Click(object sender, RoutedEventArgs e)
        {
            UserFactoryClient UserFactoryClient = new UserFactoryClient();
            UserObj = new User();

            string errorMessage = null;

            if (this.Mode == "A")
            {
                UserObj.UserId = txt_UserId.Text;
                UserObj.UserName = txt_UserName.Text;
                List<UserStatus> UserStatusList = new List<UserStatus>();
                if (BusinessTierState.GetValue<List<UserStatus>>("UserStatusList", out UserStatusList, out errorMessage) == true)
                    UserObj.Status = UserStatusList.Find(UserStatus => UserStatus.UserStatusId == Convert.ToInt32(cmb_UserStatus.SelectedValue));
                else
                {
                    WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Add User", 0, "ERROR");
                    MsgBox.ShowDialog();
                    return;
                }
                UserObj.DateofBirth = date_UserDateofBirth.SelectedDate.Value;

                List<GenderType> GenderTypeList = new List<GenderType>();
                if (BusinessTierState.GetValue<List<GenderType>>("GenderTypeList", out GenderTypeList, out errorMessage) == true)
                    UserObj.GenderType = GenderTypeList.Find(GenderType => GenderType.GenderTypeId == Convert.ToInt32(cmb_UserGender.SelectedValue));
                else
                {
                    WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Add User", 0, "ERROR");
                    MsgBox.ShowDialog();
                    return;
                }
                UserObj.PresentAddress = txt_PresentAddress.Text;
                UserObj.PresentCity = txt_PresentCity.Text;
                UserObj.PresentPinCode = txt_PresentPinCode.Text;
                UserObj.PresentPhoneNo = txt_PresentPhone.Text;
                UserObj.Mobile = txt_PresentMobile.Text;
                UserObj.EMailId = txt_PresentEMail.Text;
                if (chk_IsPresentandPermAddSame.IsChecked == true)
                    UserObj.IsPresentPermAddressSame = true;
                else
                    UserObj.IsPresentPermAddressSame = false;
                UserObj.PermanentAddress = txt_PermanentAddress.Text;
                UserObj.PermanentCity = txt_PermanentCity.Text;
                UserObj.PermanentPinCode = txt_PermanentPinCode.Text;
                UserObj.PermanentPhoneNo = txt_PermanentPhone.Text;
                UserObj.AmountTobePaid = 0;
                UserObj.AmountPaidYTD = 0;

                List<IDProofType> IDProofTypeList = new List<IDProofType>();
                if (BusinessTierState.GetValue<List<IDProofType>>("IDProofTypeList", out IDProofTypeList, out errorMessage) == true)
                    UserObj.IDProofType = IDProofTypeList.Find(IDProofType => IDProofType.IDProofTypeId == Convert.ToInt32(cmb_UserIDProofType.SelectedValue));
                else
                {
                    WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Add User", 0, "ERROR");
                    MsgBox.ShowDialog();
                    return;
                }
                UserObj.IDProofValue = txt_UserIDProof.Text;
                UserObj.RelationshipStartDate = DateTime.Now;
                UserObj.CreateDate = DateTime.Now;
                UserObj.LastModDate = DateTime.Now;
                UserObj.UserPassword = "password@12345";
                UserObj.UserSecretQueryId = cmb_UserSecretQuery.SelectedIndex;
                UserObj.UserSecretAnswer = txt_UserSecretAnswer.Text;
                UserObj.UserThemeId = cmb_UserTheme.SelectedIndex;


                if (txt_UploadPhoto.Text.Length > 0)
                {
                    System.Drawing.Image image = System.Drawing.Image.FromFile(txt_UploadPhoto.Text, true);

                    MemoryStream ms = new MemoryStream();
                    image.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);

                    byte[] a = (byte[])ms.ToArray();

                    UserObj.Image = (byte[])a;
                }
                else
                    UserObj.Image = null;



                if (UserFactoryClient.AddUserDetails(out errorMessage, UserObj) == true)
                {
                    WinMessageBox MsgBox = new WinMessageBox("New User Record Added Successfully!", "Add User", 0, "SUCCESS");
                    MsgBox.ShowDialog();


                    _user = null;
                    ClearControls();

                    UCArgs = new UserCtrlArgs();
                    UCArgs.InvokingControlName = this.Name;
                    UCArgs.InvokingControlMode = this.Mode;
                    UCArgs.TargetControlName = this.InvokingControlName;
                    UCArgs.TargetControlMode = this.InvokingControlMode;
                    UCArgs.ControlTabName = this.ControlTabName;

                    ProcessEventArgs myargs = new ProcessEventArgs(UserDtlInputWithCustomArgsEvent, UCArgs.InvokingControlName, UCArgs.InvokingControlMode, UCArgs.TargetControlName, UCArgs.TargetControlMode, null, null, UCArgs.ControlTabName);
                    RaiseEvent(myargs);
                }
                else
                {
                    WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Add User", 0, "ERROR");
                    MsgBox.ShowDialog();
                    return;
                }
            }
            else if (this.Mode == "U")
            {
                WinMessageBox MsgBox = new WinMessageBox("Functionality Yet to be Added", "Modify User", 0, "INFORMATION");
                MsgBox.ShowDialog();
                return;
            }
        }

        private void dg_TxnResults_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string errorMessage = null;

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

        private void chk_IsPresentandPermAddSame_Checked(object sender, RoutedEventArgs e)
        {
            if (chk_IsPresentandPermAddSame.IsChecked == true)
            {
                //grpbox_PermAddressDtls.IsEnabled = false;
                txt_PermanentAddress.Text = txt_PresentAddress.Text;
                txt_PermanentCity .Text = txt_PresentCity.Text;
                txt_PermanentAddress .Text = txt_PresentAddress.Text;
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
                grid_UserDtlInputCtrl.DataContext = _user;

                date_UserStartDateofRelationship.SelectedDate = DateTime.Now;
                date_UserEndDateofRelationship.SelectedDate = DateTime.Now;

                BitmapImage logo = new BitmapImage();

                Uri relativeUri = new Uri("/images/NoImage.jpg", UriKind.Relative);

                logo.BeginInit();
                logo.UriSource = relativeUri;
                logo.EndInit();

                image1.Source = logo;

                string errorMessage = null;

                List<UserStatus> UserStatusList = new List<UserStatus>();
                if (BusinessTierState.GetValue<List<UserStatus>>("UserStatusList", out UserStatusList, out errorMessage) == true)
                    cmb_UserStatus.ItemsSource = UserStatusList;
                else
                {
                    WinMessageBox MsgBox = new WinMessageBox("Unable to Load Status Parameters", "User Main", 0, "ERROR");
                    MsgBox.ShowDialog();
                }

                List<GenderType> GenderTypeList = new List<GenderType>();
                if (BusinessTierState.GetValue<List<GenderType>>("GenderTypeList", out GenderTypeList, out errorMessage) == true)
                    cmb_UserGender.ItemsSource = GenderTypeList;
                else
                {
                    WinMessageBox MsgBox = new WinMessageBox("Unable to Load Gender Parameters", "User Main", 0, "ERROR");
                    MsgBox.ShowDialog();
                }

                List<IDProofType> IDProofTypeList = new List<IDProofType>();
                if (BusinessTierState.GetValue<List<IDProofType>>("IDProofTypeList", out IDProofTypeList, out errorMessage) == true)
                    cmb_UserIDProofType.ItemsSource = IDProofTypeList;
                else
                {
                    WinMessageBox MsgBox = new WinMessageBox("Unable to Load ID Proof Parameters", "User Main", 0, "ERROR");
                    MsgBox.ShowDialog();
                }
            }
            catch (Exception ex1)
            {
                WinMessageBox MsgBox = new WinMessageBox(ex1.Message.ToString(), "User Main", 0, "ERROR");
                MsgBox.ShowDialog();
            }
        }
    }
}

