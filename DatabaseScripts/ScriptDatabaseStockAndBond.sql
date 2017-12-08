USE [master]
GO
/****** Object:  Database [ProbicastCalculator]    Script Date: 12/8/2017 6:11:58 AM ******/
CREATE DATABASE [ProbicastCalculator]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ProbicastCalculator', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\ProbicastCalculator.mdf' , SIZE = 7168KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'ProbicastCalculator_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\ProbicastCalculator_log.ldf' , SIZE = 20096KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [ProbicastCalculator] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ProbicastCalculator].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ProbicastCalculator] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ProbicastCalculator] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ProbicastCalculator] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ProbicastCalculator] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ProbicastCalculator] SET ARITHABORT OFF 
GO
ALTER DATABASE [ProbicastCalculator] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ProbicastCalculator] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ProbicastCalculator] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ProbicastCalculator] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ProbicastCalculator] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ProbicastCalculator] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ProbicastCalculator] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ProbicastCalculator] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ProbicastCalculator] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ProbicastCalculator] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ProbicastCalculator] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ProbicastCalculator] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ProbicastCalculator] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ProbicastCalculator] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ProbicastCalculator] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ProbicastCalculator] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ProbicastCalculator] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ProbicastCalculator] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ProbicastCalculator] SET  MULTI_USER 
GO
ALTER DATABASE [ProbicastCalculator] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ProbicastCalculator] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ProbicastCalculator] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ProbicastCalculator] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [ProbicastCalculator] SET DELAYED_DURABILITY = DISABLED 
GO
USE [ProbicastCalculator]
GO
/****** Object:  Schema [Simulation]    Script Date: 12/8/2017 6:11:58 AM ******/
CREATE SCHEMA [Simulation]
GO
/****** Object:  Table [Simulation].[Forecast]    Script Date: 12/8/2017 6:11:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Simulation].[Forecast](
	[Id] [int] NOT NULL,
	[ForecastTypeId] [int] NOT NULL,
	[Forecast] [decimal](18, 6) NOT NULL,
	[RegionId] [int] NULL,
 CONSTRAINT [PK_Simulation]].[Forecast] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Simulation].[ForecastType]    Script Date: 12/8/2017 6:11:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Simulation].[ForecastType](
	[Id] [int] NOT NULL,
	[ForecastType] [varchar](16) NOT NULL,
 CONSTRAINT [PK_Simulation]].[ForecastType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Simulation].[ForecastVariable]    Script Date: 12/8/2017 6:11:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Simulation].[ForecastVariable](
	[Id] [int] NOT NULL,
	[VariableId] [int] NOT NULL,
	[ForecastId] [int] NOT NULL,
 CONSTRAINT [PK_Simulation]].[ForecastVariableId] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Simulation].[PortfolioResults]    Script Date: 12/8/2017 6:11:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Simulation].[PortfolioResults](
	[Id] [int] NOT NULL,
	[Year] [varchar](16) NOT NULL,
	[Probability] [decimal](18, 6) NOT NULL,
 CONSTRAINT [PK_Simulation]].[PortfolioResults] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Simulation].[Region]    Script Date: 12/8/2017 6:11:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Simulation].[Region](
	[Id] [int] NOT NULL,
	[Region] [varchar](16) NOT NULL,
 CONSTRAINT [PK_Simulation]].[Region] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Simulation].[UniformRandom]    Script Date: 12/8/2017 6:11:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Simulation].[UniformRandom](
	[Id] [int] NOT NULL,
	[VariableId] [int] NOT NULL,
	[RegionId] [int] NULL,
	[Rand] [decimal](24, 18) NOT NULL,
 CONSTRAINT [PK_Simulation]].[UniformRandom] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Simulation].[Variable]    Script Date: 12/8/2017 6:11:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Simulation].[Variable](
	[Id] [int] NOT NULL,
	[Variable] [varchar](16) NOT NULL,
	[ParentId] [int] NULL,
 CONSTRAINT [PK_Simulation]].[Variable] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [Simulation].[Forecast]  WITH CHECK ADD  CONSTRAINT [FK_Simulation]].[Forecast_RegionId] FOREIGN KEY([RegionId])
REFERENCES [Simulation].[Region] ([Id])
GO
ALTER TABLE [Simulation].[Forecast] CHECK CONSTRAINT [FK_Simulation]].[Forecast_RegionId]
GO
ALTER TABLE [Simulation].[Forecast]  WITH CHECK ADD  CONSTRAINT [FK_Simulation]].[ForecastTypeId] FOREIGN KEY([ForecastTypeId])
REFERENCES [Simulation].[ForecastType] ([Id])
GO
ALTER TABLE [Simulation].[Forecast] CHECK CONSTRAINT [FK_Simulation]].[ForecastTypeId]
GO
ALTER TABLE [Simulation].[ForecastVariable]  WITH CHECK ADD  CONSTRAINT [FK_Simulation]].[ForecastVariable_ForecastId] FOREIGN KEY([ForecastId])
REFERENCES [Simulation].[Forecast] ([Id])
GO
ALTER TABLE [Simulation].[ForecastVariable] CHECK CONSTRAINT [FK_Simulation]].[ForecastVariable_ForecastId]
GO
ALTER TABLE [Simulation].[ForecastVariable]  WITH CHECK ADD  CONSTRAINT [FK_Simulation]].[ForecastVariable_VariableId] FOREIGN KEY([VariableId])
REFERENCES [Simulation].[Variable] ([Id])
GO
ALTER TABLE [Simulation].[ForecastVariable] CHECK CONSTRAINT [FK_Simulation]].[ForecastVariable_VariableId]
GO
ALTER TABLE [Simulation].[UniformRandom]  WITH CHECK ADD  CONSTRAINT [FK_Simulation]].[RegionId] FOREIGN KEY([RegionId])
REFERENCES [Simulation].[Region] ([Id])
GO
ALTER TABLE [Simulation].[UniformRandom] CHECK CONSTRAINT [FK_Simulation]].[RegionId]
GO
ALTER TABLE [Simulation].[UniformRandom]  WITH CHECK ADD  CONSTRAINT [FK_Simulation]].[VariableId] FOREIGN KEY([VariableId])
REFERENCES [Simulation].[Variable] ([Id])
GO
ALTER TABLE [Simulation].[UniformRandom] CHECK CONSTRAINT [FK_Simulation]].[VariableId]
GO
ALTER TABLE [Simulation].[Variable]  WITH CHECK ADD  CONSTRAINT [FK_Simulation_Variable_ParentId] FOREIGN KEY([ParentId])
REFERENCES [Simulation].[Variable] ([Id])
GO
ALTER TABLE [Simulation].[Variable] CHECK CONSTRAINT [FK_Simulation_Variable_ParentId]
GO
/****** Object:  StoredProcedure [Simulation].[GetForecastVariables]    Script Date: 12/8/2017 6:11:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Simulation].[GetForecastVariables]
AS
SELECT DISTINCT
	forecast_variable.Id AS Id,
	forecast_variable.VariableId AS VariableId,
	forecast_variable.ForecastId AS ForecastId,
	variable.Variable AS Variable,
	region.Region AS Region,
	forecast_type.ForecastType AS ForecastType,
	forecast.Forecast AS Forecast,
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
GO
/****** Object:  StoredProcedure [Simulation].[GetPortfolioResults]    Script Date: 12/8/2017 6:11:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Simulation].[GetPortfolioResults]
AS
SELECT [Id], [Year], [Probability] FROM [Simulation].[PortfolioResults]
GO
/****** Object:  StoredProcedure [Simulation].[GetUniformRandByRegion]    Script Date: 12/8/2017 6:11:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
GO
USE [master]
GO
ALTER DATABASE [ProbicastCalculator] SET  READ_WRITE 
GO
