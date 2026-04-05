using Microsoft.EntityFrameworkCore;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Reader> Readers { get; set; }
        public DbSet<Borrowing> Borrowings { get; set; }
        public DbSet<Staff> Staff { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Borrowing>()
                .HasOne<Book>()
                .WithMany()
                .HasForeignKey(b => b.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Borrowing>()
                .HasOne<Reader>()
                .WithMany()
                .HasForeignKey(b => b.ReaderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Book>().ToTable("Book");
            modelBuilder.Entity<Reader>().ToTable("Reader");
            modelBuilder.Entity<Borrowing>().ToTable("Borrowing");
            modelBuilder.Entity<Staff>().ToTable("Staff");

            modelBuilder.Entity<Reader>()
                .HasIndex(r => r.Email)
                .IsUnique();

            modelBuilder.Entity<Staff>()
                .HasIndex(s => s.Email)
                .IsUnique();

        }
    }
}