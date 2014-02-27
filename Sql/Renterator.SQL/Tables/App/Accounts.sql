CREATE TABLE [dbo].[Accounts]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(0,1), 
    [UserId] INT NOT NULL, 
    [Name] VARCHAR(255) NOT NULL, 
    [Description] VARCHAR(500) NOT NULL, 
    CONSTRAINT [FK_Accounts_ToUsers] FOREIGN KEY ([UserId]) REFERENCES [Users]([Id])
)
