using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    public class FoxSharpClass
    {
        public void sendEmail()
        {
            var smtpClient = new System.Net.Mail.SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new System.Net.NetworkCredential("rodriguez.irwin@gmail.com", "woxihuanchange1985"),
                EnableSsl = true,
            };
            smtpClient.Send("rodriguez.irwin@gmail.com", "getiang2012@gmail.com", "el cocoyer", "hola cocoyerito! juega conmigo no seas malito!");
        }
    }
}
