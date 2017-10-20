/*
 * Restore WideWorldImportersDW-Standard to SQL Server running in Docker container
 */

PRINT "Restoring WideWorldImportersDW-Standard..."

-- Restore WideWorldImportersDW-Standard.bak into Docker container
-- The .BAK file is in the '/backups' directory that is mounted as a volume in the Docker container
RESTORE FILELISTONLY FROM DISK = '/backups/WideWorldImportersDW-Standard.bak'
GO

RESTORE DATABASE WideWorldImportersDW
FROM DISK = '/backups/WideWorldImportersDW-Standard.bak'
 WITH
    MOVE 'WWI_Primary' TO '/var/opt/mssql/data/WideWorldImportersDW.mdf',
    MOVE 'WWI_Log' TO '/var/opt/mssql/data/WideWorldImportersDW_Log.ldf',
    MOVE 'WWI_UserData' TO '/var/opt/mssql/data/WideWorldImportersDW_UserData.ndf'
GO

-- Dump database names
SELECT NAME FROM SYS.DATABASES
GO
