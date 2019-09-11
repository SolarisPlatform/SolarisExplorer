CREATE TYPE [types].[Input]
AS TABLE(
    [Coinbase] VARCHAR(2048) NULL,
	[Sequence] BIGINT NULL,
	[OutputTransactionId] CHAR(64) NULL,
	[OutputIndex] BIGINT NULL,
	[Asm] VARCHAR(2048) NULL,
	[Hex] VARCHAR(2048) NULL
)