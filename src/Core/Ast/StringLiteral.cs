using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    public class StringLiteral : IExpression
    {
        public Token Token;
        public string Value;
        public StringLiteral() { }
        public StringLiteral(Token token, string value){
            this.Token = token;
            this.Value = value;
        }
        public string Inspect()
        {
            return String.Format("\"{0}\"", Value);
        }
        public NodeType Type()
        {
            return NodeType.STRING;
        }
    }
}
