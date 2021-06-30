using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    class BuiltinDownload : IBuiltin
    {
        public IObject Run(List<IObject> args){
            if (args.Count != 2){
                return Evaluator.NewError(String.Format("unexpected argument. got:{0}, want:2", args.Count));
            }
            if (args[0].Type() != ObjectType.STRING){
                return Evaluator.NewError(String.Format("invalid data type: {0}", args[0].Type()));
            }
            var urlPath = ((StringObj)args[0]).Value;
            var fileName = ((StringObj)args[1]).Value;
            try
            {
                System.Net.WebClient webClient = new System.Net.WebClient();
                webClient.DownloadFile(urlPath, fileName);
                return (System.IO.File.Exists(fileName)) ? Evaluator.TRUE : Evaluator.FALSE;
            }catch (Exception e){
                return Evaluator.NewError(e.Message);
            }
        }
        public ObjectType Type(){
            return ObjectType.BUILTIN;
        }
        public string Inspect(){
            return "download()";
        }
    }
}
