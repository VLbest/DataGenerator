using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator {
    public class Commande {
        public float TotPrix { get; set; }
        public DateTime DateCommande { get; set; }
        public DateTime DateFinPreparation { get; set; }
        public DateTime DateLivraison { get; set; }
        public List<Lot> ListColis { get; set; } 

    }
}
