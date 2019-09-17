//============================================================================
//
// COPYRIGHT KONINKLIJKE PHILIPS ELECTRONICS N.V. 2019
// All rights are reserved. Reproduction in whole or in part is
// prohibited without the written consent of the copyright owner.
//
//============================================================================ 
using System.ServiceModel;

namespace PatientMonitorContractLib
{
    [ServiceContract]
    public interface IPatientMonitor
    {
        [OperationContract]
        string GenerateVitalSignAsJson(string patientId);
    }
}
