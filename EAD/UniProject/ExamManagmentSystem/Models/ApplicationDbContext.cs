using ExamManagmentSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Student> Students { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Batch> Batches { get; set; }
    public DbSet<Section> Sections { get; set; }

    public DbSet<Exam> Exams { get; set; }
    public DbSet<AttendanceSheet> AttendanceSheets { get; set; }
    public DbSet<SittingArrangement> SittingArrangements { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Batch>()
            .Property(b => b.Year)
            .IsRequired();

        modelBuilder.Entity<Student>()
            .HasIndex(s => s.RollNumber)
            .IsUnique();

        modelBuilder.Entity<Section>()
            .HasOne(s => s.Batch)
            .WithMany(b => b.Sections)
            .HasForeignKey(s => s.BatchId)
            .OnDelete(DeleteBehavior.Cascade);  // Keep cascade here if needed

        modelBuilder.Entity<Student>()
            .HasOne(s => s.Section)
            .WithMany(sec => sec.Students)
            .HasForeignKey(s => s.SectionId);

        modelBuilder.Entity<Exam>()
            .HasOne(e => e.Batch)
            .WithMany(b => b.Exams)
            .HasForeignKey(e => e.BatchId)
            .OnDelete(DeleteBehavior.Restrict); // Disable cascade on this FK to avoid multiple cascade paths

        modelBuilder.Entity<Exam>()
            .HasOne(e => e.Section)
            .WithMany(s => s.Exams)
            .HasForeignKey(e => e.SectionId)
            .OnDelete(DeleteBehavior.Cascade); // Keep cascade here if you want deletes in Section to delete Exams
    }


}
