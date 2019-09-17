//============================================================================
//
// COPYRIGHT KONINKLIJKE PHILIPS ELECTRONICS N.V. 2019
// All rights are reserved. Reproduction in whole or in part is
// prohibited without the written consent of the copyright owner.
//
//============================================================================ 
using System.Collections.Generic;
using VitalSignLib;

namespace DataStoreLib
{
    //It is the Data Storage 
    //It store information for enabled vital signs.
    //It store generated vital signs for patient.
    public static class DataStore
    {
        private static readonly Dictionary<string, Queue<string>> dictPatientDataMap = new Dictionary<string, Queue<string>>();
        //This will store list of enabled vital sign along with patient Id.
        public static readonly Dictionary<string, List<VitalSign>> dictPatientVitalSignEnabledMap = new Dictionary<string, List<VitalSign>>();

        //lstDefaultVitalSign will by default set all vital sign to true.
        readonly static List<VitalSign> lstDefaultVitalSign = new List<VitalSign>() {
                    new VitalSign() { VitalSignType = VitalSignContractLib.VitalSignType.SPO2, IsPatientVitalSignEnabled = true},
                new VitalSign() { VitalSignType = VitalSignContractLib.VitalSignType.Temperature, IsPatientVitalSignEnabled = true},
                new VitalSign() { VitalSignType = VitalSignContractLib.VitalSignType.PulseRate, IsPatientVitalSignEnabled = true}};

        public static Dictionary<string, Queue<string>> DictPatientDataMap
        {
            get
            {
                return dictPatientDataMap;
            }
        }

        public static List<VitalSign> LstDefaultVitalSign
        {
            get
            {
                return lstDefaultVitalSign;
            }
        }

    }
}
