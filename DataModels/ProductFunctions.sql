CREATE TABLE [dbo].[ProductFunctions] (
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NOT NULL,
	[Summary] [varchar](500) NOT NULL,
	[Detail] [varchar](max) NULL,
	CONSTRAINT [PK_ProductFunctions] PRIMARY KEY NONCLUSTERED ( [Id] )
);
