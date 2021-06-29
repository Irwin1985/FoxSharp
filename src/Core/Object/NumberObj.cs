using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    public class NumberObj : IObject
    {
        public double Value;
        public NumberObj(){}
        public NumberObj(double value)
        {
            this.Value = value;
        }
        public ObjectType Type()
        {
            return ObjectType.NUMBER;
        }
        public string Inspect()
        {
            return Value.ToString().Replace(',','.');
        }
    }
}
