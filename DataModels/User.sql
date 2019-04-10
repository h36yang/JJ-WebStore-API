﻿CREATE TABLE [dbo].[User]
(
	[Id] INT IDENTITY(1,1) NOT NULL,
	[Username] VARCHAR(50) NOT NULL,
	[Password] VARCHAR(50) NOT NULL,
	[IsAdmin] BIT NOT NULL DEFAULT 0,
	[IsActive] BIT NOT NULL DEFAULT 1,
	CONSTRAINT [PK_User] PRIMARY KEY NONCLUSTERED ( [Id] ),
	CONSTRAINT [NK_User] UNIQUE NONCLUSTERED ( [Username] )
);