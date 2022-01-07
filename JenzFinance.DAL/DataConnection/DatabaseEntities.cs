using JenzFinance.DAL.Entity;
using JenzFinance.DAL.Migrations;
using System.Data.Entity;

namespace JenzFinance.DAL.DataConnection
{
    public class DatabaseEntities : DbContext
    {
        public DatabaseEntities() : base("name=DatabaseEntities")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DatabaseEntities, Configuration>());
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SavingCategory>().
          HasOptional(e => e.Parent).
          WithMany().
          HasForeignKey(m => m.ParentID);


            base.OnModelCreating(modelBuilder);
        }
        public virtual DbSet<ApplicationSettings> ApplicationSettings { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<RolePermission> RolePermissions { get; set; }
        public virtual DbSet<SavingCategory> SavingCategories { get; set; }
        public virtual DbSet<SavingRecord> SavingRecords { get; set; }

    }
}
