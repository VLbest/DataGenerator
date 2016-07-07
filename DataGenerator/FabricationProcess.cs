using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbConnector;
using DbConnector.Fabrication;

namespace DataGenerator {
    public class FabricationProcess {

        private List<CommandEntity> commands;
        private List<FabricationEntity> fabChain;
        private int weekSlice = 2;

        public FabricationProcess() {
            commands = new List<CommandEntity>();
            fabChain = new List<FabricationEntity>();
        }

        public void beginFabrication(){
            sli()
        }

        private void sliceData() {
            commands = commands.OrderBy(x => x.DateLivraison).ToList();
        }

    }
}
