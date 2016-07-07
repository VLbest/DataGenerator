using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConnector.Fabrication
{
    public class FabricationEntity
    {
        public int idTravailFabrication { get; set; }
        public DateTime? HeureDebut { get; set; }
        public DateTime? HeureFin { get; set; }
        public int Temps { get; set; }
        public int QuantitePiece { get; set; }

    }
}
