CREATE FUNCTION [functions].[GetBlockHeight]
(
	
)
RETURNS BIGINT
AS
BEGIN
	RETURN
	(
		SELECT
			MAX(tables.Blocks.Height)
		FROM
			tables.Blocks
	)
END
