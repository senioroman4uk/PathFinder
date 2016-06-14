CREATE TABLE [dbo].[Cars]
(
	[Id] INT IDENTITY(1,1) NOT NULL,
	[OwnerId] INT NOT NULL,
	[ModelId] INT NOT NULL,
	[Comfort] INT NOT NULL,
	[ColorId] INT NOT NULL,
	[Capacity] INT NOT NULL,

	CONSTRAINT [PkCars_Id] PRIMARY KEY(Id),
	CONSTRAINT [FkCars_OwnerId] FOREIGN KEY([OwnerId]) REFERENCES [dbo].[Users](Id),
	CONSTRAINT [FkCars_ModelId] FOREIGN KEY([ModelId]) REFERENCES [dbo].[CarModels](Id),
	CONSTRAINT [FkCars_ColorId] FOREIGN KEY([ColorId]) REFERENCES [dbo].[CarColors](Id)
)
