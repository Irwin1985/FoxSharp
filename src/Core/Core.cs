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
            try{
                FoxSharp.Scanner scanner = new FoxSharp.Scanner();
                scanner.ReadString(source);
                FoxSharp.Parser parser = new FoxSharp.Parser(scanner);
                var program = parser.ParseProgram();
                var errors = parser.GetErrors();
                if (errors.Count > 0){
                    foreach (var msg in errors){
                        Console.WriteLine("Error: " + msg);
                    }
                }
                System.Windows.Forms.MessageBox.Show(program.Inspect(), "Respuesta del Parser");
            }catch (Exception e){
                System.Windows.Forms.MessageBox.Show(e.Message, "FoxSharp Error");
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
