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
    [InputSum] DECIMAL(28, 8) NOT NULL, 
    [OutputSum] DECIMAL(28, 8) NOT NULL, 
    [Json] VARCHAR(MAX) NOT NULL, 
    CONSTRAINT [PK_Transactions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Transactions_Blocks] FOREIGN KEY ([BlockId]) REFERENCES [tables].[Blocks] ([Id]) ON DELETE CASCADE
);
GO
CREATE NONCLUSTERED INDEX [IX_Transactions_BlockId]
ON [tables].[Transactions] ([BlockId])
GO

CREATE INDEX [IX_Transactions_InputSum_OutputSum] ON [tables].[Transactions] ([InputSum], [OutputSum])
GO