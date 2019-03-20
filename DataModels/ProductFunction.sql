CREATE TABLE [dbo].[ProductFunction] (
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NOT NULL,
	[Summary] [varchar](500) NOT NULL,
	[Detail] [varchar](max) NULL,
	CONSTRAINT [PK_ProductFunction] PRIMARY KEY NONCLUSTERED ( [Id] ),
	CONSTRAINT [FK_ProductFunction_Product] FOREIGN KEY ( [ProductId] )
		REFERENCES [dbo].[Product] ( [Id] )
		ON DELETE CASCADE
		ON UPDATE CASCADE
);
