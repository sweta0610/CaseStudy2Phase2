using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace PatientDbQueryControllerContractLib
{
    [ServiceContract]
    public interface IPatientDbQuery
    {
        [OperationContract]
        string SearchPatientByContact(long contactNumber);
        [OperationContract]
        bool SearchPatientByPatientId(int patientId);
        [OperationContract]
        void RegisterPatient(long contactNumber, string patientName, string patientGender, double PatientAge);
        [OperationContract]
        bool IsPatientAllocated(int patientID);
        [OperationContract]
        bool AllocateResourceToPatient(long contactNumber, int patientId, string doctorName, string category, string disease, int wardNumber, int bedNumber, string indate, long emergencyContactNumber, string outdate, bool status);
        [OperationContract]
        void InsertBedInformation(int bedNumber, int wardNumber);
        [OperationContract]
        bool IsWardExist(int wardnumber);
        [OperationContract]
        bool IsBedExist(int bedNumber);
        [OperationContract]
        void InsertWardInformation(string wardType, int wardNumber);
        [OperationContract]
        bool GetAvailableBed(string wardType, out int wardNumber, out int bedNumber);
        [OperationContract]
        void UpdateBedInformation(int bedNumber, int wardNumber, bool Availability);
        [OperationContract]
        void Discharge(int patientID);
    }
}
