﻿//============================================================================
//
// COPYRIGHT KONINKLIJKE PHILIPS ELECTRONICS N.V. 2019
// All rights are reserved. Reproduction in whole or in part is
// prohibited without the written consent of the copyright owner.
//
//============================================================================ 
using PatientVitalSignWriterLib;
using DataStoreControllerContractLib;
using PatientDbQueryControllerContractLib;
using System;
using PatientDBQuery;
using FactoryLib;
using PatientVitalSignWriterContractLib;
using DataContractsLib;

namespace DataStoreControllerLib
{
    /// <summary>
    ///  DataStore service, it exposes StorePatientVitalSigns.
    ///  This will store patient Vital as Json format in data storage 
    /// </summary>
    public class DataStoreController : IDataStore, IPatientDbQuery
    {
        readonly IPatientVitalSignWriter m_patientVitalWriter = null;
       
        public DataStoreController()
        {
            m_patientVitalWriter = Factory.GetVitalSignWriter();
        }

        public bool AllocateResourceToPatient(long contactNumber, int patientId, string doctorName, string category, string disease, int wardNumber, int bedNumber, string indate, long emergencyContactNumber, string outdate, bool status)
        {
           return PatientQuery.AllocateResourceToPatient(contactNumber, patientId, doctorName, category, disease, wardNumber, bedNumber, indate, emergencyContactNumber, outdate, status);
        }

        public void Discharge(int patientID)
        {
            PatientQuery.Discharge(patientID);
        }

        public bool GetAvailableBed(string wardType, out int wardNumber, out int bedNumber)
        {
            return PatientQuery.GetAvailableBed(wardType, out wardNumber, out bedNumber);
        }

        public void InsertBedInformation(int bedNumber, int wardNumber)
        {
            PatientQuery.InsertBedInformation(bedNumber, wardNumber);
        }

        public void InsertWardInformation(string wardType, int wardNumber)
        {
            PatientQuery.InsertWardInformation(wardType, wardNumber);
        }

        public bool IsBedAvailable(int bedNumber)
        {
            return PatientQuery.IsBedAvailable(bedNumber);
        }

        public bool IsBedExist(int bedNumber)
        {
            return PatientQuery.IsBedExist(bedNumber);
        }

        public bool IsContactNumberExists(long contactNumber)
        {
            return PatientQuery.IsContactNumberExists(contactNumber);
        }

        public bool IsPatientAllocated(int patientID)
        {
            return PatientQuery.IsPatientAllocated(patientID);
        }

        public bool IsWardExist(int wardnumber)
        {
            return PatientQuery.IsWardExist(wardnumber);
        }

        public string RegisterPatient(long contactNumber, string patientName, string patientGender, int PatientAge)
        {
            return PatientQuery.RegisterPatient(contactNumber, patientName, patientGender, PatientAge);
        }

        public int GetAdmittedPatientID(int BedNumber)    //GetPatientId
        {
            return PatientQuery.GetAdmittedPatientID(BedNumber);
        }
        public string GetPatientID(long contactNumber)    //GetPatientId
        {
            return PatientQuery.SearchPatientByContact(contactNumber);
        }

        public bool IsPatientExists(int patientId)     //IsPatientExists
        {
            return PatientQuery.SearchPatientByPatientId(patientId);
        }
        public PatientDetails GetPatientDetails(int patientId)     //new
        {
            return PatientQuery.GetPatientDetails(patientId);
        }
        public bool UpdatePatientDetails(int patientId, string patientName, string patientGender, int PatientAge, long contactNumber)                                            //new
        {
            return PatientQuery.UpdatePatientDetails(patientId,patientName,patientGender,PatientAge,contactNumber);
        }
        public int GetTotalNoOfBeds()     //new
        {
            return PatientQuery.GetTotalNoOfBeds();
        }
        public int GetTotalNoOfWards()     //new
        {
            return PatientQuery.GetTotalNoOfWards();
        }
        public int GetNoOfAvailableBeds()     //new
        {
            return PatientQuery.GetNoOfAvailableBeds();
        }

        public void StorePatientVitalSigns(string patientId, string m_jsonData)
        {
            m_patientVitalWriter.StorePatientVitalSigns(patientId, m_jsonData);
        }

        /// <summary>
        /// Clears data from all the tables of DB
        /// </summary>
        public void ClearPatientDB()
        {
            PatientQuery.ClearPatientDB();
        }

        public void UpdateBedInformation(int bedNumber, int wardNumber, bool Availability)
        {
            PatientQuery.UpdateBedInformation(bedNumber, wardNumber, Availability);
        }
    }
}
