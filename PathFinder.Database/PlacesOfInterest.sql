CREATE TABLE [dbo].[PlacesOfInterest]
(
	[Id] INT IDENTITY(1,1) NOT NULL, 
    [AuthorId] INT NOT NULL, 
    [Coordinates] [sys].[geography] NOT NULL, 
    [Description] NVARCHAR(MAX) NOT NULL, 
    [CreatedAt] DATETIME NOT NULL,

	CONSTRAINT [PkPlacesOfInterest_Id] PRIMARY KEY(Id),
	CONSTRAINT [FkPlacesOfInterest_AuthorId] FOREIGN KEY([AuthorId]) REFERENCES [dbo].[Users](Id),
)
