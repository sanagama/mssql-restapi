#!/bin/bash

set -x

docker run  -it --name 'mssql-restapi' \
            -e 'MSSQL_HOST=127.0.0.1' \
            -e 'MSSQL_PORT=1433' \
            -e 'MSSQL_USERNAME=sa' \
            -e 'MSSQL_PASSWORD=Yukon900' \
            -p 5000:5000 \
            sanagama/mssql-restapi
