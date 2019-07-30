CREATE PROCEDURE [storedprocedures].[InsertInputScriptSignature]
	@InputId UNIQUEIDENTIFIER,
	@Asm VARCHAR(1024),
	@Hex VARCHAR(1024)
AS
	INSERT INTO
		tables.InputScriptSignature
		(
			InputId,
			Asm,
			Hex
		)
	VALUES
		(
			@InputId,
			@Asm,
			@Hex
		)