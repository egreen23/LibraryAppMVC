using LibraryAppMVC.Dtos;
using LibraryAppMVC.Models;

namespace LibraryAppMVC.Mappers
{
    public static class BookMappers
    {
        public static BookDto toBookDto(this Book bookModel )
        {
            return new BookDto
            {
                Id = bookModel.Id,
                Titolo = bookModel.Titolo,
                Quantita = bookModel.Quantita,
                DataPubblicazione = bookModel.DataPubblicazione,
                Genere = bookModel.Genere,
                Prezzo = bookModel.Prezzo,
                AuthorFullname = bookModel.Author.Nome + " " + bookModel.Author.Cognome,
                AuthorId = bookModel.AuthorId
            };


        }
    }
}
