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


namespace NewApp
{
    /// <summary>
    /// Interaction logic for Window14.xaml
    /// </summary>
    public partial class Window14 : Window
    {
        public Window14()
        {
            InitializeComponent();
        }

        public class MyData
        {
            string _data;
            string _status;

            public MyData()
            {
            }

            public string Data
            {
                get { return _data; }
                set { _data = value; }
            }

            public string Status
            {
                get { return _status; }
                set { _status = value; }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            List<MyData> MyDataObjList = new List<MyData>();

            MyData MyDataObj = new MyData();
            MyDataObj.Data = "Customer Data";
            MyDataObj.Status = "Not Started";

            MyDataObjList.Add(MyDataObj);

            MyDataObj = new MyData();
            MyDataObj.Data = "Manufacturer Data";
            MyDataObj.Status = "Not Started";

            MyDataObjList.Add(MyDataObj);

            MyDataObj = new MyData();
            MyDataObj.Data = "Vendor Data";
            MyDataObj.Status = "Not Started";

            MyDataObjList.Add(MyDataObj);

            MyDataObj = new MyData();
            MyDataObj.Data = "Store Item Data";
            MyDataObj.Status = "Not Started";

            MyDataObjList.Add(MyDataObj);

            MyDataObj = new MyData();
            MyDataObj.Data = "Discount Data";
            MyDataObj.Status = "Not Started";

            MyDataObjList.Add(MyDataObj);

            MyDataObj = new MyData();
            MyDataObj.Data = "Tax Data";
            MyDataObj.Status = "Not Started";

            MyDataObjList.Add(MyDataObj);

            MyDataObj = new MyData();
            MyDataObj.Data = "User Data";
            MyDataObj.Status = "Not Started";

            MyDataObjList.Add(MyDataObj);

            DataGrid1.ItemsSource = MyDataObjList;
        }
    }
}
