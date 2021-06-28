using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    public class Program : IStatement
    {
        public List<IStatement> Statements;
        private Token Token;
        public Program() { }
        public Program(Token token)
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
                    output.Append(stmt.Inspect() + ";");
                }
            }
            return output.ToString();
        }
        public NodeType Type()
        {
            return NodeType.PROGRAM;
        }
    }
}
