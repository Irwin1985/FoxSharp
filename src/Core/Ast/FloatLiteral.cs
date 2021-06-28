using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    public class FloatLiteral : IExpression
    {
        public Token Token;
        public double Value;
        public FloatLiteral() { }
        public FloatLiteral(Token token, double value){
            this.Token = token;
            this.Value = value;
        }
        public string Inspect(){
            return Value.ToString();
        }
    }
}
