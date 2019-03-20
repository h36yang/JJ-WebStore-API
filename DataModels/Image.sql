CREATE TABLE [dbo].[Image] (
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Url] [varchar](max) NOT NULL,
	CONSTRAINT [PK_Image] PRIMARY KEY CLUSTERED ( [Id] )
);
