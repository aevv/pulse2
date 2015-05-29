CREATE TABLE [dbo].[UserProfile]
(
	[UserProfileId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [UserId] UNIQUEIDENTIFIER NOT NULL, 
    [Firstname] NVARCHAR(50) NULL, 
    [Lastname] NVARCHAR(50) NULL, 
    CONSTRAINT [FK_UserProfile_UserId] FOREIGN KEY ([UserId]) REFERENCES [User]([UserId])
)
