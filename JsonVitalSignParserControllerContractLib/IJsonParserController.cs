using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace JsonVitalSignParserControllerContractLib
{
    [ServiceContract]
    public interface IJsonParserController
    {
        [OperationContract]
        string[] ParseJsonData(string m_jsonData);
    }
}
