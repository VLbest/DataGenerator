﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConnector {
    public class LotEntity {

        public int ID { get; set; }
        public String Ref { get; set; }
        public float Count { get; set; }
        public float Price { get; set; }
        public List<ProductsEntity> Products { get; set; }

    }
}
