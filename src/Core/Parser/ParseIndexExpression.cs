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
            if (p.CurTokenIs(TokenType.COLON))
            {
                index.Index = new NumberLiteral(new Token(), 0.0);
            } else{
                index.Index = p.ParseExpression(p.LOWEST);
            }
            if (p.CurTokenIs(TokenType.COLON)){
                p.NextToken(); // skip ':'
                if (!p.CurTokenIs(TokenType.RBRACKET)){
                    index.Top = p.ParseExpression(p.LOWEST);
                }else{
                    index.Top = new NumberLiteral(new Token(), 0.0);
                }
            }
            p.Expect(TokenType.RBRACKET);

            return index;
        }
    }
}
