using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    public class FloatLiteral : IExpression
    {
        public Token Token;
        public float Value;
        public FloatLiteral() { }
        public FloatLiteral(Token token)
        {
            this.Token = token;
        }
        public string Inspect()
        {
            return Value.ToString();
        }
    }
}
