using NewApp.BusinessTier.Models;
using NewApp.UI.Windows;
using NewApp.UserFactorySvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;



namespace NewApp.UI.UserControls
{
    /// <summary>
    /// Interaction logic for SalesMainCtrl.xaml
    /// </summary>
    public partial class UserMainCtrl : UserControl
    {
        List<User> UserObjList;

        public static readonly RoutedEvent UserMainWithCustomArgsEvent = EventManager.RegisterRoutedEvent("UserMainWithCustomArgs", RoutingStrategy.Bubble, typeof(UserMainWithCustomArgsEventHandler), typeof(UserMainCtrl));
        public delegate void UserMainWithCustomArgsEventHandler(object sender, ProcessEventArgs e);


        public delegate void ProcessHandler(object sender, ProcessEventArgs data);
        string _mode;
        string _invokingCtrlMode;
        string _invokingCtrlName;
        string _CtrlTabName;

        UserCtrlArgs UCArgs;

        public UserMainCtrl()
        {
            InitializeComponent();

            uc_UserDetails.Visibility = Visibility.Collapsed;
        }

        public string Mode
        {
            get{return _mode;}
            set
            {
                _mode = value;

                if (_mode == "S")
                    btn_SelUser.Visibility = Visibility.Visible;
                else
                    btn_SelUser.Visibility = Visibility.Collapsed;
            }
        }

        public string InvokingControlMode
        {
            get{return _invokingCtrlMode;}
            set{_invokingCtrlMode = value;}
        }

        public string InvokingControlName
        {
            get{return _invokingCtrlName;}
            set{_invokingCtrlName = value;}
        }

        public string ControlTabName
        {
            get { return _CtrlTabName; }
            set { _CtrlTabName = value; }
        }

        public event UserMainWithCustomArgsEventHandler UserMainWithCustomArgs
        {
            add { AddHandler(UserMainWithCustomArgsEvent, value); }
            remove { RemoveHandler(UserMainWithCustomArgsEvent, value); }
        }


        private void Btn_Close_Click(object sender, RoutedEventArgs e)
        {
            UCArgs = new UserCtrlArgs();
            UCArgs.InvokingControlName = this.Name;
            UCArgs.InvokingControlMode = this.Mode;
            UCArgs.TargetControlName = this.InvokingControlName;
            UCArgs.TargetControlMode = this.InvokingControlMode;
            UCArgs.ControlTabName = this.ControlTabName;

            e.Handled = true;
            ProcessEventArgs myargs = new ProcessEventArgs(UserMainWithCustomArgsEvent, UCArgs.InvokingControlName, UCArgs.InvokingControlMode, UCArgs.TargetControlName, UCArgs.TargetControlMode, null, null, UCArgs.ControlTabName);
            RaiseEvent(myargs);
        }

        private void txt_UserNameId_TextChanged(object sender, TextChangedEventArgs e)
        {
            dg_UserMain.ItemsSource = "";
            if (txt_UserNameId.Text != "")
            {
                SearchUsers(txt_UserNameId.Text);
                if (dg_UserMain.Items.Count > 0)
                {
                    dg_UserMain.SelectedIndex = 0;
                    if ((UserObjList != null) && (UserObjList.Count > 0) && (dg_UserMain.SelectedIndex > -1))
                    {
                        this.uc_UserDetails.ClearControls();
                        this.uc_UserDetails.DisplayUser((User)UserObjList[dg_UserMain.SelectedIndex]);
                        this.uc_UserDetails.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        this.uc_UserDetails.ClearControls();
                        this.uc_UserDetails.Visibility = Visibility.Collapsed;
                    }
                }
            }
            else
            {
                dg_UserMain.ItemsSource = "";
            }

            this.Focus();
        }

        public void SearchUsers(string Str)
        {
            string errorMessage = null;

            User User = new User();
            User[] UserObjArray;
            UserObjList = new List<User>();

            UserFactoryClient UserFactoryClient = new UserFactoryClient();

            if (UserFactoryClient.GetUsers(out errorMessage, out UserObjArray, Str) == true)
            {
                if (UserObjArray != null)
                {
                    UserObjList = UserObjArray.ToList<User>();
                    this.DataContext = UserObjList;
                    if (UserObjList.Count > 0)
                        dg_UserMain.ItemsSource = UserObjList;
                    else
                        dg_UserMain.ItemsSource = "";
                }
            }
            else
            {
                if ((errorMessage != "") && (errorMessage != null))
                {
                    WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Search Users", 0, "ERROR");
                    MsgBox.ShowDialog();
                    return;
                }
            }
        }

        private void btn_AddUser_Click(object sender, RoutedEventArgs e)
        {
            UCArgs = new UserCtrlArgs();
            UCArgs.InvokingControlName = this.Name;
            UCArgs.InvokingControlMode = this.Mode;
            UCArgs.TargetControlName = "MyUserDtlInputCtrl";
            UCArgs.TargetControlMode = "A";
            UCArgs.ControlTabName = this.ControlTabName;

            ProcessEventArgs myargs = new ProcessEventArgs(UserMainWithCustomArgsEvent, UCArgs.InvokingControlName, UCArgs.InvokingControlMode, UCArgs.TargetControlName, UCArgs.TargetControlMode, null, null, UCArgs.ControlTabName);
            RaiseEvent(myargs);
        }

        private void btn_ModifyUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dg_UserMain.SelectedIndex > -1)
                {
                    if (this.Parent != null && this.Parent is StackPanel)
                    {
                        StackPanel parentControl = this.Parent as StackPanel;
                        foreach (UIElement child in parentControl.Children)
                        {
                            if (child is UserDtlInputCtrl)
                            {
                                UserCtrlArgs Args = new UserCtrlArgs();
                                Args.TargetControlName = "UserDtlInputCtrl";
                                Args.TargetControlMode = "U";

                                Args.InvokingControlName = this.Name;
                                Args.InvokingControlMode = this.Mode;

                                User UserObj = (User)UserObjList[dg_UserMain.SelectedIndex];
                                ((UserDtlInputCtrl)child).ClearControls();
                                ((UserDtlInputCtrl)child).DisplayUser(UserObj);

                                //this.NotifyedUserControl(this, Args);
                            }
                        }
                    }
                }
            }
            catch (Exception ex1)
            {
                WinMessageBox MsgBox = new WinMessageBox(ex1.Message.ToString(), "User Main - Modify", 0, "ERROR");
                MsgBox.ShowDialog();
            }
        }

        private void btn_DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            string errorMessage = null;

            try
            {
                if (dg_UserMain.SelectedIndex > -1)
                {
                    User UserObj = (User)UserObjList[dg_UserMain.SelectedIndex];

                    UserFactoryClient UserFactoryClient = new UserFactoryClient();

                    if (UserFactoryClient.DeleteUserDetails(out errorMessage, UserObj) == true)
                    {
                        WinMessageBox MsgBox = new WinMessageBox("New User Record Deleted Successfully!", "User Main - Delete", 0, "SUCCESS");
                        MsgBox.ShowDialog();

                        SearchUsers(txt_UserNameId.Text);
                    }
                    else
                    {
                        WinMessageBox MsgBox = new WinMessageBox(errorMessage, "User Main - Delete", 0, "ERROR");
                        MsgBox.ShowDialog();
                        return;
                    }
                }
            }
            catch (Exception ex1)
            {
                WinMessageBox MsgBox = new WinMessageBox(ex1.Message.ToString(), "User Main - Delete", 0, "ERROR");
                MsgBox.ShowDialog();
            }
        }

        private void btn_SelUser_Click(object sender, RoutedEventArgs e)
        {
            string errorMessage = null;

            try
            {
                if (dg_UserMain.SelectedIndex > -1)
                {
                    User UserObj = new User();

                    if (ApplicationState.GetValue<User>("UserObj", out UserObj, out errorMessage) == true)
                        ApplicationState.DeleteValue("UserObj");

                    UserObj = (User)dg_UserMain.SelectedItem;

                    ApplicationState.SetValue("UserObj", UserObj);

                    Sale NewSaleObj;

                    if (ApplicationState.GetValue<Sale>("NewSaleObj", out NewSaleObj, out errorMessage) == true)
                    {
                        NewSaleObj.UserInfo = UserObj;

                        UCArgs = new UserCtrlArgs();
                        UCArgs.InvokingControlName = this.Name;
                        UCArgs.InvokingControlMode = this.Mode;
                        UCArgs.TargetControlName = "MySaleMainCtrl";
                        UCArgs.TargetControlMode = "A";
                        UCArgs.ControlTabName = this.ControlTabName;

                        ProcessEventArgs myargs = new ProcessEventArgs(UserMainWithCustomArgsEvent, UCArgs.InvokingControlName, UCArgs.InvokingControlMode, UCArgs.TargetControlName, UCArgs.TargetControlMode, null, null, UCArgs.ControlTabName);
                        RaiseEvent(myargs);
                    }
                    else if ((errorMessage != "") && (errorMessage != null))
                    {
                        WinMessageBox MsgBox = new WinMessageBox(errorMessage, "User Main - Select", 0, "ERROR");
                        MsgBox.ShowDialog();
                        return;
                    }
                }
            }
            catch (Exception ex1)
            {
                WinMessageBox MsgBox = new WinMessageBox(ex1.Message.ToString(), "User Main - Select", 0, "ERROR");
                MsgBox.ShowDialog();
            }
        }

        private void dg_UserMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((UserObjList != null) && (UserObjList.Count > 0) && (dg_UserMain.SelectedIndex > -1))
            {
                this.uc_UserDetails.Mode = "V";

                uc_UserDetails.dg_TxnResults.ItemsSource = null;
                uc_UserDetails.dg_TxnResults.Items.Clear();

                User SelectedUser = new User();
                SelectedUser = (User)UserObjList[dg_UserMain.SelectedIndex];
                this.uc_UserDetails.ClearControls();
                this.uc_UserDetails.DisplayUser(SelectedUser);

                #region Search Users

                    string errorMessage = null;

                    User UserObj = new User();
                    UserFactoryClient UserFactoryClient = new UserFactoryClient();


                    DateTime dt = DateTime.Now;

                    uc_UserDetails.dg_TxnResults.Columns.Clear();


                    Sale[] SaleObjArray = null;

                    if (UserFactoryClient.GetUserSaleDetails(out errorMessage, out SaleObjArray, SelectedUser.UserId) == true)
                    {
                        if (SaleObjArray != null)
                        {
                            UserObj.SalesDetails = SaleObjArray.ToList<Sale>();

                            uc_UserDetails.dg_TxnResults.ItemsSource = UserObj.SalesDetails;

                            DataGridTextColumn NewColumn;

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Sale Id";
                            NewColumn.Binding = new Binding("SalesId");
                            NewColumn.Width = 65;
                            uc_UserDetails.dg_TxnResults.Columns.Add(NewColumn);

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Sale Date";
                            NewColumn.Binding = new Binding("SaleDate");
                            NewColumn.Width = 125;
                            NewColumn.Binding.StringFormat = "{0:f}";
                            uc_UserDetails.dg_TxnResults.Columns.Add(NewColumn);

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Total Amt";
                            NewColumn.Binding = new Binding("TotalSaleAmount");
                            NewColumn.Binding.StringFormat = "{0:C2}";
                            NewColumn.Width = 90;
                            uc_UserDetails.dg_TxnResults.Columns.Add(NewColumn);

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Discount";
                            NewColumn.Binding = new Binding("TotalDiscountAmount");
                            NewColumn.Binding.StringFormat = "{0:C2}";
                            NewColumn.Width = 85;
                            uc_UserDetails.dg_TxnResults.Columns.Add(NewColumn);

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Tax";
                            NewColumn.Binding = new Binding("TotalTaxAmount");
                            NewColumn.Binding.StringFormat = "{0:C2}";
                            NewColumn.Width = 90;
                            uc_UserDetails.dg_TxnResults.Columns.Add(NewColumn);

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Final Amt";
                            NewColumn.Binding = new Binding("FinalSaleAmount");
                            NewColumn.Binding.StringFormat = "{0:C2}";
                            NewColumn.Width = 90;
                            uc_UserDetails.dg_TxnResults.Columns.Add(NewColumn);

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Amt Paid";
                            NewColumn.Binding = new Binding("PaidAmount");
                            NewColumn.Binding.StringFormat = "{0:C2}";
                            NewColumn.Width = 90;
                            uc_UserDetails.dg_TxnResults.Columns.Add(NewColumn);

                            NewColumn = new DataGridTextColumn();
                            NewColumn.Header = "Balance";
                            NewColumn.Binding = new Binding("BalanceAmount");
                            NewColumn.Binding.StringFormat = "{0:C2}";
                            NewColumn.Width = 90;
                            uc_UserDetails.dg_TxnResults.Columns.Add(NewColumn);
                        }
                    }
                    else
                        if ((errorMessage != "") && (errorMessage != null))
                        {
                            WinMessageBox MsgBox = new WinMessageBox(errorMessage, "Add User", 0, "ERROR");
                            MsgBox.ShowDialog();
                            return;
                        }
                #endregion



                this.uc_UserDetails.Visibility = Visibility.Visible;
            }
            else
            {
                this.uc_UserDetails.ClearControls();
                this.uc_UserDetails.Visibility = Visibility.Collapsed;
            }
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.Visibility == Visibility.Visible)
            {
                if (txt_UserNameId.Text != "")
                    SearchUsers(txt_UserNameId.Text);
                else
                    dg_UserMain.ItemsSource = "";
            }
        }

        private void dg_UserMain_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (dg_UserMain.SelectedIndex > -1)
                if (UserObjList != null)
                    if ((UserObjList.Count > 0) && (dg_UserMain.SelectedIndex > -1))
                    {
                        this.uc_UserDetails.ClearControls();
                        this.uc_UserDetails.DisplayUser((User)UserObjList[dg_UserMain.SelectedIndex]);
                        this.uc_UserDetails.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        this.uc_UserDetails.ClearControls();
                        this.uc_UserDetails.Visibility = Visibility.Collapsed;
                    }

        }

        private void uc_UserDetails_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
