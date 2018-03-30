CREATE PROCEDURE [Simulation].[GetForecastVariables]
AS
SELECT DISTINCT
	forecast_variable.Id AS Id,
	forecast_variable.VariableId AS VariableId,
	forecast_variable.ForecastId AS ForecastId,
	variable.Variable AS Variable,
	region.Region AS Region,
	forecast_type.ForecastType AS ForecastType,
	Cast(forecast.Forecast as decimal(18,2)) AS Forecast,
	parent_variable.Variable AS Parent
FROM [Simulation].ForecastVariable AS forecast_variable
JOIN [Simulation].Forecast AS forecast
ON forecast.Id = forecast_variable.ForecastId
JOIN [Simulation].Variable AS variable
ON variable.Id = forecast_variable.VariableId
JOIN [Simulation].[ForecastType] As forecast_type
ON forecast_type.Id = forecast.ForecastTypeId
LEFT JOIN [Simulation].Region AS region
ON region.Id = forecast.RegionId
LEFT JOIN [Simulation].Variable AS parent_variable
ON parent_variable.Id = variable.ParentId