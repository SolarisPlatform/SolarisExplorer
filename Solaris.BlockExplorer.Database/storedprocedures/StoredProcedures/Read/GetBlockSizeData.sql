CREATE PROCEDURE [storedprocedures].[GetBlockSizeData]

AS
DECLARE @MaxHeight BIGINT = 
ISNULL
(
	(
		SELECT 
			MAX([Height]) 
		FROM 
			tables.Blocks
	)
, 0)

SELECT
	tables.Blocks.Height,
	CAST(tables.Blocks.Size / CAST(1024 AS DECIMAL(28, 3)) AS DECIMAL(28, 3)) AS Size
FROM
	tables.Blocks
WHERE
	tables.Blocks.Height
BETWEEN
	@MaxHeight - 1440
AND
	@MaxHeight
