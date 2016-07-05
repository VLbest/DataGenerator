using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator {
    class WordClock {

        public DateTime? ActualTime;
        public DateTime? StartTime;

        public WordClock() {
            this.ActualTime = null;
        }

        public void init() {
            this.ActualTime = DateTime.Now.AddMonths(-1);
            this.StartTime = DateTime.Now;
        }

        public bool PerformTimeTick() {
            ActualTime = this.ActualTime.Value.AddDays(1);
            if (this.ActualTime <= StartTime.Value.AddMonths(3)) {
                Console.WriteLine(StartTime.ToString() + " / " + ActualTime.ToString());
                return true;
            }
            return false;
        }

        internal bool isWeekend() {
            if ( ActualTime.Value.DayOfWeek == DayOfWeek.Sunday || ActualTime.Value.DayOfWeek == DayOfWeek.Saturday ) {
                return true;
            }
            return false;
        }
    }
}
