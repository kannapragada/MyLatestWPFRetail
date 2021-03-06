﻿using System;
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
using NewApp.BusinessTier.Common;
using NewApp.BusinessTier.Models;
using NewApp.UI.UserControls;
using NewApp.UI.Windows;
using NewApp.GenderTypeFactorySvc;
using NewApp.IDProofTypeFactorySvc;



namespace NewApp.UI.Windows
{
    /// <summary>
    /// Interaction logic for Sales.xaml
    /// </summary>
    public partial class WinInputBox : Window
    {
        public WinInputBox()
        {
            InitializeComponent();
        }

        public WinInputBox(string InputBoxMessage)
        {
            InitializeComponent();

            if (InputBoxMessage != null) 
                InputBoxCtrl.txt_InputBoxCaption.Text = InputBoxMessage;
        }

        private void InputBoxCtrl_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
