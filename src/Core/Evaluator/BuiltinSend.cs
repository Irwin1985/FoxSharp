using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    class BuiltinSend : IBuiltin
    {
        public IObject Run(List<IObject> args){
            if (args.Count != 1){
                return Evaluator.NewError(String.Format("unexpected argument. got:{0}, want:1", args.Count));
            }
            if (args[0].Type() != ObjectType.SMTP){
                return Evaluator.NewError(String.Format("invalid data type: {0}", args[0].Type()));
            }
            
            var smtpObj = (SmtpObj)args[0];
            var provider = "";
            var port = 0.0;
            var user = "";
            var pass = "";
            var from = "";
            var toList = new List<String>();
            var attachList = new List<String>();
            var subject = "";
            var body = "";
            var htmlBody = false;

            if (smtpObj.Properties.ContainsKey("provider")){
                provider = smtpObj.Properties["provider"].Inspect();
            }
            if (smtpObj.Properties.ContainsKey("port")){
                port = ((NumberObj)smtpObj.Properties["port"]).Value;
            }
            if (smtpObj.Properties.ContainsKey("user")){
                user = smtpObj.Properties["user"].Inspect();
            }
            if (smtpObj.Properties.ContainsKey("pass")){
                pass = smtpObj.Properties["pass"].Inspect();
            }
            if (smtpObj.Properties.ContainsKey("from")){
                from = smtpObj.Properties["from"].Inspect();
            }
            if (smtpObj.Properties.ContainsKey("to")){
                var toObj = smtpObj.Properties["to"];
                if (toObj.Type() == ObjectType.ARRAY){
                    var arrTo = (ArrayObj)toObj;
                    foreach(var str in arrTo.Elements){
                        toList.Add(str.Inspect());
                    }
                } else{
                    toList.Add(toObj.Inspect());
                }
            }
            if (smtpObj.Properties.ContainsKey("files")){
                var attachObj = smtpObj.Properties["files"];
                if (attachObj.Type() == ObjectType.ARRAY){
                    var arrTo = (ArrayObj)attachObj;
                    foreach (var str in arrTo.Elements){
                        attachList.Add(str.Inspect());
                    }
                }else{
                    attachList.Add(attachObj.Inspect());
                }
            }
            if (smtpObj.Properties.ContainsKey("subject")){
                subject = smtpObj.Properties["subject"].Inspect();
            }
            if (smtpObj.Properties.ContainsKey("body")){
                body = smtpObj.Properties["body"].Inspect();
            } else if(smtpObj.Properties.ContainsKey("html")){
                body = smtpObj.Properties["html"].Inspect();
                htmlBody = true;
            }
            try
            {
                var smtpClient = new System.Net.Mail.SmtpClient(provider)
                {
                    Port = (int)port,
                    Credentials = new System.Net.NetworkCredential(user, pass),
                    EnableSsl = true,
                };
                var mail = new System.Net.Mail.MailMessage();
                mail.From = new System.Net.Mail.MailAddress(from);
                
                // add email addresses
                foreach(var toStr in toList){
                    mail.To.Add(toStr);
                }
                mail.Subject = subject;
                
                // add attachments
                foreach (var fileName in attachList){
                    mail.Attachments.Add(new System.Net.Mail.Attachment(fileName));
                }

                mail.Body = body;
                mail.IsBodyHtml = htmlBody;

                smtpClient.Send(mail);
            }catch(Exception e){
                return Evaluator.NewError(e.Message);
            }
            return Evaluator.TRUE;
        }
        public ObjectType Type()
        {
            return ObjectType.BUILTIN;
        }
        public string Inspect()
        {
            return "send()";
        }
    }
}
