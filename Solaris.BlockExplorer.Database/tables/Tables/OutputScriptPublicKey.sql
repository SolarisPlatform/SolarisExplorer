CREATE TABLE [tables].[OutputScriptPublicKey] (
    [OutputId]            UNIQUEIDENTIFIER NOT NULL,
    [Asm]                 VARCHAR (1024)   NOT NULL,
    [Hex]                 VARCHAR (1024)   NOT NULL,
    [RequestedSignatures] BIGINT           NOT NULL,
    [Type]                VARCHAR (100)    NOT NULL,
    CONSTRAINT [PK_OutputScriptPublicKey] PRIMARY KEY CLUSTERED ([OutputId] ASC),
    CONSTRAINT [FK_OutputScriptPublicKey_Outputs] FOREIGN KEY ([OutputId]) REFERENCES [tables].[Outputs] ([Id])
);

