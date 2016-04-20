CREATE TABLE [dbo].[SystemComments]
(
	[Id] INT IDENTITY(1,1) NOT NULL,
	[Message] NVARCHAR(MAX) NOT NULL,
	[Author] NVARCHAR(MAX) NOT NULL,
	[CreatedAt] DateTime NOT NULL,
	[Email] NVARCHAR(255) NOT NULL, 

    CONSTRAINT [PkSystemComments_Id] PRIMARY KEY(Id),
)
