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
using NewApp.UI.Windows;

namespace NewApp.UI.UserControls
{
    /// <summary>
    /// Interaction logic for CustDtlsCtrl.xaml
    /// </summary>
    public partial class AppLogo : UserControl
    {
        public AppLogo()
        {
            InitializeComponent();
        }

        private void menuitem_LogOut_Click(object sender, RoutedEventArgs e)
        {
            ApplicationState.Clear();
            App.Current.Shutdown();
        }
    }
}
