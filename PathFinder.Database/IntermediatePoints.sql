CREATE TABLE [dbo].[IntermediatePoints]
(
	[Id] INT IDENTITY(1,1) NOT NULL,
	[TripId] INT NOT NULL,
	[PassangerId] INT NOT NULL, 
	[Coordinates] [sys].[geography] NOT NULL, 

    CONSTRAINT [PkIntermediatePoints_Id] PRIMARY KEY(Id),
	CONSTRAINT [FkTrips_TripId] FOREIGN KEY(TripId) REFERENCES [dbo].[Trips](Id),
	CONSTRAINT [FkIntermediatePoints_PassangerId] FOREIGN KEY([PassangerId]) REFERENCES [dbo].[AspNetUsers](Id),
)
