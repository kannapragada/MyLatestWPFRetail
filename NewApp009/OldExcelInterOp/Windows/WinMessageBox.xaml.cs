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

namespace NewApp.UI.Windows
{
    /// <summary>
    /// Interaction logic for WinMessageBox.xaml
    /// </summary>
    public partial class WinMessageBox : Window
    {
        int _yesNoCancelValue;

        public WinMessageBox()
        {
            InitializeComponent();
        }

        public WinMessageBox(string Message, string MessageHeader, int BehaviorOption, string MessageType)
        {
            InitializeComponent();

            tb_Message.Text = Message.ToString();
            tb_AppMessageHeader.Text = MessageHeader;

            if (BehaviorOption == 0)
            {
                btn_Ok.Visibility = Visibility.Visible;
                btn_Yes.Visibility = Visibility.Collapsed;
                btn_No.Visibility = Visibility.Collapsed;
            }
            else if (BehaviorOption == 1)
            {
                btn_Ok.Visibility = Visibility.Collapsed;
                btn_Yes.Visibility = Visibility.Visible;
                btn_No.Visibility = Visibility.Visible;
            }

            BitmapImage logo = new BitmapImage();

            logo.BeginInit();
            if (MessageType == "INFORMATION")
                logo.UriSource = new Uri(@"pack://application:,,/images/MessageBox_Information.jpg");
            else if (MessageType == "QUESTION")
                logo.UriSource = new Uri(@"pack://application:,,/images/MessageBox_question.jpg");
            else if (MessageType == "WARNING")
                    logo.UriSource = new Uri(@"pack://application:,,/images/MessageBox_warning.jpg");
            else if (MessageType == "SUCCESS")
                    logo.UriSource = new Uri(@"pack://application:,,/images/MessageBox_Success.jpg");
            else if (MessageType == "FAILURE")
                    logo.UriSource = new Uri(@"pack://application:,,/images/MessageBox_failure.jpg");
            else if (MessageType == "VALIDATION")
                logo.UriSource = new Uri(@"pack://application:,,/images/MessageBox_validation.jpg");
            else if (MessageType == "ERROR")
                    logo.UriSource = new Uri(@"pack://application:,,/images/MessageBox_error.jpg");


            logo.EndInit();
            img_MessageType.Source = logo;

            if (Message.ToString().Length > 300)
            {
                tb_Message.FontSize = 11;
                this.Height = 250;
            }
            else if (Message.ToString().Length > 100)
            {
                tb_Message.FontSize = 15;
                this.Height = 225;
            }
            else if (Message.ToString().Length > 10)
            {
                tb_Message.FontSize = 18;
                this.Height = 225;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Ok_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public int YesNoCancelValue
        {
            set { _yesNoCancelValue = value; }
            get { return _yesNoCancelValue; }
        }

        private void btn_Yes_Click(object sender, RoutedEventArgs e)
        {            
            _yesNoCancelValue = 0;
            this.DialogResult = true;
        }

        private void btn_No_Click(object sender, RoutedEventArgs e)
        {
            _yesNoCancelValue = 1;
            this.DialogResult = true;
        }


    }
}
