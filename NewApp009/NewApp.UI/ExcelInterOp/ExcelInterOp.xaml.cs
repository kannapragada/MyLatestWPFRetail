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
using Excel = Microsoft.Office.Interop.Excel;
using System.ComponentModel;
using System.Data;
using Microsoft.Win32;
using System.Reflection;
using NewApp.BusinessTier.Models;
using NewApp.ExcelInterOp;


namespace NewApp.ExcelInterOp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ExcelInterOpCtrl_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
