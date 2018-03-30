CREATE TABLE [Simulation].[UniformRandom] (
    [Id]         INT              NOT NULL,
    [VariableId] INT              NOT NULL,
    [RegionId]   INT              NULL,
    [Rand]       DECIMAL (24, 18) NOT NULL,
    CONSTRAINT [PK_Simulation]].[UniformRandom] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Simulation]].[RegionId] FOREIGN KEY ([RegionId]) REFERENCES [Simulation].[Region] ([Id]),
    CONSTRAINT [FK_Simulation]].[VariableId] FOREIGN KEY ([VariableId]) REFERENCES [Simulation].[Variable] ([Id])
);

