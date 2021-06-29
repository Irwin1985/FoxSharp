using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    public class BooleanObj : IObject
    {
        public bool Value;
        public BooleanObj() { }
        public BooleanObj(bool value)
        {
            this.Value = value;
        }
        public ObjectType Type()
        {
            return ObjectType.BOOLEAN;
        }
        public string Inspect()
        {
            return Value ? "true" : "false";
        }
        
    }
}
