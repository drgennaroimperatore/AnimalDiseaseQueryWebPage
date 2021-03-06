
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 02/14/2019 11:52:35
-- Generated from EDMX file: C:\Users\spike\source\repos\AnimalDiseaseQueryPage\AnimalDiseaseQueryWebApp\Models\Model1.edmx
-- --------------------------------------------------



-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ProbabilityAnimal]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Likelihoods] DROP CONSTRAINT [FK_ProbabilityAnimal];
GO
IF OBJECT_ID(N'[dbo].[FK_SignProbability]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Likelihoods] DROP CONSTRAINT [FK_SignProbability];
GO
IF OBJECT_ID(N'[dbo].[FK_DiseaseProbability]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Likelihoods] DROP CONSTRAINT [FK_DiseaseProbability];
GO
IF OBJECT_ID(N'[dbo].[FK_DiseaseTreatment_Disease]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DiseaseTreatment] DROP CONSTRAINT [FK_DiseaseTreatment_Disease];
GO
IF OBJECT_ID(N'[dbo].[FK_DiseaseTreatment_Treatment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DiseaseTreatment] DROP CONSTRAINT [FK_DiseaseTreatment_Treatment];
GO
IF OBJECT_ID(N'[dbo].[FK_DiseasePriorsDiseases]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Diseases] DROP CONSTRAINT [FK_DiseasePriorsDiseases];
GO

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
IF OBJECT_ID(N'[dbo].[PriorsDiseases]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PriorsDiseases];
GO
IF OBJECT_ID(N'[dbo].[Likelihoods]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Likelihoods];
GO
IF OBJECT_ID(N'[dbo].[Treatments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Treatments];
GO
IF OBJECT_ID(N'[dbo].[DiseaseTreatment]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DiseaseTreatment];
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
    [PriorsDiseas_Id] int  NOT NULL
);
GO

-- Creating table 'Signs'
CREATE TABLE [dbo].[Signs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Type_of_Value] int  NOT NULL,
    [Probability] nvarchar(max)  NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'PriorsDiseases'
CREATE TABLE [dbo].[PriorsDiseases] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Probability] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Likelihoods'
CREATE TABLE [dbo].[Likelihoods] (
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

-- Creating primary key on [Id] in table 'PriorsDiseases'
ALTER TABLE [dbo].[PriorsDiseases]
ADD CONSTRAINT [PK_PriorsDiseases]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Likelihoods'
ALTER TABLE [dbo].[Likelihoods]
ADD CONSTRAINT [PK_Likelihoods]
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

-- Creating foreign key on [AnimalId] in table 'Likelihoods'
ALTER TABLE [dbo].[Likelihoods]
ADD CONSTRAINT [FK_ProbabilityAnimal]
    FOREIGN KEY ([AnimalId])
    REFERENCES [dbo].[Animals]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProbabilityAnimal'
CREATE INDEX [IX_FK_ProbabilityAnimal]
ON [dbo].[Likelihoods]
    ([AnimalId]);
GO

-- Creating foreign key on [SignId] in table 'Likelihoods'
ALTER TABLE [dbo].[Likelihoods]
ADD CONSTRAINT [FK_SignProbability]
    FOREIGN KEY ([SignId])
    REFERENCES [dbo].[Signs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SignProbability'
CREATE INDEX [IX_FK_SignProbability]
ON [dbo].[Likelihoods]
    ([SignId]);
GO

-- Creating foreign key on [DiseaseId] in table 'Likelihoods'
ALTER TABLE [dbo].[Likelihoods]
ADD CONSTRAINT [FK_DiseaseProbability]
    FOREIGN KEY ([DiseaseId])
    REFERENCES [dbo].[Diseases]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DiseaseProbability'
CREATE INDEX [IX_FK_DiseaseProbability]
ON [dbo].[Likelihoods]
    ([DiseaseId]);
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

-- Creating foreign key on [PriorsDiseas_Id] in table 'Diseases'
ALTER TABLE [dbo].[Diseases]
ADD CONSTRAINT [FK_DiseasePriorsDiseases]
    FOREIGN KEY ([PriorsDiseas_Id])
    REFERENCES [dbo].[PriorsDiseases]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DiseasePriorsDiseases'
CREATE INDEX [IX_FK_DiseasePriorsDiseases]
ON [dbo].[Diseases]
    ([PriorsDiseas_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------