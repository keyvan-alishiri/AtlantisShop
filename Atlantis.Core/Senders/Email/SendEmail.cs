using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace AtlantisShop.Core.Senders.Email
{
    public class SendEmail
    {
        public static void Send(string Subject,string Body,string UserEmail)
        {
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            System.Net.Mail.MailAddress address = new System.Net.Mail.MailAddress("k.alishiri@gmail.com", "فروشگاه اینترنتی", System.Text.Encoding.UTF8);

            mail.From = address;
            mail.Sender = address;
            mail.ReplyToList.Add(address.Address);

            //به چه کسي
            System.Net.Mail.MailAddress to = new System.Net.Mail.MailAddress(UserEmail, Subject, System.Text.Encoding.UTF8);
            mail.To.Add(to);

            
            //بدنه و موضوع
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.Body = Body;
            mail.Subject = Subject;
            mail.IsBodyHtml = true;
            mail.Priority = System.Net.Mail.MailPriority.High;
            mail.DeliveryNotificationOptions = System.Net.Mail.DeliveryNotificationOptions.None;

            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(address.Address, "z4x3c2v1")


            };

            smtp.Timeout = 100000;
            smtp.EnableSsl = true;
            smtp.Send(mail);

           
        }
    }
}
