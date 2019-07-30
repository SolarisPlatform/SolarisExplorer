CREATE PROCEDURE [storedprocedures].[InsertOutputScriptPublicKeyAddress]
	@OutputId UNIQUEIDENTIFIER,
	@Address VARCHAR(100)
AS
	INSERT INTO
		tables.OutputScriptPublicKeyAddresses
		(
			OutputId,
			[Address]
		)
	VALUES
		(
			@OutputId,
			@Address
		)
