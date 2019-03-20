CREATE TABLE [dbo].[Images] (
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Url] [varchar](max) NOT NULL,
	CONSTRAINT [PK_Images] PRIMARY KEY CLUSTERED ( [Id] )
);
