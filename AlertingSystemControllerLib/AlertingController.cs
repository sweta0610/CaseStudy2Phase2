//============================================================================
//
// COPYRIGHT KONINKLIJKE PHILIPS ELECTRONICS N.V. 2019
// All rights are reserved. Reproduction in whole or in part is
// prohibited without the written consent of the copyright owner.
//
//============================================================================ 
using AlertingSystemControllerContractLib;
using DataAccessContractLib;
using FactoryLib;
using JsonPatientVitalSignParserContractLib;
using PatientVitalSignReaderContractLib;
using System.Collections.Generic;
using ValidatePatientVitalSignContractLib;
using VitalSignContractLib;
using VitalSignLib;

namespace AlertingSystemControllerLib
{
    /// <summary>
    ///  This is Alerting Service It exposes ReadVitalSigns: It will read data from Datastorage
    /// It exposes ValidatePulseRate,validateSpo2,ValidateTemperature
    // : It will validate Vital signs. is it in range, not ?
    /// </summary>
    public class AlertingController : IAlertingController
    {
        readonly List<IValidateVitalSign> m_lstValidateVitalSign = null;
        readonly IReader m_reader = null;
        readonly IParser m_parser = null;
        readonly IDataAccess m_dataAccess = null;

        public AlertingController()
        {
            m_lstValidateVitalSign = new List<IValidateVitalSign>()
            {
                Factory.GetVitalSignValidator( VitalSignType.SPO2),
                Factory.GetVitalSignValidator( VitalSignType.PulseRate),
                Factory.GetVitalSignValidator( VitalSignType.Temperature)
            };
            m_reader = Factory.GetVitalSignReader();
            m_parser = Factory.GetParser();
            m_dataAccess = Factory.GetDataAccess();
        }

        public string ReadPatientVitalSigns(string patientId)
        {
            return m_reader.ReadPatientVitalSigns(patientId);
        }

        public bool ValidatePatientVitalSigns(string patientId, string patientVitalSignAsJson, out string alertMessage)
        {
            bool bValidateResult = true;
            alertMessage = string.Empty;

            string [] parsedJsonData = m_parser.ParseJsonData(patientVitalSignAsJson);
            List<VitalSign> lstEnabledVitalSign = m_dataAccess.GetEnabledVitalSignForPatient(patientId);

            if (lstEnabledVitalSign != null && parsedJsonData.Length == (lstEnabledVitalSign.Count + 1))
            {
                bValidateResult = ValidateEnabledVitalSigns(lstEnabledVitalSign, parsedJsonData, patientId, out alertMessage);
            }

            return bValidateResult;
        }

        private bool ValidateEnabledVitalSigns(List<VitalSign> lstEnabledVitalSign, string[] parsedJsonData, string patientId, out string alertMessage)
        {
            bool bValidateResult = true;
            alertMessage = string.Empty;

            foreach (IValidateVitalSign vitalSignValidator in m_lstValidateVitalSign)
            {
                int index = GetIndexForVitalSign(lstEnabledVitalSign, vitalSignValidator.VitalSignType);
                if (!ValidateVitalSign(index, vitalSignValidator, parsedJsonData, patientId, out alertMessage))
                {
                    bValidateResult = false;
                    break;
                }
            }
            return bValidateResult;
        }

        private int GetIndexForVitalSign(List<VitalSign> lstEnabledVitalSign, VitalSignType vitalSignType)
        {
            return lstEnabledVitalSign.FindIndex(x => (x.VitalSignType == vitalSignType));
        }

        private bool ValidateVitalSign(int index, IValidateVitalSign vitalSignValidator, string[] parsedJsonData, string patientId, out string alertMessage)
        {
            bool bValidateResult = false;
            alertMessage = string.Empty;

            if (index >= 0)
            {
                alertMessage = vitalSignValidator.ValidateVitalSign(patientId, parsedJsonData[index]);
                if (string.IsNullOrEmpty(alertMessage))
                {
                    bValidateResult = true;
                }
            }

            return bValidateResult;
        }

        private IValidateVitalSign GetVitalSignValidator(VitalSignType vitalSignType)
        {
            IValidateVitalSign validateVitalSign = null;
            foreach (IValidateVitalSign tempVitalSignValidator in m_lstValidateVitalSign)
            {
                if (tempVitalSignValidator.VitalSignType == vitalSignType)
                {
                    validateVitalSign = tempVitalSignValidator;
                    break;
                }
            }

            return validateVitalSign;
        }

        public string ValidatePulseRate(string patientId, string m_pulseRate)
        {
            string strResult = string.Empty;
            IValidateVitalSign pulseRateValidator = GetVitalSignValidator(VitalSignType.PulseRate);

            if(pulseRateValidator != null)
            {
                strResult = pulseRateValidator.ValidateVitalSign(patientId, m_pulseRate);
            }
            return strResult;
        }

        public string ValidateSpo2(string patientId, string m_spo2)
        {
            string strResult = string.Empty;
            IValidateVitalSign pulseRateValidator = GetVitalSignValidator(VitalSignType.SPO2);

            if (pulseRateValidator != null)
            {
                strResult = pulseRateValidator.ValidateVitalSign(patientId, m_spo2);
            }
            return strResult;
        }

        public string ValidateTemperature(string patientId, string m_temperature)
        {
            string strResult = string.Empty;
            IValidateVitalSign pulseRateValidator = GetVitalSignValidator(VitalSignType.Temperature);

            if (pulseRateValidator != null)
            {
                strResult = pulseRateValidator.ValidateVitalSign(patientId, m_temperature);
            }
            return strResult;
        }
    }
}
