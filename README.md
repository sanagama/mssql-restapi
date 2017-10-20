# What's here?

This is a prototype of a simple REST API middle-tier that uses the [SQL Server Management Objects (SMO)](https://www.nuget.org/packages/Microsoft.SqlServer.SqlManagementObjects) APIs on .NET Core 2.0 to connect to SQL running anywhere and dynamically browse Server, Database and Table data and metadata.

As you probably heard, the [SQL Server Management Objects (SMO)](https://www.nuget.org/packages/Microsoft.SqlServer.SqlManagementObjects) APIs are now available on .NET Core 2.0. Developers and system administrators can finally use the nifty SMO APIs in .NET Core 2.0 client apps or PowerShell cmdlets on Linux, macOS and Windows to programmatically connect to and manage SQL Server running anywhere, Azure SQL Database and Azure SQL Data Warehouse.

Take a look at the [SQL Server Management Objects (SMO) Programming Guide](https://docs.microsoft.com/en-us/sql/relational-databases/server-management-objects-smo/sql-server-management-objects-smo-programming-guide) for samples and API reference documentation.

The REST API in this prototype is an [ASP.NET Core 2.0](https://docs.microsoft.com/en-us/aspnet/core/getting-started) Web API app that uses [SQL Server Management Objects (SMO)](https://www.nuget.org/packages/Microsoft.SqlServer.SqlManagementObjects) APIs under the covers to dynamically discover and browse data and metadata for SQL Server running anywhere. It also has a REST end-point that uses the SMO APIs to generate CREATE scripts for databases and tables.

Currently, the prototype only implements the ```GET``` verb. Other verbs (```PUT``` ```POST``` ```UPDATE``` and ```DELETE```) are coming soon.

## Try it out!

The instructions below are for a MacBook with SQL Server 2017 running in Docker. Modify as necessary if you're using Linux or Windows.

### 1. Prerequisites

1. Download and install Docker for your operating system: <https://www.docker.com>
1. Increase Docker memory to 4 GB to run SQL Server 2017 as documented [here](https://docs.microsoft.com/en-us/sql/linux/quickstart-install-connect-docker#requirements)
1. Download and install .NET Core for your operating system: <https://www.microsoft.com/net/core>

### 2. Get the source code

> *TIP:* If you have ```Git``` installed then you can do ```git clone https://github.com/sanagama/mssql-restapi.git``` instead of downloading and extracting the ZIP file as described below.

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

### 4. Launch the REST API middle-tier

Type the following commands in the ```Terminal``` window to launch the REST API:

```bash
cd ~/mssql-restapi
dotnet restore
dotnet build
dotnet run
```

### 5. Play with the REST API

> *TIP:* [Google Chrome](https://www.google.com/chrome/) with the [JSON Formatter](https://github.com/callumlocke/json-formatter) Chrome extension is a great way to play with REST APIs.

1. Launch a browser and browse to <http://localhost:5000/api/mssql>
1. Click on the links in the JSON response to navigate database objects.
