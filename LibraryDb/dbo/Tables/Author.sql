CREATE TABLE [dbo].[Author] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [Nome]         NVARCHAR (30)  NOT NULL,
    [Cognome]      NVARCHAR (50)  NOT NULL,
    [Nazionalita]  NVARCHAR (MAX) NOT NULL,
    [DoB]          DATE           NOT NULL,
    [LuogoNascita] NVARCHAR (MAX) NOT NULL,
    [DoD]          DATE           NULL,
    CONSTRAINT [PK_Author] PRIMARY KEY CLUSTERED ([Id] ASC)
);

