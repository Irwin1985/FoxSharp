using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    public class Identifier : IExpression
    {
        public Token Token;
        public string Value;
        public Identifier() {}
        public Identifier(Token token, string value)
        {
            this.Token = token;
            this.Value = value;
        }
        public string Inspect()
        {
            return Value;
        }
    }
}
