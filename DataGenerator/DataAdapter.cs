using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace DataGenerator {
   public class DataAdapter {
        private JavaScriptSerializer Serializer;

        public DataAdapter() {
            this.Serializer = new JavaScriptSerializer();
        }

        public String ToJson(List<CommandEntity> commands) {
            //String json = Serializer.Serialize(commands);
            String json = JsonConvert.SerializeObject(commands, Formatting.Indented);
            return json;
        }

        internal List<Product> FromJson(Task<string> responseString) {
            var data = Serializer.Deserialize<List<Product>>( responseString.ToString());
            return data;
        }
    }
}
