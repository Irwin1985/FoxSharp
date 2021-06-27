using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    public class Core
    {
        public void Run(string source)
        {
            try
            {
                FoxSharp.Scanner scanner = new FoxSharp.Scanner();
                scanner.ReadString(source);

                var tok = scanner.NextToken();
                while (tok.type != TokenType.EOF)
                {
                    System.Windows.Forms.MessageBox.Show(tok.ToString(), "FoxSharp Debbuger");
                    tok = scanner.NextToken();
                }
                Console.WriteLine(tok.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            //System.Windows.Forms.MessageBox.Show(source, "FoxSharp");
        }
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
