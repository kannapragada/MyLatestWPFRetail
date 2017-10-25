using NewApp.BusinessTier.Models;
using NewApp.CustomerFactorySvc;
using NewApp.ManufFactorySvc;
using NewApp.SaleFactorySvc;
using NewApp.StoreItemFactorySvc;
using NewApp.UI.Windows;
using NewApp.VendorFactorySvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;




namespace NewApp.UI.UserControls
{
    /// <summary>
    /// Interaction logic for SalesMainCtrl.xaml
    /// </summary>
    public partial class SearchMainCtrl : UserControl
    {
        public static readonly RoutedEvent SearchMainWithCustomArgsEvent = EventManager.RegisterRoutedEvent("SearchMainWithCustomArgs", RoutingStrategy.Bubble, typeof(SearchMainWithCustomArgsEventHandler), typeof(SearchMainCtrl));
        public delegate void SearchMainWithCustomArgsEventHandler(object sender, ProcessEventArgs e);


        public delegate void ProcessHandler(object sender, ProcessEventArgs data);
        string _mode;
        string _invokingCtrlMode;
        string _invokingCtrlName;
        string _CtrlTabName;

        UserCtrlArgs UCArgs;

        List<StoreItem> StoreItemObjList;


        public SearchMainCtrl()
        {
            InitializeComponent();
        }

        public string Mode
        {
            get { return _mode; }
            set {_mode = value;}
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

        public string ControlTabName
        {
            get { return _CtrlTabName; }
            set { _CtrlTabName = value; }
        }

        public event SearchMainWithCustomArgsEventHandler SearchMainWithCustomArgs
        {
            add { AddHandler(SearchMainWithCustomArgsEvent, value); }
            remove { RemoveHandler(SearchMainWithCustomArgsEvent, value); }
        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;

            UCArgs = new UserCtrlArgs();
            UCArgs.InvokingControlName = this.Name;
            UCArgs.InvokingControlMode = this.Mode;
            UCArgs.TargetControlName = this.InvokingControlName;
            UCArgs.TargetControlMode = this.InvokingControlMode;
            UCArgs.ControlTabName = this.ControlTabName;

            ProcessEventArgs myargs = new ProcessEventArgs(SearchMainWithCustomArgsEvent, UCArgs.InvokingControlName, UCArgs.InvokingControlMode, UCArgs.TargetControlName, UCArgs.TargetControlMode, null, null, UCArgs.ControlTabName);
            RaiseEvent(myargs);
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
            string SelectedTabName;

            SelectedTabName = ((TabItem)tab_Main.SelectedItem).Name.ToString();

            try
            {
                #region Search Items

                if (SelectedTabName == tab_ItemSearch.Name)
                {
                    string errorMessage = null;

                    DateTime dt = DateTime.Now;

                    dg_SearchResults.Columns.Clear();

                    StoreItemObjList = new List<StoreItem>();
                    StoreItem[] StoreItemObjArray;
                    StoreItemFactoryClient StoreItemFactoryClient = new StoreItemFactoryClient();

                    if (StoreItemFactoryClient.GetStoreItemByCriteria(out errorMessage, out StoreItemObjArray, txt_ItemId.Text, txt_ItemName.Text, txt_BatchId.Text, Convert.ToDateTime(date_StartDateofManuf.SelectedDate), Convert.ToDateTime(date_StartDateofManuf.SelectedDate), Convert.ToDateTime(date_StartDateofExp.SelectedDate), Convert.ToDateTime(date_EndDateofExp.SelectedDate)) == true)
                    {
                        if (StoreItemObjArray != null)
                        {
                            StoreItemObjList = StoreItemObjArray.ToList<StoreItem>();
                            dg_SearchResults.ItemsSource = StoreItemObjList;

                            DataGridTextColumn NewColumn;

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Item Id";
                            NewColumn.Binding = new Binding("ItemId");
                            NewColumn.Width = 100;

                            dg_SearchResults.Columns.Add(NewColumn);

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Item Name";
                            NewColumn.Binding = new Binding("ItemName");
                            NewColumn.Width = 200;
                            dg_SearchResults.Columns.Add(NewColumn);

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Batch";
                            NewColumn.Binding = new Binding("BatchId");
                            NewColumn.Width = 100;
                            dg_SearchResults.Columns.Add(NewColumn);

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Qty Available";
                            NewColumn.Width = 100;
                            NewColumn.Binding = new Binding("QtyAvailable");
                            NewColumn.Binding.StringFormat = "{0:N2}";
                            dg_SearchResults.Columns.Add(NewColumn);

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "MRP";
                            NewColumn.Binding = new Binding("MRP");
                            NewColumn.Binding.StringFormat = "{0:C2}";
                            NewColumn.Width = 100;
                            dg_SearchResults.Columns.Add(NewColumn);

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Selling Price";
                            NewColumn.Width = 100;
                            NewColumn.Binding = new Binding("SellingPricePerUnit");
                            NewColumn.Binding.StringFormat = "{0:C2}";
                            dg_SearchResults.Columns.Add(NewColumn);

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Disc/Unit";
                            NewColumn.Binding = new Binding("DiscountAmountPerUnit");
                            NewColumn.Binding.StringFormat = "{0:C2}";
                            NewColumn.Width = 100;
                            dg_SearchResults.Columns.Add(NewColumn);
                        }
                        else
                        {
                            if ((errorMessage != "") && (errorMessage != null))
                            {
                                WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Search Items", 0, "INFORMATION");
                                MsgBox.ShowDialog();
                            }
                        }
                    }
                    else
                    {
                        if ((errorMessage != "") && (errorMessage != null))
                        {
                            WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Search Items", 0, "ERROR");
                            MsgBox.ShowDialog();
                        }
                    }
                }

                #endregion
            }
            catch (Exception ex1)
            {
                WinMessageBox MsgBox = new WinMessageBox(ex1.Message, "Items Search", 0, "ERROR");
                MsgBox.ShowDialog();
            }

            try
            {
                #region Search Sales

                if (SelectedTabName == tab_SalesSearch.Name)
                {
                    string errorMessage = null;
                    Sale[] SaleObjArray;
                    List<Sale> SaleObjList = new List<Sale>();
                    SaleFactoryClient SaleFactoryClient = new SaleFactoryClient();


                    DateTime dt = DateTime.Now;

                    dg_SearchResults.Columns.Clear();

                    if (SaleFactoryClient.SearchSaleDetails(out SaleObjArray, out errorMessage, txt_SalesId.Text, txt_SalesCustId.Text, txt_SalesCustName.Text, Convert.ToDateTime(date_StartSalesDate.SelectedDate), Convert.ToDateTime(date_EndSalesDate.SelectedDate)) == true)
                    {
                        if (SaleObjArray != null)
                        {
                            SaleObjList = SaleObjArray.ToList<Sale>();

                            dg_SearchResults.ItemsSource = SaleObjList;


                            DataGridTextColumn NewColumn;

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Sales Id";
                            NewColumn.Binding = new Binding("SalesId");
                            NewColumn.Width = 90;

                            dg_SearchResults.Columns.Add(NewColumn);

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Customer Id";
                            NewColumn.Binding = new Binding("CustomerInfo.CustomerId");
                            NewColumn.Width = 90;
                            dg_SearchResults.Columns.Add(NewColumn);

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Customer Name";
                            NewColumn.Binding = new Binding("CustomerInfo.CustomerName");
                            NewColumn.Width = 200;
                            dg_SearchResults.Columns.Add(NewColumn);

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Sale Date";
                            NewColumn.Binding = new Binding("SaleDate");
                            NewColumn.Width = 125;
                            dg_SearchResults.Columns.Add(NewColumn);

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Final Sale Amt";
                            NewColumn.Binding = new Binding("FinalSaleAmount");
                            NewColumn.Binding.StringFormat = "{0:C2}";
                            NewColumn.Width = 100;
                            dg_SearchResults.Columns.Add(NewColumn);

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Amount Paid";
                            NewColumn.Binding = new Binding("PaidAmount");
                            NewColumn.Binding.StringFormat = "{0:C2}";
                            NewColumn.Width = 100;
                            dg_SearchResults.Columns.Add(NewColumn);

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Balance";
                            NewColumn.Binding = new Binding("BalanceAmount");
                            NewColumn.Binding.StringFormat = "{0:C2}";
                            NewColumn.Width = 90;
                            dg_SearchResults.Columns.Add(NewColumn);
                        }
                        else
                            if ((errorMessage != "") && (errorMessage != null))
                            {
                                WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Search Sales", 0, "INFORMATION");
                                MsgBox.ShowDialog();
                            }
                    }
                    else
                    {
                        if ((errorMessage != "") && (errorMessage != null))
                        {
                            WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Search Sales", 0, "ERROR");
                            MsgBox.ShowDialog();
                        }
                    }
                }

                #endregion
            }
            catch (Exception ex1)
            {
                WinMessageBox MsgBox = new WinMessageBox(ex1.Message, "Sales Search", 0, "ERROR");
                MsgBox.ShowDialog();
            }

            try
            {
                #region Search Customers

                if (SelectedTabName == tab_CustSearch.Name)
                {
                    string errorMessage = null;

                    Customer[] CustObjArray;
                    List<Customer> CustObjList = new List<Customer>();

                    DateTime dt = DateTime.Now;

                    dg_SearchResults.Columns.Clear();

                    CustomerFactoryClient CustomerFactoryClient = new CustomerFactoryClient();

                    if (CustomerFactoryClient.GetCustomersByCriteria(out errorMessage, out CustObjArray, txt_CustId.Text, txt_CustName.Text, txt_CustAddress.Text, -1, null, Convert.ToDateTime(date_FromDateofBirth.SelectedDate), Convert.ToDateTime(date_ToDateofBirth.SelectedDate), Convert.ToDateTime(date_FromDateofStartRelationship.SelectedDate), Convert.ToDateTime(date_ToDateofStartRelationship.SelectedDate), Convert.ToDateTime(date_FromRelationshipExpiryDate.SelectedDate), Convert.ToDateTime(date_ToRelationshipExpiryDate.SelectedDate)) == true)
                    {
                        if (CustObjArray != null)
                        {
                            CustObjList = CustObjArray.ToList<Customer>();
                            dg_SearchResults.ItemsSource = CustObjList;

                            DataGridTextColumn NewColumn;

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Customer Id";
                            NewColumn.Binding = new Binding("CustomerId");
                            NewColumn.Width = 90;
                            dg_SearchResults.Columns.Add(NewColumn);

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Customer Name";
                            NewColumn.Binding = new Binding("CustomerName");
                            NewColumn.Width = 175;
                            dg_SearchResults.Columns.Add(NewColumn);

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Date of Birth";
                            NewColumn.Binding = new Binding("DateofBirth");
                            NewColumn.Width = 100;
                            dg_SearchResults.Columns.Add(NewColumn);

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Paid";
                            NewColumn.Width = 90;
                            NewColumn.Binding = new Binding("AmountPaidYTD");
                            NewColumn.Binding.StringFormat = "{0:C2}";
                            dg_SearchResults.Columns.Add(NewColumn);

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Balance";
                            NewColumn.Width = 90;
                            NewColumn.Binding = new Binding("AmountTobePaid");
                            NewColumn.Binding.StringFormat = "{0:C2}";
                            dg_SearchResults.Columns.Add(NewColumn);

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Start Date";
                            NewColumn.Width = 125;
                            NewColumn.Binding = new Binding("RelationshipStartDate");
                            dg_SearchResults.Columns.Add(NewColumn);

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "End Date";
                            NewColumn.Width = 125;
                            NewColumn.Binding = new Binding("RelationshipExpiryDate");
                            dg_SearchResults.Columns.Add(NewColumn);
                        }
                        else
                            if ((errorMessage != "") && (errorMessage != null))
                            {
                                WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Search Customers", 0, "INFORMATION");
                                MsgBox.ShowDialog();
                            }
                    }
                    else
                        if ((errorMessage != "") && (errorMessage != null))
                        {
                            WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Search Customers", 0, "ERROR");
                            MsgBox.ShowDialog();
                        }
                }

                #endregion
            }
            catch (Exception ex1)
            {
                WinMessageBox MsgBox = new WinMessageBox(ex1.Message, "Customers Search", 0, "ERROR");
                MsgBox.ShowDialog();
            }

            try
            {
                #region Search Manufacturers

                if (SelectedTabName == tabManufSearch.Name)
                {
                    string errorMessage = null;

                    DateTime dt = DateTime.Now;

                    dg_SearchResults.Columns.Clear();

                    Manufacturer[] ManufObjArray;
                    List<Manufacturer> ManufObjList = new List<Manufacturer>();

                    ManufacturerFactoryClient ManufFactoryClient = new ManufacturerFactoryClient();

                    if (ManufFactoryClient.GetManufacturersByCriteria(out errorMessage, out ManufObjArray, txtManufId.Text, txtManufName.Text, txtManufAddress.Text, "", "", Convert.ToDateTime(date_FromDateofBirth.SelectedDate), Convert.ToDateTime(date_ToDateofBirth.SelectedDate), Convert.ToDateTime(date_FromDateofStartRelationship.SelectedDate), Convert.ToDateTime(date_ToDateofStartRelationship.SelectedDate), Convert.ToDateTime(date_FromRelationshipExpiryDate.SelectedDate), Convert.ToDateTime(date_ToRelationshipExpiryDate.SelectedDate)) == true)
                    {
                        if (ManufObjArray != null)
                        {
                            ManufObjList = ManufObjArray.ToList<Manufacturer>();
                            dg_SearchResults.ItemsSource = ManufObjList;

                            DataGridTextColumn NewColumn;

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Manuf Id";
                            NewColumn.Binding = new Binding("ManufId");
                            NewColumn.Width = 100;
                            dg_SearchResults.Columns.Add(NewColumn);

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Manuf Name";
                            NewColumn.Binding = new Binding("ManufName");
                            NewColumn.Width = 200;
                            dg_SearchResults.Columns.Add(NewColumn);

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Date of Birth";
                            NewColumn.Binding = new Binding("DateofBirth");
                            NewColumn.Width = 100;
                            dg_SearchResults.Columns.Add(NewColumn);

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Gender";
                            NewColumn.Binding = new Binding("Gender");
                            NewColumn.Width = 100;
                            dg_SearchResults.Columns.Add(NewColumn);

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Address";
                            NewColumn.Width = 100;
                            NewColumn.Binding = new Binding("Address");
                            dg_SearchResults.Columns.Add(NewColumn);

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "IDProof Type";
                            NewColumn.Width = 100;
                            NewColumn.Binding = new Binding("IDProofType");
                            dg_SearchResults.Columns.Add(NewColumn);

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "IDProof Value";
                            NewColumn.Width = 100;
                            NewColumn.Binding = new Binding("IDProofValue");
                            dg_SearchResults.Columns.Add(NewColumn);

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Relationship Start Date";
                            NewColumn.Width = 100;
                            NewColumn.Binding = new Binding("RelationshipStartDate");
                            dg_SearchResults.Columns.Add(NewColumn);

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Relationship Expiry Date";
                            NewColumn.Width = 100;
                            NewColumn.Binding = new Binding("RelationshipExpiryDate");
                            dg_SearchResults.Columns.Add(NewColumn);

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Amount To be Paid";
                            NewColumn.Width = 100;
                            NewColumn.Binding = new Binding("AmountTobePaid");
                            NewColumn.Binding.StringFormat = "{0:C2}";
                            dg_SearchResults.Columns.Add(NewColumn);

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Amount Paid YTD";
                            NewColumn.Width = 100;
                            NewColumn.Binding = new Binding("AmountPaidYTD");
                            NewColumn.Binding.StringFormat = "{0:C2}";
                            dg_SearchResults.Columns.Add(NewColumn);
                        }
                        else
                            if ((errorMessage != "") && (errorMessage != null))
                            {
                                WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Search Manufacturers", 0, "INFORMATION");
                                MsgBox.ShowDialog();
                            }
                    }
                    else
                        if ((errorMessage != "") && (errorMessage != null))
                        {
                            WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Search Manufacturers", 0, "ERROR");
                            MsgBox.ShowDialog();
                        }
                }

                #endregion
            }
            catch (Exception ex1)
            {
                WinMessageBox MsgBox = new WinMessageBox(ex1.Message, "Vendors Search", 0, "ERROR");
                MsgBox.ShowDialog();
            }

            try
            {
                #region Search Vendors

                if (SelectedTabName == tabVendorSearch.Name)
                {
                    string errorMessage = null;

                    DateTime dt = DateTime.Now;

                    dg_SearchResults.Columns.Clear();

                    Vendor[] VendorObjArray;
                    List<Vendor> VendorObjList = new List<Vendor>();

                    VendorFactoryClient VendorFactoryClient = new VendorFactoryClient();

                    if (VendorFactoryClient.GetVendorsByCriteria(out errorMessage, out VendorObjArray, txtVendorId.Text, txtVendorName.Text, txtVendorAddress.Text, "", "", Convert.ToDateTime(date_FromDateofBirth.SelectedDate), Convert.ToDateTime(date_ToDateofBirth.SelectedDate), Convert.ToDateTime(date_FromDateofStartRelationship.SelectedDate), Convert.ToDateTime(date_ToDateofStartRelationship.SelectedDate), Convert.ToDateTime(date_FromRelationshipExpiryDate.SelectedDate), Convert.ToDateTime(date_ToRelationshipExpiryDate.SelectedDate)) == true)
                    {
                        if (VendorObjArray != null)
                        {
                            VendorObjList = VendorObjArray.ToList<Vendor>();
                            dg_SearchResults.ItemsSource = VendorObjList;

                            DataGridTextColumn NewColumn;

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Vendor Id";
                            NewColumn.Binding = new Binding("VendorId");
                            NewColumn.Width = 100;
                            dg_SearchResults.Columns.Add(NewColumn);

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Vendor Name";
                            NewColumn.Binding = new Binding("VendorName");
                            NewColumn.Width = 200;
                            dg_SearchResults.Columns.Add(NewColumn);

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Date of Birth";
                            NewColumn.Binding = new Binding("DateofBirth");
                            NewColumn.Width = 100;
                            dg_SearchResults.Columns.Add(NewColumn);

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Gender";
                            NewColumn.Binding = new Binding("Gender");
                            NewColumn.Width = 100;
                            dg_SearchResults.Columns.Add(NewColumn);

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Address";
                            NewColumn.Width = 100;
                            NewColumn.Binding = new Binding("Address");
                            dg_SearchResults.Columns.Add(NewColumn);

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "IDProof Type";
                            NewColumn.Width = 100;
                            NewColumn.Binding = new Binding("IDProofType");
                            dg_SearchResults.Columns.Add(NewColumn);

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "IDProof Value";
                            NewColumn.Width = 100;
                            NewColumn.Binding = new Binding("IDProofValue");
                            dg_SearchResults.Columns.Add(NewColumn);

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Relationship Start Date";
                            NewColumn.Width = 100;
                            NewColumn.Binding = new Binding("RelationshipStartDate");
                            dg_SearchResults.Columns.Add(NewColumn);

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Relationship Expiry Date";
                            NewColumn.Width = 100;
                            NewColumn.Binding = new Binding("RelationshipExpiryDate");
                            dg_SearchResults.Columns.Add(NewColumn);

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Amount To be Paid";
                            NewColumn.Width = 100;
                            NewColumn.Binding = new Binding("AmountTobePaid");
                            NewColumn.Binding.StringFormat = "{0:C2}";
                            dg_SearchResults.Columns.Add(NewColumn);

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Amount Paid YTD";
                            NewColumn.Width = 100;
                            NewColumn.Binding = new Binding("AmountPaidYTD");
                            NewColumn.Binding.StringFormat = "{0:C2}";
                            dg_SearchResults.Columns.Add(NewColumn);
                        }
                        else
                            if ((errorMessage != "") && (errorMessage != null))
                            {
                                WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Search Vendors", 0, "INFORMATION");
                                MsgBox.ShowDialog();
                            }
                    }
                    else
                        if ((errorMessage != "") && (errorMessage != null))
                        {
                            WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Search Vendors", 0, "ERROR");
                            MsgBox.ShowDialog();
                        }
                }

                #endregion
            }
            catch (Exception ex1)
            {
                WinMessageBox MsgBox = new WinMessageBox(ex1.Message, "Vendors Search", 0, "ERROR");
                MsgBox.ShowDialog();
            }
        }

        private void dg_SearchResults_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string errorMessage = null;

            try
            {
                string SelectedTabName = ((TabItem)tab_Main.SelectedItem).Name.ToString();

                if (SelectedTabName == tab_SalesSearch.Name)
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
            }
            catch (Exception ex1)
            {
                WinMessageBox MsgBox = new WinMessageBox(ex1.Message, "Sales Details", 0, "ERROR");
                MsgBox.ShowDialog();
            }
        }
    }
}
