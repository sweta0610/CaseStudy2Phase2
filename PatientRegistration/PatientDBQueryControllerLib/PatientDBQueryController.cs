//============================================================================
//
// COPYRIGHT KONINKLIJKE PHILIPS ELECTRONICS N.V. 2019
// All rights are reserved. Reproduction in whole or in part is
// prohibited without the written consent of the copyright owner.
//
//============================================================================ 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PatientDBQuery;
using PatientDbQueryControllerContractLib;

namespace PatientDBQueryControllerLib
{
    /// <summary>
    /// Class to Query DB
    /// </summary>
    public class PatientDBQueryController : IPatientDbQuery
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PatientDBQueryController()
        {
        }

        /// <summary>
        /// this function takes contactNumber as parameter and returns patientID for that.
        /// </summary>
        /// <param name="contactNumber"></param>
        /// <returns></returns>
        public string SearchPatientByContact(long contactNumber)
        {
            return PatientQuery.SearchPatientByContact(contactNumber);
        }
        /// <summary>
        /// this function takes patientId and searches in PatientData table then returns true or false
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public bool SearchPatientByPatientId(int patientId)
        {
            return PatientQuery.SearchPatientByPatientId(patientId);
        }

        /// <summary>
        /// this function checks whether contactNo exists or not
        /// </summary>
        /// <param name="contactNumber"></param>
        /// <returns></returns>
        public bool IsContactNumberExists(long contactNumber)
        {
            return PatientQuery.IsContactNumberExists(contactNumber);
        }

        /// <summary>
        /// adds entry patient details to the PatientData table
        /// </summary>
        /// <param name="contactNumber"></param>
        /// <param name="patientName"></param>
        /// <param name="patientGender"></param>
        /// <param name="PatientAge"></param>
        /// <returns></returns>
        public string RegisterPatient(long contactNumber, string patientName, string patientGender, double PatientAge)
        {
            return PatientQuery.RegisterPatient(contactNumber, patientName, patientGender, PatientAge);
        }
        /// <summary>
        /// this function takes patientID as parameter and checks resources have been allocated to the patient or not.
        /// </summary>
        /// <param name="patientID"></param>
        /// <returns></returns>
        public bool IsPatientAllocated(int patientID)
        {
            return PatientQuery.IsPatientAllocated(patientID);
        }

        /// <summary>
        /// this function adds resources to the AllocateResourceToPatient table
        /// </summary>
        /// <param name="contactNumber"></param>
        /// <param name="patientId"></param>
        /// <param name="doctorName"></param>
        /// <param name="category"></param>
        /// <param name="disease"></param>
        /// <param name="wardNumber"></param>
        /// <param name="bedNumber"></param>
        /// <param name="indate"></param>
        /// <param name="emergencyContactNumber"></param>
        /// <param name="outdate"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool AllocateResourceToPatient(long contactNumber, int patientId, string doctorName, string category, string disease, int wardNumber, int bedNumber, string indate, long emergencyContactNumber, string outdate, bool status)
        {
            return PatientQuery.AllocateResourceToPatient(contactNumber, patientId, doctorName, category, disease, wardNumber, bedNumber, indate, emergencyContactNumber, outdate, status);
        }

        /// <summary>
        /// this inserts bed information in the table
        /// </summary>
        /// <param name="bedNumber"></param>
        /// <param name="wardNumber"></param>
        public void InsertBedInformation(int bedNumber, int wardNumber)
        {
            PatientQuery.InsertBedInformation(bedNumber, wardNumber);
        }

        /// <summary>
        /// checks whether given wardNumber exists  or not
        /// </summary>
        /// <param name="wardnumber"></param>
        /// <returns></returns>
        public bool IsWardExist(int wardnumber)
        {
            return PatientQuery.IsWardExist(wardnumber);
        }

        /// <summary>
        /// checks whether given bedNumber exists or not
        /// </summary>
        /// <param name="bedNumber"></param>
        /// <returns></returns>
        public bool IsBedExist(int bedNumber)
        {
            return PatientQuery.IsBedExist(bedNumber);
        }

        /// <summary>
        /// this function inserts ward data to the table
        /// </summary>
        /// <param name="wardType"></param>
        /// <param name="wardNumber"></param>
        public void InsertWardInformation(string wardType, int wardNumber)
        {
            PatientQuery.InsertWardInformation(wardType, wardNumber);
        }

        /// <summary>
        /// checks the bed which is available in the bedData
        /// </summary>
        /// <param name="wardType"></param>
        /// <param name="wardNumber"></param>
        /// <param name="bedNumber"></param>
        /// <returns></returns>
        public bool GetAvailableBed(string wardType, out int wardNumber, out int bedNumber)
        {
            return PatientQuery.GetAvailableBed(wardType, out wardNumber, out bedNumber);
        }

        /// <summary>
        /// this updates the availability of bed depending on the existance of bed and ward number
        /// </summary>
        /// <param name="bedNumber"></param>
        /// <param name="wardNumber"></param>
        /// <param name="Availability"></param>
        public void UpdateBedInformation(int bedNumber, int wardNumber, bool Availability)
        {
            PatientQuery.UpdateBedInformation(bedNumber, wardNumber, Availability);
        }

        /// <summary>
        /// Discharges the patient
        /// </summary>
        /// <param name="patientID"></param>
        public void Discharge(int patientID)
        {
            PatientQuery.Discharge(patientID);
        }

        /// <summary>
        /// checks whether bed is available
        /// </summary>
        /// <param name="bedNumber"></param>
        /// <returns></returns>
        public bool IsBedAvailable(int bedNumber)
        {
            return PatientQuery.IsBedAvailable(bedNumber);
        }
        /// <summary>
        /// Clears data from all the tables of DB
        /// </summary>
        public void ClearPatientDB()
        {
            PatientQuery.ClearPatientDB();
        }
    }
}
