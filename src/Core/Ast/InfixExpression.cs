using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    public class InfixExpression : IExpression
    {
        public Token Token;
        public IExpression Left;
        public string Operator;
        public IExpression Right;
        public InfixExpression() { }
        public InfixExpression(Token token)
        {
            this.Token = token;
        }
        public InfixExpression(Token token, string _operator, IExpression left)
        {
            this.Token = token;
            this.Operator = _operator;
            this.Left = left;
        }
        public string Inspect()
        {
            if (Operator == "."){
                return String.Format("({0}{1}{2})", Left.Inspect(), Operator, Right.Inspect());
            }
            return String.Format("({0} {1} {2})", Left.Inspect(), Operator, Right.Inspect());
        }
        public NodeType Type()
        {
            return NodeType.INFIX;
        }

    }
}
