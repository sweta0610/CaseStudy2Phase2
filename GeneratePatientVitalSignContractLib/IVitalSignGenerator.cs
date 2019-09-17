//============================================================================
//
// COPYRIGHT KONINKLIJKE PHILIPS ELECTRONICS N.V. 2019
// All rights are reserved. Reproduction in whole or in part is
// prohibited without the written consent of the copyright owner.
//
//============================================================================ 
using VitalSignContractLib;

namespace GeneratePatientVitalSignContractLib
{
    public interface IVitalSignGenerator
    {
        VitalSignType VitalSignType { get; }
        double PatientVitalSignGenerator(string patientId);
    }
}
