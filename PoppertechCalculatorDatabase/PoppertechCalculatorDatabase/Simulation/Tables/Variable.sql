CREATE TABLE [Simulation].[Variable] (
    [Id]       INT          NOT NULL,
    [Variable] VARCHAR (16) NOT NULL,
    [ParentId] INT          NULL,
    CONSTRAINT [PK_Simulation]].[Variable] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Simulation_Variable_ParentId] FOREIGN KEY ([ParentId]) REFERENCES [Simulation].[Variable] ([Id])
);

