using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using DataGenerator;
using DbConnector.Fabrication;

namespace DbConnector
{
   public class DbHolder{

        private static string dbProducts;
        private static string dbExpeition;

        static DbHolder() {
            
        }

        public static void initConnStrings(){
            dbProducts = System.Configuration.ConfigurationManager.ConnectionStrings["DbProducts"].ConnectionString;
            dbExpeition = System.Configuration.ConfigurationManager.ConnectionStrings["DbExperdition"].ConnectionString;
        }

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
        

        public static void insertNewCommande(CommandEntity command) {
            MySqlConnection conn = new MySqlConnection(dbExpeition);
            conn.Open();

            string query = @"
INSERT INTO `commande`(`destination`, `date_livraison`, `date_commande`) 
VALUES ('{0}','{1}','{2}')
";
            query = string.Format(query, command.Destination,
                command.DateLivraison.Value.ToString("yyyy-MM-dd"),
                command.DateCreation.Value.ToString("yyyy-MM-dd"));
            SqlQuery(query, dbExpeition);

            query = "SELECT MAX(id_commande) FROM commande";
            int lastCommand = SqlSelect(query, dbExpeition).Rows[0].Field<int>(0);

            foreach(LotEntity lot in command.ListLots) {
                query = "SELECT ID FROM produit_fini WHERE Ref = '{0}'";
                query = string.Format(query, lot.Ref);
                int lotID = SqlSelect(query, dbProducts).Rows[0].Field<int>(0);

                query = @"INSERT INTO `contient`(`nombre`, `id_produit_fini`, `id_commande`) VALUES ('{0}','{1}','{2}')";
                query = string.Format(query, lot.Nb, lotID, lastCommand);
                SqlQuery(query, dbExpeition);
            }

            conn.Close();
        }

        public static DataTable getFabrication()
        {
            DataTable Fabrication = SqlSelect("SELECT * FROM travail_fabrication", dbProducts);
            return Fabrication;
        }

        public static void insertNewFabrication(FabricationEntity fabrication)
        {
            MySqlConnection conn = new MySqlConnection(dbExpeition);
            conn.Open();

            string query = @"
INSERT INTO `travail_fabrication`(`heure_debut`, `heure_fin`, `temps`, 'Quantite_piece') 
VALUES ('{0}','{1}','{2}','{3}','{4}')
";
            query = string.Format(query, fabrication.HeureDebut.Value.ToString("yyyy-MM-dd"),
                fabrication.HeureFin.Value.ToString("yyyy-MM-dd"),
                fabrication.Temps,
                fabrication.QuantitePiece);
            SqlQuery(query, dbExpeition);

            //query = "SELECT MAX(id_commande) FROM commande";
            //int lastCommand = SqlSelect(query, dbExpeition).Rows[0].Field<int>(0);

            /*foreach (LotEntity lot in fabrication.ListLots)
            {
                query = "SELECT ID FROM produit_fini WHERE Ref = '{0}'";
                query = string.Format(query, lot.Ref);
                int lotID = SqlSelect(query, dbProducts).Rows[0].Field<int>(0);

                query = @"INSERT INTO `contient`(`nombre`, `id_produit_fini`, `id_commande`) VALUES ('{0}','{1}','{2}')";
                query = string.Format(query, lot.Nb, lotID, lastCommand);
                SqlQuery(query, dbExpeition);
            }*/

            conn.Close();
        }

        public static void truncExpeditions() {
            SqlQuery("TRUNCATE contient;", dbExpeition);
            SqlQuery("TRUNCATE palette;", dbExpeition);
            SqlQuery("SET FOREIGN_KEY_CHECKS = 0; TRUNCATE commande; SET FOREIGN_KEY_CHECKS = 1;", dbExpeition);
        }

        private static int SqlQuery(String query, String connStr) {
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand command = new MySqlCommand(query, conn);
            conn.Open();
            int res = command.ExecuteNonQuery();
            conn.Close();
            return res;
        }

        private static int SqlQuery(String query, MySqlConnection conn) {
            MySqlCommand command = new MySqlCommand(query, conn);
            int res = command.ExecuteNonQuery();
            return res;
        }

        private static DataTable SqlSelect(String query, String connStr) {
            MySqlConnection conn = new MySqlConnection(connStr);
            DataTable data = new DataTable();
            conn.Open();
            MySqlDataAdapter Adapter = new MySqlDataAdapter(query, conn);
            Adapter.Fill(data);
            conn.Close();
            return data;
        }

    }
}
