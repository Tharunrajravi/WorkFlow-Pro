using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace WorkflowPro.Models
{
    public class EmployeeDBContext : DbContext
    {
        public EmployeeDBContext() : base("name=EmployeeDBContext")
        {
            // Schema is managed via /Database SQL scripts, not EF Migrations.
            Database.SetInitializer<EmployeeDBContext>(null);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectAssignment> ProjectAssignments { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Entity<Employee>()
                .HasOptional(e => e.ReportingManager)
                .WithMany(e => e.DirectReports)
                .HasForeignKey(e => e.ReportingManagerId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasRequired(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Project>()
                .HasRequired(p => p.Department)
                .WithMany(d => d.Projects)
                .HasForeignKey(p => p.DepartmentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Project>()
                .HasOptional(p => p.ProjectManager)
                .WithMany()
                .HasForeignKey(p => p.ProjectManagerId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProjectAssignment>()
                .HasRequired(pa => pa.Project)
                .WithMany(p => p.ProjectAssignments)
                .HasForeignKey(pa => pa.ProjectId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProjectAssignment>()
                .HasRequired(pa => pa.Employee)
                .WithMany(e => e.ProjectAssignments)
                .HasForeignKey(pa => pa.EmployeeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasOptional(u => u.Employee)
                .WithMany()
                .HasForeignKey(u => u.EmployeeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Document>()
                .HasOptional(d => d.Employee)
                .WithMany(e => e.Documents)
                .HasForeignKey(d => d.EmployeeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Document>()
                .HasOptional(d => d.Project)
                .WithMany(p => p.Documents)
                .HasForeignKey(d => d.ProjectId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Document>()
                .HasOptional(d => d.Department)
                .WithMany(dep => dep.Documents)
                .HasForeignKey(d => d.DepartmentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Document>()
                .HasRequired(d => d.UploadedByUser)
                .WithMany()
                .HasForeignKey(d => d.UploadedByUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AuditLog>()
                .HasOptional(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserId)
                .WillCascadeOnDelete(false);
        }
    }
}
