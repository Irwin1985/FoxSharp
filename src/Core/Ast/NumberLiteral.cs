using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    public class NumberLiteral : IExpression
    {
        public Token Token;
        public double Value;
        public NumberLiteral() { }
        public NumberLiteral(Token token, double value)
        {
            this.Token = token;
            this.Value = value;
        }        
        public NodeType Type()
        {
            return NodeType.NUMBER;
        }
        public string Inspect()
        {
            return Value.ToString();
        }
    }
}
