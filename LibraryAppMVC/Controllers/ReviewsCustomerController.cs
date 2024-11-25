using LibraryAppMVC.IServices;
using LibraryAppMVC.Models;
using LibraryAppMVC.Utility;
using LibraryAppMVC.ViewModels.Reviews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;

namespace LibraryAppMVC.Controllers
{
    [Authorize(Roles = SD.Role_User)]
    public class ReviewsCustomerController : Controller
    {
        private readonly IReviewService _reviewService;
        private readonly IBookService _bookService;
        private readonly UserManager<ApplicationUser> _userManager;

        
        public ReviewsCustomerController(IReviewService reviewService, IBookService bookService, UserManager<ApplicationUser> userManager)
        {
            _bookService = bookService;
            _userManager = userManager;
            _reviewService = reviewService;
        }

        public async Task<IActionResult> Index()
        {
            string UserId = _userManager.GetUserId(User);


            var reviews = await _reviewService.GetAllByUserAsync(UserId);

            var reviewVM = new List<IndexReviewViewModel>();

            foreach (var review in reviews)
            {
                var r = new IndexReviewViewModel
                {
                    Id = review.Id,
                    BookId = review.Book.Id,
                    UserName = review.User.UserName,
                    TitoloBook = review.Book.Titolo
                };

                reviewVM.Add(r);
            }

            return View(reviewVM);
        }

        public async Task<IActionResult> Details(int id)
        {
            string UserId = _userManager.GetUserId(User);

            var review = await _reviewService.GetByIdByUserAsync(UserId, id);

            if (review == null) return NotFound();

            var reviewVM = new IndexReviewViewModel
            {
                Id = review.Id,
                TitoloBook = review.Book.Titolo,
                UserName = review.User.UserName,
                Testo = review.Testo
            };
            return View(reviewVM);
        }

        public async Task<IActionResult> Create()
        {
            var books = await _bookService.GetAllAsync();
            var createVM = new CreateReviewViewModel
            {
                BooksList = new SelectList(books, "Id", "Titolo")
            };
            return View(createVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateReviewViewModel reviewViewModel)
        {
            if (ModelState.IsValid)
            {
                string UserId = _userManager.GetUserId(User);
                var review = new Review
                {
                    Testo = reviewViewModel.Testo,
                    BookId = reviewViewModel.BookId,
                    UserID = UserId
                };
                var result = await _reviewService.CreateReviewAsync(review);
                return RedirectToAction(nameof(Index));

            }
            return RedirectToAction(nameof(Create));

        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _reviewService.GetByIdAsync((int)id);
            if (review == null)
            {
                return NotFound();
            }

            var books = await _bookService.GetAllAsync();

            var UpdateReviewVM = new UpdateReviewViewModel
            {
                Id = review.Id,
                Testo = review.Testo,
                TitoloBook = review.Book.Titolo,
                BookId = review.Book.Id,
                UserName = review.User.UserName,
                BooksList = new SelectList(books, "Id", "Titolo", review.Book.Id)
            };

            return View(UpdateReviewVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateReviewViewModel updateReviewModel)
        {
            if (!updateReviewModel.Testo.IsNullOrEmpty())
            {
                var reviewmodel = new Review
                {
                    Id = updateReviewModel.Id,
                    Testo = updateReviewModel.Testo,
                    BookId = updateReviewModel.BookId
                };

                var row = await _reviewService.UpdateReviewAsync(reviewmodel);

                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Create));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _reviewService.GetByIdAsync((int)id);
            if (review == null)
            {
                return NotFound();
            }

            var deleteVM = new IndexReviewViewModel
            {
                Id = review.Id,
                Testo = review.Testo,
                TitoloBook = review.Book.Titolo,
                BookId = review.Book.Id,
                UserName = review.User.UserName
            };

            return View(deleteVM);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _reviewService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
