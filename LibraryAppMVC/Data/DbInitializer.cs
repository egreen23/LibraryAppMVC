using LibraryAppMVC.Models;
using Microsoft.SqlServer.Server;

namespace LibraryAppMVC.Data
{
    public class DbInitializer
    {
        public static void Initialize(LibraryDbContext context)
        {
            // Look for any users.
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            #region users
            var alexander = new User
            {
                Nome = "Carson",
                Cognome = "Alexander",
                DoB = DateTime.Parse("2016-09-01"),
                Tipo = 'A',
                Telefono = "0832642183"
            };

            var giorgio = new User
            {
                Nome = "Giorgio",
                Cognome = "Mastro",
                DoB = DateTime.Parse("2017-09-11"),
                Tipo = 'A',
                Telefono = "08326421456"
            };

            var mina = new User
            {
                Nome = "Maria",
                Cognome = "Giannotta",
                DoB = DateTime.Parse("2004-12-20"),
                Tipo = 'U',
                Telefono = "0832218935"
            };

            var maximo = new User
            {
                Nome = "Bob",
                Cognome = "Alexander",
                DoB = DateTime.Parse("2019-07-04"),
                Tipo = 'U',
                Telefono = "08326421125"
            };

            var users = new User[]
            {
                alexander,
                giorgio,
                mina,
                maximo
            };

            context.AddRange(users);
            #endregion

            #region authors
            var gionni = new Author
            {
                Nome = "Steve",
                Cognome = "Maximo",
                Nazionalita = "inglese",
                DoB = DateTime.Parse("2006-10-11"),
                LuogoNascita = "Birmingham",
            };

            var mario = new Author
            {
                Nome = "Harry",
                Cognome = "Terry",
                Nazionalita = "inglese",
                DoB = DateTime.Parse("1942-10-30"),
                LuogoNascita = "Londra",
                DoD = DateTime.Parse("1984-12-21")
            };

            var como = new Author
            {
                Nome = "Mario",
                Cognome = "Margiotta",
                Nazionalita = "italiana",
                DoB = DateTime.Parse("1954-05-07"),
                LuogoNascita = "Birmingham",
                DoD = DateTime.Parse("1994-12-31")

            };

            var iaia = new Author
            {
                Nome = "Thierry",
                Cognome = "Henry",
                Nazionalita = "francese",
                DoB = DateTime.Parse("1931-12-01"),
                LuogoNascita = "Birmingham",
                DoD = DateTime.Parse("1962-02-03")

            };

            var authors = new Author[]
            {
                como,
                gionni,
                iaia,
                mario
            };

            context.AddRange(authors);
            #endregion

            #region books
            var lala = new Book
            {
                Titolo = "Biancaneve",
                Quantita = 3,
                DataPubblicazione = DateTime.Parse("02-04-1999"),
                Genere = "Fantasia",
                Prezzo = 1.30m,
                Author = como
            };

            var mimi = new Book
            {
                Titolo = "Cenerentola",
                Quantita = 3,
                DataPubblicazione = DateTime.Parse("15-02-1988"),
                Genere = "Fantasia",
                Prezzo = 1.80m,
                Author = como
            };

            var gigi = new Book
            {
                Titolo = "Oxford Murderers",
                Quantita = 1,
                DataPubblicazione = DateTime.Parse("22-04-2004"),
                Genere = "Thriller",
                Prezzo = 3,
                Author = iaia
            };

            var cici = new Book
            {
                Titolo = "Orgoglio e Pregiudizio",
                Quantita = 3,
                DataPubblicazione = DateTime.Parse("13-06-1921"),
                Genere = "Romanzo",
                Prezzo = 2.50m,
                Author = mario
            };

            var books = new Book[] {
                lala,
                mimi,
                gigi,
                cici
            };

            context.AddRange(books);
            #endregion

            #region Loans
            var fatt = new Loan
            {
                Totale = 5.50m,
                DataInizio = DateTime.Parse("20-11-2023"),
                DataFine = DateTime.Parse("10-01-2024"),
                User = mina
            };

            var fatt2 = new Loan
            {
                Totale = 3.10m,
                DataInizio = DateTime.Parse("30-11-2023"),
                DataFine = DateTime.Parse("20-02-2024"),
                User = maximo
            };

            var loans = new Loan[] {
                fatt,
                fatt2
            };

            context.AddRange(loans);
            #endregion

            #region useraddress
            var ind1 = new UserAddress
            {
                Indirizzo = "via Garibaldi 9",
                Citta = "Lecce",
                Stato = "Italia",
                User = alexander
            };

            var ind2 = new UserAddress
            {
                Indirizzo = "via Trieste 31",
                Citta = "Lequile",
                Stato = "Italia",
                User = giorgio
            };

            var ind3 = new UserAddress
            {
                Indirizzo = "via Padre Diego 12",
                Citta = "Lecce",
                Stato = "Italia",
                User = mina
            };

            var ind4 = new UserAddress
            {
                Indirizzo = "via Solano 2",
                Citta = "Galatina",
                Stato = "Italia",
                User = maximo
            };

            var useraddresses = new UserAddress[] {
                ind1, ind2, ind3, ind4
            };

            context.AddRange(useraddresses);
            #endregion

            #region loanbook
            var loan11 = new LoanBook
            {
                Loan = fatt,
                Book = gigi
            };

            var loan12 = new LoanBook
            {
                Loan = fatt,
                Book = cici
            };

            var loan21 = new LoanBook
            {
                Loan = fatt2,
                Book = lala
            };

            var loan22 = new LoanBook
            {
                Loan = fatt2,
                Book = mimi
            };

            var loanbooks = new LoanBook[] {
                loan11, loan12, loan21 , loan22
            };

            context.AddRange(loanbooks);
            #endregion

            context.SaveChanges();



        }
    }
}
