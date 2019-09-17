using DBManagerLib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PatientDBQuery;
using System.ServiceModel;

namespace Host
{
    class Program
    {
        static void Main(string[] args)
        {
            //int bedNumber;
            //int wardNumber;
            //PatientQuery patientQuery = new PatientQuery();
            //patientQuery.RegisterPatient(9663625749, "Ashritha", "Male", 32);
            //patientQuery.AllocateResourceToPatient(9663625749, 50,"Cherry","emergency","Synus",5,6, "2019-02-13", 1234567892, "2019-02-31", true);
            //patientQuery.InsertWardInformation("special", 3);
            //patientQuery.InsertBedInformation(2, 3);
            //patientQuery.GetAvailableBed("special", out wardNumber, out bedNumber);
            //patientQuery.UpdateBedInformation(2, 3, false);
            //patientQuery.Discharge(21);
            using (ServiceHost host = new ServiceHost(typeof(PatientDBQueryControllerLib.PatientDBQueryController)))
            {
                host.Open();
                Console.WriteLine("Host started @ " + DateTime.Now.ToString());
                using (ServiceHost host1 = new ServiceHost(typeof(DbManagerControllerLib.DbManagerController)))
                {
                    host1.Open();
                    Console.ReadLine();
                }
            }

        }
    }
}
