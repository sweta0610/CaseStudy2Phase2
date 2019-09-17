//============================================================================
//
// COPYRIGHT KONINKLIJKE PHILIPS ELECTRONICS N.V. 2019
// All rights are reserved. Reproduction in whole or in part is
// prohibited without the written consent of the copyright owner.
//
//============================================================================ 
using FactoryLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatientVitalSignReaderContractLib;
using PatientVitalSignWriterContractLib;

namespace PatientVitalSignWriter.Test
{
    [TestClass]
    public class PatientVitalSignWriterUnitTest
    {
        [TestMethod]
        public void Given_PatientId_Along_With_Json_Data_When_Invoke_StorePatientVitalSignsInDB_Valid_Json_Stores_In_Queue()
        {
            IPatientVitalSignWriter m_writer = Factory.GetVitalSignWriter();
            string m_expected = "{patient id: Patient_123, SPO2: 99, Temp: 98, PulseRate: 94}";
            m_writer.StorePatientVitalSigns("Patient_123", m_expected);

            IReader reader = Factory.GetVitalSignReader();
            string ActualValue = reader.ReadPatientVitalSigns("Patient_123");
            Assert.AreEqual(ActualValue, m_expected);
        }
    }
}
