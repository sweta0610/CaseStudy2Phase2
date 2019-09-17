//============================================================================
//
// COPYRIGHT KONINKLIJKE PHILIPS ELECTRONICS N.V. 2019
// All rights are reserved. Reproduction in whole or in part is
// prohibited without the written consent of the copyright owner.
//
//============================================================================ 
using System.ServiceModel;
using VitalSignContractLib;

namespace ValidatePatientVitalSignContractLib
{
    public interface IValidateVitalSign
    {
        VitalSignType VitalSignType { get; }
        string ValidateVitalSign(string patientId, string m_vitalSignValue);
    }
}
