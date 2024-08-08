using Microsoft.EntityFrameworkCore;

namespace HRMS.Models
{
    public partial class DataContext : DbContext
    {
        
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
          
        }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<UserAttendance> UserAttendances { get; set; }
        public virtual DbSet<WorkDiary>  WorkDiaries { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<UserRole>().HasData(
              new UserRole
              {
                  Id = 1,
                  RoleName = "Administrator",
              });

            modelBuilder.Entity<Users>().HasData(
               new Users
               {
                   UserId = 1,
                   Name = "Administrator",
                   UserName = "sysadmin",
                   Password = "PRXJ+NOwy1N/lESh44wPHQ==",
                   RoleId = 1,
                   CreatedDate = DateTime.UtcNow,
                   LastPasswordUpdated = DateTime.UtcNow,
                   DepartmentId = null,
                   //Password = "wiqi59ebdcWKS03EETAEvA==",

               });

           
        }
    }
}
