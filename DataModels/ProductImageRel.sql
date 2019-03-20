CREATE TABLE [dbo].[ProductImageRel] (
	[ProductId] [int] NOT NULL,
	[ImageId] [int] NOT NULL,
	CONSTRAINT [PK_ProductImageRel] PRIMARY KEY NONCLUSTERED ( [ProductId], [ImageId] )
);
