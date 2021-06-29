using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    class BuiltinQuestion : IBuiltin
    {
        public IObject Run(List<IObject> args)
        {
            string message = "";
            foreach (var obj in args){
                message += obj.Inspect() + "\n";
            }
            var result = System.Windows.Forms.MessageBox.Show(message, "Question", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question);
            if (result == System.Windows.Forms.DialogResult.Yes){
                return Evaluator.TRUE;
            }else{
                return Evaluator.FALSE;
            }
        }
        public ObjectType Type()
        {
            return ObjectType.BUILTIN;
        }
        public string Inspect()
        {
            return "question()";
        }
    }
}
