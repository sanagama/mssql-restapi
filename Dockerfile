#
# A simple REST API for SQL Server using SMO on .NET Core
# Oct 2017
#

FROM microsoft/aspnetcore-build:latest
LABEL author="Sanjay Nagamangalam <sanagama2@gmail.com>"
LABEL version=1.0

# Environment variables
# Chaining the ENV allows for only one layer, instead of one per ENV statement
ENV HOMEDIR=/mssql-restapi \
    MSSQL_HOST=127.0.0.1 \
    MSSQL_PORT=1433 \
    MSSQL_DATABASE=master \
    MSSQL_USERNAME=sa \
    MSSQL_PASSWORD=Yukon900

# Web API listens at http://localhost:5000 in container
EXPOSE 5000

# Copy web app directory to image
RUN mkdir -pv $HOMEDIR
WORKDIR $HOMEDIR
COPY . $HOMEDIR

RUN ["dotnet", "restore"]
RUN ["dotnet", "build"]
CMD ["dotnet", "run"]
