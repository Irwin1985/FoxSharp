using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    class BuiltinWarning : IBuiltin
    {
        public IObject Run(List<IObject> args)
        {
            string message = "";
            foreach (var obj in args)
            {
                message += obj.Inspect() + "\n";
            }
            System.Windows.Forms.MessageBox.Show(message, "Warning", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
            return Evaluator.TRUE;
        }
        public ObjectType Type()
        {
            return ObjectType.BUILTIN;
        }
        public string Inspect()
        {
            return "warning()";
        }
    }
}
