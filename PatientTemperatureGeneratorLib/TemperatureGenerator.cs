//============================================================================
//
// COPYRIGHT KONINKLIJKE PHILIPS ELECTRONICS N.V. 2019
// All rights are reserved. Reproduction in whole or in part is
// prohibited without the written consent of the copyright owner.
//
//============================================================================ 
using System;
using GeneratePatientVitalSignContractLib;
using VitalSignContractLib;

namespace PatientTemperatureGeneratorLib
{
    //This class will generate value of Temperature
    public class TemperatureGenerator : IVitalSignGenerator
    {
        public VitalSignType VitalSignType
        {
            get
            {
                return VitalSignType.Temperature;
            }
        }
        private double RandomizeDouble(double m_nMin, double m_nMax)
        {
            Random m_rand = new Random();
            return m_rand.Next((int)m_nMin, (int)m_nMax);
        }
        public double PatientVitalSignGenerator(string patientId)
        {
            double m_vitalSignValue = 0;
            m_vitalSignValue = RandomizeDouble(97, 99);
            return m_vitalSignValue;
        }
    }
}
