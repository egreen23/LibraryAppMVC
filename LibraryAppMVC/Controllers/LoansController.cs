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

namespace LibraryAppMVC.Controllers
{
    public class LoansController : Controller
    {
        private readonly LibraryDbContext _context;
        private readonly ILoanService _loanService;
        private readonly IBookService _bookService;

        public LoansController(LibraryDbContext context, ILoanService loanService, IBookService bookService)
        {
            _context = context;
            _loanService = loanService;
            _bookService = bookService;
        }

        // GET: Loans
        public async Task<IActionResult> Index()
        {
            var loans = await _loanService.GetAllAsync();
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

        // GET: Loans/Create
        public async Task<IActionResult> Create()
        {
            var allBooks = await _bookService.GetAllAsync();

            var creatLoanVM = new CreateLoanViewModel
            {
                Books = new SelectList(allBooks, "Id", "Titolo")

                
            };
           //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Cognome");
            return View(creatLoanVM);
            
        }
      
        public async Task<IActionResult> Add(CreateLoanViewModel model)
        {
            if (model.BookId == null) return View(model);


            int addbookid = (int)model.BookId;

            var addbook = await _bookService.GetByIdAsync(addbookid);

            model.booksAdded.Add(addbook);

            var allBooks = await _bookService.GetAllAsync();

            model.Books = new SelectList(allBooks, "Id", "Titolo");

            return View("Create",model);

        }

        // POST: Loans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Totale,DataInizio,DataFine,UserId")] Loan loan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Cognome", loan.UserId);
            return View(loan);
        }

        // GET: Loans/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var loan = await _context.Loans.FindAsync(id);
        //    if (loan == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["UserId"] = new SelectList(_context.Users, "Id", "Cognome", loan.UserId);
        //    return View(loan);
        //}

        // POST: Loans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Totale,DataInizio,DataFine,UserId")] Loan loan)
        //{
        //    if (id != loan.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(loan);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!LoanExists(loan.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["UserId"] = new SelectList(_context.Users, "Id", "Cognome", loan.UserId);
        //    return View(loan);
        //}

        // GET: Loans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loan = await _context.Loans
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var loan = await _context.Loans.FindAsync(id);
            if (loan != null)
            {
                _context.Loans.Remove(loan);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
