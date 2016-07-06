using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConnector {
    public class ProductsApi {

        public LinkedList<Lot> LotsList;

        public ProductsApi() {
            this.LotsList = new LinkedList<Lot>();
        }

        public LinkedList<Lot> getAvailableLots() {
            DataTable data = DbHolder.getLots();
            LinkedList<Lot> Lots;
            foreach(DataRow item in data) {
                                
            }


            return null;
        }

    }
}
