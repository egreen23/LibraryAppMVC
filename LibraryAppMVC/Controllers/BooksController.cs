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
using System.Collections;
using LibraryAppMVC.ViewModels.Book;

namespace LibraryAppMVC.Controllers
{
   // [Route("books")]
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IAuthorService _authorService;
        
        public BooksController(IAuthorService authorService, IBookService bookService)
        {
            _authorService = authorService;
            _bookService = bookService;
        }

        // GET: Books
        //public async Task<IActionResult> Index()
        //{
        //    var libraryDbContext = _context.Books.Include(b => b.Author);
        //    return View(await libraryDbContext.ToListAsync());
        //}
      //  [HttpGet]
        public async Task<IActionResult> Index()
        {
            var books = await _bookService.GetAllAsync();
            List<IndexBookViewModel> booksVM = new List<IndexBookViewModel>();
            foreach(var  b in books)
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
                booksVM.Add(item);
            }

            return View(booksVM);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            var book = await _bookService.GetByIdAsync(id);

            var bookVM = new IndexBookViewModel
            {
                Id = book.Id,
                Titolo = book.Titolo,
                Quantita = book.Quantita,
                DataPubblicazione = book.DataPubblicazione,
                Genere = book.Genere,
                Prezzo = book.Prezzo,
                AuthorFullname = book.Author.FullName
            };

            if (book == null)
            {
                return NotFound();
            }

            return View(bookVM);
        }

        //// GET: Books/Create
        public async Task<IActionResult> Create()
        {
            var authors = await _authorService.GetAllAuthorsAsync();
            //ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Cognome");
            var createVM = new CreateBookViewModel
            {
                AuthorsList = new SelectList(authors, "Id", "FullName")
            };
            return View(createVM);
        }

        //// POST: Books/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titolo,Quantita,DataPubblicazione,Genere,Prezzo,AuthorId")] Book book)
        {
            if (ModelState.IsValid)
            {
                await _bookService.CreateAsync(book);
                return RedirectToAction(nameof(Index));
            }
            //SelectList(IEnumerable, String, String, Object) Inizializza una nuova istanza della SelectList classe usando gli elementi specificati per l'elenco, il campo valore dati, il campo testo dati e un valore selezionato.
            //ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Cognome", book.AuthorId); 
            return View(book);
        }

        //// GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var book = await _context.Books.FindAsync(id);
            var book = await _bookService.GetByIdAsync((int)id);
            var authors = await _authorService.GetAllAuthorsAsync();

            if (book == null)
            {
                return NotFound();
            }

            var updateVM = new UpdateBookViewModel
            {
                Id = book.Id,
                Titolo = book.Titolo,
                Quantita = book.Quantita,
                DataPubblicazione = book.DataPubblicazione,
                Genere = book.Genere,
                Prezzo = book.Prezzo,
                AuthorsList = new SelectList(authors, "Id", "FullName", book.AuthorId)
            };
            //ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Cognome", book.AuthorId);
            return View(updateVM);
        }

        //// POST: Books/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titolo,Quantita,DataPubblicazione,Genere,Prezzo,AuthorId")] Book book)
        {
            //if (id != book.Id)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                try
                {
                //    //_context.Update(book);
                //    //await _context.SaveChangesAsync();
                        await _bookService.UpdateAsync(id, book);
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    
                    if (!(await _bookService.BookExists(id)))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            //ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Cognome", book.AuthorId);
            return View(book);
        }

        //// GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _bookService.GetByIdAsync((int)id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        //// POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _bookService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }


    }
}
