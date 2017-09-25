#!/bin/bash
set -x

sqlcmd -S 127.0.0.1 -U sa -P Yukon900 -Q "CREATE DATABASE demodb"

sqlcmd -S 127.0.0.1 -U sa -P Yukon900 -i ..\backups\create-BollywoodDB.sql
