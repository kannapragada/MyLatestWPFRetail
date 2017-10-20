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
using NewApp.BusinessTier.Common;
using NewApp.BusinessTier.Models;
using NewApp.UI.UserControls;
using NewApp.Themes;
using NewApp.UI.Windows;
using NewApp.SQLDataAccessFactorySvc;
using NewApp.GenderTypeFactorySvc;
using NewApp.IDProofTypeFactorySvc;




namespace NewApp.UI
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {
        public delegate void NotifyUserControl(object sender, UserCtrlArgs UserCtrlArgs);
        string _mode;
        string _invokingCtrlMode;
        string _invokingCtrlName;
        private BackgroundWorker bgworker;

        private string _userid;
        private string _password;




        public LoginScreen()
        {
            InitializeComponent();


            pb_ProgressBar.Visibility = Visibility.Collapsed;
            tb_CurrentLoadingMsg.Text = "";

            this.bgworker = new BackgroundWorker();
            bgworker.WorkerReportsProgress = true;

            bgworker.DoWork += bgworker_DoWork;
            bgworker.RunWorkerCompleted += bgworker_RunWorkerCompleted;
            bgworker.ProgressChanged += bgworker_ProgressChanged;
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
            try
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

                pb_ProgressBar.Visibility = Visibility.Visible;
                tb_CurrentLoadingMsg.Text = "";

                _userid = txt_UserName.Text;
                _password = txt_Password.Password.ToString();
                bgworker.RunWorkerAsync();


                //string[] Themes = ThemeManager.GetThemes();
                //themes.ItemsSource = ThemeManager.GetThemes();



                Application.Current.ApplyTheme("PlainVanilla");
            }
            catch(Exception ex1)
            {
                WinMessageBox WinMsgBox = new WinMessageBox(ex1.Message, "Login", 0, "ERROR");
                WinMsgBox.ShowDialog();
                return;
            }
        }

        private void bgworker_DoWork(object sender, DoWorkEventArgs e)
        {
            string errorMessage = null;

            try
            {
                AppBusinessTierState AppBusinessTierState = new AppBusinessTierState();
                DataAccessServiceClient DataAccessServiceClient = new DataAccessServiceClient();


                bgworker.ReportProgress(5);
                if (DataAccessServiceClient.CheckDataAccess(out errorMessage) == true)
                {
                    System.Threading.Thread.Sleep(50);
                    bgworker.ReportProgress(15);
                }
                else
                {
                    e.Result = errorMessage.ToString();
                    return;
                }

                bgworker.ReportProgress(25);
                if (AppBusinessTierState.AuthenticateUser(_userid, _password, out errorMessage) == true)
                {
                    System.Threading.Thread.Sleep(50);
                    bgworker.ReportProgress(45);
                }
                else
                {
                    e.Result = errorMessage.ToString();
                    return;
                }

                bgworker.ReportProgress(50);
                if (AppBusinessTierState.LoadTypeParameters(out errorMessage) == true)
                {
                    System.Threading.Thread.Sleep(50);
                    bgworker.ReportProgress(65);
                }
                else
                {
                    e.Result = errorMessage.ToString();
                    return;
                }

                bgworker.ReportProgress(77);
                if (AppBusinessTierState.LoadStatusParameters(out errorMessage) == true)
                {
                    System.Threading.Thread.Sleep(50);
                    bgworker.ReportProgress(85);
                }
                else
                {
                    e.Result = errorMessage.ToString();
                    return;
                }

                bgworker.ReportProgress(90);
                if (AppBusinessTierState.LoadUserProfileInformation(out errorMessage) == true)
                {
                    System.Threading.Thread.Sleep(50);
                    bgworker.ReportProgress(100);
                    e.Result = "SUCCESS";
                }
                else
                {
                    e.Result = errorMessage.ToString();
                    return;
                }
            }
            catch(Exception ex1)
            {
                e.Result = ex1.Message.ToString();
                return;
            }
        }

        private void bgworker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 5)
                tb_CurrentLoadingMsg.Text = "Initializing...";
            else if (e.ProgressPercentage == 15)
                tb_CurrentLoadingMsg.Text = "Completed Checking Database Access";
            else if (e.ProgressPercentage == 25)
                tb_CurrentLoadingMsg.Text = "Authenticating User";
            else if (e.ProgressPercentage == 45)
                tb_CurrentLoadingMsg.Text = "User is Authenticated";
            else if (e.ProgressPercentage == 50)
                tb_CurrentLoadingMsg.Text = "Loading Type Parameters...";
            else if (e.ProgressPercentage == 65)
                tb_CurrentLoadingMsg.Text = "Completed Loading Type Parameters";
            else if (e.ProgressPercentage == 77)
                tb_CurrentLoadingMsg.Text = "Loading Status Parameters...";
            else if (e.ProgressPercentage == 85)
                tb_CurrentLoadingMsg.Text = "Completed Loading Status Parameters";
            else if (e.ProgressPercentage == 90)
                tb_CurrentLoadingMsg.Text = "Loading User Profile details...";
            else if (e.ProgressPercentage == 100)
                tb_CurrentLoadingMsg.Text = "Completed Loading User Profile details";

            pb_ProgressBar.Value = e.ProgressPercentage; //update progress bar
        }

        private void bgworker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string errorMessage = null;

            errorMessage = e.Result.ToString();


            if (errorMessage == "SUCCESS")
            {
                tb_CurrentLoadingMsg.Text = "Loading Application Main Screen...";

                NewMain WndNewMain = new NewMain();

                WndNewMain.Show();
                this.Close();
            }
            else
            {
                WinMessageBox WinMsgBox = new WinMessageBox(Convert.ToString(e.Result.ToString()), "Login", 0, "ERROR");
                WinMsgBox.ShowDialog();
                pb_ProgressBar.Visibility = Visibility.Collapsed;
                tb_CurrentLoadingMsg.Text = "";
                return;
            }


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
