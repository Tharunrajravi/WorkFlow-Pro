-- ===============================================================================
-- Workflow Pro - Complete Database Creation Script
-- Generated from Entity Framework Code-First Models
-- Database Engine: Microsoft SQL Server
-- Connection String: Data Source=localhost\SQLEXPRESS;Initial Catalog=EmployeeDB;Integrated Security=True;TrustServerCertificate=True
-- ===============================================================================

IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'EmployeeDB')
BEGIN
    CREATE DATABASE [EmployeeDB];
END
GO

USE [EmployeeDB];
GO

-- 1. Create Departments Table
IF OBJECT_ID(N'[dbo].[Departments]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Departments] (
        [Id] INT IDENTITY(1,1) NOT NULL,
        [DepartmentName] NVARCHAR(100) NOT NULL,
        [Code] NVARCHAR(20) NOT NULL,
        [Description] NVARCHAR(250) NULL,
        [IsActive] BIT NOT NULL CONSTRAINT [DF_Departments_IsActive] DEFAULT ((1)),
        [CreatedDate] DATETIME2 NOT NULL CONSTRAINT [DF_Departments_CreatedDate] DEFAULT (GETUTCDATE()),
        CONSTRAINT [PK_Departments] PRIMARY KEY CLUSTERED ([Id] ASC)
    );

    CREATE UNIQUE NONCLUSTERED INDEX [IX_Department_Code] ON [dbo].[Departments]([Code] ASC);
END
GO

-- 2. Create Employees Table
IF OBJECT_ID(N'[dbo].[Employees]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Employees] (
        [Id] INT IDENTITY(1,1) NOT NULL,
        [EmployeeCode] NVARCHAR(50) NOT NULL,
        [FirstName] NVARCHAR(50) NOT NULL,
        [LastName] NVARCHAR(50) NOT NULL,
        [Email] NVARCHAR(100) NOT NULL,
        [Phone] NVARCHAR(20) NULL,
        [Designation] NVARCHAR(100) NULL,
        [DepartmentId] INT NOT NULL,
        [HireDate] DATETIME2 NOT NULL,
        [Salary] DECIMAL(18, 2) NOT NULL CONSTRAINT [DF_Employees_Salary] DEFAULT ((0.00)),
        [IsActive] BIT NOT NULL CONSTRAINT [DF_Employees_IsActive] DEFAULT ((1)),
        [ProfileImagePath] NVARCHAR(500) NULL,
        [CreatedDate] DATETIME2 NOT NULL CONSTRAINT [DF_Employees_CreatedDate] DEFAULT (GETUTCDATE()),
        CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED ([Id] ASC),
        CONSTRAINT [FK_Employees_Departments] FOREIGN KEY ([DepartmentId]) REFERENCES [dbo].[Departments] ([Id])
    );

    CREATE UNIQUE NONCLUSTERED INDEX [IX_Employee_EmployeeCode] ON [dbo].[Employees]([EmployeeCode] ASC);
    CREATE UNIQUE NONCLUSTERED INDEX [IX_Employee_Email] ON [dbo].[Employees]([Email] ASC);
END
GO

-- 3. Create Users Table
IF OBJECT_ID(N'[dbo].[Users]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Users] (
        [Id] INT IDENTITY(1,1) NOT NULL,
        [Username] NVARCHAR(50) NOT NULL,
        [PasswordHash] NVARCHAR(256) NOT NULL,
        [Email] NVARCHAR(100) NOT NULL,
        [Role] NVARCHAR(30) NOT NULL,
        [IsActive] BIT NOT NULL CONSTRAINT [DF_Users_IsActive] DEFAULT ((1)),
        [EmployeeId] INT NULL,
        [LastLoginDate] DATETIME2 NULL,
        [CreatedDate] DATETIME2 NOT NULL CONSTRAINT [DF_Users_CreatedDate] DEFAULT (GETUTCDATE()),
        CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC),
        CONSTRAINT [FK_Users_Employees] FOREIGN KEY ([EmployeeId]) REFERENCES [dbo].[Employees] ([Id])
    );

    CREATE UNIQUE NONCLUSTERED INDEX [IX_User_Username] ON [dbo].[Users]([Username] ASC);
    CREATE UNIQUE NONCLUSTERED INDEX [IX_User_Email] ON [dbo].[Users]([Email] ASC);
END
GO

-- 4. Create Projects Table
IF OBJECT_ID(N'[dbo].[Projects]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Projects] (
        [Id] INT IDENTITY(1,1) NOT NULL,
        [ProjectName] NVARCHAR(150) NOT NULL,
        [ProjectCode] NVARCHAR(30) NOT NULL,
        [ClientName] NVARCHAR(100) NULL,
        [StartDate] DATETIME2 NOT NULL,
        [EndDate] DATETIME2 NULL,
        [Budget] DECIMAL(18, 2) NOT NULL CONSTRAINT [DF_Projects_Budget] DEFAULT ((0.00)),
        [Status] NVARCHAR(30) NOT NULL,
        [DepartmentId] INT NOT NULL,
        [CreatedDate] DATETIME2 NOT NULL CONSTRAINT [DF_Projects_CreatedDate] DEFAULT (GETUTCDATE()),
        CONSTRAINT [PK_Projects] PRIMARY KEY CLUSTERED ([Id] ASC),
        CONSTRAINT [FK_Projects_Departments] FOREIGN KEY ([DepartmentId]) REFERENCES [dbo].[Departments] ([Id])
    );

    CREATE UNIQUE NONCLUSTERED INDEX [IX_Project_ProjectCode] ON [dbo].[Projects]([ProjectCode] ASC);
END
GO

-- 5. Create Documents Table (Stores file paths on file system, not binary payload)
IF OBJECT_ID(N'[dbo].[Documents]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Documents] (
        [Id] INT IDENTITY(1,1) NOT NULL,
        [Title] NVARCHAR(200) NOT NULL,
        [DocumentType] NVARCHAR(50) NULL,
        [FilePath] NVARCHAR(500) NOT NULL,
        [FileName] NVARCHAR(255) NULL,
        [ContentType] NVARCHAR(100) NULL,
        [FileSizeByte] BIGINT NOT NULL CONSTRAINT [DF_Documents_FileSizeByte] DEFAULT ((0)),
        [EmployeeId] INT NULL,
        [ProjectId] INT NULL,
        [UploadedBy] NVARCHAR(100) NULL,
        [UploadedDate] DATETIME2 NOT NULL CONSTRAINT [DF_Documents_UploadedDate] DEFAULT (GETUTCDATE()),
        CONSTRAINT [PK_Documents] PRIMARY KEY CLUSTERED ([Id] ASC),
        CONSTRAINT [FK_Documents_Employees] FOREIGN KEY ([EmployeeId]) REFERENCES [dbo].[Employees] ([Id]),
        CONSTRAINT [FK_Documents_Projects] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Projects] ([Id])
    );
END
GO

-- 6. Create AuditLogs Table
IF OBJECT_ID(N'[dbo].[AuditLogs]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[AuditLogs] (
        [Id] INT IDENTITY(1,1) NOT NULL,
        [Action] NVARCHAR(100) NOT NULL,
        [EntityName] NVARCHAR(100) NOT NULL,
        [EntityId] NVARCHAR(50) NULL,
        [Details] NVARCHAR(1000) NULL,
        [IpAddress] NVARCHAR(50) NULL,
        [UserId] INT NULL,
        [Timestamp] DATETIME2 NOT NULL CONSTRAINT [DF_AuditLogs_Timestamp] DEFAULT (GETUTCDATE()),
        CONSTRAINT [PK_AuditLogs] PRIMARY KEY CLUSTERED ([Id] ASC),
        CONSTRAINT [FK_AuditLogs_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
    );
END
GO

-- 7. Create Settings Table
IF OBJECT_ID(N'[dbo].[Settings]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Settings] (
        [Id] INT IDENTITY(1,1) NOT NULL,
        [Key] NVARCHAR(100) NOT NULL,
        [Value] NVARCHAR(MAX) NOT NULL,
        [Description] NVARCHAR(250) NULL,
        [Category] NVARCHAR(50) NULL,
        [UpdatedDate] DATETIME2 NOT NULL CONSTRAINT [DF_Settings_UpdatedDate] DEFAULT (GETUTCDATE()),
        [UpdatedBy] NVARCHAR(100) NULL,
        CONSTRAINT [PK_Settings] PRIMARY KEY CLUSTERED ([Id] ASC)
    );

    CREATE UNIQUE NONCLUSTERED INDEX [IX_Setting_Key] ON [dbo].[Settings]([Key] ASC);
END
GO

-- 8. Create FileMetadata Table
IF OBJECT_ID(N'[dbo].[FileMetadata]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[FileMetadata] (
        [Id] INT IDENTITY(1,1) NOT NULL,
        [OriginalFileName] NVARCHAR(255) NOT NULL,
        [StoredFileName] NVARCHAR(255) NOT NULL,
        [RelativePath] NVARCHAR(500) NOT NULL,
        [ContentType] NVARCHAR(100) NULL,
        [FileSizeByte] BIGINT NOT NULL,
        [FolderCategory] NVARCHAR(50) NOT NULL,
        [UploadedDate] DATETIME2 NOT NULL CONSTRAINT [DF_FileMetadata_UploadedDate] DEFAULT (GETUTCDATE()),
        [UploadedBy] NVARCHAR(100) NULL,
        CONSTRAINT [PK_FileMetadata] PRIMARY KEY CLUSTERED ([Id] ASC)
    );
END
GO

