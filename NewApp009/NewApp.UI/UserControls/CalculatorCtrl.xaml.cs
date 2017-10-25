using System;
using System.Windows;
using System.Windows.Controls;

namespace NewApp.UI.UserControls
{
    public partial class CalculatorCtrl : UserControl
    {
        public CalculatorCtrl()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Button b = (Button) sender;
            tb_result.Text += b.Content.ToString();
        }

        private void Result_click(object sender, RoutedEventArgs e)
        {
            try{result();}
            catch (Exception ex1)
            {tb_result.Text = ex1.Message;}
        }

        private void result()
        {
            String op;
            int iOp = 0;
            if (tb_result.Text.Contains("+"))
            {iOp = tb_result.Text.IndexOf("+");}
            else if (tb_result.Text.Contains("-"))
            {iOp = tb_result.Text.IndexOf("-");}
            else if (tb_result.Text.Contains("*"))
            {iOp = tb_result.Text.IndexOf("*");}
            else if (tb_result.Text.Contains("/"))
            {iOp = tb_result.Text.IndexOf("/");}
            else
            {
                //error
            }
            
            op = tb_result.Text.Substring(iOp, 1);
            double op1 = Convert.ToDouble(tb_result.Text.Substring(0, iOp));
            double op2 = Convert.ToDouble(tb_result.Text.Substring(iOp + 1, tb_result.Text.Length - iOp - 1));

            if (op == "+")
            {tb_result.Text += "=" + (op1 + op2);}
            else if (op == "-")
            {tb_result.Text += "=" + (op1 - op2);}
            else if (op == "*")
            {tb_result.Text += "=" + (op1 * op2);}
            else
            {tb_result.Text += "=" + (op1 / op2);}
        }

        private void Off_Click_1(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            tb_result.Text = "";
        }

        private void R_Click(object sender, RoutedEventArgs e)
        {
            if (tb_result.Text.Length > 0){tb_result.Text = tb_result.Text.Substring(0, tb_result.Text.Length - 1);}
        }
    }
}
