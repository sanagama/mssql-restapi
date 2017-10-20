#!/bin/bash

set -x

docker build . -t sanagama/mssql-restapi

docker images

docker push sanagama/mssql-restapi
