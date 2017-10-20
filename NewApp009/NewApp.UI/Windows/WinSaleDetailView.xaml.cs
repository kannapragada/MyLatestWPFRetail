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
using NewApp.BusinessTier.Common;
using NewApp.BusinessTier.Models;
using NewApp.UI.UserControls;
using NewApp.UI.Windows;
using NewApp.GenderTypeFactorySvc;
using NewApp.IDProofTypeFactorySvc;



namespace NewApp.UI.Windows
{
    /// <summary>
    /// Interaction logic for SaleDetailView.xaml
    /// </summary>
    public partial class WinSaleDetailView : Window
    {

        string _sideHeaderText = "";
        string _mode;

        SaleDetailViewCtrl SaleDetailViewCtrl;



        public WinSaleDetailView()
        {
            InitializeComponent();
        }

        public WinSaleDetailView(string Mode)
        {
            InitializeComponent();

            _mode = Mode;
        }


        public string SideHeaderText
        {
            get { return _sideHeaderText; }
            set { _sideHeaderText = value;}
        }


        private void WndSaleDetailView_Loaded(object sender, RoutedEventArgs e)
        {
            SaleDetailViewCtrl = new SaleDetailViewCtrl(_mode);
            dockpanel_SaleDetailView.Children.Add(SaleDetailViewCtrl);
            SaleDetailViewCtrl.tb_SideHeader.Text = _sideHeaderText;
        }
    }
}
