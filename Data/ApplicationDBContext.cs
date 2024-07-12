using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Class> Classes { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<GradeItem> GradeItems { get; set; }
        public DbSet<Grade> Grades { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Seed roles
            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);

            // Configure relationships

            // AppUser and Class: One-to-Many (One teacher to many classes)
            builder.Entity<Class>()
                .HasOne(c => c.Teacher)
                .WithMany(u => u.Classes)
                .HasForeignKey(c => c.TeacherId);


            // Student and Class: Many-to-Many
            builder.Entity<Student>()
                .HasMany(s => s.Classes)
                .WithMany(c => c.Students)
                .UsingEntity(j => j.ToTable("StudentClasses"));

            // Student and Attendance: One-to-Many
            builder.Entity<Attendance>()
                .HasOne(a => a.Student)
                .WithMany(s => s.Attendances)
                .HasForeignKey(a => a.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Class and Attendance: One-to-Many
            builder.Entity<Attendance>()
                .HasOne(a => a.Class)
                .WithMany(c => c.Attendances)
                .HasForeignKey(a => a.ClassId);

            // GradeItem and Class: One-to-Many
            builder.Entity<GradeItem>()
                .HasOne(gi => gi.Class)
                .WithMany(c => c.GradeItems)
                .HasForeignKey(gi => gi.ClassId);

            // Grade and GradeItem: One-to-Many
            builder.Entity<Grade>()
                .HasOne(g => g.GradeItem)
                .WithMany(gi => gi.Grades)
                .HasForeignKey(g => g.GradeItemId);

            // Grade and Student: One-to-Many
            builder.Entity<Grade>()
                .HasOne(g => g.Student)
                .WithMany(s => s.Grades)
                .HasForeignKey(g => g.StudentId)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}