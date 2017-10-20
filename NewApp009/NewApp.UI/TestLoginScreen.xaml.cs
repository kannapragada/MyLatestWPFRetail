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
using System.Collections;
using System.ComponentModel;
using System.Data;
using NewApp.BusinessTier.Common;
using NewApp.BusinessTier.Models;
using NewApp.UI.UserControls;
using NewApp.UI.Windows;
using WPF.Themes;
using NewApp.SQLDataAccessSvc;
using NewApp.GenderTypeFactorySvc;
using NewApp.IDProofTypeFactorySvc;





namespace NewApp.UI
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class TestLoginScreen : Window
    {
        public delegate void NotifyUserControl(object sender, UserCtrlArgs UserCtrlArgs);
        string _mode;
        string _invokingCtrlMode;
        string _invokingCtrlName;


        private string _userid;
        private string _password;

        private DataAccessServiceClient dbobj;

        
        public TestLoginScreen()
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

        private void Btn_Ok_Click(object sender, RoutedEventArgs e)
        {
            if (txt_UserName.Text == "")
            {
                WinMessageBox WinMsgBox = new WinMessageBox("User Id Cannot be Blank", "Login", 0, "ERROR");
                WinMsgBox.ShowDialog();
                return;
            }

            if (txt_Password.Password.ToString().Length == 0)
            {
                WinMessageBox WinMsgBox = new WinMessageBox("Password Cannot be Blank", "Login", 0, "ERROR");
                WinMsgBox.ShowDialog();
                return;
            }
            
            _userid = txt_UserName.Text;
            _password = txt_Password.Password.ToString();

            string errorMessage = null;

            dbobj = new DataAccessServiceClient();

            if (dbobj.CheckDataAccess(out errorMessage) == false)
                MessageBox.Show(errorMessage);
            else
                MessageBox.Show("Success!");


            //string[] Themes = ThemeManager.GetThemes();
            //themes.ItemsSource = ThemeManager.GetThemes();

            DataTable datatable = new DataTable();
            DataSet ds = new DataSet();

            Application.Current.ApplyTheme("PlainVanilla");

            dg_datatable.ItemsSource = "";

            datatable = dbobj.GetNormalTable();
            dg_datatable.ItemsSource = datatable.DefaultView;

            if (dbobj.GetNormalDataSet(out ds) == true)
            {
                dg_dataset_table1.ItemsSource = ds.Tables["table1"].DefaultView;
                dg_dataset_table2.ItemsSource = ds.Tables["table2"].DefaultView;
            }
            else
            {
                dg_dataset_table1.ItemsSource = "";
                dg_dataset_table2.ItemsSource = "";
            }

            this.Focus();
        }

        private void Btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            ForgotPwdScreen ForgotPwdScreen = new ForgotPwdScreen();

            this.Visibility = Visibility.Collapsed;
            
            ForgotPwdScreen.ShowDialog();

            this.Visibility = Visibility.Visible;
        }
    }
}
