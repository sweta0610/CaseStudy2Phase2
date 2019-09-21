//============================================================================
//
// COPYRIGHT KONINKLIJKE PHILIPS ELECTRONICS N.V. 2019
// All rights are reserved. Reproduction in whole or in part is
// prohibited without the written consent of the copyright owner.
//
//============================================================================ 
using System;
using System.Data.Common;

namespace DBManagerLib
{
    public class DBManager
    {
        const string startPoint = "System.Data.SqlClient";
        const string connectionString = @"Data Source=ingbtcpic5nbxrv\sqlexpress;Initial Catalog=HospitalDB;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";
        DbConnection connection = null;

        //opens DB connection
        public void OpenDBConnection()
        {
            DbProviderFactory factory = DbProviderFactories.GetFactory(startPoint);
            connection = factory.CreateConnection();
            if (connection == null)
            {
                Console.WriteLine("Conection error");
                Console.ReadLine();
                return;
            }
            connection.ConnectionString = connectionString;
            connection.Open();
        }

        //closes DB connection
        public void CloseDBConnection()
        {
            connection.Close();
            connection.Dispose();
            connection = null;
        }

        //creates the command
        public DbCommand CreateDBCommand(string commandText)
        {
            DbProviderFactory factory = DbProviderFactories.GetFactory(startPoint);
            DbCommand command = factory.CreateCommand();
            if (command == null)
            {
                Console.WriteLine("Command error");
                Console.ReadLine();
                return null;
            }
            command.Connection = connection;
            command.CommandText = commandText;

            return command;
        }

        //executes the command
        public DbDataReader ExecuteCommand(DbCommand command)
        {
            return command.ExecuteReader();
        }
    }
}

