CREATE TABLE [dbo].[Users]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Email] VARCHAR(255) NOT NULL, 
    [PasswordHash] CHAR(70) NOT NULL, 
    [FirstName] NVARCHAR(35) NOT NULL, 
    [LastName] NVARCHAR(35) NOT NULL, 
    [IsActive] BIT NOT NULL, 
    [IsAdmin] BIT NOT NULL DEFAULT 0,
    [LastLoginDate] DATETIME NOT NULL DEFAULT GETDATE()
)
