using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    class ParseGroupedExpression : IPrefixParse
    {
        public IExpression ParsePrefixExpression(Parser p)
        {
            p.NextToken(); // skip '('
            var exp = p.ParseExpression(p.LOWEST);
            p.Expect(TokenType.RPAREN);
            return exp;
        }
    }
}
