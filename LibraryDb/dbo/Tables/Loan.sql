CREATE TABLE [dbo].[Loan] (
    [Id]         INT             IDENTITY (1, 1) NOT NULL,
    [Totale]     DECIMAL (18, 2) NOT NULL,
    [DataInizio] DATE            NOT NULL,
    [DataFine]   DATE            NOT NULL,
    [UserId]     NVARCHAR (450)  NULL,
    CONSTRAINT [PK_Loan] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Loan_ApplicationUser_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[ApplicationUser] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Loan_UserId]
    ON [dbo].[Loan]([UserId] ASC);

