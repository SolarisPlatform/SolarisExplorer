CREATE PROCEDURE [storedprocedures].[GetDifficultyData]

AS
DECLARE @MaxTime BIGINT = 
ISNULL
(
	(
		SELECT 
			MAX([Time]) 
		FROM 
			tables.Blocks
	)
, 0)

SELECT
	[Time],
	[Difficulty],
	[Height]
FROM
	tables.Blocks
WHERE
	[Time]
BETWEEN
	@MaxTime - 24*60*60 
AND
	@MaxTime
ORDER BY
	[Time]
