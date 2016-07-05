using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator {
    public class GenConstrains {

        private Random random;

        public int MAX_COMMAND { get; set; } = 500;

        [Obsolete]
        public int MAX_COMMAND_PER_DAY { get; set; } = 6;
        [Obsolete]
        public int MIN_COMMAND_PER_DAY { get; set; } = 2;

        public int MAX_LOTS_PER_COMMAND { get; set; } = 6;
        public int MIN_LOTS_PER_COMMAND { get; set; } = 2;
        public int MIN_PRODUCTS_BY_REF { get; set; } = 15;
        public int MAX_PRODUCTS_BY_REF { get; set; } = 20;


        public GenConstrains() {
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
