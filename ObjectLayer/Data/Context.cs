using Microsoft.EntityFrameworkCore;
using ObjectLayer;

namespace ExFit.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }


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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
        }
    }
}
