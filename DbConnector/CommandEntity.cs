using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator
{
    public class CommandEntity
    {
        public int IdCommande { get; set; }
        public string Destination { get; set; }
        public DateTime DateLivraison { get; set; }
    }
}
