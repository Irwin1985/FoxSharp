using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    class WhileStatement : IStatement
    {
        public Token Token;
        public IExpression Condition;
        public BlockStatement Body;
        public WhileStatement() { }
        public WhileStatement(Token token){
            this.Token = token;
        }
        public NodeType Type()
        {
            return NodeType.WHILE;
        }
        public string Inspect()
        {
            return "while";
        }
    }
}
