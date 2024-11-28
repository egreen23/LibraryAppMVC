CREATE TABLE [dbo].[Book] (
    [Id]                INT             IDENTITY (1, 1) NOT NULL,
    [Titolo]            NVARCHAR (70)   NOT NULL,
    [Quantita]          INT             NOT NULL,
    [DataPubblicazione] DATE            NOT NULL,
    [Genere]            NVARCHAR (MAX)  NOT NULL,
    [Prezzo]            DECIMAL (18, 2) NOT NULL,
    [AuthorId]          INT             NULL,
    CONSTRAINT [PK_Book] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Book_Author_AuthorId] FOREIGN KEY ([AuthorId]) REFERENCES [dbo].[Author] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Book_AuthorId]
    ON [dbo].[Book]([AuthorId] ASC);

