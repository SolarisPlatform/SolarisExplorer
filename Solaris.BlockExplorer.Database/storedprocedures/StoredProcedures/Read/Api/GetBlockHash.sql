CREATE PROCEDURE [apistoredprocedures].[GetBlockHash]
	@Index BIGINT
AS
	SELECT
		tables.Blocks.Id
	FROM
		tables.Blocks
	WHERE
		tables.Blocks.Height = @Index
