using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    class BuiltinAlltrim : IBuiltin
    {
        public IObject Run(List<IObject> args)
        {
            if (args.Count != 1){
                return Evaluator.NewError(String.Format("unexpected argument. got:{0}, want:1", args.Count));
            }
            if (args[0].Type() != ObjectType.STRING){
                return Evaluator.NewError("argument must be a STRING.");
            }
            var newString = ((StringObj)args[0]).Value.TrimStart().TrimEnd();
            return new StringObj(newString);
        }
        public ObjectType Type()
        {
            return ObjectType.BUILTIN;
        }
        public string Inspect()
        {
            return "alltrim()";
        }
    }
}
