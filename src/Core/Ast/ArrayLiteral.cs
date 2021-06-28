using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    public class ArrayLiteral : IExpression
    {
        public Token Token;
        public List<IExpression> Elements;
        public ArrayLiteral() { }
        public ArrayLiteral(Token token)
        {
            this.Token = token;
        }
        public string Inspect()
        {
            var output = new StringBuilder();
            output.Append("[");
            if (Elements.Count > 0)
            {
                var items = new List<String>();
                foreach(var element in Elements)
                {
                    items.Add(element.Inspect());
                }
                output.Append(String.Join(",", items));
            }
            output.Append("]");
            return output.ToString();
        }
    }
}
