using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConnector
{
    class CommandsApi
    {
        public LinkedList<CommandEntity> CommandList;

        public CommandsApi() {
            this.CommandList = new LinkedList<CommandEntity>();
        }

        public LinkedList<CommandEntity> getAvailableCommands() {
            DataTable data = DbHolder.getCommands();
            LinkedList<CommandEntity> Commands;
            foreach(DataRow Commands in data.Rows) {
                                
            }


            return null;
        }

    }
    }
}
