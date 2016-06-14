CREATE TABLE [dbo].[Roles] (
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,

	CONSTRAINT [PK_dbo.Roles] PRIMARY KEY(Id)  
 )
