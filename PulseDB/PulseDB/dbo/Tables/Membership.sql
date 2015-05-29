CREATE TABLE [dbo].[Membership]
(
	[UserId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Password] NVARCHAR(50) NOT NULL, 
    [PasswordSalt] NVARCHAR(50) NOT NULL, 
    [Email] NVARCHAR(250) NOT NULL, 
    [PasswordQuestion] NVARCHAR(250) NULL, 
    [PasswordAnswer] NVARCHAR(50) NULL, 
    [IsEmailVerified] BIT NOT NULL, 
    [IsLockedOut] BIT NOT NULL, 
    [CreateDate] DATETIME NOT NULL, 
    [LastLoginDate] DATETIME NOT NULL, 
    [LastPasswordChangeDate] DATETIME NOT NULL, 
    [LastLockOutDate] DATETIME NOT NULL, 
    [FailedPasswordAttemptCount] DATETIME NOT NULL, 
    [FailedPasswordAnswerAttemptCount] INT NOT NULL, 
    CONSTRAINT [FK_Membership_User] FOREIGN KEY ([UserId]) REFERENCES [User]([UserId])
)
