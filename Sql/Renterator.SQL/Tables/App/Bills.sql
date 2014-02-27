CREATE TABLE [dbo].[Bills]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(0,1), 
    [Date] DATE NOT NULL, 
    [Description] VARCHAR(200) NOT NULL, 
    [Amount] MONEY NOT NULL, 
    [BillTypeId] INT NOT NULL DEFAULT 1, 
    CONSTRAINT [FK_Bills_BillTypes] FOREIGN KEY ([BillTypeId]) REFERENCES [BillTypes]([Id])
)
