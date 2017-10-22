#!/bin/bash

#
# Run mssql-restapi locally with .NET Core 2.0 and connect to an Azure SQL Database.
#
# Replace the following before running this script:
#  <server>: Azure SQL DB logical server name
#  <username>: Azure SQL DB username
#  <password>: Azure SQL DB password
#
# Follow instructions at https://docs.microsoft.com/en-us/azure/sql-database/sql-database-get-started-portal#create-a-server-level-firewall-rule
# to allow the computer running the Web API app to connect to your Azure SQL Database.
#

set -x

MSSQL_HOST="<server>.database.windows.net" \
MSSQL_PORT="1433" \
MSSQL_USERNAME="<username>" \
MSSQL_PASSWORD="<password>" \
dotnet run
