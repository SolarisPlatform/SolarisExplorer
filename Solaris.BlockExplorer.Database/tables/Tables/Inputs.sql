CREATE TABLE [tables].[Inputs] (
    [Id]            UNIQUEIDENTIFIER CONSTRAINT [DF_Inputs_Id] DEFAULT (newsequentialid()) NOT NULL,
    [Coinbase]      VARCHAR (1024)   NULL,
    [Sequence]      BIGINT           NULL,
    [TransactionId] CHAR (64)        NULL,
    [OutputIndex]   BIGINT           NULL,
    CONSTRAINT [PK_Inputs] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Inputs_TransactionOutputs] FOREIGN KEY ([TransactionId], [OutputIndex]) REFERENCES [tables].[TransactionOutputs] ([TransactionId], [OutputIndex]),
    CONSTRAINT [FK_Inputs_Transactions] FOREIGN KEY ([TransactionId]) REFERENCES [tables].[Transactions] ([Id]),
    UNIQUE NONCLUSTERED ([TransactionId] ASC, [OutputIndex] ASC)
);

