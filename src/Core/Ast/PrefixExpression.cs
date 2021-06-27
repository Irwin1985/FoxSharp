using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    class PrefixExpression : IExpression
    {
        public Token Token;
        public IExpression Right;
        public string Operator;
        public PrefixExpression() { }
        public PrefixExpression(Token token)
        {
            this.Token = token;
        }
        public PrefixExpression(Token token, string _operator)
        {
            this.Token = token;
            this.Operator = _operator;
        }
        public string Inspect()
        {
            return String.Format("{0}{1};", Operator, Right.Inspect());
        }
    }
}
