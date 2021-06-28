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
        public SmtpLiteral() { }
        public SmtpLiteral(Token token){
            this.Token = token;
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
    }
}
