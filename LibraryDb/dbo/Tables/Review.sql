CREATE TABLE [dbo].[Review] (
    [Id]     INT            IDENTITY (1, 1) NOT NULL,
    [Testo]  NVARCHAR (MAX) NOT NULL,
    [UserID] NVARCHAR (450) NULL,
    [BookId] INT            NULL,
    CONSTRAINT [PK_Review] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Review_ApplicationUser_UserID] FOREIGN KEY ([UserID]) REFERENCES [dbo].[ApplicationUser] ([Id]),
    CONSTRAINT [FK_Review_Book_BookId] FOREIGN KEY ([BookId]) REFERENCES [dbo].[Book] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Review_BookId]
    ON [dbo].[Review]([BookId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Review_UserID]
    ON [dbo].[Review]([UserID] ASC);

