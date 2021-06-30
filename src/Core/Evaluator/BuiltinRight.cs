using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    class BuiltinRight : IBuiltin
    {
        public IObject Run(List<IObject> args)
        {
            if (args.Count != 2)
            {
                return Evaluator.NewError(String.Format("invalid arguments. got:{0}, want:2", args.Count));
            }
            if (args[0].Type() != ObjectType.STRING)
            {
                return Evaluator.NewError("first argument must be a STRING.");
            }
            if (args[1].Type() != ObjectType.NUMBER)
            {
                return Evaluator.NewError("second argument must be a NUMBER.");
            }
            string newString = ((StringObj)args[0]).Value;
            int index = (int)((NumberObj)args[1]).Value;
            var start = newString.Length - index;
            try{
                return new StringObj(newString.Substring(start, index));
            }catch (Exception e){
                return Evaluator.NewError(e.Message);
            }
        }
        public ObjectType Type()
        {
            return ObjectType.BUILTIN;
        }
        public string Inspect()
        {
            return "right()";
        }
    }
}
