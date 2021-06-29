using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    class BuiltinInfo : IBuiltin
    {
        public IObject Run(List<IObject> args){
            string message = "";
            foreach(var obj in args){
                message += obj.Inspect() + "\n";
            }
            System.Windows.Forms.MessageBox.Show(message, "Info", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            return Evaluator.TRUE;
        }
        public ObjectType Type()
        {
            return ObjectType.BUILTIN;
        }
        public string Inspect()
        {
            return "info()";
        }
    }
}
