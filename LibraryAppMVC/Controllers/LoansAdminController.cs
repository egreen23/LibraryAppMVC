using LibraryAppMVC.IServices;
using LibraryAppMVC.Models;
using LibraryAppMVC.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAppMVC.Controllers
{
    [Authorize(Roles = SD.Role_Admin)]
    public class LoansAdminController : Controller
    {
        private readonly ILoanService _loanService;
        private readonly IBookService _bookService;
        private readonly UserManager<ApplicationUser> _userManager;

        public LoansAdminController(ILoanService loanService, IBookService bookService, UserManager<ApplicationUser> userManager)
        {
            _loanService = loanService;
            _bookService = bookService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {

            var loans = await _loanService.GetAllAsync();
            return View(loans);
        }

        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loandetails = await _loanService.GetByIdAsync((int)id);

            if (loandetails == null)
            {
                return NotFound();
            }

            return View(loandetails);
        }

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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loan = await _loanService.GetByIdAsync((int)id);
            if (await _loanService.LoanExists(id))
            {
                //quantità libro +1
                foreach (var book in loan.bookList!)
                {
                    await _bookService.UpdateQuantitaByIdAsync(book.BookId, 1);
                }
                await _loanService.DeleteAsync(id);
            }
            return RedirectToAction(nameof(Index));
        }


    }
}
