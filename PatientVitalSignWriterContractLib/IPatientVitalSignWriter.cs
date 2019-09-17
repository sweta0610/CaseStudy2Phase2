//============================================================================
//
// COPYRIGHT KONINKLIJKE PHILIPS ELECTRONICS N.V. 2019
// All rights are reserved. Reproduction in whole or in part is
// prohibited without the written consent of the copyright owner.
//
//============================================================================ 
using System.ServiceModel;

namespace PatientVitalSignWriterContractLib
{
    [ServiceContract]
    public interface IPatientVitalSignWriter
    {
        [OperationContract]
        void StorePatientVitalSigns(string patientId, string m_jsonData);
    }
}
