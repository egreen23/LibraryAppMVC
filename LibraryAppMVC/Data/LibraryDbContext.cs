using LibraryAppMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibraryAppMVC.Data
{
    public class LibraryDbContext : IdentityDbContext<ApplicationUser>
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> dbContextOptions) : base(dbContextOptions) 
        {
            
        }

        //tabelle
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<LoanBook> LoanBooks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //rinomina tabelle nel db in modo siano scirtte al singolare
            modelBuilder.Entity<Review>().ToTable(nameof(Review));
            modelBuilder.Entity<Author>().ToTable(nameof(Author));
            modelBuilder.Entity<Book>().ToTable(nameof(Book));
            modelBuilder.Entity<ApplicationUser>().ToTable(nameof(ApplicationUser));
            modelBuilder.Entity<Loan>().ToTable(nameof(Loan));
            modelBuilder.Entity<LoanBook>().ToTable(nameof(LoanBook));

            modelBuilder.Entity<Author>().HasData(
                new Author
                {
                    Id = 1,
                    Nome = "Steve",
                    Cognome = "Maximo",
                    Nazionalita = "inglese",
                    DoB = DateTime.Parse("2006-10-11"),
                    LuogoNascita = "Birmingham",
                },

                new Author
                {
                    Id = 2,
                    Nome = "Harry",
                    Cognome = "Terry",
                    Nazionalita = "inglese",
                    DoB = DateTime.Parse("1942-10-30"),
                    LuogoNascita = "Londra",
                    DoD = DateTime.Parse("1984-12-21")
                },

                new Author
                {
                    Id = 3,
                    Nome = "Mario",
                    Cognome = "Margiotta",
                    Nazionalita = "italiana",
                    DoB = DateTime.Parse("1954-05-07"),
                    LuogoNascita = "Birmingham",
                    DoD = DateTime.Parse("1994-12-31")

                },

                new Author
                {
                    Id = 4,
                    Nome = "Thierry",
                    Cognome = "Henry",
                    Nazionalita = "francese",
                    DoB = DateTime.Parse("1931-12-01"),
                    LuogoNascita = "Birmingham",
                    DoD = DateTime.Parse("1962-02-03")
                },
                new Author
                {
                    Id = 48,
                    Nome = "Marco",
                    Cognome = "Massafra",
                    Nazionalita = "italiano",
                    DoB = DateTime.Parse("1945-02-01"),
                    LuogoNascita = "Firenze",
                    DoD = DateTime.Parse("1988-02-03")
                },
                new Author
                {
                    Id = 49,
                    Nome = "Giovanni",
                    Cognome = "Quarta",
                    Nazionalita = "italiano",
                    DoB = DateTime.Parse("1966-02-01"),
                    LuogoNascita = "Torino",
                    DoD = DateTime.Parse("1999-02-03")
                }
                );

            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = 1,
                    Titolo = "Biancaneve",
                    Quantita = 3,
                    DataPubblicazione = DateTime.Parse("02-04-1999"),
                    Genere = "Fantasia",
                    Prezzo = 1.30m,
                    AuthorId = 1
                },
                new Book
                {
                    Id = 2,
                    Titolo = "Cenerentola",
                    Quantita = 3,
                    DataPubblicazione = DateTime.Parse("15-02-1988"),
                    Genere = "Fantasia",
                    Prezzo = 1.80m,
                    AuthorId = 1
                },
                new Book
                {
                    Id = 3,
                    Titolo = "Oxford Murderers",
                    Quantita = 1,
                    DataPubblicazione = DateTime.Parse("22-04-2004"),
                    Genere = "Thriller",
                    Prezzo = 3,
                    AuthorId = 2
                },
                new Book
                {
                    Id = 4,
                    Titolo = "Orgoglio e Pregiudizio",
                    Quantita = 3,
                    DataPubblicazione = DateTime.Parse("13-06-1921"),
                    Genere = "Romanzo",
                    Prezzo = 2.50m,
                    AuthorId = 3
                }
                );
        }
    }
}
