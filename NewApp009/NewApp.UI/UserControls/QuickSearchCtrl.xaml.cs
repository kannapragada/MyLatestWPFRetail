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
using NewApp.UI.UserControls;
using NewApp.BusinessTier.Common;
using NewApp.BusinessTier.Models;
using NewApp.UI.Windows;
using NewApp.GenderTypeFactorySvc;
using NewApp.IDProofTypeFactorySvc;



namespace NewApp.UI.UserControls
{
    /// <summary>
    /// Interaction logic for SalesMainCtrl.xaml
    /// </summary>
    public partial class QuickSearchCtrl : UserControl
    {
        public delegate void NotifyUserControl(object sender, UserCtrlArgs UserCtrlArgs);
        public event NotifyUserControl NotifyedUserControl;
        string _mode;
        string _invokingCtrlMode;
        string _invokingCtrlName;


        public QuickSearchCtrl()
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

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;

            UserCtrlArgs Args = new UserCtrlArgs();
            Args.TargetControlName = this.InvokingControlName;
            Args.TargetControlMode = this.InvokingControlMode;

            this.NotifyedUserControl(this, Args);
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.Visibility == Visibility.Visible)
                dg_SearchResults.ItemsSource = "";
        }

        public void ClearControls()
        {
        }

        private void btn_Search_Click(object sender, RoutedEventArgs e)
        {
            //string SelectedTabName;

            //SelectedTabName = ((TabItem)tab_Main.SelectedItem).Name.ToString();

            try
            {
                //#region Search Items

                //if (SelectedTabName == tab_ItemSearch.Name)
                //{
                //    string errorMessage = null;

                //    StoreItem StoreItem = new StoreItem();
                //    StoreItemObjList = new List<StoreItem>();

                //    DateTime dt = DateTime.Now;

                //    dg_SearchResults.Columns.Clear();

                //    if (StoreItem.GetStoreItems(txt_ItemId.Text, txt_ItemName.Text, txt_BatchId.Text, Convert.ToDateTime(date_StartDateofManuf.SelectedDate), Convert.ToDateTime(date_StartDateofManuf.SelectedDate), Convert.ToDateTime(date_StartDateofManuf.SelectedDate), Convert.ToDateTime(date_StartDateofManuf.SelectedDate), out errorMessage, out StoreItemObjList) == true)
                //    {
                //        dg_SearchResults.ItemsSource = StoreItemObjList;


                //        DataGridTextColumn NewColumn;

                //        NewColumn = new DataGridTextColumn();
                //        NewColumn.Header = "Item Id";
                //        NewColumn.Binding = new Binding("ItemId");
                //        NewColumn.Width = 100;

                //        dg_SearchResults.Columns.Add(NewColumn);

                //        NewColumn = new DataGridTextColumn();
                //        NewColumn.Header = "Item Name";
                //        NewColumn.Binding = new Binding("ItemName");
                //        NewColumn.Width = 200;
                //        dg_SearchResults.Columns.Add(NewColumn);

                //        NewColumn = new DataGridTextColumn();
                //        NewColumn.Header = "Batch Id";
                //        NewColumn.Binding = new Binding("BatchId");
                //        NewColumn.Width = 100;
                //        dg_SearchResults.Columns.Add(NewColumn);

                //        NewColumn = new DataGridTextColumn();
                //        NewColumn.Header = "MRP";
                //        NewColumn.Binding = new Binding("MRP");
                //        NewColumn.Width = 100;
                //        dg_SearchResults.Columns.Add(NewColumn);

                //        NewColumn = new DataGridTextColumn();
                //        NewColumn.Header = "Qty Available";
                //        NewColumn.Width = 100;
                //        NewColumn.Binding = new Binding("QtyAvailable");
                //        dg_SearchResults.Columns.Add(NewColumn);

                //        NewColumn = new DataGridTextColumn();
                //        NewColumn.Header = "Selling Price";
                //        NewColumn.Width = 100;
                //        NewColumn.Binding = new Binding("SellingPricePerUnit");
                //        dg_SearchResults.Columns.Add(NewColumn);

                //        NewColumn = new DataGridTextColumn();
                //        NewColumn.Header = "Disc/Unit";
                //        NewColumn.Binding = new Binding("DiscountAmountPerUnit");
                //        NewColumn.Width = 100;
                //        dg_SearchResults.Columns.Add(NewColumn);
                //    }
                //    else
                //    {
                //        if ((errorMessage != "") && (errorMessage != null))
                //        {
                //            WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Search Items", 0, "ERROR");
                //            MsgBox.ShowDialog();
                //        }
                //    }
                //}

                //#endregion
            }
            catch (Exception ex1)
            {
                WinMessageBox MsgBox = new WinMessageBox(ex1.Message, "Search Items", 0, "ERROR");
                MsgBox.ShowDialog();
            }

            try
            {
                //#region Search Sales

                //if (SelectedTabName == tab_SalesSearch.Name)
                //{
                //    string errorMessage = null;
                //    Sale Sale = new Sale();
                //    List<Sale> SaleObjList = new List<Sale>();


                //    DateTime dt = DateTime.Now;

                //    dg_SearchResults.Columns.Clear();

                //    if (Sale.SearchSaleDetails(txt_SalesId.Text, txt_SalesCustId.Text, txt_SalesCustName.Text, Convert.ToDateTime(date_StartSalesDate.SelectedDate), Convert.ToDateTime(date_EndSalesDate.SelectedDate), out SaleObjList, out errorMessage) == true)
                //    {
                //        dg_SearchResults.ItemsSource = SaleObjList;


                //        DataGridTextColumn NewColumn;

                //        NewColumn = new DataGridTextColumn();
                //        NewColumn.Header = "Sales Id";
                //        NewColumn.Binding = new Binding("SalesId");
                //        NewColumn.Width = 100;

                //        dg_SearchResults.Columns.Add(NewColumn);

                //        NewColumn = new DataGridTextColumn();
                //        NewColumn.Header = "Customer Id";
                //        NewColumn.Binding = new Binding("CustomerInfo.CustomerId");
                //        NewColumn.Width = 100;
                //        dg_SearchResults.Columns.Add(NewColumn);

                //        NewColumn = new DataGridTextColumn();
                //        NewColumn.Header = "Customer Name";
                //        NewColumn.Binding = new Binding("CustomerInfo.CustomerName");
                //        NewColumn.Width = 100;
                //        dg_SearchResults.Columns.Add(NewColumn);

                //        NewColumn = new DataGridTextColumn();
                //        NewColumn.Header = "Total Sale Amount";
                //        NewColumn.Binding = new Binding("TotalSaleAmount");
                //        NewColumn.Width = 100;
                //        dg_SearchResults.Columns.Add(NewColumn);

                //        NewColumn = new DataGridTextColumn();
                //        NewColumn.Header = "Total Discount Amount";
                //        NewColumn.Width = 100;
                //        NewColumn.Binding = new Binding("TotalDiscountAmount");
                //        dg_SearchResults.Columns.Add(NewColumn);

                //        NewColumn = new DataGridTextColumn();
                //        NewColumn.Header = "Total Tax Amount";
                //        NewColumn.Width = 100;
                //        NewColumn.Binding = new Binding("TotalTaxAmount");
                //        dg_SearchResults.Columns.Add(NewColumn);

                //        NewColumn = new DataGridTextColumn();
                //        NewColumn.Header = "Final Sale Amount";
                //        NewColumn.Binding = new Binding("FinalSaleAmount");
                //        NewColumn.Width = 100;
                //        dg_SearchResults.Columns.Add(NewColumn);

                //        NewColumn = new DataGridTextColumn();
                //        NewColumn.Header = "Paid Amount";
                //        NewColumn.Binding = new Binding("PaidAmount");
                //        NewColumn.Width = 100;
                //        dg_SearchResults.Columns.Add(NewColumn);

                //        NewColumn = new DataGridTextColumn();
                //        NewColumn.Header = "Balance Amount";
                //        NewColumn.Binding = new Binding("BalanceAmount");
                //        NewColumn.Width = 100;
                //        dg_SearchResults.Columns.Add(NewColumn);
                //    }
                //    else
                //    {
                //        if ((errorMessage != "") && (errorMessage != null))
                //        {
                //            WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Search Sales", 0, "ERROR");
                //            MsgBox.ShowDialog();
                //        }
                //    }
                //}

                //#endregion
            }
            catch (Exception ex1)
            {
                WinMessageBox MsgBox = new WinMessageBox(ex1.Message, "Search Sales", 0, "ERROR");
                MsgBox.ShowDialog();
            }

            try
            {
                //#region Search Customers

                //if (SelectedTabName == tab_CustSearch.Name)
                //{
                //    string errorMessage = null;

                //    Customer CustObj = new Customer();
                //    List<Customer> CustObjList = new List<Customer>();

                //    DateTime dt = DateTime.Now;

                //    dg_SearchResults.Columns.Clear();

                //    if (CustObj.GetCustomers(txt_CustId.Text, txt_CustName.Text, txt_CustAddress.Text, "", "", Convert.ToDateTime(date_FromDateofBirth.SelectedDate), Convert.ToDateTime(date_ToDateofBirth.SelectedDate), Convert.ToDateTime(date_FromDateofStartRelationship.SelectedDate), Convert.ToDateTime(date_ToDateofStartRelationship.SelectedDate), Convert.ToDateTime(date_FromRelationshipExpiryDate.SelectedDate), Convert.ToDateTime(date_ToRelationshipExpiryDate.SelectedDate), out errorMessage, out CustObjList) == true)
                //    {
                //        dg_SearchResults.ItemsSource = CustObjList;

                //        DataGridTextColumn NewColumn;

                //        NewColumn = new DataGridTextColumn();
                //        NewColumn.Header = "Customer Id";
                //        NewColumn.Binding = new Binding("CustomerId");
                //        NewColumn.Width = 100;
                //        dg_SearchResults.Columns.Add(NewColumn);

                //        NewColumn = new DataGridTextColumn();
                //        NewColumn.Header = "Customer Name";
                //        NewColumn.Binding = new Binding("CustomerName");
                //        NewColumn.Width = 200;
                //        dg_SearchResults.Columns.Add(NewColumn);

                //        NewColumn = new DataGridTextColumn();
                //        NewColumn.Header = "Date of Birth";
                //        NewColumn.Binding = new Binding("DateofBirth");
                //        NewColumn.Width = 100;
                //        dg_SearchResults.Columns.Add(NewColumn);

                //        NewColumn = new DataGridTextColumn();
                //        NewColumn.Header = "Amount To be Paid";
                //        NewColumn.Width = 100;
                //        NewColumn.Binding = new Binding("AmountTobePaid");
                //        dg_SearchResults.Columns.Add(NewColumn);

                //        NewColumn = new DataGridTextColumn();
                //        NewColumn.Header = "Amount Paid YTD";
                //        NewColumn.Width = 100;
                //        NewColumn.Binding = new Binding("AmountPaidYTD");
                //        dg_SearchResults.Columns.Add(NewColumn);

                //        NewColumn = new DataGridTextColumn();
                //        NewColumn.Header = "Relationship Start Date";
                //        NewColumn.Width = 100;
                //        NewColumn.Binding = new Binding("RelationshipStartDate");
                //        dg_SearchResults.Columns.Add(NewColumn);

                //        NewColumn = new DataGridTextColumn();
                //        NewColumn.Header = "Relationship Expiry Date";
                //        NewColumn.Width = 100;
                //        NewColumn.Binding = new Binding("RelationshipExpiryDate");
                //        dg_SearchResults.Columns.Add(NewColumn);
                //    }
                //    else
                //        if ((errorMessage != "") && (errorMessage != null))
                //        {
                //            WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Search Customer", 0, "ERROR");
                //            MsgBox.ShowDialog();
                //        }
                //}

                //#endregion
            }
            catch (Exception ex1)
            {
                WinMessageBox MsgBox = new WinMessageBox(ex1.Message, "Search Customers", 0, "ERROR");
                MsgBox.ShowDialog();
            }

            try
            {
                //#region Search Vendors

                //if (SelectedTabName == tab_VendorSearch.Name)
                //{
                //    string errorMessage = null;

                //    Vendor VendorObj = new Vendor();
                //    List<Vendor> VendorObjList = new List<Vendor>();

                //    DateTime dt = DateTime.Now;

                //    dg_SearchResults.Columns.Clear();

                //    if (VendorObj.GetVendors(cmb_VendorId.Text, txt_VendorName.Text, txt_VendorRefId.Text, "", "", Convert.ToDateTime(date_FromDateofBirth.SelectedDate), Convert.ToDateTime(date_ToDateofBirth.SelectedDate), Convert.ToDateTime(date_FromDateofStartRelationship.SelectedDate), Convert.ToDateTime(date_ToDateofStartRelationship.SelectedDate), Convert.ToDateTime(date_FromRelationshipExpiryDate.SelectedDate), Convert.ToDateTime(date_ToRelationshipExpiryDate.SelectedDate), out errorMessage, out VendorObjList) == true)
                //    {
                //        dg_SearchResults.ItemsSource = VendorObjList;

                //        DataGridTextColumn NewColumn;

                //        NewColumn = new DataGridTextColumn();
                //        NewColumn.Header = "Vendor Id";
                //        NewColumn.Binding = new Binding("VendorId");
                //        NewColumn.Width = 100;
                //        dg_SearchResults.Columns.Add(NewColumn);

                //        NewColumn = new DataGridTextColumn();
                //        NewColumn.Header = "Vendor Name";
                //        NewColumn.Binding = new Binding("VendorName");
                //        NewColumn.Width = 200;
                //        dg_SearchResults.Columns.Add(NewColumn);

                //        NewColumn = new DataGridTextColumn();
                //        NewColumn.Header = "Date of Birth";
                //        NewColumn.Binding = new Binding("DateofBirth");
                //        NewColumn.Width = 100;
                //        dg_SearchResults.Columns.Add(NewColumn);

                //        NewColumn = new DataGridTextColumn();
                //        NewColumn.Header = "Gender";
                //        NewColumn.Binding = new Binding("Gender");
                //        NewColumn.Width = 100;
                //        dg_SearchResults.Columns.Add(NewColumn);

                //        NewColumn = new DataGridTextColumn();
                //        NewColumn.Header = "Address";
                //        NewColumn.Width = 100;
                //        NewColumn.Binding = new Binding("Address");
                //        dg_SearchResults.Columns.Add(NewColumn);

                //        NewColumn = new DataGridTextColumn();
                //        NewColumn.Header = "IDProof Type";
                //        NewColumn.Width = 100;
                //        NewColumn.Binding = new Binding("IDProofType");
                //        dg_SearchResults.Columns.Add(NewColumn);

                //        NewColumn = new DataGridTextColumn();
                //        NewColumn.Header = "IDProof Value";
                //        NewColumn.Width = 100;
                //        NewColumn.Binding = new Binding("IDProofValue");
                //        dg_SearchResults.Columns.Add(NewColumn);

                //        NewColumn = new DataGridTextColumn();
                //        NewColumn.Header = "Relationship Start Date";
                //        NewColumn.Width = 100;
                //        NewColumn.Binding = new Binding("RelationshipStartDate");
                //        dg_SearchResults.Columns.Add(NewColumn);

                //        NewColumn = new DataGridTextColumn();
                //        NewColumn.Header = "Relationship Expiry Date";
                //        NewColumn.Width = 100;
                //        NewColumn.Binding = new Binding("RelationshipExpiryDate");
                //        dg_SearchResults.Columns.Add(NewColumn);

                //        NewColumn = new DataGridTextColumn();
                //        NewColumn.Header = "Amount To be Paid";
                //        NewColumn.Width = 100;
                //        NewColumn.Binding = new Binding("AmountTobePaid");
                //        dg_SearchResults.Columns.Add(NewColumn);

                //        NewColumn = new DataGridTextColumn();
                //        NewColumn.Header = "Amount Paid YTD";
                //        NewColumn.Width = 100;
                //        NewColumn.Binding = new Binding("AmountPaidYTD");
                //        dg_SearchResults.Columns.Add(NewColumn);
                //    }
                //    else
                //        if ((errorMessage != "") && (errorMessage != null))
                //        {
                //            WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Search Vendor", 0, "ERROR");
                //            MsgBox.ShowDialog();
                //        }
                //}

                //#endregion
            }
            catch (Exception ex1)
            {
                WinMessageBox MsgBox = new WinMessageBox(ex1.Message, "Search Vendors", 0, "ERROR");
                MsgBox.ShowDialog();
            }
        }

        private void dg_SearchResults_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string errorMessage = null;

            try
            {
                if (dg_SearchResults.SelectedIndex > -1)
                {
                    Sale SaleObj = new Sale();
                    if (ApplicationState.GetValue<Sale>("SaleObj", out SaleObj, out errorMessage) == true)
                        ApplicationState.DeleteValue("SaleObj");

                    SaleObj = (Sale)dg_SearchResults.Items[dg_SearchResults.SelectedIndex];

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
                WinMessageBox MsgBox = new WinMessageBox(ex1.Message, "Sales Details", 0, "ERROR");
                MsgBox.ShowDialog();
            }
        }
    }
}
