using System.Windows.Controls;

namespace NewApp.UI.UserControls
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : UserControl
    {
        public delegate void NotifyUserControl(object sender, UserCtrlArgs UserCtrlArgs);
        string _mode;
        string _invokingCtrlMode;
        string _invokingCtrlName;

        public Dashboard()
        {
            InitializeComponent();
        }

        public string Mode
        {
            get{return _mode;}
            set{_mode = value;}
        }

        public string InvokingControlMode
        {
            get{return _invokingCtrlMode;}
            set{_invokingCtrlMode = value;}
        }

        public string InvokingControlName
        {
            get{return _invokingCtrlName;}
            set{_invokingCtrlName = value;}
        }
    }
}
