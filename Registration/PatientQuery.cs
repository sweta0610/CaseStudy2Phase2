using System;
using DBManagerLib;
using System.Data.Common;

namespace PatientDBQuery
{
    public class PatientQuery
    {
        public string SearchPatientByContact(long contactNumber)
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
                    Console.WriteLine(patientId);
                }
            }

            dbManager.CloseDBConnection();

            return patientId;
        }

        public bool SearchPatientByPatientId(int patientId)
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
                    break;
                }
            }

            dbManager.CloseDBConnection();

            return isPatientExist;
        }

        public void RegisterPatient(long contactNumber, string patientName, string patientGender, double PatientAge)
        {
            
            string insertQuery= "INSERT INTO PatientData (ContactNumber,PatientName, PatientGender, PatientAge)  VALUES(" + "'" + contactNumber + "'," + "'" + patientName + "'," + "'" + patientGender + "'," + PatientAge + ")";
            DBManager dbManager = new DBManager();

            dbManager.OpenDBConnection();

            DbCommand command = dbManager.CreateDBCommand(insertQuery);
            try
            {
                using (DbDataReader dataReader = dbManager.ExecuteCommand(command))
                {
                }

            } catch(Exception)
            {
                Console.WriteLine("duplicate entries for contact number");
            }


            dbManager.CloseDBConnection();
        }

        public bool IsPatientAllocated(int patientID)
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

        public bool AllocateResourceToPatient(long contactNumber,int patientId,string doctorName,string category,string disease,int wardNumber,int bedNumber,string indate,long emergencyContactNumber,string outdate,bool status )
        {
            bool bSuccess = false;
            int statusInBit = status ? 1 : 0;
            DBManager dbManager = new DBManager();
                //Patient Exists && not allocated 
                if (SearchPatientByPatientId(patientId) && !IsPatientAllocated(patientId))
                {
                    string insertQuery = "INSERT INTO PatientResourceAllocationData (ContactNumber,PatientID, DoctorName,Category,Disease,WardNumber,BedNumber,indate,EmergencyContactNumber,OutDate,Status)  VALUES(" + "'" + contactNumber + "'," + "'" + patientId + "'," + "'" + doctorName + "','" + category + "'," + "'" + disease + "'," + "'" + wardNumber + "'," + "'" + bedNumber + "'," + "'" + indate + "'," + "'" + emergencyContactNumber + "'," + "'" + outdate + "'," + statusInBit + ")";


                    dbManager.OpenDBConnection();

                    DbCommand command = dbManager.CreateDBCommand(insertQuery);


                    using (DbDataReader dataReader = dbManager.ExecuteCommand(command))
                    {
                    }

                    dbManager.CloseDBConnection();

                    //Lets make the bed un-available.
                    UpdateBedInformation(bedNumber, wardNumber, false);

                    bSuccess = true;
                }
           // }

            return bSuccess;
        }
        
        public void InsertBedInformation(int bedNumber,int wardNumber)
        {
            //Check WardNumber exists in WardData && check bedNumber is not available in bed data

            if (!IsBedExist(bedNumber) && IsWardExist(wardNumber))
            {
                string insertQuery = "INSERT INTO BedData (BedNumber,WardNumber)  VALUES(" + "'" + bedNumber + "'," + "'" + wardNumber + "'" + ")";
                DBManager dbManager = new DBManager();

                dbManager.OpenDBConnection();

                DbCommand command = dbManager.CreateDBCommand(insertQuery);

                using (DbDataReader dataReader = dbManager.ExecuteCommand(command))
                {
                }

                dbManager.CloseDBConnection();
            }            
        }

        public bool IsWardExist(int wardnumber)
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
                    break;
                }
            }

            dbManager.CloseDBConnection();

            return isWardExist;
        }

        public bool IsBedExist(int bedNumber)
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
                    break;
                }
            }

            dbManager.CloseDBConnection();

            return isBedExist;
        }

        public void InsertWardInformation(string wardType, int wardNumber)
        {
            if (!IsWardExist(wardNumber))
            {
                string insertQuery = "INSERT INTO WardData (WardType,WardNumber)  VALUES(" + "'" + wardType + "'," + "'" + wardNumber + "'" + ")";
                DBManager dbManager = new DBManager();

                dbManager.OpenDBConnection();

                DbCommand command = dbManager.CreateDBCommand(insertQuery);

                using (DbDataReader dataReader = dbManager.ExecuteCommand(command))
                {
                }

                dbManager.CloseDBConnection();
            }
        }

        public bool GetAvailableBed(string wardType, out int wardNumber, out int bedNumber)
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

        public void UpdateBedInformation(int bedNumber, int wardNumber, bool Availability)
        {
            if (IsBedExist(bedNumber) && IsWardExist(wardNumber))
            {
                string updateQuery = "Update BedData set Availability = " + Availability + "where BedNumber = " + bedNumber + "WardNumber = " + wardNumber ;
                DBManager dbManager = new DBManager();

                dbManager.OpenDBConnection();

                DbCommand command = dbManager.CreateDBCommand(updateQuery);

                using (DbDataReader dataReader = dbManager.ExecuteCommand(command))
                {
                }

                dbManager.CloseDBConnection();
            }
        }

        public void Discharge(int patientID)
        {
            string updateQueryToAllocateResourceToPatient = "update PatientResourceAllocationData set Status = " + 0 + " where PatientID = " + patientID;
            DBManager dbManager = new DBManager();

            dbManager.OpenDBConnection();

            DbCommand command = dbManager.CreateDBCommand(updateQueryToAllocateResourceToPatient);

            using (DbDataReader dataReader = dbManager.ExecuteCommand(command))
            {
            }
            dbManager.CloseDBConnection();
            //string updateQueryToBedData = "update BedData set Availability = " + 1;
            //DBManager dbManager1 = new DBManager();

            //dbManager1.OpenDBConnection();

            //DbCommand command1 = dbManager.CreateDBCommand(updateQueryToBedData);

            //using (DbDataReader dataReader = dbManager.ExecuteCommand(command1))
            //{
            //}

            //dbManager.CloseDBConnection();
        }
    }
}
