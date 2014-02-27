CREATE TABLE [dbo].[UserRoles]
(
	[UserId] INT NOT NULL,
	[RoleId] INT NOT NULL, 
    CONSTRAINT [PK_UserRoles] PRIMARY KEY ([UserId], [RoleId]), 
    CONSTRAINT [FK_UserRoles_User] FOREIGN KEY ([UserId]) REFERENCES [Users]([Id]) ON DELETE CASCADE, 
    CONSTRAINT [FK_UserRoles_Roles] FOREIGN KEY ([RoleId]) REFERENCES [Roles]([Id]) ON DELETE CASCADE 
)
