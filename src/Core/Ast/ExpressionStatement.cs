using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    class ExpressionStatement : IStatement
    {
        public IStatement Expression;
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
    }
}
