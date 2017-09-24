namespace MSSqlWebapi.Models
{
    public static class Constants
    {
        public const string LinkNameSelf = "self";
        public const string LinkNameParent = "parent";

        public const string ApiRoutePathRoot = "/api/mssql";
        public const string ApiRouteNameServer = "Server";

        public const string ApiRouteNameDatabases = "databases";
        public const string ApiRoutePathDatabases = Constants.ApiRoutePathRoot + "/" + Constants.ApiRouteNameDatabases;

        public const string ApiRouteNameDatabase = "database";
        public const string ApiRoutePathDatabase = Constants.ApiRoutePathDatabases + "/{dbName}";

        public const string ApiRouteNameScript = "script";
        public const string ApiRouteNameDatabaseScript = "db" + Constants.ApiRouteNameScript;
        public const string ApiRoutePathDatabaseScript = Constants.ApiRoutePathDatabase + "/" + Constants.ApiRouteNameScript;

        public const string ApiRouteNameTables = "tables";
        public const string ApiRoutePathTables = Constants.ApiRoutePathDatabase + "/" + Constants.ApiRouteNameTables;

        public const string ApiRouteNameTable = "table";
        public const string ApiRoutePathTable = Constants.ApiRoutePathTables + "/{tableName}";

        public const string ApiRouteNameTableScript = Constants.ApiRouteNameTable + Constants.ApiRouteNameScript;
        public const string ApiRoutePathTableScript = Constants.ApiRoutePathTable + "/" + Constants.ApiRouteNameScript;
        
        public const string ApiRouteNameTableColumns = "columns";
        public const string ApiRoutePathTableColumns = Constants.ApiRoutePathTable + "/" + Constants.ApiRouteNameTableColumns;

        public const string ApiRouteNameTableColumn = "column";
        public const string ApiRoutePathTableColumn = Constants.ApiRoutePathTableColumns + "/{columnName}";

        public const string ApiRouteNameTableRows = "rows";
        public const string ApiRoutePathTableRows = Constants.ApiRoutePathTable + "/" + Constants.ApiRouteNameTableRows;


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