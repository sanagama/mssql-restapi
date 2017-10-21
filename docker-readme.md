# mssql-restapi

This is a *prototype* of a simple [ASP.NET Core 2.0](https://docs.microsoft.com/en-us/aspnet/core/getting-started) Web API app that uses [SQL Server Management Objects (SMO)](https://www.nuget.org/packages/Microsoft.SqlServer.SqlManagementObjects) under the covers to provide a RESTful interface for SQL Server running anywhere.

Demo walkthrough + source code at: <https://github.com/sanagama/mssql-restapi>

### Run locally

Type the commands below in a ```Terminal``` window to run this prototype in Docker. Use environment variables to specify the connection to your SQL Server instance.

>*TIP:* Replace *server*, *username* and *password* in the example below as appropriate for your SQL Server instance or Azure SQL Database.

```
docker pull sanagama/mssql-restapi

MSSQL_HOST="<server>" \
MSSQL_PORT="1433" \
MSSQL_USERNAME="<username>" \
MSSQL_PASSWORD="<password>" \
docker run -it --name 'mssql-restapi' -p 5000:5000 sanagama/mssql-restapi
```

### Run in Azure Web App on Linux

To use this Docker image in Azure Web App on Linux, see: <https://docs.microsoft.com/en-us/azure/app-service-web/app-service-linux-using-custom-docker-image>
