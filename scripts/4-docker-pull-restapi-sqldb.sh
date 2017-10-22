#!/bin/bash

#
# Pull and run sanagama/mssql-restapi in Docker and connect to an Azure SQL Database.
#
# Replace the following before running this script:
#  <server>: Azure SQL DB logical server name
#  <username>: Azure SQL DB username
#  <password>: Azure SQL DB password
#
# Follow instructions at https://docs.microsoft.com/en-us/azure/sql-database/sql-database-get-started-portal#create-a-server-level-firewall-rule
# to allow the computer running Docker to connect to your Azure SQL Database.
#
set -x

docker pull sanagama/mssql-restapi

docker run -it -p 5000:5000 \
           -e MSSQL_HOST="<server>.database.windows.net" \
           -e MSSQL_PORT="1433" \
           -e MSSQL_USERNAME="<username>" \
           -e MSSQL_PASSWORD="<password>" \
           sanagama/mssql-restapi
