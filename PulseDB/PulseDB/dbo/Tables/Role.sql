﻿CREATE TABLE [dbo].[Role]
(
	[RoleId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(250) NULL
)
