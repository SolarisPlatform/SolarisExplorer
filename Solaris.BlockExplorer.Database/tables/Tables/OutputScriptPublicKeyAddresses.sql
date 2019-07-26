CREATE TABLE [tables].[OutputScriptPublicKeyAddresses] (
    [Id]       UNIQUEIDENTIFIER CONSTRAINT [DF_OutputScriptPublicKeyAddresses_Id] DEFAULT (newsequentialid()) NOT NULL,
    [OutputId] UNIQUEIDENTIFIER NOT NULL,
    [Address]  VARCHAR (100)    NOT NULL,
    CONSTRAINT [PK_OutputScriptPublicKeyAddresses] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OutputScriptPublicKeyAddresses_OutputScriptPublicKey] FOREIGN KEY ([OutputId]) REFERENCES [tables].[OutputScriptPublicKey] ([OutputId])
);

