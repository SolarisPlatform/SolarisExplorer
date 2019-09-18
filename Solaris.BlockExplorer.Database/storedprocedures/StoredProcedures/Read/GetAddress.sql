CREATE PROCEDURE [storedprocedures].[GetAddress]
	@PublicKey VARCHAR(100)
AS
SELECT
	ISNULL(
		SUM(
		CASE WHEN 
			tables.AddressTransactions.Value < 0 AND 
			tables.AddressTransactions.IsMined = 0 THEN
				tables.AddressTransactions.[Value]
			ELSE
				0
			END
		), 0) AS Sent,
	ISNULL(
		SUM(
		CASE WHEN 
			tables.AddressTransactions.Value < 0 AND 
			tables.AddressTransactions.IsMined = 0 THEN
				1
			ELSE
				NULL
			END
		), 0) AS SentCount,
	ISNULL(
		SUM(
		CASE WHEN 
			tables.AddressTransactions.Value > 0 AND 
			tables.AddressTransactions.IsMined = 0 THEN
				tables.AddressTransactions.[Value]
			ELSE
				0
			END
		), 0) AS Received,
	ISNULL(
		COUNT(
		CASE WHEN 
			tables.AddressTransactions.Value > 0 AND 
			tables.AddressTransactions.IsMined = 0 THEN
				1
			ELSE
				NULL
			END
		), 0) AS ReceivedCount,
	ISNULL(
		SUM(
		CASE WHEN 
			tables.AddressTransactions.IsMined = 1 THEN
				tables.AddressTransactions.[Value]
			ELSE
				0
			END
		), 0) AS Mined,
	ISNULL(
		COUNT(
		CASE WHEN 
			tables.AddressTransactions.IsMined = 1 THEN
				1
			ELSE
				NULL
			END
		), 0) AS MinedCount,
	ISNULL(
		SUM(tables.AddressTransactions.[Value]), 0) AS Balance,
		@PublicKey AS PublicKey
FROM
	tables.AddressTransactions
WHERE
	tables.AddressTransactions.Addresses like '%' + @PublicKey + '%'
