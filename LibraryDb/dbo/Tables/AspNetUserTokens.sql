CREATE TABLE [dbo].[AspNetUserTokens] (
    [UserId]        NVARCHAR (450) NOT NULL,
    [LoginProvider] NVARCHAR (450) NOT NULL,
    [Name]          NVARCHAR (450) NOT NULL,
    [Value]         NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED ([UserId] ASC, [LoginProvider] ASC, [Name] ASC),
    CONSTRAINT [FK_AspNetUserTokens_ApplicationUser_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[ApplicationUser] ([Id]) ON DELETE CASCADE
);

