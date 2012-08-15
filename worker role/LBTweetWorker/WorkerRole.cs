using System;
using System.Data;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Data.SqlClient;
using System.Threading;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.StorageClient;

namespace LBTweetWorker
{
    public class WorkerRole : RoleEntryPoint
    {
        public override void Run()
        {
            // This is a sample worker implementation. Replace with your logic.
            Trace.WriteLine("$projectname$ entry point called", "Information");

            while (true)
            {
                Trace.WriteLine("Working", "Information");
                ExecutetJob();
                Thread.Sleep(1*3600000);
            }
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections 
            ServicePointManager.DefaultConnectionLimit = 12;
            return base.OnStart();
        }

        private string db_string()
        {
            SqlConnectionStringBuilder connStringBuilder;
            connStringBuilder = new SqlConnectionStringBuilder();
            connStringBuilder.DataSource = "mzw1908bpk.database.windows.net";
            connStringBuilder.InitialCatalog = "LBTweets";
            connStringBuilder.Encrypt = true;
            connStringBuilder.TrustServerCertificate = false;
            connStringBuilder.UserID = "VikNeoAdmin";
            connStringBuilder.Password = "Epsxe@193";
            return connStringBuilder.ToString();
        }

        protected void ExecutetJob()
        {
            using (SqlConnection sqlConnection = new SqlConnection(db_string()))
            {
                try
                {
                    // Open the connection
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand("RemoveStaleTweets", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.ExecuteNonQuery();
                }
                catch (SqlException)
                {
                    Trace.WriteLine("SqlException", "Error");
                    throw;
                }
            }
        }
    } // WorkerRole : RoleEntryPoint
}
