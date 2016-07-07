using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DataGenerator {
    public class GenAnalizer {
        internal string getNbOfCommands(List<CommandEntity> commandes) {
            return commandes.Count.ToString();
        }

        internal string getNbPLots(List<CommandEntity> commandes) {
            return commandes.AsEnumerable().Sum(x => x.ListLots.AsEnumerable().Sum(y => y.Nb)).ToString();
        }

        internal string getNbPieces(List<CommandEntity> commandes) {
            return commandes.AsEnumerable().Sum(x => x.ListLots.AsEnumerable().Sum(y => y.Products.Count * y.Nb)).ToString();
        }
    }
}
