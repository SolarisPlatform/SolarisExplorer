CREATE PROCEDURE [apistoredprocedures].[GetBlock]
	@Hash CHAR(64)
AS
	SELECT
		tables.Blocks.Json
	FROM
		tables.Blocks
	WHERE
		tables.Blocks.Id = @Hash