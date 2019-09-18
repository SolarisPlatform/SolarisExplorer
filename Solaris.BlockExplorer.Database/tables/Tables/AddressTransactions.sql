CREATE TABLE [tables].[AddressTransactions]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWSEQUENTIALID(), 
    [Value] DECIMAL(28, 8) NOT NULL,
    [IsMined] BIT NOT NULL, 
    [Time] BIGINT NOT NULL, 
    [Addresses] VARCHAR(MAX) NOT NULL, 
    [TransactionId] CHAR(64) NOT NULL, 
    CONSTRAINT [FK_AddressTransactions_Transactions] FOREIGN KEY ([TransactionId]) REFERENCES [tables].[Transactions]([Id])
)

GO

CREATE INDEX [IX_AddressTransactions_Time] ON [tables].[AddressTransactions] ([Time] DESC) INCLUDE ([Id],[Addresses])
GO
CREATE INDEX [IX_AddressTransactions_Value] ON [tables].[AddressTransactions] ([Value]) INCLUDE ([Id],[Addresses])
GO
