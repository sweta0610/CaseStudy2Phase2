using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataContractsLib
{
    [DataContract]
    public class PatientDetails
    {
        [DataMember]
        public int PatientID;
        [DataMember]
        public string PatientName;
        [DataMember]
        public string PatientGender;
        [DataMember]
        public int PatientAge;
        [DataMember]
        public long PatientContact;
    }
    
}
