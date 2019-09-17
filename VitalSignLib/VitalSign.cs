//============================================================================
//
// COPYRIGHT KONINKLIJKE PHILIPS ELECTRONICS N.V. 2019
// All rights are reserved. Reproduction in whole or in part is
// prohibited without the written consent of the copyright owner.
//
//============================================================================ 
using System.Runtime.Serialization;
using VitalSignContractLib;

namespace VitalSignLib
{
    //This gives vital sign type using enum i.e spo2,temperature,pulserate
    //It will check what vital signs are enabled for patient 
    [DataContract]
    public class VitalSign : IVitalsSign
    {
        [DataMember]
        public VitalSignType VitalSignType { get; set; }
        [DataMember]
        public bool IsPatientVitalSignEnabled { get; set; }
       
    }
}
