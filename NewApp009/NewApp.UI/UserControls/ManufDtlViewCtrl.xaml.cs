using NewApp.BusinessTier.Models;
using NewApp.GenderTypeFactorySvc;
using NewApp.IDProofTypeFactorySvc;
using NewApp.ManufStatusFactorySvc;
using NewApp.UI.Windows;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;



namespace NewApp.UI.UserControls
{
    /// <summary>
    /// Interaction logic for SalesCtrl.xaml
    /// </summary>
    public partial class ManufDtlViewCtrl : UserControl
    {
        private Manufacturer _Manufacturer = new Manufacturer();

        public delegate void NotifyUserControl(object sender, UserCtrlArgs UserCtrlArgs);
        public event NotifyUserControl NotifyedUserControl;
        string _mode;
        string _invokingCtrlMode;
        string _invokingCtrlName;

        public ManufDtlViewCtrl()
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
                    grid_Actions.Visibility = Visibility.Collapsed;
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

        public void DisplayManufacturer(Manufacturer ManufObj)
        {
            string errorMessage = null;

            txt_ManufId.Text = ManufObj.ManufacturerId.ToString();
            txt_ManufName.Text = ManufObj.ManufacturerName.ToString();

            ManufacturerStatus ManufacturerStatus = new ManufacturerStatus();
            ManufacturerStatusFactoryClient ManufacturerStatusFactoryClient = new ManufacturerStatusFactoryClient();
            if (ManufacturerStatusFactoryClient.GetManufacturerStatus(out ManufacturerStatus, out errorMessage, Convert.ToInt32(ManufObj.Status.ManufacturerStatusId)) == true)
                this.txt_ManufacturerStatus.Text = ManufacturerStatus.ManufacturerStatusValue.ToString();
            GenderType GenderType = new GenderType();
            GenderTypeFactoryClient GenderTypeFactoryClient = new GenderTypeFactoryClient();
            if (GenderTypeFactoryClient.GetGenderType(out GenderType, out errorMessage, Convert.ToInt32(ManufObj.GenderType.GenderTypeId)) == true)
                txt_ManufGender.Text = GenderType.GenderTypeValue.ToString();
            date_ManufDateofBirth.Text = String.Format("{0:f}", ManufObj.DateofBirth);
            date_ManufStartDateofRelationship.Text = String.Format("{0:f}", ManufObj.RelationshipStartDate);
            if (ManufObj.RelationshipExpiryDate != null)
                if ((ManufObj.RelationshipExpiryDate > DateTime.MinValue) && (ManufObj.RelationshipExpiryDate < DateTime.MaxValue))
                    date_ManufEndDateofRelationship.Text = String.Format("{0:f}", ManufObj.RelationshipExpiryDate);
            IDProofType IDProofType = new IDProofType();
            IDProofTypeFactoryClient IDProofTypeFactoryClient = new IDProofTypeFactoryClient();
            if (IDProofTypeFactoryClient.GetIDProofType(out IDProofType, out errorMessage, Convert.ToInt32(ManufObj.IDProofType.IDProofTypeId)) == true)
                txt_ManufIDProofType.Text = IDProofType.IDProofTypeValue.ToString();
            txt_ManufIDProof.Text = ManufObj.IDProofValue.ToString();
            txt_ManufAmtToBePaid.Text = String.Format("{0:C2}", ManufObj.AmountTobePaid);
            txt_ManufAmtPaidYTD.Text = String.Format("{0:C2}", ManufObj.AmountPaidYTD);
            txt_CreateDate.Text = String.Format("{0:f}", ManufObj.CreateDate);
            if (ManufObj.LastModDate != null)
                if ((ManufObj.LastModDate > DateTime.MinValue) && (ManufObj.LastModDate < DateTime.MaxValue))
                    txt_LastModDate.Text = String.Format("{0:f}", ManufObj.LastModDate);
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

            image1.Source = null;

            if (ManufObj.Image != null)
            {
                MemoryStream stream = new MemoryStream((byte[])ManufObj.Image);
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
            txt_ManufId.Text = "";
            txt_ManufName.Text = "";
            txt_ManufacturerStatus.Text = "";
            date_ManufDateofBirth.Text = "";
            date_ManufStartDateofRelationship.Text = "";
            date_ManufEndDateofRelationship.Text = "";
            txt_ManufGender.Text = "";
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
            txt_ManufIDProofType.Text = "";
            txt_ManufIDProof.Text = "";
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

