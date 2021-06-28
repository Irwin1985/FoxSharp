using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    public class HashObj : IObject
    {
        public Dictionary<string, IObject> Pairs;
        public HashObj() { }
        public ObjectType Type(){
            return ObjectType.HASH_OBJ;
        }
        public string Inspect(){
            var output = new StringBuilder();
            output.Append("{");
            if (Pairs.Count > 0){
                var pairs = new List<String>();
                foreach (KeyValuePair<string, IObject> kvp in Pairs){
                    pairs.Add(String.Format("{0}:{1}", kvp.Key.Inspect(), kvp.Value.Inspect()));
                }
                output.Append(String.Join(",", pairs));
            }
            output.Append("}");
            return output.ToString();
        }
    }
    
}
