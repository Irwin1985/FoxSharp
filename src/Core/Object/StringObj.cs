using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    public class StringObj : IObject
    {
        public string Value;
        public StringObj() { }
        public StringObj(string value)
        {
            this.Value = value;
        }
        public ObjectType Type()
        {
            return ObjectType.STRING_OBJ;
        }
        public string Inspect()
        {
            return Value;
        }
    }
}
