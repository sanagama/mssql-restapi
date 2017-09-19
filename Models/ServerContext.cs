using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Serilog;
using Serilog.Events;
using SMO = Microsoft.SqlServer.Management.Smo;  
using SMOCommon = Microsoft.SqlServer.Management.Common;  

namespace MSSqlWebapi.Models
{
    public class ServerContext
    {
        private string _host;
        private string _port;
        private string _username;
        private string _password;
        private SMO.Server _smoServer;
        private SMOCommon.ServerConnection _serverConnection;

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
            this._host = Environment.GetEnvironmentVariable(Constants.SqlServerEnvVarHost) ?? Constants.DefaultHost;
            this._port = Environment.GetEnvironmentVariable(Constants.SqlServerEnvVarPort) ?? Constants.DefaultPort;
            this._username = Environment.GetEnvironmentVariable(Constants.SqlServerEnvVarUsername) ?? Constants.DefaultUsername;
            this._password = Environment.GetEnvironmentVariable(Constants.SqlServerEnvVarPassword) ?? Constants.DefaultPassword;

            Log.Information("{0}: {1}, {2}: {3}, {4}: {5}, {6}: {7}",
                Constants.SqlServerEnvVarHost, this._host,
                Constants.SqlServerEnvVarPort, this._port,
                Constants.SqlServerEnvVarUsername, this._username,
                Constants.SqlServerEnvVarPassword, this._password);

            this.Initialize();
        }
        public ServerContext(string host, string port, string username, string password)
        {
            Log.Information("Initializing ServerContext");         
            this._host = host;
            this._port = port;
            this._username = username;
            this._password = password;

            Log.Information("Host: {0}, Port: {1}, Username: {2}, Password: {3}",
                this._host, this._port, this._username, this._password);

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
                    builder.ApplicationName = "mssql-webapi";
                    builder.DataSource = this._host + "," + this._port;
                    builder.UserID = this._username;
                    builder.Password = this._password;
                    builder.ConnectTimeout = 30;
                    builder.IntegratedSecurity = false;
                    builder.InitialCatalog = "master";
                    
                    SqlConnection sqlConnection = new SqlConnection(builder.ConnectionString);
                    _serverConnection = new SMOCommon.ServerConnection(sqlConnection);
                    _smoServer = new SMO.Server(_serverConnection);
                }
                catch(SqlException e)
                {
                    throw e;
                }
            }
        }
    }
}