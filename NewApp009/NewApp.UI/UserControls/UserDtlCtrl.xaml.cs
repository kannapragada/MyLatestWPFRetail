using System.Windows.Controls;

namespace NewApp.UI.UserControls
{
    /// <summary>
    /// Interaction logic for CustDtlsCtrl.xaml
    /// </summary>
    public partial class UserDtlCtrl : UserControl
    {

        public delegate void NotifyUserControl(object sender, UserCtrlArgs UserCtrlArgs);
        //public event NotifyUserControl NotifyedUserControl;
        string _mode;
        //string _invokingCtrlMode;
        //string _invokingCtrlName;

        public UserDtlCtrl()
        {
            InitializeComponent();
        }

        public string Mode
        {
            get{return _mode;}
            set{_mode = value;}
        }
    }
}
