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
	IF NOT EXISTS(SELECT * FROM tables.Blocks WHERE tables.Blocks.Id = @BlockId)
	BEGIN
		RETURN;
	END
	
	DECLARE @OutputSum DECIMAL(28, 8) =
	ISNULL(
	(
		SELECT 
			SUM(Outputs.[Value]) 
		FROM 
			@Outputs Outputs
	), 0.00000000)
	DECLARE @InputSum DECIMAL(28, 8) =
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
	), 0.00000000)


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
			@OutputSum,
			@InputSum,
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

	DECLARE @IsMined BIT = 
	CAST
	(
		CASE WHEN 
			@OutputSum > @InputSum THEN 
				1 
			ELSE 
				0 
			END 
		AS BIT
	)

	INSERT INTO
		tables.AddressTransactions
		(
			[Value],
			[IsMined],
			[Time],
			[Addresses],
			[TransactionId]
		)
		SELECT
			SUM([Value]),
			@IsMined,
			@Time,
			Addresses,
			@Id
		FROM
		(
			SELECT
				-SUM(tables.Outputs.Value) AS [Value],
				tables.Outputs.Addresses
			FROM
				@Inputs Inputs
			INNER JOIN
				tables.Outputs
			ON
				tables.Outputs.TransactionId = Inputs.OutputTransactionId 
			AND 
				tables.Outputs.[Index] = Inputs.OutputIndex		
			WHERE 
				NOT tables.Outputs.Addresses IS NULL
			GROUP BY
				tables.Outputs.Addresses
			UNION
			SELECT
				SUM(Outputs.[Value]) AS [Value],
				Outputs.Addresses
			FROM
				@Outputs Outputs
			GROUP BY
				Outputs.Addresses
		) [Values]
		WHERE NOT
			[Values].Addresses = ''
		GROUP BY
			[Values].Addresses

	