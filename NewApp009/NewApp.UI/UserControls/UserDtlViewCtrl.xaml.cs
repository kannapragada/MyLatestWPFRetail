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
using NewApp.GenderTypeFactorySvc;
using NewApp.IDProofTypeFactorySvc;
using NewApp.UserStatusFactorySvc;
using NewApp.UserTypeFactorySvc;
using NewApp.UI.Windows;
using NewApp.UI.UserControls;


namespace NewApp.UI.UserControls
{
    /// <summary>
    /// Interaction logic for SalesCtrl.xaml
    /// </summary>
    public partial class UserDtlViewCtrl : UserControl
    {
        private User _user = new User();

        public delegate void NotifyUserControl(object sender, UserCtrlArgs UserCtrlArgs);
        public event NotifyUserControl NotifyedUserControl;
        string _mode;
        string _invokingCtrlMode;
        string _invokingCtrlName;

        public UserDtlViewCtrl()
        {
            InitializeComponent();
        }

        public string Mode
        {
            get{return _mode;}

            set
            {
                _mode = value;

                if (_mode == "V")
                    btn_Back.Visibility = Visibility.Collapsed;
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

        public void DisplayUser(User UserObj)
        {
            string errorMessage = null;

            txt_UserId.Text = UserObj.UserId.ToString();
            txt_UserName.Text = UserObj.UserName.ToString();

            UserStatus UserStatus = new UserStatus();
            UserStatusFactoryClient UserStatusFactoryClient = new UserStatusFactoryClient();
            if (UserStatusFactoryClient.GetUserStatus(out UserStatus, out errorMessage, Convert.ToInt32(UserObj.Status.UserStatusId)) == true)
                this.txt_UserStatus.Text = UserStatus.UserStatusValue.ToString();
            GenderType GenderType = new GenderType();
            GenderTypeFactoryClient GenderTypeFactoryClient = new GenderTypeFactoryClient();
            if (GenderTypeFactoryClient.GetGenderType(out GenderType, out errorMessage, Convert.ToInt32(UserObj.GenderType.GenderTypeId)) == true)
                txt_UserGender.Text = GenderType.GenderTypeValue.ToString();
            date_UserDateofBirth.Text = String.Format("{0:f}", UserObj.DateofBirth);
            IDProofType IDProofType = new IDProofType();
            IDProofTypeFactoryClient IDProofTypeFactoryClient = new IDProofTypeFactoryClient();
            if (IDProofTypeFactoryClient.GetIDProofType(out IDProofType, out errorMessage, Convert.ToInt32(UserObj.IDProofType.IDProofTypeId)) == true)
                txt_UserIDProofType.Text = IDProofType.IDProofTypeValue.ToString();
            date_UserStartDateofRelationship.Text = String.Format("{0:f}", UserObj.RelationshipStartDate);
            if (UserObj.RelationshipExpiryDate != null)
                if ((UserObj.RelationshipExpiryDate > DateTime.MinValue) && (UserObj.RelationshipExpiryDate < DateTime.MaxValue))
                    date_UserEndDateofRelationship.Text = String.Format("{0:f}", UserObj.RelationshipExpiryDate);
            date_UserStartDateofRelationship.Text = Convert.ToDateTime(UserObj.RelationshipStartDate).ToLongDateString();
            txt_UserIDProof.Text = UserObj.IDProofValue.ToString();
            txt_UserAmtToBePaid.Text = String.Format("{0:C2}", UserObj.AmountTobePaid);
            txt_UserAmtPaidYTD.Text = String.Format("{0:C2}", UserObj.AmountPaidYTD);
            txt_CreateDate.Text = String.Format("{0:f}", UserObj.CreateDate);
            if (UserObj.LastModDate != null)
                if ((UserObj.LastModDate > DateTime.MinValue) && (UserObj.LastModDate < DateTime.MaxValue))
                    txt_LastModDate.Text = String.Format("{0:f}", UserObj.LastModDate);
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

        private void btn_Back_Click(object sender, RoutedEventArgs e)
        {
                e.Handled = true;
                ClearControls();

                UserCtrlArgs Args = new UserCtrlArgs();
                Args.TargetControlName = this.InvokingControlName;
                Args.TargetControlMode = this.InvokingControlMode;

                this.NotifyedUserControl(this, Args);
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ClearControls();
        }

        public void ClearControls()
        {
            txt_UserId.Text = "";
            txt_UserName.Text = "";
            txt_UserStatus.Text = "";
            date_UserDateofBirth.Text = "";
            date_UserStartDateofRelationship.Text = "";
            date_UserEndDateofRelationship.Text = "";
            txt_UserGender.Text = "";
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
            txt_UserIDProofType.Text = "";
            txt_UserIDProof.Text = "";
            txt_CreateDate.Text = "";
            txt_LastModDate.Text = "";
        }

        private void dg_TxnResults_Selected()
        {
            string errorMessage = null;

             if (dg_TxnResults.SelectedIndex > -1)
            {
                Sale SaleObj = new Sale();
                if (ApplicationState.GetValue<Sale>("SaleObj", out SaleObj, out errorMessage) == true)
                    ApplicationState.DeleteValue("SaleObj");

                SaleObj = (Sale)dg_TxnResults.Items[dg_TxnResults.SelectedIndex];

                ApplicationState.SetValue("SaleObj", SaleObj);
                Window.GetWindow(this).WindowStyle = System.Windows.WindowStyle.None;
                Window.GetWindow(this).Opacity = 0.25;

                WinSaleDetailView WndSaleDetailView = new WinSaleDetailView("V");

                WndSaleDetailView.SideHeaderText = "Sales Details For Sale Id: " + SaleObj.SalesId;
                WndSaleDetailView.ShowDialog();

                Window.GetWindow(this).WindowStyle = System.Windows.WindowStyle.None;
                Window.GetWindow(this).Opacity = 1;
             }
        }

        private void dg_TxnResults_MouseDown(object sender, MouseButtonEventArgs e)
        {
            dg_TxnResults_Selected();
        }

        private void dg_TxnResults_KeyDown(object sender, KeyEventArgs e)
        {
            dg_TxnResults_Selected();
        }
    }
}

