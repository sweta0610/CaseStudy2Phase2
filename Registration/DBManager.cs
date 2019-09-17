using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace DBManagerLib
{
    public class DBManager
    {
        string startPoint = "System.Data.SqlClient";
        string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=RegistrationProcess;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";
        DbConnection connection = null;

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

        public void CloseDBConnection()
        {
            connection.Close();
            connection.Dispose();
            connection = null;
        }

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
            command.CommandText = commandText;// "select * from SearchOldPatient";

            return command;
        }

        public DbDataReader ExecuteCommand(DbCommand command)
        {
            return command.ExecuteReader();
        }
    }
}

