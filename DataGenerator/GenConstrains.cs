using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator {
    public class GenConstrains {

        private Random random;

        public int MAX_COMMAND { get; set; }

        public int AVG_COMM_PER_DAY { get; set; }
        public int MAX_COMMAND_PER_DAY { get; set; }
        public int MIN_COMMAND_PER_DAY { get; set; }

        public int MAX_LOTS_PER_COMMAND { get; set; }
        public int MIN_LOTS_PER_COMMAND { get; set; }
        public int MIN_PRODUCTS_BY_REF { get; set; }
        public int MAX_PRODUCTS_BY_REF { get; set; }

        public int COMM_PREP_DELAY { get; set; }


        public GenConstrains() {
            MAX_COMMAND = 500;
            AVG_COMM_PER_DAY = 6;
            MAX_COMMAND_PER_DAY = 6;
            MIN_COMMAND_PER_DAY = 6;
            MAX_LOTS_PER_COMMAND = 6;
            MIN_LOTS_PER_COMMAND = 2;
            MIN_PRODUCTS_BY_REF = 15;
            MAX_PRODUCTS_BY_REF = 20;
            COMM_PREP_DELAY = 30;
            this.random = new Random();
        }

        public void init() {
            int seedDisplace = (((15 * MAX_COMMAND) / 100) / 6) / 2;
            seedDisplace = seedDisplace == 0 ? 1 : seedDisplace;
            AVG_COMM_PER_DAY = seedDisplace * 3;
            if(AVG_COMM_PER_DAY > MAX_COMMAND) {
                AVG_COMM_PER_DAY = MAX_COMMAND;
                MAX_COMMAND_PER_DAY = AVG_COMM_PER_DAY;
                MIN_COMMAND_PER_DAY = AVG_COMM_PER_DAY;
            } else {
                MAX_COMMAND_PER_DAY = AVG_COMM_PER_DAY + seedDisplace;
                MIN_COMMAND_PER_DAY = AVG_COMM_PER_DAY - seedDisplace;
            }
        }

        public int NbCommandToday() {
            return random.Next(MIN_COMMAND_PER_DAY, MAX_COMMAND_PER_DAY + 1);
        }

        public int NbLotsForACommand() {
            return random.Next( MIN_LOTS_PER_COMMAND, MAX_LOTS_PER_COMMAND + 1 );
        }

        internal int NbPiecesByRef() {
            return random.Next( MIN_PRODUCTS_BY_REF, MAX_PRODUCTS_BY_REF + 1 );
        }

        internal int getLivrDate() {
            return random.Next((COMM_PREP_DELAY - COMM_PREP_DELAY / 2), (COMM_PREP_DELAY + COMM_PREP_DELAY / 2));
        }
    }
}
