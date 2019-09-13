CREATE TABLE [tables].[Outputs] (
    [Id]            UNIQUEIDENTIFIER CONSTRAINT [DF_Outputs_Id] DEFAULT (newsequentialid()) NOT NULL,
    [Value]         DECIMAL (28, 8)  NOT NULL,
    [Index]         BIGINT           NOT NULL,
    [TransactionId] CHAR (64)        NOT NULL,
    [Asm]                 VARCHAR (2048)   NULL,
    [Hex]                 VARCHAR (2048)   NULL,
    [RequestedSignatures] BIGINT           NULL,
    [Type]                VARCHAR (255)    NULL,
    [Addresses] VARCHAR(MAX) NULL, 
    CONSTRAINT [PK_Outputs] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Outputs_Transactions] FOREIGN KEY ([TransactionId]) REFERENCES [tables].[Transactions] ([Id]) ON DELETE CASCADE
);
GO
CREATE NONCLUSTERED INDEX [IX_Outputs_TransactionId]
ON [tables].[Outputs] ([TransactionId])
INCLUDE ([Value])
GO
CREATE INDEX [IX_Outputs_Addresses] ON [tables].[Outputs] ([Id]) INCLUDE(Addresses)
GO
