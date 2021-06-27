using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    class InfixExpression : IExpression
    {
        public Token Token;
        public IExpression Left;
        public string Operator;
        public IExpression Right;
        public InfixExpression() { }
        public InfixExpression(Token token)
        {
            this.Token = token;
        }
        public InfixExpression(Token token, string _operator)
        {
            this.Token = token;
            this.Operator = _operator;
        }
        public string Inspect()
        {
            return String.Format("({0}{1}{2})", Left.Inspect(), Operator, Right.Inspect());
        }

    }
}
