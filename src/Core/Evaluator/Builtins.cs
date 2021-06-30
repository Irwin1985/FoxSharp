using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    public class Builtins
    {
        public Dictionary<string, IBuiltin> Pool = new Dictionary<string, IBuiltin>();
        public Builtins()
        {
            Pool.Add("type", new BuiltinType());
            Pool.Add("len", new BuiltinLen());
            Pool.Add("send", new BuiltinSend());
            Pool.Add("load", new BuiltinLoad());
            Pool.Add("file", new BuiltinFile());
            Pool.Add("format", new BuiltinFormat());
            Pool.Add("left", new BuiltinLeft());
            Pool.Add("right", new BuiltinRight());
            Pool.Add("alltrim", new BuiltinAlltrim());
            Pool.Add("inputbox", new BuiltinInputBox());
            // Input / Output
            Pool.Add("info", new BuiltinInfo());
            Pool.Add("warning", new BuiltinWarning());
            Pool.Add("error", new BuiltinError());
            Pool.Add("question", new BuiltinQuestion());
            Pool.Add("download", new BuiltinDownload());
        }
    }
    public interface IBuiltin : IObject
    {
        IObject Run(List<IObject> args);
    }
}
