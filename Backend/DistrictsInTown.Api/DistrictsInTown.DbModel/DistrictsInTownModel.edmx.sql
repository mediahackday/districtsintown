
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/01/2015 12:13:22
-- Generated from EDMX file: C:\Users\prochnowc\Documents\Visual Studio 2013\Projects\districtsintown\Backend\DistrictsInTown.Api\DistrictsInTown.DbModel\DistrictsInTownModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [DistrictsInTown];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Places'
CREATE TABLE [dbo].[Places] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Location] geography  NOT NULL,
    [Score] float  NOT NULL,
    [Keyword] nvarchar(max)  NOT NULL,
    [Source] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Places'
ALTER TABLE [dbo].[Places]
ADD CONSTRAINT [PK_Places]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------