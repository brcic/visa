using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Visa.Domain;

namespace Visa.Service
{
    public class SendService
    {
        public static bool SendMail(eMail mail)
        {
            try
            {

                MailMessage myEmail = new MailMessage();
                myEmail.From = new MailAddress(mail.fromMail);
                myEmail.To.Add(mail.toMail);
                myEmail.Subject = mail.mTitle;
                myEmail.IsBodyHtml = true;
                myEmail.Body = mail.mBody;
                myEmail.BodyEncoding = Encoding.UTF8;
                myEmail.Priority = MailPriority.Normal;
                SmtpClient smtp = new SmtpClient();
                smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtp.Credentials = new System.Net.NetworkCredential(mail.userName, mail.uPassword);
                if (string.IsNullOrEmpty(mail.uHost))
                    smtp.Host = "smtp.exmail.qq.com";//host;smtp.exmail.qq.com
                else
                    smtp.Host = mail.uHost;
                smtp.Port = 25;
                smtp.EnableSsl = false;
                smtp.Send(myEmail);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
