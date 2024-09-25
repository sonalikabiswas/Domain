CREATE TABLE [dbo].[SavedListings]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY (1,1), 
    [UserId] BIGINT NOT NULL, 
    [ListingId] BIGINT NOT NULL, 
    CONSTRAINT [FK_SavedListings_ToUsers] FOREIGN KEY ([UserId]) REFERENCES [Users]([Id]), 
    CONSTRAINT [FK_SavedListings_ToSavedListings] FOREIGN KEY ([ListingId]) REFERENCES [Listings](Id)

)
