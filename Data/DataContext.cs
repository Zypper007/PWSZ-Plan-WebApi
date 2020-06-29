using Microsoft.EntityFrameworkCore;
using PWSZ_Plan_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWSZ_Plan_WebApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}

        public DbSet<User> Users { get; set; }
        public DbSet<Institute> Institutes { get; set; }
        public DbSet<Major> Majors { get; set; }
        public DbSet<Specialization> Specyfications { get; set; }
        public DbSet<Year> Years { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<PlanClass> PlanClass { get; set; }
        public DbSet<Information> Informations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            #region Information
            modelBuilder.Entity<Information>()
                .HasOne(i => i.Author)
                .WithMany(u => u.Informations)
                .HasForeignKey(i => i.AuthorID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Information>()
                .HasOne(i => i.Class)
                .WithMany(c => c.Informations)
                .HasForeignKey(i => i.ClassID)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region Major
            modelBuilder.Entity<Major>()
                .HasOne(m => m.Institute)
                .WithMany(i => i.Majors)
                .HasForeignKey(m => m.InstituteID)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region Class-Plan
            modelBuilder.Entity<PlanClass>()
                .HasKey(pc => new { pc.PlanID, pc.ClassID });

            modelBuilder.Entity<PlanClass>()
                .HasOne(pc => pc.Plan)
                .WithMany(p => p.Classes)
                .HasForeignKey(pc => pc.PlanID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PlanClass>()
                .HasOne(pc => pc.Class)
                .WithMany(c => c.Plans)
                .HasForeignKey(pc => pc.ClassID)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region Plan
            modelBuilder.Entity<Plan>()
                .Property(p => p.EndSessionDate)
                .IsRequired(false);

            modelBuilder.Entity<Plan>()
                .Property(p => p.StartSessionDate)
                .IsRequired(false);

            modelBuilder.Entity<Plan>()
                 .HasOne(p => p.Year)
                 .WithMany(y => y.Plans)
                 .HasForeignKey(p => p.YearID)
                 .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region Specialization
            modelBuilder.Entity<Specialization>()
                .HasOne(s => s.Major)
                .WithMany(m => m.Specializations)
                .HasForeignKey(s => s.MajorID)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region User
            modelBuilder.Entity<User>()
                .HasOne(u => u.Institute)
                .WithMany(i => i.Managers)
                .HasForeignKey(u => u.InstituteID)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
            #endregion

            #region Class
            modelBuilder.Entity<Class>()
                .HasOne(c => c.Subject)
                .WithMany(s => s.Classes)
                .HasForeignKey(c => c.SubjectID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Class>()
                .HasOne(c => c.Room)
                .WithMany(r => r.Classes)
                .HasForeignKey(c => c.RoomID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Class>()
                .HasOne(c => c.Lecturer)
                .WithMany(u => u.LecturerClasses)
                .HasForeignKey(c => c.LecturerID)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion
        }
    }
}
