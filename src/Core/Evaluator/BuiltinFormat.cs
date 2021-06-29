using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    class BuiltinFormat : IBuiltin
    {
        public IObject Run(List<IObject> args)
        {
            if (args.Count == 0){
                return Evaluator.NewError("expected arguments.");
            }
            if (args[0].Type() != ObjectType.STRING){
                return Evaluator.NewError("first argument must be a STRING.");
            }
            var mainString = ((StringObj)args[0]).Value;
            var index = 0;
            if (args.Count == 2 && args[1].Type() == ObjectType.ARRAY){
                var arr = (ArrayObj)args[1];
                index = 0;
                foreach(var arg in arr.Elements){
                    var hint = "{" + index.ToString() + "}";
                    if (mainString.Contains(hint)){
                        mainString = mainString.Replace(hint, arg.Inspect());
                    }
                    index += 1;
                }
            } else{
                index = -1;
                foreach(var arg in args){
                    index += 1;
                    if (index == 0) continue; // skip the first argument.
                    var hint = "{" + (index-1).ToString() + "}";
                    if (mainString.Contains(hint)){
                        mainString = mainString.Replace(hint, arg.Inspect());
                    }
                }
            }
            return new StringObj(mainString);
        }
        public ObjectType Type()
        {
            return ObjectType.BUILTIN;
        }
        public string Inspect()
        {
            return "format()";
        }
    }
}
