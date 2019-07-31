CREATE TABLE [tables].[Outputs] (
    [Id]            UNIQUEIDENTIFIER CONSTRAINT [DF_Outputs_Id] DEFAULT (newsequentialid()) NOT NULL,
    [Value]         DECIMAL (28, 8)  NOT NULL,
    [Index]         BIGINT           NOT NULL,
    [TransactionId] CHAR (64)        NOT NULL,
    CONSTRAINT [PK_Outputs] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Outputs_Transactions] FOREIGN KEY ([TransactionId]) REFERENCES [tables].[Transactions] ([Id]) ON DELETE CASCADE
);

