using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    class ParseIndexExpression : IParseInfix
    {
        public IExpression ParseInfixExpression(Parser p, IExpression left)
        {
            var index = new IndexExpression(p.curToken, left);
            
            p.NextToken(); // advance '['
            index.Index = p.ParseExpression(p.LOWEST);
            p.Expect(TokenType.RBRACKET);

            return index;
        }
    }
}
