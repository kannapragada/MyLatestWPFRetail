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
using System.Windows.Threading;
using System.Threading;
using System.ComponentModel;
using System.IO;
using NewApp.UI.UserControls;
using NewApp.Reports;
using NewApp.BusinessTier.Common;
using NewApp.BusinessTier.Models;
using NewApp.UI.Windows;
using NewApp.GenderTypeFactorySvc;
using NewApp.IDProofTypeFactorySvc;




namespace NewApp.UI.Windows
{
    /// <summary>
    /// Interaction logic for WinBill.xaml
    /// </summary>
    public partial class WinBill : Window
    {


        bool _BusyBarBusy = false;
        bool m_autoPrint = false;
        string _mode;

        BillReport BillReport;


        public WinBill()
        {
            InitializeComponent();
        }

        public WinBill(Sale Sale, bool AutoPrint, string Mode)
        {
            InitializeComponent();

            _mode = Mode;
            m_autoPrint = AutoPrint;

            BackgroundWorker worker = new BackgroundWorker();

            worker.DoWork += (o, er) =>
            {
                Print();
            };

            worker.RunWorkerAsync();


        }


        private void Print()
        {
            string errorMessage = null;

            Sale SaleObj = new Sale();
            if (_mode == "V")
            {
                if (ApplicationState.GetValue<Sale>("SaleObj", out SaleObj, out errorMessage) == false)
                {
                    WinMessageBox WinMsgBox = new WinMessageBox("Nothing To Print!", "Bill Report", 0, "INFORMATION");
                    WinMsgBox.ShowDialog();
                    return;
                }
            }
            else
            {
                if (ApplicationState.GetValue<Sale>("NewSaleObj", out SaleObj, out errorMessage) == false)
                {
                    WinMessageBox WinMsgBox = new WinMessageBox("Nothing To Print!", "Bill Report", 0, "INFORMATION");
                    WinMsgBox.ShowDialog();
                    return;
                }
            }


            Application.Current.Dispatcher.BeginInvoke(
                        DispatcherPriority.Normal,
                        new Action(() => BillReport = new BillReport(SaleObj.SalesId, m_autoPrint)));


            Application.Current.Dispatcher.BeginInvoke(
                            DispatcherPriority.Normal,
                            new Action(() => BillReport._reportViewer.ShowToolBar = false));

            Application.Current.Dispatcher.BeginInvoke(
                        DispatcherPriority.Normal,
                        new Action(() => dockpanel_docpanel.Children.Add(BillReport)));
        }

        private void Btn_Ok_Click(object sender, RoutedEventArgs e)
        {
            CloseThisWindow();
        }

        private void CloseThisWindow()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.Name == "WndBill")
                {
                    window.Close();
                    break;
                }
            }
        }

        //protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        //{
        //    base.OnClosing(e);
        //    e.Cancel = true;
        //    this.Hide();
        //}

        public bool BusyBarBusy
        {
            get { return _BusyBarBusy; }
            set { _BusyBarBusy = value; }
        }
    }
}
