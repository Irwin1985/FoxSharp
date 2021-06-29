using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    public class SmtpObj : IObject
    {
        public Dictionary<string, IObject> Properties;
        public ObjectType Type()
        {
            return ObjectType.SMTP;
        }
        public string Inspect(){
            var output = new StringBuilder();
            output.Append("smtp {\n");
            if (Properties.Count > 0)
            {
                var props = new List<String>();
                foreach (KeyValuePair<string, IObject> kvp in Properties)
                {
                    props.Add(String.Format("{0}:{1}\n", kvp.Key, kvp.Value.Inspect()));
                }
                output.Append(String.Join(",", props));
            }
            output.Append("}");
            return output.ToString();
        }
    }
}
