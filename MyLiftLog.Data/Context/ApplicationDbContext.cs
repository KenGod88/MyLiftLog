using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyLiftLog.Data.Models;
using MyLiftLog.IdentityServer.Models;
using System;

namespace MyLiftLog.Data.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<WorkoutExercise> WorkoutExercises { get; set; }
        public DbSet<Set> Sets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure WorkoutExercise
            modelBuilder.Entity<WorkoutExercise>()
                .HasKey(we => we.Id);

            modelBuilder.Entity<WorkoutExercise>()
                .HasOne(we => we.Workout)
                .WithMany(w => w.WorkoutExercises)
                .HasForeignKey(we => we.WorkoutId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WorkoutExercise>()
                .HasOne(we => we.Exercise)
                .WithMany(e => e.WorkoutExercises)
                .HasForeignKey(we => we.ExerciseId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure Set
            modelBuilder.Entity<Set>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<Set>()
                .HasOne(s => s.WorkoutExercise)
                .WithMany(we => we.Sets)
                .HasForeignKey(s => s.WorkoutExerciseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}