#!/bin/bash

#
# Create sample databases (Adventureworks, BollywoodDB & WideWorldImporters) in SQL Server 2017 in Docker
#
set -x

DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"

# run sqlcmd outside the Docker container to create a database
sqlcmd -S 127.0.0.1 -U sa -P Yukon900 -Q "CREATE DATABASE demodb"

# run sqlcmd outside the Docker container to create a database
sqlcmd -S 127.0.0.1 -U sa -P Yukon900 -i $DIR/../backups/create-BollywoodDB.sql

# run a script inside the Docker container to restore database backups in the mounted /backups directory
docker exec -it sqldocker /backups/restore-db.sh
