using System.Windows;
using System.Windows.Controls;

namespace NewApp.UI.UserControls
{
    /// <summary>
    /// Interaction logic for HdrControls.xaml
    /// </summary>
    public partial class HdrControls : UserControl
    {
        public delegate void NotifyUserControl(object sender, UserCtrlArgs UserCtrlArgs);
        //public event NotifyUserControl NotifyedUserControl;
        string _mode;
        string _invokingCtrlMode;
        string _invokingCtrlName;

        public HdrControls()
        {
            InitializeComponent();
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
