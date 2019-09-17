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

namespace PatientPulserateGeneratorLib
{   //This class will generate value of pulse rate.
    public class PulserateGenerator : IVitalSignGenerator
    {
        public VitalSignType VitalSignType
        {
            get
            {
                return VitalSignType.PulseRate;
            }
        }
        //This function will randomly gives data for pulse rate in a particular range
        private double RandomizeDouble(double nMin, double nMax)
        {
            Random rand = new Random();
            return rand.Next((int)nMin, (int)nMax);
        }
        public double PatientVitalSignGenerator(string patientId)
        {
            double vitalSignValue = 0;
            vitalSignValue = RandomizeDouble(60 - 1, 100);
            return vitalSignValue;
        }
    }
}
