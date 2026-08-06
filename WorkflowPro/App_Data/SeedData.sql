-- ===============================================================================
-- Workflow Pro - Complete Seed Data Script (Phase 2)
-- Generated for Database: EmployeeDB
-- Includes:
--   - 10 Departments
--   - 100 Employees (EMP001 - EMP100)
--   - 20 Projects (PRJ-001 - PRJ-020)
--   - 5 Users
--   - 50 Documents
--   - 200 Audit Logs
--   - Settings Records
-- ===============================================================================

USE [EmployeeDB];
GO

SET NOCOUNT ON;
BEGIN TRANSACTION;

---------------------------------------------------------------------------------
-- 1. SEED 10 DEPARTMENTS
---------------------------------------------------------------------------------
PRINT 'Seeding 10 Departments...';

MERGE INTO [dbo].[Departments] WITH (HOLDLOCK) AS target
USING (VALUES
    (1, N'Engineering', N'ENG', N'Software Engineering and Infrastructure Development', 1, GETUTCDATE()),
    (2, N'Human Resources', N'HR', N'Talent Acquisition, Employee Relations & HR Ops', 1, GETUTCDATE()),
    (3, N'Finance & Accounting', N'FIN', N'Financial Management, Accounting and Audits', 1, GETUTCDATE()),
    (4, N'Marketing', N'MKT', N'Digital Marketing, Branding and Communications', 1, GETUTCDATE()),
    (5, N'Sales & Business Dev', N'SALES', N'Enterprise Sales and Business Development', 1, GETUTCDATE()),
    (6, N'Information Technology', N'IT', N'IT Support, Network Operations and Systems', 1, GETUTCDATE()),
    (7, N'Operations & Logistics', N'OPS', N'Internal Operations and Logistics Management', 1, GETUTCDATE()),
    (8, N'Product Management', N'PROD', N'Product Strategy, UX/UI and Roadmap Execution', 1, GETUTCDATE()),
    (9, N'Legal & Compliance', N'LEGAL', N'Corporate Governance and Regulatory Compliance', 1, GETUTCDATE()),
    (10, N'Customer Support', N'CS', N'Client Support, Success & Account Services', 1, GETUTCDATE())
) AS source ([Id], [DepartmentName], [Code], [Description], [IsActive], [CreatedDate])
ON target.[Code] = source.[Code]
WHEN NOT MATCHED THEN
    INSERT ([DepartmentName], [Code], [Description], [IsActive], [CreatedDate])
    VALUES (source.[DepartmentName], source.[Code], source.[Description], source.[IsActive], source.[CreatedDate]);

---------------------------------------------------------------------------------
-- 2. SEED 100 EMPLOYEES
---------------------------------------------------------------------------------
PRINT 'Seeding 100 Employees...';

IF NOT EXISTS (SELECT 1 FROM [dbo].[Employees] WHERE [EmployeeCode] = 'EMP001')
BEGIN
    INSERT INTO [dbo].[Employees] ([EmployeeCode], [FirstName], [LastName], [Email], [Phone], [Designation], [DepartmentId], [HireDate], [Salary], [IsActive], [ProfileImagePath], [CreatedDate])
    VALUES
    (N'EMP001', N'Alexander', N'Wright', N'alexander.wright@WorkflowPro.com', N'+1-555-0101', N'Chief Technology Officer', 1, '2018-03-15', 165000.00, 1, N'~/Images/Employees/emp001.jpg', GETUTCDATE()),
    (N'EMP002', N'Eleanor', N'Vance', N'eleanor.vance@WorkflowPro.com', N'+1-555-0102', N'HR Vice President', 2, '2019-01-10', 145000.00, 1, N'~/Images/Employees/emp002.jpg', GETUTCDATE()),
    (N'EMP003', N'Marcus', N'Sterling', N'marcus.sterling@WorkflowPro.com', N'+1-555-0103', N'Finance Director', 3, '2019-05-20', 150000.00, 1, N'~/Images/Employees/emp003.jpg', GETUTCDATE()),
    (N'EMP004', N'Sophia', N'Chen', N'sophia.chen@WorkflowPro.com', N'+1-555-0104', N'Principal Software Architect', 1, '2020-02-01', 140000.00, 1, N'~/Images/Employees/emp004.jpg', GETUTCDATE()),
    (N'EMP005', N'David', N'Miller', N'david.miller@WorkflowPro.com', N'+1-555-0105', N'IT Support Operations Lead', 6, '2020-06-15', 95000.00, 1, N'~/Images/Employees/emp005.jpg', GETUTCDATE()),
    (N'EMP006', N'Olivia', N'Taylor', N'olivia.taylor@WorkflowPro.com', N'+1-555-0106', N'Marketing Director', 4, '2020-08-10', 135000.00, 1, N'~/Images/Employees/emp006.jpg', GETUTCDATE()),
    (N'EMP007', N'James', N'Wilson', N'james.wilson@WorkflowPro.com', N'+1-555-0107', N'VP of Sales', 5, '2019-11-01', 155000.00, 1, N'~/Images/Employees/emp007.jpg', GETUTCDATE()),
    (N'EMP008', N'Emma', N'Davis', N'emma.davis@WorkflowPro.com', N'+1-555-0108', N'Operations Director', 7, '2020-10-05', 130000.00, 1, N'~/Images/Employees/emp008.jpg', GETUTCDATE()),
    (N'EMP009', N'Benjamin', N'Martinez', N'benjamin.martinez@WorkflowPro.com', N'+1-555-0109', N'VP of Product Management', 8, '2021-01-15', 148000.00, 1, N'~/Images/Employees/emp009.jpg', GETUTCDATE()),
    (N'EMP10', N'Charlotte', N'Anderson', N'charlotte.anderson@WorkflowPro.com', N'+1-555-0110', N'General Counsel', 9, '2018-09-01', 160000.00, 1, N'~/Images/Employees/emp010.jpg', GETUTCDATE()),

    -- EMP011 - EMP020 (Engineering & Tech Staff)
    (N'EMP011', N'Daniel', N'Thomas', N'daniel.thomas@WorkflowPro.com', N'+1-555-0111', N'Senior Full Stack Developer', 1, '2021-03-01', 115000.00, 1, N'~/Images/Employees/emp011.jpg', GETUTCDATE()),
    (N'EMP012', N'Sophia', N'Jackson', N'sophia.jackson@WorkflowPro.com', N'+1-555-0112', N'Senior Backend Engineer', 1, '2021-04-12', 120000.00, 1, N'~/Images/Employees/emp012.jpg', GETUTCDATE()),
    (N'EMP013', N'Matthew', N'White', N'matthew.white@WorkflowPro.com', N'+1-555-0113', N'DevOps Engineer', 1, '2021-06-01', 110000.00, 1, N'~/Images/Employees/emp013.jpg', GETUTCDATE()),
    (N'EMP014', N'Amelia', N'Harris', N'amelia.harris@WorkflowPro.com', N'+1-555-0114', N'Frontend Developer', 1, '2021-07-15', 98000.00, 1, N'~/Images/Employees/emp014.jpg', GETUTCDATE()),
    (N'EMP015', N'Lucas', N'Martin', N'lucas.martin@WorkflowPro.com', N'+1-555-0115', N'Database Administrator', 1, '2021-08-20', 105000.00, 1, N'~/Images/Employees/emp015.jpg', GETUTCDATE()),
    (N'EMP016', N'Harper', N'Thompson', N'harper.thompson@WorkflowPro.com', N'+1-555-0116', N'QA Automation Engineer', 1, '2021-09-10', 92000.00, 1, N'~/Images/Employees/emp016.jpg', GETUTCDATE()),
    (N'EMP017', N'Henry', N'Garcia', N'henry.garcia@WorkflowPro.com', N'+1-555-0117', N'Software Engineer', 1, '2022-01-10', 90000.00, 1, N'~/Images/Employees/emp017.jpg', GETUTCDATE()),
    (N'EMP018', N'Evelyn', N'Martinez', N'evelyn.martinez@WorkflowPro.com', N'+1-555-0118', N'Cloud Security Specialist', 1, '2022-02-15', 125000.00, 1, N'~/Images/Employees/emp018.jpg', GETUTCDATE()),
    (N'EMP019', N'Sebastian', N'Robinson', N'sebastian.robinson@WorkflowPro.com', N'+1-555-0119', N'Systems Developer', 1, '2022-03-01', 88000.00, 1, N'~/Images/Employees/emp019.jpg', GETUTCDATE()),
    (N'EMP020', N'Abigail', N'Clark', N'abigail.clark@WorkflowPro.com', N'+1-555-0120', N'Mobile Engineer', 1, '2022-04-10', 96000.00, 1, N'~/Images/Employees/emp020.jpg', GETUTCDATE()),

    -- EMP021 - EMP030 (HR & Admin Staff)
    (N'EMP021', N'Logan', N'Rodriguez', N'logan.rodriguez@WorkflowPro.com', N'+1-555-0121', N'HR Manager', 2, '2021-02-01', 85000.00, 1, N'~/Images/Employees/emp021.jpg', GETUTCDATE()),
    (N'EMP022', N'Emily', N'Lewis', N'emily.lewis@WorkflowPro.com', N'+1-555-0122', N'Senior Talent Acquisition Lead', 2, '2021-05-15', 82000.00, 1, N'~/Images/Employees/emp022.jpg', GETUTCDATE()),
    (N'EMP023', N'Jackson', N'Lee', N'jackson.lee@WorkflowPro.com', N'+1-555-0123', N'Compensation & Benefits Specialist', 2, '2021-08-01', 78000.00, 1, N'~/Images/Employees/emp023.jpg', GETUTCDATE()),
    (N'EMP024', N'Ella', N'Walker', N'ella.walker@WorkflowPro.com', N'+1-555-0124', N'HR Generalist', 2, '2022-01-20', 68000.00, 1, N'~/Images/Employees/emp024.jpg', GETUTCDATE()),
    (N'EMP025', N'Aiden', N'Hall', N'aiden.hall@WorkflowPro.com', N'+1-555-0125', N'Recruiter', 2, '2022-03-15', 62000.00, 1, N'~/Images/Employees/emp025.jpg', GETUTCDATE()),
    (N'EMP026', N'Scarlett', N'Allen', N'scarlett.allen@WorkflowPro.com', N'+1-555-0126', N'HR Operations Coordinator', 2, '2022-06-01', 58000.00, 1, N'~/Images/Employees/emp026.jpg', GETUTCDATE()),
    (N'EMP027', N'Matthew', N'Young', N'matthew.young@WorkflowPro.com', N'+1-555-0127', N'Training & Development Manager', 2, '2022-08-10', 80000.00, 1, N'~/Images/Employees/emp027.jpg', GETUTCDATE()),
    (N'EMP028', N'Grace', N'Hernandez', N'grace.hernandez@WorkflowPro.com', N'+1-555-0128', N'HR Assistant', 2, '2023-01-10', 48000.00, 1, N'~/Images/Employees/emp028.jpg', GETUTCDATE()),
    (N'EMP029', N'Samuel', N'King', N'samuel.king@WorkflowPro.com', N'+1-555-0129', N'Employee Relations Specialist', 2, '2023-02-15', 72000.00, 1, N'~/Images/Employees/emp029.jpg', GETUTCDATE()),
    (N'EMP030', N'Chloe', N'Wright', N'chloe.wright@WorkflowPro.com', N'+1-555-0130', N'People Analytics Lead', 2, '2023-04-01', 90000.00, 1, N'~/Images/Employees/emp030.jpg', GETUTCDATE()),

    -- EMP031 - EMP040 (Finance Staff)
    (N'EMP031', N'Joseph', N'Lopez', N'joseph.lopez@WorkflowPro.com', N'+1-555-0131', N'Senior Financial Analyst', 3, '2020-04-01', 95000.00, 1, N'~/Images/Employees/emp031.jpg', GETUTCDATE()),
    (N'EMP032', N'Victoria', N'Hill', N'victoria.hill@WorkflowPro.com', N'+1-555-0132', N'Accounting Manager', 3, '2020-07-15', 102000.00, 1, N'~/Images/Employees/emp032.jpg', GETUTCDATE()),
    (N'EMP033', N'Owen', N'Scott', N'owen.scott@WorkflowPro.com', N'+1-555-0133', N'Tax Specialist', 3, '2021-01-10', 88000.00, 1, N'~/Images/Employees/emp033.jpg', GETUTCDATE()),
    (N'EMP034', N'Riley', N'Green', N'riley.green@WorkflowPro.com', N'+1-555-0134', N'Payroll Supervisor', 3, '2021-05-01', 76000.00, 1, N'~/Images/Employees/emp034.jpg', GETUTCDATE()),
    (N'EMP035', N'Wyatt', N'Adams', N'wyatt.adams@WorkflowPro.com', N'+1-555-0135', N'Senior Auditor', 3, '2021-09-15', 90000.00, 1, N'~/Images/Employees/emp035.jpg', GETUTCDATE()),
    (N'EMP036', N'Aria', N'Baker', N'aria.baker@WorkflowPro.com', N'+1-555-0136', N'Accounts Payable Lead', 3, '2022-02-01', 65000.00, 1, N'~/Images/Employees/emp036.jpg', GETUTCDATE()),
    (N'EMP037', N'Carter', N'Gonzalez', N'carter.gonzalez@WorkflowPro.com', N'+1-555-0137', N'Accounts Receivable Lead', 3, '2022-04-15', 65000.00, 1, N'~/Images/Employees/emp037.jpg', GETUTCDATE()),
    (N'EMP038', N'Zoey', N'Nelson', N'zoey.nelson@WorkflowPro.com', N'+1-555-0138', N'Budget Analyst', 3, '2022-07-01', 78000.00, 1, N'~/Images/Employees/emp038.jpg', GETUTCDATE()),
    (N'EMP039', N'Luke', N'Carter', N'luke.carter@WorkflowPro.com', N'+1-555-0139', N'Staff Accountant', 3, '2023-01-15', 60000.00, 1, N'~/Images/Employees/emp039.jpg', GETUTCDATE()),
    (N'EMP040', N'Penelope', N'Mitchell', N'penelope.mitchell@WorkflowPro.com', N'+1-555-0140', N'Financial Controller', 3, '2019-08-01', 125000.00, 1, N'~/Images/Employees/emp040.jpg', GETUTCDATE()),

    -- EMP041 - EMP050 (Marketing Staff)
    (N'EMP041', N'Gabriel', N'Perez', N'gabriel.perez@WorkflowPro.com', N'+1-555-0141', N'Digital Marketing Lead', 4, '2021-02-15', 88000.00, 1, N'~/Images/Employees/emp041.jpg', GETUTCDATE()),
    (N'EMP042', N'Layla', N'Roberts', N'layla.roberts@WorkflowPro.com', N'+1-555-0142', N'Content Strategy Manager', 4, '2021-06-01', 82000.00, 1, N'~/Images/Employees/emp042.jpg', GETUTCDATE()),
    (N'EMP043', N'Anthony', N'Turner', N'anthony.turner@WorkflowPro.com', N'+1-555-0143', N'SEO Specialist', 4, '2021-10-15', 70000.00, 1, N'~/Images/Employees/emp043.jpg', GETUTCDATE()),
    (N'EMP044', N'Lillian', N'Phillips', N'lillian.phillips@WorkflowPro.com', N'+1-555-0144', N'Brand Specialist', 4, '2022-01-10', 72000.00, 1, N'~/Images/Employees/emp044.jpg', GETUTCDATE()),
    (N'EMP045', N'Dylan', N'Campbell', N'dylan.campbell@WorkflowPro.com', N'+1-555-0145', N'Social Media Manager', 4, '2022-03-01', 65000.00, 1, N'~/Images/Employees/emp045.jpg', GETUTCDATE()),
    (N'EMP046', N'Nora', N'Parker', N'nora.parker@WorkflowPro.com', N'+1-555-0146', N'Graphic Designer', 4, '2022-05-15', 64000.00, 1, N'~/Images/Employees/emp046.jpg', GETUTCDATE()),
    (N'EMP047', N'Leo', N'Evans', N'leo.evans@WorkflowPro.com', N'+1-555-0147', N'PR & Communications Manager', 4, '2022-09-01', 86000.00, 1, N'~/Images/Employees/emp047.jpg', GETUTCDATE()),
    (N'EMP048', N'Mila', N'Edwards', N'mila.edwards@WorkflowPro.com', N'+1-555-0148', N'Event Marketing Lead', 4, '2023-01-10', 75000.00, 1, N'~/Images/Employees/emp048.jpg', GETUTCDATE()),
    (N'EMP049', N'Julian', N'Collins', N'julian.collins@WorkflowPro.com', N'+1-555-0149', N'Marketing Analyst', 4, '2023-03-15', 68000.00, 1, N'~/Images/Employees/emp049.jpg', GETUTCDATE()),
    (N'EMP050', N'Hannah', N'Stewart', N'hannah.stewart@WorkflowPro.com', N'+1-555-0150', N'Demand Generation Specialist', 4, '2023-06-01', 78000.00, 1, N'~/Images/Employees/emp050.jpg', GETUTCDATE()),

    -- EMP051 - EMP060 (Sales Staff)
    (N'EMP051', N'Christopher', N'Sanchez', N'christopher.sanchez@WorkflowPro.com', N'+1-555-0151', N'Enterprise Sales Director', 5, '2020-02-01', 135000.00, 1, N'~/Images/Employees/emp051.jpg', GETUTCDATE()),
    (N'EMP052', N'Addison', N'Morris', N'addison.morris@WorkflowPro.com', N'+1-555-0152', N'Senior Account Executive', 5, '2020-05-15', 110000.00, 1, N'~/Images/Employees/emp052.jpg', GETUTCDATE()),
    (N'EMP053', N'Jaxon', N'Rogers', N'jaxon.rogers@WorkflowPro.com', N'+1-555-0153', N'Account Executive', 5, '2021-01-10', 95000.00, 1, N'~/Images/Employees/emp053.jpg', GETUTCDATE()),
    (N'EMP054', N'Stella', N'Reed', N'stella.reed@WorkflowPro.com', N'+1-555-0154', N'Sales Development Lead', 5, '2021-04-01', 80000.00, 1, N'~/Images/Employees/emp054.jpg', GETUTCDATE()),
    (N'EMP055', N'Asher', N'Cook', N'asher.cook@WorkflowPro.com', N'+1-555-0155', N'Business Development Manager', 5, '2021-08-15', 105000.00, 1, N'~/Images/Employees/emp055.jpg', GETUTCDATE()),
    (N'EMP056', N'Natalie', N'Morgan', N'natalie.morgan@WorkflowPro.com', N'+1-555-0156', N'Regional Sales Manager', 5, '2022-02-01', 115000.00, 1, N'~/Images/Employees/emp056.jpg', GETUTCDATE()),
    (N'EMP057', N'Thomas', N'Bell', N'thomas.bell@WorkflowPro.com', N'+1-555-0157', N'Sales Operations Manager', 5, '2022-05-10', 92000.00, 1, N'~/Images/Employees/emp057.jpg', GETUTCDATE()),
    (N'EMP058', N'Zoe', N'Murphy', N'zoe.murphy@WorkflowPro.com', N'+1-555-0158', N'Solution Engineer', 5, '2022-09-01', 102000.00, 1, N'~/Images/Employees/emp058.jpg', GETUTCDATE()),
    (N'EMP059', N'Ezra', N'Bailey', N'ezra.bailey@WorkflowPro.com', N'+1-555-0159', N'Inside Sales Rep', 5, '2023-02-01', 65000.00, 1, N'~/Images/Employees/emp059.jpg', GETUTCDATE()),
    (N'EMP060', N'Audrey', N'Rivera', N'audrey.rivera@WorkflowPro.com', N'+1-555-0160', N'Partner Alliance Manager', 5, '2023-05-15', 100000.00, 1, N'~/Images/Employees/emp060.jpg', GETUTCDATE()),

    -- EMP061 - EMP070 (IT Support Staff)
    (N'EMP061', N'Hudson', N'Cooper', N'hudson.cooper@WorkflowPro.com', N'+1-555-0161', N'Senior Systems Administrator', 6, '2020-03-15', 92000.00, 1, N'~/Images/Employees/emp061.jpg', GETUTCDATE()),
    (N'EMP062', N'Brooklyn', N'Richardson', N'brooklyn.richardson@WorkflowPro.com', N'+1-555-0162', N'Network Infrastructure Engineer', 6, '2020-08-01', 98000.00, 1, N'~/Images/Employees/emp062.jpg', GETUTCDATE()),
    (N'EMP063', N'Nolan', N'Cox', N'nolan.cox@WorkflowPro.com', N'+1-555-0163', N'IT Helpdesk Supervisor', 6, '2021-02-10', 75000.00, 1, N'~/Images/Employees/emp063.jpg', GETUTCDATE()),
    (N'EMP064', N'Claire', N'Howard', N'claire.howard@WorkflowPro.com', N'+1-555-0164', N'IT Security Analyst', 6, '2021-06-15', 94000.00, 1, N'~/Images/Employees/emp064.jpg', GETUTCDATE()),
    (N'EMP065', N'Easton', N'Ward', N'easton.ward@WorkflowPro.com', N'+1-555-0165', N'Desktop Support Specialist', 6, '2021-11-01', 60000.00, 1, N'~/Images/Employees/emp065.jpg', GETUTCDATE()),
    (N'EMP066', N'Skylar', N'Torres', N'skylar.torres@WorkflowPro.com', N'+1-555-0166', N'IT Support Engineer', 6, '2022-03-15', 62000.00, 1, N'~/Images/Employees/emp066.jpg', GETUTCDATE()),
    (N'EMP067', N'Colton', N'Peterson', N'colton.peterson@WorkflowPro.com', N'+1-555-0167', N'Systems Administrator', 6, '2022-07-01', 82000.00, 1, N'~/Images/Employees/emp067.jpg', GETUTCDATE()),
    (N'EMP068', N'Bella', N'Gray', N'bella.gray@WorkflowPro.com', N'+1-555-0168', N'Cloud Infrastructure Specialist', 6, '2022-10-15', 108000.00, 1, N'~/Images/Employees/emp068.jpg', GETUTCDATE()),
    (N'EMP069', N'Carson', N'Ramirez', N'carson.ramirez@WorkflowPro.com', N'+1-555-0169', N'IT Asset Manager', 6, '2023-01-10', 70000.00, 1, N'~/Images/Employees/emp069.jpg', GETUTCDATE()),
    (N'EMP070', N'Aaliyah', N'James', N'aaliyah.james@WorkflowPro.com', N'+1-555-0170', N'Service Desk Engineer', 6, '2023-04-01', 58000.00, 1, N'~/Images/Employees/emp070.jpg', GETUTCDATE()),

    -- EMP071 - EMP080 (Operations & Product Staff)
    (N'EMP071', N'Eli', N'Watson', N'eli.watson@WorkflowPro.com', N'+1-555-0171', N'Senior Operations Manager', 7, '2020-01-15', 105000.00, 1, N'~/Images/Employees/emp071.jpg', GETUTCDATE()),
    (N'EMP072', N'Savannah', N'Brooks', N'savannah.brooks@WorkflowPro.com', N'+1-555-0172', N'Supply Chain Specialist', 7, '2020-09-01', 78000.00, 1, N'~/Images/Employees/emp072.jpg', GETUTCDATE()),
    (N'EMP073', N'Aaron', N'Kelly', N'aaron.kelly@WorkflowPro.com', N'+1-555-0173', N'Facilities Manager', 7, '2021-03-10', 82000.00, 1, N'~/Images/Employees/emp073.jpg', GETUTCDATE()),
    (N'EMP074', N'Camila', N'Sanders', N'camila.sanders@WorkflowPro.com', N'+1-555-0174', N'Procurement Lead', 7, '2021-07-01', 85000.00, 1, N'~/Images/Employees/emp074.jpg', GETUTCDATE()),
    (N'EMP075', N'Landon', N'Price', N'landon.price@WorkflowPro.com', N'+1-555-0175', N'Operations Analyst', 7, '2022-01-15', 68000.00, 1, N'~/Images/Employees/emp075.jpg', GETUTCDATE()),
    (N'EMP076', N'Aria', N'Bennett', N'aria.bennett@WorkflowPro.com', N'+1-555-0176', N'Senior Product Manager', 8, '2020-04-10', 125000.00, 1, N'~/Images/Employees/emp076.jpg', GETUTCDATE()),
    (N'EMP077', N'Jonathan', N'Wood', N'jonathan.wood@WorkflowPro.com', N'+1-555-0177', N'Product Owner', 8, '2021-01-15', 108000.00, 1, N'~/Images/Employees/emp077.jpg', GETUTCDATE()),
    (N'EMP078', N'Ellie', N'Barnes', N'ellie.barnes@WorkflowPro.com', N'+1-555-0178', N'UX/UI Lead Designer', 8, '2021-05-01', 102000.00, 1, N'~/Images/Employees/emp078.jpg', GETUTCDATE()),
    (N'EMP079', N'Jeremiah', N'Ross', N'jeremiah.ross@WorkflowPro.com', N'+1-555-0179', N'Product Analyst', 8, '2022-02-15', 82000.00, 1, N'~/Images/Employees/emp079.jpg', GETUTCDATE()),
    (N'EMP080', N'Maya', N'Henderson', N'maya.henderson@WorkflowPro.com', N'+1-555-0180', N'UI Designer', 8, '2022-08-01', 78000.00, 1, N'~/Images/Employees/emp080.jpg', GETUTCDATE()),

    -- EMP081 - EMP090 (Legal & Customer Support Staff)
    (N'EMP081', N'Christian', N'Coleman', N'christian.coleman@WorkflowPro.com', N'+1-555-0181', N'Senior Legal Counsel', 9, '2019-10-15', 138000.00, 1, N'~/Images/Employees/emp081.jpg', GETUTCDATE()),
    (N'EMP082', N'Sarah', N'Jenkins', N'sarah.jenkins@WorkflowPro.com', N'+1-555-0182', N'Compliance Manager', 9, '2020-06-01', 110000.00, 1, N'~/Images/Employees/emp082.jpg', GETUTCDATE()),
    (N'EMP083', N'Hunter', N'Perry', N'hunter.perry@WorkflowPro.com', N'+1-555-0183', N'Paralegal Specialist', 9, '2021-04-15', 68000.00, 1, N'~/Images/Employees/emp083.jpg', GETUTCDATE()),
    (N'EMP084', N'Serenity', N'Powell', N'serenity.powell@WorkflowPro.com', N'+1-555-0184', N'Contract Manager', 9, '2022-01-10', 92000.00, 1, N'~/Images/Employees/emp084.jpg', GETUTCDATE()),
    (N'EMP085', N'Connor', N'Long', N'connor.long@WorkflowPro.com', N'+1-555-0185', N'Compliance Analyst', 9, '2022-07-15', 74000.00, 1, N'~/Images/Employees/emp085.jpg', GETUTCDATE()),
    (N'EMP086', N'Autumn', N'Patterson', N'autumn.patterson@WorkflowPro.com', N'+1-555-0186', N'Customer Support Manager', 10, '2020-03-01', 88000.00, 1, N'~/Images/Employees/emp086.jpg', GETUTCDATE()),
    (N'EMP087', N'Adrian', N'Hughes', N'adrian.hughes@WorkflowPro.com', N'+1-555-0187', N'Customer Success Lead', 10, '2020-11-15', 84000.00, 1, N'~/Images/Employees/emp087.jpg', GETUTCDATE()),
    (N'EMP088', N'Eva', N'Flores', N'eva.flores@WorkflowPro.com', N'+1-555-0188', N'Technical Support Specialist', 10, '2021-08-01', 62000.00, 1, N'~/Images/Employees/emp088.jpg', GETUTCDATE()),
    (N'EMP089', N'Jonathan', N'Washington', N'jonathan.washington@WorkflowPro.com', N'+1-555-0189', N'Client Services Rep', 10, '2022-03-10', 56000.00, 1, N'~/Images/Employees/emp089.jpg', GETUTCDATE()),
    (N'EMP090', N'Piper', N'Butler', N'piper.butler@WorkflowPro.com', N'+1-555-0190', N'Customer Success Specialist', 10, '2022-09-15', 65000.00, 1, N'~/Images/Employees/emp090.jpg', GETUTCDATE()),

    -- EMP091 - EMP100 (General Staff & Associates)
    (N'EMP091', N'Charles', N'Simmons', N'charles.simmons@WorkflowPro.com', N'+1-555-0191', N'Software Engineer', 1, '2023-01-15', 85000.00, 1, N'~/Images/Employees/emp091.jpg', GETUTCDATE()),
    (N'EMP092', N'Ruby', N'Foster', N'ruby.foster@WorkflowPro.com', N'+1-555-0192', N'QA Tester', 1, '2023-03-01', 72000.00, 1, N'~/Images/Employees/emp092.jpg', GETUTCDATE()),
    (N'EMP093', N'Thomas', N'Gonzales', N'thomas.gonzales@WorkflowPro.com', N'+1-555-0193', N'Financial Analyst', 3, '2023-04-15', 76000.00, 1, N'~/Images/Employees/emp093.jpg', GETUTCDATE()),
    (N'EMP094', N'Alice', N'Bryant', N'alice.bryant@WorkflowPro.com', N'+1-555-0194', N'Recruitment Associate', 2, '2023-06-01', 54000.00, 1, N'~/Images/Employees/emp094.jpg', GETUTCDATE()),
    (N'EMP095', N'David', N'Alexander', N'david.alexander@WorkflowPro.com', N'+1-555-0195', N'Account Executive', 5, '2023-07-15', 88000.00, 1, N'~/Images/Employees/emp095.jpg', GETUTCDATE()),
    (N'EMP096', N'Hailey', N'Russell', N'hailey.russell@WorkflowPro.com', N'+1-555-0196', N'Support Specialist', 10, '2023-08-20', 55000.00, 1, N'~/Images/Employees/emp096.jpg', GETUTCDATE()),
    (N'EMP097', N'Gavin', N'Griffin', N'gavin.griffin@WorkflowPro.com', N'+1-555-0197', N'DevOps Associate', 1, '2023-10-01', 82000.00, 1, N'~/Images/Employees/emp097.jpg', GETUTCDATE()),
    (N'EMP098', N'Kylie', N'Diaz', N'kylie.diaz@WorkflowPro.com', N'+1-555-0198', N'Marketing Coordinator', 4, '2023-11-15', 58000.00, 1, N'~/Images/Employees/emp098.jpg', GETUTCDATE()),
    (N'EMP099', N'Isaac', N'Hayes', N'isaac.hayes@WorkflowPro.com', N'+1-555-0199', N'IT Technician', 6, '2024-01-10', 56000.00, 1, N'~/Images/Employees/emp099.jpg', GETUTCDATE()),
    (N'EMP100', N'Reagan', N'Myers', N'reagan.myers@WorkflowPro.com', N'+1-555-0200', N'Operations Associate', 7, '2024-02-15', 55000.00, 1, N'~/Images/Employees/emp100.jpg', GETUTCDATE());
END
GO

---------------------------------------------------------------------------------
-- 3. SEED 20 PROJECTS
---------------------------------------------------------------------------------
PRINT 'Seeding 20 Projects...';

IF NOT EXISTS (SELECT 1 FROM [dbo].[Projects] WHERE [ProjectCode] = 'PRJ-001')
BEGIN
    INSERT INTO [dbo].[Projects] ([ProjectName], [ProjectCode], [ClientName], [StartDate], [EndDate], [Budget], [Status], [DepartmentId], [CreatedDate])
    VALUES
    (N'WorkflowPro Cloud Portal Phase 1', N'PRJ-001', N'Internal Operations', '2024-01-01', '2024-06-30', 250000.00, N'Completed', 1, GETUTCDATE()),
    (N'Enterprise ERP Integration', N'PRJ-002', N'Acme Corporation', '2024-02-15', '2024-11-30', 450000.00, N'In Progress', 1, GETUTCDATE()),
    (N'HR Operations Automation', N'PRJ-003', N'Global HR Solutions', '2024-03-01', '2024-09-15', 120000.00, N'In Progress', 2, GETUTCDATE()),
    (N'Financial Reporting System Modernization', N'PRJ-004', N'Starlight Financial', '2024-01-15', '2024-07-15', 300000.00, N'Completed', 3, GETUTCDATE()),
    (N'Brand Redesign & Digital Campaign', N'PRJ-005', N'WorkflowPro Marketing Group', '2024-04-01', '2024-10-01', 95000.00, N'In Progress', 4, GETUTCDATE()),
    (N'Global Sales CRM Rollout', N'PRJ-006', N'TechSphere Sales Inc', '2024-02-01', '2024-08-31', 280000.00, N'Completed', 5, GETUTCDATE()),
    (N'Zero Trust Network Security Overhaul', N'PRJ-007', N'Internal IT Infra', '2024-05-01', '2024-12-31', 350000.00, N'In Progress', 6, GETUTCDATE()),
    (N'Supply Chain Logistics Optimization', N'PRJ-008', N'Apex Logistics Corp', '2024-03-15', '2025-03-15', 600000.00, N'In Progress', 7, GETUTCDATE()),
    (N'NextGen Mobile App Suite', N'PRJ-009', N'Vanguard Mobility', '2024-06-01', '2025-01-31', 500000.00, N'In Progress', 8, GETUTCDATE()),
    (N'GDPR & Compliance Audit System', N'PRJ-010', N'Compliance Trust Europe', '2024-02-15', '2024-06-15', 180000.00, N'Completed', 9, GETUTCDATE()),
    (N'Customer Portal Self-Service Module', N'PRJ-011', N'OmniClient Success', '2024-07-01', '2024-12-15', 175000.00, N'In Progress', 10, GETUTCDATE()),
    (N'AI Customer Chatbot Integration', N'PRJ-012', N'OmniClient Success', '2024-08-01', '2025-02-28', 140000.00, N'Planning', 10, GETUTCDATE()),
    (N'Data Warehouse Migration to Azure', N'PRJ-013', N'Internal Data Team', '2024-04-15', '2024-10-31', 400000.00, N'In Progress', 1, GETUTCDATE()),
    (N'Global Employee Onboarding Portal', N'PRJ-014', N'Internal HR', '2024-05-15', '2024-11-15', 85000.00, N'In Progress', 2, GETUTCDATE()),
    (N'Automated Tax Processing Pipeline', N'PRJ-015', N'FinTech Corp', '2024-06-15', '2025-01-15', 220000.00, N'Planning', 3, GETUTCDATE()),
    (N'SEO & Content Strategy Expansion', N'PRJ-016', N'WorkflowPro Marketing Group', '2024-07-15', '2024-12-31', 65000.00, N'In Progress', 4, GETUTCDATE()),
    (N'Partner Portal Revamp', N'PRJ-017', N'Alliance Network Inc', '2024-08-15', '2025-04-30', 320000.00, N'Planning', 5, GETUTCDATE()),
    (N'Infrastructure Disaster Recovery Plan', N'PRJ-018', N'Internal Systems', '2024-03-01', '2024-09-30', 210000.00, N'In Progress', 6, GETUTCDATE()),
    (N'Warehouse Asset Tracking Solution', N'PRJ-019', N'LogiTrack Global', '2024-09-01', '2025-05-31', 480000.00, N'Planning', 7, GETUTCDATE()),
    (N'Corporate Legal Contract Engine', N'PRJ-020', N'Internal Legal', '2024-04-01', '2024-09-30', 160000.00, N'Completed', 9, GETUTCDATE());
END
GO

---------------------------------------------------------------------------------
-- 4. SEED 5 USERS
---------------------------------------------------------------------------------
PRINT 'Seeding 5 Users...';

IF NOT EXISTS (SELECT 1 FROM [dbo].[Users] WHERE [Username] = 'admin')
BEGIN
    INSERT INTO [dbo].[Users] ([Username], [PasswordHash], [Email], [Role], [IsActive], [EmployeeId], [LastLoginDate], [CreatedDate])
    VALUES
    (N'admin', N'AQAAAAEAACcQAAAAEH4Wv7...hashed_admin_sec_2026', N'admin@WorkflowPro.com', N'Admin', 1, 1, '2026-08-04 09:30:00', GETUTCDATE()),
    (N'hr.manager', N'AQAAAAEAACcQAAAAEH4Wv7...hashed_hrmgr_sec_2026', N'eleanor.vance@WorkflowPro.com', N'HR', 1, 2, '2026-08-04 10:15:00', GETUTCDATE()),
    (N'finance.lead', N'AQAAAAEAACcQAAAAEH4Wv7...hashed_finlead_sec_2026', N'marcus.sterling@WorkflowPro.com', N'Finance', 1, 3, '2026-08-04 11:00:00', GETUTCDATE()),
    (N'tech.lead', N'AQAAAAEAACcQAAAAEH4Wv7...hashed_techlead_sec_2026', N'sophia.chen@WorkflowPro.com', N'Manager', 1, 4, '2026-08-04 08:45:00', GETUTCDATE()),
    (N'support.user', N'AQAAAAEAACcQAAAAEH4Wv7...hashed_suppusr_sec_2026', N'david.miller@WorkflowPro.com', N'Employee', 1, 5, '2026-08-04 14:20:00', GETUTCDATE());
END
GO

---------------------------------------------------------------------------------
-- 5. SEED 50 DOCUMENTS (Paths stored in SQL Server; files on disk)
---------------------------------------------------------------------------------
PRINT 'Seeding 50 Documents...';

IF NOT EXISTS (SELECT 1 FROM [dbo].[Documents] WHERE [Title] = 'WorkflowPro Cloud Portal System Architecture')
BEGIN
    INSERT INTO [dbo].[Documents] ([Title], [DocumentType], [FilePath], [FileName], [ContentType], [FileSizeByte], [EmployeeId], [ProjectId], [UploadedBy], [UploadedDate])
    VALUES
    (N'WorkflowPro Cloud Portal System Architecture', N'Technical', N'~/Uploads/Documents/doc001.pdf', N'doc001.pdf', N'application/pdf', 2450000, 4, 1, N'admin', GETUTCDATE()),
    (N'Acme Corp ERP Integration Master Service Agreement', N'Contract', N'~/Uploads/Documents/doc002.pdf', N'doc002.pdf', N'application/pdf', 1850000, 1, 2, N'admin', GETUTCDATE()),
    (N'Q1 2024 HR Operations Policy & Guidelines', N'Policy', N'~/Uploads/Documents/doc003.pdf', N'doc003.pdf', N'application/pdf', 980000, 2, 3, N'hr.manager', GETUTCDATE()),
    (N'Starlight Financial Q2 Audit Report', N'Report', N'~/Uploads/Documents/doc004.pdf', N'doc004.pdf', N'application/pdf', 3100000, 3, 4, N'finance.lead', GETUTCDATE()),
    (N'WorkflowPro 2024 Brand Guidelines & Assets', N'Report', N'~/Uploads/Documents/doc005.pdf', N'doc005.pdf', N'application/pdf', 5400000, 6, 5, N'admin', GETUTCDATE()),
    (N'TechSphere CRM Integration Technical Specs', N'Technical', N'~/Uploads/Documents/doc006.docx', N'doc006.docx', N'application/vnd.openxmlformats-officedocument.wordprocessingml.document', 750000, 7, 6, N'tech.lead', GETUTCDATE()),
    (N'Zero Trust Security Audit & Penetration Testing Report', N'Report', N'~/Uploads/Documents/doc007.pdf', N'doc007.pdf', N'application/pdf', 4200000, 5, 7, N'admin', GETUTCDATE()),
    (N'Supply Chain Logistics Master Contract - Apex Logistics', N'Contract', N'~/Uploads/Documents/doc008.pdf', N'doc008.pdf', N'application/pdf', 2150000, 8, 8, N'admin', GETUTCDATE()),
    (N'NextGen Mobile UX Wireframes & Prototype Specifications', N'Technical', N'~/Uploads/Documents/doc009.pdf', N'doc009.pdf', N'application/pdf', 6800000, 9, 9, N'tech.lead', GETUTCDATE()),
    (N'GDPR Compliance Certification & Data Processing Addendum', N'Policy', N'~/Uploads/Documents/doc010.pdf', N'doc010.pdf', N'application/pdf', 1450000, 10, 10, N'admin', GETUTCDATE()),

    -- Documents 11 to 20
    (N'Customer Portal API Specification Document', N'Technical', N'~/Uploads/Documents/doc011.pdf', N'doc011.pdf', N'application/pdf', 1250000, 11, 11, N'tech.lead', GETUTCDATE()),
    (N'AI Chatbot NLP Training Dataset & Architecture', N'Technical', N'~/Uploads/Documents/doc012.pdf', N'doc012.pdf', N'application/pdf', 3800000, 12, 12, N'tech.lead', GETUTCDATE()),
    (N'Azure Data Warehouse Migration Strategy', N'Technical', N'~/Uploads/Documents/doc013.pdf', N'doc013.pdf', N'application/pdf', 2900000, 13, 13, N'tech.lead', GETUTCDATE()),
    (N'Employee Onboarding Playbook 2024', N'Policy', N'~/Uploads/Documents/doc014.pdf', N'doc014.pdf', N'application/pdf', 1100000, 21, 14, N'hr.manager', GETUTCDATE()),
    (N'Automated Tax Engine Functional Requirements', N'Technical', N'~/Uploads/Documents/doc015.docx', N'doc015.docx', N'application/vnd.openxmlformats-officedocument.wordprocessingml.document', 820000, 31, 15, N'finance.lead', GETUTCDATE()),
    (N'2024 Q3 SEO Performance Analysis', N'Report', N'~/Uploads/Documents/doc016.pdf', N'doc016.pdf', N'application/pdf', 1950000, 41, 16, N'admin', GETUTCDATE()),
    (N'Alliance Network Partner Agreement', N'Contract', N'~/Uploads/Documents/doc017.pdf', N'doc017.pdf', N'application/pdf', 1700000, 51, 17, N'admin', GETUTCDATE()),
    (N'IT Disaster Recovery & Business Continuity Plan', N'Policy', N'~/Uploads/Documents/doc018.pdf', N'doc018.pdf', N'application/pdf', 2300000, 61, 18, N'support.user', GETUTCDATE()),
    (N'LogiTrack RFID Asset Tracking Specs', N'Technical', N'~/Uploads/Documents/doc019.pdf', N'doc019.pdf', N'application/pdf', 3150000, 71, 19, N'admin', GETUTCDATE()),
    (N'Corporate Non-Disclosure Agreement Template', N'Contract', N'~/Uploads/Documents/doc020.pdf', N'doc020.pdf', N'application/pdf', 650000, 81, 20, N'admin', GETUTCDATE()),

    -- Documents 21 to 30
    (N'Employee Code of Conduct 2024', N'Policy', N'~/Uploads/Documents/doc021.pdf', N'doc021.pdf', N'application/pdf', 920000, 2, NULL, N'hr.manager', GETUTCDATE()),
    (N'Q1 Corporate Financial Statement', N'Report', N'~/Uploads/Documents/doc022.pdf', N'doc022.pdf', N'application/pdf', 4100000, 3, NULL, N'finance.lead', GETUTCDATE()),
    (N'Q2 Corporate Financial Statement', N'Report', N'~/Uploads/Documents/doc023.pdf', N'doc023.pdf', N'application/pdf', 4350000, 3, NULL, N'finance.lead', GETUTCDATE()),
    (N'Information Security Management Policy (ISO 27001)', N'Policy', N'~/Uploads/Documents/doc024.pdf', N'doc024.pdf', N'application/pdf', 2800000, 5, NULL, N'support.user', GETUTCDATE()),
    (N'Remote Work & Telecommute Guidelines', N'Policy', N'~/Uploads/Documents/doc025.pdf', N'doc025.pdf', N'application/pdf', 740000, 21, NULL, N'hr.manager', GETUTCDATE()),
    (N'Employee Performance Evaluation Framework', N'Policy', N'~/Uploads/Documents/doc026.pdf', N'doc026.pdf', N'application/pdf', 1280000, 22, NULL, N'hr.manager', GETUTCDATE()),
    (N'2024 Corporate Tax Compliance Filing', N'Report', N'~/Uploads/Documents/doc027.pdf', N'doc027.pdf', N'application/pdf', 3600000, 32, NULL, N'finance.lead', GETUTCDATE()),
    (N'Enterprise Software License Audit Report', N'Report', N'~/Uploads/Documents/doc028.pdf', N'doc028.pdf', N'application/pdf', 1890000, 62, NULL, N'support.user', GETUTCDATE()),
    (N'Marketing Campaign ROI Analysis Q2', N'Report', N'~/Uploads/Documents/doc029.pdf', N'doc029.pdf', N'application/pdf', 2100000, 42, NULL, N'admin', GETUTCDATE()),
    (N'Enterprise Sales Incentive Structure 2024', N'Policy', N'~/Uploads/Documents/doc030.pdf', N'doc030.pdf', N'application/pdf', 850000, 52, NULL, N'admin', GETUTCDATE()),

    -- Documents 31 to 40
    (N'Employee Medical Insurance Benefits Overview', N'Policy', N'~/Uploads/Documents/doc031.pdf', N'doc031.pdf', N'application/pdf', 1420000, 23, NULL, N'hr.manager', GETUTCDATE()),
    (N'Travel & Expense Reimbursement Policy', N'Policy', N'~/Uploads/Documents/doc032.pdf', N'doc032.pdf', N'application/pdf', 690000, 33, NULL, N'finance.lead', GETUTCDATE()),
    (N'Data Protection Impact Assessment (DPIA)', N'Report', N'~/Uploads/Documents/doc033.pdf', N'doc033.pdf', N'application/pdf', 2400000, 82, NULL, N'admin', GETUTCDATE()),
    (N'Cloud Infrastructure Billing Invoice - June 2024', N'Invoice', N'~/Uploads/Documents/doc034.pdf', N'doc034.pdf', N'application/pdf', 450000, 34, 13, N'finance.lead', GETUTCDATE()),
    (N'Software Vendor SLA Agreement - Microsoft', N'Contract', N'~/Uploads/Documents/doc035.pdf', N'doc035.pdf', N'application/pdf', 1950000, 63, NULL, N'support.user', GETUTCDATE()),
    (N'Facility Lease Agreement - Headquarters', N'Contract', N'~/Uploads/Documents/doc036.pdf', N'doc036.pdf', N'application/pdf', 3800000, 73, NULL, N'admin', GETUTCDATE()),
    (N'Product Roadmap & Feature Specs 2025', N'Technical', N'~/Uploads/Documents/doc037.pdf', N'doc037.pdf', N'application/pdf', 4120000, 76, 9, N'tech.lead', GETUTCDATE()),
    (N'Customer Satisfaction Survey Summary 2024', N'Report', N'~/Uploads/Documents/doc038.pdf', N'doc038.pdf', N'application/pdf', 1670000, 86, 11, N'admin', GETUTCDATE()),
    (N'Annual Internal Quality Assurance Audit', N'Report', N'~/Uploads/Documents/doc039.pdf', N'doc039.pdf', N'application/pdf', 2850000, 16, NULL, N'tech.lead', GETUTCDATE()),
    (N'Hardware Procurement Authorization Invoice', N'Invoice', N'~/Uploads/Documents/doc040.pdf', N'doc040.pdf', N'application/pdf', 520000, 64, 7, N'support.user', GETUTCDATE()),

    -- Documents 41 to 50
    (N'Global Talent Acquisition Strategy 2025', N'Report', N'~/Uploads/Documents/doc041.pdf', N'doc041.pdf', N'application/pdf', 1780000, 22, NULL, N'hr.manager', GETUTCDATE()),
    (N'Corporate Audit Committee Charter', N'Policy', N'~/Uploads/Documents/doc042.pdf', N'doc042.pdf', N'application/pdf', 890000, 35, NULL, N'finance.lead', GETUTCDATE()),
    (N'Mobile App Penetration Testing Vulnerability Report', N'Report', N'~/Uploads/Documents/doc043.pdf', N'doc043.pdf', N'application/pdf', 3450000, 18, 9, N'tech.lead', GETUTCDATE()),
    (N'Customer Support Ticket Escalation Protocol', N'Policy', N'~/Uploads/Documents/doc044.pdf', N'doc044.pdf', N'application/pdf', 620000, 87, 11, N'admin', GETUTCDATE()),
    (N'Logistics Warehouse Safety Compliance Standard', N'Policy', N'~/Uploads/Documents/doc045.pdf', N'doc045.pdf', N'application/pdf', 1350000, 72, 8, N'admin', GETUTCDATE()),
    (N'Enterprise ERP Data Schema Dictionary', N'Technical', N'~/Uploads/Documents/doc046.pdf', N'doc046.pdf', N'application/pdf', 2980000, 15, 2, N'tech.lead', GETUTCDATE()),
    (N'WorkflowPro Q3 Investor Relations Deck', N'Report', N'~/Uploads/Documents/doc047.pdf', N'doc047.pdf', N'application/pdf', 5800000, 3, NULL, N'admin', GETUTCDATE()),
    (N'Sales Commission Payout Reconciliation July 2024', N'Invoice', N'~/Uploads/Documents/doc048.pdf', N'doc048.pdf', N'application/pdf', 720000, 34, 6, N'finance.lead', GETUTCDATE()),
    (N'Intellectual Property Patent Filing Summary', N'Contract', N'~/Uploads/Documents/doc049.pdf', N'doc049.pdf', N'application/pdf', 2100000, 81, NULL, N'admin', GETUTCDATE()),
    (N'Phase 1 Completion Signoff & Transition Document', N'Report', N'~/Uploads/Documents/doc050.pdf', N'doc050.pdf', N'application/pdf', 1150000, 1, 1, N'admin', GETUTCDATE());
END
GO

---------------------------------------------------------------------------------
-- 6. SEED 200 AUDIT LOGS
---------------------------------------------------------------------------------
PRINT 'Seeding 200 Audit Logs...';

IF NOT EXISTS (SELECT 1 FROM [dbo].[AuditLogs] WHERE [Id] = 1)
BEGIN
    DECLARE @i INT = 1;
    DECLARE @UserCount INT = 5;
    DECLARE @ActionName NVARCHAR(100);
    DECLARE @Entity NVARCHAR(100);
    DECLARE @EntityIdVal NVARCHAR(50);
    DECLARE @DetailText NVARCHAR(1000);
    DECLARE @Ip NVARCHAR(50);
    DECLARE @TargetUserId INT;
    DECLARE @EventDate DATETIME2;

    WHILE @i <= 200
    BEGIN
        SET @TargetUserId = ((@i - 1) % 5) + 1;
        SET @Ip = N'192.168.1.' + CAST((10 + (@i % 40)) AS NVARCHAR(10));
        SET @EventDate = DATEADD(HOUR, -(@i * 6), GETUTCDATE());

        IF @i % 7 = 1
        BEGIN
            SET @ActionName = N'UserLogin';
            SET @Entity = N'User';
            SET @EntityIdVal = CAST(@TargetUserId AS NVARCHAR(50));
            SET @DetailText = N'User successfully authenticated and established web session.';
        END
        ELSE IF @i % 7 = 2
        BEGIN
            SET @ActionName = N'CreateEmployee';
            SET @Entity = N'Employee';
            SET @EntityIdVal = CAST(((@i * 3) % 100) + 1 AS NVARCHAR(50));
            SET @DetailText = N'New employee record created and assigned to department.';
        END
        ELSE IF @i % 7 = 3
        BEGIN
            SET @ActionName = N'UpdateEmployee';
            SET @Entity = N'Employee';
            SET @EntityIdVal = CAST(((@i * 2) % 100) + 1 AS NVARCHAR(50));
            SET @DetailText = N'Employee contact info and designation updated successfully.';
        END
        ELSE IF @i % 7 = 4
        BEGIN
            SET @ActionName = N'UploadDocument';
            SET @Entity = N'Document';
            SET @EntityIdVal = CAST(((@i * 5) % 50) + 1 AS NVARCHAR(50));
            SET @DetailText = N'New document uploaded and file path registered on server disk.';
        END
        ELSE IF @i % 7 = 5
        BEGIN
            SET @ActionName = N'UpdateProjectStatus';
            SET @Entity = N'Project';
            SET @EntityIdVal = CAST(((@i * 4) % 20) + 1 AS NVARCHAR(50));
            SET @DetailText = N'Project status milestone transitioned and saved to database.';
        END
        ELSE IF @i % 7 = 6
        BEGIN
            SET @ActionName = N'ModifySetting';
            SET @Entity = N'Setting';
            SET @EntityIdVal = N'1';
            SET @DetailText = N'System configuration key value updated in portal settings.';
        END
        ELSE
        BEGIN
            SET @ActionName = N'ViewAuditReport';
            SET @Entity = N'AuditLog';
            SET @EntityIdVal = N'N/A';
            SET @DetailText = N'System administrator generated security audit log export.';
        END

        INSERT INTO [dbo].[AuditLogs] ([Action], [EntityName], [EntityId], [Details], [IpAddress], [UserId], [Timestamp])
        VALUES (@ActionName, @Entity, @EntityIdVal, @DetailText, @Ip, @TargetUserId, @EventDate);

        SET @i = @i + 1;
    END
END
GO

---------------------------------------------------------------------------------
-- 7. SEED 1 SETTINGS RECORD (With Core System Settings Entries)
---------------------------------------------------------------------------------
PRINT 'Seeding Settings Records...';

IF NOT EXISTS (SELECT 1 FROM [dbo].[Settings] WHERE [Key] = 'Portal:SystemConfig')
BEGIN
    INSERT INTO [dbo].[Settings] ([Key], [Value], [Description], [Category], [UpdatedDate], [UpdatedBy])
    VALUES
    (N'Portal:SystemConfig', N'{"PortalName":"Workflow Pro","Environment":"Production","Version":"2.0.0","MaxUploadMB":20,"StoragePath":"~/Uploads","EnableAuditLogging":true}', N'Main System Configuration for Workflow Pro', N'General', GETUTCDATE(), N'admin');
END
GO

COMMIT TRANSACTION;
PRINT 'Seed Data Execution Completed Successfully!';
GO


