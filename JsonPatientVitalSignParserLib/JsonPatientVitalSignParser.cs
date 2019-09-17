//============================================================================
//
// COPYRIGHT KONINKLIJKE PHILIPS ELECTRONICS N.V. 2019
// All rights are reserved. Reproduction in whole or in part is
// prohibited without the written consent of the copyright owner.
//
//============================================================================ 
using JsonPatientVitalSignParserContractLib;

namespace JsonPatientVitalSignParserLib
{
    /// <summary>
    /// This class will parse patient Json vital string.
    /// </summary>

    public class JsonPatientVitalSignParser : IParser
    {
        public string[] ParseJsonData(string m_jsonData)
        {
            int len = m_jsonData.Length;

            //gives jsonstring without curly paranethesis  
            //i.e { "patient id": "TRJIW432", "SPO2": 96, "pulse rate": 75, "temperature": 98.6  
            string m_withoutParenthesis = m_jsonData.Substring(1, len - 2);

            //This array will store pairs i.e patient id": "TRJIW432
            string[] m_splitByComma = m_withoutParenthesis.Split(',');

            //This array will store only values of vital signs i.e:TRJIW432, 96, 75, 98.6
            string[] m_splitByColon = new string[m_splitByComma.Length];

            for (int i = 0; i < m_splitByComma.Length; i++)
            {
                string temp = m_splitByComma[i];
                int posOfColon = temp.IndexOf(':') + 2;
                string token = temp.Substring(posOfColon, temp.Length - posOfColon);
                m_splitByColon[i] = token;
            }
            return m_splitByColon;
        }
    }
}
