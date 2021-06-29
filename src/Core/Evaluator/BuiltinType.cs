using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    public class BuiltinType : IBuiltin
    {
        public IObject Run(List<IObject> args)
        {
            if (args.Count != 1){
                return Evaluator.NewError(String.Format("unexpected argument. got:{0}, want:1", args.Count));
            }
            switch (args[0].Type()){
                case ObjectType.NUMBER:
                    return new StringObj("N");
                case ObjectType.STRING:
                    return new StringObj("C");
                case ObjectType.NULL:
                    return new StringObj("X");
                case ObjectType.ARRAY:
                    return new StringObj("A");
                case ObjectType.HASH:
                    return new StringObj("H");
                case ObjectType.FUNCTION:
                    return new StringObj("F");
                case ObjectType.BUILTIN:
                    return new StringObj("B");
                default:
                    return new StringObj("U");
            }            
        }
        public ObjectType Type()
        {
            return ObjectType.BUILTIN;
        }
        public string Inspect()
        {
            return "type()";
        }
    }
}
