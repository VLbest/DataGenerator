using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataGenerator;
using System.ComponentModel;

namespace DbConnector
{
    public class CommandsApi
    {
        public List<CommandEntity> CommandList;
        BackgroundWorker backgroundWorker;

        public CommandsApi(BackgroundWorker backgroundWorker) {
            this.CommandList = new List<CommandEntity>();
            this.backgroundWorker = backgroundWorker;
            backgroundWorker.DoWork += backgroundWorker_DoInsertWork;
        }

        public LinkedList<CommandEntity> getAvailableCommands() {
            DataTable data = DbHolder.getCommands();
            LinkedList<CommandEntity> Commands;
            return null;
        }

        public void insertCommands(List<CommandEntity> commands) {

            CommandList = commands;
            
            backgroundWorker.RunWorkerAsync();
        }

        void backgroundWorker_DoInsertWork(object sender, DoWorkEventArgs e) {
            DbHolder.truncExpeditions();
            foreach(CommandEntity command in CommandList) {
                int id = CommandList.IndexOf(command);
                backgroundWorker.ReportProgress(( id * 100 ) / CommandList.Count);
                DbHolder.insertNewCommande(command);
            }
            backgroundWorker.CancelAsync();
        }
    }
    
}
