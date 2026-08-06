/*
  WorkflowPro - EmployeeDB schema script
  ---------------------------------------
  Run this once against your existing EmployeeDB database.
  It is idempotent: every CREATE TABLE is guarded so the script can be
  re-run safely and will only create objects that are missing.

  IMPORTANT: This does NOT redesign your existing schema. If your
  EmployeeDB already has Employees/Departments tables with different
  column names, reconcile them with the EF mappings in
  Models/Employee.cs, Models/Department.cs before running the app -
  do not just run this script blindly over a differently-shaped table.
*/

USE EmployeeDB;
GO

-- ============================================================
-- Users (Authentication)
-- ============================================================
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Users')
BEGIN
    CREATE TABLE dbo.Users
    (
        UserId        INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        Username      NVARCHAR(50)      NOT NULL,
        PasswordHash  NVARCHAR(200)     NOT NULL,
        FullName      NVARCHAR(100)     NOT NULL,
        Role          NVARCHAR(20)      NOT NULL,  -- 'Admin' or 'User'
        IsActive      BIT               NOT NULL DEFAULT (1),
        CreatedDate   DATETIME          NOT NULL DEFAULT (GETDATE()),
        CONSTRAINT UQ_Users_Username UNIQUE (Username)
    );
END
GO

-- ============================================================
-- Departments
-- ============================================================
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Departments')
BEGIN
    CREATE TABLE dbo.Departments
    (
        DepartmentId    INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        DepartmentName  NVARCHAR(100)     NOT NULL,
        Description     NVARCHAR(250)     NULL,
        CreatedDate     DATETIME          NOT NULL DEFAULT (GETDATE()),
        CONSTRAINT UQ_Departments_Name UNIQUE (DepartmentName)
    );
END
GO

-- ============================================================
-- Employees
-- ============================================================
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Employees')
BEGIN
    CREATE TABLE dbo.Employees
    (
        EmployeeId        INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        EmployeeCode      NVARCHAR(20)      NOT NULL,
        FirstName         NVARCHAR(50)      NOT NULL,
        LastName          NVARCHAR(50)      NOT NULL,
        Email             NVARCHAR(100)     NOT NULL,
        Phone             NVARCHAR(20)      NULL,
        DepartmentId      INT               NOT NULL,
        Designation       NVARCHAR(100)     NULL,
        Salary            DECIMAL(10,2)     NOT NULL DEFAULT (0),
        ProfilePhotoPath  NVARCHAR(260)     NULL,
        CreatedDate       DATETIME          NOT NULL DEFAULT (GETDATE()),
        CONSTRAINT UQ_Employees_Code UNIQUE (EmployeeCode),
        CONSTRAINT FK_Employees_Departments FOREIGN KEY (DepartmentId)
            REFERENCES dbo.Departments (DepartmentId)
    );
END
GO

-- If Employees already existed without the photo column, add it (per the
-- "only add missing columns if absolutely necessary" instruction).
IF EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Employees')
   AND NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Employees') AND name = 'ProfilePhotoPath')
BEGIN
    ALTER TABLE dbo.Employees ADD ProfilePhotoPath NVARCHAR(260) NULL;
END
GO

-- ============================================================
-- Documents (metadata only - actual files live under
-- ~/Uploads/Documents on the web server's file system)
-- ============================================================
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Documents')
BEGIN
    CREATE TABLE dbo.Documents
    (
        DocumentId       INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        Title            NVARCHAR(150)     NOT NULL,
        FileName         NVARCHAR(255)     NOT NULL,
        StoredFileName   NVARCHAR(255)     NOT NULL,
        ContentType      NVARCHAR(100)     NULL,
        FileSizeKB       BIGINT            NOT NULL DEFAULT (0),
        UploadedBy       NVARCHAR(50)      NULL,
        UploadedDate     DATETIME          NOT NULL DEFAULT (GETDATE())
    );
END
GO

-- ============================================================
-- Sample departments (safe to skip/edit - only inserted if the
-- table is empty, so this never overwrites your existing data)
-- ============================================================
IF NOT EXISTS (SELECT 1 FROM dbo.Departments)
BEGIN
    INSERT INTO dbo.Departments (DepartmentName, Description) VALUES
        (N'Human Resources', N'Employee relations, hiring and policy'),
        (N'Information Technology', N'Infrastructure, applications and support'),
        (N'Finance', N'Accounts, payroll and budgeting'),
        (N'Operations', N'Day-to-day business operations');
END
GO

/*
  NOTE on the admin login:
  The application seeds a default admin account itself the first time
  it starts against an empty Users table (username: admin,
  password: Admin@123 - see Infrastructure/DbSeeder.cs), because
  generating a BCrypt hash requires the BCrypt.Net-Next library rather
  than plain T-SQL. You do not need to insert a Users row manually.
  Change the password immediately after first login by updating the
  PasswordHash column with a hash generated via PasswordHasher.Hash(...).
*/
