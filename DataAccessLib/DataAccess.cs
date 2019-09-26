//============================================================================
//
// COPYRIGHT KONINKLIJKE PHILIPS ELECTRONICS N.V. 2019
// All rights are reserved. Reproduction in whole or in part is
// prohibited without the written consent of the copyright owner.
//
//============================================================================ 
using System.Collections.Generic;
using DataAccessContractLib;
using VitalSignLib;
using DataStoreLib;

namespace DataAccessLib
{
    //This is data access layer 
    //every operation related to dataStorage will go thru this layer.
    public class DataAccess : IDataAccess
    {
        public void EnableVitalSignForPatient(string patientId, List<VitalSign> m_vitalSigns)
        {
            if (DataStore.dictPatientVitalSignEnabledMap.ContainsKey(patientId))
            {
                DataStore.dictPatientVitalSignEnabledMap.Remove(patientId);
            }
            //Store the list of enabled/disabled vital sign for patientId in dataStorage.

            List<VitalSign> enabledList = new List<VitalSign>();    //new
            foreach(var item in m_vitalSigns)
            {
                if (item.IsPatientVitalSignEnabled == true)
                    enabledList.Add(item);
            }
            DataStore.dictPatientVitalSignEnabledMap.Add(patientId, enabledList);
        }

        public string ReadPatientVitalSigns(string patientId)
        {
            string m_jsonData = string.Empty;
            if (DataStore.DictPatientDataMap != null && DataStore.DictPatientDataMap.Count > 0)
            {
                m_jsonData = GetPatientDataFromQueue(patientId);
            }
            return m_jsonData;
        }
        private string GetPatientDataFromQueue(string patientId)
        {
            string patientData = string.Empty;

            Queue<string> queuePatientData = GetQueueForPatient(patientId);

            patientData = (queuePatientData.Count > 0) ? queuePatientData.Dequeue() : string.Empty;

            return patientData;
        }
        private Queue<string> GetQueueForPatient(string patientId)
        {
            Queue<string> queuePatientData = null;

            //If new patient comes a new queue will be created to store its vital signs
            //If not then, old patient's queue will be fetched from data storage and new vitals will be store.
            if (!DataStore.DictPatientDataMap.ContainsKey(patientId))
            {
                queuePatientData = new Queue<string>();
                DataStore.DictPatientDataMap.Add(patientId, queuePatientData);
            }
            queuePatientData = DataStore.DictPatientDataMap[patientId];

            return queuePatientData;
        }

        public List<VitalSign> GetEnabledVitalSignForPatient(string patientId)
        {
            List<VitalSign> lstVitalSign = DataStore.dictPatientVitalSignEnabledMap.ContainsKey(patientId)
                ? DataStore.dictPatientVitalSignEnabledMap[patientId]
                : DataStore.LstDefaultVitalSign;

            return lstVitalSign;
        }

        public void StorePatientVitalSigns(string patientId, string m_jsonData)
        {
            if (DataStore.DictPatientDataMap != null)
            {
                Queue<string> m_queuePatientData = null;
                //Checks if patient already exist 
                //If new patient and create a new queue and enqueue vitalsigns in it.
                //Then push to storage in Dictionary. 
                if (!DataStore.DictPatientDataMap.ContainsKey(patientId))
                {
                    m_queuePatientData = new Queue<string>();
                    DataStore.DictPatientDataMap.Add(patientId, m_queuePatientData);
                }
                m_queuePatientData = DataStore.DictPatientDataMap[patientId];

                m_queuePatientData.Enqueue(m_jsonData);
            }
        }
    }
}
