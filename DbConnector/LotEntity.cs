using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConnector {
    class LotEntity {

        public int Ref { get; set; }
        public int Count { get; set; }
        public float Price { get; set; }
        public List<LotsEntity> Products { get; set; }

    }
}
