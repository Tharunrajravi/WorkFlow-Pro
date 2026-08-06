using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace WorkflowPro.Models
{
    public class EmployeeDBContext : DbContext
    {
        public EmployeeDBContext() : base("name=EmployeeDBContext")
        {
            Database.SetInitializer<EmployeeDBContext>(null);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<FileMetadata> FileMetadatas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Prevent automatic cascade deletes on all relationships to avoid multiple cascade paths
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            // Decimal Precision Configuration
            modelBuilder.Entity<Employee>()
                .Property(e => e.Salary)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Project>()
                .Property(p => p.Budget)
                .HasPrecision(18, 2);

            // Department -> Employees (1-to-Many)
            modelBuilder.Entity<Department>()
                .HasMany(d => d.Employees)
                .WithRequired(e => e.Department)
                .HasForeignKey(e => e.DepartmentId)
                .WillCascadeOnDelete(false);

            // Department -> Projects (1-to-Many)
            modelBuilder.Entity<Department>()
                .HasMany(d => d.Projects)
                .WithRequired(p => p.Department)
                .HasForeignKey(p => p.DepartmentId)
                .WillCascadeOnDelete(false);

            // Employee -> User (Optional 1-to-1 / Foreign Key)
            modelBuilder.Entity<User>()
                .HasOptional(u => u.Employee)
                .WithMany()
                .HasForeignKey(u => u.EmployeeId)
                .WillCascadeOnDelete(false);

            // User -> AuditLogs (1-to-Many)
            modelBuilder.Entity<User>()
                .HasMany(u => u.AuditLogs)
                .WithOptional(a => a.User)
                .HasForeignKey(a => a.UserId)
                .WillCascadeOnDelete(false);

            // Employee -> Documents (1-to-Many Optional)
            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Documents)
                .WithOptional(d => d.Employee)
                .HasForeignKey(d => d.EmployeeId)
                .WillCascadeOnDelete(false);

            // Project -> Documents (1-to-Many Optional)
            modelBuilder.Entity<Project>()
                .HasMany(p => p.Documents)
                .WithOptional(d => d.Project)
                .HasForeignKey(d => d.ProjectId)
                .WillCascadeOnDelete(false);
        }
    }
}

