using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataGenerator;

namespace DbConnector.Fabrication
{
    public class FabricationApi
    {
        public List<FabricationEntity> FabricationList;
        BackgroundWorker backgroundWorker;

        public FabricationApi(BackgroundWorker backgroundWorker) {
            this.FabricationList = new List<FabricationEntity>();
            this.backgroundWorker = backgroundWorker;
            backgroundWorker.DoWork += backgroundWorker_DoInsertWork;
        }

        public LinkedList<FabricationEntity> getAvailableCommands() {
            DataTable data = DbHolder.getFabrication();
            LinkedList<FabricationEntity> Fabrication;
            return null;
        }

        public void insertFabrication(List<FabricationEntity> fabrications)
        {

            FabricationList = fabrications;

            backgroundWorker.RunWorkerAsync();
        }

        void backgroundWorker_DoInsertWork(object sender, DoWorkEventArgs e)
        {
            DbHolder.truncExpeditions();
            foreach (FabricationEntity fabrication in FabricationList)
            {
                int id = FabricationList.IndexOf(fabrication);
                backgroundWorker.ReportProgress((id * 100) / FabricationList.Count);
                DbHolder.insertNewFabrication(fabrication);
            }
            backgroundWorker.CancelAsync();
        }
    }
}
