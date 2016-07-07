using DbConnector;
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
        public List<CommandEntity> Commandes { get; set; }
        private List<LotEntity> AvailableLots;
        private GenConstrains Constrains;
        private WordClock Clock;
        private BackgroundWorker BackgroundWorker;
        private DataTransmitter DataTransmitter;
        public bool response { get; set; }
        Object lockMe = new Object();
        private Random rand;
        public DataBuilder(BackgroundWorker backgroundWorker) {
            this.rand = new Random();
            this.DataTransmitter = new DataTransmitter();
            this.Commandes = new List<CommandEntity>();
            this.Clock = new WordClock();
            this.Constrains = new GenConstrains();
            AvailableLots = DataTransmitter.getAvailableRefs();
            this.BackgroundWorker = backgroundWorker;
            backgroundWorker.DoWork += BackgroundWorker_DoWork;
        }


        public bool StartGeneration(GenConstrains constrains) {
            this.Constrains = constrains;
            BackgroundWorker.RunWorkerAsync();
            return response;
            //poo
            #region Obsolete

            /*
            Clock.init();
            while ( Clock.PerformTimeTick() ) {

                int nbComms = Constrains.NbCommandToday();
                try {
                Parallel.For( 0, nbComms, i =>
                {
                    List<LotEntity> LotsOfCommand = SelectLots();
                    //List<LotEntity> listProducts = generateColis( productsOfCommand );
                    lock ( lockMe ) {
                        Commandes.Add(generateCommands(LotsOfCommand));
                    }
                    BackgroundWorker.ReportProgress( (int)( ( ( Commandes.Count ) * 100 ) / Constrains.MAX_COMMAND ) );
                } );
                BackgroundWorker.CancelAsync();
                this.response = true;
            } catch ( Exception ) {
                this.response = false;
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
            Clock.init();
            while(Clock.PerformTimeTick() && commCount > 0) {
                int nbComms = Constrains.NbCommandToday();
                nbComms = commCount - nbComms < 0 ? nbComms + (commCount - nbComms) : nbComms;
                commCount -= nbComms;
                try {
                    Parallel.For(0, nbComms, i => {
                        List<LotEntity> LotsOfCommand = SelectLots();
                        lock(lockMe) {
                            Commandes.Add(generateCommands(LotsOfCommand));
                        }
                        BackgroundWorker.ReportProgress((int)(((Commandes.Count) * 100) / Constrains.MAX_COMMAND));
                    });

                    this.response = true;
                } catch(Exception) {
                    this.response = false;
                }
            }
            BackgroundWorker.CancelAsync();
            #region obsolete
            /*
            try {
                Parallel.For( 0, commCount, i =>
                {
                    List<LotEntity> LotsOfCommand = SelectLots();
                    //List<LotEntity> listProducts = generateColis( productsOfCommand );
                    lock ( lockMe ) {
                        Commandes.Add(generateCommands(LotsOfCommand));
                    }
                    
                    BackgroundWorker.ReportProgress( (int)( ( ( Commandes.Count ) * 100 ) / Constrains.MAX_COMMAND ) );
                } );
                BackgroundWorker.CancelAsync();
                this.response = true;
            } catch ( Exception ) {
                this.response = false;
            }
             */
            #endregion
        }

        private List<LotEntity> SelectLots() {
            List<LotEntity> productsOfCommand = new List<LotEntity>();
            int nbProds = Constrains.NbLotsForACommand();
            var rnd = new Random();
            var randomNumbers = Enumerable.Range( 0, AvailableLots.Count).OrderBy( x => rnd.Next() ).Take( nbProds ).ToList();
            nbProds = nbProds >= randomNumbers.Count ? randomNumbers.Count : nbProds;
            Parallel.For(0,
                         nbProds,
                         i => {
                             lock ( lockMe ) {
                                 LotEntity lot = AvailableLots[randomNumbers[i]];
                                 lot.Nb = Constrains.NbPiecesByRef();
                                 productsOfCommand.Add( lot );
                             }
                         } );

            return productsOfCommand;
        }

        public List<LotEntity> generateColis(List<ProductsEntity> products) {
            List < LotEntity > colises = new List<LotEntity>();
            foreach (ProductsEntity product in products) {
                LotEntity lot = new LotEntity() {Ref = product.Refs, Nb = Constrains.NbPiecesByRef()};
                lot.Price = lot.Nb*product.Price;
                colises.Add(lot);
            }
            return colises;
        }

        public CommandEntity generateCommands(List<LotEntity> lots) {
            CommandEntity commande = new CommandEntity();
            commande.ListLots = lots;
            commande.DateCreation = Clock.ActualTime;
            commande.DateLivraison = Clock.ActualTime.Value.AddDays(Constrains.getLivrDate());
            int id = rand.Next(0, 6);
            commande.Destination = Pays.PaysList[id];
            return commande;
        }

    }
}
