CREATE TABLE [dbo].[AccountEntries]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(0,1), 
    [AccountId] INT NOT NULL,
    [Date] DATE NOT NULL DEFAULT GETDATE(), 
    [Description] VARCHAR(500) NOT NULL, 
    [Amount] MONEY NOT NULL, 
    CONSTRAINT [FK_AccountEntries_ToAccounts] FOREIGN KEY ([AccountId]) REFERENCES [Accounts]([Id])
)
