//============================================================================
//
// COPYRIGHT KONINKLIJKE PHILIPS ELECTRONICS N.V. 2019
// All rights are reserved. Reproduction in whole or in part is
// prohibited without the written consent of the copyright owner.
//
//============================================================================ 

using System;
using ValidatePatientVitalSignContractLib;
using ConstantLib;
using VitalSignContractLib;

namespace ValidatePatientPulseRateLib
{
    //This class will check pulse rate in range or not.
    //It will give alert if pulse rate goes out of range.
    public class ValidatePatientPulseRate : IValidateVitalSign
    {
        public VitalSignType VitalSignType
        {
            get
            {
                return VitalSignType.PulseRate;
            }
        }
        public bool IsVitalSignInRange(int m_value)
        {
            if (m_value <= Constant.MAX_PULSE_RATE && m_value >= Constant.MIN_PULSE_RATE)
            {
                return true;
            }
            return false;
        }

        public string ValidateVitalSign(string patientId, string m_vitalSignValue)
        {
            int m_PulseRate = Convert.ToInt32(m_vitalSignValue);
            if (!IsVitalSignInRange(m_PulseRate))
            {
                Console.WriteLine("Emergency situation, alerting nurse!!!");
                return "Alert!!! Pulse Rate not in range Pulse Rate: " + m_PulseRate + " for patient: " + patientId + "";
            }
            return "";
        }
    }
}
