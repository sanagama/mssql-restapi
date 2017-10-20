using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Serilog;
using Serilog.Events;
using SMO = Microsoft.SqlServer.Management.Smo;  
using SMOCommon = Microsoft.SqlServer.Management.Common;  

namespace MSSqlRestApi.Models
{
    public class ServerContext
    {
        private string _host;
        public string Host { get {return this._host;} }
        private string _port;
        public string Port { get {return this._port;} }
        private string _username;
        public string Username { get {return this._username;} }
        private string _password;
        public string Password { get {return this._password;} }
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
            this._host = Environment.GetEnvironmentVariable(Constants.MSSQLEnvVarHost) ?? Constants.MSSQLDefaultHost;
            this._port = Environment.GetEnvironmentVariable(Constants.MSSQLEnvVarPort) ?? Constants.MSSQLDefaultPort;
            this._username = Environment.GetEnvironmentVariable(Constants.MSSQLEnvVarUsername) ?? Constants.MSSQLDefaultUsername;
            this._password = Environment.GetEnvironmentVariable(Constants.MSSQLEnvVarPassword) ?? Constants.MSSQLDefaultPassword;

            Log.Information("{0}: {1}, {2}: {3}, {4}: {5}, {6}: {7}",
                Constants.MSSQLEnvVarHost, this._host,
                Constants.MSSQLEnvVarPort, this._port,
                Constants.MSSQLEnvVarUsername, this._username,
                Constants.MSSQLEnvVarPassword, this._password);

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