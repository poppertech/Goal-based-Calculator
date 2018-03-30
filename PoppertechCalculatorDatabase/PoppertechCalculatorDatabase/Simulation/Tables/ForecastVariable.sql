CREATE TABLE [Simulation].[ForecastVariable] (
    [Id]         INT NOT NULL,
    [VariableId] INT NOT NULL,
    [ForecastId] INT NOT NULL,
    CONSTRAINT [PK_Simulation]].[ForecastVariableId] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Simulation]].[ForecastVariable_ForecastId] FOREIGN KEY ([ForecastId]) REFERENCES [Simulation].[Forecast] ([Id]),
    CONSTRAINT [FK_Simulation]].[ForecastVariable_VariableId] FOREIGN KEY ([VariableId]) REFERENCES [Simulation].[Variable] ([Id])
);

