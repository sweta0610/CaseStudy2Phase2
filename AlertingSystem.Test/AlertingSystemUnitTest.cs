//============================================================================
//
// COPYRIGHT KONINKLIJKE PHILIPS ELECTRONICS N.V. 2019
// All rights are reserved. Reproduction in whole or in part is
// prohibited without the written consent of the copyright owner.
//
//============================================================================ 
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ValidatePatientPulseRateLib;
using ValidatePatientSpo2Lib;
using ValidatePatientTemperatureLib;
using FactoryLib;
using PatientVitalSignWriterContractLib;
using PatientVitalSignReaderContractLib;
using JsonPatientVitalSignParserLib;
using DataAccessContractLib;
using PatientVitalSignWriterLib;
using PatientVitalSignReaderLib;

namespace AlertingSystem.Test
{/// <summary>
///Test class for Alerting Components
///It test ReadPatientVitalsign.
///Test validation of vital signs
/// </summary>
    [TestClass]
    public class AlertingSystemUnitTest
    {
        readonly IDataAccess m_dataAccess = Factory.GetDataAccess();

        [TestMethod]
        public void Given_PatientId_When_ReadPatientVitalSigns_Invoke_Then_Valid_Result_Asserted()
        {
            IPatientVitalSignWriter writer = new PatientVitalSignWriter(m_dataAccess);
            string Expected = "{patient id: Patient_123, SPO2: 99, Temp: 98, PulseRate: 94}";
            writer.StorePatientVitalSigns("Patient_123", Expected);

            IReader reader = new PatientVitalSignReader(m_dataAccess);
            string ActualValue = reader.ReadPatientVitalSigns("Patient_123");
            Assert.AreEqual(ActualValue, Expected); 
        }
        [TestMethod]
        //Checks if dataStorage is empty ReadPatientVitalsign should return null
        public void Given_PatientId_When_ReadPatientVitalSigns_Invoke_Then_Null_Asserted()
        {
            IReader reader = new PatientVitalSignReader(m_dataAccess);
            string ActualValue = reader.ReadPatientVitalSigns("Patient_124");
            string ExpectedValue = "";
            Assert.AreEqual(ActualValue, ExpectedValue);
        }
        [TestMethod]
        public void Given_Valid_Spo2Value_When_validateVitalSign_Invoke_Then_Valid_Result_Asserted()
        { 
            //when vital's are in range it will return null else will return alert message.
            ValidatePatientSpo2 spo2 = new ValidatePatientSpo2();
            string Actual = spo2.ValidateVitalSign("Patient_123","97");
            string Expected = null;
            Assert.AreEqual(Actual, Expected);
        }
        [TestMethod]
        public void Given_InValid_Spo2Value_When_validateVitalSign_Invoke_Then_Valid_Result_Asserted()
        {
            //when vital's are in range it will return null else will return alert message.
            ValidatePatientSpo2 spo2 = new ValidatePatientSpo2();
            string Actual = spo2.ValidateVitalSign("Patient_123", "104");
            string Expected = "Alert!!! SPO2 not in range SPO2: 104 for patient: Patient_123";
            Assert.AreEqual(Actual, Expected);
        }
        [TestMethod]
        public void Given_Valid_Spo2_Value_when_isVitalSignInRange_Invoke_Then_Valid_Result_Asserted()
        {
            ValidatePatientSpo2 inRange = new ValidatePatientSpo2();
            bool Actual = inRange.IsVitalSignInRange(97);
            bool Expected = true;
            Assert.AreEqual(Actual, Expected);
        }
        [TestMethod]
        public void Given_InValid_Spo2_Value_when_isVitalSignInRange_Invoke_Then_Valid_Result_Asserted()
        {
            ValidatePatientSpo2 inRange = new ValidatePatientSpo2();
            bool Actual = inRange.IsVitalSignInRange(104);
            bool Expected = false;
            Assert.AreEqual(Actual, Expected);
        }

        [TestMethod]
        public void Given_Valid_PulseRate_Value_when_isVitalSignInRange_Invoke_Then_Valid_Result_Asserted()
        {
            ValidatePatientPulseRate inRange = new ValidatePatientPulseRate();
            bool Actual = inRange.IsVitalSignInRange(97);
            bool Expected = true;
            Assert.AreEqual(Actual, Expected);
        }
        [TestMethod]
        public void Given_InValid_PulseRate_Value_when_isVitalSignInRange_Invoke_Then_Valid_Result_Asserted()
        {
            ValidatePatientPulseRate inRange = new ValidatePatientPulseRate();
            bool Actual = inRange.IsVitalSignInRange(104);
            bool Expected = false;
            Assert.AreEqual(Actual, Expected);
        }
        [TestMethod]
        public void Given_Valid_Temperature_Value_when_isVitalSignInRange_Invoke_Then_Valid_Result_Asserted()
        {
            ValidatePatientTemperature inRange = new ValidatePatientTemperature();
            bool Actual = inRange.IsVitalSignInRange(99);
            bool Expected = true;
            Assert.AreEqual(Actual, Expected);
        }
        [TestMethod]
        public void Given_InValid_Temperature_Value_when_isVitalSignInRange_Invoke_Then_Valid_Result_Asserted()
        {
            ValidatePatientTemperature inRange = new ValidatePatientTemperature();
            bool m_actual = inRange.IsVitalSignInRange(104);
            bool Expected = false;
            Assert.AreEqual(m_actual, Expected);
        }
        [TestMethod]
        public void Given_Valid_Json_String_When_ParseJsonData_Invoke_Then_Valid_Result_Asserted()
        {
            JsonPatientVitalSignParser parser = new JsonPatientVitalSignParser();
            string[] m_actual = parser.ParseJsonData("{patient id: Patient_123, SPO2: 99, Temp: 98, PulseRate: 94}");
            string[] m_expected = {"Patient_123", "99", "98", "94"};

            Assert.AreEqual(m_actual.Length, m_expected.Length);
        }
        [TestMethod]
        public void Given_Valid_Json_String_With_two_Vitals_Enabled_When_ParseJsonData_Invoke_Then_Valid_Result_Asserted()
        {
            JsonPatientVitalSignParser parser = new JsonPatientVitalSignParser();
            string[] m_actual = parser.ParseJsonData("{patient id: Patient_123,Temp: 98, PulseRate: 94}");
            string[] m_expected = { "Patient_123", "98", "94" };

            Assert.AreEqual(m_actual.Length, m_expected.Length);
        }

    }
}
