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

namespace PatientSpo2GeneratorLib
{
    //It will generate and give value of spo2
    public class Spo2Generator : IVitalSignGenerator
    {
        public VitalSignType VitalSignType
        {
            get
            {
                return VitalSignType.SPO2;
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
            m_vitalSignValue = RandomizeDouble(95 - 1, 100);
            return m_vitalSignValue;
        }
    }
}
