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

namespace ValidatePatientTemperatureLib
{
    //This class will check the range of temperature and gives alert if temperature goes out of range.
    public class ValidatePatientTemperature : IValidateVitalSign
    {
        public VitalSignType VitalSignType
        {
            get
            {
                return VitalSignType.Temperature;
            }
        }
        //This will check range of Temperature for patient
        public bool IsVitalSignInRange(int m_value)
        {
            if (m_value <= Constant.MAX_TEMPERATURE && m_value >= Constant.MIN_TEMPERATURE)
            {
                return true;
            }
            return false;
        }
        //This will give alert if temperature goes out of range for patient with id patientId
        public string ValidateVitalSign(string patientId, string m_vitalSignValue)
        {
            int m_temperature = Convert.ToInt32(m_vitalSignValue);
            if (!IsVitalSignInRange(m_temperature))
            {
                Console.WriteLine("Emergency situation, alerting nurse!!!");
                return "Alert!!! Temperature not in range Temperature: " + m_temperature + " for patient " + patientId + "";
            }
            return "";
        }
    }
}
