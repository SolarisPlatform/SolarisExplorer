CREATE PROCEDURE [storedprocedures].[InsertOutputScriptPublicKey]
	@OutputId UNIQUEIDENTIFIER,
	@Asm VARCHAR(1024),
	@Hex VARCHAR(1024),
	@RequestedSignatures BIGINT,
	@Type VARCHAR(100)
AS
	INSERT INTO
		tables.OutputScriptPublicKey
		(
			OutputId,
			Asm,
			Hex,
			RequestedSignatures,
			Type
		)
	VALUES
		(
			@OutputId,
			@Asm,
			@Hex,
			@RequestedSignatures,
			@Type
		)