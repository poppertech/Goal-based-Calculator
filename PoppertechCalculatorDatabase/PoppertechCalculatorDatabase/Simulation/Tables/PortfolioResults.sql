CREATE TABLE [Simulation].[PortfolioResults] (
    [Id]          INT             NOT NULL,
    [Year]        VARCHAR (16)    NOT NULL,
    [Probability] DECIMAL (18, 6) NOT NULL,
    CONSTRAINT [PK_Simulation]].[PortfolioResults] PRIMARY KEY CLUSTERED ([Id] ASC)
);

