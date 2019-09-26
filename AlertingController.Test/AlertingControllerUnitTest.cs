//============================================================================
//
// COPYRIGHT KONINKLIJKE PHILIPS ELECTRONICS N.V. 2019
// All rights are reserved. Reproduction in whole or in part is
// prohibited without the written consent of the copyright owner.
//
//============================================================================ 
using FactoryLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatientVitalSignWriterContractLib;
using PatientVitalSignWriterLib;


namespace AlertingController.Test
{
     /// <summary>
     /// Test class for AlertingController
    ///It checks readPatientVitalSign from storage.
    ///Itchecks for validation of vitals signs
    /// </summary>
    [TestClass]
    public class AlertingControllerUnitTest
    {
        [TestMethod]
        public void Given_PatientId_When_ReadPatientVitalSigns_Invoke_Then_Valid_Result_Asserted()
        {
            IPatientVitalSignWriter m_writer = Factory.GetVitalSignWriter();
            string m_expectedValue = "{patient id: Patient_123, SPO2: 99, Temp: 98, PulseRate: 94}";
            m_writer.StorePatientVitalSigns("Patient_123", m_expectedValue);

            AlertingSystemControllerLib.AlertingController m_alertControl = new AlertingSystemControllerLib.AlertingController();
            string m_actualValue = m_alertControl.ReadPatientVitalSigns("Patient_123");
            Assert.AreEqual(m_actualValue,m_expectedValue);
        }
        [TestMethod]
        public void Given_Valid_Argument_When_ValidatePulseRate_Invoke_Then_Valid_Result_Asserted()
        {
            AlertingSystemControllerLib.AlertingController m_validate = new AlertingSystemControllerLib.AlertingController();
            string m_actualValue = m_validate.ValidatePulseRate("Patient_123","10");
            string m_expectedValue = "Alert!!! Pulse Rate not in range Pulse Rate: 10 for patient: Patient_123";
            Assert.AreEqual(m_actualValue, m_expectedValue);
        }
        [TestMethod]
        public void Given_Valid_Argument_When_ValidateSpo2_Invoke_Then_Valid_Result_Asserted()
        {
            AlertingSystemControllerLib.AlertingController m_validate = new AlertingSystemControllerLib.AlertingController();
            string m_actualValue = m_validate.ValidateSpo2("Patient_123", "10");
            string m_expectedValue = "Alert!!! SPO2 not in range SPO2: 10 for patient: Patient_123";
            Assert.AreEqual(m_actualValue, m_expectedValue);
        }
        [TestMethod]
        public void Given_Valid_Argument_When_Temperature_Invoke_Then_Valid_Result_Asserted()
        {
            AlertingSystemControllerLib.AlertingController m_validate = new AlertingSystemControllerLib.AlertingController();
            string m_actualValue = m_validate.ValidateTemperature("Patient_123", "10");
            string m_expected = "Alert!!! Temperature not in range Temperature: 10 for patient Patient_123";
            Assert.AreEqual(m_actualValue, m_expected);
        }
        [TestMethod]
        public void Given_Valid_Json_string_ValidatePatientVitalSigns_Invoke_Then_Validate_Result_Asserted()
        {            
            AlertingSystemControllerLib.AlertingController m_validate = new AlertingSystemControllerLib.AlertingController();
            string alertMessage;
            bool m_actual = m_validate.ValidatePatientVitalSigns("587", "{ patientId: 1, SPO2: 98, Temperature: 100, PulseRate: 101}", out alertMessage);
            Assert.AreEqual(false, m_actual);
            //{ patientId: 1, SPO2: 98, Temperature: 100, PulseRate: 101}
        }


    }
}
