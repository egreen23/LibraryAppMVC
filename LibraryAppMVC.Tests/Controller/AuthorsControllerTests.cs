using LibraryAppMVC.Controllers;
using LibraryAppMVC.IServices;
using LibraryAppMVC.Models;
using LibraryAppMVC.ViewModels.Author;
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
    public class AuthorsControllerTests
    {
        [Fact]
        public async Task Index_ReturnsAViewResult_WithAListOfAuthors()
        {
            //Arrange
            var mockServiceAuthor = new Mock<IAuthorService>();
            mockServiceAuthor.Setup(serv => serv.GetAllAuthorsAsync()).ReturnsAsync(GetAuthorsTest());
            var controller = new AuthorsController(mockServiceAuthor.Object);
            //Act
            var result = await controller.Index();

            //Asserts
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IndexAuthorViewModel>(
                viewResult.ViewData.Model);
        }

        [Fact]
        public async Task Details_ReturnsAViewResult_WithAnAuthor()
        {
            //Arrange
            int testAuthorId = 1;
            var mockServiceAuthor = new Mock<IAuthorService>();
            mockServiceAuthor.Setup(serv => serv.GetByIdAsync(testAuthorId)).ReturnsAsync(GetAuthorsTest().FirstOrDefault(b => b.Id == testAuthorId));
            var controller = new AuthorsController(mockServiceAuthor.Object);
            //Act vvv
            var result = await controller.Details(testAuthorId);

            //Asserts
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Author>(
                viewResult.ViewData.Model);
            Assert.Equal("Nome", model.Nome);
            Assert.Equal("Cognome", model.Cognome);
            Assert.Equal(testAuthorId, model.Id);

        }


        [Fact]
        public async Task Details_ReturnsNotFound_WhenAuthorNull()
        {
            //Arrange
            int testAuthorId = 123;
            var mockServiceAuthor = new Mock<IAuthorService>();
            mockServiceAuthor.Setup(serv => serv.GetByIdAsync(testAuthorId)).ReturnsAsync((Author)null);
            var controller = new AuthorsController(mockServiceAuthor.Object);
            //Act 
            var result = await controller.Details(testAuthorId) as NotFoundResult;

            //Asserts
            var notFoundObjectResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, result.StatusCode);

        }

        [Fact]
        public async Task Details_ReturnsNotFound_WhenIdIsNull()
        {
            var mockServiceAuthor = new Mock<IAuthorService>();
            var controller = new AuthorsController(mockServiceAuthor.Object);

            //act
            var result = await controller.Delete(id: null) as NotFoundResult;
            //asserts

            var notFoundObjectResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, result.StatusCode);

        }

        //[Fact]
        //public async Task CreateGet_ReturnsCreateAuthorFormView()
        //{
        //    var mockServiceAuthor = new Mock<IAuthorService>();
        //    var controller = new AuthorsController(mockServiceAuthor.Object);
        //    //act
        //    var result = controller.Create();

        //    //Assert
        //    var viewResult = Assert.IsType<ViewResult>(result);
        //    var model = Assert.IsAssignableFrom<CreateAuthorViewModel>(
        //        viewResult.ViewData.Model);
        //}

        [Fact]
        public async Task CreatePost_ReturnsARedirectAndCreateAnAuthor_WhenModelStateIsValid()
        {
            var mockServiceAuthor = new Mock<IAuthorService>();
            mockServiceAuthor.Setup(serv => serv.CreateAsync(It.IsAny<CreateAuthorViewModel>())).Verifiable();
            var controller = new AuthorsController(mockServiceAuthor.Object);
            var author = new CreateAuthorViewModel
            {
                isAlive = true,
            };
            //act
            var result = await controller.Create(author);
            //asserts

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            mockServiceAuthor.Verify();
        }

        [Fact]
        public async Task CreatePost_ReturnsAView_WhenModelStateIsInvalid()
        {
            var mockServiceAuthor = new Mock<IAuthorService>();
            var controller = new AuthorsController(mockServiceAuthor.Object);
            controller.ModelState.AddModelError("Nome", "Required");
            var author = new CreateAuthorViewModel
            {
                isAlive = true,
            };
            //act
            var result = await controller.Create(author);
            //asserts

            var viewResult = Assert.IsType<ViewResult>(result);

        }

        [Fact]
        public async Task CreatePost_ReturnsARedirect_WhenDateValuesAreWrong()
        {
            var mockServiceAuthor = new Mock<IAuthorService>();
            var controller = new AuthorsController(mockServiceAuthor.Object);
            var author = new CreateAuthorViewModel
            {
                isAlive = false,
                DoB = DateTime.UtcNow.AddDays(1),
                DoD = DateTime.UtcNow,
            };
            //act
            var result = await controller.Create(author);
            //asserts

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Create", redirectToActionResult.ActionName);

        }

        [Fact]
        public async Task EditGet_ReturnsNotFound_WhenIdIsNull()
        {
            //var id = 123;
            var mockServiceAuthor = new Mock<IAuthorService>();
            var controller = new AuthorsController(mockServiceAuthor.Object);

            //act
            var result = await controller.Edit(id: null) as NotFoundResult;
            //asserts
            var notFoundObjectResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, result.StatusCode);

        }

        [Fact]
        public async Task EditGet_ReturnsNotFound_WhenAuthorIsNull()
        {
            int testAuthorId = 123;
            var mockServiceAuthor = new Mock<IAuthorService>();
            mockServiceAuthor.Setup(serv => serv.GetByIdAsync(testAuthorId)).ReturnsAsync((Author)null);
            var controller = new AuthorsController(mockServiceAuthor.Object);

            //act
            var result = await controller.Edit(testAuthorId) as NotFoundResult;
            //asserts

            var notFoundObjectResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async Task EditGet_ReturnsAViewResult_WithAnAuthor()
        {
            //Arrange
            int testAuthorId = 1;
            var mockServiceAuthor = new Mock<IAuthorService>();
            mockServiceAuthor.Setup(serv => serv.GetByIdAsync(testAuthorId)).ReturnsAsync(GetAuthorsTest().FirstOrDefault(b => b.Id == testAuthorId));
            var controller = new AuthorsController(mockServiceAuthor.Object);
            //Act vvv
            var result = await controller.Edit(testAuthorId);

            //Asserts
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<UpdateAuthorViewModel>(
                viewResult.ViewData.Model);
            Assert.Equal("Nome", model.Nome);
            Assert.Equal("Cognome", model.Cognome);
            Assert.Equal(testAuthorId, model.Id);

        }

        [Fact]
        public async Task EditPost_ReturnsARedirectionToAction_WhenModelStateIsValid()
        {
            var mockServiceAuthor = new Mock<IAuthorService>();
            var authorVM = new UpdateAuthorViewModel
            {
                Id = 1,
                Nome = "a",
                Cognome = "b",
                Nazionalita = "c",
                DoB = DateTime.Now,
                LuogoNascita = "d",
                isAlive = true
            };
            var author = new Author()
            {
                Id = authorVM.Id,
                Nome = authorVM.Nome,
                Cognome = authorVM.Cognome,
                Nazionalita = authorVM.Nazionalita,
                DoB = authorVM.DoB,
                LuogoNascita = authorVM.LuogoNascita,
                DoD = null
            };
            mockServiceAuthor.Setup(serv => serv.UpdateAsync(authorVM.Id, author));

            var controller = new AuthorsController(mockServiceAuthor.Object);

            //Act 
            var result = await controller.Edit(authorVM);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            mockServiceAuthor.Verify();
        }
        [Fact]
        public async Task EditPost_ReturnsAView_WhenModelStateIsInvalid()
        {
            var mockServiceAuthor = new Mock<IAuthorService>();

            var controller = new AuthorsController(mockServiceAuthor.Object); 
            controller.ModelState.AddModelError("Nome", "Required");
            //act
            var result = await controller.Edit(new UpdateAuthorViewModel());
            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        //[Fact]
        //public async Task EditPost_ReturnsNotFound_WhenHandlesException()
        //{

        //    var mockServiceAuthor = new Mock<IAuthorService>();
        //    UpdateAuthorViewModel authorVM = new UpdateAuthorViewModel
        //    {
        //        Id = 1,
        //        Nome = "a",
        //        Cognome = "b",
        //        Nazionalita = "c",
        //        DoB = DateTime.Now,
        //        LuogoNascita = "d",
        //        isAlive = true
        //    };
        //    var author = new Author()
        //    {
        //        Id = authorVM.Id,
        //        Nome = authorVM.Nome,
        //        Cognome = authorVM.Cognome,
        //        Nazionalita = authorVM.Nazionalita,
        //        DoB = authorVM.DoB,
        //        LuogoNascita = authorVM.LuogoNascita,
        //        DoD = null
        //    };
        //    mockServiceAuthor.Setup(serv => serv.UpdateAsync(authorVM.Id, author)).ThrowsAsync(new DbUpdateConcurrencyException());
        //    mockServiceAuthor.Setup(serv => serv.AuthorExists(authorVM.Id)).ReturnsAsync(false);


        //    var controller = new AuthorsController(mockServiceAuthor.Object);

        //    // Optionally clear ModelState before calling the controller method to avoid early return due to validation errors
        //    controller.ModelState.Clear();
        //    //act
        //    var result = await controller.Edit(authorVM);

        //    //var result = await controller.Edit(authorVM);
        //    //Assert
        //    var notFoundObjectResult = Assert.IsType<NotFoundResult>(result);
        //    Assert.Equal(404, notFoundObjectResult.StatusCode);
        //    mockServiceAuthor.Verify(serv => serv.UpdateAsync(authorVM.Id, author), Times.Once);  // Ensure UpdateAsync was called once
        //    mockServiceAuthor.Verify(serv => serv.AuthorExists(authorVM.Id), Times.Once);  // Ensure AuthorExists was called once
        //}

        [Fact]
        public async Task DeleteGet_ReturnsNotFound_WhenIdIsNull()
        {
            var mockServiceAuthor = new Mock<IAuthorService>();
            var controller = new AuthorsController(mockServiceAuthor.Object);

            //act
            var result = await controller.Delete(id: null) as NotFoundResult;
            //asserts

            var notFoundObjectResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, result.StatusCode);

        }

        [Fact]
        public async Task DeleteGet_ReturnsNotFound_WhenAuthorIsNull()
        {
            int testAuthorId = 123;
            var mockServiceAuthor = new Mock<IAuthorService>();
            mockServiceAuthor.Setup(serv => serv.GetByIdAsync(testAuthorId)).ReturnsAsync((Author)null);
            var controller = new AuthorsController(mockServiceAuthor.Object);

            //act
            var result = await controller.Delete(testAuthorId) as NotFoundResult;
            //asserts

            var notFoundObjectResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async Task DeleteGet_ReturnsAView_WhenAuthorFound()
        {
            int testAuthorId = 1;
            var mockServiceAuthor = new Mock<IAuthorService>();
            mockServiceAuthor.Setup(serv => serv.GetByIdAsync(testAuthorId)).ReturnsAsync(GetAuthorsTest().FirstOrDefault(b => b.Id == testAuthorId));
            var controller = new AuthorsController(mockServiceAuthor.Object);
            //Act vvv
            var result = await controller.Delete(testAuthorId);

            //Asserts
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Author>(
                viewResult.ViewData.Model);
            Assert.Equal("Nome", model.Nome);
            Assert.Equal(testAuthorId, model.Id);
        }

        [Fact]
        public async Task DeletePost_ReturnsARedirection()
        {
            int testAuthorId = 1;
            var mockServiceAuthor = new Mock<IAuthorService>();

            mockServiceAuthor.Setup(serv => serv.DeleteAsync(testAuthorId)).Verifiable();
            var controller = new AuthorsController(mockServiceAuthor.Object);
            //act
            var result = await controller.DeleteConfirmed(testAuthorId);

            //Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            mockServiceAuthor.Verify();
        }
        private List<Author> GetAuthorsTest()
        {
            var authors = new List<Author>();
            authors.Add(new Author
            {
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
    }
}
