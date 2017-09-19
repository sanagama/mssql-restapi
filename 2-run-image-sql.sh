#!/bin/bash
set -x

docker run --name 'sqldocker' --cap-add SYS_PTRACE \
           -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=Yukon900' \
           -e 'MSSQL_PID=Developer' \
           -p 1433:1433 \
           -d microsoft/mssql-server-linux

docker ps

