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
        NULL_OBJ,
        ERROR_OBJ,
        INTEGER_OBJ,
        FLOAT_OBJ,
        BOOLEAN_OBJ,
        STRING_OBJ,
        RETURN_OBJ,
        FUNCTION_OBJ,
        BUILTIN_OBJ,
        ARRAY_OBJ,
        HASH_OBJ,
        COMPILED_FUNCTION_OBJ,
        CLOSURE_OBJ,
    }
}
