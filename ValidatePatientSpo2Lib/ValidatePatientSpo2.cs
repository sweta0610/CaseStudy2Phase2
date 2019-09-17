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

namespace ValidatePatientSpo2Lib
{   //This class will check the range of spo2 for each patient and gives Alert if spo2 goes out of range.
    public class ValidatePatientSpo2 : IValidateVitalSign
    {
        public VitalSignType VitalSignType
        {
            get
            {
                return VitalSignType.SPO2;
            }
        }
        //This will check the range of spo2
        public bool IsVitalSignInRange(int m_value)
        {
            if (m_value <= Constant.MAX_SPO2 && m_value >= Constant.MIN_SPO2)
            {
                return true;
            }
            return false;
        }
        //This will give alert when spo2 goes out of range.
        public string ValidateVitalSign(string patientId, string m_vitalSignValue)
        {
            int m_spo2 = Convert.ToInt32(m_vitalSignValue);
            if (!IsVitalSignInRange(m_spo2))
            {
                Console.WriteLine("Emergency situation, alerting nurse!!!");
                return "Alert!!! SPO2 not in range SPO2: " + m_spo2 + " for patient: " + patientId + "";
            }
            return null;
        }
    }
}
