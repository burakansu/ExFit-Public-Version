using Microsoft.EntityFrameworkCore;
using ObjectLayer;
using ObjectLayer.Extensions;
using System.Collections.Generic;

namespace ExFit.Data
{
    public class Context : DbContext
    {
        public Context()
        {

        }
        public Context(DbContextOptions<Context> dbContext)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(ConnectionExtensions.GetConnectionString());
        }
        public DbSet<ObjCompany> Companies { get; set; }
        public DbSet<ObjUser> Users { get; set; }
        public DbSet<ObjMember> Members { get; set; }
        public DbSet<ObjMemberMeazurement> MemberMeazurements { get; set; }
        public DbSet<ObjDiet> Diets { get; set; }
        public DbSet<ObjExcersize> Excersizes { get; set; }
        public DbSet<ObjFood> Foods { get; set; }
        public DbSet<ObjPractice> Practices { get; set; }
        public DbSet<ObjCost> Costs { get; set; }
        public DbSet<ObjIncome> Incomes { get; set; }
        public DbSet<ObjTask> Tasks { get; set; }
        public DbSet<ObjPackage> Packages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ObjCompany>().ToTable("TBL_Company");
            modelBuilder.Entity<ObjUser>().ToTable("TBL_Users");
            modelBuilder.Entity<ObjMember>().ToTable("TBL_Members");
            modelBuilder.Entity<ObjMemberMeazurement>().ToTable("TBL_Members_Meazurements");
            modelBuilder.Entity<ObjDiet>().ToTable("TBL_Diet");
            modelBuilder.Entity<ObjExcersize>().ToTable("TBL_Excersize");
            modelBuilder.Entity<ObjFood>().ToTable("TBL_Food");
            modelBuilder.Entity<ObjPractice>().ToTable("TBL_Practice");
            modelBuilder.Entity<ObjCost>().ToTable("TBL_Cost");
            modelBuilder.Entity<ObjIncome>().ToTable("TBL_Income");
            modelBuilder.Entity<ObjTask>().ToTable("TBL_Tasks");
            modelBuilder.Entity<ObjPackage>().ToTable("TBL_Package");
        }
    }
}
