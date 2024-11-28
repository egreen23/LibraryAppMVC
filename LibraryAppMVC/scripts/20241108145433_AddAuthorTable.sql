IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Author] (
    [Id] int NOT NULL IDENTITY,
    [Nome] nvarchar(30) NOT NULL,
    [Cognome] nvarchar(50) NOT NULL,
    [Nazionalita] nvarchar(max) NOT NULL,
    [DoB] date NOT NULL,
    [LuogoNascita] nvarchar(max) NOT NULL,
    [DoD] date NULL,
    CONSTRAINT [PK_Author] PRIMARY KEY ([Id])
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Cognome', N'DoB', N'DoD', N'LuogoNascita', N'Nazionalita', N'Nome') AND [object_id] = OBJECT_ID(N'[Author]'))
    SET IDENTITY_INSERT [Author] ON;
INSERT INTO [Author] ([Id], [Cognome], [DoB], [DoD], [LuogoNascita], [Nazionalita], [Nome])
VALUES (1, N'Maximo', '2006-10-11', NULL, N'Birmingham', N'inglese', N'Steve'),
(2, N'Terry', '1942-10-30', '1984-12-21', N'Londra', N'inglese', N'Harry'),
(3, N'Margiotta', '1954-05-07', '1994-12-31', N'Birmingham', N'italiana', N'Mario'),
(4, N'Henry', '1931-12-01', '1962-02-03', N'Birmingham', N'francese', N'Thierry');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Cognome', N'DoB', N'DoD', N'LuogoNascita', N'Nazionalita', N'Nome') AND [object_id] = OBJECT_ID(N'[Author]'))
    SET IDENTITY_INSERT [Author] OFF;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241108145433_AddAuthorTable', N'8.0.10');
GO

COMMIT;
GO

