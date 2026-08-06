using System.Data.Entity;

namespace WorkflowPro.Models
{
    // Maps onto the existing EmployeeDB database (Database-first mindset,
    // Code-First mapping). No migrations are enabled - the schema is
    // controlled by Database/Schema.sql, not by EF.
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("name=EmployeeDBContext")
        {
            Database.SetInitializer<ApplicationDbContext>(null);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Document> Documents { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Department>().ToTable("Departments");
            modelBuilder.Entity<Employee>().ToTable("Employees");
            modelBuilder.Entity<Document>().ToTable("Documents");

            modelBuilder.Entity<Employee>()
                .HasRequired(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}
