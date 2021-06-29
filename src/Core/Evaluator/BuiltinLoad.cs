using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    public class BuiltinLoad : IBuiltin
    {
        public IObject Run(List<IObject> args)
        {
            if (args.Count != 1){
                return Evaluator.NewError(String.Format("unexpected argument. got:{0}, want:1", args.Count));
            }
            if (args[0].Type() != ObjectType.STRING){
                return Evaluator.NewError(String.Format("invalid data type: {0}", args[0].Type()));
            }
            var fileName = ((StringObj)args[0]).Value;
            try{
                return new StringObj(System.IO.File.ReadAllText(fileName));
            } catch(Exception e){
                return Evaluator.NewError(e.Message);
            }
        }
        public ObjectType Type()
        {
            return ObjectType.BUILTIN;
        }
        public string Inspect()
        {
            return "load()";
        }
    }
}
