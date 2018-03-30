CREATE PROCEDURE [Simulation].[GetPortfolioResults]
AS
SELECT [Id], [Year], [Probability] FROM [Simulation].[PortfolioResults]