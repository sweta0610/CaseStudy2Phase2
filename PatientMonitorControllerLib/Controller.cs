//============================================================================
//
// COPYRIGHT KONINKLIJKE PHILIPS ELECTRONICS N.V. 2019
// All rights are reserved. Reproduction in whole or in part is
// prohibited without the written consent of the copyright owner.
//
//============================================================================ 
using System.Collections.Generic;
using PatientMonitorLib;
using PatientMonitorControllerContractLib;
using VitalSignLib;
using PatientVitalSignWriterContractLib;
using PatientVitalSignWriterLib;
using DataAccessContractLib;
using FactoryLib;
using EnableVitalSignContractLib;
using PatientMonitorContractLib;

namespace ControllerLib
{
    //This is Patient Monitor service 
    //This exposes GenerateVitalSignAsJson.
    //It Generate vital sign for patient in json format
    public class Controller : IController
    {
        readonly IPatientMonitor m_patientMonitor = null;
        readonly IEnableVitalSign m_vitalSignEnabler = null;
        readonly IPatientVitalSignWriter m_vitalSignWriter = null;
        public Controller()
        {
            m_patientMonitor = Factory.GetPatientMonitor();
            m_vitalSignEnabler = Factory.GetVitalSignEnabler();
            m_vitalSignWriter = Factory.GetVitalSignWriter();
        }

        #region PatientMonitor Public Methods
        public string GenerateVitalSignAsJson(string patientId)
        {
            return m_patientMonitor.GenerateVitalSignAsJson(patientId);
        }
        public void EnableVitalSignForPatient(string patientId, List<VitalSign> m_vitalSigns)
        {
            m_vitalSignEnabler.EnableVitalSignForPatient(patientId, m_vitalSigns);
        }
        public void StorePatientVitalSignsInDB(string patientId, string m_jsonData)
        {
            m_vitalSignWriter.StorePatientVitalSigns(patientId, m_jsonData);
        }
        #endregion
    }
}
