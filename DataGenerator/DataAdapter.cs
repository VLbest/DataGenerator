using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace DataGenerator {
    class DataAdapter {
        private JavaScriptSerializer Serializer;

        public DataAdapter() {
            this.Serializer = new JavaScriptSerializer();
        }

        public String ToJson(Commande commande) {
            String json = Serializer.Serialize(commande);
            return json;
        }

        internal List<Product> FromJson(Task<string> responseString) {
            var data = Serializer.Deserialize<List<Product>>( responseString.ToString());
            return data;
        }
    }
}
