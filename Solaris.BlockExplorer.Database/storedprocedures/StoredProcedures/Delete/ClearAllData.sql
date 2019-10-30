CREATE PROCEDURE [storedprocedures].[ClearAllData]

AS
DELETE FROM tables.Inputs
DELETE FROM tables.Blocks
DELETE FROM tables.Outputs
DELETE FROM tables.Transactions
DELETE FROM tables.AddressTransactions
