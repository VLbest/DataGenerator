using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace DataGenerator {
    public class DataBuilder {
        public List<Commande> Commandes { get; set; }
        private List<Product> Products;
        private GenConstrains Constrains;
        private WordClock Clock;
        private BackgroundWorker BackgroundWorker;
        private DataTransmitter DataTransmitter;
        public bool response { get; set; }
        Object lockMe = new Object();

        public DataBuilder(BackgroundWorker backgroundWorker) {
            this.DataTransmitter = new DataTransmitter();
            this.Commandes = new List<Commande>();
            this.Clock = new WordClock();
            this.Constrains = new GenConstrains();
            Products = DataTransmitter.getAvailableRefs();
            this.BackgroundWorker = backgroundWorker;
            backgroundWorker.DoWork += BackgroundWorker_DoWork;
        }


        public bool StartGeneration(GenConstrains constrains) {
            this.Constrains = constrains;
            BackgroundWorker.RunWorkerAsync();
            return response;

            #region Obsolete

            /*
            Clock.init();
            while ( Clock.PerformTimeTick() ) {

                int nbComms = Constrains.NbCommandToday();
                for ( int command = 0; command < nbComms; command++ ) {
                    List<Product> productsOfCommand = SelectProducts();
                    List<Lot> colisList = generateColis( productsOfCommand );
                    Commandes.Add( generateCommands( colisList ) );
                }

            }
            */

            #endregion
        }

        internal void reinit() {
            this.Commandes.Clear();
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e) {
            int commCount = Constrains.MAX_COMMAND;
            try {
                Parallel.For( 0, commCount, i =>
                {
                    List<Product> productsOfCommand = SelectProducts();
                    List<Lot> colisList = generateColis( productsOfCommand );
                    lock ( lockMe ) {
                        Commandes.Add( generateCommands( colisList ) );
                    }
                    
                    BackgroundWorker.ReportProgress( (int)( ( ( Commandes.Count ) * 100 ) / Constrains.MAX_COMMAND ) );
                } );
                BackgroundWorker.CancelAsync();
                this.response = true;
            } catch ( Exception ) {
                this.response = false;
            }
        }

        private List<Product> SelectProducts() {
            List<Product> productsOfCommand = new List<Product>();
            int nbProds = Constrains.NbLotsForACommand();
            var rnd = new Random();
            var randomNumbers = Enumerable.Range( 0, Products.Count).OrderBy( x => rnd.Next() ).Take( nbProds ).ToList();
            nbProds = nbProds >= randomNumbers.Count ? randomNumbers.Count : nbProds;
            Parallel.For(0,
                         nbProds,
                         i => {
                             lock ( lockMe ) {
                                 productsOfCommand.Add( Products[randomNumbers[i]] );
                             }
                         } );

            return productsOfCommand;
        }

        public List<Lot> generateColis(List<Product> products) {
            List < Lot > colises = new List<Lot>();
            foreach (Product product in products) {
                Lot lot = new Lot() {Ref = product.Refs, Count = Constrains.NbPiecesByRef()};
                lot.Price = lot.Count*product.Price;
                colises.Add(lot);
            }
            return colises;
        }

        public Commande generateCommands(List<Lot> colises) {
            Commande commande = new Commande();
            commande.ListColis = colises;
            commande.DateCommande = DateTime.Now;
            return commande;
        }
    }
}
