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
using LibraryAppMVC.ViewModels.Author;
using LibraryAppMVC.Services;
using Microsoft.AspNetCore.Authorization;
using LibraryAppMVC.Utility;

namespace LibraryAppMVC.Controllers
{
    [Authorize(Roles = SD.Role_Admin)]
    public class AuthorsController : Controller
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        // GET: Authors
        public async Task<IActionResult> Index()
        {
            return View(await _authorService.GetAllAuthorsAsync());
        }

        // GET: Authors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _authorService.GetByIdAsync((int)id);

            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // GET: Authors/Create
        public IActionResult Create()
        {
            CreateAuthorViewModel authorvm = new CreateAuthorViewModel();
            return View(authorvm);
        }

        // POST: Authors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAuthorViewModel createAuthorModel)
        {
            if (ModelState.IsValid)
            {
                if(createAuthorModel.isAlive == false)
                {
                    int result = DateTime.Compare(createAuthorModel.DoB, createAuthorModel.DoD);
                    if (result == 0 || result > 0)
                    {
                        Console.WriteLine("date incorrette, nessuno muore prima di nascere");
                        return RedirectToAction("Create");
                    }
                    else
                    {
                        await _authorService.CreateAsync(createAuthorModel);
                        return RedirectToAction(nameof(Index));
                    }
                }  
                else
                {
                    await _authorService.CreateAsync(createAuthorModel);
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(createAuthorModel);
        }

        // GET: Authors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _authorService.GetByIdAsync((int)id);
            if (author == null)
            {
                return NotFound();
            }

            var UpdateAuthorVM = new UpdateAuthorViewModel
            {
                Id = author.Id,
                Nome = author.Nome,
                Cognome = author.Cognome,
                Nazionalita = author.Nazionalita,
                DoB = author.DoB,
                LuogoNascita = author.LuogoNascita,
            };

            if (author.DoD == null) {
                UpdateAuthorVM.DoD = DateTime.Now;
                UpdateAuthorVM.isAlive = true;
            } else
            {
                UpdateAuthorVM.DoD = (DateTime)author.DoD;
                UpdateAuthorVM.isAlive = false;
            }

            return View(UpdateAuthorVM);
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateAuthorViewModel updateAuthorModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var author = new Author
                    {
                        Id = updateAuthorModel.Id,
                        Nome = updateAuthorModel.Nome,
                        Cognome= updateAuthorModel.Cognome, 
                        Nazionalita = updateAuthorModel.Nazionalita,
                        DoB = updateAuthorModel.DoB,
                        LuogoNascita = updateAuthorModel.LuogoNascita
                    };
                    if (updateAuthorModel.isAlive == true)
                    {
                        author.DoD = null;
                    }
                    else
                    {
                        author.DoD = updateAuthorModel.DoD;
                    }
                    await _authorService.UpdateAsync(updateAuthorModel.Id, author);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(await _authorService.AuthorExists(updateAuthorModel.Id)))
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
            return View(updateAuthorModel);
        }

        // GET: Authors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _authorService.GetByIdAsync((int)id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _authorService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
