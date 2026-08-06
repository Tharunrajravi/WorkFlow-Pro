# WorkflowPro

Simple ASP.NET MVC 5 (.NET Framework 4.7.2) internal HR application:
Authentication, Dashboard, Departments, Employees (with photo upload),
and Documents (upload/download/delete). Built for IIS on Windows
Server 2016.

## ⚠️ Important note on scope

The public repo (`Tharunrajravi/WorkFlow-Pro`) only exposed a bare
`WorkflowPro.sln` + `WorkflowPro` folder when this was generated -
its file contents weren't reachable from this environment (GitHub
blocks automated browsing of the tree view, and the repo API wasn't
accessible either). So this is **not a diff against your actual
Phase 1/2 code** - it's a complete, self-consistent implementation of
everything in your spec, built fresh on the architecture you
described (Forms Auth + BCrypt + EF6 + MVC5 + Bootstrap 5).

**Before you deploy:** if your real Phase 1/2 code has different
table/column names, a different auth approach, or extra fields,
reconcile this code against that first - especially `Models/*.cs`
(EF mappings) and `Database/Schema.sql`.

This was also written and reviewed as text - there's no compiler in
the environment that produced it, so a build in Visual Studio is the
first real syntax check. Do that before deploying.

## What's included

| Module | Status |
|---|---|
| Authentication (Login/Logout, Forms Auth, BCrypt, role-based auth) | ✅ |
| Dashboard (counts + welcome message) | ✅ |
| Department CRUD | ✅ |
| Employee CRUD (+ profile photo upload) | ✅ |
| Document upload / download / delete | ✅ |

Deliberately **not** included, per spec: Projects, Reports,
Notifications, Email, a complex Admin module, any Web API/JWT,
SignalR, microservices, background services, or logging frameworks.

## Project layout

```
WorkflowPro.sln
WorkflowPro/                  <- the deployable IIS web application
  Controllers/                <- Account, Dashboard, Department, Employee, Document
  Models/                     <- User, Department, Employee, Document + EF DbContext
  Models/ViewModels/
  Views/
  Infrastructure/             <- PasswordHasher (BCrypt wrapper), CustomPrincipal,
                                  DbSeeder (creates the first admin account)
  Uploads/Documents/          <- uploaded document files land here
  Uploads/Employees/          <- employee profile photos land here
  Web.config
Database/
  Schema.sql                  <- NOT part of the deployed site; run manually against SQL Server
```

`Database/Schema.sql` lives outside the `WorkflowPro/` project folder
on purpose, so it's never accidentally published to the web server.

## Assumptions made (reconcile if your existing DB differs)

- Database name: `EmployeeDB`, reached via the `EmployeeDBContext`
  connection string in `Web.config`.
- Tables: `Users`, `Departments`, `Employees`, `Documents` - see
  `Database/Schema.sql` for exact columns. The script only creates
  tables that don't already exist, and only adds
  `Employees.ProfilePhotoPath` if it's missing - it never touches
  existing data.
- Roles are a simple string column (`Admin` / `User`) on `Users`,
  not a separate roles table - kept intentionally simple.
- Documents are metadata-only rows in SQL Server; the actual files
  live under `Uploads/Documents` on disk, referenced by a
  GUID-based `StoredFileName` (the original filename is kept
  separately for display/download).

## First-time setup

1. **Database.** Run `Database/Schema.sql` against your SQL Server
   instance (adjust the `USE EmployeeDB;` line if needed).
2. **Connection string.** Update `WorkflowPro/Web.config` →
   `connectionStrings/EmployeeDBContext` with your real
   server/credentials.
3. **Restore NuGet packages** in Visual Studio (Right-click solution
   → Restore NuGet Packages). Packages used: `Microsoft.AspNet.Mvc`
   5.2.9, `EntityFramework` 6.4.4, `BCrypt.Net-Next` 4.0.3,
   `Newtonsoft.Json`.
4. **Build** the solution (Release configuration) and fix anything
   the compiler flags - see the caveat above about this not having
   been compiled yet.
5. **Machine key.** Replace the placeholder `validationKey` /
   `decryptionKey` values in `Web.config` → `<machineKey>` with your
   own generated values before going to production (search "IIS
   generate machineKey" for a generator, or use
   `System.Web.Security.Membership` locally) - especially important
   if you'll ever run more than one server/app pool instance.
6. **First login.** The app seeds a single default admin account the
   first time it starts against an empty `Users` table:
   - Username: `admin`
   - Password: `Admin@123`

   Change this password immediately after first login (there's no
   in-app "change password" screen per the "keep it simple"
   instruction - update the `Users.PasswordHash` row directly using
   a hash produced by `WorkflowPro.Infrastructure.PasswordHasher.Hash(...)`,
   e.g. by temporarily calling it from Immediate Window / a scratch
   console app that references the same BCrypt.Net-Next package).
7. To add more users/roles, insert rows into `Users` the same way
   (hash the password with `PasswordHasher.Hash`, set `Role` to
   `Admin` or `User`).

## Publishing to IIS on Windows Server 2016

1. In Visual Studio: right-click the `WorkflowPro` project → **Publish**
   → **Folder** (or **Web Deploy** if you have that configured on the
   server) → publish to a local folder or directly to the server.
2. On the server, create a new IIS site/application pointing at the
   published folder.
   - Application Pool: **.NET CLR Version v4.0**, **Integrated**
     pipeline mode.
   - Ensure the app pool identity has **write** permission on
     `Uploads/Documents` and `Uploads/Employees` (uploads are saved
     there at runtime) - grant Modify permission to the app pool
     identity (e.g. `IIS AppPool\YourAppPoolName`) on those two
     folders.
3. Make sure **ASP.NET 4.7** is registered with IIS on the server
   (`%windir%\Microsoft.NET\Framework64\v4.0.30319\aspnet_regiis.exe -i`
   if it isn't already, or enable the ASP.NET 4.x feature via
   Server Manager → Add Roles and Features → Web Server (IIS) →
   Application Development).
4. Confirm SQL Server connectivity from the web server (firewall,
   SQL auth vs. Windows auth - the app pool identity needs DB access
   if using Integrated Security).
5. Browse to the site - you should land on the login page.

## Notes on the auth model

- Forms Authentication issues an encrypted cookie whose `UserData`
  carries the user's role. `Global.asax.cs` decrypts that cookie on
  every request and attaches a role-aware `IPrincipal`, so
  `[Authorize(Roles = "Admin")]` on controllers/actions works without
  pulling in ASP.NET Identity/OWIN - kept deliberately lightweight.
- All authenticated users (`Admin` or `User`) can view Departments,
  Employees, and Documents, and can upload/download documents.
  Only `Admin` can create/edit/delete Departments and Employees, and
  delete Documents. Adjust the `[Authorize(Roles = "Admin")]`
  attributes in the controllers if you want different permissions.

## Known limitations (by design, per "keep it simple")

- No password-change or user-management UI - manage the `Users`
  table directly.
- No pagination on list views - fine for a small/medium employee
  count; add `.Skip()/.Take()` in the controllers later if lists grow
  large.
- No client-side image resizing/cropping for profile photos - files
  are stored as uploaded (size-capped at 5 MB).
- No automated tests, logging framework, or CI - matches the
  "no enterprise features" instruction.
