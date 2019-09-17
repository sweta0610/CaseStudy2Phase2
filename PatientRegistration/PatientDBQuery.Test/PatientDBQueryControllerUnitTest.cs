//============================================================================
//
// COPYRIGHT KONINKLIJKE PHILIPS ELECTRONICS N.V. 2019
// All rights are reserved. Reproduction in whole or in part is
// prohibited without the written consent of the copyright owner.
//
//============================================================================ 
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using PatientDBQueryControllerLib;

namespace PatientDBQuery.Test
{
    [TestClass]
    public class PatientDBQueryControllerUnitTest
    {
        PatientDBQueryController patientDBQueryController = new PatientDBQueryController();

        [SetUp]
        public void ClearDB()
        {
            patientDBQueryController.ClearPatientDB();
        }
        
        [TestMethod]
        public void Given_Valid_ContactNumber_When_SearchPatientByContact_Is_Invoked_Then_Validate_Result()
        {
            string patientId = patientDBQueryController.RegisterPatient(9663625749, "Ashritha", "Female", 23);
            string actualValue = patientDBQueryController.SearchPatientByContact(9663625749);
            string expectedValue = patientId;
            Assert.AreEqual(expectedValue, actualValue);
        }
        [TestMethod]
        public void Given_Valid_PatientID_When_SearchPatientByID_Is_Invoked__Then_Validate_Result()
        {
            string patientId = patientDBQueryController.RegisterPatient(9663625749, "Ashritha", "Female", 23);
            bool actualValue = patientDBQueryController.SearchPatientByPatientId(int.Parse(patientId));
            bool expectedValue = true;
            Assert.AreEqual(expectedValue, actualValue);
        }
        [TestMethod]
        public void Given_Valid_ContactNumber_When_IsContactNumberExists_Is_Invoked_Then_Validate_Result()
        {
            patientDBQueryController.RegisterPatient(9663625749, "Ashritha", "Female", 23);
            bool actualValue = patientDBQueryController.IsContactNumberExists(9663625749);
            bool expectedValue = true;
            Assert.AreEqual(expectedValue, actualValue);
        }
        [TestMethod]
        public void Given_Valid_PatientData_When_RegisterPatient_Is_Invoked_Then_Validate_Result()
        {
           string actualValue = patientDBQueryController.RegisterPatient(9663625744,"ash","female",22);
           string expectedValue = patientDBQueryController.SearchPatientByContact(9663625744);
            Assert.AreEqual(expectedValue, actualValue);
        }
        [TestMethod]
        public void Given_Valid_patientID_When_IsPatientAllocated_Is_Invoked_Then_Validate_Result()
        {
            string patientId = patientDBQueryController.RegisterPatient(9663625749, "ashi", "Female", 23);
            patientDBQueryController.AllocateResourceToPatient(9663625749, int.Parse(patientId), "ashi", "general", "synus", 3, 101, "2019-02-02", 9535619688, "2019-02-02", true);
            bool actualValue = patientDBQueryController.IsPatientAllocated(int.Parse(patientId));
            bool expectedValue = true;
            Assert.AreEqual(expectedValue, actualValue);
        }
        [TestMethod]
        public void Given_Valid_Allocation_Details_When_AllocateResourceToPatient_Is_Invoked_Then_Validate_Result()
        {
            string patientId = patientDBQueryController.RegisterPatient(9663775749, "ashi", "Female", 23);
            patientDBQueryController.AllocateResourceToPatient(9663625749, int.Parse(patientId), "ashi", "general", "synus", 3, 301, "2019-02-02", 9535619688, "2019-02-02", true);
            bool actualValue = patientDBQueryController.IsPatientAllocated(int.Parse(patientId));
            bool expectedValue = true;
            Assert.AreEqual(expectedValue, actualValue);

        }
        [TestMethod]
        public void Given_Valid_BedAndWardNumber_When_InsertWardInformation_Is_Invoked_Then_Validate_Result()
        {
            patientDBQueryController.InsertWardInformation("special", 3);
            bool expectedValue = patientDBQueryController.IsWardExist(3);
            Assert.AreEqual(expectedValue, true);
        }
        [TestMethod]
        public void Given_Valid_BedAndWardNumber_When_InsertBedInformation_Is_Invoked_Then_Validate_Result()
        {
            patientDBQueryController.InsertWardInformation("special", 3);
            patientDBQueryController.InsertBedInformation(302, 3);
            bool expectedValue = patientDBQueryController.IsBedExist(302);
            Assert.AreEqual(expectedValue, true);
        }        
        [TestMethod]
        public void Given_Valid_BedNumber_When_IsBedExists_Is_Invoked_Then_Validate_Result()
        {
            patientDBQueryController.InsertWardInformation("special", 3);
            patientDBQueryController.InsertBedInformation(303, 3);
            bool expectedValue = patientDBQueryController.IsBedExist(303);
            Assert.AreEqual(expectedValue, true);
        }
        [TestMethod]
        public void Given_Valid_WardNumber_When_IsWardExists_Is_Invoked_Then_Validate_Result()
        {
            patientDBQueryController.InsertWardInformation("special", 4);
            bool expectedValue = patientDBQueryController.IsWardExist(4);
            Assert.AreEqual(expectedValue, true);
        }
        [TestMethod]
        public void Given_Valid_BedAndWardNumber_When_GetAvailableBed_Is_Invoked_Then_Validate_Result()
        {
            int bedNumber = -1;
            int wardNumber = -1;
            bool expectedValue = patientDBQueryController.GetAvailableBed("special", out bedNumber,out wardNumber);
            if(bedNumber != -1 && wardNumber != -1)
            {
                Assert.AreEqual(expectedValue, true);
            }
        }
        [TestMethod]
        public void Given_Valid_BedWardNumberAndavailability_When_UpdatebedInformation_Is_Invoked_Then_Validate_Result()
        {
            patientDBQueryController.UpdateBedInformation(303, 3, true);
            bool isAvailable = patientDBQueryController.IsBedAvailable(303);
            Assert.AreEqual(true, isAvailable);
        }
        [TestMethod]
        public void Given_Valid_PatientId_When_Discharge_Is_Invoked_Then_Validate_Result()
        {
            string patientId = patientDBQueryController.RegisterPatient(9663625749, "ashi", "Female", 23);
            patientDBQueryController.AllocateResourceToPatient(9663625749, int.Parse(patientId), "ashi", "general", "synus", 3, 101, "2019-02-02", 9535619688, "2019-02-02", false);
            patientDBQueryController.Discharge(int.Parse(patientId));
            bool isAvailable = patientDBQueryController.IsBedAvailable(101);
            Assert.AreEqual(true, isAvailable);
        }
    }
}
