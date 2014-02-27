CREATE USER [IIS APPPOOL\Renterator] FOR LOGIN [IIS APPPOOL\Renterator]
GO

GRANT CONNECT TO [IIS APPPOOL\Renterator]
GO

EXEC sp_addrolemember N'db_datawriter', N'IIS APPPOOL\Renterator'
GO

EXEC sp_addrolemember N'db_datareader', N'IIS APPPOOL\Renterator'
GO