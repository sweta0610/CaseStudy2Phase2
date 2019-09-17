using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnablePatientVitalSignsLib
{
    public class EnablePatientVitalSigns
    {
        public void EnableVitalSignForPatient(string patientId, List<VitalSign> vitalSigns)
        {
            if (dictPatientVitalSignEnabledMap.ContainsKey(patientId))
            {
                dictPatientVitalSignEnabledMap.Remove(patientId);
            }
            dictPatientVitalSignEnabledMap.Add(patientId, vitalSigns);
        }
    }
}
