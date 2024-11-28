BEGIN TRANSACTION;
GO

CREATE TABLE [Book] (
    [Id] int NOT NULL IDENTITY,
    [Titolo] nvarchar(70) NOT NULL,
    [Quantita] int NOT NULL,
    [DataPubblicazione] date NOT NULL,
    [Genere] nvarchar(max) NOT NULL,
    [Prezzo] decimal(18,2) NOT NULL,
    [AuthorId] int NULL,
    CONSTRAINT [PK_Book] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Book_Author_AuthorId] FOREIGN KEY ([AuthorId]) REFERENCES [Author] ([Id])
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AuthorId', N'DataPubblicazione', N'Genere', N'Prezzo', N'Quantita', N'Titolo') AND [object_id] = OBJECT_ID(N'[Book]'))
    SET IDENTITY_INSERT [Book] ON;
INSERT INTO [Book] ([Id], [AuthorId], [DataPubblicazione], [Genere], [Prezzo], [Quantita], [Titolo])
VALUES (1, 1, '1999-04-02', N'Fantasia', 1.3, 3, N'Biancaneve'),
(2, 1, '1988-02-15', N'Fantasia', 1.8, 3, N'Cenerentola'),
(3, 2, '2004-04-22', N'Thriller', 3.0, 1, N'Oxford Murderers'),
(4, 3, '1921-06-13', N'Romanzo', 2.5, 3, N'Orgoglio e Pregiudizio');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AuthorId', N'DataPubblicazione', N'Genere', N'Prezzo', N'Quantita', N'Titolo') AND [object_id] = OBJECT_ID(N'[Book]'))
    SET IDENTITY_INSERT [Book] OFF;
GO

CREATE INDEX [IX_Book_AuthorId] ON [Book] ([AuthorId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241108150908_AddBookTable', N'8.0.10');
GO

COMMIT;
GO

