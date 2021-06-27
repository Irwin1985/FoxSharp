using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    class IndexExpression : IExpression
    {
        public Token Token;
        public IExpression Left;
        public IExpression Index;
        public IndexExpression() { }
        public IndexExpression(Token token, IExpression left)
        {
            this.Token = token;
            this.Left = left;
        }
        public string Inspect()
        {
            return String.Format("{0}[{1}]", Left.Inspect(), Index.Inspect());
        }
    }
}
