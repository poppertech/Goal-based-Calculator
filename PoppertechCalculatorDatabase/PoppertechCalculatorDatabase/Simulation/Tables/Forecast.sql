CREATE TABLE [Simulation].[Forecast] (
    [Id]             INT             NOT NULL,
    [ForecastTypeId] INT             NOT NULL,
    [Forecast]       DECIMAL (18, 6) NOT NULL,
    [RegionId]       INT             NULL,
    CONSTRAINT [PK_Simulation]].[Forecast] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Simulation]].[Forecast_RegionId] FOREIGN KEY ([RegionId]) REFERENCES [Simulation].[Region] ([Id]),
    CONSTRAINT [FK_Simulation]].[ForecastTypeId] FOREIGN KEY ([ForecastTypeId]) REFERENCES [Simulation].[ForecastType] ([Id])
);

