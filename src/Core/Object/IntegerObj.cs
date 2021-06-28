using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    public class IntegerObj : IObject
    {
        int Value;
        public IntegerObj(){}
        public IntegerObj(int value)
        {
            this.Value = value;
        }
        public ObjectType Type()
        {
            return ObjectType.INTEGER_OBJ;
        }
        public string Inspect()
        {
            return Value.ToString();
        }
    }
}
