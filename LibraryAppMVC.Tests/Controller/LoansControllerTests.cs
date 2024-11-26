using LibraryAppMVC.Controllers;
using LibraryAppMVC.IServices;
using LibraryAppMVC.Models;
using LibraryAppMVC.ViewModels.Author;
using LibraryAppMVC.ViewModels.Loan;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryAppMVC.Tests.Controller
{
    public class LoansControllerTests
    {
        //[Fact]
        //public async Task Index_ReturnsAViewResult_WithAListOfUserLoans()
        //{
        //    //Arrange
        //    var mockServiceLoan = new Mock<ILoanService>();
        //    var mockServiceBook = new Mock<IBookService>();
        //    //var mockServiceUser = new Mock<UserManager<ApplicationUser>>(MockBehavior.Default);
        //    var ls = new List<ApplicationUser> { new ApplicationUser() };
        //    var _userManager = MockUserManager<ApplicationUser>(ls).Object;
        //    var userId = "user123";
        //    // Mock UserManager to return the fake user ID
        //    //mockServiceUser.Setup(m => m.GetUserId(It.IsAny<System.Security.Claims.ClaimsPrincipal>())).Returns(userId);
        //    //_userManager.GetUserId(It.IsAny<System.Security.Claims.ClaimsPrincipal>()).Return(userId);
        //    mockServiceLoan.Setup(serv => serv.GetAllByUserIdAsync(userId)).ReturnsAsync(GetLoansTest());
        //    var controller = new LoansController(mockServiceLoan.Object, mockServiceBook.Object,_userManager);
        //    //Act
        //    var result = await controller.Index();

        //    //Asserts
        //    var viewResult = Assert.IsType<ViewResult>(result);
        //    var model = Assert.IsAssignableFrom<IEnumerable<IndexLoanViewModel>>(
        //        viewResult.ViewData.Model);
        //}

        //public static Mock<UserManager<TUser>> MockUserManager<TUser>(List<TUser> ls) where TUser : class
        //{
        //    var store = new Mock<IUserStore<TUser>>();
        //    var mgr = new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
        //    mgr.Object.UserValidators.Add(new UserValidator<TUser>());
        //    mgr.Object.PasswordValidators.Add(new PasswordValidator<TUser>());

        //    mgr.Setup(x => x.DeleteAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);
        //    mgr.Setup(x => x.CreateAsync(It.IsAny<TUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success).Callback<TUser, string>((x, y) => ls.Add(x));
        //    mgr.Setup(x => x.UpdateAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);

        //    return mgr;
        //}

        //private List<IndexLoanViewModel> GetLoansTest()
        //{
        //    var loans = new List<IndexLoanViewModel>();
        //    var lb = new LoanBook
        //    {
        //        Id = 1,
        //        BookId = 1,
        //        LoanId = 1
        //    };
        //    var fatt = new Loan
        //    {
        //        Totale = 5.50m,
        //        DataInizio = DateTime.Parse("20-11-2023"),
        //        DataFine = DateTime.Parse("10-01-2024"),
        //        User = new ApplicationUser
        //        {
        //            Id = "a"
        //        },
        //        LoanBooks = new LoanBook[]
        //        {
        //            lb
        //        },

        //    };
        //    loans.Add(fatt)

        //}

        private List<IndexLoanViewModel> GetLoansTest()
        {
            var loans = new List<IndexLoanViewModel>();
            var l1 = new IndexLoanViewModel
            {
                Id = 1,
                Totale = 1,
                DataFine = DateTime.Now.AddDays(1),
                DataInizio = DateTime.Now,
                UserFullName = "Test"
            };
            var l2 = new IndexLoanViewModel
            {
                Id = 2,
                Totale = 1,
                DataFine = DateTime.Now.AddDays(1),
                DataInizio = DateTime.Now,
                UserFullName = "Test2"
            };
            loans.Add(l1);
            loans.Add(l2);
            return loans;
        }
    }


}
