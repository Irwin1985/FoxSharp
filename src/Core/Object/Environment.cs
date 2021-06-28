using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    public class Environment
    {
        private Dictionary<string, IObject> Store = new Dictionary<string, IObject>();
        private Environment Outer;
        public Environment() {}
        public Environment(Environment outer)
        {
            this.Outer = outer;
        }
        public IObject Set(string name, IObject value)
        {
            Store.Add(name, value);
            return value;
        }
        public IObject Get(string name)
        {
            if (Store.ContainsKey(name))
            {
                return Store[name];
            }
            return null;
        }
    }
}
