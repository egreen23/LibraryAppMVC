CREATE TABLE [dbo].[LoanBook] (
    [Id]     INT IDENTITY (1, 1) NOT NULL,
    [LoanId] INT NOT NULL,
    [BookId] INT NOT NULL,
    CONSTRAINT [PK_LoanBook] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_LoanBook_Book_BookId] FOREIGN KEY ([BookId]) REFERENCES [dbo].[Book] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_LoanBook_Loan_LoanId] FOREIGN KEY ([LoanId]) REFERENCES [dbo].[Loan] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_LoanBook_BookId]
    ON [dbo].[LoanBook]([BookId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_LoanBook_LoanId]
    ON [dbo].[LoanBook]([LoanId] ASC);

