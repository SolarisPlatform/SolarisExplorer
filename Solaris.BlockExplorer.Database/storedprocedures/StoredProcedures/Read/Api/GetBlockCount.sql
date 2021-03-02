CREATE PROCEDURE [apistoredprocedures].[GetBlockCount]

AS
	SELECT TOP 1
		tables.Blocks.Height
	FROM
		tables.Blocks
	ORDER BY
		tables.Blocks.Height
	DESC
