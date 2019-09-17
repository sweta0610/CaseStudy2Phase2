using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.ServiceModel;

namespace DbManagerControllerContractLib
{
    [ServiceContract]
    public interface IDbManager
    {
        [OperationContract]
        void OpenDBConnection();
        [OperationContract]
        void CloseDBConnection();
        [OperationContract]
        DbCommand CreateDBCommand(string commandText);
        [OperationContract]
        DbDataReader ExecuteCommand(DbCommand command);
    }
}
