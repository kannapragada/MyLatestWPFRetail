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
using NewApp.UI;
using NewApp.UI.UserControls;
using NewApp.BusinessTier.Models;
using NewApp.ExcelInterOp;
using NewApp.UI.Windows;
using NewApp.CustomerFactorySvc;
using NewApp.StoreItemFactorySvc;
using NewApp.ManufFactorySvc;
using NewApp.VendorFactorySvc;
using NewApp.TaxFactorySvc;
using NewApp.TaxKindFactorySvc;
using NewApp.TaxTypeFactorySvc;
using NewApp.TaxItemFactorySvc;
using NewApp.DiscountFactorySvc;
using NewApp.DiscountItemFactorySvc;
using NewApp.DiscountTypeFactorySvc;
using NewApp.UserFactorySvc;


namespace NewApp.ExcelInterOp.UserControls
{
    /// <summary>
    /// Interaction logic for ImportFromExcel.xaml
    /// </summary>
    public partial class ExcelInterOpCtrl : UserControl
    {
        public static readonly RoutedEvent ExcelInterOpWithCustomArgsEvent = EventManager.RegisterRoutedEvent("ExcelInterOpWithCustomArgs", RoutingStrategy.Bubble, typeof(ExcelInterOpWithCustomArgsEventHandler), typeof(ExcelInterOpCtrl));
        public delegate void ExcelInterOpWithCustomArgsEventHandler(object sender, ProcessEventArgs e);


        public delegate void ProcessHandler(object sender, ProcessEventArgs data);
        string _mode;
        string _invokingCtrlMode;
        string _invokingCtrlName;
        string _CtrlTabName;

        UserCtrlArgs UCArgs;

        ExcelData exceldata;
        Customer CustObj;
        StoreItem StoreItemObj;
        Manufacturer ManufObj;
        Vendor VendorObj;
        TaxItem TaxItemObj;
        Tax TaxObj;
        Discount DiscObj;
        DiscountItem DiscItemObj;
        User UserObj;

        StoreItemFactoryClient MyStoreItemFactoryClient;
        ManufacturerFactoryClient MyManufacturerFactoryClient;
        VendorFactoryClient MyVendorFactoryClient;
        TaxFactoryClient MyTaxFactoryClient;
        DiscountFactoryClient MyDiscountFactoryClient;
        DiscountItemFactoryClient MyDiscountItemFactoryClient;

        DataSet Ds;
        DataTable MyDt;
        DataTable MyDt1;

        public ExcelInterOpCtrl()
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

        public event ExcelInterOpWithCustomArgsEventHandler ExcelInterOpWithCustomArgs
        {
            add { AddHandler(ExcelInterOpWithCustomArgsEvent, value); }
            remove { RemoveHandler(ExcelInterOpWithCustomArgsEvent, value); }
        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            UCArgs = new UserCtrlArgs();
            UCArgs.InvokingControlName = this.Name;
            UCArgs.InvokingControlMode = this.Mode;
            UCArgs.TargetControlName = this.InvokingControlName;
            UCArgs.TargetControlMode = this.InvokingControlMode;
            UCArgs.ControlTabName = this.ControlTabName;

            e.Handled = true;
            ProcessEventArgs myargs = new ProcessEventArgs(ExcelInterOpWithCustomArgsEvent, UCArgs.InvokingControlName, UCArgs.InvokingControlMode, UCArgs.TargetControlName, UCArgs.TargetControlMode, null, null, UCArgs.ControlTabName);
            RaiseEvent(myargs);
        }

        private void btn_ExportToExcel_Click(object sender, RoutedEventArgs e)
        {
            ExportToExcel<Customer, List<Customer>> s = new ExportToExcel<Customer, List<Customer>>();

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

            s.dataToPrint = MyCustList;
            s.GenerateReport();
        }

        private void btn_UploadtoDb_Click(object sender, RoutedEventArgs e)
        {
            string errorMessage = null;

            WinMessageBox MsgBox;

            try
            {
                DataRow Dr;
                DataView dv;
                int itemRowPos;

                exceldata = new ExcelData();

                ListBoxItem MyItem;

                Ds = exceldata.Data;

                MyItem = new ListBoxItem();
                MyItem.Content = "Completed reading Excel Data";
                lb_Sequence.Items.Add(MyItem);


                if (Ds != null)
                {
                    if (Ds.Tables.Count <= 0)
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


                MyItem = new ListBoxItem();
                MyItem.Content = "Start Upload Data";
                lb_Sequence.Items.Add(MyItem);

                #region Customer

                    MyDt = (DataTable)Ds.Tables["Customer"];

                    dv = MyDt.DefaultView;
                    dv.Sort = "RelationshipStartDate asc";
                    MyDt = dv.ToTable();

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

                        CustObj.CreateDate = DateTime.Now;
                        CustObj.LastModDate = DateTime.Now;

                        CustObjArray[i] = CustObj;
                    }

                    List<Customer> MyCustList = new List<Customer>();
                    MyCustList = CustObjArray.ToList<Customer>();

                        #region CreateCustomer

                            CustomerFactoryClient CustomerFactoryClient = new CustomerFactoryClient();

                            foreach (Customer CustObj in MyCustList)
                            {
                                if (CustomerFactoryClient.AddCustomerDetails(out errorMessage, CustObj) == false)
                                {
                                    MsgBox = new WinMessageBox(errorMessage, "Customer Details - Add", 0, "ERROR");
                                    MsgBox.ShowDialog();
                                    return;
                                }
                            }

                        #endregion

                    #endregion

                MyItem = new ListBoxItem();
                MyItem.Content = "Customer Data Upload Completed";
                lb_Sequence.Items.Add(MyItem);

                #region Manufacturer

                    MyDt = (DataTable)Ds.Tables["Manufacturer"];

                    dv = MyDt.DefaultView;
                    dv.Sort = "RelationshipStartDate asc";
                    MyDt = dv.ToTable();

                    Manufacturer[] ManufObjArray = new Manufacturer[MyDt.Rows.Count];


                    for (int i = 0; i < MyDt.Rows.Count; i++)
                    {
                        ManufObj = new Manufacturer();

                        Dr = MyDt.Rows[i];

                        ManufObj.ManufacturerId = Dr[0].ToString();
                        ManufObj.ManufacturerName = Dr[1].ToString();
                        ManufObj.Status.ManufacturerStatusId = Convert.ToInt32(Dr[2].ToString());
                        ManufObj.DateofBirth = Convert.ToDateTime(Dr[3].ToString());
                        ManufObj.GenderType.GenderTypeId = Convert.ToInt32(Dr[4].ToString());
                        ManufObj.PresentAddress = Dr[5].ToString();
                        ManufObj.PresentCity = Dr[6].ToString();
                        ManufObj.PresentPinCode = Dr[7].ToString();
                        ManufObj.PresentPhoneNo = Dr[8].ToString();
                        ManufObj.Mobile = Dr[9].ToString();
                        ManufObj.EMailId = Dr[10].ToString();
                        if (Dr[11].ToString() == "0")
                            ManufObj.IsPresentPermAddressSame = false;
                        else if (Dr[11].ToString() == "1")
                            ManufObj.IsPresentPermAddressSame = true;
                        ManufObj.PermanentAddress = Dr[12].ToString();
                        ManufObj.PermanentCity = Dr[13].ToString();
                        ManufObj.PermanentPinCode = Dr[14].ToString();
                        ManufObj.PermanentPhoneNo = Dr[15].ToString();
                        ManufObj.IDProofType.IDProofTypeId = Convert.ToInt32(Dr[16].ToString());
                        if ((Dr[18].ToString() != null) && (Dr[18].ToString() != ""))
                            ManufObj.RelationshipStartDate = Convert.ToDateTime(Dr[18].ToString());
                        if ((Dr[19].ToString() != null) && (Dr[19].ToString() != ""))
                            ManufObj.RelationshipExpiryDate = Convert.ToDateTime(Dr[19].ToString());
                        if (Dr[20] != null)
                            ManufObj.AmountTobePaid = Convert.ToDouble(Dr[20].ToString());
                        if (Dr[21] != null)
                            ManufObj.AmountPaidYTD = Convert.ToDouble(Dr[21].ToString());

                        ManufObj.CreateDate = DateTime.Now;
                        ManufObj.LastModDate = DateTime.Now;

                        ManufObjArray[i] = ManufObj;
                    }

                    List<Manufacturer> MyManufList = new List<Manufacturer>();
                    MyManufList = ManufObjArray.ToList<Manufacturer>();

                    #region CreateManufacturer

                        MyManufacturerFactoryClient = new ManufacturerFactoryClient();

                        foreach (Manufacturer ManufObj in MyManufList)
                        {
                            if (MyManufacturerFactoryClient.AddManufacturerDetails(out errorMessage, ManufObj) == false)
                            {
                                MsgBox = new WinMessageBox(errorMessage, "Manufacturer Details - Add", 0, "ERROR");
                                MsgBox.ShowDialog();
                                return;
                            }
                        }

                    #endregion

                #endregion

                MyItem = new ListBoxItem();
                MyItem.Content = "Manufacturer Data Upload Completed";
                lb_Sequence.Items.Add(MyItem);

                #region Vendor

                    MyDt = (DataTable)Ds.Tables["Vendor"];    
                
                    dv = MyDt.DefaultView;
                    dv.Sort = "RelationshipStartDate asc";
                    MyDt = dv.ToTable();

                    Vendor[] VendorObjArray = new Vendor[MyDt.Rows.Count];


                    for (int i = 0; i < MyDt.Rows.Count; i++)
                    {
                        VendorObj = new Vendor();

                        Dr = MyDt.Rows[i];

                        VendorObj.VendorId = Dr[0].ToString();
                        VendorObj.VendorName = Dr[1].ToString();
                        VendorObj.Status.VendorStatusId = Convert.ToInt32(Dr[2].ToString());
                        VendorObj.DateofBirth = Convert.ToDateTime(Dr[3].ToString());
                        VendorObj.GenderType.GenderTypeId = Convert.ToInt32(Dr[4].ToString());
                        VendorObj.PresentAddress = Dr[5].ToString();
                        VendorObj.PresentCity = Dr[6].ToString();
                        VendorObj.PresentPinCode = Dr[7].ToString();
                        VendorObj.PresentPhoneNo = Dr[8].ToString();
                        VendorObj.Mobile = Dr[9].ToString();
                        VendorObj.EMailId = Dr[10].ToString();
                        if (Dr[11].ToString() == "0")
                            VendorObj.IsPresentPermAddressSame = false;
                        else if (Dr[11].ToString() == "1")
                            VendorObj.IsPresentPermAddressSame = true;
                        VendorObj.PermanentAddress = Dr[12].ToString();
                        VendorObj.PermanentCity = Dr[13].ToString();
                        VendorObj.PermanentPinCode = Dr[14].ToString();
                        VendorObj.PermanentPhoneNo = Dr[15].ToString();
                        VendorObj.IDProofType.IDProofTypeId = Convert.ToInt32(Dr[16].ToString());
                        if ((Dr[18].ToString() != null) && (Dr[18].ToString() != ""))
                            VendorObj.RelationshipStartDate = Convert.ToDateTime(Dr[18].ToString());
                        if ((Dr[19].ToString() != null) && (Dr[19].ToString() != ""))
                            VendorObj.RelationshipExpiryDate = Convert.ToDateTime(Dr[19].ToString());
                        if (Dr[20] != null)
                            VendorObj.AmountTobePaid = Convert.ToDouble(Dr[20].ToString());
                        if (Dr[21] != null)
                            VendorObj.AmountPaidYTD = Convert.ToDouble(Dr[21].ToString());

                        VendorObj.CreateDate = DateTime.Now;
                        VendorObj.LastModDate = DateTime.Now;

                        VendorObjArray[i] = VendorObj;
                    }

                    List<Vendor> MyVendorList = new List<Vendor>();
                    MyVendorList = VendorObjArray.ToList<Vendor>();

                    #region CreateVendor

                        MyVendorFactoryClient = new VendorFactoryClient();

                        foreach (Vendor VendorObj in MyVendorList)
                        {
                            if (MyVendorFactoryClient.AddVendorDetails(out errorMessage, VendorObj) == false)
                            {
                                MsgBox = new WinMessageBox(errorMessage, "Vendor Details - Add", 0, "ERROR");
                                MsgBox.ShowDialog();
                                return;
                            }
                        }

                    #endregion

                #endregion

                MyItem = new ListBoxItem();
                MyItem.Content = "Vendor Data Upload Completed";
                lb_Sequence.Items.Add(MyItem);

                #region StoreItem

                    MyStoreItemFactoryClient = new StoreItemFactoryClient();
                    MyVendorFactoryClient = new VendorFactoryClient();

                    MyDt = Ds.Tables["ItemDetail"];
                    MyDt1 = Ds.Tables["ItemBasic"];

                    dv = MyDt.DefaultView;
                    dv.Sort = "IBItemId asc";
                    MyDt = dv.ToTable();

                    dv = MyDt1.DefaultView;
                    dv.Sort = "ItemId asc";
                    MyDt1 = dv.ToTable();

                    StoreItem[] StoreItemObjArray = new StoreItem[MyDt.Rows.Count];

                    for (int i = 0; i < MyDt1.Rows.Count; i++)
                    {
                        StoreItemObj = new StoreItem();

                        Dr = MyDt.Rows[i];

                        StoreItemObj.ItemId = Dr[0].ToString();
                        StoreItemObj.BatchId = Dr[1].ToString();
                        StoreItemObj.BarCode = Dr[2].ToString();
                        StoreItemObj.MRP = Convert.ToDouble(Dr[3].ToString());
                        StoreItemObj.QtyProcured = Convert.ToInt32(Dr[4].ToString());
                        StoreItemObj.ProcuredPricePerUnit = Convert.ToDouble(Dr[5].ToString());
                        StoreItemObj.DateProcured = Convert.ToDateTime(Dr[6].ToString());
                        StoreItemObj.WeightProcured = Convert.ToDouble(Dr[7].ToString());
                        StoreItemObj.QtyAvailable = Convert.ToInt32(Dr[8].ToString());
                        StoreItemObj.WeightAvailable = Convert.ToDouble(Dr[9].ToString());
                        StoreItemObj.DateOfManuf = Convert.ToDateTime(Dr[10].ToString());
                        StoreItemObj.DateOfExp = Convert.ToDateTime(Dr[11].ToString());
                        StoreItemObj.SellingPricePerUnit = Convert.ToDouble(Dr[12].ToString());
                        StoreItemObj.ItemCreateDate = DateTime.Now;
                        StoreItemObj.BatchCreateDate = DateTime.Now;

                        dv = Ds.Tables["Manufacturer"].DefaultView;
                        dv.Sort = "Name asc";

                        string Str = Dr[13].ToString();

                        MyManufacturerFactoryClient = new ManufacturerFactoryClient();
                        List<Manufacturer> ManufObjList = new List<Manufacturer>();
                        Manufacturer MyManufacturer = new Manufacturer();

                        if (MyManufacturerFactoryClient.GetManufacturers(out errorMessage, out ManufObjArray, Str) == true)
                        {
                            if (ManufObjArray != null)
                            {
                                ManufObjList = ManufObjArray.ToList<Manufacturer>();
                                if (ManufObjList.Count > 0)
                                {
                                    MyManufacturer = ManufObjList.Find(ManufObj => ManufObj.ManufacturerName == Dr[13].ToString());
                                    StoreItemObj.ManufacturerInfo.ManufacturerId = MyManufacturer.ManufacturerId;
                                }
                            }
                        }

                        dv = Ds.Tables["Vendor"].DefaultView;
                        dv.Sort = "Name asc";

                        Str = Dr[14].ToString();

                        MyVendorFactoryClient = new VendorFactoryClient();
                        List<Vendor> VendorObjList = new List<Vendor>();
                        Vendor MyVendor = new Vendor();

                        if (MyVendorFactoryClient.GetVendors(out errorMessage, out VendorObjArray, Str) == true)
                        {
                            if (VendorObjArray != null)
                            {
                                VendorObjList = VendorObjArray.ToList<Vendor>();
                                if (VendorObjList.Count > 0)
                                {
                                    MyVendor = VendorObjList.Find(VendorObj => VendorObj.VendorName == Dr[14].ToString());
                                    StoreItemObj.VendorInfo.VendorId = MyVendor.VendorId;
                                }
                            }
                        }

                        dv = Ds.Tables["ItemBasic"].DefaultView;
                        dv.Sort = "ItemId asc";
                        
                        itemRowPos = dv.Find(StoreItemObj.ItemId.ToString());
                        StoreItemObj.ItemName = dv[itemRowPos][1].ToString();
                        StoreItemObj.ItemDescription = dv[itemRowPos][2].ToString();

                        StoreItemObjArray[i] = StoreItemObj;
                    }

                    List<StoreItem> MyStoreItemList = new List<StoreItem>();
                    MyStoreItemList = StoreItemObjArray.ToList<StoreItem>();

                    #region CreateStoreItem

                    MyStoreItemFactoryClient = new StoreItemFactoryClient();

                    foreach (StoreItem StoreItemObj in MyStoreItemList)
                    {
                        if (MyStoreItemFactoryClient.AddStoreItem(out errorMessage, StoreItemObj) == false)
                        {
                            MsgBox = new WinMessageBox(errorMessage, "Store Item Details - Add", 0, "ERROR");
                            MsgBox.ShowDialog();
                            return;
                        }
                    }

                    #endregion

                #endregion

                MyItem = new ListBoxItem();
                MyItem.Content = "StoreItem Data Upload Completed";
                lb_Sequence.Items.Add(MyItem);

                #region Tax

                    MyDt = Ds.Tables["TaxBasic"];
                    MyDt1 = Ds.Tables["TaxItemDtl"];

                    dv = MyDt.DefaultView;
                    dv.Sort = "TaxCode asc";
                    MyDt = dv.ToTable();

                    dv = MyDt1.DefaultView;
                    dv.Sort = "TITaxCode asc";
                    MyDt1 = dv.ToTable();


                    Tax[] TaxObjArray = new Tax[MyDt.Rows.Count];

                    for (int i = 0; i < MyDt.Rows.Count; i++)
                    {
                        TaxObj = new Tax();

                        Dr = MyDt.Rows[i];

                        TaxObj.TaxCode = Dr[0].ToString();
                        TaxObj.TaxName = Dr[1].ToString();
                        TaxObj.TaxDescription = Dr[2].ToString();
                        TaxObj.TaxKind.TaxKindId = Convert.ToInt32(Dr[3].ToString());
                        TaxObj.TaxType.TaxTypeId = Convert.ToInt32(Dr[4].ToString());
                        TaxObj.TaxValue = Convert.ToDouble(Dr[5].ToString());
                        TaxObj.StartDate = Convert.ToDateTime(Dr[6].ToString());
                        TaxObj.EndDate = Convert.ToDateTime(Dr[7].ToString());
                        TaxObj.CreateDate = DateTime.Now;

                        TaxObjArray[i] = TaxObj;
                    }

                    List<Tax> MyTaxList = new List<Tax>();
                    MyTaxList = TaxObjArray.ToList<Tax>();

                    #region CreateTax

                        MyTaxFactoryClient = new TaxFactoryClient();

                        foreach (Tax TaxObj in MyTaxList)
                        {
                            if (MyTaxFactoryClient.AddNewTax(out errorMessage, TaxObj) == false)
                            {
                                MsgBox = new WinMessageBox(errorMessage, "Tax Details - Add", 0, "ERROR");
                                MsgBox.ShowDialog();
                                return;
                            }
                        }

                    #endregion

                    if (MyTaxFactoryClient.GetTaxList(out errorMessage, out TaxObjArray, "ALL") == true)
                    {
                        MyTaxList = TaxObjArray.ToList<Tax>();

                        foreach (DataRow MyDataRow in MyDt.Rows)
                        {
                            foreach (Tax TaxObj in MyTaxList)
                            {
                                if (MyDataRow[1].ToString() == TaxObj.TaxName)
                                {
                                    MyDataRow[8] = TaxObj.TaxCode;
                                }
                            }
                        }
                    }

                    dv = MyDt.DefaultView;
                    dv.Sort = "TaxCode asc";

                    MyStoreItemFactoryClient = new StoreItemFactoryClient();

                    if (MyStoreItemFactoryClient.GetStoreItemByItemIdOrName(out errorMessage, out StoreItemObjArray, "ALL") == true)
                    {
                        MyStoreItemList = StoreItemObjArray.ToList<StoreItem>();

                        foreach (DataRow MyDataRow in MyDt1.Rows)
                        {
                            foreach (StoreItem StoreItemObj in MyStoreItemList)
                            {
                                if (MyDataRow[2].ToString() == StoreItemObj.BatchId)
                                {
                                    MyDataRow[1] = StoreItemObj.ItemId;
                                }
                            }
                        }
                    }

                    TaxItem[] TaxItemObjArray = new TaxItem[MyDt1.Rows.Count];

                    for (int i = 0; i < MyDt1.Rows.Count; i++)
                    {
                        TaxItemObj = new TaxItem();

                        Dr = MyDt1.Rows[i];

                        TaxItemObj.TaxCode = Dr[0].ToString();
                        TaxItemObj.ItemId = Dr[1].ToString();
                        TaxItemObj.BatchId = Dr[2].ToString();
                        TaxItemObj.StartDate = Convert.ToDateTime(Dr[3].ToString());
                        TaxItemObj.EndDate = Convert.ToDateTime(Dr[4].ToString());
                        TaxItemObj.Remarks = Dr[5].ToString();
                        TaxItemObj.CreateDate = DateTime.Now;

                        itemRowPos = dv.Find(TaxItemObj.TaxCode.ToString());
                        TaxItemObj.TaxCode = dv[itemRowPos][8].ToString();

                        TaxItemObjArray[i] = TaxItemObj;
                    }

                    List<TaxItem> MyTaxItemList = new List<TaxItem>();
                    MyTaxItemList = TaxItemObjArray.ToList<TaxItem>();

                    #region CreateTaxItem

                        TaxItemFactoryClient MyTaxItemFactoryClient = new TaxItemFactoryClient();

                        foreach (TaxItem TaxItemObj in MyTaxItemList)
                        {
                            if (MyTaxItemFactoryClient.AddItemToTaxCode(out errorMessage, TaxItemObj) == false)
                            {
                                MsgBox = new WinMessageBox(errorMessage, "Tax Item Details - Add", 0, "ERROR");
                                MsgBox.ShowDialog();
                                return;
                            }
                        }

                    #endregion

                #endregion

                MyItem = new ListBoxItem();
                MyItem.Content = "TaxItem Data Upload Completed";
                lb_Sequence.Items.Add(MyItem);

                #region Discounts

                    MyDt = Ds.Tables["DiscBasic"];
                    MyDt1 = Ds.Tables["DiscItemDtl"];

                    dv = MyDt.DefaultView;
                    dv.Sort = "DiscCode asc";
                    MyDt = dv.ToTable();

                    dv = MyDt1.DefaultView;
                    dv.Sort = "DiscIDiscCode asc";
                    MyDt1 = dv.ToTable();


                    Discount[] DiscObjArray = new Discount[MyDt.Rows.Count];

                    for (int i = 0; i < MyDt.Rows.Count; i++)
                    {
                        DiscObj = new Discount();

                        Dr = MyDt.Rows[i];

                        DiscObj.DiscountCode = Dr[0].ToString();
                        DiscObj.DiscountName = Dr[1].ToString();
                        DiscObj.DiscountDescription = Dr[2].ToString();
                        DiscObj.DiscountKind.DiscountKindId = Convert.ToInt32(Dr[3].ToString());
                        DiscObj.DiscountType.DiscountTypeId = Convert.ToInt32(Dr[4].ToString());
                        DiscObj.DiscountValue = Convert.ToDouble(Dr[5].ToString());
                        DiscObj.DiscStartDate = Convert.ToDateTime(Dr[6].ToString());
                        DiscObj.DiscEndDate = Convert.ToDateTime(Dr[7].ToString());
                        DiscObj.DiscCreateDate = DateTime.Now;
                        DiscObj.DiscLastModDate = DateTime.Now;

                        DiscObjArray[i] = DiscObj;
                    }

                    List<Discount> MyDiscList = new List<Discount>();
                    MyDiscList = DiscObjArray.ToList<Discount>();

                    #region CreateDisc

                        MyDiscountFactoryClient = new DiscountFactoryClient();

                        foreach (Discount DiscObj in MyDiscList)
                        {
                            if (MyDiscountFactoryClient.AddNewDiscount(out errorMessage, DiscObj) == false)
                            {
                                MsgBox = new WinMessageBox(errorMessage, "Discount Details - Add", 0, "ERROR");
                                MsgBox.ShowDialog();
                                return;
                            }
                        }

                    #endregion

                    if (MyDiscountFactoryClient.GetDiscountList(out errorMessage, out DiscObjArray, "ALL") == true)
                    {
                        MyDiscList = DiscObjArray.ToList<Discount>();

                        foreach (DataRow MyDataRow in MyDt.Rows)
                        {
                            foreach (Discount DiscObj in MyDiscList)
                            {
                                if (MyDataRow[1].ToString() == DiscObj.DiscountName)
                                {
                                    MyDataRow[7] = DiscObj.DiscountCode;
                                }
                            }
                        }
                    }

                    dv = MyDt.DefaultView;
                    dv.Sort = "DiscCode asc";

                    MyStoreItemFactoryClient = new StoreItemFactoryClient();

                    if (MyStoreItemFactoryClient.GetStoreItemByItemIdOrName(out errorMessage, out StoreItemObjArray, "ALL") == true)
                    {
                        MyStoreItemList = StoreItemObjArray.ToList<StoreItem>();

                        foreach (DataRow MyDataRow in MyDt1.Rows)
                        {
                            foreach (StoreItem StoreItemObj in MyStoreItemList)
                            {
                                if (MyDataRow[2].ToString() == StoreItemObj.BatchId)
                                {
                                    MyDataRow[1] = StoreItemObj.ItemId;
                                }
                            }
                        }
                    }

                    DiscountItem[] DiscItemObjArray = new DiscountItem[MyDt1.Rows.Count];

                    for (int i = 0; i < MyDt1.Rows.Count; i++)
                    {
                        DiscItemObj = new DiscountItem();

                        Dr = MyDt1.Rows[i];

                        DiscItemObj.DiscountCode = Dr[0].ToString();
                        DiscItemObj.DiscItemId = Dr[1].ToString();
                        DiscItemObj.DiscItemBatchId = Dr[2].ToString();
                        DiscItemObj.DiscItemStartDate = Convert.ToDateTime(Dr[3].ToString());
                        DiscItemObj.DiscItemEndDate = Convert.ToDateTime(Dr[4].ToString());
                        DiscItemObj.DiscRemarks = Dr[5].ToString();
                        DiscItemObj.DiscCreateDate = DateTime.Now;

                        itemRowPos = dv.Find(DiscItemObj.DiscountCode.ToString());
                        DiscItemObj.DiscountCode = dv[itemRowPos][7].ToString();

                        DiscItemObjArray[i] = DiscItemObj;
                    }

                    List<DiscountItem> MyDiscItemList = new List<DiscountItem>();
                    MyDiscItemList = DiscItemObjArray.ToList<DiscountItem>();

                    #region CreateDiscountItem

                        MyDiscountItemFactoryClient = new DiscountItemFactoryClient();

                        foreach (DiscountItem DiscItemObj in MyDiscItemList)
                        {
                            if (MyDiscountItemFactoryClient.AddItemToDiscount(out errorMessage, DiscItemObj) == false)
                            {
                                MsgBox = new WinMessageBox(errorMessage, "Discount Item Details - Add", 0, "ERROR");
                                MsgBox.ShowDialog();
                                return;
                            }
                        }

                    #endregion

                #endregion

                MyItem = new ListBoxItem();
                MyItem.Content = "DiscountItem Data Upload Completed";
                lb_Sequence.Items.Add(MyItem);

                #region User

                    MyDt = (DataTable)Ds.Tables["Customer"];    
                
                    dv = MyDt.DefaultView;
                    dv.Sort = "RelationshipStartDate asc";
                    MyDt = dv.ToTable();

                    User[] UserObjArray = new User[MyDt.Rows.Count];


                    for (int i = 0; i < MyDt.Rows.Count; i++)
                    {
                        UserObj = new User();

                        Dr = MyDt.Rows[i];

                        UserObj.UserId = Dr[0].ToString();
                        UserObj.UserName = Dr[1].ToString();
                        UserObj.Status.UserStatusId = Convert.ToInt32(Dr[2].ToString());
                        UserObj.DateofBirth = Convert.ToDateTime(Dr[3].ToString());
                        UserObj.GenderType.GenderTypeId = Convert.ToInt32(Dr[4].ToString());
                        UserObj.PresentAddress = Dr[5].ToString();
                        UserObj.PresentCity = Dr[6].ToString();
                        UserObj.PresentPinCode = Dr[7].ToString();
                        UserObj.PresentPhoneNo = Dr[8].ToString();
                        UserObj.Mobile = Dr[9].ToString();
                        UserObj.EMailId = Dr[10].ToString();
                        if (Dr[11].ToString() == "0")
                            UserObj.IsPresentPermAddressSame = false;
                        else if (Dr[11].ToString() == "1")
                            UserObj.IsPresentPermAddressSame = true;
                        UserObj.PermanentAddress = Dr[12].ToString();
                        UserObj.PermanentCity = Dr[13].ToString();
                        UserObj.PermanentPinCode = Dr[14].ToString();
                        UserObj.PermanentPhoneNo = Dr[15].ToString();
                        UserObj.IDProofType.IDProofTypeId = Convert.ToInt32(Dr[16].ToString());
                        if ((Dr[18].ToString() != null) && (Dr[18].ToString() != ""))
                            UserObj.RelationshipStartDate = Convert.ToDateTime(Dr[18].ToString());
                        if ((Dr[19].ToString() != null) && (Dr[19].ToString() != ""))
                            UserObj.RelationshipExpiryDate = Convert.ToDateTime(Dr[19].ToString());
                        if (Dr[20] != null)
                            UserObj.AmountTobePaid = Convert.ToDouble(Dr[20].ToString());
                        if (Dr[21] != null)
                            UserObj.AmountPaidYTD = Convert.ToDouble(Dr[21].ToString());

                        UserObj.UserPassword = "password";

                        UserObj.CreateDate = DateTime.Now;
                        UserObj.LastModDate = DateTime.Now;

                        UserObjArray[i] = UserObj;
                    }

                    List<User> MyUserList = new List<User>();
                    MyUserList = UserObjArray.ToList<User>();

                    #region CreateUser

                        UserFactoryClient UserFactoryClient = new UserFactoryClient();

                        foreach (User UserObj in MyUserList)
                        {
                            if (UserFactoryClient.AddUserDetails(out errorMessage, UserObj) == false)
                            {
                                MsgBox = new WinMessageBox(errorMessage, "User Details - Add", 0, "ERROR");
                                MsgBox.ShowDialog();
                                return;
                            }
                        }

                    #endregion

                #endregion

                MyItem = new ListBoxItem();
                MyItem.Content = "User Data Upload Completed";
                lb_Sequence.Items.Add(MyItem);

                MyItem = new ListBoxItem();
                MyItem.Content = "Completed Upload Data";
                lb_Sequence.Items.Add(MyItem);


                MsgBox = new WinMessageBox("Upload Data Completed Successfully", "Upload Data", 0, "SUCCESS");
                MsgBox.ShowDialog();
                return;
            }
            catch (Exception ex1)
            {
                MsgBox = new WinMessageBox(ex1.Message, "Upload Data", 0, "ERROR");
                MsgBox.ShowDialog();
                return;
            }
        }
    }
}
