CREATE TABLE [tables].[TransactionInputs] (
    [TransactionId] CHAR (64)        NOT NULL,
    [InputId]       UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_TransactionInputs] PRIMARY KEY CLUSTERED ([TransactionId] ASC, [InputId] ASC),
    CONSTRAINT [FK_TransactionInputs_Inputs] FOREIGN KEY ([InputId]) REFERENCES [tables].[Inputs] ([Id]),
    CONSTRAINT [FK_TransactionInputs_Transactions] FOREIGN KEY ([TransactionId]) REFERENCES [tables].[Transactions] ([Id])
);

