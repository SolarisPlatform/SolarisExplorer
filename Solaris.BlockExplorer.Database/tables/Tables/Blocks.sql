CREATE TABLE [tables].[Blocks] (
    [Id]            CHAR (64)       NOT NULL,
    [Size]          BIGINT          NOT NULL,
    [Height]        BIGINT          NOT NULL,
    [Version]       BIGINT          NOT NULL,
    [Merkleroot]    CHAR (64)       NOT NULL,
    [Time]          BIGINT          NOT NULL,
    [Nonce]         BIGINT          NOT NULL,
    [Weight]        BIGINT          NOT NULL,
    [MedianTime]    BIGINT          NOT NULL,
    [Bits]          VARCHAR (10)    NOT NULL,
    [Difficulty]    DECIMAL (28, 8) NOT NULL,
    [Chainwork]     CHAR (64)       NOT NULL,
    [PreviousBlock] CHAR (64)       NULL,
    CONSTRAINT [PK_Blocks] PRIMARY KEY ([Id] ASC),
    CONSTRAINT [FK_Blocks_Blocks] FOREIGN KEY ([PreviousBlock]) REFERENCES [tables].[Blocks] ([Id])
);

