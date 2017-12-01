#!/bin/bash

#
# Pull and run sanagama/mssql-restapi and connect to SQL Server 2017 running locally in Docker
#
# Uses default values for MSSQL_PORT, MSSQL_DATABASE, MSSQL_USERNAME and MSSQL_PASSWORD
# as described here: https://github.com/sanagama/mssql-restapi#environment-variables
#
set -x

docker pull sanagama/mssql-restapi

docker run --name 'mssql-restapi' -it -p 5000:5000 -e MSSQL_HOST=`hostname -s` sanagama/mssql-restapi

