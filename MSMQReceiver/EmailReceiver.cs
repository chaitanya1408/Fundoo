using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace MSMQReceiver
{
    public class EmailReceiver
    {
        
        public  void Email(string email,string token)
        {
            const string QueuePath = @".\Private$\EmailQueue";
            MessageQueue messageQueue = new MessageQueue(QueuePath);
            string url = "";
            messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            MailMessage mail = new MailMessage();
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.UseDefaultCredentials = true;
            mail.From = new MailAddress(email); 
            mail.To.Add(email);
            mail.Subject = "Link for ResttingPassword";
            mail.Body = "click on link  " + url + "  to reset password  " + token;
            smtpClient.Credentials = new System.Net.NetworkCredential("chaitanya95vaidya@gmail.com", "chaitanya_v_95");
            smtpClient.Send(mail);
            Console.WriteLine("Mail for Resetting password is been sent. Please check your given Email Account");
        }
    }
}
