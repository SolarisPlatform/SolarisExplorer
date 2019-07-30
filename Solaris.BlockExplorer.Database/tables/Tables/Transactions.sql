CREATE TABLE [tables].[Transactions] (
    [Id]         CHAR (64) NOT NULL,
    [Hash]       CHAR (64) NOT NULL,
    [Version]    BIGINT    NOT NULL,
    [Size]       BIGINT    NOT NULL,
    [VSize]      BIGINT    NOT NULL,
    [LockTime]   BIGINT    NOT NULL,
    [BlockId]    CHAR (64) NOT NULL,
    [Time]       BIGINT    NOT NULL,
    [BlockTime]  BIGINT    NOT NULL,
    [BlockOrder] BIGINT    NOT NULL,
    CONSTRAINT [PK_Transactions] PRIMARY KEY ([Id] ASC),
    CONSTRAINT [FK_Transactions_Blocks] FOREIGN KEY ([BlockId]) REFERENCES [tables].[Blocks] ([Id])
);

