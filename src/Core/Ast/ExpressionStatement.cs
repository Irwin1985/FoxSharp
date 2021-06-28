using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    public class ExpressionStatement : IStatement
    {
        public IExpression Expression;
        public Token Token;
        public ExpressionStatement() { }
        public ExpressionStatement(Token token)
        {
            this.Token = token;
        }
        public string Inspect()
        {
            return Expression.Inspect();
        }
        public NodeType Type()
        {
            return NodeType.EXPR_STMT;
        }
    }
}
