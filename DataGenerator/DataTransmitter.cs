using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Flurl;
using Flurl.Http;

namespace DataGenerator {
    class DataTransmitter {

        private DataAdapter DataAdapter;
        //private ProductsAPI ProductsApi;
        private String someUrl = "http://www.example.com/recepticle.aspx";

        public DataTransmitter() {
            this.DataAdapter = new DataAdapter();
            //this.ProductsApi = new ProductsAPI();
        }

        public void sendToDataBase(String jsonData) {

            var responseString = someUrl
            .PostUrlEncodedAsync( new { commande = jsonData } )
            .ReceiveString();
        }

        internal List<Product> getAvailableRefs() {

            //var list = ProductsApi.getAllLots();
            return null;
            /*
            var responseString = someUrl.GetStringAsync();
            return DataAdapter.FromJson(responseString);
            */
        }
    }
}
