using LibraryAppMVC.IServices;
using LibraryAppMVC.Models;
using LibraryAppMVC.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LibraryAppMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBookService _bookService;
        private readonly IAuthorService _authorService;

        public HomeController(ILogger<HomeController> logger, IAuthorService authorService, IBookService bookService)
        {
            _logger = logger;
            _authorService = authorService;
            _bookService = bookService;
        }

        public async Task<IActionResult> Index()
        {
            var dashBooks = await _bookService.GetDashboardBooks(3);

            var dashAuthors = await _authorService.GetDashboardAuthors(3);

            var dashBooksVM = new List<BookDashboard>();
            var dashAuthorsVM = new List<AuthorDashboard>();

            foreach (var dashBook in dashBooks)
            {
                var book = new BookDashboard
                {
                    Id = dashBook.Id,
                    Titolo = dashBook.Titolo,
                    Genere = dashBook.Genere,
                    AuthorFullName = dashBook.Author.FullName
                };

                dashBooksVM.Add(book);
            }

            foreach(var author in dashAuthors)
            {
                var a = new AuthorDashboard
                {
                    Id = author.Id,
                    Nome = author.Nome,
                    Cognome = author.Cognome
                };

                dashAuthorsVM.Add(a);
            }

            var modelVM = new HomeViewModel
            {
                books = dashBooksVM,
                authors = dashAuthorsVM
            };
            return View(modelVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
