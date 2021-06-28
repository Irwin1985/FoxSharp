using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    public class ErrorObj : IObject
    {
        public string Message;
        public ErrorObj() { }
        public ErrorObj(string message)
        {
            this.Message = message;
        }
        public ObjectType Type()
        {
            return ObjectType.ERROR_OBJ;
        }
        public string Inspect()
        {
            return Message;
        }
    }
}
