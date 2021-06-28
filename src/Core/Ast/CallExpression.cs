using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    public class CallExpression : IExpression
    {
        public Token Token;
        public IExpression Function;
        public List<IExpression> Arguments;
        public CallExpression() { }
        public CallExpression(Token token, IExpression function)
        {
            this.Token = token;
            this.Function = function;
        }
        public string Inspect()
        {
            var output = new StringBuilder();
            output.Append(Function.Inspect());
            output.Append("(");
            if (Arguments.Count > 0)
            {
                var args = new List<String>();
                foreach (var arg in Arguments)
                {
                    args.Add(arg.Inspect());
                }
                output.Append(String.Join(",", args));
            }
            output.Append(")");
            return output.ToString();
        }
        public NodeType Type()
        {
            return NodeType.CALL;
        }
    }
}
