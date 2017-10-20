/*
 * Restore AdventureworksLT to SQL Server running in Docker container
 */

PRINT "Restoring Adventureworks..."

-- Restore database into Docker container
-- The .BAK file is in the '/backups' directory that is mounted as a volume in the Docker container
RESTORE FILELISTONLY FROM DISK = '/backups/AdventureworksLT.bak'
GO

RESTORE DATABASE AdventureworksLT
FROM DISK = '/backups/AdventureworksLT.bak'
 WITH
    MOVE 'AdventureworksLT_Data' TO '/var/opt/mssql/data/AdventureworksLT_Data.mdf',
    MOVE 'AdventureworksLT_Log' TO '/var/opt/mssql/data/AdventureworksLT_Log.ldf'
GO

-- Dump database names
SELECT NAME FROM SYS.DATABASES
GO
