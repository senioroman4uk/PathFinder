CREATE TABLE [dbo].[CarModels]
(
	[Id] INT IDENTITY(1,1) NOT NULL,
	[BrandId] INT NOT NULL,
	[Model] NVARCHAR(50) NOT NULL,

	CONSTRAINT [PkCarModels_Id] PRIMARY KEY(Id),
	CONSTRAINT [FkCarModels_BrandId] FOREIGN KEY([BrandId]) REFERENCES [dbo].[CarBrands](Id),
)
