using NewApp.UI.Windows;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;


namespace NewApp.UI.UserControls
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ComposeMailCtrl : UserControl
    {

        public delegate void NotifyUserControl(object sender, UserCtrlArgs UserCtrlArgs);
        public event NotifyUserControl NotifyedUserControl;
        string _mode;
        string _invokingCtrlMode;
        string _invokingCtrlName;


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

        public ComposeMailCtrl()
        {
            InitializeComponent();
        }

        private void sendButtonClick(object sender, RoutedEventArgs e)
        {
            WinMessageBox WinMsgBox;
            
            string ReturnMessage = null;
            string errorMessage = null;


            if (txt_mailTo.Text.Length <= 0)
            {
                WinMsgBox = new WinMessageBox("Please provide EMailId of Recepient", "Send EMail", 0, "ERROR");
                WinMsgBox.ShowDialog();
                return;
            }

            if (txt_mailSubject.Text.Length <= 0)
            {
                WinMsgBox = new WinMessageBox("Please provide Subject of EMail", "Send EMail", 0, "ERROR");
                WinMsgBox.ShowDialog();
                return;
            }

            string bodyMail = new TextRange(mailBody.Document.ContentStart, mailBody.Document.ContentEnd).Text;

            if (bodyMail.ToString().Length <= 0)
            {
                WinMsgBox = new WinMessageBox("Please provide Body of EMail", "Send EMail", 0, "ERROR");
                WinMsgBox.ShowDialog();
                return;
            }

            List<string> _AttachmentsList = new List<string>();

            foreach (Attachment file in dataGrid1.Items)
            {
                _AttachmentsList.Add(file.FileName.ToString());
            }

            EMail sm = new EMail("annapragada.k@hcl.com", "Avkm@759", txt_mailTo.Text, txt_mailSubject.Text, bodyMail, _AttachmentsList);

            if (sm.SendMail(out ReturnMessage, out errorMessage) == false)
            {
                if ((ReturnMessage != null) && (errorMessage != null))
                {
                    WinMsgBox = new WinMessageBox(ReturnMessage + " " + errorMessage, "Send EMail", 0, "ERROR");
                    WinMsgBox.ShowDialog();
                    return;
                }
                else if (ReturnMessage != null)
                {
                    WinMsgBox = new WinMessageBox(ReturnMessage + " " + errorMessage, "Send EMail", 0, "INFORMATION");
                    WinMsgBox.ShowDialog();
                    return;
                }
                else if (errorMessage != null)
                {
                    WinMsgBox = new WinMessageBox(ReturnMessage + " " + errorMessage, "Send EMail", 0, "ERROR");
                    WinMsgBox.ShowDialog();
                    return;
                }
            }
            else
            {
                WinMsgBox = new WinMessageBox("EMail Successfully Sent!", "Send EMail", 0, "SUCCESS");
                WinMsgBox.ShowDialog();
                return;
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            if (MessageBox.Show("Are you sure you want to cancel?", "Send Mail", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                UserCtrlArgs Args = new UserCtrlArgs();
                Args.TargetControlName = "SalesMainCtrl";
                Args.TargetControlMode = "";
                Args.InvokingControlName = this.Name;
                Args.InvokingControlMode = this.Mode;

                this.NotifyedUserControl(this, Args);
            }
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            WinMessageBox WinMsgBox;
            
            var fileDialog = new System.Windows.Forms.OpenFileDialog();
            var result = fileDialog.ShowDialog();
            switch (result)
            {
                case System.Windows.Forms.DialogResult.OK:
                    var file = fileDialog.FileName;
                    var data = new Attachment { FileName = file };

                    dataGrid1.Items.Add(data);

                    WinMsgBox = new WinMessageBox(file.ToString(), "Selected File", 0, "INFORMATION");
                    WinMsgBox.ShowDialog();
                    break;
                case System.Windows.Forms.DialogResult.Cancel:
                default:
                    //TxtFile.Text = null;
                    //TxtFile.ToolTip = null;
                    break;
            }

        }
    }

    public class Attachment
    {
        public string FileName { get; set; }
   }
}
