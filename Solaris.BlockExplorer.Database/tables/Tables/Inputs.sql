CREATE TABLE [tables].[Inputs] (
    [Id]                  UNIQUEIDENTIFIER CONSTRAINT [DF_Inputs_Id] DEFAULT (newsequentialid()) NOT NULL,
    [Coinbase]            VARCHAR (1024)   NULL,
    [Sequence]            BIGINT           NULL,
    [OutputTransactionId] CHAR (64)        NULL,
    [OutputIndex]         BIGINT           NULL,
    [TransactionId]       CHAR (64)        NOT NULL,
    CONSTRAINT [PK_Inputs] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Inputs_Transactions] FOREIGN KEY ([TransactionId]) REFERENCES [tables].[Transactions] ([Id]),
    CONSTRAINT [FK_Inputs_Transactions_Output] FOREIGN KEY ([OutputTransactionId]) REFERENCES [tables].[Transactions] ([Id])
);

