//============================================================================
//
// COPYRIGHT KONINKLIJKE PHILIPS ELECTRONICS N.V. 2019
// All rights are reserved. Reproduction in whole or in part is
// prohibited without the written consent of the copyright owner.
//
//============================================================================ 
using DataAccessContractLib;
using DataAccessLib;
using FactoryLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatientMonitorContractLib;
using PatientVitalSignReaderContractLib;
using PatientVitalSignWriterContractLib;
using PatientVitalSignWriterLib;
using System.Collections.Generic;
using VitalSignLib;

namespace PatientMonitoring.Test
{
    [TestClass]
    //Test class for patientMonitoring
    public class PatientMonitoringUnitTest
    {
        [TestMethod]
        //It checks if there is any null in generated json

        public void Given_Patient_Id_When_GenerateVitalSignAsJson_Invoke_Then_Valid_Result_Asserted()
        {
            IPatientMonitor m_patientMonitor = Factory.GetPatientMonitor();
            string m_actualValue = m_patientMonitor.GenerateVitalSignAsJson("PatientId_23");
            string[] m_values = m_actualValue.Split(',');
            //It checks there should be 8 entity in the json string
            Assert.AreEqual(4, m_values.Length);

            foreach (string value in m_values)
            {
                string[] args = value.Split(':');
                if (args.Length != 2)
                {
                    Assert.Fail("Failed");
                }
            }
        }
        [TestMethod]
        public void Given_Valid_PatientID_And_ListOfVitals_When_EnableVitalSignForPatient_Invoke_Then_Valid_Result_Asserted()
        {
            List<VitalSign> lstDefaultVitalSign = new List<VitalSign>() {
                    new VitalSign() { VitalSignType = VitalSignContractLib.VitalSignType.SPO2, IsPatientVitalSignEnabled = true},
                new VitalSign() { VitalSignType = VitalSignContractLib.VitalSignType.Temperature, IsPatientVitalSignEnabled = true},
                new VitalSign() { VitalSignType = VitalSignContractLib.VitalSignType.PulseRate, IsPatientVitalSignEnabled = true}};
            IDataAccess dataAcess = Factory.GetDataAccess();
            dataAcess.EnableVitalSignForPatient("Patient_1", lstDefaultVitalSign);
            int m_actual = lstDefaultVitalSign.Capacity;
            Assert.AreEqual(4, m_actual);
        }
        [TestMethod]
        public void Given_Valid_JsonString_When_StorePatientVitalSigns_Invoke_Then_Valid_Result_Asserted()
        {
            IDataAccess m_dataAccess = Factory.GetDataAccess();
            IPatientVitalSignWriter vitalSignWriter = new PatientVitalSignWriter(m_dataAccess);
            vitalSignWriter.StorePatientVitalSigns("Patient_123", "{ patientId: Patient_123, SPO2: 98, Temperature: 98, PulseRate: 84}");
           
            IReader vitalSignReader = Factory.GetVitalSignReader();
            string json = vitalSignReader.ReadPatientVitalSigns("Patient_123");
            Assert.AreEqual("{ patientId: Patient_123, SPO2: 98, Temperature: 98, PulseRate: 84}",json);
        }
    }
}
