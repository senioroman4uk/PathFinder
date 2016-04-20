CREATE TABLE [dbo].[Trips]
(
	[Id] INT IDENTITY(1,1) NOT NULL,
	[DriverId] INT NOT NULL,
	[DepartureDate] DateTime NOT NULL,
	[WithReturning] BIT NOT NULL,
	[ReturnDate] DateTime NULL,
	[IsRegular] BIT NOT NULL,
	[Price] DECIMAL NOT NULL,
	[Places] INT NOT NULL,
	[StartPoint] geography NOT NULL,
	[EndPoint] geography NOT NULL,

	CONSTRAINT [PkTrips_Id] PRIMARY KEY(Id),
)
