using LibraryAppMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAppMVC.Data
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> dbContextOptions) : base(dbContextOptions) 
        {
            
        }

        //tabelle
        public DbSet<User> Users { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<LoanBook> LoanBooks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //rinomina tabelle nel db in modo siano scirtte al singolare
            modelBuilder.Entity<User>().ToTable(nameof(User));
            modelBuilder.Entity<UserAddress>().ToTable(nameof(UserAddress));
            modelBuilder.Entity<Author>().ToTable(nameof(Author));
            modelBuilder.Entity<Book>().ToTable(nameof(Book));
            modelBuilder.Entity<Loan>().ToTable(nameof(Loan));
            modelBuilder.Entity<LoanBook>().ToTable(nameof(LoanBook));
        }
    }
}
