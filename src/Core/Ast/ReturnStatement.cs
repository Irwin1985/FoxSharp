using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    public class ReturnStatement : IStatement
    {
        public Token Token;
        public IExpression ReturnValue;
        public ReturnStatement() { }
        public ReturnStatement(Token token)
        {
            this.Token = token;
        }
        public string Inspect()
        {
            return String.Format("return {0}", ReturnValue.Inspect());
        }
        public NodeType Type()
        {
            return NodeType.RETURN;
        }
    }
}
