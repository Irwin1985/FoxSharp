using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    public class FloatObj : IObject
    {
        public double Value;
        public FloatObj() { }
        public FloatObj(double value)
        {
            this.Value = value;
        }
        public ObjectType Type()
        {
            return ObjectType.FLOAT_OBJ;
        }
        public string Inspect()
        {
            return Value.ToString();
        }
    }
}
