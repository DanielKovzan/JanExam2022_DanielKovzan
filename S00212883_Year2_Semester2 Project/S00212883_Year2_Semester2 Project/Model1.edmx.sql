
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/23/2022 11:44:21
-- Generated from EDMX file: C:\Users\kovza\OneDrive\School\Year 2\Semester 2\OOD\S00212883_Year2_Semester2 Project\S00212883_Year2_Semester2 Project\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [C:\USERS\KOVZA\ONEDRIVE\SCHOOL\YEAR 2\SEMESTER 2\OOD\WARHAMMER40KDB.MDF];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_FactionsSubFactions]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SubFactions1] DROP CONSTRAINT [FK_FactionsSubFactions];
GO
IF OBJECT_ID(N'[dbo].[FK_UnitTypesUnits]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Units] DROP CONSTRAINT [FK_UnitTypesUnits];
GO
IF OBJECT_ID(N'[dbo].[FK_SubFactionsUnits]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Units] DROP CONSTRAINT [FK_SubFactionsUnits];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Factions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Factions];
GO
IF OBJECT_ID(N'[dbo].[SubFactions1]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SubFactions1];
GO
IF OBJECT_ID(N'[dbo].[UnitTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UnitTypes];
GO
IF OBJECT_ID(N'[dbo].[Units]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Units];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Factions'
CREATE TABLE [dbo].[Factions] (
    [FactionID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'SubFactions1'
CREATE TABLE [dbo].[SubFactions1] (
    [SubFactionID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [FactionID] int  NOT NULL
);
GO

-- Creating table 'UnitTypes'
CREATE TABLE [dbo].[UnitTypes] (
    [UnitTypeID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Units'
CREATE TABLE [dbo].[Units] (
    [UnitUD] int IDENTITY(1,1) NOT NULL,
    [UnitTypeID] int  NOT NULL,
    [SubFactionID] int  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [UnitValue] nvarchar(max)  NOT NULL,
    [UnitImage] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [FactionID] in table 'Factions'
ALTER TABLE [dbo].[Factions]
ADD CONSTRAINT [PK_Factions]
    PRIMARY KEY CLUSTERED ([FactionID] ASC);
GO

-- Creating primary key on [SubFactionID] in table 'SubFactions1'
ALTER TABLE [dbo].[SubFactions1]
ADD CONSTRAINT [PK_SubFactions1]
    PRIMARY KEY CLUSTERED ([SubFactionID] ASC);
GO

-- Creating primary key on [UnitTypeID] in table 'UnitTypes'
ALTER TABLE [dbo].[UnitTypes]
ADD CONSTRAINT [PK_UnitTypes]
    PRIMARY KEY CLUSTERED ([UnitTypeID] ASC);
GO

-- Creating primary key on [UnitUD] in table 'Units'
ALTER TABLE [dbo].[Units]
ADD CONSTRAINT [PK_Units]
    PRIMARY KEY CLUSTERED ([UnitUD] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [FactionID] in table 'SubFactions1'
ALTER TABLE [dbo].[SubFactions1]
ADD CONSTRAINT [FK_FactionsSubFactions]
    FOREIGN KEY ([FactionID])
    REFERENCES [dbo].[Factions]
        ([FactionID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FactionsSubFactions'
CREATE INDEX [IX_FK_FactionsSubFactions]
ON [dbo].[SubFactions1]
    ([FactionID]);
GO

-- Creating foreign key on [UnitTypeID] in table 'Units'
ALTER TABLE [dbo].[Units]
ADD CONSTRAINT [FK_UnitTypesUnits]
    FOREIGN KEY ([UnitTypeID])
    REFERENCES [dbo].[UnitTypes]
        ([UnitTypeID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UnitTypesUnits'
CREATE INDEX [IX_FK_UnitTypesUnits]
ON [dbo].[Units]
    ([UnitTypeID]);
GO

-- Creating foreign key on [SubFactionID] in table 'Units'
ALTER TABLE [dbo].[Units]
ADD CONSTRAINT [FK_SubFactionsUnits]
    FOREIGN KEY ([SubFactionID])
    REFERENCES [dbo].[SubFactions1]
        ([SubFactionID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SubFactionsUnits'
CREATE INDEX [IX_FK_SubFactionsUnits]
ON [dbo].[Units]
    ([SubFactionID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------