CREATE TABLE [tables].[BlockTransactions] (
    [BlockId]       CHAR (64)        NOT NULL,
    [TransactionId] CHAR (64)        NOT NULL,
    CONSTRAINT [PK_BlockTransactions] PRIMARY KEY CLUSTERED ([BlockId] ASC, [TransactionId] ASC),
    CONSTRAINT [FK_BlockTransactions_Blocks] FOREIGN KEY ([BlockId]) REFERENCES [tables].[Blocks] ([Id]),
    CONSTRAINT [FK_BlockTransactions_Transactions] FOREIGN KEY ([TransactionId]) REFERENCES [tables].[Transactions] ([Id])
);

