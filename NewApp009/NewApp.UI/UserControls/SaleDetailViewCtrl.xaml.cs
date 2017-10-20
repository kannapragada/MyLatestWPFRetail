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
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class SaleDetailViewCtrl : UserControl
    {
        public delegate void NotifyUserControl(object sender, UserCtrlArgs UserCtrlArgs);
        //public event NotifyUserControl NotifyedUserControl;
        string _mode;
        string _invokingCtrlMode;
        string _invokingCtrlName;

        
        public SaleDetailViewCtrl()
        {
            InitializeComponent();

            this.Visibility = Visibility.Visible;
            this.Focus();
            DisplayDetails();

        }

        public SaleDetailViewCtrl(string Mode)
        {
            InitializeComponent();

            _mode = Mode;
            
            this.Visibility = Visibility.Visible;
            this.Focus();
            DisplayDetails();

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

        
        public void DisplayDetails()
        {
            string errorMessage = null;
            Sale SaleObj = new Sale();

            if (_mode == "V")
            {
                if (ApplicationState.GetValue<Sale>("SaleObj", out SaleObj, out errorMessage) == false)
                {
                    WinMessageBox WinMsgBox = new WinMessageBox("Nothing To Display", "Sale Details", 0, "ERROR");
                    WinMsgBox.ShowDialog();
                    return;
                }
            }
            else
            {
                if (ApplicationState.GetValue<Sale>("NewSaleObj", out SaleObj, out errorMessage) == false)
                {
                    WinMessageBox WinMsgBox = new WinMessageBox("Nothing To Display", "Sale Details", 0, "ERROR");
                    WinMsgBox.ShowDialog();
                    return;
                }
            }

            dg_SaleDetailView.ItemsSource = null;
            dg_SaleDetailView.ItemsSource = (List<SaleItem>)SaleObj.SaleItemsList;
            dg_SaleDetailView.Width = 790;
            dg_SaleDetailView.Height = 300;

            SetHeightWidthToThisWindow(Convert.ToInt32(dg_SaleDetailView.Width + 50), Convert.ToInt32(dg_SaleDetailView.Height + 125));


            DataGridTextColumn NewColumn;

            NewColumn = new DataGridTextColumn();
            NewColumn.Header = "Item Id";
            NewColumn.Binding = new Binding("ItemId");
            NewColumn.Width = 60;
            dg_SaleDetailView.Columns.Add(NewColumn);

            NewColumn = new DataGridTextColumn();
            NewColumn.Header = "Batch Id";
            NewColumn.Binding = new Binding("BatchId");
            NewColumn.Width = 80;
            dg_SaleDetailView.Columns.Add(NewColumn);

            NewColumn = new DataGridTextColumn();
            NewColumn.Header = "Item Name";
            NewColumn.Binding = new Binding("ItemName");
            NewColumn.Width = 150;
            dg_SaleDetailView.Columns.Add(NewColumn);

            NewColumn = new DataGridTextColumn();
            NewColumn.Header = "Quantity";
            NewColumn.Binding = new Binding("Quantity");
            NewColumn.Width = 75;
            dg_SaleDetailView.Columns.Add(NewColumn);

            NewColumn = new DataGridTextColumn();
            NewColumn.Header = "Amount/Unit";
            NewColumn.Binding = new Binding("ItemAmtPerUnit");
            NewColumn.Binding.StringFormat = "{0:N2}";
            NewColumn.Width = 100;
            dg_SaleDetailView.Columns.Add(NewColumn);

            NewColumn = new DataGridTextColumn();
            NewColumn.Header = "Discount/Unit";
            NewColumn.Binding = new Binding("ItemDiscAmount");
            NewColumn.Binding.StringFormat = "{0:N2}";
            NewColumn.Width = 100;
            dg_SaleDetailView.Columns.Add(NewColumn);

            NewColumn = new DataGridTextColumn();
            NewColumn.Header = "Total Amount";
            NewColumn.Binding = new Binding("TotalItemAmt");
            NewColumn.Binding.StringFormat = "{0:N2}";
            NewColumn.Width = 100;
            dg_SaleDetailView.Columns.Add(NewColumn);

            NewColumn = new DataGridTextColumn();
            NewColumn.Header = "Final Amount";
            NewColumn.Binding = new Binding("FinalItemAmt");
            NewColumn.Binding.StringFormat = "{0:C2}";
            NewColumn.Width = 100;
            dg_SaleDetailView.Columns.Add(NewColumn);
        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;

            CloseThisWindow();
        }

        private void CloseThisWindow()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.Name == "WndSaleDetailView")
                {
                    window.Close();
                    break;
                }
            }
        }

        private void SetHeightWidthToThisWindow(int width, int height)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.Name == "WndSaleDetailView")
                {
                    window.Width = width;
                    window.Height = height;
                    break;
                }
            }
        }

        private void btn_Print_Click(object sender, RoutedEventArgs e)
        {
            string errorMessage = null;
            Sale SaleObj = new Sale();

            if (ApplicationState.GetValue<Sale>("SaleObj", out SaleObj, out errorMessage) == true)
            {
                Window.GetWindow(this).WindowStyle = System.Windows.WindowStyle.None;
                Window.GetWindow(this).Opacity = 0.25;


                WinBill WinBill = new WinBill(SaleObj, true, _mode);

                try
                {
                    WinBill.ShowDialog();
                }
                catch (Exception ex1)
                {
                    Window.GetWindow(this).WindowStyle = System.Windows.WindowStyle.None;
                    Window.GetWindow(this).Opacity = 1;
                }

                Window.GetWindow(this).WindowStyle = System.Windows.WindowStyle.None;
                Window.GetWindow(this).Opacity = 1;
            }
        }
    }
}
