using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator {
    public class Lot {

        public int Ref { get; set; }
        public int Count { get; set; }
        public float Price { get; set; }
        public List<Product> Products { get; set; }

    }
}
