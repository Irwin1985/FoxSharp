using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    class BlockStatement : IStatement
    {
        public List<IStatement> Statements;
        public Token Token;
        public BlockStatement() { }
        public BlockStatement(Token token)
        {
            this.Token = token;
        }
        public string Inspect()
        {
            var output = new StringBuilder();
            if (Statements.Count > 0)
            {
                foreach (var stmt in Statements)
                {
                    output.Append(stmt.Inspect());
                }
            }
            return output.ToString();
        }
    }
}
