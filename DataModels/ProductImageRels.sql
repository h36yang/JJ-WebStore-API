CREATE TABLE [dbo].[ProductImageRels] (
	[ProductId] [int] NOT NULL,
	[ImageId] [int] NOT NULL,
	CONSTRAINT [PK_ProductImageRels] PRIMARY KEY NONCLUSTERED ( [ProductId], [ImageId] )
);
