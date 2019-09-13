CREATE TABLE [tables].[Inputs] (
    [Id]                  UNIQUEIDENTIFIER CONSTRAINT [DF_Inputs_Id] DEFAULT (newsequentialid()) NOT NULL,
    [Coinbase]            VARCHAR (2048)   NULL,
    [Sequence]            BIGINT           NULL,
    [OutputId]			  UNIQUEIDENTIFIER	       NULL,
    [TransactionId] CHAR(64) NOT NULL, 
	[Asm]     VARCHAR (2048)   NULL,
    [Hex]     VARCHAR (2048)   NULL,
    CONSTRAINT [PK_Inputs] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_Inputs_Outputs] FOREIGN KEY ([OutputId]) REFERENCES [tables].[Outputs]([Id]) ON DELETE CASCADE
);
GO
CREATE NONCLUSTERED INDEX [IX_Inputs_TransactionId]
ON [tables].[Inputs] ([TransactionId])
GO
CREATE NONCLUSTERED INDEX [IX_Inputs_OutputId]
ON [tables].[Inputs] ([OutputId])
GO