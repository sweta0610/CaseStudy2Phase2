//============================================================================
//
// COPYRIGHT KONINKLIJKE PHILIPS ELECTRONICS N.V. 2019
// All rights are reserved. Reproduction in whole or in part is
// prohibited without the written consent of the copyright owner.
//
//============================================================================ 
using GeneratePatientVitalSignContractLib;
using PatientSpo2GeneratorLib;
using PatientTemperatureGeneratorLib;
using PatientPulserateGeneratorLib;
using EnableVitalSignContractLib;
using EnableVitalSignLib;
using DataAccessContractLib;
using PatientVitalSignWriterContractLib;
using PatientVitalSignWriterLib;
using PatientVitalSignReaderContractLib;
using PatientVitalSignReaderLib;
using PatientMonitorContractLib;
using PatientMonitorLib;
using System.Collections.Generic;
using DataAccessLib;
using JsonPatientVitalSignParserContractLib;
using JsonPatientVitalSignParserLib;
using ValidatePatientVitalSignContractLib;
using VitalSignContractLib;
using ValidatePatientSpo2Lib;
using ValidatePatientPulseRateLib;
using ValidatePatientTemperatureLib;

namespace FactoryLib
{
    //Factory will create object and gives.
    public static class Factory
    {
        static IVitalSignGenerator spo2Generator = new Spo2Generator();
        static IVitalSignGenerator pulserateGenerator = new PulserateGenerator();
        static IVitalSignGenerator temperatureGenerator = new TemperatureGenerator();
        static IValidateVitalSign spo2Validator = new ValidatePatientSpo2();
        static IValidateVitalSign pulseRateValidator = new ValidatePatientPulseRate();
        static IValidateVitalSign temperatureValidator = new ValidatePatientTemperature();
        static IEnableVitalSign vitalSignEnabler = null;
        static IPatientVitalSignWriter vitalSignWriter = null;
        static IReader vitalSignReader = null;
        static IDataAccess dataAccess = null;
        static IPatientMonitor patientMonitor = null;
        static IParser parser = null;

        public static IDataAccess GetDataAccess()
        {
            if(dataAccess == null)
            {
                dataAccess = new DataAccess();
            }

            return dataAccess;
        }

        public static IReader GetVitalSignReader()
        {
            if (vitalSignReader == null)
            {
                vitalSignReader = new PatientVitalSignReader(GetDataAccess());
            }
            return vitalSignReader;
        }
        public static IParser GetParser()
        {
            if(parser == null)
            {
                parser = new JsonPatientVitalSignParser();
            }
            return parser;
        }
        public static IPatientMonitor GetPatientMonitor()
        {
            if(patientMonitor == null)
            {
                List<IVitalSignGenerator> lstVitalSignGenerator =
                    new List<IVitalSignGenerator>() { spo2Generator , pulserateGenerator , temperatureGenerator };
                patientMonitor = new PatientMonitor(lstVitalSignGenerator);
            }
            return patientMonitor;
        }
        public static IEnableVitalSign GetVitalSignEnabler()
        {
            if (vitalSignEnabler == null)
            {
                vitalSignEnabler = new EnableVitalSign(GetDataAccess());
            }
            return vitalSignEnabler;
        }
        public static IPatientVitalSignWriter GetVitalSignWriter()
        {
            if (vitalSignWriter == null)
            {
                vitalSignWriter = new PatientVitalSignWriter(GetDataAccess());
            }
            return vitalSignWriter;
        }

        public static IVitalSignGenerator GetDataGenerator(VitalSignType type)
        {
            IVitalSignGenerator vitalSignGenerator = null;
            switch (type)
            {
                case VitalSignType.SPO2:
                    {
                    vitalSignGenerator = spo2Generator;
                    }
                break;
                case VitalSignType.PulseRate:
                    {
                    vitalSignGenerator = pulserateGenerator;
                    }
                break;
                case VitalSignType.Temperature:
                    {
                    vitalSignGenerator = temperatureGenerator;
                    }
                break;
            }
            return vitalSignGenerator;
        }

        public static IValidateVitalSign GetVitalSignValidator(VitalSignType type)
        {
            IValidateVitalSign vitalSignValidator = null;
            switch (type)
            {
                case VitalSignContractLib.VitalSignType.SPO2:
                    {
                        vitalSignValidator = spo2Validator;
                    }
                    break;
                case VitalSignContractLib.VitalSignType.PulseRate:
                    {
                        vitalSignValidator = pulseRateValidator;
                    }
                    break;
                case VitalSignContractLib.VitalSignType.Temperature:
                    {
                        vitalSignValidator = temperatureValidator;
                    }
                    break;
            }
            return vitalSignValidator;
        }
    }
}
