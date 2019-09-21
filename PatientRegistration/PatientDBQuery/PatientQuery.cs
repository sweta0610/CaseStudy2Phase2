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
using DBManagerLib;
using System.Data.Common;
using System.Runtime.Serialization;
using DataContractsLib;

namespace PatientDBQuery
{
    public static class PatientQuery
    {
        //this function takes contactNumber as parameter and returns patientID for that.
        public static string SearchPatientByContact(long contactNumber)
        {
            string patientId = string.Empty;
            string selectQuery = "select PatientID from PatientData where ContactNumber=" + contactNumber;

            DBManager dbManager = new DBManager();

            dbManager.OpenDBConnection();

            DbCommand command = dbManager.CreateDBCommand(selectQuery);

            using (DbDataReader dataReader = dbManager.ExecuteCommand(command))
            {
                while (dataReader.Read())
                {
                    patientId = dataReader["PatientID"].ToString();
                }
            }

            dbManager.CloseDBConnection();

            return patientId;
        }

        //this function takes patientId and searches in PatientData table then returns true or false
        public static bool SearchPatientByPatientId(int patientId)
        {
            bool isPatientExist = false;
            string selectQuery = "select * from PatientData where PatientID=" + patientId;

            DBManager dbManager = new DBManager();

            dbManager.OpenDBConnection();

            DbCommand command = dbManager.CreateDBCommand(selectQuery);

            using (DbDataReader dataReader = dbManager.ExecuteCommand(command))
            {
                while (dataReader.Read())
                {
                    isPatientExist = true;
                }
            }

            dbManager.CloseDBConnection();

            return isPatientExist;
        }

        public static PatientDetails GetPatientDetails(int patientID)
        {
            PatientDetails details = new PatientDetails();
            if(SearchPatientByPatientId(patientID))
            {
                string Query = "SELECT * FROM PatientData WHERE PatientID=" + patientID;

                DBManager dbManager = new DBManager();

                dbManager.OpenDBConnection();

                DbCommand command = dbManager.CreateDBCommand(Query);

                using (DbDataReader dataReader = dbManager.ExecuteCommand(command))
                {
                    while (dataReader.Read())
                    {
                        //details = dataReader["PatientName"].ToString() + ";" + dataReader["PatientGender"].ToString() + ";" + dataReader["PatientAge"].ToString() + ";" + dataReader["ContactNumber"].ToString();
                        details.PatientName = dataReader["PatientName"].ToString();
                        details.PatientGender = dataReader["PatientGender"].ToString();
                        details.PatientAge = int.Parse(dataReader["PatientAge"].ToString());
                        details.PatientContact = long.Parse(dataReader["ContactNumber"].ToString());
                        details.PatientID = patientID;
                    }
                }

                dbManager.CloseDBConnection();
            }
            return details;
        }

        //this function checks whether contactNo exists or not
        public static bool IsContactNumberExists(long contactNumber)
        {
            if(SearchPatientByContact(contactNumber) == string.Empty)
            {
                return false;
            }
            return true;
        }

        //adds entry patient details to the PatientData table 
        public static string RegisterPatient(long contactNumber, string patientName, string patientGender, double PatientAge)
        {
            string patientId = string.Empty;
            if (!IsContactNumberExists(contactNumber))
            {
                string insertQuery = "INSERT INTO PatientData (ContactNumber,PatientName, PatientGender, PatientAge)  VALUES(" + "'" + contactNumber + "'," + "'" + patientName + "'," + "'" + patientGender + "'," + PatientAge + ")";
                DBManager dbManager = new DBManager();

                dbManager.OpenDBConnection();

                DbCommand command = dbManager.CreateDBCommand(insertQuery);
                dbManager.ExecuteCommand(command);

                patientId = SearchPatientByContact(contactNumber);

                dbManager.CloseDBConnection();
            }

            return patientId;
        }
          
        //this function takes patientID as parameter and checks resources have been allocated to the patient or not.
        public static bool IsPatientAllocated(int patientID)
        {
            bool isPatientAllocated = false;
            string searchQuery = "select * from PatientResourceAllocationData where PatientID = " + patientID + " AND Status = " + 1;
            DBManager dbManager = new DBManager();

            dbManager.OpenDBConnection();

            DbCommand command = dbManager.CreateDBCommand(searchQuery);

            using (DbDataReader dataReader = dbManager.ExecuteCommand(command))
            {
               if(dataReader.Read())
                {
                    isPatientAllocated = true;
                }
            }

            dbManager.CloseDBConnection();
            return isPatientAllocated;
        }

        //this function adds resources to the AllocateResourceToPatient table
        public static bool AllocateResourceToPatient(long contactNumber,int patientId,string doctorName,string category,string disease,int wardNumber,int bedNumber,string indate,long emergencyContactNumber,string outdate,bool status )
        {
            bool bSuccess = false;
            DBManager dbManager = new DBManager();
                //Patient Exists && not allocated 
            if (SearchPatientByPatientId(patientId) 
                && !IsPatientAllocated(patientId))
            {
                string insertQuery = "INSERT INTO PatientResourceAllocationData (ContactNumber,PatientID, DoctorName,Category,Disease,WardNumber,BedNumber,indate,EmergencyContactNumber,OutDate,Status)  VALUES(" + "'" + contactNumber + "'," + "'" + patientId + "'," + "'" + doctorName + "','" + category + "'," + "'" + disease + "'," + "'" + wardNumber + "'," + "'" + bedNumber + "'," + "'" + indate + "'," + "'" + emergencyContactNumber + "'," + "'" + outdate + "'," + Convert.ToInt32(status) + ")";


                dbManager.OpenDBConnection();
                DbCommand command = dbManager.CreateDBCommand(insertQuery);

                dbManager.ExecuteCommand(command);

                dbManager.CloseDBConnection();

                //Lets make the bed un-available.
                UpdateBedInformation(bedNumber, wardNumber, false);

                bSuccess = true;
            }

            return bSuccess;
        }
        
        //this inserts bed information in the table
        public static void InsertBedInformation(int bedNumber,int wardNumber)
        {
            //Check WardNumber exists in WardData && check bedNumber is not available in bed data

            if (!IsBedExist(bedNumber) && IsWardExist(wardNumber))
            {
                string insertQuery = "INSERT INTO BedData (BedNumber,WardNumber)  VALUES(" + "'" + bedNumber + "'," + "'" + wardNumber + "'" + ")";
                DBManager dbManager = new DBManager();

                dbManager.OpenDBConnection();

                DbCommand command = dbManager.CreateDBCommand(insertQuery);

                dbManager.ExecuteCommand(command);

                dbManager.CloseDBConnection();
            }            
        }

        //checks whether given wardNumber exists  or not
        public static bool IsWardExist(int wardnumber)
        {
            bool isWardExist = false;
            string selectQuery = "select * from WardData where WardNumber=" + wardnumber;

            DBManager dbManager = new DBManager();

            dbManager.OpenDBConnection();

            DbCommand command = dbManager.CreateDBCommand(selectQuery);

            using (DbDataReader dataReader = dbManager.ExecuteCommand(command))
            {
                while (dataReader.Read())
                {
                    isWardExist = true;
                }
            }

            dbManager.CloseDBConnection();

            return isWardExist;
        }

        //checks whether given bedNumber exists or not
        public static bool IsBedExist(int bedNumber)
        {
            bool isBedExist = false;
            string selectQuery = "select * from BedData where BedNumber=" + bedNumber;

            DBManager dbManager = new DBManager();

            dbManager.OpenDBConnection();

            DbCommand command = dbManager.CreateDBCommand(selectQuery);

            using (DbDataReader dataReader = dbManager.ExecuteCommand(command))
            {
                while (dataReader.Read())
                {
                    isBedExist = true;
                }
            }

            dbManager.CloseDBConnection();

            return isBedExist;
        }

        //this function inserts ward data to the table
        public static void InsertWardInformation(string wardType, int wardNumber)
        {
            if (!IsWardExist(wardNumber))
            {
                string insertQuery = "INSERT INTO WardData (WardType,WardNumber)  VALUES(" + "'" + wardType + "'," + "'" + wardNumber + "'" + ")";
                DBManager dbManager = new DBManager();

                dbManager.OpenDBConnection();

                DbCommand command = dbManager.CreateDBCommand(insertQuery);

                dbManager.ExecuteCommand(command);

                dbManager.CloseDBConnection();
            }
        }

        //checks the bed which is available in the bedData
        public static bool GetAvailableBed(string wardType, out int wardNumber, out int bedNumber)
        {
            bedNumber = 0;
            wardNumber = 0;
            bool isAvailableBed = false;
            string query = "select WardNumber, BedNumber from BedData where Availability=" + 1;
            DBManager dbManager = new DBManager();

            dbManager.OpenDBConnection();

            DbCommand command = dbManager.CreateDBCommand(query);

            using (DbDataReader dataReader = dbManager.ExecuteCommand(command))
            {
                while (dataReader.Read())
                {
                    isAvailableBed = true;
                    wardNumber= (int)(dataReader["WardNumber"]);
                    bedNumber = (int)dataReader["BedNumber"];
                }
            }


            dbManager.CloseDBConnection();
            return isAvailableBed;
        }

        //this updates the availability of bed depending on the existance of bed and ward number
        public static void UpdateBedInformation(int bedNumber, int wardNumber, bool Availability)
        {
            if (IsBedExist(bedNumber) && IsWardExist(wardNumber))
            {
                string updateQuery = "Update BedData set Availability = " + Convert.ToInt32(Availability) + " where BedNumber = " + bedNumber + " AND WardNumber = " + wardNumber ;
                DBManager dbManager = new DBManager();

                dbManager.OpenDBConnection();

                DbCommand command = dbManager.CreateDBCommand(updateQuery);

                dbManager.ExecuteCommand(command);

                dbManager.CloseDBConnection();
            }
        }

        /// <summary>
        /// Discharges the patient
        /// </summary>
        /// <param name="patientID"></param>
        public static void Discharge(int patientID)
        {
            //First : Get the bed number
            int bedNumber = QueryBedNumberForActivePatient(patientID);
            // Second : Change the status
            DeallocatePatient(patientID);
            //Third : Change Bed Availability
            UpdateBedAvailability(bedNumber);
        }

        //to get bedNumber
        private static int QueryBedNumberForActivePatient(int patientID)
        {
            int bedNumber = -1;

            DBManager dbManager = new DBManager();

            dbManager.OpenDBConnection();

            string updateQueryToBedData = "select BedNumber from PatientResourceAllocationData where PatientID = " + patientID + " AND Status = " + 1;
            
            DbCommand command = dbManager.CreateDBCommand(updateQueryToBedData);

            using (DbDataReader dataReader = dbManager.ExecuteCommand(command))
            {
                while (dataReader.Read())
                {
                    bedNumber = (int)dataReader["BedNumber"];
                }
            }

            dbManager.CloseDBConnection();

            return bedNumber;
        }

        //change the status in PatientResourceAllocationData to 0
        private static void DeallocatePatient(int patientID)
        {
            string updateQueryToAllocateResourceToPatient = "update PatientResourceAllocationData set Status = " + 0 + " where PatientID = " + patientID;

            DBManager dbManager = new DBManager();

            dbManager.OpenDBConnection();
            
            DbCommand command = dbManager.CreateDBCommand(updateQueryToAllocateResourceToPatient);

            dbManager.ExecuteCommand(command);
            dbManager.CloseDBConnection();
        }

        //change the availability of bed.
        private static void UpdateBedAvailability(int bedNumber)
        {
            string updateQuery = "Update BedData set Availability = " + 1 + " where BedNumber = " + bedNumber;

            DBManager dbManager = new DBManager();

            dbManager.OpenDBConnection();
            
            DbCommand command = dbManager.CreateDBCommand(updateQuery);

            dbManager.ExecuteCommand(command);

            dbManager.CloseDBConnection();
        }

        //checks whether bed is available 
        public static bool IsBedAvailable(int bedNumber)
        {
            bool isBedAvailable = false;
            string updateQuery = "Select Availability from BedData where BedNumber = " + bedNumber;

            DBManager dbManager = new DBManager();

            dbManager.OpenDBConnection();

            DbCommand command = dbManager.CreateDBCommand(updateQuery);

            using (DbDataReader dataReader = dbManager.ExecuteCommand(command))
            {
                while (dataReader.Read())
                {
                    isBedAvailable = bool.Parse(dataReader["Availability"].ToString());
                }
            }

            dbManager.CloseDBConnection();

            return isBedAvailable;
        }

        public static void ClearPatientDB()
        {
            string [] queries = { "DELETE FROM BedData", "DELETE FROM WardData", "DELETE FROM PatientResourceAllocationData", "DELETE FROM PatientData" };

            DBManager dbManager = new DBManager();

            dbManager.OpenDBConnection();
            DbCommand command = null;

            foreach (string query in queries)
            {
                command = dbManager.CreateDBCommand(query);
                dbManager.ExecuteCommand(command);
            }
            dbManager.CloseDBConnection();
        }
    }
}
