#
# Simple REST API for SQL Server with ASP.NET Core Web API + SMO on .NET Core
# Sept 2017
#

FROM microsoft/aspnetcore-build:latest
LABEL author="Sanjay Nagamangalam <sanagama2@gmail.com>"
LABEL version=1.0

# Environment variables
# Chaining the ENV allows for only one layer, instead of one per ENV statement
ENV HOMEDIR=/mssql-webapi \
    MSSQL_HOST=127.0.0.1 \
    MSSQL_PORT=1433 \
    MSSQL_USERNAME=sa \
    MSSQL_PASSWORD=Yukon900

# Set computed environment variables in a different ENV so earlier ENV gets picked up
# See: https://docs.docker.com/engine/reference/builder/#environment-replacement
ENV SCRIPTDIR=$HOMEDIR/scripts

# Web API listens at http://localhost:5000 in container
EXPOSE 5000

# Copy web app directory to image
RUN mkdir -pv $HOMEDIR
WORKDIR $HOMEDIR
COPY . $HOMEDIR

RUN ["dotnet", "restore"]
RUN ["dotnet", "build"]

RUN chmod +x ./entrypoint.sh
CMD /bin/bash ./entrypoint.sh
# done!