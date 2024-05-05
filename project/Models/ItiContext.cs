using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using project.Controllers;

namespace project.Models
{
    public class ItiContext : IdentityDbContext<AppUser>  
    {
        public ItiContext() : base()
        {

        }

        public ItiContext(DbContextOptions<ItiContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-IJL9O6M;Initial Catalog=MVC_tasks;Integrated Security=True;Connect Timeout=30;Encrypt=False;");
          //  base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>().HasData(new Department() { id = 1, name = "SD", manager = "Eng.Ayman" },
                new Department() { id = 2, name = "UI/UX", manager = "Eng.Saad" }, new Department() { id = 3, name = "open source", manager = "Eng.christen" });

            modelBuilder.Entity<Instructor>().HasData(new Instructor() { id = 1, name = "Mohamed", image = "ins1.png", salary = 5000, address = "cairo" },
             new Instructor() { id = 2, name = "Ahmed", image = "ins2.webp", salary = 4000, address = "Alex" }, new Instructor() { id = 3, name = "Josphine", image = "ins3.webp", salary = 40000, address = "Minya" });

            modelBuilder.Entity<Trainee>().HasData(new Trainee() { id = 1, name = "shady", image = "stu1.jpg", grade = "excellent", address = "cairo" },
            new Trainee() { id = 2, name = "moustafa", image = "stu2.webp", grade = "good", address = "Alex" }, new Trainee() { id = 3, name = "Mona", image = "stu3.webp", grade = "failed", address = "Minya" });

            modelBuilder.Entity<Course>().HasData(new Course() { id = 1, name = "C#", degree = 100, minDegree = 60 }
            , new Course() { id = 2, name = "OOP", degree = 100, minDegree = 70 }, new Course() { id = 3, name = "J.S", degree = 100, minDegree = 50 });

            modelBuilder.Entity<CrsResult>().HasData(new CrsResult() { id = 1, degree = 90 }, new CrsResult() { id = 2, degree = 90 },
                new CrsResult() { id = 3, degree = 90 });

            base.OnModelCreating(modelBuilder);

        }

        public DbSet<Department> departments { get; set; }         
            public DbSet<Instructor> instructors { get; set; }  
            public DbSet<Trainee> trainees { get; set; }
            public DbSet<Course> courses { get; set; }
            public DbSet<CrsResult> crsResults { get; set; }  // recommended to have dbset for table represent m:m >> to access easily
    }
}
