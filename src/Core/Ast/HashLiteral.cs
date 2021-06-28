using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    public class HashLiteral : IExpression
    {
        public Token Token;
        public Dictionary<IExpression, IExpression> Pairs;
        public HashLiteral() { }
        public HashLiteral(Token token)
        {
            this.Token = token;
        }
        public string Inspect()
        {
            var output = new StringBuilder();
            output.Append("{");
            if (Pairs.Count > 0)
            {
                var pairs = new List<String>();
                foreach(KeyValuePair<IExpression, IExpression> kvp in Pairs)
                {
                    pairs.Add(String.Format("{0}:{1}", kvp.Key.Inspect(), kvp.Value.Inspect()));
                }
                output.Append(String.Join(",", pairs));
            }
            output.Append("}");
            return output.ToString();
        }
    }
}
