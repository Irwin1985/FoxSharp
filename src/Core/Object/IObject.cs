using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    public interface IObject{
        ObjectType Type();
        string Inspect();
    }
    public enum ObjectType
    {
        NULL,
        ERROR,
        NUMBER,
        BOOLEAN,
        STRING,
        RETURN,
        FUNCTION,
        BUILTIN,
        ARRAY,
        HASH,
        SMTP,
    }
}
