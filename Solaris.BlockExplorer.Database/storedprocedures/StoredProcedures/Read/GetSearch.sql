CREATE PROCEDURE [storedprocedures].[GetSearch]
	@Query VARCHAR(255)
AS
	SELECT
		Height,
		Id,
		[Type]
	FROM
	(
		SELECT 
			TOP 10
				tables.Blocks.Height AS Height,
				tables.Blocks.Id AS Id,
				'Block' AS [Type]
		FROM
			tables.Blocks
		WHERE
			tables.Blocks.Height = CASE WHEN ISNUMERIC(@Query) = 1 THEN @Query ELSE -1 END
		OR
			CAST(tables.Blocks.Height AS VARCHAR(1024)) LIKE @Query + '%'
		OR
			tables.Blocks.Id = @Query
		OR
			tables.Blocks.Id LIKE @Query + '%'
		ORDER BY
			tables.Blocks.Height
	) Blocks
	UNION
	SELECT 
		TOP 10
			0 AS Height,
			tables.Transactions.Id AS Id,
			'Transaction' AS [Type]
	FROM
		tables.Transactions
	WHERE
		tables.Transactions.Id = @Query
	OR
		tables.Transactions.Id LIKE @Query + '%'
	UNION ALL
	SELECT
		TOP 10
			0 AS Height,
			tables.Outputs.Addresses,
			'Address' AS [Type]
	FROM
		tables.Outputs
	WHERE
		tables.Outputs.Addresses LIKE '%' + @Query + '%'
