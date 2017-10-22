#!/bin/bash

#
# Pull and run SQL Server 2017 in Docker
# The directory ~/mssql-restapi/backups is mounted as /backups in the Docker container
#
set -x

docker pull microsoft/mssql-server-linux:2017-GA

docker run  --name 'sqldocker' --cap-add SYS_PTRACE \
            -v  ~/mssql-restapi/backups:/backups \
            -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=Yukon900' \
            -e 'MSSQL_PID=Developer' \
            -p 1433:1433 \
            -d microsoft/mssql-server-linux:2017-GA

