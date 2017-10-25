using NewApp.Themes;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace NewApp.UI.UserControls
{
    /// <summary>
    /// Interaction logic for PreferencesCtrl.xaml
    /// </summary>
    public partial class PreferencesCtrl : UserControl
    {
        public delegate void NotifyUserControl(object sender, UserCtrlArgs UserCtrlArgs);
        //public event NotifyUserControl NotifyedUserControl;
        string _mode;
        string _invokingCtrlMode;
        string _invokingCtrlName;

        public PreferencesCtrl()
        {
            InitializeComponent();

            string[] Themes = ThemeManager.GetThemes();
            cmb_Theme.ItemsSource = ThemeManager.GetThemes();
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

        private void btn_Commit_Click(object sender, RoutedEventArgs e)
        {
            ResourceDictionary resourceDictionary = new ResourceDictionary();

            int theme = ((ComboBox)cmb_Theme).SelectedIndex;
            string themeName = ((ComboBox)cmb_Theme).SelectedItem.ToString();
            string UriPath;

            Assembly.LoadFrom(@"NewApp.Themes.dll");
            ResourceDictionary rd = new ResourceDictionary();

            UriPath = "/NewApp.Themes;component/" + themeName + "/Theme.xaml";
            rd.Source = new Uri(UriPath);

            //Application.Current.ApplyTheme(cmb_Theme.SelectedItem.ToString());
            Application.Current.Resources.Clear();
            Application.Current.Resources = resourceDictionary;

        }

        private void Btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;

            CloseThisWindow();
        }

        private void CloseThisWindow()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.Name == "WndPreferences")
                {
                    window.Close();
                    break;
                }
            }
        }
    }
}
