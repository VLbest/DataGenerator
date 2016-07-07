using DbConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator {
    class DataTransmitter {

        private DataAdapter DataAdapter;
        private ProductsApi ProductsApi;
        

        public DataTransmitter() {
            this.DataAdapter = new DataAdapter();
            this.ProductsApi = new ProductsApi();
        }

        public void sendToDataBase(String jsonData) {

            
        }

        internal List<LotEntity> getAvailableRefs() {
            return ProductsApi.getAvailableLots();
        }
    }
}
