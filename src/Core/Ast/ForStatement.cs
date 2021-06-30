using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    class ForStatement : IStatement
    {
        public Token Token;
        public Identifier IndexOrKey;
        public Identifier Value;
        public IExpression Source;
        public BlockStatement Body;
        public ForStatement() { }
        public ForStatement(Token token)
        {
            this.Token = token;
        }
        public NodeType Type()
        {
            return NodeType.FOR;
        }
        public string Inspect()
        {
            var output = new StringBuilder();
            
            output.Append("for(");
            if (IndexOrKey != null){
                output.Append(String.Format("{0},{1}", IndexOrKey.Inspect(), Value.Inspect()));
            } else{
                output.Append(Value.Inspect());
            }
            output.Append(" in ");
            output.Append(Source.Inspect());
            output.Append(")");
            output.Append(Body.Inspect());

            return output.ToString();
        }
    }
}
