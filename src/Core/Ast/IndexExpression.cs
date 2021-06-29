using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    public class IndexExpression : IExpression
    {
        public Token Token;
        public IExpression Left;
        public IExpression Index;
        public IExpression Top; // nCharacters to fetch from substring. [Index:Top]
        public IndexExpression() { }
        public IndexExpression(Token token, IExpression left)
        {
            this.Token = token;
            this.Left = left;
        }
        public string Inspect()
        {
            if (Top != null){
                return String.Format("{0}[{1}:{2}]", Left.Inspect(), Index.Inspect(), Top.Inspect());
            } else{
                return String.Format("{0}[{1}]", Left.Inspect(), Index.Inspect());
            }
        }
        public NodeType Type()
        {
            return NodeType.INDEX;
        }
    }
}
