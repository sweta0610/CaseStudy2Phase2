//============================================================================
//
// COPYRIGHT KONINKLIJKE PHILIPS ELECTRONICS N.V. 2019
// All rights are reserved. Reproduction in whole or in part is
// prohibited without the written consent of the copyright owner.
//
//============================================================================ 
using PatientMonitorContractLib;
using System.Collections.Generic;
using System.Text;
using DataStoreLib;
using VitalSignLib;
using GeneratePatientVitalSignContractLib;
using VitalSignContractLib;
using DataAccessContractLib;

namespace PatientMonitorLib
{
    //This will generate patient vital sign and will provide in Json Format...
    //It will take list of enabled/diabled vital signs from DataStore and will generate data 
    //according to enabled vital sign.
    public class PatientMonitor : IPatientMonitor
    {
        readonly List<IVitalSignGenerator> m_lstVitalSignGenerator = null;
        #region Constructor
        public PatientMonitor(List<IVitalSignGenerator> lstVitalSignGenerator)
        {
            m_lstVitalSignGenerator = lstVitalSignGenerator;
        }
        #endregion

        public string GenerateVitalSignAsJson(string patientId)
        {
            StringBuilder m_stringBuilder = new StringBuilder();
            double m_vitalSignValue = 0;

            //By default all vitals sign will be checked (enabled).
            //LstDefaultSign this property will give default list of vital sign 
            List<VitalSign> lstVitalSign = GetEnabledVitalSignForPatient(patientId);

            m_stringBuilder.Append("{patientId: " + patientId);

            foreach (VitalSign vitalSign in lstVitalSign)
            {
                if (vitalSign.IsPatientVitalSignEnabled)
                {
                    //It will take typeObj i.e spo2obj/pulserateObj/TemperatureObj
                    //on the basis of object type patientVitalSignGenerator will be called.
                    //vitalSignValue will take values of vital sign and append to stringBuilder.
                    IVitalSignGenerator vitalSignGenerator = GetVitalSignGenerator(vitalSign.VitalSignType);
                    m_vitalSignValue = vitalSignGenerator.PatientVitalSignGenerator(patientId);
                }
                m_stringBuilder.Append(", " + vitalSign.VitalSignType.ToString() + ": " + m_vitalSignValue.ToString());
            }
            m_stringBuilder.Append("}");

            return m_stringBuilder.ToString();
        }

        private IVitalSignGenerator GetVitalSignGenerator(VitalSignType vitalSignType)
        {
            IVitalSignGenerator vitalSignGenerator = null;
            foreach (IVitalSignGenerator tempVitalSignGenerator in m_lstVitalSignGenerator)
            {
                if(tempVitalSignGenerator.VitalSignType == vitalSignType)
                {
                    vitalSignGenerator = tempVitalSignGenerator;
                    break;
                }
            }

            return vitalSignGenerator;
        }
        private List<VitalSign> GetEnabledVitalSignForPatient(string patientId)
        {
            List<VitalSign> lstVitalSign = DataStore.dictPatientVitalSignEnabledMap.ContainsKey(patientId)
                ? DataStore.dictPatientVitalSignEnabledMap[patientId]
                : DataStore.LstDefaultVitalSign;

            return lstVitalSign;
        }
    }
}
