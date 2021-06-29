using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    public class Core
    {
        public string Run(string source)
        {
            try{
                FoxSharp.Scanner scanner = new FoxSharp.Scanner();
                scanner.ReadString(source);
                FoxSharp.Parser parser = new FoxSharp.Parser(scanner);
                var program = parser.ParseProgram();
                var errors = parser.GetErrors();
                if (errors.Count > 0){
                    var strError = String.Join("\n", errors);
                    System.Windows.Forms.MessageBox.Show(strError);
                    return strError;
                }
                FoxSharp.Environment globalEnv = new FoxSharp.Environment();
                var evaluated = Evaluator.Eval(program, globalEnv);
                if (evaluated != null){
                    return evaluated.Inspect();
                }
                return "unknown error";
            }catch (Exception e){
                System.Windows.Forms.MessageBox.Show(e.Message, "FoxSharp Error");
                return e.Message;
            }
        }
    }
}
