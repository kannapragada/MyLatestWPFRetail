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
    /// Interaction logic for GenericViewer.xaml
    /// </summary>
    public partial class WinGenericViewer : Window
    {

        string _sideHeaderText = "";
        string _mode;

        GenericViewer MyGenericViewer;



        public WinGenericViewer()
        {
            InitializeComponent();
        }

        public WinGenericViewer(string Mode)
        {
            InitializeComponent();

            _mode = Mode;
        }


        public string SideHeaderText
        {
            get { return _sideHeaderText; }
            set { _sideHeaderText = value;}
        }


        private void WndGenericViewer_Loaded(object sender, RoutedEventArgs e)
        {
            MyGenericViewer = new GenericViewer(_mode);
            dockpanel_GenericViewer.Children.Add(MyGenericViewer);
            MyGenericViewer.tb_SideHeader.Text = _sideHeaderText;
        }
    }
}
