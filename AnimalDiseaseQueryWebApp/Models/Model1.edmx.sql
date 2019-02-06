
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 02/06/2019 15:45:46
-- Generated from EDMX file: C:\Users\spike\source\repos\AnimalDiseaseQueryPage\AnimalDiseaseQueryWebApp\Models\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ADDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Animals]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Animals];
GO
IF OBJECT_ID(N'[dbo].[Diseases]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Diseases];
GO
IF OBJECT_ID(N'[dbo].[Signs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Signs];
GO
IF OBJECT_ID(N'[dbo].[Priors]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Priors];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Animals'
CREATE TABLE [dbo].[Animals] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Sex] nvarchar(max)  NOT NULL,
    [Age] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Diseases'
CREATE TABLE [dbo].[Diseases] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Probability] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Signs'
CREATE TABLE [dbo].[Signs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [AnimalId] int  NOT NULL,
    [Type_of_Value] nvarchar(max)  NOT NULL,
    [Value] nvarchar(max)  NOT NULL,
    [Probability] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Priors'
CREATE TABLE [dbo].[Priors] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [AnimalId] int  NOT NULL,
    [DiseaseId] int  NOT NULL,
    [Probability] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Probabilities'
CREATE TABLE [dbo].[Probabilities] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Value] nvarchar(max)  NOT NULL,
    [AnimalId] int  NOT NULL,
    [SignId] int  NOT NULL,
    [DiseaseId] int  NOT NULL
);
GO

-- Creating table 'Treatments'
CREATE TABLE [dbo].[Treatments] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Info] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'DiseaseTreatment'
CREATE TABLE [dbo].[DiseaseTreatment] (
    [Diseases_Id] int  NOT NULL,
    [Treatments_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Animals'
ALTER TABLE [dbo].[Animals]
ADD CONSTRAINT [PK_Animals]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Diseases'
ALTER TABLE [dbo].[Diseases]
ADD CONSTRAINT [PK_Diseases]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Signs'
ALTER TABLE [dbo].[Signs]
ADD CONSTRAINT [PK_Signs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Priors'
ALTER TABLE [dbo].[Priors]
ADD CONSTRAINT [PK_Priors]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Probabilities'
ALTER TABLE [dbo].[Probabilities]
ADD CONSTRAINT [PK_Probabilities]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Treatments'
ALTER TABLE [dbo].[Treatments]
ADD CONSTRAINT [PK_Treatments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Diseases_Id], [Treatments_Id] in table 'DiseaseTreatment'
ALTER TABLE [dbo].[DiseaseTreatment]
ADD CONSTRAINT [PK_DiseaseTreatment]
    PRIMARY KEY CLUSTERED ([Diseases_Id], [Treatments_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [AnimalId] in table 'Priors'
ALTER TABLE [dbo].[Priors]
ADD CONSTRAINT [FK_AnimalPriors]
    FOREIGN KEY ([AnimalId])
    REFERENCES [dbo].[Animals]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AnimalPriors'
CREATE INDEX [IX_FK_AnimalPriors]
ON [dbo].[Priors]
    ([AnimalId]);
GO

-- Creating foreign key on [DiseaseId] in table 'Priors'
ALTER TABLE [dbo].[Priors]
ADD CONSTRAINT [FK_DiseasePriors]
    FOREIGN KEY ([DiseaseId])
    REFERENCES [dbo].[Diseases]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DiseasePriors'
CREATE INDEX [IX_FK_DiseasePriors]
ON [dbo].[Priors]
    ([DiseaseId]);
GO

-- Creating foreign key on [AnimalId] in table 'Probabilities'
ALTER TABLE [dbo].[Probabilities]
ADD CONSTRAINT [FK_ProbabilityAnimal]
    FOREIGN KEY ([AnimalId])
    REFERENCES [dbo].[Animals]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProbabilityAnimal'
CREATE INDEX [IX_FK_ProbabilityAnimal]
ON [dbo].[Probabilities]
    ([AnimalId]);
GO

-- Creating foreign key on [SignId] in table 'Probabilities'
ALTER TABLE [dbo].[Probabilities]
ADD CONSTRAINT [FK_SignProbability]
    FOREIGN KEY ([SignId])
    REFERENCES [dbo].[Signs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SignProbability'
CREATE INDEX [IX_FK_SignProbability]
ON [dbo].[Probabilities]
    ([SignId]);
GO

-- Creating foreign key on [DiseaseId] in table 'Probabilities'
ALTER TABLE [dbo].[Probabilities]
ADD CONSTRAINT [FK_DiseaseProbability]
    FOREIGN KEY ([DiseaseId])
    REFERENCES [dbo].[Diseases]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DiseaseProbability'
CREATE INDEX [IX_FK_DiseaseProbability]
ON [dbo].[Probabilities]
    ([DiseaseId]);
GO

-- Creating foreign key on [AnimalId] in table 'Signs'
ALTER TABLE [dbo].[Signs]
ADD CONSTRAINT [FK_AnimalSign]
    FOREIGN KEY ([AnimalId])
    REFERENCES [dbo].[Animals]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AnimalSign'
CREATE INDEX [IX_FK_AnimalSign]
ON [dbo].[Signs]
    ([AnimalId]);
GO

-- Creating foreign key on [Diseases_Id] in table 'DiseaseTreatment'
ALTER TABLE [dbo].[DiseaseTreatment]
ADD CONSTRAINT [FK_DiseaseTreatment_Disease]
    FOREIGN KEY ([Diseases_Id])
    REFERENCES [dbo].[Diseases]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Treatments_Id] in table 'DiseaseTreatment'
ALTER TABLE [dbo].[DiseaseTreatment]
ADD CONSTRAINT [FK_DiseaseTreatment_Treatment]
    FOREIGN KEY ([Treatments_Id])
    REFERENCES [dbo].[Treatments]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DiseaseTreatment_Treatment'
CREATE INDEX [IX_FK_DiseaseTreatment_Treatment]
ON [dbo].[DiseaseTreatment]
    ([Treatments_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------