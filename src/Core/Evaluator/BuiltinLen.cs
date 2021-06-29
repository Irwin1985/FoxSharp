using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    class BuiltinLen : IBuiltin
    {
        public IObject Run(List<IObject> args){
            if (args.Count != 1){
                return Evaluator.NewError(String.Format("unexpected argument. got:{0}, want:1", args.Count));
            }
            switch (args[0].Type())
            {
                case ObjectType.STRING:
                    return new NumberObj(((StringObj)args[0]).Value.Length);
                case ObjectType.ARRAY:
                    return new NumberObj(((ArrayObj)args[0]).Elements.Count);
                case ObjectType.HASH:
                    return new NumberObj(((HashObj)args[0]).Pairs.Count);
                default:
                    return Evaluator.NewError(String.Format("invalid data type: {0}", args[0].Type()));
            }
        }
        public ObjectType Type()
        {
            return ObjectType.BUILTIN;
        }
        public string Inspect()
        {
            return "len()";
        }
    }
}
