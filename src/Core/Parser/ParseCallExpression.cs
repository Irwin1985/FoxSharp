using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    class ParseCallExpression : IParseInfix
    {
        public IExpression ParseInfixExpression(Parser p, IExpression left)
        {
            var call = new CallExpression(p.curToken, left);
            call.Arguments = new List<IExpression>();
            p.NextToken(); // advance '('

            if (!p.CurTokenIs(TokenType.RPAREN)){
                call.Arguments = p.ParseExpressionList();
            }
            p.Expect(TokenType.RPAREN);

            return call;
        }
    }
}
