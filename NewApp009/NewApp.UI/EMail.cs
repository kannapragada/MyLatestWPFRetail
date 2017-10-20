using System;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;
using System.Collections.Generic;
using WCFRetailService;



namespace NewApp.UI.UserControls
{
    public class EMail
    {
        private string MailTo;
        private string MailFrom;      
        private string MailFromPass;
        private string Subject;
        private string Message;
        private List<string> MyAttachmentsList;
        //private string Host;

        public EMail(string _MailFrom, string _MailFromPass, string _MailTo, string _Subject, string _Message, List<string> _MyAttachmentsList)
        {
            MailTo = _MailTo;
            MailFrom = _MailFrom;
            MailFromPass = _MailFromPass;
            Subject = _Subject;
            Message = _Message;
            MyAttachmentsList = _MyAttachmentsList;
            //Host = _Host;
        }

        public bool SendMail(out string ReturnMessage, out string errorMessage)
        {
            errorMessage = null;
            ReturnMessage = null;
            
            bool retval = false;

            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(MailFrom);
                mail.To.Add(new MailAddress(MailTo));
                mail.Subject = Subject;
                mail.Body = Message;

                for(int i=0; i<MyAttachmentsList.Count; i++)
                {
                    System.Net.Mail.Attachment data = new System.Net.Mail.Attachment(MyAttachmentsList[i].ToString());
                    mail.Attachments.Add(data);
                }

                SmtpClient Smtp_Client = new SmtpClient();//it sends mail just using Gmail
                Smtp_Client.Host = "chn-hclt-olk.hclt.corp.hcl.in";
                Smtp_Client.Port = 25;
                Smtp_Client.UseDefaultCredentials = true;
                //Smtp_Client.Send(mail);

                Smtp_Client.SendCompleted += new SendCompletedEventHandler(smtp_SendCompleted);
                Smtp_Client.SendAsync(mail, null);
                //Smtp_Client.SendAsyncCancel(); 

                //if (BusinessTierState.GetValue<string>("ReturnMessage", out ReturnMessage, out errorMessage) == true)
                //{
                //    if (errorMessage == null)
                //        retval = true;
                //}
                retval = true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message.ToString();
            }

            return retval;
        }

        private void smtp_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            //if (e.Error != null)
            //    BusinessTierState.SetValue("ReturnMessage", e.Error.Message);
            //else if (e.Cancelled)
            //    BusinessTierState.SetValue("ReturnMessage", "Cancelled sending!");
            //else
            //    BusinessTierState.SetValue("ReturnMessage", "Mail successfully sent!");
        }
    }
}