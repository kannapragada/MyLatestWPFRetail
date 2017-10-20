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
using System.Globalization;  



namespace NewApp
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();

            
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            //BitmapImage logo = new BitmapImage();

            //logo.BeginInit();
            //logo.UriSource = new Uri(@"pack://application:,,/images/MessageBox_Information.bmp");
            //logo.EndInit();
            ////image1.Source = logo;

            
        }

        private void Btn_myButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime dt = DateTime.Now;
            textbox_myAmount.Text = String.Format("{0:f}", dt);
        }
    }
}
