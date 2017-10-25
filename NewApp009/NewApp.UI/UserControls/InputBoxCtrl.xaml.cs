using System.Windows;
using System.Windows.Controls;



namespace NewApp.UI.UserControls
{
    /// <summary>
    /// Interaction logic for SalesCtrl.xaml
    /// </summary>
    public partial class InputBoxCtrl : UserControl
    {
        public delegate void NotifyUserControl(object sender, UserCtrlArgs UserCtrlArgs);
        //public event NotifyUserControl NotifyedUserControl;
        string _mode;
        string _invokingCtrlMode;
        string _invokingCtrlName;

        public InputBoxCtrl()
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

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;

            CloseThisWindow();
        }

        private void btn_Ok_Click(object sender, RoutedEventArgs e)
        {
            ApplicationState.SetValue("InputBoxResult", txt_InputBoxResult.Text);

            CloseThisWindow();
        }

        private void CloseThisWindow()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.Name == "WndInputBox")
                {
                    window.Close();
                    break;
                }
            }
        }
    }
}
