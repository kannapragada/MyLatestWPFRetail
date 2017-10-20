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
using System.ComponentModel;
using System.Data;
using NewApp.BusinessTier.Models;
using ExcelInterOp;
using NewApp.UI.Windows;
using ExcelInterOp.CustomerFactorySvc;


namespace NewApp.UI.UserControls
{
    /// <summary>
    /// Interaction logic for ImportFromExcel.xaml
    /// </summary>
    public partial class ExcelInterOpCtrl : UserControl
    {
        ExcelData exceldata;
        Customer CustObj;

        DataTable MyDt;

        public ExcelInterOpCtrl()
        {
            InitializeComponent();
        }

        private void btn_ExportToExcel_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid1.Items.Count <= 0)
            {
                MessageBox.Show("No Data to Export");
                return;
            }

            ExportToExcel<Customer, List<Customer>> s = new ExportToExcel<Customer, List<Customer>>();
            ICollectionView view = CollectionViewSource.GetDefaultView(dataGrid1.ItemsSource);

            IList<Object> list = view.SourceCollection as IList<Object>;

            DataRow Dr;

            Customer[] CustObjArray = new Customer[MyDt.Rows.Count];

            CustomerFactoryClient MyCustFactoryClient = new CustomerFactoryClient();

            for (int i = 0; i < MyDt.Rows.Count; i++)
            {
                CustObj = new Customer();

                Dr = MyDt.Rows[i];

                CustObj.CustomerId = Dr[0].ToString();
                CustObj.CustomerName = Dr[1].ToString();
                CustObj.Status.CustomerStatusId = Convert.ToInt32(Dr[2].ToString());
                CustObj.DateofBirth = Convert.ToDateTime(Dr[3].ToString());
                CustObj.GenderType.GenderTypeId = Convert.ToInt32(Dr[4].ToString());
                CustObj.PresentAddress = Dr[5].ToString();
                CustObj.PresentCity = Dr[6].ToString();
                CustObj.PresentPinCode = Dr[7].ToString();
                CustObj.PresentPhoneNo = Dr[8].ToString();
                CustObj.Mobile = Dr[9].ToString();
                CustObj.EMailId = Dr[10].ToString();
                if (Dr[11].ToString() == "0")
                    CustObj.IsPresentPermAddressSame = false;
                else if (Dr[11].ToString() == "1")
                    CustObj.IsPresentPermAddressSame = true;
                CustObj.PermanentAddress = Dr[12].ToString();
                CustObj.PermanentCity = Dr[13].ToString();
                CustObj.PermanentPinCode = Dr[14].ToString();
                CustObj.PermanentPhoneNo = Dr[15].ToString();
                CustObj.IDProofType.IDProofTypeId = Convert.ToInt32(Dr[16].ToString());
                if ((Dr[18].ToString() != null) && (Dr[18].ToString() != ""))
                    CustObj.RelationshipStartDate = Convert.ToDateTime(Dr[18].ToString());
                if ((Dr[19].ToString() != null) && (Dr[19].ToString() != ""))
                    CustObj.RelationshipExpiryDate = Convert.ToDateTime(Dr[19].ToString());
                if (Dr[20] != null)
                    CustObj.AmountTobePaid = Convert.ToDouble(Dr[20].ToString());
                if (Dr[21] != null)
                    CustObj.AmountPaidYTD = Convert.ToDouble(Dr[21].ToString());

                CustObjArray[i] = CustObj;
            }

            List<Customer> MyCustList = new List<Customer>();
            MyCustList = CustObjArray.ToList<Customer>();

            s.dataToPrint = MyCustList;
            s.GenerateReport();
        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btn_ImportFromExcel_Click(object sender, RoutedEventArgs e)
        {
            exceldata = new ExcelData();
            this.dataGrid1.DataContext = exceldata;

            MyDt = new DataTable();

            MyDt = exceldata.Data;

            if (MyDt != null)
            {
                if (MyDt.Rows.Count <= 1)
                {
                    MessageBox.Show("No Data to Import");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Error While Importing Data!");
                return;
            }

            DataView dv = MyDt.DefaultView;
            dv.Sort = "RelationshipStartDate desc";
            MyDt = dv.ToTable();

            this.dataGrid1.ItemsSource = MyDt.DefaultView;
        }

        private void btn_UploadtoDb_Click(object sender, RoutedEventArgs e)
        {
            string errorMessage = null;

            try
            {
                if (dataGrid1.Items.Count <= 0)
                {
                    MessageBox.Show("No Data to Export");
                    return;
                }

                ExportToExcel<Customer, List<Customer>> s = new ExportToExcel<Customer, List<Customer>>();
                ICollectionView view = CollectionViewSource.GetDefaultView(dataGrid1.ItemsSource);

                IList<Object> list = view.SourceCollection as IList<Object>;

                DataRow Dr;

                Customer[] CustObjArray = new Customer[MyDt.Rows.Count];


                for (int i = 0; i < MyDt.Rows.Count; i++)
                {
                    CustObj = new Customer();

                    Dr = MyDt.Rows[i];

                    CustObj.CustomerId = Dr[0].ToString();
                    CustObj.CustomerName = Dr[1].ToString();
                    CustObj.Status.CustomerStatusId = Convert.ToInt32(Dr[2].ToString());
                    CustObj.DateofBirth = Convert.ToDateTime(Dr[3].ToString());
                    CustObj.GenderType.GenderTypeId = Convert.ToInt32(Dr[4].ToString());
                    CustObj.PresentAddress = Dr[5].ToString();
                    CustObj.PresentCity = Dr[6].ToString();
                    CustObj.PresentPinCode = Dr[7].ToString();
                    CustObj.PresentPhoneNo = Dr[8].ToString();
                    CustObj.Mobile = Dr[9].ToString();
                    CustObj.EMailId = Dr[10].ToString();
                    if (Dr[11].ToString() == "0")
                        CustObj.IsPresentPermAddressSame = false;
                    else if (Dr[11].ToString() == "1")
                        CustObj.IsPresentPermAddressSame = true;
                    CustObj.PermanentAddress = Dr[12].ToString();
                    CustObj.PermanentCity = Dr[13].ToString();
                    CustObj.PermanentPinCode = Dr[14].ToString();
                    CustObj.PermanentPhoneNo = Dr[15].ToString();
                    CustObj.IDProofType.IDProofTypeId = Convert.ToInt32(Dr[16].ToString());
                    if ((Dr[18].ToString() != null) && (Dr[18].ToString() != ""))
                        CustObj.RelationshipStartDate = Convert.ToDateTime(Dr[18].ToString());
                    if ((Dr[19].ToString() != null) && (Dr[19].ToString() != ""))
                        CustObj.RelationshipExpiryDate = Convert.ToDateTime(Dr[19].ToString());
                    if (Dr[20] != null)
                        CustObj.AmountTobePaid = Convert.ToDouble(Dr[20].ToString());
                    if (Dr[21] != null)
                        CustObj.AmountPaidYTD = Convert.ToDouble(Dr[21].ToString());

                    CustObjArray[i] = CustObj;
                }

                List<Customer> MyCustList = new List<Customer>();
                MyCustList = CustObjArray.ToList<Customer>();

                CustomerFactoryClient CustomerFactoryClient = new CustomerFactoryClient();

                foreach (Customer CustObj in MyCustList)
                {
                    if (CustomerFactoryClient.AddCustomerDetails(out errorMessage, CustObj) == false)
                    {
                        WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Customer Details - Add", 0, "ERROR");
                        MsgBox.ShowDialog();
                        return;
                    }
                }
            }
            catch (Exception ex1)
            {
                WinMessageBox MsgBox = new WinMessageBox(ex1.Message, "Customer Details - Add", 0, "ERROR");
                MsgBox.ShowDialog();
                return;
            }
        }
    }
}
