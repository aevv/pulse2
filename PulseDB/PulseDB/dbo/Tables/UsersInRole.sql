CREATE TABLE [dbo].[UsersInRole]
(
	[UserId] UNIQUEIDENTIFIER NOT NULL , 
    [RoleId] UNIQUEIDENTIFIER NOT NULL, 
    PRIMARY KEY ([UserId],[RoleId]), 
    CONSTRAINT [FK_UsersInRole_User] FOREIGN KEY ([UserId]) REFERENCES [User]([UserId]),
    CONSTRAINT [FK_UsersInRole_Role] FOREIGN KEY ([RoleId]) REFERENCES [Role]([RoleId])
)
