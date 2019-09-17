//============================================================================
//
// COPYRIGHT KONINKLIJKE PHILIPS ELECTRONICS N.V. 2019
// All rights are reserved. Reproduction in whole or in part is
// prohibited without the written consent of the copyright owner.
//
//============================================================================ 

using System.Collections.Generic;
using EnableVitalSignContractLib;
using VitalSignLib;
using DataAccessContractLib;

namespace EnableVitalSignLib
{
    //This class will have information of enabled of disable vitals sign.
    public class EnableVitalSign : IEnableVitalSign
    {
        readonly IDataAccess m_dataAccess = null;
        public EnableVitalSign(IDataAccess dataAccess)
        {
            m_dataAccess = dataAccess;
        }
        public void EnableVitalSignForPatient(string patientId, List<VitalSign> m_vitalSigns)
        {
            m_dataAccess.EnableVitalSignForPatient(patientId, m_vitalSigns);
           
        }
    }
}
