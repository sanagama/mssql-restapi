#!/bin/bash
set -x

DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"

# run sqlcmd inside the Docker container to restore Adventureworks
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Yukon900 -i /backups/restore-adventureworks.sql

# run sqlcmd inside the Docker container to restore WideWorldImporters
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Yukon900 -i /backups/restore-wwi.sql
