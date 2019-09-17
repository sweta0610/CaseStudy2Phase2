//============================================================================
//
// COPYRIGHT KONINKLIJKE PHILIPS ELECTRONICS N.V. 2019
// All rights are reserved. Reproduction in whole or in part is
// prohibited without the written consent of the copyright owner.
//
//============================================================================ 
using System.Runtime.Serialization;
using System.ServiceModel;

namespace VitalSignContractLib
{
    [DataContract]
    public enum VitalSignType
    {
        [EnumMember]
        None,
        [EnumMember]
        SPO2,
        [EnumMember]
        PulseRate,
        [EnumMember]
        Temperature,
    }
    [ServiceContract]
    public interface IVitalsSign
    {
        [DataMember]
        VitalSignType VitalSignType { get; set; }

        [DataMember]
        bool IsPatientVitalSignEnabled { get; set; }
    }
}
