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
