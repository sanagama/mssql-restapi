#!/bin/bash

set -x

docker build . -t sanagama/mssql-webapi

docker images

docker push sanagama/mssql-webapi

