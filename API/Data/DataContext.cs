
using Microsoft.EntityFrameworkCore;
using API.Entities;
namespace API.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    // DbSet properties
    public DbSet<User> Users { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Professor> Professors { get; set; }
    public DbSet<Staff> StaffMembers { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<Evaluation> Evaluations { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Resource> Resources { get; set; }
    public DbSet<Server> Servers { get; set; }
    public DbSet<Channel> Channels { get; set; }
    public DbSet<Message> Messages { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuration for User and its subclasses
        modelBuilder.Entity<User>()
            .HasKey(u => u.UserId);

        modelBuilder.Entity<Student>().HasBaseType<User>();
        modelBuilder.Entity<Professor>().HasBaseType<User>();
        modelBuilder.Entity<Staff>().HasBaseType<User>();

        // Configuration for Enrollment
        modelBuilder.Entity<Enrollment>()
            .HasKey(e => e.EnrollmentID);

        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.Student)
            .WithMany(s => s.Enrollments)
            .HasForeignKey(e => e.StudentId);

        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.Course)
            .WithMany(c => c.Enrollments)
            .HasForeignKey(e => e.CourseId);

        // Configuration for Evaluation
        modelBuilder.Entity<Evaluation>()
            .HasKey(e => e.EvaluationID);

        modelBuilder.Entity<Evaluation>()
            .HasOne(e => e.Student)
            .WithMany(s => s.Evaluations)
            .HasForeignKey(e => e.StudentId);

        modelBuilder.Entity<Evaluation>()
            .HasOne(e => e.Professor)
            .WithMany(p => p.Evaluations)
            .HasForeignKey(e => e.ProfessorId);

        // Configuration for Course
        modelBuilder.Entity<Course>()
            .HasKey(c => c.CourseId);

        modelBuilder.Entity<Course>()
            .HasOne(c => c.Professor)
            .WithMany(p => p.Courses)
            .HasForeignKey(c => c.ProfessorId);

        // Configuration for Resource
        modelBuilder.Entity<Resource>()
            .HasKey(r => r.ResourceId);

        modelBuilder.Entity<Resource>()
            .HasOne<Course>()
            .WithMany()
            .HasForeignKey(r => r.CourseId);
        
        // One-to-many relationship between Course and Resource
        modelBuilder.Entity<Course>()
            .HasMany(c => c.Resources)
            .WithOne(r => r.Course)
            .HasForeignKey(r => r.CourseId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configuration for Server and Channel
        modelBuilder.Entity<Server>()
            .HasKey(s => s.ServerId);

        modelBuilder.Entity<Channel>()
            .HasKey(c => c.ChannelId);

        modelBuilder.Entity<Channel>()
            .HasOne<Server>()
            .WithMany()
            .HasForeignKey(c => c.ServerId);
        
        // One-to-many relationship between Server and Channel
        modelBuilder.Entity<Server>()
            .HasMany(s => s.Channels)
            .WithOne(c => c.Server)
            .HasForeignKey(c => c.ServerId) 
            .OnDelete(DeleteBehavior.Cascade);
        
        

        // Configuration for Message
        modelBuilder.Entity<Message>()
            .HasKey(m => m.MessageId);

        modelBuilder.Entity<Message>()
            .HasOne<User>("Sender")
            .WithMany()
            .HasForeignKey(m => m.SenderId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Message>()
            .HasOne<User>("Receiver")
            .WithMany()
            .HasForeignKey(m => m.ReceiverId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
