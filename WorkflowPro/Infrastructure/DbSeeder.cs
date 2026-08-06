using System;
using System.Linq;
using WorkflowPro.Models;

namespace WorkflowPro.Infrastructure
{
    // Runs once at application start. If the Users table has no rows yet,
    // creates a single default Admin account so there is always a way to
    // log in on a freshly deployed database. Safe to leave in place -
    // it becomes a no-op the moment any user row exists.
    public static class DbSeeder
    {
        public const string DefaultUsername = "admin";
        public const string DefaultPassword = "Admin@123"; // change this after first login

        public static void SeedAdminUser()
        {
            using (var db = new ApplicationDbContext())
            {
                if (db.Users.Any())
                {
                    return;
                }

                db.Users.Add(new User
                {
                    Username = DefaultUsername,
                    PasswordHash = PasswordHasher.Hash(DefaultPassword),
                    FullName = "System Administrator",
                    Role = "Admin",
                    IsActive = true,
                    CreatedDate = DateTime.Now
                });

                db.SaveChanges();
            }
        }
    }
}
