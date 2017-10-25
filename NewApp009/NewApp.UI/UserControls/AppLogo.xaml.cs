using System.Windows;
using System.Windows.Controls;

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
