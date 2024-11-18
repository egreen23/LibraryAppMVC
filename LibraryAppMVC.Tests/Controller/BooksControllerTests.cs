using LibraryAppMVC.Controllers;
using LibraryAppMVC.IServices;
using LibraryAppMVC.Models;
using LibraryAppMVC.ViewModels.Book;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryAppMVC.Tests.Controller
{
    public class BooksControllerTests
    {
        [Fact]
        public async Task Index_ReturnsAViewResult_WithAListOfBooks()
        {
            //Arrange
            var mockServiceBook = new Mock<IBookService>();
            var mockServiceAuthor = new Mock<IAuthorService>();
            mockServiceBook.Setup(serv => serv.GetAllAsync()).ReturnsAsync(GetTestBooks());
            var controller = new BooksController(mockServiceAuthor.Object,mockServiceBook.Object); 
            //Act
            var result = await controller.Index();

            //Asserts
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<IndexBookViewModel>>(
                viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task Details_ReturnsAViewResult_WithABook()
        {
            //Arrange
            int testBookId = 1;
            var mockServiceBook = new Mock<IBookService>();
            var mockServiceAuthor = new Mock<IAuthorService>();
            mockServiceBook.Setup(serv => serv.GetByIdAsync(testBookId)).ReturnsAsync(GetTestBooks().FirstOrDefault(b => b.Id == testBookId));
            var controller = new BooksController(mockServiceAuthor.Object, mockServiceBook.Object);
            //Act vvv
            var result = await controller.Details(testBookId);

            //Asserts
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<IndexBookViewModel>(
                viewResult.ViewData.Model);
            Assert.Equal("Titolo 1", model.Titolo);
            Assert.Equal(1, model.Quantita);
            Assert.Equal(testBookId, model.Id);

        }

        [Fact]
        public async Task Details_ReturnsNotFoundWhenBookNull()
        {
            //Arrange
            int testBookId = 123;
            var mockServiceBook = new Mock<IBookService>();
            var mockServiceAuthor = new Mock<IAuthorService>();
            mockServiceBook.Setup(serv => serv.GetByIdAsync(testBookId)).ReturnsAsync((Book)null);
            var controller = new BooksController(mockServiceAuthor.Object, mockServiceBook.Object);
            //Act 
            var result = await controller.Details(testBookId) as NotFoundResult;

            //Asserts
            var notFoundObjectResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, result.StatusCode);

        }

        [Fact]
        public async Task CreateGet_ReturnsCreateBookFormView()
        {
            var mockServiceBook = new Mock<IBookService>();
            var mockServiceAuthor = new Mock<IAuthorService>();
            mockServiceAuthor.Setup(serv => serv.GetAllAuthorsAsync()).ReturnsAsync(GetAuthorsTest());
            var controller = new BooksController(mockServiceAuthor.Object, mockServiceBook.Object);
            //act
            var result = await controller.Create();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<CreateBookViewModel>(
                viewResult.ViewData.Model);
            Assert.Equal(2, model.AuthorsList!.Count());
        }

        [Fact]
        public async Task CreatePost_ReturnsARedirectAndCreateABook_WhenModelStateIsValid()
        {
            var mockServiceBook = new Mock<IBookService>();
            var mockServiceAuthor = new Mock<IAuthorService>();
            var book = new Book
            {
                Id = 1,
                Titolo = "Titolo 1",
                Quantita = 1,
                DataPubblicazione = DateTime.Now,
                Genere = "fantasy",
                Prezzo = 1,
                AuthorId = 1,
                Author = new Author
                {
                    Id = 1,
                    Nome = "nome",
                    Cognome = "cognome"
                }
            };
            mockServiceBook.Setup(serv => serv.CreateAsync(book)).Verifiable();
            var controller = new BooksController(mockServiceAuthor.Object, mockServiceBook.Object);
            //act
            var result = await controller.Create(book);

            //Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            mockServiceBook.Verify();
  
        }

        [Fact]
        public async Task CreatePost_ReturnsAView_WhenModelStateIsInvalid()
        {
            var mockServiceBook = new Mock<IBookService>();
            var mockServiceAuthor = new Mock<IAuthorService>();
            var book = new Book
            {
                Id = 1,
                Titolo = "Titolo 1",
                Quantita = 1,
                DataPubblicazione = DateTime.Now,
                Genere = "fantasy",
                Prezzo = 1,
                AuthorId = 1,
                Author = new Author
                {
                    Id = 1,
                    Nome = "nome",
                    Cognome = "cognome"
                }
            };
            var controller = new BooksController(mockServiceAuthor.Object, mockServiceBook.Object);
            controller.ModelState.AddModelError("Titolo", "Required");
            //act
            var result = await controller.Create(book);
            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);

        }
        [Fact]
        public async Task EditGet_ReturnsARedirect_WhenIdIsNull()
        {
            //var id = 123;
            var mockServiceBook = new Mock<IBookService>();
            var mockServiceAuthor = new Mock<IAuthorService>();
            var controller = new BooksController(mockServiceAuthor.Object, mockServiceBook.Object);

            //act
            var result = await controller.Edit(id: null);
            //asserts
            var redirectToActionResult =
                    Assert.IsType<RedirectToActionResult>(result);
            //Assert.Equal("Books", redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);            

        }
        [Fact]
        public async Task EditGet_ReturnsNotFound_WhenBookIsNull()
        {
            int testBookId = 123; 
            var mockServiceBook = new Mock<IBookService>();
            var mockServiceAuthor = new Mock<IAuthorService>();
            mockServiceAuthor.Setup(serv => serv.GetAllAuthorsAsync()).ReturnsAsync(GetAuthorsTest());
            mockServiceBook.Setup(serv => serv.GetByIdAsync(testBookId)).ReturnsAsync((Book)null);


            var controller = new BooksController(mockServiceAuthor.Object, mockServiceBook.Object);

            //Act 
            var result = await controller.Details(testBookId) as NotFoundResult;

            //Asserts
            var notFoundObjectResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async Task EditGet_ReturnsAViewWithBookDetails()
        {
            int testBookId = 1;
            var mockServiceBook = new Mock<IBookService>();
            var mockServiceAuthor = new Mock<IAuthorService>();
            mockServiceAuthor.Setup(serv => serv.GetAllAuthorsAsync()).ReturnsAsync(GetAuthorsTest());
            mockServiceBook.Setup(serv => serv.GetByIdAsync(testBookId)).ReturnsAsync(GetTestBooks().FirstOrDefault(b => b.Id == testBookId));
            var controller = new BooksController(mockServiceAuthor.Object, mockServiceBook.Object);

            //Act 
            var result = await controller.Details(testBookId);
            //Asserts
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<IndexBookViewModel>(
                viewResult.ViewData.Model);
            Assert.Equal("Titolo 1", model.Titolo);
            Assert.Equal(1, model.Quantita);
            Assert.Equal(testBookId, model.Id);
        }

        [Fact]
        public async Task EditPost_ReturnsARedirectionToAction_WhenModelStateIsValid()
        {
            var mockServiceBook = new Mock<IBookService>();
            var mockServiceAuthor = new Mock<IAuthorService>();
            var book = new Book
            {
                Id = 1,
                Titolo = "Titolo 1",
                Quantita = 1,
                DataPubblicazione = DateTime.Now,
                Genere = "fantasy",
                Prezzo = 1,
                AuthorId = 1,
                Author = new Author
                {
                    Id = 1,
                    Nome = "nome",
                    Cognome = "cognome"
                }
            };
            mockServiceBook.Setup(serv => serv.UpdateAsync(book.Id,book)).Verifiable();

            var controller = new BooksController(mockServiceAuthor.Object, mockServiceBook.Object);

            //Act 
            var result = await controller.Edit(book.Id, book);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            mockServiceBook.Verify();
        }

        [Fact]
        public async Task EditPost_ReturnsAView_WhenModelStateIsInvalid()
        {
            var mockServiceBook = new Mock<IBookService>();
            var mockServiceAuthor = new Mock<IAuthorService>();
            var book = new Book
            {
                Id = 1,
                Titolo = "Titolo 1",
                Quantita = 1,
                DataPubblicazione = DateTime.Now,
                Genere = "fantasy",
                Prezzo = 1,
                AuthorId = 1,
                Author = new Author
                {
                    Id = 1,
                    Nome = "nome",
                    Cognome = "cognome"
                }
            };
            var controller = new BooksController(mockServiceAuthor.Object, mockServiceBook.Object);
            controller.ModelState.AddModelError("Titolo", "Required");
            //act
            var result = await controller.Edit(book.Id, book);
            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task EditPost_ReturnsNotFound_WhenHandlesException()
        {
            var mockServiceBook = new Mock<IBookService>();
            var mockServiceAuthor = new Mock<IAuthorService>();
            var book = new Book
            {
                Id = 1,
                Titolo = "Titolo 1",
                Quantita = 1,
                DataPubblicazione = DateTime.Now,
                Genere = "fantasy",
                Prezzo = 1,
                AuthorId = 1,
                Author = new Author
                {
                    Id = 1,
                    Nome = "nome",
                    Cognome = "cognome"
                }
            };
            mockServiceBook.Setup(serv => serv.UpdateAsync(book.Id, book)).ThrowsAsync(new DbUpdateConcurrencyException());
            mockServiceBook.Setup(serv => serv.BookExists(book.Id)).ReturnsAsync(false);

            var controller = new BooksController(mockServiceAuthor.Object, mockServiceBook.Object);
            //act
            var result = await controller.Edit(book.Id, book) as NotFoundResult;
            //Assert
            var notFoundObjectResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async Task EditPost_ReturnsContent_WhenHandlesException()
        {
            var mockServiceBook = new Mock<IBookService>();
            var mockServiceAuthor = new Mock<IAuthorService>();
            var book = new Book
            {
                Id = 1,
                Titolo = "Titolo 1",
                Quantita = 1,
                DataPubblicazione = DateTime.Now,
                Genere = "fantasy",
                Prezzo = 1,
                AuthorId = 1,
                Author = new Author
                {
                    Id = 1,
                    Nome = "nome",
                    Cognome = "cognome"
                }
            };
            mockServiceBook.Setup(serv => serv.UpdateAsync(book.Id, book)).ThrowsAsync(new DbUpdateConcurrencyException());
            mockServiceBook.Setup(serv => serv.BookExists(book.Id)).ReturnsAsync(true);

            var controller = new BooksController(mockServiceAuthor.Object, mockServiceBook.Object);
            //act
            var result = await controller.Edit(book.Id, book);
            //Assert
            var contentResult = Assert.IsType<ContentResult>(result);
            Assert.Equal("conflict", contentResult.Content);
        }

        [Fact]
        public async Task DeleteGet_ReturnsNotFound_WhenIdIsNull()
        {
            var mockServiceBook = new Mock<IBookService>();
            var mockServiceAuthor = new Mock<IAuthorService>();
            var controller = new BooksController(mockServiceAuthor.Object, mockServiceBook.Object);

            //act
            var result = await controller.Delete(id: null) as NotFoundResult;
            //asserts

            var notFoundObjectResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, result.StatusCode);
            
        }

        [Fact]
        public async Task DeleteGet_ReturnsNotFound_WhenBookIsNull()
        {
            int testBookId = 123;
            var mockServiceBook = new Mock<IBookService>();
            var mockServiceAuthor = new Mock<IAuthorService>();
            mockServiceBook.Setup(serv => serv.GetByIdAsync(testBookId)).ReturnsAsync((Book)null);
            var controller = new BooksController(mockServiceAuthor.Object, mockServiceBook.Object);

            //act
            var result = await controller.Delete(testBookId) as NotFoundResult;
            //asserts

            var notFoundObjectResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async Task DeleteGet_ReturnsAView_WhenBookFound()
        {
            int testBookId = 1;
            var mockServiceBook = new Mock<IBookService>();
            var mockServiceAuthor = new Mock<IAuthorService>();
            mockServiceBook.Setup(serv => serv.GetByIdAsync(testBookId)).ReturnsAsync(GetTestBooks().FirstOrDefault(b => b.Id == testBookId));
            var controller = new BooksController(mockServiceAuthor.Object, mockServiceBook.Object);
            //Act vvv
            var result = await controller.Delete(testBookId);

            //Asserts
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Book>(
                viewResult.ViewData.Model);
            Assert.Equal("Titolo 1", model.Titolo);
            Assert.Equal(1, model.Quantita);
            Assert.Equal(testBookId, model.Id);
        }

        [Fact]
        public async Task DeletePost_ReturnsARedirection()
        {
            int testBookId = 1;
            var mockServiceBook = new Mock<IBookService>();
            var mockServiceAuthor = new Mock<IAuthorService>();
            
            mockServiceBook.Setup(serv => serv.DeleteAsync(testBookId)).Verifiable();
            var controller = new BooksController(mockServiceAuthor.Object, mockServiceBook.Object);
            //act
            var result = await controller.DeleteConfirmed(testBookId);

            //Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            mockServiceBook.Verify();
        }
        private List<Author> GetAuthorsTest()
        {
            var authors = new List<Author>();
            authors.Add(new Author {
                Id = 1,
                Nome = "Nome",
                Cognome = "Cognome",
                Nazionalita = "italia",
                DoB = DateTime.Now,
                LuogoNascita = "luogo"

            });

            authors.Add(new Author
            {
                Id = 2,
                Nome = "Nome2",
                Cognome = "Cognome2",
                Nazionalita = "italia2",
                DoB = DateTime.Now,
                LuogoNascita = "luogo2"

            });

            return authors;
        }

        private List<Book> GetTestBooks()
        {
            var books = new List<Book>();
            books.Add(new Book {
                Id = 1,
                Titolo = "Titolo 1",
                Quantita = 1,
                DataPubblicazione = DateTime.Now,
                Genere = "fantasy",
                Prezzo = 1,
                AuthorId = 1,
                Author = new Author { 
                    Id = 1,
                    Nome = "nome",
                    Cognome = "cognome"
                }
            });

            books.Add(new Book
            {
                Id = 2,
                Titolo = "Titolo 2",
                Quantita = 1,
                DataPubblicazione = DateTime.Now,
                Genere = "fantasy2",
                Prezzo = 1.50m,
                AuthorId = 1,
                Author = new Author
                {
                    Id = 1,
                    Nome = "nome",
                    Cognome = "cognome"
                }
            });

            return books;
        }
    }
}
