CREATE TABLE [dbo].[CarColors]
(
	[Id] INT IDENTITY(1,1) NOT NULL,
	[Color] NVARCHAR(50) NOT NULL,

	CONSTRAINT [PkCarColors_Id] PRIMARY KEY(Id),
	CONSTRAINT [UnCarColors_Color] UNIQUE([Color])
)
