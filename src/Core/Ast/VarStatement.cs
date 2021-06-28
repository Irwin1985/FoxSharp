using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    public class VarStatement : IStatement
    {
        public Token Token;
        public Identifier Name;
        public IExpression Value;
        public VarStatement() { }
        public VarStatement(Token token)
        {
            this.Token = token;
        }
        public string Inspect()
        {
            return String.Format("var {0} = {1}", Name.Inspect(), Value.Inspect());
        }
        public NodeType Type()
        {
            return NodeType.VAR;
        }
    }
}
