CREATE PROCEDURE [Simulation].[GetUniformRandByRegion]
	@Variable varchar(16),
	@Region varchar(16) = NULL
AS
SELECT uniform_random.Id AS Id, 
	   region.Region AS Region, 
	   uniform_random.[Rand] AS [Rand] 
FROM [Simulation].[UniformRandom] AS uniform_random 
JOIN [Simulation].[Variable] AS variable
ON variable.Id = uniform_random.VariableId
LEFT JOIN [Simulation].[Region] AS region
ON region.Id = uniform_random.RegionId
WHERE variable.Variable = @Variable
AND (@Region IS NULL OR region.Region = @Region);