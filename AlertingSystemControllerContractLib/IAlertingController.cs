//============================================================================
//
// COPYRIGHT KONINKLIJKE PHILIPS ELECTRONICS N.V. 2019
// All rights are reserved. Reproduction in whole or in part is
// prohibited without the written consent of the copyright owner.
//
//============================================================================ 
using System.ServiceModel;

namespace AlertingSystemControllerContractLib
{
    /// <summary>
    /// IAlertingController: It Reads Vital sign from global storage
    /// The it will parse the Json
    /// Then it will validate the parsed data
    /// and gives alert message.
    /// </summary>
    [ServiceContract]
    public interface IAlertingController
    {
        /// <summary>
        /// ReadPatientVitalSigns, It reads from global storage
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns>return vital sign as JSON format</returns>
        [OperationContract]
        string ReadPatientVitalSigns(string patientId);
        /// <summary>
        /// ValidatePatientVitalSigns, It calls parseJsonData[] that return array of vital values
        /// and then it validate parsed values
        /// and gives Alert
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="patientVitalSignAsJson"></param>
        /// <param name="alertMessage"></param>
        /// <returns>Alert message i.e Alert! spo2 out of range</returns>
        [OperationContract]
        bool ValidatePatientVitalSigns(string patientId, string patientVitalSignAsJson, out string alertMessage);
        /// <summary>
        /// ValidateSpo2
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="m_spo2"></param>
        /// <returns></returns>
        [OperationContract]
        string ValidateSpo2(string patientId, string m_spo2);
        /// <summary>
        /// ValidatePulseRate
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="m_pulseRate"></param>
        /// <returns></returns>
        [OperationContract]
        string ValidatePulseRate(string patientId, string m_pulseRate);
        /// <summary>
        /// ValidateTemperature
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="m_temperature"></param>
        /// <returns></returns>
        [OperationContract]
        string ValidateTemperature(string patientId, string m_temperature);
    }
}
