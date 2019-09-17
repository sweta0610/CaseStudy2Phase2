using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStoreLib;

namespace PatientVitalSignsRepositoryLib
{
    public class PatientVitalSignsRepo : IPatientVitalSignDataWriter
    {
        public void StorePatientVitalSignsInDB(string patientId, string jsonData)
        {
            if (DataStore.DictPatientDataMap != null)
            {
                Queue<string> queuePatientData = null;
                if (!DataStore.DictPatientDataMap.ContainsKey(patientId))
                {
                    queuePatientData = new Queue<string>();
                    DataStore.DictPatientDataMap.Add(patientId, queuePatientData);
                }
                queuePatientData = DataStore.DictPatientDataMap[patientId];

                queuePatientData.Enqueue(jsonData);
            }
        }
    }
}
