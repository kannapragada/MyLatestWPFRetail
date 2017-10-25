using System.Windows.Controls;

namespace NewApp.UI.UserControls
{
    /// <summary>
    /// Interaction logic for CustDtlsCtrl.xaml
    /// </summary>
    public partial class AppFooter : UserControl
    {
        public AppFooter()
        {
            InitializeComponent();

            this.Width = System.Windows.SystemParameters.PrimaryScreenWidth;
        }
    }
}
