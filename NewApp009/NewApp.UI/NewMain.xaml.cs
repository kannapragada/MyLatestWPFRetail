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
using System.Windows.Shapes;
using System.Diagnostics;
using NewApp.UI;
using NewApp.UI.UserControls;
using NewApp.ExcelInterOp.UserControls;
using NewApp.Reports;
using NewApp.UI.Windows;



namespace NewApp.UI
{
    /// <summary>
    /// Interaction logic for NewMain.xaml
    /// </summary>
    /// 
    public partial class NewMain : Window
    {
        string _mode;
        string _invokingCtrlMode;
        string _invokingCtrlName;

        string _targetCtrlMode;
        string _targetCtrlName;
        
        double ScreenHeight = 0;
        double ScreenWidth = 0;

        UserCtrlArgs UCArgs;

        TabItem MyDashboardTabItemCtrl;
        TabItem MySearchMainTabItemCtrl;
        TabItem MyManufMainTabItemCtrl;
        TabItem MyItemMainTabItemCtrl;
        TabItem MyTaxMainTabItemCtrl;
        TabItem MyDiscMainTabItemCtrl;
        TabItem MySaleMainTabItemCtrl;
        TabItem MyUserMainTabItemCtrl;
        TabItem MyVendorMainTabItemCtrl;
        TabItem MyCustMainTabItemCtrl;
        TabItem MyCustOutstandingRepTabItemCtrl;
        TabItem MyExcelInterOpTabItemCtrl;
        TabItem MyComposeMailTabItemCtrl;


        DashboardCtrl MyDashboardCtrl;
        SearchMainCtrl MySearchMainCtrl;
        ManufMainCtrl MyManufMainCtrl;
        ItemMainCtrl MyItemMainCtrl;
        ItemMainCtrl MySaleItemMainCtrl;
        TaxMainCtrl MyTaxMainCtrl;
        DiscMainCtrl MyDiscMainCtrl;
        SaleMainCtrl MySaleMainCtrl;
        UserMainCtrl MyUserMainCtrl;
        VendorMainCtrl MyVendorMainCtrl;
        CustMainCtrl MyCustMainCtrl;
        CustOutstandingRepCtrl MyCustOutstandingRepCtrl;
        ExpiringItemsRepCtrl MyExpiringItemsRepCtrl;
        DailySalesRepCtrl MyDailySalesRepCtrl;
        ExcelInterOpCtrl MyExcelInterOpCtrl;
        ComposeMailCtrl MyComposeMailCtrl;


        ManufDtlInputCtrl MyManufDtlInputCtrl;
        StoreItemCtrl MyStoreItemCtrl;
        TaxDtlCtrl MyTaxDtlCtrl;
        TaxItemCtrl MyTaxItemCtrl;
        DiscDtlCtrl MyDiscDtlCtrl;
        DiscItemCtrl MyDiscItemCtrl;
        UserDtlInputCtrl MyUserDtlInputCtrl;
        VendorDtlInputCtrl MyVendorDtlInputCtrl;
        CustDtlInputCtrl MyCustDtlInputCtrl;


        public NewMain()
        {
            InitializeComponent();


            this.Width = System.Windows.SystemParameters.PrimaryScreenWidth;
            this.Height = System.Windows.SystemParameters.PrimaryScreenHeight;

            dockpanel_SalesMain.Height = System.Windows.SystemParameters.PrimaryScreenHeight - 160;

            this.Visibility = Visibility.Collapsed;

            UCArgs = new UserCtrlArgs();
            UCArgs.TargetControlName = "MISReportCtrl";
            UCArgs.TargetControlMode = "";
            UCArgs.InvokingControlName = "";
            UCArgs.InvokingControlMode = "";
            //SetVisibility(UserCtrlArgs);

            this.Visibility = Visibility.Visible;

            tab_Main.Visibility = Visibility.Collapsed;
        }

        public string Mode
        {
            get { return _mode; }
            set { _mode = value; }
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

        public string TargetControlMode
        {
            get { return _targetCtrlMode; }
            set { _targetCtrlMode = value; }
        }

        public string TargetControlName
        {
            get { return _targetCtrlName; }
            set { _targetCtrlName = value; }
        }

        private void Menu_CustMain_Click(object sender, RoutedEventArgs e)
        {
            SetCheckedProperty(Menu_CustMain);


            if (MyCustMainTabItemCtrl == null)
            {
                MyCustMainTabItemCtrl = new TabItem();
                MyCustMainTabItemCtrl.Name = "MyCustMainTabItemCtrl";
            }

            MyCustMainCtrl = new CustMainCtrl();
            MyCustMainCtrl.InvokingControlName = "NewMain";
            MyCustMainCtrl.InvokingControlMode = "M";
            MyCustMainCtrl.Name = "MyCustMainCtrl";
            MyCustMainCtrl.Mode = "M";
            MyCustMainCtrl.ControlTabName = "MyCustMainTabItemCtrl";
            MyCustMainCtrl.VerticalAlignment = VerticalAlignment.Top;

            MyCustMainTabItemCtrl.Content = MyCustMainCtrl;
            MyCustMainTabItemCtrl.VerticalAlignment = VerticalAlignment.Top;
            MyCustMainTabItemCtrl.VerticalContentAlignment = VerticalAlignment.Top;

            MyCustMainCtrl.AddHandler(CustMainCtrl.CustMainWithCustomArgsEvent, new CustMainCtrl.CustMainWithCustomArgsEventHandler(NotifyUserControl));

            var matchingItem = tab_Main.Items.Cast<TabItem>()
                    .Where(item => item.Name == "MyCustMainTabItemCtrl")
                    .FirstOrDefault();

            if (matchingItem != null)
                tab_Main.SelectedItem = matchingItem;
            else
                tab_Main.Items.Add(MyCustMainTabItemCtrl);


            tab_Main.Visibility = Visibility.Visible;
            MyCustMainTabItemCtrl.Header = "Customers";
            MyCustMainTabItemCtrl.Focus();
        }

        public void NotifyUserControl(Object sender, ProcessEventArgs e)
        {
            UCArgs.InvokingControlName = e.InvokingControlName.ToString();
            UCArgs.InvokingControlMode = e.InvokingControlMode.ToString();
            UCArgs.TargetControlName = e.TargetControlName.ToString();
            UCArgs.TargetControlMode = e.TargetControlMode.ToString();
            UCArgs.ControlTabName = e.ControlTabName.ToString();

            switch (UCArgs.TargetControlName)
            {
                case "MyCustMainCtrl":
                        switch (UCArgs.ControlTabName)
                        {
                            case "MySaleMainTabItemCtrl":
                                MySaleMainCtrl.Visibility = Visibility.Collapsed;
                                MyCustMainCtrl = new CustMainCtrl();
                                MyCustMainCtrl.InvokingControlName = UCArgs.InvokingControlName;
                                MyCustMainCtrl.InvokingControlMode = UCArgs.InvokingControlMode;
                                MyCustMainCtrl.Name = "MyCustMainCtrl";
                                MyCustMainCtrl.Mode = UCArgs.TargetControlMode;
                                MyCustMainCtrl.ControlTabName = UCArgs.ControlTabName;
                                MySaleMainTabItemCtrl.Content = MyCustMainCtrl;

                                MyCustMainCtrl.AddHandler(CustMainCtrl.CustMainWithCustomArgsEvent, new CustMainCtrl.CustMainWithCustomArgsEventHandler(NotifyUserControl));
                                MySaleMainTabItemCtrl.Focus();
                                break;

                            case "MyCustMainTabItemCtrl":
                                MyCustDtlInputCtrl = null;
                                MyCustMainCtrl.Visibility = Visibility.Visible;
                                MyCustMainCtrl.ControlTabName = UCArgs.ControlTabName;
                                MyCustMainTabItemCtrl.Content = MyCustMainCtrl;
                                MyCustMainTabItemCtrl.Header = "Customers";
                                MyCustMainTabItemCtrl.Focus();
                                break;
                        }
                        break;

                case "MyCustDtlInputCtrl":
                        MyCustMainCtrl.Visibility = Visibility.Collapsed;
                        MyCustDtlInputCtrl = new CustDtlInputCtrl();
                        MyCustDtlInputCtrl.InvokingControlName = UCArgs.InvokingControlName;
                        MyCustDtlInputCtrl.InvokingControlMode = UCArgs.InvokingControlMode;
                        MyCustDtlInputCtrl.Name = "MyCustDtlInputCtrl";
                        MyCustDtlInputCtrl.Mode = UCArgs.TargetControlMode;
                        MyCustDtlInputCtrl.ControlTabName = UCArgs.ControlTabName;
                        MyCustMainTabItemCtrl.Content = MyCustDtlInputCtrl;

                        MyCustDtlInputCtrl.AddHandler(CustDtlInputCtrl.CustDtlInputWithCustomArgsEvent, new CustDtlInputCtrl.CustDtlInputWithCustomArgsEventHandler(NotifyUserControl));
                        MyCustMainTabItemCtrl.Focus();
                        break;

                case "MyManufMainCtrl":
                        MyManufDtlInputCtrl = null;
                        MyManufMainCtrl.Visibility = Visibility.Visible;
                        MyManufMainCtrl.ControlTabName = UCArgs.ControlTabName;
                        MyManufMainTabItemCtrl.Content = MyManufMainCtrl;
                        MyManufMainTabItemCtrl.Header = "Manufacturers";
                        MyManufMainTabItemCtrl.Focus();
                        break;

                case "MyManufDtlInputCtrl":
                        MyManufMainCtrl.Visibility = Visibility.Collapsed;
                        MyManufDtlInputCtrl = new ManufDtlInputCtrl();
                        MyManufDtlInputCtrl.InvokingControlName = UCArgs.InvokingControlName;
                        MyManufDtlInputCtrl.InvokingControlMode = UCArgs.InvokingControlMode;
                        MyManufDtlInputCtrl.Name = "MyManufDtlInputCtrl";
                        MyManufDtlInputCtrl.Mode = UCArgs.TargetControlMode;
                        MyManufDtlInputCtrl.ControlTabName = UCArgs.ControlTabName;
                        MyManufMainTabItemCtrl.Content = MyManufDtlInputCtrl;

                        MyManufDtlInputCtrl.AddHandler(ManufDtlInputCtrl.ManufDtlInputWithCustomArgsEvent, new ManufDtlInputCtrl.ManufDtlInputWithCustomArgsEventHandler(NotifyUserControl));
                        MyManufMainTabItemCtrl.Focus();
                        break;

                case "MyItemMainCtrl":
                    switch (UCArgs.ControlTabName)
                        {
                            case "MySaleMainTabItemCtrl":
                                MySaleMainCtrl.Visibility = Visibility.Collapsed;
                                MyItemMainCtrl = new ItemMainCtrl();
                                MyItemMainCtrl.InvokingControlName = UCArgs.InvokingControlName;
                                MyItemMainCtrl.InvokingControlMode = UCArgs.InvokingControlMode;
                                MyItemMainCtrl.Name = "MyItemMainCtrl";
                                MyItemMainCtrl.Mode = UCArgs.TargetControlMode;
                                MyItemMainCtrl.PrevInvokingControlName = UCArgs.InvokingControlName;
                                MyItemMainCtrl.PrevInvokingControlMode = UCArgs.InvokingControlMode;
                                MyItemMainCtrl.ControlTabName = UCArgs.ControlTabName;

                                MySaleMainTabItemCtrl.Content = MyItemMainCtrl;

                                MyStoreItemCtrl.AddHandler(StoreItemCtrl.StoreItemWithCustomArgsEvent, new StoreItemCtrl.StoreItemWithCustomArgsEventHandler(NotifyUserControl));
                                MySaleMainTabItemCtrl.Focus();
                                break;

                            case "MyItemMainTabItemCtrl":
                                MyStoreItemCtrl = null;
                                MyItemMainCtrl.Visibility = Visibility.Visible;
                                MyItemMainCtrl.InvokingControlName = UCArgs.InvokingControlName;
                                MyItemMainCtrl.InvokingControlMode = UCArgs.InvokingControlMode;
                                MyItemMainCtrl.PrevInvokingControlName = UCArgs.InvokingControlName;
                                MyItemMainCtrl.PrevInvokingControlMode = UCArgs.InvokingControlMode;
                                MyItemMainCtrl.ControlTabName = UCArgs.ControlTabName;
                                MyItemMainTabItemCtrl.Content = MyItemMainCtrl;
                                MyItemMainTabItemCtrl.Header = "Items";
                                MyItemMainTabItemCtrl.Focus();
                                break;
                        }
                        break;

                case "MyStoreItemCtrl":
                    switch (UCArgs.ControlTabName)
                        {
                            case "MySaleMainTabItemCtrl":
                                MySaleMainCtrl.Visibility = Visibility.Collapsed;
                                MyStoreItemCtrl = new StoreItemCtrl();
                                MyStoreItemCtrl.InvokingControlName = UCArgs.InvokingControlName;
                                MyStoreItemCtrl.InvokingControlMode = UCArgs.InvokingControlMode;
                                MyStoreItemCtrl.Name = "MyStoreItemCtrl";
                                MyStoreItemCtrl.Mode = UCArgs.TargetControlMode;
                                MyStoreItemCtrl.ControlTabName = UCArgs.ControlTabName;
                                MySaleMainTabItemCtrl.Content = MyStoreItemCtrl;

                                MyStoreItemCtrl.AddHandler(StoreItemCtrl.StoreItemWithCustomArgsEvent, new StoreItemCtrl.StoreItemWithCustomArgsEventHandler(NotifyUserControl));
                                MySaleMainTabItemCtrl.Focus();
                                break;

                            case "MyItemMainTabItemCtrl":
                                MyItemMainCtrl.Visibility = Visibility.Collapsed;
                                MyStoreItemCtrl = new StoreItemCtrl();
                                MyStoreItemCtrl.InvokingControlName = UCArgs.InvokingControlName;
                                MyStoreItemCtrl.InvokingControlMode = UCArgs.InvokingControlMode;
                                MyStoreItemCtrl.Name = "MyStoreItemCtrl";
                                MyStoreItemCtrl.Mode = UCArgs.TargetControlMode;
                                MyStoreItemCtrl.ControlTabName = UCArgs.ControlTabName;
                                MyItemMainTabItemCtrl.Content = MyStoreItemCtrl;

                                MyStoreItemCtrl.AddHandler(StoreItemCtrl.StoreItemWithCustomArgsEvent, new StoreItemCtrl.StoreItemWithCustomArgsEventHandler(NotifyUserControl));
                                MyItemMainTabItemCtrl.Focus();
                                break;
                        }
                        break;

                case "MyDiscMainCtrl":
                        MyDiscDtlCtrl = null;
                        MyDiscMainCtrl.Visibility = Visibility.Visible;
                        MyDiscMainCtrl.ControlTabName = UCArgs.ControlTabName;
                        MyDiscMainTabItemCtrl.Content = MyDiscMainCtrl;
                        MyDiscMainTabItemCtrl.Header = "Discounts";
                        MyDiscMainTabItemCtrl.Focus();
                        break;

                case "MyDiscDtlCtrl":
                        MyDiscMainCtrl.Visibility = Visibility.Collapsed;
                        MyDiscDtlCtrl = new DiscDtlCtrl();
                        MyDiscDtlCtrl.InvokingControlName = UCArgs.InvokingControlName;
                        MyDiscDtlCtrl.InvokingControlMode = UCArgs.InvokingControlMode;
                        MyDiscDtlCtrl.Name = "MyDiscDtlCtrl";
                        MyDiscDtlCtrl.Mode = UCArgs.TargetControlMode;
                        MyDiscDtlCtrl.ControlTabName = UCArgs.ControlTabName;
                        MyDiscMainTabItemCtrl.Content = MyDiscDtlCtrl;

                        MyDiscDtlCtrl.AddHandler(DiscDtlCtrl.DiscDtlWithCustomArgsEvent, new DiscDtlCtrl.DiscDtlWithCustomArgsEventHandler(NotifyUserControl));
                        MyDiscMainTabItemCtrl.Focus();
                        break;

                case "MyDiscItemCtrl":
                        MyDiscMainCtrl.Visibility = Visibility.Collapsed;
                        MyDiscItemCtrl = new DiscItemCtrl();
                        MyDiscItemCtrl.InvokingControlName = UCArgs.InvokingControlName;
                        MyDiscItemCtrl.InvokingControlMode = UCArgs.InvokingControlMode;
                        MyDiscItemCtrl.Name = "MyDiscItemCtrl";
                        MyDiscItemCtrl.Mode = UCArgs.TargetControlMode;
                        MyDiscItemCtrl.ControlTabName = UCArgs.ControlTabName;
                        MyDiscMainTabItemCtrl.Content = MyDiscItemCtrl;

                        MyDiscDtlCtrl.AddHandler(DiscDtlCtrl.DiscDtlWithCustomArgsEvent, new DiscDtlCtrl.DiscDtlWithCustomArgsEventHandler(NotifyUserControl));
                        MyDiscMainTabItemCtrl.Focus();
                        break;

                case "MyTaxMainCtrl":
                        MyTaxDtlCtrl = null;
                        MyTaxMainCtrl.Visibility = Visibility.Visible;
                        MyTaxMainCtrl.ControlTabName = UCArgs.ControlTabName;
                        MyTaxMainTabItemCtrl.Content = MyTaxMainCtrl;
                        MyTaxMainTabItemCtrl.Header = "Taxes";
                        MyTaxMainTabItemCtrl.Focus();
                        break;

                case "MyTaxDtlCtrl":
                        MyTaxMainCtrl.Visibility = Visibility.Collapsed;
                        MyTaxDtlCtrl = new TaxDtlCtrl();
                        MyTaxDtlCtrl.InvokingControlName = UCArgs.InvokingControlName;
                        MyTaxDtlCtrl.InvokingControlMode = UCArgs.InvokingControlMode;
                        MyTaxDtlCtrl.Name = "MyTaxDtlCtrl";
                        MyTaxDtlCtrl.Mode = UCArgs.TargetControlMode;
                        MyTaxDtlCtrl.ControlTabName = UCArgs.ControlTabName;
                        MyTaxMainTabItemCtrl.Content = MyTaxDtlCtrl;

                        MyTaxDtlCtrl.AddHandler(TaxDtlCtrl.TaxDtlWithCustomArgsEvent, new TaxDtlCtrl.TaxDtlWithCustomArgsEventHandler(NotifyUserControl));
                        MyTaxMainTabItemCtrl.Focus();
                        break;

                case "MyTaxItemCtrl":
                        MyTaxMainCtrl.Visibility = Visibility.Collapsed;
                        MyTaxItemCtrl = new TaxItemCtrl();
                        MyTaxItemCtrl.InvokingControlName = UCArgs.InvokingControlName;
                        MyTaxItemCtrl.InvokingControlMode = UCArgs.InvokingControlMode;
                        MyTaxItemCtrl.Name = "MyTaxItemCtrl";
                        MyTaxItemCtrl.Mode = UCArgs.TargetControlMode;
                        MyTaxItemCtrl.ControlTabName = UCArgs.ControlTabName;
                        MyTaxMainTabItemCtrl.Content = MyTaxItemCtrl;

                        MyTaxDtlCtrl.AddHandler(TaxItemCtrl.TaxItemWithCustomArgsEvent, new TaxItemCtrl.TaxItemWithCustomArgsEventHandler(NotifyUserControl));
                        MyTaxMainTabItemCtrl.Focus();
                        break;

                case "MyUserMainCtrl":
                        MyUserDtlInputCtrl = null;
                        MyUserMainCtrl.Visibility = Visibility.Visible;
                        MyUserMainCtrl.ControlTabName = UCArgs.ControlTabName;
                        MyUserMainTabItemCtrl.Content = MyUserMainCtrl;
                        MyUserMainTabItemCtrl.Header = "Users";
                        MyUserMainTabItemCtrl.Focus();
                        break;

                case "MyUserDtlInputCtrl":
                        MyUserMainCtrl.Visibility = Visibility.Collapsed;
                        MyUserDtlInputCtrl = new UserDtlInputCtrl();
                        MyUserDtlInputCtrl.InvokingControlName = UCArgs.InvokingControlName;
                        MyUserDtlInputCtrl.InvokingControlMode = UCArgs.InvokingControlMode;
                        MyUserDtlInputCtrl.Name = "MyUserDtlInputCtrl";
                        MyUserDtlInputCtrl.Mode = UCArgs.TargetControlMode;
                        MyUserDtlInputCtrl.ControlTabName = UCArgs.ControlTabName;
                        MyUserMainTabItemCtrl.Content = MyUserDtlInputCtrl;

                        MyUserDtlInputCtrl.AddHandler(UserDtlInputCtrl.UserDtlInputWithCustomArgsEvent, new UserDtlInputCtrl.UserDtlInputWithCustomArgsEventHandler(NotifyUserControl));
                        MyUserMainTabItemCtrl.Focus();
                        break;

                case "MyVendorMainCtrl":
                        MyVendorDtlInputCtrl = null;
                        MyVendorMainCtrl.Visibility = Visibility.Visible;
                        MyVendorMainCtrl.ControlTabName = UCArgs.ControlTabName;
                        MyVendorMainTabItemCtrl.Content = MyVendorMainCtrl;
                        MyVendorMainTabItemCtrl.Header = "Vendors";
                        MyVendorMainTabItemCtrl.Focus();
                        break;

                case "MyVendorDtlInputCtrl":
                        MyVendorMainCtrl.Visibility = Visibility.Collapsed;
                        MyVendorDtlInputCtrl = new VendorDtlInputCtrl();
                        MyVendorDtlInputCtrl.InvokingControlName = UCArgs.InvokingControlName;
                        MyVendorDtlInputCtrl.InvokingControlMode = UCArgs.InvokingControlMode;
                        MyVendorDtlInputCtrl.Name = "MyVendorDtlInputCtrl";
                        MyVendorDtlInputCtrl.Mode = UCArgs.TargetControlMode;
                        MyVendorDtlInputCtrl.ControlTabName = UCArgs.ControlTabName;
                        MyVendorMainTabItemCtrl.Content = MyVendorDtlInputCtrl;

                        MyVendorDtlInputCtrl.AddHandler(VendorDtlInputCtrl.VendorDtlInputWithCustomArgsEvent, new VendorDtlInputCtrl.VendorDtlInputWithCustomArgsEventHandler(NotifyUserControl));
                        MyVendorMainTabItemCtrl.Focus();
                        break;

                case "MySaleItemMainCtrl":
                        MySaleMainCtrl.Visibility = Visibility.Collapsed;
                        MySaleItemMainCtrl = new ItemMainCtrl();
                        MySaleItemMainCtrl.InvokingControlName = UCArgs.InvokingControlName;
                        MySaleItemMainCtrl.InvokingControlMode = UCArgs.InvokingControlMode;
                        MySaleItemMainCtrl.Name = "MySaleItemMainCtrl";
                        MySaleItemMainCtrl.Mode = UCArgs.TargetControlMode;
                        MySaleItemMainCtrl.ControlTabName = UCArgs.ControlTabName;
                        MySaleMainTabItemCtrl.Content = MySaleItemMainCtrl;

                        MySaleItemMainCtrl.AddHandler(ItemMainCtrl.ItemMainWithCustomArgsEvent, new ItemMainCtrl.ItemMainWithCustomArgsEventHandler(NotifyUserControl));
                        MySaleMainTabItemCtrl.Focus();
                        break;

                case "MySaleMainCtrl":
                        MySaleItemMainCtrl = null;
                        MySaleMainCtrl.Visibility = Visibility.Visible;
                        MySaleMainCtrl.ControlTabName = UCArgs.ControlTabName;
                        MySaleMainTabItemCtrl.Content = MySaleMainCtrl;
                        MySaleMainTabItemCtrl.Header = "Sales";
                        MySaleMainTabItemCtrl.Focus();
                        break;

                case "MyCustOutstandingRepCtrl":
                        MyCustOutstandingRepCtrl.Visibility = Visibility.Visible;
                        MyCustOutstandingRepCtrl.ControlTabName = UCArgs.ControlTabName;
                        MyCustOutstandingRepTabItemCtrl.Content = MyCustMainCtrl;
                        MyCustOutstandingRepTabItemCtrl.Header = "Customer Outstanding Report";
                        MyCustOutstandingRepTabItemCtrl.Focus();
                        break;

                case "MyExpiringItemsRepCtrl":
                        MyExpiringItemsRepCtrl.Visibility = Visibility.Visible;
                        MyExpiringItemsRepCtrl.ControlTabName = UCArgs.ControlTabName;
                        break;

                case "MyDailySalesRepCtrl":
                        MyDailySalesRepCtrl.Visibility = Visibility.Visible;
                        MyDailySalesRepCtrl.ControlTabName = UCArgs.ControlTabName;
                        break;

                case "MyExcelInterOpCtrl":
                        MyExcelInterOpCtrl = null;
                        MyExcelInterOpCtrl.Visibility = Visibility.Visible;
                        MyExcelInterOpCtrl.ControlTabName = UCArgs.ControlTabName;
                        MyExcelInterOpTabItemCtrl.Content = MyExcelInterOpCtrl;
                        MyExcelInterOpTabItemCtrl.Header = "Upload Data";
                        MyExcelInterOpTabItemCtrl.Focus();
                        break;

                case "MyComposeMailCtrl":
                        MyComposeMailCtrl = null;
                        MyComposeMailCtrl.Visibility = Visibility.Visible;
                        //MyComposeMailCtrl.ControlTabName = UCArgs.ControlTabName;
                        MyComposeMailTabItemCtrl.Content = MyExcelInterOpCtrl;
                        MyComposeMailTabItemCtrl.Header = "Compose EMail";
                        MyComposeMailTabItemCtrl.Focus();
                        break;

                case "NewMain":
                        switch (UCArgs.ControlTabName)
                        {
                            case "MySaleMainTabItemCtrl":
                                MySaleMainCtrl = null;
                                tab_Main.Items.Remove(MySaleMainTabItemCtrl);
                                break;

                            case "MyCustMainTabItemCtrl":
                                MyCustMainCtrl = null;
                                tab_Main.Items.Remove(MyCustMainTabItemCtrl);
                                break;

                            case "MyManufMainTabItemCtrl":
                                MyManufMainCtrl = null;
                                tab_Main.Items.Remove(MyManufMainTabItemCtrl);
                                break;

                            case "MyItemMainTabItemCtrl":
                                MyItemMainCtrl = null;
                                tab_Main.Items.Remove(MyItemMainTabItemCtrl);
                                break;

                            case "MyDiscMainTabItemCtrl":
                                MyDiscMainCtrl = null;
                                tab_Main.Items.Remove(MyDiscMainTabItemCtrl);
                                break;

                            case "MyTaxMainTabItemCtrl":
                                MyTaxMainCtrl = null;
                                tab_Main.Items.Remove(MyTaxMainTabItemCtrl);
                                break;

                            case "MySearchMainTabItemCtrl":
                                MySearchMainCtrl = null;
                                tab_Main.Items.Remove(MySearchMainTabItemCtrl);
                                break;

                            case "MyUserMainTabItemCtrl":
                                MyUserMainCtrl = null;
                                tab_Main.Items.Remove(MyUserMainTabItemCtrl);
                                break;

                            case "MyVendorMainTabItemCtrl":
                                MyVendorMainCtrl = null;
                                tab_Main.Items.Remove(MyVendorMainTabItemCtrl);
                                break;

                            case "MyCustOutstandingRepTabItemCtrl":
                                MyCustOutstandingRepCtrl = null;
                                tab_Main.Items.Remove(MyCustOutstandingRepTabItemCtrl);
                                break;

                            case "MyExpiringItemsRepTabItemCtrl":
                                MyExpiringItemsRepCtrl = null;
                                break;

                            case "MyDailySalesRepTabItemCtrl":
                                MyDailySalesRepCtrl = null;
                                break;

                            case "MyExcelInterOpTabItemCtrl":
                                MyExcelInterOpCtrl = null;
                                tab_Main.Items.Remove(MyExcelInterOpTabItemCtrl);
                                break;

                            case "MyComposeMailTabItemCtrl":
                                MyComposeMailCtrl = null;
                                tab_Main.Items.Remove(MyComposeMailTabItemCtrl);
                                break;
                        }

                        if (tab_Main.Items.Count == 0)
                            tab_Main.Visibility = Visibility.Collapsed;

                        break;
            }

            SetHeightWidth();

            e.Handled = true;
        }

        private void Menu_ManufMain_Click(object sender, RoutedEventArgs e)
        {
            SetCheckedProperty(Menu_ManufMain);


            if (MyManufMainTabItemCtrl == null)
            {
                MyManufMainTabItemCtrl = new TabItem();
                MyManufMainTabItemCtrl.Name = "MyManufMainTabItemCtrl";
            }

            MyManufMainCtrl = new ManufMainCtrl();
            MyManufMainCtrl.InvokingControlName = "NewMain";
            MyManufMainCtrl.InvokingControlMode = "M";
            MyManufMainCtrl.Name = "MyManufMainCtrl";
            MyManufMainCtrl.Mode = "M";
            MyManufMainCtrl.ControlTabName = "MyManufMainTabItemCtrl";
            MyManufMainCtrl.VerticalAlignment = VerticalAlignment.Top;

            MyManufMainTabItemCtrl.Content = MyManufMainCtrl;
            MyManufMainTabItemCtrl.VerticalAlignment = VerticalAlignment.Top;
            MyManufMainTabItemCtrl.VerticalContentAlignment = VerticalAlignment.Top;

            MyManufMainCtrl.AddHandler(ManufMainCtrl.ManufMainWithCustomArgsEvent, new ManufMainCtrl.ManufMainWithCustomArgsEventHandler(NotifyUserControl));

            var matchingItem = tab_Main.Items.Cast<TabItem>()
                    .Where(item => item.Name == "MyManufMainTabItemCtrl")
                    .FirstOrDefault();

            if (matchingItem != null)
                tab_Main.SelectedItem = matchingItem;
            else
                tab_Main.Items.Add(MyManufMainTabItemCtrl);


            tab_Main.Visibility = Visibility.Visible;
            MyManufMainTabItemCtrl.Header = "Manufacturers";
            MyManufMainTabItemCtrl.Focus();
        }

        private void Menu_ItemMain_Click(object sender, RoutedEventArgs e)
        {
            SetCheckedProperty(Menu_ItemMain);


            if (MyItemMainTabItemCtrl == null)
            {
                MyItemMainTabItemCtrl = new TabItem();
                MyItemMainTabItemCtrl.Name = "MyItemMainTabItemCtrl";
            }

            MyItemMainCtrl = new ItemMainCtrl();
            MyItemMainCtrl.InvokingControlName = "NewMain";
            MyItemMainCtrl.InvokingControlMode = "M";
            MyItemMainCtrl.Name = "MyItemMainCtrl";
            MyItemMainCtrl.Mode = "M";
            MyItemMainCtrl.ControlTabName = "MyItemMainTabItemCtrl";
            MyItemMainCtrl.VerticalAlignment = VerticalAlignment.Top;

            MyItemMainTabItemCtrl.Content = MyItemMainCtrl;
            MyItemMainTabItemCtrl.VerticalAlignment = VerticalAlignment.Top;
            MyItemMainTabItemCtrl.VerticalContentAlignment = VerticalAlignment.Top;


            MyItemMainCtrl.AddHandler(ItemMainCtrl.ItemMainWithCustomArgsEvent, new ItemMainCtrl.ItemMainWithCustomArgsEventHandler(NotifyUserControl));

            var matchingItem = tab_Main.Items.Cast<TabItem>()
                    .Where(item => item.Name == "MyItemMainTabItemCtrl")
                    .FirstOrDefault();

            if (matchingItem != null)
                tab_Main.SelectedItem = matchingItem;
            else
                tab_Main.Items.Add(MyItemMainTabItemCtrl);


            tab_Main.Visibility = Visibility.Visible;
            MyItemMainTabItemCtrl.Header = "Items";
            MyItemMainTabItemCtrl.Focus();
        }

        private void Menu_TaxMain_Click(object sender, RoutedEventArgs e)
        {
            SetCheckedProperty(Menu_TaxMain);


            if (MyTaxMainTabItemCtrl == null)
            {
                MyTaxMainTabItemCtrl = new TabItem();
                MyTaxMainTabItemCtrl.Name = "MyTaxMainTabItemCtrl";
            }

            MyTaxMainCtrl = new TaxMainCtrl();
            MyTaxMainCtrl.InvokingControlName = "NewMain";
            MyTaxMainCtrl.InvokingControlMode = "M";
            MyTaxMainCtrl.Name = "MyTaxMainCtrl";
            MyTaxMainCtrl.Mode = "M";
            MyTaxMainCtrl.ControlTabName = "MyTaxMainTabItemCtrl";
            MyTaxMainCtrl.VerticalAlignment = VerticalAlignment.Top;

            MyTaxMainTabItemCtrl.Content = MyTaxMainCtrl;
            MyTaxMainTabItemCtrl.VerticalAlignment = VerticalAlignment.Top;
            MyTaxMainTabItemCtrl.VerticalContentAlignment = VerticalAlignment.Top;

            MyTaxMainCtrl.AddHandler(TaxMainCtrl.TaxMainWithCustomArgsEvent, new TaxMainCtrl.TaxMainWithCustomArgsEventHandler(NotifyUserControl));

            var matchingItem = tab_Main.Items.Cast<TabItem>()
                    .Where(item => item.Name == "MyTaxMainTabItemCtrl")
                    .FirstOrDefault();

            if (matchingItem != null)
                tab_Main.SelectedItem = matchingItem;
            else
                tab_Main.Items.Add(MyTaxMainTabItemCtrl);


            tab_Main.Visibility = Visibility.Visible;
            MyTaxMainTabItemCtrl.Header = "Taxes";
            MyTaxMainTabItemCtrl.Focus();
        }

        private void Menu_DiscMain_Click(object sender, RoutedEventArgs e)
        {
            SetCheckedProperty(Menu_DiscMain);


            if (MyDiscMainTabItemCtrl == null)
            {
                MyDiscMainTabItemCtrl = new TabItem();
                MyDiscMainTabItemCtrl.Name = "MyDiscMainTabItemCtrl";
            }

            MyDiscMainCtrl = new DiscMainCtrl();
            MyDiscMainCtrl.InvokingControlName = "NewMain";
            MyDiscMainCtrl.InvokingControlMode = "M";
            MyDiscMainCtrl.Name = "MyDiscMainCtrl";
            MyDiscMainCtrl.Mode = "M";
            MyDiscMainCtrl.ControlTabName = "MyDiscMainTabItemCtrl";
            MyDiscMainCtrl.VerticalAlignment = VerticalAlignment.Top;

            MyDiscMainTabItemCtrl.Content = MyDiscMainCtrl;
            MyDiscMainTabItemCtrl.VerticalAlignment = VerticalAlignment.Top;
            MyDiscMainTabItemCtrl.VerticalContentAlignment = VerticalAlignment.Top;


            MyDiscMainCtrl.AddHandler(DiscMainCtrl.DiscMainWithCustomArgsEvent, new DiscMainCtrl.DiscMainWithCustomArgsEventHandler(NotifyUserControl));

            var matchingItem = tab_Main.Items.Cast<TabItem>()
                    .Where(item => item.Name == "MyDiscMainTabItemCtrl")
                    .FirstOrDefault();

            if (matchingItem != null)
                tab_Main.SelectedItem = matchingItem;
            else
                tab_Main.Items.Add(MyDiscMainTabItemCtrl);


            tab_Main.Visibility = Visibility.Visible;
            MyDiscMainTabItemCtrl.Header = "Discounts";
            MyDiscMainTabItemCtrl.Focus();
        }

        private void Menu_SearchMain_Click(object sender, RoutedEventArgs e)
        {
            SetCheckedProperty(Menu_SearchMain);

            MySearchMainTabItemCtrl = new TabItem();
            MySearchMainCtrl = new SearchMainCtrl();
            MySearchMainCtrl.Name = "MySearchMainCtrl";
            MySearchMainCtrl.InvokingControlName = "NewMain";
            MySearchMainCtrl.InvokingControlMode = "V";
            MySearchMainCtrl.Mode = "V";
            MySearchMainCtrl.ControlTabName = "MySearchMainTabItemCtrl";
            MySearchMainCtrl.VerticalAlignment = VerticalAlignment.Top;

            MySearchMainTabItemCtrl.Content = MySearchMainCtrl;
            MySearchMainTabItemCtrl.VerticalAlignment = VerticalAlignment.Top;
            MySearchMainTabItemCtrl.VerticalContentAlignment = VerticalAlignment.Top;


            MySearchMainCtrl.AddHandler(SearchMainCtrl.SearchMainWithCustomArgsEvent, new SearchMainCtrl.SearchMainWithCustomArgsEventHandler(NotifyUserControl));

            tab_Main.Items.Add(MySearchMainTabItemCtrl);

            tab_Main.Visibility = Visibility.Visible;
            MySearchMainTabItemCtrl.Header = "Search";
            MySearchMainTabItemCtrl.Focus();
        }

        private void Menu_SaleMain_Click(object sender, RoutedEventArgs e)
        {
            SetCheckedProperty(Menu_SaleMain);

            MySaleMainTabItemCtrl = new TabItem();
            MySaleMainCtrl = new SaleMainCtrl();
            MySaleMainCtrl.Name = "MySaleMainCtrl";
            MySaleMainCtrl.InvokingControlName = "NewMain";
            MySaleMainCtrl.InvokingControlMode = "A";
            MySaleMainCtrl.Mode = "A";
            MySaleMainCtrl.ControlTabName = "MySaleMainTabItemCtrl";
            MySaleMainCtrl.VerticalAlignment = VerticalAlignment.Top;

            MySaleMainTabItemCtrl.Content = MySaleMainCtrl;
            MySaleMainTabItemCtrl.VerticalAlignment = VerticalAlignment.Top;
            MySaleMainTabItemCtrl.VerticalContentAlignment = VerticalAlignment.Top;


            MySaleMainCtrl.AddHandler(SaleMainCtrl.SaleMainWithCustomArgsEvent, new SaleMainCtrl.SaleMainWithCustomArgsEventHandler(NotifyUserControl));

            tab_Main.Items.Add(MySaleMainTabItemCtrl);

            tab_Main.Visibility = Visibility.Visible;
            MySaleMainTabItemCtrl.Header = "Sales";
            MySaleMainTabItemCtrl.Focus();
        }

        private void Menu_MailMain_Click(object sender, RoutedEventArgs e)
        {
            SetCheckedProperty(Menu_MailMain);


            if (MyComposeMailTabItemCtrl == null)
            {
                MyComposeMailTabItemCtrl = new TabItem();
                MyComposeMailTabItemCtrl.Name = "MyComposeMailTabItemCtrl";
            }

            MyComposeMailCtrl = new ComposeMailCtrl();
            MyComposeMailCtrl.InvokingControlName = "NewMain";
            MyComposeMailCtrl.InvokingControlMode = "M";
            MyComposeMailCtrl.Name = "MyComposeMailCtrl";
            MyComposeMailCtrl.Mode = "M";
            //MyComposeMailCtrl.ControlTabName = "MyComposeMailTabItemCtrl";
            MyComposeMailCtrl.VerticalAlignment = VerticalAlignment.Top;

            MyComposeMailTabItemCtrl.Content = MyComposeMailCtrl;
            MyComposeMailTabItemCtrl.VerticalAlignment = VerticalAlignment.Top;
            MyComposeMailTabItemCtrl.VerticalContentAlignment = VerticalAlignment.Top;

            MyComposeMailCtrl.AddHandler(ExcelInterOpCtrl.ExcelInterOpWithCustomArgsEvent, new ExcelInterOpCtrl.ExcelInterOpWithCustomArgsEventHandler(NotifyUserControl));

            var matchingItem = tab_Main.Items.Cast<TabItem>()
                    .Where(item => item.Name == "MyComposeMailTabItemCtrl")
                    .FirstOrDefault();

            if (matchingItem != null)
                tab_Main.SelectedItem = matchingItem;
            else
                tab_Main.Items.Add(MyComposeMailTabItemCtrl);


            tab_Main.Visibility = Visibility.Visible;
            MyComposeMailTabItemCtrl.Header = "Compose EMail";
            MyComposeMailTabItemCtrl.Focus();
        }

        private void Menu_UserMain_Click(object sender, RoutedEventArgs e)
        {
            SetCheckedProperty(Menu_UserMain);


            if (MyUserMainTabItemCtrl == null)
            {
                MyUserMainTabItemCtrl = new TabItem();
                MyUserMainTabItemCtrl.Name = "MyUserMainTabItemCtrl";
            }

            MyUserMainCtrl = new UserMainCtrl();
            MyUserMainCtrl.InvokingControlName = "NewMain";
            MyUserMainCtrl.InvokingControlMode = "M";
            MyUserMainCtrl.Name = "MyUserMainCtrl";
            MyUserMainCtrl.Mode = "M";
            MyUserMainCtrl.ControlTabName = "MyUserMainTabItemCtrl";
            MyUserMainCtrl.VerticalAlignment = VerticalAlignment.Top;

            MyUserMainTabItemCtrl.Content = MyUserMainCtrl;
            MyUserMainTabItemCtrl.VerticalAlignment = VerticalAlignment.Top;
            MyUserMainTabItemCtrl.VerticalContentAlignment = VerticalAlignment.Top;


            MyUserMainCtrl.AddHandler(UserMainCtrl.UserMainWithCustomArgsEvent, new UserMainCtrl.UserMainWithCustomArgsEventHandler(NotifyUserControl));

            var matchingItem = tab_Main.Items.Cast<TabItem>()
                    .Where(item => item.Name == "MyUserMainTabItemCtrl")
                    .FirstOrDefault();

            if (matchingItem != null)
                tab_Main.SelectedItem = matchingItem;
            else
                tab_Main.Items.Add(MyUserMainTabItemCtrl);


            tab_Main.Visibility = Visibility.Visible;
            MyUserMainTabItemCtrl.Header = "Users";
            MyUserMainTabItemCtrl.Focus();
        }

        private void Menu_VendorMain_Click(object sender, RoutedEventArgs e)
        {
            SetCheckedProperty(Menu_VendorMain);


            if (MyVendorMainTabItemCtrl == null)
            {
                MyVendorMainTabItemCtrl = new TabItem();
                MyVendorMainTabItemCtrl.Name = "MyVendorMainTabItemCtrl";
            }

            MyVendorMainCtrl = new VendorMainCtrl();
            MyVendorMainCtrl.InvokingControlName = "NewMain";
            MyVendorMainCtrl.InvokingControlMode = "M";
            MyVendorMainCtrl.Name = "MyVendorMainCtrl";
            MyVendorMainCtrl.Mode = "M";
            MyVendorMainCtrl.ControlTabName = "MyVendorMainTabItemCtrl";
            MyVendorMainCtrl.VerticalAlignment = VerticalAlignment.Top;

            MyVendorMainTabItemCtrl.Content = MyVendorMainCtrl;
            MyVendorMainTabItemCtrl.VerticalAlignment = VerticalAlignment.Top;
            MyVendorMainTabItemCtrl.VerticalContentAlignment = VerticalAlignment.Top;


            MyVendorMainCtrl.AddHandler(VendorMainCtrl.VendorMainWithCustomArgsEvent, new VendorMainCtrl.VendorMainWithCustomArgsEventHandler(NotifyUserControl));

            var matchingItem = tab_Main.Items.Cast<TabItem>()
                    .Where(item => item.Name == "MyVendorMainTabItemCtrl")
                    .FirstOrDefault();

            if (matchingItem != null)
                tab_Main.SelectedItem = matchingItem;
            else
                tab_Main.Items.Add(MyVendorMainTabItemCtrl);


            tab_Main.Visibility = Visibility.Visible;
            MyVendorMainTabItemCtrl.Header = "Vendors";
            MyVendorMainTabItemCtrl.Focus();
        }

        private void Menu_CustOutstandingDtls_Click(object sender, RoutedEventArgs e)
        {
            UCArgs.TargetControlName = "CustOutstandingRepCtrl";
            UCArgs.TargetControlMode = "V";
            UCArgs.InvokingControlName = "NewMain";
            UCArgs.InvokingControlMode = "";

            //SetVisibility(UserCtrlArgs);
            SetCheckedProperty(Menu_CustOutstandingDtls);

            if (MyCustOutstandingRepTabItemCtrl == null)
            {
                MyCustOutstandingRepTabItemCtrl = new TabItem();
                MyCustOutstandingRepTabItemCtrl.Name = "MyCustOutstandingRepTabItemCtrl";
            }

            MyCustOutstandingRepCtrl = new CustOutstandingRepCtrl();
            MyCustOutstandingRepCtrl.InvokingControlName = "NewMain";
            MyCustOutstandingRepCtrl.InvokingControlMode = "M";
            MyCustOutstandingRepCtrl.Name = "MyCustOutstandingRepCtrl";
            MyCustOutstandingRepCtrl.Mode = "M";
            MyCustOutstandingRepCtrl.ControlTabName = "MyCustOutstandingRepTabItemCtrl";
            MyCustOutstandingRepCtrl.VerticalAlignment = VerticalAlignment.Top;

            MyCustOutstandingRepTabItemCtrl.Content = MyCustOutstandingRepCtrl;
            MyCustOutstandingRepTabItemCtrl.VerticalAlignment = VerticalAlignment.Top;
            MyCustOutstandingRepTabItemCtrl.VerticalContentAlignment = VerticalAlignment.Top;

            MyCustOutstandingRepCtrl.AddHandler(CustOutstandingRepCtrl.CustOutstandingWithCustomArgsEvent, new CustOutstandingRepCtrl.CustOutstandingWithCustomArgsEventHandler(NotifyUserControl));

            var matchingItem = tab_Main.Items.Cast<TabItem>()
                    .Where(item => item.Name == "MyCustOutstandingRepTabItemCtrl")
                    .FirstOrDefault();

            if (matchingItem != null)
                tab_Main.SelectedItem = matchingItem;
            else
                tab_Main.Items.Add(MyCustOutstandingRepTabItemCtrl);


            tab_Main.Visibility = Visibility.Visible;
            MyCustOutstandingRepTabItemCtrl.Header = "Customer Outstanding Report";
            MyCustOutstandingRepTabItemCtrl.Focus();
        }

        private void Menu_DailySales_Click(object sender, RoutedEventArgs e)
        {
            UCArgs.TargetControlName = "DailySalesRepCtrl";
            UCArgs.TargetControlMode = "";
            UCArgs.InvokingControlName = "";
            UCArgs.InvokingControlMode = "";

            //SetVisibility(UserCtrlArgs);
            SetCheckedProperty(Menu_DailySales);
        }

        private void Menu_ItemsExpiring_Click(object sender, RoutedEventArgs e)
        {
            UCArgs.TargetControlName = "ExpiringItemsRepCtrl";
            UCArgs.TargetControlMode = "";
            UCArgs.InvokingControlName = "";
            UCArgs.InvokingControlMode = "";

            //SetVisibility(UserCtrlArgs);
            SetCheckedProperty(Menu_ItemsExpiring);
        }

        private void Menu_DashBoard_Click(object sender, RoutedEventArgs e)
        {
            UCArgs.TargetControlName = "DashboardCtrl";
            UCArgs.TargetControlMode = "";
            UCArgs.InvokingControlName = "";
            UCArgs.InvokingControlMode = "";

            //SetVisibility(UserCtrlArgs);
            SetCheckedProperty(Menu_DashBoard);

            SetCheckedProperty(Menu_DashBoard);

            MyDashboardTabItemCtrl = new TabItem();
            MyDashboardCtrl = new DashboardCtrl();
            MyDashboardCtrl.Name = "DashboardCtrl";
            MyDashboardCtrl.InvokingControlName = "Dashboard";
            MyDashboardCtrl.InvokingControlMode = "V";
            MyDashboardCtrl.Mode = "V";
            MyDashboardCtrl.ControlTabName = "MyDashboardTabItemCtrl";
            MyDashboardCtrl.VerticalAlignment = VerticalAlignment.Top;

            MyDashboardTabItemCtrl.Content = MyDashboardCtrl;
            MyDashboardTabItemCtrl.VerticalAlignment = VerticalAlignment.Top;
            MyDashboardTabItemCtrl.VerticalContentAlignment = VerticalAlignment.Top;


            MyDashboardCtrl.AddHandler(DashboardCtrl.DashboardWithCustomArgsEvent, new DashboardCtrl.DashboardWithCustomArgsEventHandler(NotifyUserControl));

            tab_Main.Items.Add(MyDashboardTabItemCtrl);

            tab_Main.Visibility = Visibility.Visible;
            MyDashboardTabItemCtrl.Header = "Dashboard";
            MyDashboardTabItemCtrl.Focus();
        }

        private void Expander_Expanded(object sender, RoutedEventArgs e)
        {
            //NewMainScreen.Width = 1200;
            //SetHeightWidth();
        }

        private void Expander_Collapsed(object sender, RoutedEventArgs e)
        {
            //NewMainScreen.Width = 1000;
            //SetHeightWidth();
        }

        public void SetVisibility(UserCtrlArgs UserCtrlArgs)
        {
            switch (UCArgs.TargetControlName)
            {
                case "SalesMainCtrl":
                    MySaleMainCtrl.Mode = UCArgs.TargetControlMode;
                    MySaleMainCtrl.InvokingControlName = UCArgs.InvokingControlName;
                    MySaleMainCtrl.InvokingControlMode = UCArgs.InvokingControlMode;

                    SetVisibilityToFalse();
                    Expander_MainMenu.IsExpanded = true;

                    MySaleMainTabItemCtrl.Visibility = Visibility.Visible;
                    Expander1.Visibility = Visibility.Visible;
                    break;

                case "MyCustDtlInputCtrl":

                    MyCustDtlInputCtrl = new CustDtlInputCtrl();


                    MyCustDtlInputCtrl.Mode = UCArgs.TargetControlMode;
                    MyCustDtlInputCtrl.InvokingControlName = UCArgs.InvokingControlName;
                    MyCustDtlInputCtrl.InvokingControlMode = UCArgs.InvokingControlMode;

                    SetVisibilityToFalse();
                    Expander_Maintenance.IsExpanded = true;

                    MyCustMainTabItemCtrl.Content = MyCustDtlInputCtrl;
                    tab_Main.Items.Remove(MyCustMainTabItemCtrl);
                    tab_Main.Items.Add(MyCustMainTabItemCtrl);
                    MyCustMainTabItemCtrl.Focus();
                    Expander1.Visibility = Visibility.Visible;
                    break;

                case "CustMainCtrl":
                    MyCustMainCtrl.Mode = UCArgs.TargetControlMode;
                    MyCustMainCtrl.InvokingControlName = UCArgs.InvokingControlName;
                    MyCustMainCtrl.InvokingControlMode = UCArgs.InvokingControlMode;

                    SetVisibilityToFalse();
                    Expander_Maintenance.IsExpanded = true;

                    MyCustMainCtrl = new CustMainCtrl();
                    MyCustMainTabItemCtrl.Content = MyCustMainCtrl;
                    tab_Main.Items.Remove(MyCustMainTabItemCtrl);
                    tab_Main.Items.Add(MyCustMainTabItemCtrl);
                    MyCustMainTabItemCtrl.Focus();
                    Expander1.Visibility = Visibility.Visible;
                    break;

                case "ExcelInterOpCtrl":
                    MyExcelInterOpCtrl.Mode = UCArgs.TargetControlMode;
                    MyExcelInterOpCtrl.InvokingControlName = UCArgs.InvokingControlName;
                    MyExcelInterOpCtrl.InvokingControlMode = UCArgs.InvokingControlMode;

                    SetVisibilityToFalse();
                    Expander_Tools.IsExpanded = true;

                    MyExcelInterOpCtrl = new ExcelInterOpCtrl();
                    MyExcelInterOpTabItemCtrl.Content = MyExcelInterOpCtrl;
                    tab_Main.Items.Remove(MyExcelInterOpTabItemCtrl);
                    tab_Main.Items.Add(MyExcelInterOpTabItemCtrl);
                    MyExcelInterOpTabItemCtrl.Focus();
                    Expander1.Visibility = Visibility.Visible;
                    break;

                case "ManufDtlInputCtrl":
                    //ManufDtlInputCtrl.Mode = UCArgs.TargetControlMode;
                    //ManufDtlInputCtrl.InvokingControlName = UCArgs.InvokingControlName;
                    //ManufDtlInputCtrl.InvokingControlMode = UCArgs.InvokingControlMode;
                    //if (ManufDtlInputCtrl.Mode == "A")
                    //    tb_SideHeader.Text = "Manufacturer Details Input";
                    //else if (ManufDtlInputCtrl.Mode == "U")
                    //    tb_SideHeader.Text = "Manufacturer Details Modify";
                    //else if (ManufDtlInputCtrl.Mode == "V")
                    //    tb_SideHeader.Text = "Manufacturer Details View";

                    //SetVisibilityToFalse();
                    //Expander_Maintenance.IsExpanded = true;

                    //ManufDtlInputCtrl.Visibility = Visibility.Visible;
                    //Expander1.Visibility = Visibility.Visible;
                    //rect_Heading.Visibility = Visibility.Visible;
                    break;

                case "ManufMainCtrl":
                    //ManufMainCtrl.Mode = UCArgs.TargetControlMode;
                    //ManufMainCtrl.InvokingControlName = UCArgs.InvokingControlName;
                    //ManufMainCtrl.InvokingControlMode = UCArgs.InvokingControlMode;

                    //tb_SideHeader.Text = "Manufacturer Main";

                    //SetVisibilityToFalse();
                    //Expander_Maintenance.IsExpanded = true;

                    //ManufMainCtrl.Visibility = Visibility.Visible;
                    //Expander1.Visibility = Visibility.Visible;
                    //rect_Heading.Visibility = Visibility.Visible;
                    break;

                case "ItemMainCtrl":
                    //ItemMainCtrl.Mode = UCArgs.TargetControlMode;
                    //ItemMainCtrl.InvokingControlName = UCArgs.InvokingControlName;
                    //ItemMainCtrl.InvokingControlMode = UCArgs.InvokingControlMode;

                    //tb_SideHeader.Text = "Items Main";

                    //SetVisibilityToFalse();
                    //Expander_Maintenance.IsExpanded = true;

                    //ItemMainCtrl.Visibility = Visibility.Visible;
                    //Expander1.Visibility = Visibility.Visible;
                    //rect_Heading.Visibility = Visibility.Visible;
                    break;

                case "VendorDtlInputCtrl":
                    //VendorDtlInputCtrl.Mode = UCArgs.TargetControlMode;
                    //VendorDtlInputCtrl.InvokingControlName = UCArgs.InvokingControlName;
                    //VendorDtlInputCtrl.InvokingControlMode = UCArgs.InvokingControlMode;
                    //if (VendorDtlInputCtrl.Mode == "A")
                    //    tb_SideHeader.Text = "Vendor Details Input";
                    //else if (VendorDtlInputCtrl.Mode == "U")
                    //    tb_SideHeader.Text = "Vendor Details Modify";
                    //else if (VendorDtlInputCtrl.Mode == "V")
                    //    tb_SideHeader.Text = "Vendor Details View";

                    //SetVisibilityToFalse();
                    //Expander_Maintenance.IsExpanded = true;

                    //VendorDtlInputCtrl.Visibility = Visibility.Visible;
                    //Expander1.Visibility = Visibility.Visible;
                    //rect_Heading.Visibility = Visibility.Visible;
                    break;

                case "VendorMainCtrl":
                    //VendorMainCtrl.Mode = UCArgs.TargetControlMode;
                    //VendorMainCtrl.InvokingControlName = UCArgs.InvokingControlName;
                    //VendorMainCtrl.InvokingControlMode = UCArgs.InvokingControlMode;

                    //tb_SideHeader.Text = "Vendor Main";

                    //SetVisibilityToFalse();
                    //Expander_Maintenance.IsExpanded = true;

                    //VendorMainCtrl.Visibility = Visibility.Visible;
                    //Expander1.Visibility = Visibility.Visible;
                    //rect_Heading.Visibility = Visibility.Visible;
                    break;

                case "DiscMainCtrl":
                    //DiscMainCtrl.Mode = UCArgs.TargetControlMode;
                    //DiscMainCtrl.InvokingControlName = UCArgs.InvokingControlName;
                    //DiscMainCtrl.InvokingControlMode = UCArgs.InvokingControlMode;

                    //tb_SideHeader.Text = "Discount Main";

                    //SetVisibilityToFalse();
                    //Expander_Maintenance.IsExpanded = true;

                    //DiscMainCtrl.Visibility = Visibility.Visible;
                    //Expander1.Visibility = Visibility.Visible;
                    //rect_Heading.Visibility = Visibility.Visible;
                    break;

                case "DiscDtlCtrl":
                    //DiscDtlCtrl.Mode = UCArgs.TargetControlMode;
                    //DiscDtlCtrl.InvokingControlName = UCArgs.InvokingControlName;
                    //DiscDtlCtrl.InvokingControlMode = UCArgs.InvokingControlMode;
                    //if (DiscDtlCtrl.Mode == "A")
                    //    tb_SideHeader.Text = "Discount Details Input";
                    //else if (DiscDtlCtrl.Mode == "U")
                    //    tb_SideHeader.Text = "Discount Details Modify";
                    //else if (DiscDtlCtrl.Mode == "V")
                    //    tb_SideHeader.Text = "Discount Details View";

                    //SetVisibilityToFalse();
                    //Expander_Maintenance.IsExpanded = true;

                    //DiscDtlCtrl.Visibility = Visibility.Visible;
                    //Expander1.Visibility = Visibility.Visible;
                    //rect_Heading.Visibility = Visibility.Visible;
                    break;

                case "DiscItemCtrl":
                    //DiscItemCtrl.Mode = UCArgs.TargetControlMode;
                    //DiscItemCtrl.InvokingControlName = UCArgs.InvokingControlName;
                    //DiscItemCtrl.InvokingControlMode = UCArgs.InvokingControlMode;
                    //if (DiscItemCtrl.Mode == "A")
                    //    tb_SideHeader.Text = "Add Items to Discount";
                    //else if (DiscItemCtrl.Mode == "U")
                    //    tb_SideHeader.Text = "Items to Discount Modify";
                    //else if (DiscItemCtrl.Mode == "V")
                    //    tb_SideHeader.Text = "Items to Discount View";

                    //SetVisibilityToFalse();
                    //Expander_Maintenance.IsExpanded = true;

                    //DiscItemCtrl.Visibility = Visibility.Visible;
                    //Expander1.Visibility = Visibility.Visible;
                    //rect_Heading.Visibility = Visibility.Visible;
                    break;

                //case "ItemMainCtrl":
                //    //ItemMainCtrl.Mode = UCArgs.TargetControlMode;
                //    //ItemMainCtrl.InvokingControlName = UCArgs.InvokingControlName;
                //    //ItemMainCtrl.InvokingControlMode = UCArgs.InvokingControlMode;
                //    //tb_SideHeader.Text = "Items List";

                //    //SetVisibilityToFalse();
                //    //Expander_Maintenance.IsExpanded = true;

                //    //ItemMainCtrl.Visibility = Visibility.Visible;
                //    //Expander1.Visibility = Visibility.Visible;
                //    //rect_Heading.Visibility = Visibility.Visible;
                //    break;

                case "SearchMainCtrl":
                    //SearchMainCtrl.Mode = UCArgs.TargetControlMode;
                    //SearchMainCtrl.InvokingControlName = UCArgs.InvokingControlName;
                    //SearchMainCtrl.InvokingControlMode = UCArgs.InvokingControlMode;
                    //tb_SideHeader.Text = "Search Main";

                    //SetVisibilityToFalse();
                    //Expander_MainMenu.IsExpanded = true;

                    //SearchMainCtrl.Visibility = Visibility.Visible;
                    //Expander1.Visibility = Visibility.Visible;
                    //rect_Heading.Visibility = Visibility.Visible;
                    break;

                case "StoreItemCtrl":
                    //StoreItemCtrl.Mode = UCArgs.TargetControlMode;
                    //StoreItemCtrl.InvokingControlName = UCArgs.InvokingControlName;
                    //StoreItemCtrl.InvokingControlMode = UCArgs.InvokingControlMode;
                    //if (StoreItemCtrl.Mode == "A")
                    //    tb_SideHeader.Text = "Store Item Details Input";
                    //else if (StoreItemCtrl.Mode == "U")
                    //    tb_SideHeader.Text = "Store Item Details Modify";
                    //else if (StoreItemCtrl.Mode == "V")
                    //    tb_SideHeader.Text = "Store Item Details View";

                    //SetVisibilityToFalse();
                    //Expander_Maintenance.IsExpanded = true;

                    //StoreItemCtrl.Visibility = Visibility.Visible;
                    //Expander1.Visibility = Visibility.Visible;
                    //rect_Heading.Visibility = Visibility.Visible;
                    break;

                case "TaxMainCtrl":
                    //TaxMainCtrl.Mode = UCArgs.TargetControlMode;
                    //TaxMainCtrl.InvokingControlName = UCArgs.InvokingControlName;
                    //TaxMainCtrl.InvokingControlMode = UCArgs.InvokingControlMode;
                    //tb_SideHeader.Text = "Tax Main";

                    //SetVisibilityToFalse();
                    //Expander_Maintenance.IsExpanded = true;

                    //TaxMainCtrl.Visibility = Visibility.Visible;
                    //Expander1.Visibility = Visibility.Visible;
                    //rect_Heading.Visibility = Visibility.Visible;
                    break;

                case "TaxDtlCtrl":
                    //TaxDtlCtrl.Mode = UCArgs.TargetControlMode;
                    //TaxDtlCtrl.InvokingControlName = UCArgs.InvokingControlName;
                    //TaxDtlCtrl.InvokingControlMode = UCArgs.InvokingControlMode;
                    //if (TaxDtlCtrl.Mode == "A")
                    //    tb_SideHeader.Text = "Tax Details Input";
                    //else if (TaxDtlCtrl.Mode == "U")
                    //    tb_SideHeader.Text = "Tax Details Modify";
                    //else if (TaxDtlCtrl.Mode == "V")
                    //    tb_SideHeader.Text = "Tax Details View";

                    //SetVisibilityToFalse();
                    //Expander_Maintenance.IsExpanded = true;

                    //TaxDtlCtrl.Visibility = Visibility.Visible;
                    //Expander1.Visibility = Visibility.Visible;
                    //rect_Heading.Visibility = Visibility.Visible;
                    break;

                case "TaxItemCtrl":
                    //TaxItemCtrl.Mode = UCArgs.TargetControlMode;
                    //TaxItemCtrl.InvokingControlName = UCArgs.InvokingControlName;
                    //TaxItemCtrl.InvokingControlMode = UCArgs.InvokingControlMode;
                    //if (TaxItemCtrl.Mode == "A")
                    //    tb_SideHeader.Text = "Add Items to Tax Code";
                    //else if (TaxItemCtrl.Mode == "U")
                    //    tb_SideHeader.Text = "Items Linked to Tax Code - Modify";
                    //else if (TaxItemCtrl.Mode == "V")
                    //    tb_SideHeader.Text = "Items Linked to Tax Code - View";

                    //SetVisibilityToFalse();
                    //Expander_Maintenance.IsExpanded = true;

                    //TaxItemCtrl.Visibility = Visibility.Visible;
                    //Expander1.Visibility = Visibility.Visible;
                    //rect_Heading.Visibility = Visibility.Visible;
                    break;

                case "ComposeMailCtrl":
                    MyComposeMailCtrl.Mode = UCArgs.TargetControlMode;
                    MyComposeMailCtrl.InvokingControlName = UCArgs.InvokingControlName;
                    MyComposeMailCtrl.InvokingControlMode = UCArgs.InvokingControlMode;

                    SetVisibilityToFalse();
                    Expander_Tools.IsExpanded = true;

                    MyComposeMailCtrl = new ComposeMailCtrl();
                    MyComposeMailTabItemCtrl.Content = MyComposeMailCtrl;
                    tab_Main.Items.Remove(MyComposeMailTabItemCtrl);
                    tab_Main.Items.Add(MyComposeMailTabItemCtrl);
                    MyComposeMailTabItemCtrl.Focus();
                    Expander1.Visibility = Visibility.Visible;
                    break;

                case "BillRepCtrl":
                    //tb_SideHeader.Text = "Bill Details";
                    //BillRepCtrl.Mode = UCArgs.TargetControlMode;
                    //BillRepCtrl.InvokingControlName = UCArgs.InvokingControlName;
                    //BillRepCtrl.InvokingControlMode = UCArgs.InvokingControlMode;

                    //SetVisibilityToFalse();
                    //Expander_Reports.IsExpanded = true;

                    //BillRepCtrl.Visibility = Visibility.Visible;
                    //Expander1.Visibility = Visibility.Visible;
                    //rect_Heading.Visibility = Visibility.Visible;
                    break;

                case "CustOutstandingRepCtrl":
                    //tb_SideHeader.Text = "Customer Outstanding Report";
                    //CustOutstandingRepCtrl.Mode = UCArgs.TargetControlMode;
                    //CustOutstandingRepCtrl.InvokingControlName = UCArgs.InvokingControlName;
                    //CustOutstandingRepCtrl.InvokingControlMode = UCArgs.InvokingControlMode;

                    //SetVisibilityToFalse();
                    //Expander_Reports.IsExpanded = true;

                    //CustOutstandingRepCtrl.Visibility = Visibility.Visible;
                    //Expander1.Visibility = Visibility.Visible;
                    //rect_Heading.Visibility = Visibility.Visible;
                    break;

                case "DailySalesRepCtrl":
                    //tb_SideHeader.Text = "Daily Sales Report";
                    //DailySalesRepCtrl.Mode = UCArgs.TargetControlMode;
                    //DailySalesRepCtrl.InvokingControlName = UCArgs.InvokingControlName;
                    //DailySalesRepCtrl.InvokingControlMode = UCArgs.InvokingControlMode;

                    //SetVisibilityToFalse();
                    //Expander_Reports.IsExpanded = true;

                    //DailySalesRepCtrl.Visibility = Visibility.Visible;
                    //Expander1.Visibility = Visibility.Visible;
                    //rect_Heading.Visibility = Visibility.Visible;
                    break;

                //case "ExpiringItemsRepCtrl":
                //        tb_SideHeader.Text = "Expiring Items Report";
                //        ExpiringItemsRepCtrl.Mode = UCArgs.TargetControlMode;
                //        ExpiringItemsRepCtrl.InvokingControlName = UCArgs.InvokingControlName;
                //        ExpiringItemsRepCtrl.InvokingControlMode = UCArgs.InvokingControlMode;

                //        SetVisibilityToFalse();
                //        Expander_Reports.IsExpanded = true;

                //        ExpiringItemsRepCtrl.Visibility = Visibility.Visible;
                //        Expander1.Visibility = Visibility.Visible;
                //        rect_Heading.Visibility = Visibility.Visible;
                //        break;

                case "MISReportCtrl":
                    //tb_SideHeader.Text = "Dash Board";
                    //MISReportCtrl.Mode = UCArgs.TargetControlMode;
                    //MISReportCtrl.InvokingControlName = UCArgs.InvokingControlName;
                    //MISReportCtrl.InvokingControlMode = UCArgs.InvokingControlMode;

                    //SetVisibilityToFalse();

                    //Expander_MainMenu.IsExpanded = true;
                    //Expander1.IsExpanded = true;
                    //MISReportCtrl.Visibility = Visibility.Visible;
                    //Expander1.Visibility = Visibility.Visible;
                    //rect_Heading.Visibility = Visibility.Visible;
                    break;

                case "UserDtlInputCtrl":
                    //UserDtlInputCtrl.Mode = UCArgs.TargetControlMode;
                    //UserDtlInputCtrl.InvokingControlName = UCArgs.InvokingControlName;
                    //UserDtlInputCtrl.InvokingControlMode = UCArgs.InvokingControlMode;
                    //if (UserDtlInputCtrl.Mode == "A")
                    //    tb_SideHeader.Text = "User Details Input";
                    //else if (UserDtlInputCtrl.Mode == "U")
                    //    tb_SideHeader.Text = "User Details Modify";
                    //else if (UserDtlInputCtrl.Mode == "V")
                    //    tb_SideHeader.Text = "User Details View";

                    //SetVisibilityToFalse();
                    //Expander_Maintenance.IsExpanded = true;

                    //UserDtlInputCtrl.Visibility = Visibility.Visible;
                    //Expander1.Visibility = Visibility.Visible;
                    //rect_Heading.Visibility = Visibility.Visible;
                    break;

                case "UserMainCtrl":
                    //UserMainCtrl.Mode = UCArgs.TargetControlMode;
                    //UserMainCtrl.InvokingControlName = UCArgs.InvokingControlName;
                    //UserMainCtrl.InvokingControlMode = UCArgs.InvokingControlMode;

                    //tb_SideHeader.Text = "Users Main";

                    //SetVisibilityToFalse();
                    //Expander_Maintenance.IsExpanded = true;

                    //UserMainCtrl.Visibility = Visibility.Visible;
                    //Expander1.Visibility = Visibility.Visible;
                    //rect_Heading.Visibility = Visibility.Visible;
                    break;

                default:
                    SetVisibilityToFalse();
                    Expander_MainMenu.IsExpanded = true;

                    //MISReportCtrl.Visibility = Visibility.Visible;
                    Expander1.Visibility = Visibility.Visible;
                    SetCheckedProperty();
                    break;
            }

            SetHeightWidth();
        }

        public void SetVisibilityToFalse()
        {
            //SalesMainCtrl.Visibility = Visibility.Collapsed;
            //CustDtlInputCtrl.Visibility = Visibility.Collapsed;
            //CustMainCtrl.Visibility = Visibility.Collapsed;
            //ManufDtlInputCtrl.Visibility = Visibility.Collapsed;
            //ManufMainCtrl.Visibility = Visibility.Collapsed;
            //VendorDtlInputCtrl.Visibility = Visibility.Collapsed;
            //VendorMainCtrl.Visibility = Visibility.Collapsed;
            //DiscMainCtrl.Visibility = Visibility.Collapsed;
            //DiscDtlCtrl.Visibility = Visibility.Collapsed;
            //DiscItemCtrl.Visibility = Visibility.Collapsed;
            //ItemMainCtrl.Visibility = Visibility.Collapsed;
            //ItemMainCtrl.Visibility = Visibility.Collapsed;
            //SearchMainCtrl.Visibility = Visibility.Collapsed;
            //StoreItemCtrl.Visibility = Visibility.Collapsed;
            //TaxMainCtrl.Visibility = Visibility.Collapsed;
            //TaxDtlCtrl.Visibility = Visibility.Collapsed;
            //TaxItemCtrl.Visibility = Visibility.Collapsed;
            //ComposeMailCtrl.Visibility = Visibility.Collapsed;
            //BillRepCtrl.Visibility = Visibility.Collapsed;
            //MISReportCtrl.Visibility = Visibility.Collapsed;
            //CustOutstandingRepCtrl.Visibility = Visibility.Collapsed;
            //DailySalesRepCtrl.Visibility = Visibility.Collapsed;
            ////ExpiringItemsRepCtrl.Visibility = Visibility.Collapsed;
            //UserDtlInputCtrl.Visibility = Visibility.Collapsed;
            //UserDtlViewCtrl.Visibility = Visibility.Collapsed;
            //UserMainCtrl.Visibility = Visibility.Collapsed;

            Expander1.Visibility = Visibility.Collapsed;
            //CalcCtrl.Visibility = Visibility.Visible;


            Expander_MainMenu.IsExpanded = false;
            Expander_Reports.IsExpanded = false;
            Expander_Maintenance.IsExpanded = false;
        }

        //public void SetVisibility(UserCtrlArgs UserCtrlArgs)
        //{
        //    switch (UCArgs.TargetControlName)
        //    {
        //        case "SalesMainCtrl":
        //            //SalesMainCtrl.Mode = UCArgs.TargetControlMode;
        //            //SalesMainCtrl.InvokingControlName = UCArgs.InvokingControlName;
        //            //SalesMainCtrl.InvokingControlMode = UCArgs.InvokingControlMode;
        //            //tb_SideHeader.Text = "Sales Main";

        //            //SetVisibilityToFalse();
        //            //Expander_MainMenu.IsExpanded = true;

        //            //SalesMainCtrl.Visibility = Visibility.Visible;
        //            //Expander1.Visibility = Visibility.Visible;
        //            //rect_Heading.Visibility = Visibility.Visible;

        //            //tab_Main.Items.Remove();

        //            break;

        //        case "CustDtlInputCtrl":
        //            //CustDtlInputCtrl.Mode = UCArgs.TargetControlMode;
        //            //CustDtlInputCtrl.InvokingControlName = UCArgs.InvokingControlName;
        //            //CustDtlInputCtrl.InvokingControlMode = UCArgs.InvokingControlMode;
        //            //if (CustDtlInputCtrl.Mode == "A")
        //            //    tb_SideHeader.Text = "Customer Details Input";
        //            //else if (CustDtlInputCtrl.Mode == "U")
        //            //    tb_SideHeader.Text = "Customer Details Modify";
        //            //else if (CustDtlInputCtrl.Mode == "V")
        //            //    tb_SideHeader.Text = "Customer Details View";

        //            //SetVisibilityToFalse();
        //            //Expander_Maintenance.IsExpanded = true;

        //            //CustDtlInputCtrl.Visibility = Visibility.Visible;
        //            //Expander1.Visibility = Visibility.Visible;
        //            //rect_Heading.Visibility = Visibility.Visible;
        //            break;

        //        case "CustMainCtrl":
        //            //CustMainCtrl.Mode = UCArgs.TargetControlMode;
        //            //CustMainCtrl.InvokingControlName = UCArgs.InvokingControlName;
        //            //CustMainCtrl.InvokingControlMode = UCArgs.InvokingControlMode;

        //            //tb_SideHeader.Text = "Customers Main";

        //            //SetVisibilityToFalse();
        //            //Expander_Maintenance.IsExpanded = true;

        //            //CustMainCtrl.Visibility = Visibility.Visible;
        //            //Expander1.Visibility = Visibility.Visible;
        //            //rect_Heading.Visibility = Visibility.Visible;
        //            break;

        //        case "ManufDtlInputCtrl":
        //            //ManufDtlInputCtrl.Mode = UCArgs.TargetControlMode;
        //            //ManufDtlInputCtrl.InvokingControlName = UCArgs.InvokingControlName;
        //            //ManufDtlInputCtrl.InvokingControlMode = UCArgs.InvokingControlMode;
        //            //if (ManufDtlInputCtrl.Mode == "A")
        //            //    tb_SideHeader.Text = "Manufacturer Details Input";
        //            //else if (ManufDtlInputCtrl.Mode == "U")
        //            //    tb_SideHeader.Text = "Manufacturer Details Modify";
        //            //else if (ManufDtlInputCtrl.Mode == "V")
        //            //    tb_SideHeader.Text = "Manufacturer Details View";

        //            //SetVisibilityToFalse();
        //            //Expander_Maintenance.IsExpanded = true;

        //            //ManufDtlInputCtrl.Visibility = Visibility.Visible;
        //            //Expander1.Visibility = Visibility.Visible;
        //            //rect_Heading.Visibility = Visibility.Visible;
        //            break;

        //        case "ManufMainCtrl":
        //            //ManufMainCtrl.Mode = UCArgs.TargetControlMode;
        //            //ManufMainCtrl.InvokingControlName = UCArgs.InvokingControlName;
        //            //ManufMainCtrl.InvokingControlMode = UCArgs.InvokingControlMode;

        //            //tb_SideHeader.Text = "Manufacturer Main";

        //            //SetVisibilityToFalse();
        //            //Expander_Maintenance.IsExpanded = true;

        //            //ManufMainCtrl.Visibility = Visibility.Visible;
        //            //Expander1.Visibility = Visibility.Visible;
        //            //rect_Heading.Visibility = Visibility.Visible;
        //            break;

        //        case "ItemMainCtrl":
        //            //ItemMainCtrl.Mode = UCArgs.TargetControlMode;
        //            //ItemMainCtrl.InvokingControlName = UCArgs.InvokingControlName;
        //            //ItemMainCtrl.InvokingControlMode = UCArgs.InvokingControlMode;

        //            //tb_SideHeader.Text = "Items Main";

        //            //SetVisibilityToFalse();
        //            //Expander_Maintenance.IsExpanded = true;

        //            //ItemMainCtrl.Visibility = Visibility.Visible;
        //            //Expander1.Visibility = Visibility.Visible;
        //            //rect_Heading.Visibility = Visibility.Visible;
        //            break;

        //        case "VendorDtlInputCtrl":
        //            //VendorDtlInputCtrl.Mode = UCArgs.TargetControlMode;
        //            //VendorDtlInputCtrl.InvokingControlName = UCArgs.InvokingControlName;
        //            //VendorDtlInputCtrl.InvokingControlMode = UCArgs.InvokingControlMode;
        //            //if (VendorDtlInputCtrl.Mode == "A")
        //            //    tb_SideHeader.Text = "Vendor Details Input";
        //            //else if (VendorDtlInputCtrl.Mode == "U")
        //            //    tb_SideHeader.Text = "Vendor Details Modify";
        //            //else if (VendorDtlInputCtrl.Mode == "V")
        //            //    tb_SideHeader.Text = "Vendor Details View";

        //            //SetVisibilityToFalse();
        //            //Expander_Maintenance.IsExpanded = true;

        //            //VendorDtlInputCtrl.Visibility = Visibility.Visible;
        //            //Expander1.Visibility = Visibility.Visible;
        //            //rect_Heading.Visibility = Visibility.Visible;
        //            break;

        //        case "VendorMainCtrl":
        //            //VendorMainCtrl.Mode = UCArgs.TargetControlMode;
        //            //VendorMainCtrl.InvokingControlName = UCArgs.InvokingControlName;
        //            //VendorMainCtrl.InvokingControlMode = UCArgs.InvokingControlMode;

        //            //tb_SideHeader.Text = "Vendor Main";

        //            //SetVisibilityToFalse();
        //            //Expander_Maintenance.IsExpanded = true;

        //            //VendorMainCtrl.Visibility = Visibility.Visible;
        //            //Expander1.Visibility = Visibility.Visible;
        //            //rect_Heading.Visibility = Visibility.Visible;
        //            break;

        //        case "DiscMainCtrl":
        //            //DiscMainCtrl.Mode = UCArgs.TargetControlMode;
        //            //DiscMainCtrl.InvokingControlName = UCArgs.InvokingControlName;
        //            //DiscMainCtrl.InvokingControlMode = UCArgs.InvokingControlMode;

        //            //tb_SideHeader.Text = "Discount Main";

        //            //SetVisibilityToFalse();
        //            //Expander_Maintenance.IsExpanded = true;

        //            //DiscMainCtrl.Visibility = Visibility.Visible;
        //            //Expander1.Visibility = Visibility.Visible;
        //            //rect_Heading.Visibility = Visibility.Visible;
        //            break;

        //        case "DiscDtlCtrl":
        //            //DiscDtlCtrl.Mode = UCArgs.TargetControlMode;
        //            //DiscDtlCtrl.InvokingControlName = UCArgs.InvokingControlName;
        //            //DiscDtlCtrl.InvokingControlMode = UCArgs.InvokingControlMode;
        //            //if (DiscDtlCtrl.Mode == "A")
        //            //    tb_SideHeader.Text = "Discount Details Input";
        //            //else if (DiscDtlCtrl.Mode == "U")
        //            //    tb_SideHeader.Text = "Discount Details Modify";
        //            //else if (DiscDtlCtrl.Mode == "V")
        //            //    tb_SideHeader.Text = "Discount Details View";

        //            //SetVisibilityToFalse();
        //            //Expander_Maintenance.IsExpanded = true;

        //            //DiscDtlCtrl.Visibility = Visibility.Visible;
        //            //Expander1.Visibility = Visibility.Visible;
        //            //rect_Heading.Visibility = Visibility.Visible;
        //            break;

        //        case "DiscItemCtrl":
        //            //DiscItemCtrl.Mode = UCArgs.TargetControlMode;
        //            //DiscItemCtrl.InvokingControlName = UCArgs.InvokingControlName;
        //            //DiscItemCtrl.InvokingControlMode = UCArgs.InvokingControlMode;
        //            //if (DiscItemCtrl.Mode == "A")
        //            //    tb_SideHeader.Text = "Add Items to Discount";
        //            //else if (DiscItemCtrl.Mode == "U")
        //            //    tb_SideHeader.Text = "Items to Discount Modify";
        //            //else if (DiscItemCtrl.Mode == "V")
        //            //    tb_SideHeader.Text = "Items to Discount View";

        //            //SetVisibilityToFalse();
        //            //Expander_Maintenance.IsExpanded = true;

        //            //DiscItemCtrl.Visibility = Visibility.Visible;
        //            //Expander1.Visibility = Visibility.Visible;
        //            //rect_Heading.Visibility = Visibility.Visible;
        //            break;

        //        //case "ItemMainCtrl":
        //        //    //ItemMainCtrl.Mode = UCArgs.TargetControlMode;
        //        //    //ItemMainCtrl.InvokingControlName = UCArgs.InvokingControlName;
        //        //    //ItemMainCtrl.InvokingControlMode = UCArgs.InvokingControlMode;
        //        //    //tb_SideHeader.Text = "Items List";

        //        //    //SetVisibilityToFalse();
        //        //    //Expander_Maintenance.IsExpanded = true;

        //        //    //ItemMainCtrl.Visibility = Visibility.Visible;
        //        //    //Expander1.Visibility = Visibility.Visible;
        //        //    //rect_Heading.Visibility = Visibility.Visible;
        //        //    break;

        //        case "SearchMainCtrl":
        //            //SearchMainCtrl.Mode = UCArgs.TargetControlMode;
        //            //SearchMainCtrl.InvokingControlName = UCArgs.InvokingControlName;
        //            //SearchMainCtrl.InvokingControlMode = UCArgs.InvokingControlMode;
        //            //tb_SideHeader.Text = "Search Main";

        //            //SetVisibilityToFalse();
        //            //Expander_MainMenu.IsExpanded = true;

        //            //SearchMainCtrl.Visibility = Visibility.Visible;
        //            //Expander1.Visibility = Visibility.Visible;
        //            //rect_Heading.Visibility = Visibility.Visible;
        //            break;

        //        case "StoreItemCtrl":
        //            //StoreItemCtrl.Mode = UCArgs.TargetControlMode;
        //            //StoreItemCtrl.InvokingControlName = UCArgs.InvokingControlName;
        //            //StoreItemCtrl.InvokingControlMode = UCArgs.InvokingControlMode;
        //            //if (StoreItemCtrl.Mode == "A")
        //            //    tb_SideHeader.Text = "Store Item Details Input";
        //            //else if (StoreItemCtrl.Mode == "U")
        //            //    tb_SideHeader.Text = "Store Item Details Modify";
        //            //else if (StoreItemCtrl.Mode == "V")
        //            //    tb_SideHeader.Text = "Store Item Details View";

        //            //SetVisibilityToFalse();
        //            //Expander_Maintenance.IsExpanded = true;

        //            //StoreItemCtrl.Visibility = Visibility.Visible;
        //            //Expander1.Visibility = Visibility.Visible;
        //            //rect_Heading.Visibility = Visibility.Visible;
        //            break;

        //        case "TaxMainCtrl":
        //            //TaxMainCtrl.Mode = UCArgs.TargetControlMode;
        //            //TaxMainCtrl.InvokingControlName = UCArgs.InvokingControlName;
        //            //TaxMainCtrl.InvokingControlMode = UCArgs.InvokingControlMode;
        //            //tb_SideHeader.Text = "Tax Main";

        //            //SetVisibilityToFalse();
        //            //Expander_Maintenance.IsExpanded = true;

        //            //TaxMainCtrl.Visibility = Visibility.Visible;
        //            //Expander1.Visibility = Visibility.Visible;
        //            //rect_Heading.Visibility = Visibility.Visible;
        //            break;

        //        case "TaxDtlCtrl":
        //            //TaxDtlCtrl.Mode = UCArgs.TargetControlMode;
        //            //TaxDtlCtrl.InvokingControlName = UCArgs.InvokingControlName;
        //            //TaxDtlCtrl.InvokingControlMode = UCArgs.InvokingControlMode;
        //            //if (TaxDtlCtrl.Mode == "A")
        //            //    tb_SideHeader.Text = "Tax Details Input";
        //            //else if (TaxDtlCtrl.Mode == "U")
        //            //    tb_SideHeader.Text = "Tax Details Modify";
        //            //else if (TaxDtlCtrl.Mode == "V")
        //            //    tb_SideHeader.Text = "Tax Details View";

        //            //SetVisibilityToFalse();
        //            //Expander_Maintenance.IsExpanded = true;

        //            //TaxDtlCtrl.Visibility = Visibility.Visible;
        //            //Expander1.Visibility = Visibility.Visible;
        //            //rect_Heading.Visibility = Visibility.Visible;
        //            break;

        //        case "TaxItemCtrl":
        //            //TaxItemCtrl.Mode = UCArgs.TargetControlMode;
        //            //TaxItemCtrl.InvokingControlName = UCArgs.InvokingControlName;
        //            //TaxItemCtrl.InvokingControlMode = UCArgs.InvokingControlMode;
        //            //if (TaxItemCtrl.Mode == "A")
        //            //    tb_SideHeader.Text = "Add Items to Tax Code";
        //            //else if (TaxItemCtrl.Mode == "U")
        //            //    tb_SideHeader.Text = "Items Linked to Tax Code - Modify";
        //            //else if (TaxItemCtrl.Mode == "V")
        //            //    tb_SideHeader.Text = "Items Linked to Tax Code - View";

        //            //SetVisibilityToFalse();
        //            //Expander_Maintenance.IsExpanded = true;

        //            //TaxItemCtrl.Visibility = Visibility.Visible;
        //            //Expander1.Visibility = Visibility.Visible;
        //            //rect_Heading.Visibility = Visibility.Visible;
        //            break;

        //        case "ComposeMailCtrl":
        //            //tb_SideHeader.Text = "Send Mail";
        //            //ComposeMailCtrl.Mode = UCArgs.TargetControlMode;
        //            //ComposeMailCtrl.InvokingControlName = UCArgs.InvokingControlName;
        //            //ComposeMailCtrl.InvokingControlMode = UCArgs.InvokingControlMode;


        //            //SetVisibilityToFalse();
        //            //Expander_MainMenu.IsExpanded = true;

        //            //ComposeMailCtrl.Visibility = Visibility.Visible;
        //            //Expander1.Visibility = Visibility.Visible;
        //            //rect_Heading.Visibility = Visibility.Visible;
        //            break;

        //        case "BillRepCtrl":
        //            //tb_SideHeader.Text = "Bill Details";
        //            //BillRepCtrl.Mode = UCArgs.TargetControlMode;
        //            //BillRepCtrl.InvokingControlName = UCArgs.InvokingControlName;
        //            //BillRepCtrl.InvokingControlMode = UCArgs.InvokingControlMode;

        //            //SetVisibilityToFalse();
        //            //Expander_Reports.IsExpanded = true;

        //            //BillRepCtrl.Visibility = Visibility.Visible;
        //            //Expander1.Visibility = Visibility.Visible;
        //            //rect_Heading.Visibility = Visibility.Visible;
        //            break;

        //        case "CustOutstandingRepCtrl":
        //            //tb_SideHeader.Text = "Customer Outstanding Report";
        //            //CustOutstandingRepCtrl.Mode = UCArgs.TargetControlMode;
        //            //CustOutstandingRepCtrl.InvokingControlName = UCArgs.InvokingControlName;
        //            //CustOutstandingRepCtrl.InvokingControlMode = UCArgs.InvokingControlMode;

        //            //SetVisibilityToFalse();
        //            //Expander_Reports.IsExpanded = true;

        //            //CustOutstandingRepCtrl.Visibility = Visibility.Visible;
        //            //Expander1.Visibility = Visibility.Visible;
        //            //rect_Heading.Visibility = Visibility.Visible;
        //            break;

        //        case "DailySalesRepCtrl":
        //            //tb_SideHeader.Text = "Daily Sales Report";
        //            //DailySalesRepCtrl.Mode = UCArgs.TargetControlMode;
        //            //DailySalesRepCtrl.InvokingControlName = UCArgs.InvokingControlName;
        //            //DailySalesRepCtrl.InvokingControlMode = UCArgs.InvokingControlMode;

        //            //SetVisibilityToFalse();
        //            //Expander_Reports.IsExpanded = true;

        //            //DailySalesRepCtrl.Visibility = Visibility.Visible;
        //            //Expander1.Visibility = Visibility.Visible;
        //            //rect_Heading.Visibility = Visibility.Visible;
        //            break;

        //        //case "ExpiringItemsRepCtrl":
        //        //        tb_SideHeader.Text = "Expiring Items Report";
        //        //        ExpiringItemsRepCtrl.Mode = UCArgs.TargetControlMode;
        //        //        ExpiringItemsRepCtrl.InvokingControlName = UCArgs.InvokingControlName;
        //        //        ExpiringItemsRepCtrl.InvokingControlMode = UCArgs.InvokingControlMode;

        //        //        SetVisibilityToFalse();
        //        //        Expander_Reports.IsExpanded = true;

        //        //        ExpiringItemsRepCtrl.Visibility = Visibility.Visible;
        //        //        Expander1.Visibility = Visibility.Visible;
        //        //        rect_Heading.Visibility = Visibility.Visible;
        //        //        break;

        //        case "MISReportCtrl":
        //            //tb_SideHeader.Text = "Dash Board";
        //            //MISReportCtrl.Mode = UCArgs.TargetControlMode;
        //            //MISReportCtrl.InvokingControlName = UCArgs.InvokingControlName;
        //            //MISReportCtrl.InvokingControlMode = UCArgs.InvokingControlMode;

        //            //SetVisibilityToFalse();

        //            //Expander_MainMenu.IsExpanded = true;
        //            //Expander1.IsExpanded = true;
        //            //MISReportCtrl.Visibility = Visibility.Visible;
        //            //Expander1.Visibility = Visibility.Visible;
        //            //rect_Heading.Visibility = Visibility.Visible;
        //            break;

        //        case "UserDtlInputCtrl":
        //            //UserDtlInputCtrl.Mode = UCArgs.TargetControlMode;
        //            //UserDtlInputCtrl.InvokingControlName = UCArgs.InvokingControlName;
        //            //UserDtlInputCtrl.InvokingControlMode = UCArgs.InvokingControlMode;
        //            //if (UserDtlInputCtrl.Mode == "A")
        //            //    tb_SideHeader.Text = "User Details Input";
        //            //else if (UserDtlInputCtrl.Mode == "U")
        //            //    tb_SideHeader.Text = "User Details Modify";
        //            //else if (UserDtlInputCtrl.Mode == "V")
        //            //    tb_SideHeader.Text = "User Details View";

        //            //SetVisibilityToFalse();
        //            //Expander_Maintenance.IsExpanded = true;

        //            //UserDtlInputCtrl.Visibility = Visibility.Visible;
        //            //Expander1.Visibility = Visibility.Visible;
        //            //rect_Heading.Visibility = Visibility.Visible;
        //            break;

        //        case "UserMainCtrl":
        //            //UserMainCtrl.Mode = UCArgs.TargetControlMode;
        //            //UserMainCtrl.InvokingControlName = UCArgs.InvokingControlName;
        //            //UserMainCtrl.InvokingControlMode = UCArgs.InvokingControlMode;

        //            //tb_SideHeader.Text = "Users Main";

        //            //SetVisibilityToFalse();
        //            //Expander_Maintenance.IsExpanded = true;

        //            //UserMainCtrl.Visibility = Visibility.Visible;
        //            //Expander1.Visibility = Visibility.Visible;
        //            //rect_Heading.Visibility = Visibility.Visible;
        //            break;

        //        default:
        //            tb_SideHeader.Text = "";

        //            SetVisibilityToFalse();
        //            Expander_MainMenu.IsExpanded = true;

        //            //MISReportCtrl.Visibility = Visibility.Visible;
        //            Expander1.Visibility = Visibility.Visible;
        //            rect_Heading.Visibility = Visibility.Visible;
        //            SetCheckedProperty();
        //            break;
        //    }

        //    SetHeightWidth();
        //}

        //public void SetVisibilityToFalse()
        //{
        //    SalesMainCtrl.Visibility = Visibility.Collapsed;
        //    CustDtlInputCtrl.Visibility = Visibility.Collapsed;
        //    CustMainCtrl.Visibility = Visibility.Collapsed;
        //    ManufDtlInputCtrl.Visibility = Visibility.Collapsed;
        //    ManufMainCtrl.Visibility = Visibility.Collapsed;
        //    VendorDtlInputCtrl.Visibility = Visibility.Collapsed;
        //    VendorMainCtrl.Visibility = Visibility.Collapsed;
        //    DiscMainCtrl.Visibility = Visibility.Collapsed;
        //    DiscDtlCtrl.Visibility = Visibility.Collapsed;
        //    DiscItemCtrl.Visibility = Visibility.Collapsed;
        //    ItemMainCtrl.Visibility = Visibility.Collapsed;
        //    ItemMainCtrl.Visibility = Visibility.Collapsed;
        //    SearchMainCtrl.Visibility = Visibility.Collapsed;
        //    StoreItemCtrl.Visibility = Visibility.Collapsed;
        //    TaxMainCtrl.Visibility = Visibility.Collapsed;
        //    TaxDtlCtrl.Visibility = Visibility.Collapsed;
        //    TaxItemCtrl.Visibility = Visibility.Collapsed;
        //    ComposeMailCtrl.Visibility = Visibility.Collapsed;
        //    BillRepCtrl.Visibility = Visibility.Collapsed;
        //    MISReportCtrl.Visibility = Visibility.Collapsed;
        //    CustOutstandingRepCtrl.Visibility = Visibility.Collapsed;
        //    DailySalesRepCtrl.Visibility = Visibility.Collapsed;
        //    //ExpiringItemsRepCtrl.Visibility = Visibility.Collapsed;
        //    UserDtlInputCtrl.Visibility = Visibility.Collapsed;
        //    UserDtlViewCtrl.Visibility = Visibility.Collapsed;
        //    UserMainCtrl.Visibility = Visibility.Collapsed;

        //    Expander1.Visibility = Visibility.Collapsed;
        //    rect_Heading.Visibility = Visibility.Collapsed;
        //    //CalcCtrl.Visibility = Visibility.Visible;


        //    Expander_MainMenu.IsExpanded = false;
        //    Expander_Reports.IsExpanded = false;
        //    Expander_Maintenance.IsExpanded = false;
        //    Expander_QuickSearch.IsExpanded = false;
        //    Expander_Calculator.IsExpanded = false;
        //}

        private void SetHeightWidth()
        {
            ScreenHeight = NewMainScreen.Height;
            ScreenWidth = NewMainScreen.Width;

            if (ScreenWidth > 0)
            {
                AppLogo.Width = ScreenWidth - 10;
                Footer.Width = ScreenWidth - 10;
            }
        }

        private void SalesMainCtrl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Expander_MainMenu_Expanded(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
        }

        private void Expander_MainMenu_Collapsed(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
        }

        private void Expander_QuickSearch_Expanded(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
        }

        private void Expander_QuickSearch_Collapsed(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
        }

        private void Expander_Calculator_Expanded(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
        }

        private void Expander_Calculator_Collapsed(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
        }

        //private void ClearAll()
        //{
        //    CustDtlInputCtrl.ClearControls();
        //    CustMainCtrl.uc_CustDetails.ClearControls();
        //    VendorDtlInputCtrl.ClearControls();
        //    VendorMainCtrl.uc_VendorDetails.ClearControls();
        //    ManufDtlInputCtrl.ClearControls();
        //    ManufMainCtrl.uc_ManufDetails.ClearControls();
        //    DiscMainCtrl.ClearControls();
        //    DiscDtlCtrl.ClearControls();
        //    ItemMainCtrl.ClearControls();
        //    ItemMainCtrl.ClearControls();
        //    SearchMainCtrl.ClearControls();
        //    StoreItemCtrl.ClearControls();
        //}

        private void SetCheckedProperty(MenuItem MenuItem)
        {
            foreach (MenuItem child in Menu_MainMenu.Items)
            {
                ((MenuItem)child).IsChecked = false;
            }

            foreach (MenuItem child in Menu_Maintenance.Items)
            {
                ((MenuItem)child).IsChecked = false;
            }

            foreach (MenuItem child in Menu_Reports.Items)
            {
                ((MenuItem)child).IsChecked = false;
            }

            foreach (MenuItem child in Menu_Reports.Items)
            {
                ((MenuItem)child).IsChecked = false;
            }

            MenuItem.IsChecked = true;
        }

        private void SetCheckedProperty()
        {
            foreach (MenuItem child in Menu_MainMenu.Items)
            {
                ((MenuItem)child).IsChecked = false;
            }

            foreach (MenuItem child in Menu_Reports.Items)
            {
                ((MenuItem)child).IsChecked = false;
            }

            foreach (MenuItem child in Menu_Reports.Items)
            {
                ((MenuItem)child).IsChecked = false;
            }

            foreach (MenuItem child in Menu_Reports.Items)
            {
                ((MenuItem)child).IsChecked = false;
            }
        }

        private void Expander_Reports_Expanded(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
        }

        private void Expander_Reports_Collapsed(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
        }

        private void NewMainScreen_ContentRendered(object sender, EventArgs e)
        {
            //dockpanel_MainScreen.Height = dockpanel_MainScreen.ActualHeight - 30;

            //AppLogo.Width = this.ActualWidth - 10;
            //Footer.Width = this.ActualWidth - 10;

            //Expander1.Height = this.Height;
        }

        private void NewMainScreen_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Menu_Preferences_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).WindowStyle = System.Windows.WindowStyle.None;
            Window.GetWindow(this).Opacity = 0.25;

            WinPreferences MyWinPreferences = new WinPreferences();
            MyWinPreferences.ShowDialog();

            Window.GetWindow(this).WindowStyle = System.Windows.WindowStyle.None;
            Window.GetWindow(this).Opacity = 1;

        }

        private void Expander_Tools_Expanded(object sender, RoutedEventArgs e)
        {

        }

        private void Expander_Tools_Collapsed(object sender, RoutedEventArgs e)
        {

        }

        private void Menu_UploadFromExcel_Click(object sender, RoutedEventArgs e)
        {
            SetCheckedProperty(Menu_UploadFromExcel);


            if (MyExcelInterOpTabItemCtrl == null)
            {
                MyExcelInterOpTabItemCtrl = new TabItem();
                MyExcelInterOpTabItemCtrl.Name = "MyExcelInterOpTabItemCtrl";
            }

            MyExcelInterOpCtrl = new ExcelInterOpCtrl();
            MyExcelInterOpCtrl.InvokingControlName = "NewMain";
            MyExcelInterOpCtrl.InvokingControlMode = "M";
            MyExcelInterOpCtrl.Name = "MyExcelInterOpCtrl";
            MyExcelInterOpCtrl.Mode = "M";
            MyExcelInterOpCtrl.ControlTabName = "MyExcelInterOpTabItemCtrl";
            MyExcelInterOpCtrl.VerticalAlignment = VerticalAlignment.Top;

            MyExcelInterOpTabItemCtrl.Content = MyExcelInterOpCtrl;
            MyExcelInterOpTabItemCtrl.VerticalAlignment = VerticalAlignment.Top;
            MyExcelInterOpTabItemCtrl.VerticalContentAlignment = VerticalAlignment.Top;

            MyExcelInterOpCtrl.AddHandler(ExcelInterOpCtrl.ExcelInterOpWithCustomArgsEvent, new ExcelInterOpCtrl.ExcelInterOpWithCustomArgsEventHandler(NotifyUserControl));

            var matchingItem = tab_Main.Items.Cast<TabItem>()
                    .Where(item => item.Name == "MyExcelInterOpTabItemCtrl")
                    .FirstOrDefault();

            if (matchingItem != null)
                tab_Main.SelectedItem = matchingItem;
            else
                tab_Main.Items.Add(MyExcelInterOpTabItemCtrl);


            tab_Main.Visibility = Visibility.Visible;
            MyExcelInterOpTabItemCtrl.Header = "Upload Data";
            MyExcelInterOpTabItemCtrl.Focus();
        }

        private void Menu_Calculator_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start("WPFCalculator.exe");
            }
            catch (Exception ex1)
            {
                WinMessageBox MsgBox = new WinMessageBox("Calculator Not Found!", "Calculator", 0, "ERROR");
                MsgBox.ShowDialog();
            }
        }

        //private void cmb_Theme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    ResourceDictionary resourceDictionary = new ResourceDictionary();

        //    int theme = cmb_Theme.SelectedIndex;

        //    switch (theme)
        //    {
        //        case 0:
        //            resourceDictionary = Application.LoadComponent(
        //            new Uri(@"Resources\BlackBerryTheme.xaml",
        //            UriKind.Relative)) as ResourceDictionary;
        //            break;
        //        case 1:
        //            resourceDictionary = Application.LoadComponent(
        //            new Uri(@"Resources\ForestGreenTheme.xaml",
        //            UriKind.Relative)) as ResourceDictionary;
        //            break;
        //        default:
        //            break;
        //    }

        //    Application.Current.Resources.Clear();
        //    Application.Current.Resources = resourceDictionary;
        //}
    }
}
