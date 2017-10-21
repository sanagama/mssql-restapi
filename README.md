# What's here?

This is a *prototype* of a simple [ASP.NET Core 2.0](https://docs.microsoft.com/en-us/aspnet/core/getting-started) Web API app that uses [SQL Server Management Objects (SMO)](https://www.nuget.org/packages/Microsoft.SqlServer.SqlManagementObjects) under the covers to provide a RESTful interface for SQL Server running anywhere.

You can run this prototype on Linux, macOS, Windows or Docker and optionally use environment variables to connect to a local or remote SQL Server instance, Azure SQL Database and Azure SQL Data Warehouse. For fun, it also has a REST end-point to generate ```CREATE DATABASE``` and ```CREATE TABLE``` T-SQL scripts.

Currently, the prototype only supports the ```GET``` verb for Server, Database, Table and Column. I hope to support additional database objects and verbs (```PUT``` ```POST``` ```UPDATE``` and ```DELETE```) in the future.

## Context

The main motivation for this prototype was to try out the [SQL Server Management Objects (SMO)](https://www.nuget.org/packages/Microsoft.SqlServer.SqlManagementObjects) APIs on .NET Core 2.0.

As you've probably heard, the [SQL Server Management Objects (SMO)](https://www.nuget.org/packages/Microsoft.SqlServer.SqlManagementObjects) APIs are now available on .NET Core 2.0. Developers and system administrators can finally use the nifty SMO APIs in .NET Core 2.0 client apps (like this prototype) or PowerShell cmdlets on Linux, macOS and Windows to programmatically connect to and manage SQL Server running anywhere, Azure SQL Database and Azure SQL Data Warehouse.

Take a look at the [SQL Server Management Objects (SMO) Programming Guide](https://docs.microsoft.com/en-us/sql/relational-databases/server-management-objects-smo/sql-server-management-objects-smo-programming-guide) for samples and API reference documentation.

## Try it out!

The instructions below are for a MacBook with SQL Server 2017 running in Docker. Modify as necessary if you're using Linux or Windows.

### 1. Prerequisites

- Download and install Docker for your operating system: <https://www.docker.com>
- Increase Docker memory to 4 GB to run SQL Server 2017 as documented [here](https://docs.microsoft.com/en-us/sql/linux/quickstart-install-connect-docker#requirements)
- Download and install .NET Core for your operating system: <https://www.microsoft.com/net/core>

### 2. Get the source code

> *TIP:* If you have ```Git``` installed then you can do ```git clone https://github.com/sanagama/mssql-restapi.git``` instead of downloading and extracting the ZIP file as described below.

- Browse to <https://github.com/sanagama/mssql-restapi>
- Click ```Clone or Download``` then click ```Download ZIP```
- Save the ZIP file to your ```HOME``` directory as ```~/mssql-restapi.zip```
- Extract the zip file to your ```HOME``` directory ```~/mssql-restapi```

### 3. Run SQL Server 2017 in Docker

Launch a ```Terminal``` window and type the following commands to start SQL Server 2017 in Docker:

```
cd ~/mssql-restapi
cat ./scripts/1-docker-mssql.sh
./scripts/1-docker-mssql.sh
```

Type the following commands in the ```Terminal``` window to restore a couple of sample databases to SQL Servr 2017 running in Docker:

```
cd ~/mssql-restapi
cat ./scripts/2-docker-create-db.sh
./scripts/2-docker-create-db.sh
```

### 4. Launch the REST API middle-tier

Type the following commands in the ```Terminal``` window to launch the REST API:

```
cd ~/mssql-restapi
dotnet restore
dotnet build
dotnet run
```

### 5. Play with the REST API

> *TIP:* [Google Chrome](https://www.google.com/chrome/) with the [JSON Formatter](https://github.com/callumlocke/json-formatter) extension is a great way to play with REST APIs.

- Launch a browser and browse to <http://localhost:5000/api/mssql>
- Click on the links in the JSON response to navigate database objects.

## Environment variables

You can use environment variables to to make the prototype connect to a local or remote SQL Server instance, Azure SQL Database and Azure SQL Data Warehouse.

Environment variable | Default Value | Description
--------------- | ------ | ------------
**MSSQL_HOST** | *127.0.0.1* | The fully qualified server name
**MSSQL_PORT** | *1433* | SQL Server port
**MSSQL_USERNAME** | *sa* | Username for SQL Server authentication
**MSSQL_PASSWORD** | *Yukon900* | Password for SQL Server authentication

## Connect to Azure SQL Database or Azure SQL Data Warehouse

Use environment variables to use the prototype with an Azure SQL Database or Azure SQL Data Warehouse.

>*TIP:* Follow instructions at [Configure a server-level firewall rule](https://docs.microsoft.com/en-us/azure/sql-database/sql-database-get-started-portal#create-a-server-level-firewall-rule) to allow the computer running the Web API app to connect to your Azure SQL Database.

>*TIP:* Replace *server*, *username* and *password* in the example below as appropriate for your Azure SQL Database.

```
MSSQL_HOST="<server>.database.windows.net" \
MSSQL_PORT="1433" \
MSSQL_USERNAME="<username>" \
MSSQL_PASSWORD="<password>" \
dotnet run
```

## Running in Docker

An image with this prototype is available on Docker Hub: <https://hub.docker.com/r/sanagama/mssql-restapi/>

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
