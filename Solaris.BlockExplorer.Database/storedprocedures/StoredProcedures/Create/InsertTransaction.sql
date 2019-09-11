CREATE PROCEDURE [storedprocedures].[InsertTransaction]
	@Id CHAR(64),
	@BlockTime BIGINT,
	@Hash CHAR(64),
	@Locktime BIGINT,
	@Size BIGINT,
	@Time BIGINT,
	@Version BIGINT,
	@Vsize BIGINT,
	@BlockId CHAR(64),
	@BlockOrder BIGINT,
	@Inputs types.Input READONLY,
	@Outputs types.[Output] READONLY,
	@Json VARCHAR(MAX)
AS
	INSERT INTO
		tables.Transactions
		(
			Id,
			BlockTime,
			Hash,
			LockTime,
			Size,
			Time,
			Version,
			VSize,
			BlockId,
			BlockOrder,
			OutputSum,
			InputSum,
			Json
		)
	VALUES
		(
			@Id,
			@BlockTime,
			@Hash,
			@Locktime,
			@Size,
			@Time,
			@Version,
			@Vsize,
			@BlockId,
			@BlockOrder,
			ISNULL(
			(
				SELECT 
					SUM(Outputs.[Value]) 
				FROM 
					@Outputs Outputs
			), 0.00000000),
			ISNULL(
			(
				SELECT 
					SUM(tables.Outputs.[Value]) 
				FROM 
					@Inputs Inputs
				INNER JOIN
					tables.Outputs
				ON
					tables.Outputs.TransactionId = Inputs.OutputTransactionId
				AND
					tables.Outputs.[Index] = Inputs.OutputIndex
			), 0.00000000),
			@Json
		)
	INSERT INTO 
		tables.Inputs
		(
			Coinbase,
			Sequence,
			OutputId,
			TransactionId,
			Asm,
			Hex
		)
	SELECT
		Inputs.Coinbase,
		Inputs.Sequence,
		(
			SELECT 
				tables.Outputs.Id 
			FROM 
				tables.Outputs 
			WHERE 
				tables.Outputs.TransactionId = Inputs.OutputTransactionId 
			AND 
				tables.Outputs.[Index] = Inputs.OutputIndex
		),
		@Id,
		Inputs.Asm,
		Inputs.Hex
	FROM
		@Inputs Inputs

	INSERT INTO 
		tables.Outputs
		(
			[Value],
			[Index],
			[TransactionId],
			[Asm],
			[Hex],
			[RequestedSignatures],
			[Type],
			[Addresses]
		)
	SELECT
		Outputs.[Value],
		Outputs.[Index],
		@Id,
		Outputs.Asm,
		Outputs.Hex,
		Outputs.RequestedSignatures,
		Outputs.[Type],
		Outputs.Addresses
	FROM
		@Outputs Outputs
