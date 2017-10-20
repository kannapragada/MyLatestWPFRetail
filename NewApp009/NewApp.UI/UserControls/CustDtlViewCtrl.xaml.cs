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
using NewApp.UI.Windows;
using NewApp.CustomerFactorySvc;
using NewApp.CustomerStatusFactorySvc;
using NewApp.CustomerTypeFactorySvc;
using NewApp.GenderTypeFactorySvc;
using NewApp.IDProofTypeFactorySvc;



namespace NewApp.UI.UserControls
{
    /// <summary>
    /// Interaction logic for SalesCtrl.xaml
    /// </summary>
    public partial class CustDtlViewCtrl : UserControl
    {
        private Customer _customer = new Customer();

        public delegate void NotifyUserControl(object sender, UserCtrlArgs UserCtrlArgs);
        public event NotifyUserControl NotifyedUserControl;
        string _mode;
        string _invokingCtrlMode;
        string _invokingCtrlName;

        public CustDtlViewCtrl()
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

        public void DisplayCustomer(Customer CustObj)
        {
            string errorMessage = null;

            try
            {
                txt_CustId.Text = CustObj.CustomerId.ToString();
                txt_CustName.Text = CustObj.CustomerName.ToString();

                CustomerStatus CustomerStatus = new CustomerStatus();
                CustomerStatusFactoryClient CustomerStatusFactoryClient = new CustomerStatusFactoryClient();
                if (CustomerStatusFactoryClient.GetCustomerStatus(out CustomerStatus, out errorMessage, Convert.ToInt32(CustObj.Status.CustomerStatusId)) == true)
                    this.txt_customerStatus.Text = CustomerStatus.CustomerStatusValue.ToString();
                GenderType GenderType = new GenderType();
                GenderTypeFactoryClient GenderTypeFactoryClient = new GenderTypeFactoryClient();
                if (GenderTypeFactoryClient.GetGenderType(out GenderType, out errorMessage, Convert.ToInt32(CustObj.GenderType.GenderTypeId)) == true)
                    txt_CustGender.Text = GenderType.GenderTypeValue.ToString();
                date_CustDateofBirth.Text = String.Format("{0:f}", CustObj.DateofBirth);
                date_CustStartDateofRelationship.Text = String.Format("{0:f}", CustObj.RelationshipStartDate);
                if (CustObj.RelationshipExpiryDate != null)
                    if ((CustObj.RelationshipExpiryDate > DateTime.MinValue) && (CustObj.RelationshipExpiryDate < DateTime.MaxValue))
                        date_CustEndDateofRelationship.Text =  String.Format("{0:f}", CustObj.RelationshipExpiryDate);
                IDProofType IDProofType = new IDProofType();
                IDProofTypeFactoryClient IDProofTypeFactoryClient = new IDProofTypeFactoryClient();
                if (IDProofTypeFactoryClient.GetIDProofType(out IDProofType, out errorMessage, Convert.ToInt32(CustObj.IDProofType.IDProofTypeId)) == true)
                    txt_CustIDProofType.Text = IDProofType.IDProofTypeValue.ToString();
                txt_CustIDProof.Text = CustObj.IDProofValue.ToString();
                txt_CustAmtToBePaid.Text = String.Format("{0:C2}", CustObj.AmountTobePaid);
                txt_CustAmtPaidYTD.Text = String.Format("{0:C2}", CustObj.AmountPaidYTD);
                txt_CreateDate.Text = String.Format("{0:f}", CustObj.CreateDate);
                if (CustObj.LastModDate != null)
                    if ((CustObj.LastModDate > DateTime.MinValue) && (CustObj.LastModDate < DateTime.MaxValue))
                        txt_LastModDate.Text = String.Format("{0:f}", CustObj.LastModDate);
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


                image1.Source = Img;
            }
            catch (Exception ex1)
            {
                WinMessageBox MsgBox = new WinMessageBox(ex1.Message.ToString(), "Customer Details", 0, "ERROR");
                MsgBox.ShowDialog();
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
            txt_CustId.Text = "";
            txt_CustName.Text = "";
            txt_customerStatus.Text = "";
            date_CustDateofBirth.Text = "";
            date_CustStartDateofRelationship.Text = "";
            date_CustEndDateofRelationship.Text = "";
            txt_CustGender.Text = "";
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
            txt_CustIDProofType.Text = "";
            txt_CustIDProof.Text = "";
            txt_CreateDate.Text = "";
            txt_LastModDate.Text = "";
        }

        private void dg_TxnResults_Selected()
        {
            string errorMessage = null;

            try
            {
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
            catch (Exception ex1)
            {
                WinMessageBox MsgBox = new WinMessageBox(ex1.Message.ToString(), "Customer Transaction Details", 0, "ERROR");
                MsgBox.ShowDialog();
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

