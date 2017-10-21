using System;
//using System.Collections.Generic;
using System.Data.SqlClient;
using Serilog;
using Serilog.Events;
using SMO = Microsoft.SqlServer.Management.Smo;  
using SMOCommon = Microsoft.SqlServer.Management.Common;  

namespace MSSqlRestApi.Models
{
    public class ServerContext
    {
        public string Host { get; }
        public string Port { get; }
        public string Database { get; }
        public string Username { get; }
        public string Password { get; }
        private SMO.Server _smoServer;

        public SMO.Server SmoServer
        {
            get 
            { 
                if (_smoServer == null)
                {
                    this.Initialize();
                }
                return _smoServer; 
            }
        }
        public ServerContext()
        {
            Log.Information("Initializing ServerContext with Environment variables:");
            this.Host = Environment.GetEnvironmentVariable(Constants.MSSQLEnvVarHost) ?? Constants.MSSQLDefaultHost;
            this.Port = Environment.GetEnvironmentVariable(Constants.MSSQLEnvVarPort) ?? Constants.MSSQLDefaultPort;
            this.Database = Environment.GetEnvironmentVariable(Constants.MSSQLEnvVarDatabase) ?? Constants.MSSQLDefaultDatabase;
            this.Username = Environment.GetEnvironmentVariable(Constants.MSSQLEnvVarUsername) ?? Constants.MSSQLDefaultUsername;
            this.Password = Environment.GetEnvironmentVariable(Constants.MSSQLEnvVarPassword) ?? Constants.MSSQLDefaultPassword;

            Log.Information("{0}: {1}, {2}: {3}, {4}: {5}, {6}: {7}, {8}: {9}",
                Constants.MSSQLEnvVarHost, this.Host,
                Constants.MSSQLEnvVarPort, this.Port,
                Constants.MSSQLEnvVarDatabase, this.Database,
                Constants.MSSQLEnvVarUsername, this.Username,
                Constants.MSSQLEnvVarPassword, this.Password);

            this.Initialize();
        }
        private void Initialize()
        {
            if(_smoServer == null)
            {
                try
                {
                    // Build connection string
                    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                    builder.ApplicationName = "mssql-restapi";
                    builder.DataSource = this.Host + "," + this.Port;
                    builder.InitialCatalog = this.Database;
                    builder.UserID = this.Username;
                    builder.Password = this.Password;
                    builder.MultipleActiveResultSets = true; // required for SQL Azure
                    builder.ConnectTimeout = 30;
                    builder.ConnectRetryCount = 3;
                    builder.ConnectRetryInterval = 15;
                    builder.IntegratedSecurity = false;
                    
                    // Create a SMO connection
                    SqlConnection sqlConnection = new SqlConnection(builder.ConnectionString);
                    SMOCommon.ServerConnection serverConnection = new SMOCommon.ServerConnection(sqlConnection);
                    _smoServer = new SMO.Server(serverConnection);
                }
                catch(SqlException e)
                {
                    throw e;
                }
            }
        }
    }
}