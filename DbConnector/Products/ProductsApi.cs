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
            DbHolder.initConnStrings();
            this.LotsList = new LinkedList<LotEntity>();
        }

        public List<LotEntity> getAvailableLots() {
            DataTable data = DbHolder.getLots();
            List<LotEntity> Lots = new List<LotEntity>();

            foreach(DataRow lot in data.Rows) {

                LotEntity newLot = new LotEntity() {
                    ID = lot.Field<int>("ID"),
                    Ref = lot.Field<String>("Ref"),
                    Nom = lot.Field<String>("Nom"),
                    Price = (float)lot.Field<float>("Prix"),
                };

                DataTable prodData = DbHolder.getProductsOfLot(newLot);
                List<ProductsEntity> productsList = new List<ProductsEntity>();
                foreach(DataRow p in prodData.Rows) {
                    ProductsEntity product = new ProductsEntity() {
                        ID = p.Field<int>(0),
                        Refs = p.Field<String>(1),
                        Nom = p.Field<String>(2),
                        Price = p.Field<float>(3),
                    };
                    productsList.Add(product);
                }
                newLot.Products = productsList;
                Lots.Add(newLot);
            }

            return Lots;
        }

    }
}
