//============================================================================
//
// COPYRIGHT KONINKLIJKE PHILIPS ELECTRONICS N.V. 2019
// All rights are reserved. Reproduction in whole or in part is
// prohibited without the written consent of the copyright owner.
//
//============================================================================ 
using System.Collections.Generic;
using System.ServiceModel;
using VitalSignLib;

namespace EnableVitalSignContractLib
{
    [ServiceContract]
    public interface IEnableVitalSign
    {
        [OperationContract]
        void EnableVitalSignForPatient(string patientId, List<VitalSign> m_vitalSigns);
    }
}
