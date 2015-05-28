CREATE TABLE [dbo].[User]
(
	[UserId] UNIQUEIDENTIFIER NOT NULL, 
    [Username] NVARCHAR(50) NOT NULL, 
    [LastActivity] DATETIME NOT NULL, 
    CONSTRAINT [PK_User] PRIMARY KEY ([UserId]) 
)
