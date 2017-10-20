# What's here?

This is a prototype of a simple REST API that uses SMO on .NET Core to connect to SQL running anywhere.

As you probably heard, [SQL Server Management Objects (SMO)](https://www.nuget.org/packages/Microsoft.SqlServer.SqlManagementObjects) is now available on .NET Core. This means developers can use the nifty SMO APIs in client apps on Linux, macOS and Windows to programmatically manage SQL Server running anywhere, Azure SQL Database and Azure SQL Data Warehouse.

Take a look at the [SQL Server Management Objects (SMO) Programming Guide](https://docs.microsoft.com/en-us/sql/relational-databases/server-management-objects-smo/sql-server-management-objects-smo-programming-guide) for samples and API reference documentation.

The REST API in this prototype is an [ASP.NET Core 2.0](https://docs.microsoft.com/en-us/aspnet/core/getting-started) Web API app that uses [SQL Management Objects (SMO)](https://www.nuget.org/packages/Microsoft.SqlServer.SqlManagementObjects) on .NET Core under the covers to connect to SQL Server running anywhere and browse Server, Database and Table data and metadata.

The prototyle only implements the ```GET``` verb at the moment. Other verbs (```PUT``` ```POST``` and ```DELETE```) are coming next.

## Try it out!

The instructions below are for a MacBook with SQL Server 2017 running in Docker. Modify as necessary if you're using Linux or Windows.

### 1. Pre-requisites

1. Download and install Docker for your operating system: <https://www.docker.com>
1. Increase Docker memory to 4 GB to run SQL Server 2017 as documented [here](https://docs.microsoft.com/en-us/sql/linux/quickstart-install-connect-docker#requirements)
1. Download and install .NET Core for your operating system: <https://www.microsoft.com/net/core>

### 2. Get the source code

1. Browse to <https://github.com/sanagama/mssql-restapi>
1. Click ```Clone or Download``` then click ```Download ZIP```
1. Save the ZIP file to your ```HOME``` directory as ```~/mssql-restapi.zip```
1. Extract the zip file to your ```HOME``` directory ```~/mssql-restapi```

### 3. Run SQL Server 2017 in Docker

Launch a ```Terminal``` window and type the following commands to start SQL Server 2017 in Docker:

```bash
cd ~/mssql-restapi
cat ./scripts/1-docker-mssql.sh
./scripts/1-docker-mssql.sh
```

Type the following commands in the ```Terminal``` window to restore a couple of sample databases to SQL Servr 2017 running in Docker:

```bash
cd ~/mssql-restapi
cat ./scripts/2-docker-create-db.sh
./scripts/2-docker-create-db.sh
```

### 4. Run the Web API app

Type the following commands in the ```Terminal``` window to start the ASP.NET Core REST API web app:

```bash
cd ~/mssql-restapi
dotnet restore
dotnet build
dotnet run
```

### 5. Play with the REST API

> *TIP:* I use [Google Chrome](https://www.google.com/chrome/) with the [JSON Formatter](https://github.com/callumlocke/json-formatter) Chrome extension to play with the REST API.

1. Launch a browser and browse to <http://localhost:5000/api/mssql>
1. Click on the links in the JSON response to navigate database objects.
