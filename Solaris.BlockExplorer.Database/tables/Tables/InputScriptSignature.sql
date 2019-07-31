CREATE TABLE [tables].[InputScriptSignature] (
    [InputId] UNIQUEIDENTIFIER NOT NULL,
    [Asm]     VARCHAR (1024)   NOT NULL,
    [Hex]     VARCHAR (1024)   NOT NULL,
    CONSTRAINT [PK_InputScriptSignature] PRIMARY KEY ([InputId]),
    CONSTRAINT [FK_InputScriptSignature_Inputs] FOREIGN KEY ([InputId]) REFERENCES [tables].[Inputs] ([Id]) ON DELETE CASCADE
);

