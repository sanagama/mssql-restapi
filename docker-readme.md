### mssql-restapi

This is a *prototype* of a simple [ASP.NET Core 2.0](https://docs.microsoft.com/en-us/aspnet/core/getting-started) REST API Web app that uses [SQL Server Management Objects (SMO)](https://www.nuget.org/packages/Microsoft.SqlServer.SqlManagementObjects) under the covers to provide a RESTful interface for SQL Server running anywhere.

Demo walkthrough + source code is here: <https://github.com/sanagama/mssql-restapi>

### Run locally in Docker

1. Type the commands below in a ```Terminal``` window to run the REST API web app in Docker. You can use environment variables to connect to a local or remote SQL Server instance, Azure SQL Database and Azure SQL Data Warehouse

    >*TIP:* See the demo walkthrough at <https://github.com/sanagama/mssql-restapi> for how to use with SQL Server 2017 running in Docker.

    >*TIP:* Change **<server>**, **<username>** and **<password>** in the example below as appropriate to connect to your SQL Server instance, Azure SQL Database or Azure SQL Data Warehouse.
    
    ```
    docker pull sanagama/mssql-restapi

    docker run -it -p 5000:5000 \
            -e MSSQL_HOST="<server>" \
            -e MSSQL_PORT="1433" \
            -e MSSQL_USERNAME="<username>" \
            -e MSSQL_PASSWORD="<password>" \
            sanagama/mssql-restapi
    ```

1. Launch your browser and navigate to <http://localhost:5000/api/mssql>
1. Click on the various links in the JSON response to navigate databases, tables and column objects in the SQL instance.

### Run in Azure Web App on Linux

To use this Docker image in Azure Web App on Linux, see: <https://docs.microsoft.com/en-us/azure/app-service-web/app-service-linux-using-custom-docker-image>
