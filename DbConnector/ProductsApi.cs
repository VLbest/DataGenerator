using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConnector {
    public class ProductsApi {

        public LinkedList<LotEntity> LotsList;

        public ProductsApi() {
            this.LotsList = new LinkedList<LotEntity>();
        }

        public LinkedList<LotEntity> getAvailableLots() {
            DataTable data = DbHolder.getLots();
            LinkedList<LotEntity> Lots;
            IEnumerable<DataRow> collection = data.Rows.Cast<DataRow>();


            return null;
        }

    }
}
