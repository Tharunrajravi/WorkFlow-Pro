/* ================================================================
   WorkflowPro - EmployeeDB
   Phase 2 - Schema creation script
   Target: SQL Server 2019 Express
   ================================================================ */

IF DB_ID('EmployeeDB') IS NULL
BEGIN
    CREATE DATABASE EmployeeDB;
END
GO

USE EmployeeDB;
GO

/* ---------------------------------------------------------------
   Departments
   --------------------------------------------------------------- */
IF OBJECT_ID('dbo.Departments', 'U') IS NOT NULL DROP TABLE dbo.Departments;
GO
CREATE TABLE dbo.Departments
(
    DepartmentId    INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Name            NVARCHAR(150)     NOT NULL,
    Code            NVARCHAR(20)      NOT NULL,
    Description     NVARCHAR(500)     NULL,
    IsActive        BIT               NOT NULL DEFAULT(1),
    CreatedOn       DATETIME          NOT NULL DEFAULT(GETDATE()),
    ModifiedOn      DATETIME          NULL,
    CONSTRAINT UQ_Departments_Code UNIQUE (Code)
);
GO

/* ---------------------------------------------------------------
   Users  (login / Forms Authentication)
   --------------------------------------------------------------- */
IF OBJECT_ID('dbo.Users', 'U') IS NOT NULL DROP TABLE dbo.Users;
GO
CREATE TABLE dbo.Users
(
    UserId          INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Username        NVARCHAR(100)     NOT NULL,
    Email           NVARCHAR(150)     NOT NULL,
    PasswordHash    NVARCHAR(256)     NOT NULL,
    PasswordSalt    NVARCHAR(256)     NOT NULL,
    Role            NVARCHAR(50)      NOT NULL DEFAULT('Employee'), -- Admin, HR, Manager, Employee
    EmployeeId      INT               NULL,   -- FK added after Employees table exists
    IsActive        BIT               NOT NULL DEFAULT(1),
    IsLocked        BIT               NOT NULL DEFAULT(0),
    FailedLoginCount INT              NOT NULL DEFAULT(0),
    LastLoginOn     DATETIME          NULL,
    CreatedOn       DATETIME          NOT NULL DEFAULT(GETDATE()),
    ModifiedOn      DATETIME          NULL,
    CONSTRAINT UQ_Users_Username UNIQUE (Username),
    CONSTRAINT UQ_Users_Email UNIQUE (Email)
);
GO

/* ---------------------------------------------------------------
   Employees
   --------------------------------------------------------------- */
IF OBJECT_ID('dbo.Employees', 'U') IS NOT NULL DROP TABLE dbo.Employees;
GO
CREATE TABLE dbo.Employees
(
    EmployeeId      INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    EmployeeCode    NVARCHAR(20)      NOT NULL,
    FirstName       NVARCHAR(100)     NOT NULL,
    LastName        NVARCHAR(100)     NOT NULL,
    Email           NVARCHAR(150)     NOT NULL,
    Phone           NVARCHAR(20)      NULL,
    DepartmentId    INT               NOT NULL,
    Designation     NVARCHAR(100)     NULL,
    DateOfJoining   DATE              NOT NULL,
    DateOfBirth     DATE              NULL,
    Gender          NVARCHAR(10)      NULL,
    Address         NVARCHAR(300)     NULL,
    ReportingManagerId INT            NULL,
    IsActive        BIT               NOT NULL DEFAULT(1),
    CreatedOn       DATETIME          NOT NULL DEFAULT(GETDATE()),
    ModifiedOn      DATETIME          NULL,
    CONSTRAINT UQ_Employees_Code UNIQUE (EmployeeCode),
    CONSTRAINT UQ_Employees_Email UNIQUE (Email),
    CONSTRAINT FK_Employees_Departments FOREIGN KEY (DepartmentId)
        REFERENCES dbo.Departments(DepartmentId),
    CONSTRAINT FK_Employees_Manager FOREIGN KEY (ReportingManagerId)
        REFERENCES dbo.Employees(EmployeeId)
);
GO

ALTER TABLE dbo.Users
    ADD CONSTRAINT FK_Users_Employees FOREIGN KEY (EmployeeId)
        REFERENCES dbo.Employees(EmployeeId);
GO

/* ---------------------------------------------------------------
   Projects
   --------------------------------------------------------------- */
IF OBJECT_ID('dbo.Projects', 'U') IS NOT NULL DROP TABLE dbo.Projects;
GO
CREATE TABLE dbo.Projects
(
    ProjectId       INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    ProjectCode     NVARCHAR(20)      NOT NULL,
    Name            NVARCHAR(200)     NOT NULL,
    Description     NVARCHAR(1000)    NULL,
    DepartmentId    INT               NOT NULL,
    ProjectManagerId INT              NULL,
    StartDate       DATE              NOT NULL,
    EndDate         DATE              NULL,
    Status          NVARCHAR(30)      NOT NULL DEFAULT('Planned'), -- Planned, InProgress, OnHold, Completed, Cancelled
    Priority        NVARCHAR(20)      NOT NULL DEFAULT('Medium'),  -- Low, Medium, High, Critical
    IsActive        BIT               NOT NULL DEFAULT(1),
    CreatedOn       DATETIME          NOT NULL DEFAULT(GETDATE()),
    ModifiedOn      DATETIME          NULL,
    CONSTRAINT UQ_Projects_Code UNIQUE (ProjectCode),
    CONSTRAINT FK_Projects_Departments FOREIGN KEY (DepartmentId)
        REFERENCES dbo.Departments(DepartmentId),
    CONSTRAINT FK_Projects_Manager FOREIGN KEY (ProjectManagerId)
        REFERENCES dbo.Employees(EmployeeId)
);
GO

/* ---------------------------------------------------------------
   ProjectAssignments (Employees <-> Projects, many-to-many)
   --------------------------------------------------------------- */
IF OBJECT_ID('dbo.ProjectAssignments', 'U') IS NOT NULL DROP TABLE dbo.ProjectAssignments;
GO
CREATE TABLE dbo.ProjectAssignments
(
    ProjectAssignmentId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    ProjectId       INT               NOT NULL,
    EmployeeId      INT               NOT NULL,
    RoleOnProject   NVARCHAR(100)     NULL,
    AssignedOn      DATETIME          NOT NULL DEFAULT(GETDATE()),
    RemovedOn       DATETIME          NULL,
    CONSTRAINT FK_ProjectAssignments_Projects FOREIGN KEY (ProjectId)
        REFERENCES dbo.Projects(ProjectId),
    CONSTRAINT FK_ProjectAssignments_Employees FOREIGN KEY (EmployeeId)
        REFERENCES dbo.Employees(EmployeeId),
    CONSTRAINT UQ_ProjectAssignments UNIQUE (ProjectId, EmployeeId)
);
GO

/* ---------------------------------------------------------------
   Documents
   --------------------------------------------------------------- */
IF OBJECT_ID('dbo.Documents', 'U') IS NOT NULL DROP TABLE dbo.Documents;
GO
CREATE TABLE dbo.Documents
(
    DocumentId      INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Title           NVARCHAR(200)     NOT NULL,
    FileName        NVARCHAR(260)     NOT NULL,
    FilePath        NVARCHAR(500)     NOT NULL,
    FileType        NVARCHAR(20)      NULL,
    FileSizeKB      INT               NULL,
    EmployeeId      INT               NULL,
    ProjectId       INT               NULL,
    DepartmentId    INT               NULL,
    UploadedByUserId INT              NOT NULL,
    UploadedOn      DATETIME          NOT NULL DEFAULT(GETDATE()),
    IsActive        BIT               NOT NULL DEFAULT(1),
    CONSTRAINT FK_Documents_Employees FOREIGN KEY (EmployeeId)
        REFERENCES dbo.Employees(EmployeeId),
    CONSTRAINT FK_Documents_Projects FOREIGN KEY (ProjectId)
        REFERENCES dbo.Projects(ProjectId),
    CONSTRAINT FK_Documents_Departments FOREIGN KEY (DepartmentId)
        REFERENCES dbo.Departments(DepartmentId),
    CONSTRAINT FK_Documents_UploadedBy FOREIGN KEY (UploadedByUserId)
        REFERENCES dbo.Users(UserId)
);
GO

/* ---------------------------------------------------------------
   AuditLogs
   --------------------------------------------------------------- */
IF OBJECT_ID('dbo.AuditLogs', 'U') IS NOT NULL DROP TABLE dbo.AuditLogs;
GO
CREATE TABLE dbo.AuditLogs
(
    AuditLogId      BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    UserId          INT               NULL,
    Username        NVARCHAR(100)     NULL,
    Action          NVARCHAR(100)     NOT NULL,   -- Create, Update, Delete, Login, Logout, Export...
    EntityName      NVARCHAR(100)     NULL,       -- Employee, Department, Project, Document...
    EntityId        NVARCHAR(50)      NULL,
    Details         NVARCHAR(MAX)     NULL,
    IPAddress       NVARCHAR(50)      NULL,
    CreatedOn       DATETIME          NOT NULL DEFAULT(GETDATE()),
    CONSTRAINT FK_AuditLogs_Users FOREIGN KEY (UserId)
        REFERENCES dbo.Users(UserId)
);
GO

CREATE INDEX IX_Employees_DepartmentId ON dbo.Employees(DepartmentId);
CREATE INDEX IX_Projects_DepartmentId ON dbo.Projects(DepartmentId);
CREATE INDEX IX_Documents_EmployeeId ON dbo.Documents(EmployeeId);
CREATE INDEX IX_Documents_ProjectId ON dbo.Documents(ProjectId);
CREATE INDEX IX_AuditLogs_UserId ON dbo.AuditLogs(UserId);
CREATE INDEX IX_AuditLogs_CreatedOn ON dbo.AuditLogs(CreatedOn);
GO

/* One user account per employee (nulls allowed/ignored) */
CREATE UNIQUE INDEX UQ_Users_EmployeeId ON dbo.Users(EmployeeId)
    WHERE EmployeeId IS NOT NULL;
GO
