using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator {
    class DataTransmitter {

        private DataAdapter DataAdapter;
        //private ProductsAPI ProductsApi;
        

        public DataTransmitter() {
            this.DataAdapter = new DataAdapter();
            //this.ProductsApi = new ProductsAPI();
        }

        public void sendToDataBase(String jsonData) {

            
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
