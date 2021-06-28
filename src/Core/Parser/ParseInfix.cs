using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    class ParseInfix : IInfixParse
    {
        public IExpression ParseInfixExpression(Parser p, IExpression Left)
        {
            var expression = new InfixExpression(p.curToken, p.curToken.literal, Left);
            var precedence = p.CurPrecedence();
            p.NextToken();

            expression.Right = p.ParseExpression(precedence);
            return expression;
        }
    }
}
