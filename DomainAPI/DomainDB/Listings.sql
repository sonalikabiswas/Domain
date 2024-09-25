CREATE TABLE [dbo].[Listings] (
    [Id]       BIGINT  NOT NULL PRIMARY KEY IDENTITY (1, 1),
    [Address]  VARCHAR (MAX) NOT NULL,
    [Suburb]   VARCHAR (100) NOT NULL,
    [State]    VARCHAR (100) NOT NULL,
    [Postcode] INT           NOT NULL
);
