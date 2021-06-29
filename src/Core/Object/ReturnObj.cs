using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    public class ReturnObj : IObject
    {
        public IObject Value;
        public ReturnObj() { }
        public ReturnObj(IObject value)
        {
            this.Value = value;
        }
        public ObjectType Type()
        {
            return ObjectType.RETURN;
        }
        public string Inspect()
        {
            return Value.Inspect();
        }
    }
}
