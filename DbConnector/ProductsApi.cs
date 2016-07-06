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
            List<LotEntity> Lots = new List<LotEntity>();
            IEnumerable<DataRow> LotsCollection = data.Rows.Cast<DataRow>();
            Lots = LotsCollection.Select(x => new LotEntity() {
                ID = x.Field<int>("ID"),
                Ref = x.Field<String>("Ref"),
                Price = (float)x.Field<double>("Price"),

            }).ToList();

            return null;
        }

    }
}
