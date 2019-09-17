//============================================================================
//
// COPYRIGHT KONINKLIJKE PHILIPS ELECTRONICS N.V. 2019
// All rights are reserved. Reproduction in whole or in part is
// prohibited without the written consent of the copyright owner.
//
//============================================================================ 
using JsonPatientVitalSignParserLib;
using JsonVitalSignParserControllerContractLib;

namespace JsonDataParserControllerLib
{
    //ParserController is a service which exposes ParseJsonData 
    //It will parse json data and gives an array which contains only values of vital signs
    public class ParserController : IJsonParserController
    {
        readonly JsonPatientVitalSignParser m_parser = new JsonPatientVitalSignParser();
        public string[] ParseJsonData(string m_jsonData)
        {
            return m_parser.ParseJsonData(m_jsonData);
        }
    }
}
