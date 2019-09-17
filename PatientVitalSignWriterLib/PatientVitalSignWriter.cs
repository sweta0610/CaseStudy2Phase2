//============================================================================
//
// COPYRIGHT KONINKLIJKE PHILIPS ELECTRONICS N.V. 2019
// All rights are reserved. Reproduction in whole or in part is
// prohibited without the written consent of the copyright owner.
//
//============================================================================ 
using System.Collections.Generic;
using PatientVitalSignWriterContractLib;
using DataStoreLib;
using DataAccessContractLib;

namespace PatientVitalSignWriterLib
{
    //This writer class will write patient vital sign in storage 
    public class PatientVitalSignWriter : IPatientVitalSignWriter
    {
        readonly IDataAccess m_dataAccess = null;
        public PatientVitalSignWriter(IDataAccess dataAccess)
        {
            m_dataAccess = dataAccess;
        }
        public void StorePatientVitalSigns(string patientId, string m_jsonData)
        {
            m_dataAccess.StorePatientVitalSigns(patientId, m_jsonData);
        }
    }
}
