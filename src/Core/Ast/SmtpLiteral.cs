using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    public class SmtpLiteral : IExpression {
        public Token Token;
        public Dictionary<IExpression, IExpression> Properties;
        public string[] prop = {
            "provider", 
            "port",
            "user",
            "pass",
            "from",
            "to",
            "files",
            "subject",
            "body",
            "html",
        };
        public SmtpLiteral() { }
        public SmtpLiteral(Token token){
            this.Token = token;
        }
        public bool HasProperty(string key)
        {
            return prop.Contains(key);
        }
        public string Inspect(){
            var output = new StringBuilder();
            output.Append("smtp {\n");
            if (Properties.Count > 0){
                var props = new List<String>();
                foreach (KeyValuePair<IExpression, IExpression> kvp in Properties){
                    props.Add(String.Format("{0}:{1}\n", kvp.Key.Inspect(), kvp.Value.Inspect()));
                }
                output.Append(String.Join(",", props));
            }
            output.Append("}");
            return output.ToString();
        }
        public NodeType Type()
        {
            return NodeType.SMTP;
        }
    }
}
