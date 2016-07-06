using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DbConnector
{
    class DbHolder{

        private static string dbProducts = "Server=10.133.50.5;Port=8020;Database=Products;Uid=odbc;Pwd=odbc;";

        public static DataTable getLots() {
            DataTable Lots = SqlSelect("SELECT * FROM produit_fini", dbProducts);
            return Lots;
        }

        public static DataTable getProductsOfLot(LotEntity Lot) {
            string query = @"
SELECT * FROM matieres_premieres LEFT JOIN matieres_de_produit on matieres_premieres.ID = matieres_de_produit.ID
WHERE matieres_de_produit.ID_produit_fini = {0}
";
            query = string.Format(query, Lot.ID);
            DataTable Products = SqlSelect(query, dbProducts);
            return Products;
        }

        public static DataTable getCommands() {
            DataTable Commands = SqlSelect("SELECT * FROM Commande", dbProducts);
            return Commands;
        }


        private static DataTable SqlSelect(String connStr, String query){
            MySqlConnection conn = new MySqlConnection(dbProducts);
            DataTable data = new DataTable();
            conn.Open();
            MySqlDataAdapter Adapter = new MySqlDataAdapter(query, conn);
            Adapter.Fill(data);
            return data;
        }

    }
}
