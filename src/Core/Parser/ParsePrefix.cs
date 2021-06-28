using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    class ParsePrefix : IPrefixParse
    {
        public IExpression ParsePrefixExpression(Parser p)
        {
            var prefix = new PrefixExpression(p.curToken, p.curToken.literal);
            p.Expect(p.curToken.type);

            prefix.Right = p.ParseExpression(p.PREFIX);

            return prefix;
        }
    }
}
