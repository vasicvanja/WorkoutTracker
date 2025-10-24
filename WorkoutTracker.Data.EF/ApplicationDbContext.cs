using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WorkoutTracker.DomainModels;

namespace WorkoutTracker.Data.EF
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the composite key for the WorkoutExercise join table
            modelBuilder.Entity<WorkoutExercise>()
                .HasKey(we => new { we.WorkoutId, we.ExerciseId });

            modelBuilder.Entity<WorkoutExercise>()
                .HasOne(we => we.Workout)
                .WithMany(w => w.WorkoutExercises)
                .HasForeignKey(we => we.WorkoutId);

            modelBuilder.Entity<WorkoutExercise>()
                .HasOne(we => we.Exercise)
                .WithMany(e => e.WorkoutExercises)
                .HasForeignKey(we => we.ExerciseId);

            modelBuilder.Entity<WorkoutExercise>()
               .HasOne(we => we.Workout)
               .WithMany(w => w.WorkoutExercises)
               .HasForeignKey(we => we.WorkoutId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WorkoutExercise>()
                .HasOne(we => we.Exercise)
                .WithMany(e => e.WorkoutExercises)
                .HasForeignKey(we => we.ExerciseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Workout>()
                .HasOne(w => w.ApplicationUser)
                .WithMany(u => u.Workouts)
                .HasForeignKey(w => w.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Goal>()
                .HasOne(g => g.ApplicationUser)
                .WithMany(u => u.Goals)
                .HasForeignKey(g => g.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Measurement>()
                .HasOne(m => m.ApplicationUser)
                .WithMany(u => u.Measurements)
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<Workout> Workouts { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<WorkoutExercise> WorkoutExercises { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<Measurement> Measurements { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }
        public DbSet<SmtpSettings> SmtpSettings { get; set; }
    }
}
