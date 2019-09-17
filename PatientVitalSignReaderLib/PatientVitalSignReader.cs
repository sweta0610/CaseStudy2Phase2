//============================================================================
//
// COPYRIGHT KONINKLIJKE PHILIPS ELECTRONICS N.V. 2019
// All rights are reserved. Reproduction in whole or in part is
// prohibited without the written consent of the copyright owner.
//
//============================================================================ 

using System.Collections.Generic;
using PatientVitalSignReaderContractLib;
using DataStoreLib;
using DataAccessContractLib;

namespace PatientVitalSignReaderLib
{
    //This class will Read the patient vital signs from DataStorage
    public class PatientVitalSignReader : IReader
    {
        readonly IDataAccess m_dataAccess = null;
        public PatientVitalSignReader(IDataAccess dataAccess)
        {
            m_dataAccess = dataAccess;
        }
        public string ReadPatientVitalSigns(string patientId)
        {
           return m_dataAccess.ReadPatientVitalSigns(patientId);
        }
    }
}
