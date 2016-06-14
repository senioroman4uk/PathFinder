CREATE TABLE [dbo].[IntermediatePoints]
(
	[Id] INT IDENTITY(1,1) NOT NULL,
	[TripId] INT NOT NULL,
	[Coordinates] [sys].[geography] NOT NULL,
	[FormattedAddress] NVARCHAR(MAX) NOT NULL,
	[Name] NVARCHAR(MAX) NOT NULL,
	[PlaceId] NVARCHAR(MAX) NOT NULL,
	[IsStart] int NOT NULL,
	[IsEnd] int NOT NULL

    CONSTRAINT [PkIntermediatePoints_Id] PRIMARY KEY(Id),
	CONSTRAINT [FkTrips_TripId] FOREIGN KEY(TripId) REFERENCES [dbo].[Trips](Id),
)
