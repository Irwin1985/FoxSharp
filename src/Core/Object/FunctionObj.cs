using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    public class FunctionObj : IObject
    {
        public List<Identifier> Parameters;
        public BlockStatement Body;
        public Environment Env;

        public FunctionObj() { }
        public ObjectType Type()
        {
            return ObjectType.FUNCTION_OBJ;
        }
        public string Inspect()
        {
            return "fn!";
        }
    }
}
