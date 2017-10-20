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
using NewApp.VendorFactorySvc;
using NewApp.VendorStatusFactorySvc;
using NewApp.GenderTypeFactorySvc;
using NewApp.IDProofTypeFactorySvc;
using NewApp.UI.Windows;


namespace NewApp.UI.UserControls
{
    /// <summary>
    /// Interaction logic for SalesCtrl.xaml
    /// </summary>
    public partial class VendorDtlViewCtrl : UserControl
    {
        private Vendor _Vendor = new Vendor();

        public delegate void NotifyUserControl(object sender, UserCtrlArgs UserCtrlArgs);
        public event NotifyUserControl NotifyedUserControl;
        string _mode;
        string _invokingCtrlMode;
        string _invokingCtrlName;

        public VendorDtlViewCtrl()
        {
            InitializeComponent();
        }

        public string Mode
        {
            get
            {
                return _mode;
            }
            set
            {
                _mode = value;

                if (_mode == "V")
                    btn_Back.Visibility = Visibility.Collapsed;
            }
        }

        public string InvokingControlMode
        {
            get
            {
                return _invokingCtrlMode;
            }
            set
            {
                _invokingCtrlMode = value;
            }
        }

        public string InvokingControlName
        {
            get
            {
                return _invokingCtrlName;
            }
            set
            {
                _invokingCtrlName = value;
            }
        }

        public void DisplayVendor(Vendor VendorObj)
        {
            string errorMessage = null;

            txt_VendorId.Text = VendorObj.VendorId.ToString();
            txt_VendorName.Text = VendorObj.VendorName.ToString();

            VendorStatus VendorStatus = new VendorStatus();
            VendorStatusFactoryClient VendorStatusFactoryClient = new VendorStatusFactoryClient();

            if (VendorStatusFactoryClient.GetVendorStatus(out VendorStatus, out errorMessage, Convert.ToInt32(VendorObj.Status.VendorStatusId)) == true)
                this.txt_VendorStatus.Text = VendorStatus.VendorStatusValue.ToString();
            GenderType GenderType = new GenderType();
            GenderTypeFactoryClient GenderTypeFactoryClient = new GenderTypeFactoryClient();
            if (GenderTypeFactoryClient.GetGenderType(out GenderType, out errorMessage, Convert.ToInt32(VendorObj.GenderType.GenderTypeId)) == true)
                txt_VendorGender.Text = GenderType.GenderTypeValue.ToString();
            date_VendorDateofBirth.Text = String.Format("{0:f}", VendorObj.DateofBirth);
            date_VendorStartDateofRelationship.Text = String.Format("{0:f}", VendorObj.RelationshipStartDate);
            if (VendorObj.RelationshipExpiryDate != null)
                if ((VendorObj.RelationshipExpiryDate > DateTime.MinValue) && (VendorObj.RelationshipExpiryDate < DateTime.MaxValue))
                    date_VendorEndDateofRelationship.Text = String.Format("{0:f}", VendorObj.RelationshipExpiryDate);
            IDProofType IDProofType = new IDProofType();
            IDProofTypeFactoryClient IDProofTypeFactoryClient = new IDProofTypeFactoryClient();
            if (IDProofTypeFactoryClient.GetIDProofType(out IDProofType, out errorMessage, Convert.ToInt32(VendorObj.IDProofType.IDProofTypeId)) == true)
                txt_VendorIDProofType.Text = IDProofType.IDProofTypeValue.ToString();
            txt_VendorIDProof.Text = VendorObj.IDProofValue.ToString();
            txt_VendorAmtToBePaid.Text = String.Format("{0:C2}", VendorObj.AmountTobePaid);
            txt_VendorAmtPaidYTD.Text = String.Format("{0:C2}", VendorObj.AmountPaidYTD);
            txt_CreateDate.Text = String.Format("{0:f}", VendorObj.CreateDate);
            if (VendorObj.LastModDate != null)
                if ((VendorObj.LastModDate > DateTime.MinValue) && (VendorObj.LastModDate < DateTime.MaxValue))
                    txt_LastModDate.Text = String.Format("{0:f}", VendorObj.LastModDate);
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


            image1.Source = null;

            if (VendorObj.Image != null)
            {
                MemoryStream stream = new MemoryStream((byte[])VendorObj.Image);
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
            txt_VendorId.Text = "";
            txt_VendorName.Text = "";
            txt_VendorStatus.Text = "";
            date_VendorDateofBirth.Text = "";
            date_VendorStartDateofRelationship.Text = "";
            date_VendorEndDateofRelationship.Text = "";
            txt_VendorGender.Text = "";
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
            txt_VendorIDProofType.Text = "";
            txt_VendorIDProof.Text = "";
            txt_CreateDate.Text = "";
            txt_LastModDate.Text = "";
        }

        private void dg_TxnResults_Selected()
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

                WinSaleDetailView NewSaleDetailView = new WinSaleDetailView("V");

                NewSaleDetailView.SideHeaderText = "Sales Details For Sale Id: " + SaleObj.SalesId;
                NewSaleDetailView.ShowDialog();

                Window.GetWindow(this).WindowStyle = System.Windows.WindowStyle.None;
                Window.GetWindow(this).Opacity = 1;
             }
        }

        private void dg_TxnResults_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            dg_TxnResults_Selected();
        }

        private void dg_TxnResults_KeyDown(object sender, KeyEventArgs e)
        {
            dg_TxnResults_Selected();
        }
    }
}

