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
        public Environment(Environment outer){
            this.Outer = outer;
        }
        public IObject Set(string name, IObject value){
            if (Store.ContainsKey(name)){
                Store[name] = value;
                return value;
            }
            Store.Add(name, value);
            return value;
        }
        public IObject Get(string name)
        {
            if (Store.ContainsKey(name)){
                return Store[name];
            }
            if (Outer != null){
                return Outer.Get(name);
            }
            return null;
        }
        public void GetAndSet(string name, IObject newValue){
            if (Store.ContainsKey(name)){
                Store[name] = newValue;
            }
            if (Outer != null){
                Outer.GetAndSet(name, newValue);
            }
        }
    }
}
