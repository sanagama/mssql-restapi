namespace MSSqlWebapi.Models
{
    public static class Constants
    {
        public const string LinkNameSelf = "self";
        public const string LinkNameParent = "parent";

        public const string ApiRouteRoot = "/api/mssql";
        public const string ApiRouteNameServer = "Server";

        public const string ApiRouteNameDatabases = "databases";
        public const string ApiRouteDatabases = Constants.ApiRouteRoot + "/" + Constants.ApiRouteNameDatabases;

        public const string ApiRouteNameDatabase = "database";
        public const string ApiRouteDatabase = Constants.ApiRouteDatabases + "/{dbName}";

        public const string ApiRouteNameScript = "script";
        public const string ApiRouteNameDatabaseScript = "db" + Constants.ApiRouteNameScript;
        public const string ApiRouteDatabaseScript = Constants.ApiRouteDatabase + "/" + Constants.ApiRouteNameScript;

        public const string ApiRouteNameTables = "tables";
        public const string ApiRouteTables = Constants.ApiRouteDatabase + "/" + Constants.ApiRouteNameTables;

        public const string ApiRouteNameTable = "table";
        public const string ApiRouteTable = Constants.ApiRouteTables + "/{tableName}";

        public const string ApiRouteNameTableScript = "table" + Constants.ApiRouteNameScript;
        public const string ApiRouteTableScript = Constants.ApiRouteTable + "/" + Constants.ApiRouteNameScript;
        
        public const string SqlServerEnvVarHost = "SQLSERVER_HOST";
        public const string SqlServerEnvVarPort = "SQLSERVER_PORT";
        public const string SqlServerEnvVarUsername = "SQLSERVER_USERNAME";
        public const string SqlServerEnvVarPassword = "SQLSERVER_PASSWORD";
        public const string DefaultHost = "127.0.0.1";
        public const string DefaultPort = "1433";
        public const string DefaultUsername = "sa";
        public const string DefaultPassword = "Yukon900";
    }
}