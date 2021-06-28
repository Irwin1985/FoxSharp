using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    public class ArrayObj : IObject
    {
        public List<IObject> Elements;
        public ObjectType Type()
        {
            return ObjectType.ARRAY_OBJ;
        }
        public string Inspect()
        {
            var output = new StringBuilder();
            output.Append("[");
            if (Elements.Count > 0){
                var items = new List<String>();
                foreach (var item in Elements)
                {
                    items.Add(item.Inspect());
                }
                output.Append(String.Join(",", items));
            }
            output.Append("]");
            return output.ToString();
        }
    }
}
