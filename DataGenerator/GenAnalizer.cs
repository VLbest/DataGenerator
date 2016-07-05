using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DataGenerator {
    public class GenAnalizer {
        internal string getNbOfCommands(List<Commande> commandes) {
            return commandes.Count.ToString();
        }

        internal string getNbPieces(List<Commande> commandes) {
            return commandes.AsEnumerable().Sum(x => x.ListColis.AsEnumerable().Sum(y => y.Count)).ToString();
        }
    }
}
