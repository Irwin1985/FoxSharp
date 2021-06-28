using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    public class IfExpression : IExpression
    {
        public Token Token;
        public IExpression Condition;
        public BlockStatement Consequence;
        public BlockStatement Alternative;
        public IfExpression() { }
        public IfExpression(Token token)
        {
            this.Token = token;
        }
        public string Inspect()
        {
            var output = new StringBuilder();
            output.Append(String.Format("if({0}){1}", Condition.Inspect(), Consequence.Inspect()));
            if (Alternative != null)
            {
                output.Append(String.Format(" else{0} ", Alternative.Inspect()));
            }
            return output.ToString();
        }
        public NodeType Type()
        {
            return NodeType.IF;
        }
    }
}
