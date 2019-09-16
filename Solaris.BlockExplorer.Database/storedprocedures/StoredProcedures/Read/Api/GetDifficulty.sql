CREATE PROCEDURE [apistoredprocedures].[GetDifficulty]

AS
	SELECT TOP (1)
		tables.Blocks.Difficulty
	FROM
		tables.Blocks
	ORDER BY
		tables.Blocks.Height
	DESC
