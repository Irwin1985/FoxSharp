using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    public class FunctionLiteral : IExpression
    {
        public Token Token;
        public string Name;
        public List<Identifier> Parameters;
        public BlockStatement Body;
        public bool LastParamArray = false;
        public FunctionLiteral() { }
        public FunctionLiteral(Token token)
        {
            this.Token = token;
        }
        public string Inspect()
        {
            var output = new StringBuilder();
            output.Append("fn(");
            if (Parameters.Count > 0)
            {
                List<String> parameters = new List<string>();
                foreach (var param in Parameters)
                {
                    parameters.Add(param.Inspect());
                }
                output.Append(String.Join(",", parameters));
            }
            output.Append(")");
            output.Append(Body.Inspect());

            return output.ToString();
        }
        public NodeType Type()
        {
            return NodeType.FUNCTION;
        }
    }
}
