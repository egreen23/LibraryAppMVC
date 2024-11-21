using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryAppMVC.Data;
using LibraryAppMVC.Models;
using LibraryAppMVC.IServices;
using LibraryAppMVC.ViewModels.Loan;
using Microsoft.AspNetCore.Identity;
using LibraryAppMVC.Utility;
using Microsoft.AspNetCore.Authorization;
using static NuGet.Packaging.PackagingConstants;
using LibraryAppMVC.ViewModels.Book;
using static System.Reflection.Metadata.BlobBuilder;
using Microsoft.IdentityModel.Tokens;

namespace LibraryAppMVC.Controllers
{
    [Authorize(Roles = SD.Role_User)]
    public class LoansController : Controller
    {
        private readonly ILoanService _loanService;
        private readonly IBookService _bookService;
        private readonly UserManager<ApplicationUser> _userManager;

        
        public LoansController(ILoanService loanService, IBookService bookService, UserManager<ApplicationUser> userManager)
        {
            _loanService = loanService;
            _bookService = bookService;
            _userManager = userManager;
        }

        // GET: Loans
        
        public async Task<IActionResult> Index()
        {
            string UserId = _userManager.GetUserId(User);
            var loans = await _loanService.GetAllByUserIdAsync(UserId);

            return View(loans);
        }

        // GET: Loans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //List<DetailLoanViewModel> loandetails = await _loanService.GetByIdAsync((int)id);
            var loandetails = await _loanService.GetByIdAsync((int)id);

            if (loandetails == null)
            {
                return NotFound();
            }

            return View(loandetails);
        }
        public async Task<IActionResult> Create()
        {

            var model = HttpContext.Session.Get<CreateLoanViewModel>("CreateLoanViewModel");
            if (model == null)
            {
                var allbooks = await _bookService.GetAllAsync();
                
                List<IndexBookViewModel> booklist = new List<IndexBookViewModel>();
                foreach (var b in allbooks)
                {
                    var item = new IndexBookViewModel
                    {
                        Titolo = b.Titolo,
                        Quantita = b.Quantita,
                        DataPubblicazione = b.DataPubblicazione,
                        Genere = b.Genere,
                        Prezzo = b.Prezzo,
                        Id = b.Id,
                        AuthorFullname = b.Author.FullName
                    };
                    if (item.Quantita == 0) item.OutofStock = true;
                    booklist.Add(item);
                }

                //var cartQuery = from book in booklist
                //                where term == "" || book.Titolo.ToLowerInvariant().StartsWith(term)
                //                select book;


                model = new CreateLoanViewModel
                {
                    BooksAddedToCart = new List<LoanItemViewModel>(),
                    //Books = booklist.Where(b => b.Quantita > 0)
                    Books = booklist
        
                };

                HttpContext.Session.Set<CreateLoanViewModel>("CreateLoanViewModel", model);
                
            }

            

            return View(model);

        }
        [HttpPost]
        public async Task<IActionResult> Search(string term)
        {
            term = string.IsNullOrEmpty(term) ? "" : term.ToLowerInvariant();

            var model = HttpContext.Session.Get<CreateLoanViewModel>("CreateLoanViewModel");
            if (model == null)
            {
                var allbooks = await _bookService.GetAllAsync();

                List<IndexBookViewModel> booklist = new List<IndexBookViewModel>();
                foreach (var b in allbooks)
                {
                    var item = new IndexBookViewModel
                    {
                        Titolo = b.Titolo,
                        Quantita = b.Quantita,
                        DataPubblicazione = b.DataPubblicazione,
                        Genere = b.Genere,
                        Prezzo = b.Prezzo,
                        Id = b.Id,
                        AuthorFullname = b.Author.FullName
                    };
                    if (item.Quantita == 0) item.OutofStock = true;
                    booklist.Add(item);
                }

                //var cartQuery = from book in booklist
                //                where term == "" || book.Titolo.ToLowerInvariant().StartsWith(term)
                //                select book;


                model = new CreateLoanViewModel
                {
                    BooksAddedToCart = new List<LoanItemViewModel>(),
                    //Books = booklist.Where(b => b.Quantita > 0)
                    Books = booklist

                };

                HttpContext.Session.Set<CreateLoanViewModel>("CreateLoanViewModel", model);

            }
            if (!term.IsNullOrEmpty())
            {
                var cartQuery = from book in model.Books
                                where term == "" || book.Titolo.ToLowerInvariant().StartsWith(term!)
                                select book;
                model.Books = cartQuery;
            } else
            {
                var allbooks = await _bookService.GetAllAsync();

                List<IndexBookViewModel> booklist = new List<IndexBookViewModel>();
                foreach (var b in allbooks)
                {
                    var item = new IndexBookViewModel
                    {
                        Titolo = b.Titolo,
                        Quantita = b.Quantita,
                        DataPubblicazione = b.DataPubblicazione,
                        Genere = b.Genere,
                        Prezzo = b.Prezzo,
                        Id = b.Id,
                        AuthorFullname = b.Author.FullName
                    };
                    if (item.Quantita == 0) item.OutofStock = true;
                    booklist.Add(item);
                }
                model.Books = booklist;
            }

            HttpContext.Session.Set<CreateLoanViewModel>("CreateLoanViewModel", model);



            return View("Create", model);
        }



        [HttpPost]
        public async Task<IActionResult> AddBooktoCart(int bookId)
        {
            var book = await _bookService.GetByIdAsync(bookId);
            if (book == null)
            {
                return NotFound();
            }

            // Retrieve or create a CreateLoanViewModel from session or other state management
            var model = HttpContext.Session.Get<CreateLoanViewModel>("CreateLoanViewModel");
            if (model == null)
            {
                var allbooks = await _bookService.GetAllAsync();
                List<IndexBookViewModel> booklist = new List<IndexBookViewModel>();
                foreach (var b in allbooks)
                {
                    var item = new IndexBookViewModel
                    {
                        Titolo = b.Titolo,
                        Quantita = b.Quantita,
                        DataPubblicazione = b.DataPubblicazione,
                        Genere = b.Genere,
                        Prezzo = b.Prezzo,
                        Id = b.Id,
                        AuthorFullname = b.Author.FullName
                    };
                    if (item.Quantita == 0) item.OutofStock = true;
                    booklist.Add(item);
                }
                model = new CreateLoanViewModel
                {
                    BooksAddedToCart = new List<LoanItemViewModel>(),
                    //Books = booklist.Where(b => b.Quantita > 0)
                    Books = booklist
                };
               

            }

            // Check if the product is already in the order
            var existingBoook = model.BooksAddedToCart.FirstOrDefault(oi => oi.BookId == bookId);

            if (existingBoook == null)
            {
                model.BooksAddedToCart.Add(new LoanItemViewModel
                {
                    BookId = book.Id,
                    Titolo = book.Titolo,
                    Prezzo = book.Prezzo,
                });
            }

            model.Totale = model.BooksAddedToCart.Sum(b => b.Prezzo);

            // Save updated OrderViewModel to session
            HttpContext.Session.Set("CreateLoanViewModel", model);

            // Redirect back to Create to show updated order items
            return RedirectToAction("Create", model);

        }

       
        [HttpGet]
        public async Task<IActionResult> Cart()
        {

            // Retrieve the OrderViewModel from session or other state management
            var model = HttpContext.Session.Get<CreateLoanViewModel>("CreateLoanViewModel");

            if (model == null || model.BooksAddedToCart.Count == 0)
            {
                return RedirectToAction("Create");
            }

            return View(model);
        }

      
        public async Task<IActionResult> RemoveFromCart(int bookid)
        {
            var model = HttpContext.Session.Get<CreateLoanViewModel>("CreateLoanViewModel");
            var bookremoved = model.BooksAddedToCart.Single(b => b.BookId == bookid);
            model.BooksAddedToCart.Remove(bookremoved);
            model.Totale -= bookremoved.Prezzo;
            HttpContext.Session.Set("CreateLoanViewModel", model);
            if (model == null || model.BooksAddedToCart.Count == 0)
            {
                return RedirectToAction("Create",model);
            }
            else
            {
                return RedirectToAction("Cart",model);
            }
        }


        [HttpPost]
        public async Task<IActionResult> PlaceOrder()
        {
            var model = HttpContext.Session.Get<CreateLoanViewModel>("CreateLoanViewModel");
            if (model == null || model.BooksAddedToCart.Count == 0)
            {
                return RedirectToAction("Create");
            }
            int result = DateTime.Compare(model.DataInizio, model.DataFine);

            if (result == 0 || result > 0)
            {
                Console.WriteLine("date sbagliate");
                return RedirectToAction("Create");
            }
            else
            {
                // Create a new Order entity
                Loan loan = new Loan
                {
                    DataInizio = model.DataInizio,
                    DataFine = model.DataFine,
                    Totale = model.Totale,
                    UserId = _userManager.GetUserId(User)
                };

                // Add OrderItems to the Order entity
                foreach (var item in model.BooksAddedToCart)
                {
                    loan.LoanBooks.Add(new LoanBook
                    {
                        BookId = item.BookId
                    });
                }
                await _loanService.CreateAsync(loan);
                //-1 quantità libro
                foreach (var item in loan.LoanBooks!)
                {
                    var bookid = item.BookId;
                    Book book = await _bookService.GetByIdAsync(bookid);
                    book.Quantita -= 1;
                    await _bookService.UpdateAsync(bookid,book);
                }

                // Clear the OrderViewModel from session or other state management
                HttpContext.Session.Remove("CreateLoanViewModel");

                // Redirect to the Order Confirmation page
                return RedirectToAction("Index");
            }          
            
        }


        //GET: Loans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loan = await _loanService.GetByIdAsync((int)id);
            if (loan == null)
            {
                return NotFound();
            }

            return View(loan);
        }

       // POST: Loans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loan = await _loanService.GetByIdAsync((int)id);
            if (await _loanService.LoanExists(id))
            {
                //quantità libro +1
                foreach(var book in loan.bookList!)
                {
                    await _bookService.UpdateQuantitaByIdAsync(book.BookId, 1);
                }
                await _loanService.DeleteAsync(id);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
