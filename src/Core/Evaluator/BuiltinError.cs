using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    class BuiltinError : IBuiltin
    {
        public IObject Run(List<IObject> args){
            string message = "";
            foreach (var obj in args){
                message += obj.Inspect() + "\n";
            }
            System.Windows.Forms.MessageBox.Show(message, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            return Evaluator.TRUE;
        }
        public ObjectType Type()
        {
            return ObjectType.BUILTIN;
        }
        public string Inspect()
        {
            return "error()";
        }
    }
}
