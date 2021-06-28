using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    public class NullObj : IObject
    {
        public ObjectType Type()
        {
            return ObjectType.NULL_OBJ;
        }
        public string Inspect()
        {
            return "null";
        }
    }
}
