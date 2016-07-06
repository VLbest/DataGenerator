using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator {
    public class GenConstrains {

        private Random random;

        public int MAX_COMMAND { get; set; }

        [Obsolete]
        public int MAX_COMMAND_PER_DAY { get; set; }
        [Obsolete]
        public int MIN_COMMAND_PER_DAY { get; set; }

        public int MAX_LOTS_PER_COMMAND { get; set; }
        public int MIN_LOTS_PER_COMMAND { get; set; }
        public int MIN_PRODUCTS_BY_REF { get; set; }
        public int MAX_PRODUCTS_BY_REF { get; set; }


        public GenConstrains() {
            MAX_COMMAND = 500;
            MAX_COMMAND_PER_DAY = 6;
            MIN_COMMAND_PER_DAY = 2;
            MAX_LOTS_PER_COMMAND = 6;
            MIN_LOTS_PER_COMMAND = 2;
            MIN_PRODUCTS_BY_REF = 15;
            MAX_PRODUCTS_BY_REF = 20;
            this.random = new Random();
        }

        [Obsolete]
        public int NbCommandToday() {
            return random.Next(MIN_COMMAND_PER_DAY, MAX_COMMAND + 1 );
        }

        public int NbLotsForACommand() {
            return random.Next( MIN_LOTS_PER_COMMAND, MAX_LOTS_PER_COMMAND + 1 );
        }

        internal int NbPiecesByRef() {
            return random.Next( MIN_PRODUCTS_BY_REF, MAX_PRODUCTS_BY_REF + 1 );
        }
    }
}
