using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PatientDBQuery;
using PatientDbQueryControllerContractLib;

namespace PatientDBQueryControllerLib
{
    public class PatientDBQueryController : IPatientDbQuery
    {
        PatientQuery query = new PatientQuery();

        public string SearchPatientByContact(long contactNumber)
        {
            return query.SearchPatientByContact(contactNumber);
        }
        public bool SearchPatientByPatientId(int patientId)
        {
            return query.SearchPatientByPatientId(patientId);
        }
        public void RegisterPatient(long contactNumber, string patientName, string patientGender, double PatientAge)
        {
            query.RegisterPatient(contactNumber, patientName, patientGender, PatientAge);
        }
        public bool IsPatientAllocated(int patientID)
        {
            return query.IsPatientAllocated(patientID);
        }
        public bool AllocateResourceToPatient(long contactNumber, int patientId, string doctorName, string category, string disease, int wardNumber, int bedNumber, string indate, long emergencyContactNumber, string outdate, bool status)
        {
            return query.AllocateResourceToPatient(contactNumber, patientId, doctorName, category, disease, wardNumber, bedNumber, indate, emergencyContactNumber, outdate, status);
        }
        public void InsertBedInformation(int bedNumber, int wardNumber)
        {
             query.InsertBedInformation(bedNumber, wardNumber);
        }
        public bool IsWardExist(int wardnumber)
        {
            return query.IsWardExist(wardnumber);
        }
        public bool IsBedExist(int bedNumber)
        {
            return query.IsBedExist(bedNumber);
        }
        public void InsertWardInformation(string wardType, int wardNumber)
        {
            query.InsertWardInformation(wardType, wardNumber);
        }
        public bool GetAvailableBed(string wardType, out int wardNumber, out int bedNumber)
        {
            return query.GetAvailableBed(wardType, out wardNumber, out bedNumber);
        }
        public void UpdateBedInformation(int bedNumber, int wardNumber, bool Availability)
        {
            query.UpdateBedInformation(bedNumber, wardNumber, Availability);
        }
        public void Discharge(int patientID)
        {
            query.Discharge(patientID);
        }
    }
}
