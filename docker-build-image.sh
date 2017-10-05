#!/bin/bash

set -x

docker build . -t sanagama/mssql-webapi

docker images

