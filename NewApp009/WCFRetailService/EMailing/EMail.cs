using System;
using System.Net.Mail;
using System.Net;
using WCFRetailService;



namespace NewApp.BusinessTier.EMailing
{
    public class EMail
    {
        private string MailTo;
        private string MailFrom;      
        private string MailFromPass;
        private string Message;
        //private string Host;

        public EMail(string _MailFrom, string _MailFromPass, string _MailTo, string _Message)
        {
            MailTo = _MailTo;
            MailFrom = _MailFrom;
            MailFromPass = _MailFromPass;
            Message = _Message;
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
                mail.Body = Message;

                SmtpClient Smtp_Client = new SmtpClient("smtp.gmail.com", 587);//it sends mail just using Gmail
                //SmtpClient Smtp_Client = new SmtpClient("smtp.hotmail.com", 587);
                Smtp_Client.EnableSsl = true;
                Smtp_Client.Credentials = new NetworkCredential(MailFrom, MailFromPass); ;
                //Smtp_Client.Send(mail);
                
                Smtp_Client.SendCompleted += new SendCompletedEventHandler(smtp_SendCompleted);
                Smtp_Client.SendAsync(mail, null);
                //Smtp_Client.SendAsyncCancel(); 



                //if (BusinessTierState.GetValue<string>("ReturnMessage", out ReturnMessage, out errorMessage) == true)
                //{
                //    if (errorMessage == null)
                //        retval = true;
                //}
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