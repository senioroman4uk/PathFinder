CREATE TABLE [dbo].[UserLogins]
(
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [int] NOT NULL,

	CONSTRAINT [PK_dbo.UserLogins] PRIMARY KEY 
	(
		[LoginProvider] ASC,
		[ProviderKey] ASC,
		[UserId] ASC
	),
	CONSTRAINT [FK_dbo.UserLogins_dbo.Users_UserId] FOREIGN KEY([UserId]) REFERENCES [dbo].[Users] ([Id]) ON DELETE CASCADE
)