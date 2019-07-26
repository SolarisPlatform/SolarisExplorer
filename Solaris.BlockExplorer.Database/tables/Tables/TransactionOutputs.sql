CREATE TABLE [tables].[TransactionOutputs] (
    [TransactionId] CHAR (64)        NOT NULL,
    [OutputId]      UNIQUEIDENTIFIER NOT NULL,
    [OutputIndex]   BIGINT           NOT NULL,
    CONSTRAINT [PK_TransactionOutputs] PRIMARY KEY CLUSTERED ([TransactionId] ASC, [OutputId] ASC),
    CONSTRAINT [FK_TransactionOutputs_Outputs] FOREIGN KEY ([OutputId]) REFERENCES [tables].[Outputs] ([Id]),
    CONSTRAINT [FK_TransactionOutputs_Transactions] FOREIGN KEY ([TransactionId]) REFERENCES [tables].[Transactions] ([Id]),
    UNIQUE NONCLUSTERED ([TransactionId] ASC, [OutputIndex] ASC)
);

