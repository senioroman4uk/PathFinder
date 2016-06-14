CREATE TABLE [dbo].[UserComments]
(
	[Id] INT IDENTITY(1,1) NOT NULL, 
    [AuthorId] INT NOT NULL, 
    [TargetId] INT NOT NULL,
	[TripId] INT NOT NULL,
    [Message] NVARCHAR(MAX) NOT NULL,
	[CreatedAt] DATETIME NOT NULL,

	CONSTRAINT [PkUserComments_Id] PRIMARY KEY(Id),
	CONSTRAINT [FkUserComments_AuthorId] FOREIGN KEY([AuthorId]) REFERENCES [dbo].[Users](Id),
	CONSTRAINT [FkUserComments_TargetId] FOREIGN KEY([TargetId]) REFERENCES [dbo].[Users](Id), 
	CONSTRAINT [FkUserComments_TripId] FOREIGN KEY(TripId) REFERENCES [dbo].[Trips](Id),
	CONSTRAINT [CkUserComments_AuthorIdTargetId] CHECK ([TargetId] <> [AuthorId])
)
