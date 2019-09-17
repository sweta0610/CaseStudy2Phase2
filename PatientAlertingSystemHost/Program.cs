//============================================================================
//
// COPYRIGHT KONINKLIJKE PHILIPS ELECTRONICS N.V. 2019
// All rights are reserved. Reproduction in whole or in part is
// prohibited without the written consent of the copyright owner.
//
//============================================================================ 
using System;
using System.ServiceModel;

namespace PatientAlertingSystemHost
{
    //This will host all the services. 
    static class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(ControllerLib.Controller));
            host.Open();
            Console.WriteLine("Host started @ " + DateTime.Now.ToString());
            ServiceHost host1 = new ServiceHost(typeof(AlertingSystemControllerLib.AlertingController));
            host1.Open();
            ServiceHost host2 = new ServiceHost(typeof(DataStoreControllerLib.DataStoreController));
            host2.Open();
            Console.ReadLine();


            host.Close();
            host1.Close();
            host2.Close();

        }
      }
    }

