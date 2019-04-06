CREATE TABLE [dbo].[ProductCategory]
(
	[Id] INT IDENTITY(1,1) NOT NULL,
	[Name] VARCHAR(50) NOT NULL,
	[IsActive] [bit] NOT NULL DEFAULT 1,
	CONSTRAINT [PK_ProductCategory] PRIMARY KEY NONCLUSTERED ( [Id] ),
	CONSTRAINT [NK_ProductCategory] UNIQUE NONCLUSTERED ( [Name] )
);
