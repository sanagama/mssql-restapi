#!/bin/bash
set -x -e

SMO_DOTNET_CORE_BASEDIR=../smo-dotnet-core
SMO_DOTNET_CORE_OSX=$SMO_DOTNET_CORE_BASEDIR/osx
SMO_DOTNET_CORE_DEBIAN=$SMO_DOTNET_CORE_BASEDIR/debian
SQLTOOLS_SERVICE_GITHUB=https://github.com/Microsoft/sqltoolsservice/releases/download/v1.2.0-alpha.4
SQLTOOLS_SERVICE_TAR_OSX=$SQLTOOLS_SERVICE_GITHUB/Microsoft.SqlTools.ServiceLayer-osx-x64-netcoreapp2.0.tar.gz
SQLTOOLS_SERVICE_TAR_DEBIAN=$SQLTOOLS_SERVICE_GITHUB/Microsoft.SqlTools.ServiceLayer-debian-x64-netcoreapp2.0.tar.gz
FILES_TO_EXTRACT="NetCoreGlobalization.* Microsoft.Data.Tools.Sql.* Microsoft.SqlServer.*"

TMPFILE=`mktemp`
rm -fr $SMO_DOTNET_CORE_BASEDIR

# get the sqltoolsservice for OSX and extract SMO .NET Core assemblies
mkdir -p $SMO_DOTNET_CORE_OSX
wget "$SQLTOOLS_SERVICE_TAR_OSX" -O $TMPFILE
tar xvf $TMPFILE -C $SMO_DOTNET_CORE_OSX $FILES_TO_EXTRACT
rm $TMPFILE

# get the sqltoolsservice for Debian and extract SMO .NET Core assemblies
mkdir -p $SMO_DOTNET_CORE_DEBIAN
wget "$SQLTOOLS_SERVICE_TAR_DEBIAN" -O $TMPFILE
tar xvf $TMPFILE -C $SMO_DOTNET_CORE_DEBIAN $FILES_TO_EXTRACT
rm $TMPFILE
