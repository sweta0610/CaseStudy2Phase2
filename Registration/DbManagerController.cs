using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbManagerControllerContractLib;
using DBManagerLib;

namespace DbManagerControllerLib
{
    public class DbManagerController : IDbManager
    {
        DBManager dBManager = new DBManager();

        public void CloseDBConnection()
        {
            dBManager.CloseDBConnection();
        }

        public DbCommand CreateDBCommand(string commandText)
        {
            return dBManager.CreateDBCommand(commandText);
        }

        public DbDataReader ExecuteCommand(DbCommand command)
        {
            return dBManager.ExecuteCommand(command);
        }

        public void OpenDBConnection()
        {
            dBManager.OpenDBConnection();
        }
    }
}
