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
            Pool.Add("format", new BuiltinFormat());
            // Input / Output
            Pool.Add("info", new BuiltinInfo());
            Pool.Add("warning", new BuiltinWarning());
            Pool.Add("error", new BuiltinError());
            Pool.Add("question", new BuiltinQuestion());
        }
    }
    public interface IBuiltin : IObject
    {
        IObject Run(List<IObject> args);
    }
}
