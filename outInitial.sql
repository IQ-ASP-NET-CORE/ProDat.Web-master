CREATE TABLE [History] (
    [HistoryId] int NOT NULL IDENTITY,
    [ImportId] int NOT NULL,
    [EntityName] int NOT NULL,
    [Pk1] int NOT NULL,
    [Pk2] int NOT NULL,
    [AttributeName] nvarchar(max) NULL,
    [AttributeValue] nvarchar(max) NULL,
    [Created] datetime2 NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    CONSTRAINT [PK_History] PRIMARY KEY ([HistoryId])
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20211206053401_Historian Table', N'3.1.30');

GO

ALTER TABLE [EngDataCode] ADD [EngDataClassId] int NOT NULL DEFAULT 0;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221125011438_EngDataClassNew', N'3.1.30');

GO

CREATE TABLE [EngDataClass] (
    [EngDataClassId] int NOT NULL IDENTITY,
    [EngDataClassName] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_EngDataClass] PRIMARY KEY ([EngDataClassId])
);

GO

CREATE INDEX [IX_EngDataCode_EngDataClassId] ON [EngDataCode] ([EngDataClassId]);

GO

ALTER TABLE [EngDataCode] ADD CONSTRAINT [FK_EngDataCode_EngDataClass_EngDataClassId] FOREIGN KEY ([EngDataClassId]) REFERENCES [EngDataClass] ([EngDataClassId]) ON DELETE CASCADE;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221128004020_AddEngDataClass', N'3.1.30');

GO

ALTER TABLE [Area] DROP CONSTRAINT [FK_Area_MaintenancePlant];

GO

DROP INDEX [IX_Area_MaintenancePlantID] ON [Area];

GO

EXEC sp_rename N'[EngStatus].[U_EngStatus]', N'IX_EngStatus_EngStatusName', N'INDEX';

GO

ALTER TABLE [Location] ADD [Elevation] nvarchar(max) NULL;

GO

ALTER TABLE [Location] ADD [Latitude] nvarchar(max) NULL;

GO

ALTER TABLE [Location] ADD [Longitude] nvarchar(max) NULL;

GO

ALTER TABLE [Area] ADD [Elevation] nvarchar(max) NULL;

GO

ALTER TABLE [Area] ADD [EngPlantSectionId] int NOT NULL DEFAULT 0;

GO

ALTER TABLE [Area] ADD [EngPlantSectionsEngPlantSectionId] int NOT NULL DEFAULT 0;

GO

ALTER TABLE [Area] ADD [Latitude] nvarchar(max) NULL;

GO

ALTER TABLE [Area] ADD [Longititude] nvarchar(max) NULL;

GO

CREATE TABLE [Company] (
    [CompanyId] int NOT NULL IDENTITY,
    [CompanyName] nvarchar(max) NULL,
    CONSTRAINT [PK_Company] PRIMARY KEY ([CompanyId])
);

GO

CREATE TABLE [Division] (
    [DivisionId] int NOT NULL IDENTITY,
    [DivisionName] nvarchar(max) NULL,
    [CompanyId] int NULL,
    CONSTRAINT [PK_Division] PRIMARY KEY ([DivisionId]),
    CONSTRAINT [FK_Division_Company_CompanyId] FOREIGN KEY ([CompanyId]) REFERENCES [Company] ([CompanyId]) ON DELETE NO ACTION
);

GO

CREATE TABLE [EngPlant] (
    [EngPlantId] int NOT NULL IDENTITY,
    [EngPlantName] nvarchar(max) NULL,
    [DivisionId] int NULL,
    [Longitude] nvarchar(max) NULL,
    [Latitude] nvarchar(max) NULL,
    CONSTRAINT [PK_EngPlant] PRIMARY KEY ([EngPlantId]),
    CONSTRAINT [FK_EngPlant_Division_DivisionId] FOREIGN KEY ([DivisionId]) REFERENCES [Division] ([DivisionId]) ON DELETE NO ACTION
);

GO

CREATE TABLE [EngPlantSection] (
    [EngPlantSectionId] int NOT NULL IDENTITY,
    [EngPlantSectionName] nvarchar(max) NULL,
    [EngPlantId] int NULL,
    [Longitude] nvarchar(max) NULL,
    [Latitude] nvarchar(max) NULL,
    CONSTRAINT [PK_EngPlantSection] PRIMARY KEY ([EngPlantSectionId]),
    CONSTRAINT [FK_EngPlantSection_EngPlant_EngPlantId] FOREIGN KEY ([EngPlantId]) REFERENCES [EngPlant] ([EngPlantId]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_Area_EngPlantSectionId] ON [Area] ([EngPlantSectionId]);

GO

CREATE INDEX [IX_Area_EngPlantSectionsEngPlantSectionId] ON [Area] ([EngPlantSectionsEngPlantSectionId]);

GO

CREATE INDEX [IX_Division_CompanyId] ON [Division] ([CompanyId]);

GO

CREATE INDEX [IX_EngPlant_DivisionId] ON [EngPlant] ([DivisionId]);

GO

CREATE INDEX [IX_EngPlantSection_EngPlantId] ON [EngPlantSection] ([EngPlantId]);

GO

ALTER TABLE [Area] ADD CONSTRAINT [FK_Area_MaintenancePlant_EngPlantSectionId] FOREIGN KEY ([EngPlantSectionId]) REFERENCES [MaintenancePlant] ([MaintenancePlantID]) ON DELETE NO ACTION;

GO

ALTER TABLE [Area] ADD CONSTRAINT [FK_Area_EngPlantSection_EngPlantSectionsEngPlantSectionId] FOREIGN KEY ([EngPlantSectionsEngPlantSectionId]) REFERENCES [EngPlantSection] ([EngPlantSectionId]) ON DELETE CASCADE;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221130055519_NicoChanges01', N'3.1.30');

GO

ALTER TABLE [DocType] ADD [DocTypeDiscId] int NOT NULL DEFAULT 0;

GO

ALTER TABLE [DocType] ADD [EngDiscId] int NULL;

GO

CREATE TABLE [EngClassRequiredDocs] (
    [EngClassRequiredDocsId] int NOT NULL IDENTITY,
    [EngClassId] int NOT NULL,
    [DocTypeId] int NOT NULL,
    [BCC] int NOT NULL,
    CONSTRAINT [PK_EngClassRequiredDocs] PRIMARY KEY ([EngClassRequiredDocsId]),
    CONSTRAINT [FK_EngClassRequiredDocs_DocType_DocTypeId] FOREIGN KEY ([DocTypeId]) REFERENCES [DocType] ([DocTypeID]) ON DELETE CASCADE,
    CONSTRAINT [FK_EngClassRequiredDocs_EngClass_EngClassId] FOREIGN KEY ([EngClassId]) REFERENCES [EngClass] ([EngClassID]) ON DELETE CASCADE
);

GO

CREATE TABLE [EngDataClassxEngDataCode] (
    [EngDataClassxEngDataCodeId] int NOT NULL IDENTITY,
    [EngClassId] int NOT NULL,
    [EngDataCodeId] int NOT NULL,
    CONSTRAINT [PK_EngDataClassxEngDataCode] PRIMARY KEY ([EngDataClassxEngDataCodeId]),
    CONSTRAINT [FK_EngDataClassxEngDataCode_EngClass_EngClassId] FOREIGN KEY ([EngClassId]) REFERENCES [EngClass] ([EngClassID]) ON DELETE CASCADE,
    CONSTRAINT [FK_EngDataClassxEngDataCode_EngDataCode_EngDataCodeId] FOREIGN KEY ([EngDataCodeId]) REFERENCES [EngDataCode] ([EngDataCodeID]) ON DELETE CASCADE
);

GO

CREATE TABLE [KeyList] (
    [KeyListId] int NOT NULL IDENTITY,
    [KeyListName] nvarchar(max) NULL,
    CONSTRAINT [PK_KeyList] PRIMARY KEY ([KeyListId])
);

GO

CREATE TABLE [KeyListxEngClass] (
    [KeyListxEngClassId] int NOT NULL IDENTITY,
    [KeyListId] int NOT NULL,
    [EngClassID] int NOT NULL,
    CONSTRAINT [PK_KeyListxEngClass] PRIMARY KEY ([KeyListxEngClassId]),
    CONSTRAINT [FK_KeyListxEngClass_EngClass_EngClassID] FOREIGN KEY ([EngClassID]) REFERENCES [EngClass] ([EngClassID]) ON DELETE CASCADE,
    CONSTRAINT [FK_KeyListxEngClass_KeyList_KeyListId] FOREIGN KEY ([KeyListId]) REFERENCES [KeyList] ([KeyListId]) ON DELETE CASCADE
);

GO

CREATE TABLE [KeyListxEngDataCode] (
    [KeyListxEngDataCodeId] int NOT NULL IDENTITY,
    [KeyListId] int NOT NULL,
    [EngDataCode] int NOT NULL,
    [EngDataCodesEngDataCodeId] int NULL,
    CONSTRAINT [PK_KeyListxEngDataCode] PRIMARY KEY ([KeyListxEngDataCodeId]),
    CONSTRAINT [FK_KeyListxEngDataCode_EngDataCode_EngDataCodesEngDataCodeId] FOREIGN KEY ([EngDataCodesEngDataCodeId]) REFERENCES [EngDataCode] ([EngDataCodeID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_KeyListxEngDataCode_KeyList_KeyListId] FOREIGN KEY ([KeyListId]) REFERENCES [KeyList] ([KeyListId]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_DocType_EngDiscId] ON [DocType] ([EngDiscId]);

GO

CREATE INDEX [IX_EngClassRequiredDocs_DocTypeId] ON [EngClassRequiredDocs] ([DocTypeId]);

GO

CREATE UNIQUE INDEX [IX_EngClassRequiredDocs_EngClassId] ON [EngClassRequiredDocs] ([EngClassId]);

GO

CREATE INDEX [IX_EngDataClassxEngDataCode_EngClassId] ON [EngDataClassxEngDataCode] ([EngClassId]);

GO

CREATE INDEX [IX_EngDataClassxEngDataCode_EngDataCodeId] ON [EngDataClassxEngDataCode] ([EngDataCodeId]);

GO

CREATE INDEX [IX_KeyListxEngClass_EngClassID] ON [KeyListxEngClass] ([EngClassID]);

GO

CREATE INDEX [IX_KeyListxEngClass_KeyListId] ON [KeyListxEngClass] ([KeyListId]);

GO

CREATE INDEX [IX_KeyListxEngDataCode_EngDataCodesEngDataCodeId] ON [KeyListxEngDataCode] ([EngDataCodesEngDataCodeId]);

GO

CREATE INDEX [IX_KeyListxEngDataCode_KeyListId] ON [KeyListxEngDataCode] ([KeyListId]);

GO

ALTER TABLE [DocType] ADD CONSTRAINT [FK_DocType_EngDisc_EngDiscId] FOREIGN KEY ([EngDiscId]) REFERENCES [EngDisc] ([EngDiscID]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221207050315_NicoChanges02', N'3.1.30');

GO

ALTER TABLE [Area] DROP CONSTRAINT [FK_Area_MaintenancePlant_EngPlantSectionId];

GO

ALTER TABLE [Area] DROP CONSTRAINT [FK_Area_EngPlantSection_EngPlantSectionsEngPlantSectionId];

GO

DROP TABLE [EngPlantSection];

GO

DROP TABLE [EngPlant];

GO

DROP TABLE [Division];

GO

DROP TABLE [Company];

GO

DROP INDEX [IX_Area_EngPlantSectionId] ON [Area];

GO

DROP INDEX [IX_Area_EngPlantSectionsEngPlantSectionId] ON [Area];

GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Area]') AND [c].[name] = N'Elevation');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Area] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Area] DROP COLUMN [Elevation];

GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Area]') AND [c].[name] = N'EngPlantSectionId');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Area] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Area] DROP COLUMN [EngPlantSectionId];

GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Area]') AND [c].[name] = N'EngPlantSectionsEngPlantSectionId');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Area] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Area] DROP COLUMN [EngPlantSectionsEngPlantSectionId];

GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Area]') AND [c].[name] = N'Latitude');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Area] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [Area] DROP COLUMN [Latitude];

GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Area]') AND [c].[name] = N'Longititude');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Area] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [Area] DROP COLUMN [Longititude];

GO

ALTER TABLE [Location] ADD [LocationType] nvarchar(max) NULL;

GO

ALTER TABLE [Location] ADD [LocationsLocationId] int NULL;

GO

ALTER TABLE [Location] ADD [ParentLocation] nvarchar(max) NULL;

GO

CREATE TABLE [LocationTypes] (
    [LocationTypesId] int NOT NULL IDENTITY,
    [LocationTypeDescription] nvarchar(max) NOT NULL,
    [LocationsPLocationTypesId] int NULL,
    CONSTRAINT [PK_LocationTypes] PRIMARY KEY ([LocationTypesId]),
    CONSTRAINT [FK_LocationTypes_LocationTypes_LocationsPLocationTypesId] FOREIGN KEY ([LocationsPLocationTypesId]) REFERENCES [LocationTypes] ([LocationTypesId]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_Location_LocationsLocationId] ON [Location] ([LocationsLocationId]);

GO

CREATE INDEX [IX_Area_MaintenancePlantID] ON [Area] ([MaintenancePlantID]);

GO

CREATE INDEX [IX_LocationTypes_LocationsPLocationTypesId] ON [LocationTypes] ([LocationsPLocationTypesId]);

GO

ALTER TABLE [Area] ADD CONSTRAINT [FK_Area_MaintenancePlant_MaintenancePlantID] FOREIGN KEY ([MaintenancePlantID]) REFERENCES [MaintenancePlant] ([MaintenancePlantID]) ON DELETE CASCADE;

GO

ALTER TABLE [Location] ADD CONSTRAINT [FK_Location_Location_LocationsLocationId] FOREIGN KEY ([LocationsLocationId]) REFERENCES [Location] ([LocationID]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221208055939_Schema3Changes', N'3.1.30');

GO

ALTER TABLE [Location] DROP CONSTRAINT [FK_Location_Location_LocationsLocationId];

GO

DROP INDEX [IX_Location_LocationsLocationId] ON [Location];

GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Location]') AND [c].[name] = N'LocationsLocationId');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Location] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [Location] DROP COLUMN [LocationsLocationId];

GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Location]') AND [c].[name] = N'ParentLocation');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Location] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [Location] ALTER COLUMN [ParentLocation] int NOT NULL;

GO

DECLARE @var7 sysname;
SELECT @var7 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Location]') AND [c].[name] = N'Elevation');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [Location] DROP CONSTRAINT [' + @var7 + '];');
ALTER TABLE [Location] ALTER COLUMN [Elevation] int NOT NULL;

GO

ALTER TABLE [Location] ADD [ParentLocationsLocationId] int NULL;

GO

CREATE INDEX [IX_Location_ParentLocationsLocationId] ON [Location] ([ParentLocationsLocationId]);

GO

ALTER TABLE [Location] ADD CONSTRAINT [FK_Location_Location_ParentLocationsLocationId] FOREIGN KEY ([ParentLocationsLocationId]) REFERENCES [Location] ([LocationID]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221208081828_Schema3ChangesMod', N'3.1.30');

GO

