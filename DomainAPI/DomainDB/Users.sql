CREATE TABLE [dbo].[Users]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY (1,1), 
    [Name] VARCHAR(MAX) NOT NULL, 
    [Age] INT NOT NULL, 
    [Gender] VARCHAR(10) NULL, 
    [Occupation] VARCHAR(100) NULL
)
