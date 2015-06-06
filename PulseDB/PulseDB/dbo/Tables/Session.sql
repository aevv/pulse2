CREATE TABLE [dbo].[Session]
(
	[SessionId] INT NOT NULL  IDENTITY, 
    [UserId] UNIQUEIDENTIFIER NOT NULL, 
    [Token] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [FK_Sessions_ToTable] FOREIGN KEY ([UserId]) REFERENCES [User]([UserId]), 
    PRIMARY KEY ([UserId], [SessionId])
)

GO


