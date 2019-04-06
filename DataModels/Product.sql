CREATE TABLE [dbo].[Product] (
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CategoryId] [int] NOT NULL,
	[AvatarImageId] [int] NULL,
	[Name] [varchar](50) NULL,
	[LongName] [varchar](500) NULL,
	[Description] [varchar](max) NULL,
	[ProductNumber] [varchar](50) NULL,
	[Ingredient] [varchar](50) NULL,
	[Type] [varchar](50) NULL,
	[Price] [decimal](9,2) NOT NULL,
	[Volume] [varchar](50) NULL,
	[Origin] [varchar](50) NULL,
	[Producer] [varchar](50) NULL,
	[Highlight] [varchar](200) NULL,
	[IsActive] [bit] NOT NULL DEFAULT 1
	CONSTRAINT [PK_Product] PRIMARY KEY NONCLUSTERED ( [Id] )
);
